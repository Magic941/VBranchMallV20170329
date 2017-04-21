using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json;
using Maticsoft.Common;

namespace Maticsoft.Web.Handlers
{
    public class UploadWeChatImgHandler : UploadImageHandlerBase
    {
        public new const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            JsonObject json = new JsonObject();
            json.Put(KEY_DATA, uploadPath + "{0}" + fileName);
            json.Put(KEY_SUCCESS, true);
            context.Response.Write(json.ToString());
        }
        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            //保存临时原图
            file.SaveAs(uploadPath + fileName);
            //生成临时缩略图
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "N_" + fileName, 360, 200, MakeThumbnailMode.Cut);
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T_" + fileName, 200, 200, MakeThumbnailMode.Cut);
        }
    }
}