using System;
using System.Drawing;
using System.Web;
using System.Collections.Generic;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Handlers.SNS
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadImageHandler : UploadImageHandlerBase
    {
        public new const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";
        public UploadImageHandler()
        {
            string StoreWay = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay");
            if (StoreWay == "1")
            {
                IsLocalSave = false;
                ApplicationKeyType = ApplicationKeyType.SNS;
            }
        }
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            try
            {
                JsonObject json = new JsonObject();
                string StoreWay = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay");
                if (StoreWay != "1")
                {
                    json.Put(KEY_DATA, uploadPath + "{0}" + fileName);
                }
                else
                {
                    json.Put(KEY_DATA, fileName);
                }
                json.Put(KEY_SUCCESS, true);
                context.Response.Write(json.ToString());
            }
            catch (Exception)
            {
                
            }
        }
        protected override List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Model.Ms.EnumHelper.AreaType.SNS,MvcApplication.ThemeName);
        }
       
    }
}