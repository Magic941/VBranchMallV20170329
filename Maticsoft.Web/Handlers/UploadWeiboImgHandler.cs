using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers
{
    public class UploadWeiboImgHandler : UploadImageHandlerBase
    {
                //TODO: 请使用父类的 KEY_DATA/STATUS_SUCCESS TO: 涂 BEN ADD 20130416
        public new const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            try
            {
                JsonObject json = new JsonObject();
                    json.Put(KEY_DATA, uploadPath + "{0}" + fileName);
                json.Put(KEY_SUCCESS, true);
                context.Response.Write(json.ToString());
            }
            catch (Exception)
            {
                
            }
        }
    }
}