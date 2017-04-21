/**
* GetData.cs
*
* 功 能： [N/A]
* 类 名： GetData
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/6 14:03:51  孙鹏    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Service
{
    /// <summary>
    /// GetData 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
     [System.Web.Script.Services.ScriptService]
    public class GetData : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string GetEnteName(string prefixText, int limit)
        {
            if (string.IsNullOrWhiteSpace(prefixText)) return string.Empty;
            string encodeText = this.HtmlEncode(prefixText);
            BLL.Ms.Enterprise bll = new BLL.Ms.Enterprise();
            DataSet ds = bll.GetEnteName(encodeText, limit);
            Maticsoft.Json.JsonArray jsonList = new Maticsoft.Json.JsonArray();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string tmpProductName = ds.Tables[0].Rows[i]["Name"].ToString();
                    if (CheckHtmlCode(ref tmpProductName, prefixText))
                    {
                        Maticsoft.Json.JsonObject json = new Maticsoft.Json.JsonObject();
                        json.Accumulate("name", tmpProductName);
                        jsonList.Add(json);
                    }
                }
            }
            return jsonList.ToString();
        }

        [WebMethod]
        public string GetUserName(string prefixText, int limit)
        {
            if (string.IsNullOrWhiteSpace(prefixText)) return string.Empty;
            string encodeText = this.HtmlEncode(prefixText);
            BLL.Members.UsersExp bll = new BLL.Members.UsersExp();
            DataSet ds = bll.GetUserName(encodeText, limit);
            Maticsoft.Json.JsonArray jsonList = new Maticsoft.Json.JsonArray();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string tmpProductName = ds.Tables[0].Rows[i]["UserName"].ToString();
                    if (CheckHtmlCode(ref tmpProductName, prefixText))
                    {
                        Maticsoft.Json.JsonObject json = new Maticsoft.Json.JsonObject();
                        json.Accumulate("name", tmpProductName);
                        jsonList.Add(json);
                    }
                }
            }
            return jsonList.ToString();
        }

        /// <summary>
        /// HTML编码 (防注入)
        /// </summary>
        /// <param name="prefixText"></param>
        private string HtmlEncode(string prefixText)
        {
            return InjectionFilter.QuoteFilter(HttpUtility.HtmlEncode(prefixText));
        }

        /// <summary>
        /// 检测并过滤不存在的HTML转义符
        /// </summary>
        /// <param name="tmpStr">Check内容(HTML解码)</param>
        /// <param name="value">输入值</param>
        private bool CheckHtmlCode(ref string tmpStr, string value)
        {
            //HTML解码
            tmpStr = HttpUtility.HtmlDecode(tmpStr);
            //忽略大小写
            return (tmpStr.StartsWith(value, true, System.Globalization.CultureInfo.CurrentCulture));
        }
    }
}
