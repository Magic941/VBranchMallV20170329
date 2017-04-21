/**
* PayConfiguration.cs
*
* 功 能： 支付网关配置读取
* 类 名： PayConfiguration
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;
using Maticsoft.Payment.Core;

namespace Maticsoft.Payment.Configuration
{
    public class PayConfiguration
    {
        private Dictionary<string, string> _supportedCurrencies = new Dictionary<string, string>();
        public const string CacheKey = "Maticsoft_PayConfiguration";
        private IList<string> keys = new List<string>();
        private Hashtable providers = new Hashtable();
        private XmlDocument XmlDoc;

        public PayConfiguration(XmlDocument doc)
        {
            this.XmlDoc = doc;
            this.LoadValuesFromConfigurationXml();
        }

        public static PayConfiguration GetConfig()
        {
            PayConfiguration configuration = DataCache.Get("Maticsoft_PayConfiguration") as PayConfiguration;
            if (configuration == null)
            {
                string filename = null;
                //获取Gateway.config
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    filename = current.Request.MapPath("~/Gateway.config");
                }
                else
                {
                    filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gateway.config");
                }

                if (!File.Exists(filename))
                {
                    throw new FileNotFoundException("THE GATEWAY FILE NOT FOUND! PATH: " + filename);
                }

                XmlDocument doc = new XmlDocument();
                doc.Load(filename);
                configuration = new PayConfiguration(doc);
                DataCache.Max("Maticsoft_PayConfiguration", configuration, new CacheDependency(filename));
            }
            return configuration;
        }

        public XmlNode GetConfigSection(string nodePath)
        {
            return this.XmlDoc.SelectSingleNode(nodePath);
        }

        internal void GetCurrencies(XmlNode node)
        {
            foreach (XmlNode node2 in node.ChildNodes)
            {
                if ((string.Compare(node2.Attributes["enabled"].Value, "true", false, CultureInfo.InvariantCulture) == 0) && !this._supportedCurrencies.ContainsKey(node2.Attributes["code"].Value))
                {
                    this._supportedCurrencies.Add(node2.Attributes["code"].Value, node2.Attributes["symbol"].Value);
                }
            }
        }

        internal void GetProviders(XmlNode node, Hashtable table)
        {
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                XmlNode node2 = node.ChildNodes[i];
                string name = node2.Name;
                if (name != null)
                {
                    if (!(name == "add"))
                    {
                        if (name == "remove")
                        {
                            table.Remove(node2.Attributes["name"].Value.ToLower());
                            this.keys.Remove(node2.Attributes["name"].Value.ToLower());
                            continue;
                        }
                        if (name == "clear")
                        {
                            table.Clear();
                            this.keys.Clear();
                        }
                    }
                    else
                    {
                        table.Add(node2.Attributes["name"].Value.ToLower(), new GatewayProvider(node2.Attributes));
                        this.keys.Add(node2.Attributes["name"].Value.ToLower());
                    }
                }
                continue;
            }
        }

        internal void LoadValuesFromConfigurationXml()
        {
            foreach (XmlNode node2 in this.GetConfigSection("Gateway").ChildNodes)
            {
                if (node2.Name == "currencies")
                {
                    this.GetCurrencies(node2);
                }
                if (node2.Name == "providers")
                {
                    this.GetProviders(node2, this.providers);
                }
            }
        }

        public IList<string> Keys
        {
            get
            {
                return this.keys;
            }
        }

        public Hashtable Providers
        {
            get
            {
                return this.providers;
            }
        }

        public Dictionary<string, string> SupportedCurrencies
        {
            get
            {
                return this._supportedCurrencies;
            }
        }
    }
}

