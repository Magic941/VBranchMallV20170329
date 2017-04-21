using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json;
using Maticsoft.Common;
using System.Drawing;

namespace Maticsoft.Web.Handlers
{
    public class UploadGravatarHandler : UploadImageHandlerBase
    {
        //TODO: 请使用父类的 KEY_DATA/STATUS_SUCCESS TO: 涂 BEN ADD 20130416
        public new const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            JsonObject json = new JsonObject();
            json.Put(KEY_DATA, uploadPath + "T_" + fileName);
            json.Put(KEY_SUCCESS, true);
            context.Response.Write(json.ToString());
        }
        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            //保存临时原图
            file.SaveAs(uploadPath + fileName);
            //生成临时缩略图
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath +"T_"+ fileName, 420, 400, MakeThumbnailMode.Auto);
        }
    }
}