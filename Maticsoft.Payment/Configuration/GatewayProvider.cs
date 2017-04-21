/**
* GatewayProvider.cs
*
* 功 能： 支付网关处理
* 类 名： GatewayProvider
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;

namespace Maticsoft.Payment.Configuration
{
    /// <summary>
    /// 支付网关
    /// </summary>
    public class GatewayProvider
    {
        private string displayName;
        private string name;
        private string notifyType;
        private NameValueCollection providerAttributes;
        private string requestType;
        private IList<string> supportedCurrencys;
        private bool useNotifyMode;

        public GatewayProvider(XmlAttributeCollection attributes)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException("attributes");
            }
            this.name = attributes["name"].Value.ToLower();
            this.requestType = attributes["requestType"].Value;
            this.notifyType = attributes["notifyType"].Value;
            this.displayName = attributes["displayName"].Value;

            this.useNotifyMode = attributes["useNotifyMode"] != null &&
                Core.Globals.SafeBool(attributes["useNotifyMode"].Value, false);

            string[] strArray = attributes["supportedCurrency"].Value.Split(new char[] { ',' });
            this.supportedCurrencys = new List<string>();
            foreach (string str in strArray)
            {
                this.supportedCurrencys.Add(str);
            }
            this.providerAttributes = new NameValueCollection();
            foreach (XmlAttribute attribute in attributes)
            {
                if ((((attribute.Name != "name") &&
                    (attribute.Name != "displayName")) &&
                    ((attribute.Name != "requestType") &&
                    (attribute.Name != "notifyType"))) &&
                    (attribute.Name != "supportedCurrency"))
                {
                    this.providerAttributes.Add(attribute.Name, attribute.Value);
                }
            }
        }

        public NameValueCollection Attributes
        {
            get
            {
                return this.providerAttributes;
            }
        }

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public string NotifyType
        {
            get
            {
                return this.notifyType;
            }
        }

        public string RequestType
        {
            get
            {
                return this.requestType;
            }
        }

        public IList<string> SupportedCurrencys
        {
            get
            {
                return this.supportedCurrencys;
            }
        }

        public bool UseNotifyMode
        {
            get { return useNotifyMode; }
        }
    }
}

