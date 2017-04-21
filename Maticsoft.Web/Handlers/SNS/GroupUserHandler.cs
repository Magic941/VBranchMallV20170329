using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;
using Maticsoft.Common;

namespace Maticsoft.Web.Handlers.SNS
{
    public class GroupUserHandler : HandlerBase
    {

        //
        // GET: /GroupUserHandler/
        Maticsoft.BLL.SNS.GroupUsers groupUserBll = new BLL.SNS.GroupUsers();

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
                   

                    case "RecommandUser":
                        RecommandUser(context);
                        break;

                    case "SetAdmin":
                        SetAdmin(context);
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

        #region 实现方法


        private void RecommandUser(HttpContext context)
        {
            int GroupID = Globals.SafeInt(context.Request.Params["GroupID"], 0);
            int UserID = Globals.SafeInt(context.Request.Params["UserID"], 0);
            int Recommand= Globals.SafeInt(context.Request.Params["recommand"], 0);
            JsonObject json = new JsonObject();
            if (groupUserBll.UpdateRecommand(GroupID, UserID,Recommand))
            {
                json.Accumulate("STATUS", "OK");
            }
            else
            {
                json.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(json.ToString());
        }
        private void SetAdmin(HttpContext context)
        {
            int GroupID = Globals.SafeInt(context.Request.Params["GroupID"], 0);
            int UserID = Globals.SafeInt(context.Request.Params["UserID"], 0);
            int Role = Globals.SafeInt(context.Request.Params["Role"], 0);
            JsonObject json = new JsonObject();
            if (groupUserBll.UpdateRole(GroupID, UserID,Role))
            {
                json.Accumulate("STATUS", "OK");
            }
            else
            {
                json.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(json.ToString());
        
        
        
        
        
        }

        //private void EditStatus(HttpContext context)
        //{
        //    int PhotoID = Globals.SafeInt(context.Request.Params["PhotoID"], 0);
        //    int Recomend = Globals.SafeInt(context.Request.Params["Status"], 0);
        //    JsonObject json = new JsonObject();
        //    if (groupUserBll.UpdateRecomend(PhotoID, Recomend))
        //    {
        //        json.Accumulate("STATUS", "OK");
        //    }
        //    else
        //    {
        //        json.Accumulate("STATUS", "NODATA");
        //    }
        //    context.Response.Write(json.ToString());
        //}
        #endregion
    }
}
