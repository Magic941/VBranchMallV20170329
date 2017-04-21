using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json;
using Maticsoft.Common;

namespace Maticsoft.Web.Handlers.SNS
{
    public class PhotosHandler : HandlerBase
    {

        //定义一个全局变量(方便构造树形节点使用)
        Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
        #region IHttpHandler 成员

        public override bool IsReusable
        {
            get { return false; }
        }

        public override void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有产品相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            try
            {
                switch (action)
                {
                    #region 社区商品

                    case "EditRecomend":
                        EditRecomend(context);
                        break;

                    case "EditStatus":
                        EditStatus(context);
                        break;


                    #endregion 社区商品分类

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

        #endregion IHttpHandler 成员
        #region 实现方法


        private void EditRecomend(HttpContext context)
        {
            int PhotoID = Globals.SafeInt(context.Request.Params["PhotoID"], 0);
            int Recomend = Globals.SafeInt(context.Request.Params["Recomend"], 0);
            JsonObject json = new JsonObject();
            if (photoBll.UpdateRecomend(PhotoID, Recomend))
            {
                json.Accumulate("STATUS", "OK");
            }
            else
            {
                json.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(json.ToString());
        }

        private void EditStatus(HttpContext context)
        {
            int PhotoID = Globals.SafeInt(context.Request.Params["PhotoID"], 0);
            int Recomend = Globals.SafeInt(context.Request.Params["Status"], 0);
            JsonObject json = new JsonObject();
            if (photoBll.UpdateStatus(PhotoID, Recomend))
            {
                json.Accumulate("STATUS", "OK");
            }
            else
            {
                json.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(json.ToString());
        }
        #endregion
    }
}
