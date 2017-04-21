/**
* Product.cs
*
* 功 能： [N/A]
* 类 名： Product
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/20 18:04:38  huhy    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.Shop
{

    public class ProductHandler : HandlerBase
    {
        #region HandlerBase
        public override bool IsReusable
        {
            get { return false; }
        }
        public override void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            //安全起见, 所有产品相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];
            context.Response.Clear();
            context.Response.ContentType = "application/json";
            try
            {
                switch (action)
                {
                    case "GetSKUByProductId":
                        GetSKUByProductId(context);
                        break;
                    case "MaxSequence":
                        GetMaxSequence(context);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(KEY_STATUS, STATUS_ERROR);
                json.Put(KEY_DATA, ex);
                context.Response.Write(json.ToString());
            }
        }
        #endregion


        #region GetSKUByProductId
        private void GetSKUByProductId(HttpContext context)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region GetMaxSequence 得到一个类别下商品的最大顺序
        /// <summary>
        /// 得到一个类别下商品的最大顺序
        /// </summary>
        /// <param name="context"></param>
        private void GetMaxSequence(HttpContext context)
        {
            Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();
            JsonObject json = new JsonObject();
            string CategoryPath = context.Request.Form["CategoryPath"];
            json.Put(KEY_STATUS, STATUS_SUCCESS);
            json.Put(KEY_DATA, productBll.MaxSequence(CategoryPath) + 1);
            context.Response.Write(json);
        }
        #endregion


    }
}