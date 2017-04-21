/**
* SupplierUploadLogoHandler.cs
*
* 功 能： [N/A]
* 类 名： SupplierUploadLogoHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/11 19:24:27  Rock    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.Shop
{
    public class SupplierUploadLogoHandler : UploadImageHandlerBase
    {
        protected SupplierUploadLogoHandler() : base(MakeThumbnailMode.Auto) { }

        public const string POLL_KEY_DATA = "data";
        public const string POLL_KEY_SUCCESS = "success";

        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            file.SaveAs(uploadPath + fileName);
            //对生成水印之后的图片进行缩略
            //ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T980X68_" + fileName, 980, 68, MakeThumbnailMode.Cut);// pc版店铺首页显示logo
            //ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T180X60_" + fileName, 180, 60, MakeThumbnailMode.Cut);//pc版搜索店铺显示logo
            //ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + "T80X80_" + fileName, 80, 80, MakeThumbnailMode.Cut);//手机版店铺首页显示logo  
        }
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            try
            {
                JsonObject json = new JsonObject();
                json.Put(POLL_KEY_SUCCESS, true);
                json.Put(POLL_KEY_DATA, uploadPath + "{0}" + fileName);
                context.Response.Write(json.ToString());
            }
            catch (Exception)
            {

            }
        }
        protected override List<Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return null;
        }
    }
}