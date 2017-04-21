/**
* ProductUploadImgHandler.cs
*
* 功 能： 产品上传图片
* 类 名： ProductUploadImgHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/1 19:52:00    Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Collections.Generic;
using System.Drawing;
using System.Web;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using System;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.CMS
{
    public class UploadArticleImgHandler : UploadImageHandlerBase
    {

        protected UploadArticleImgHandler() : base(MakeThumbnailMode.Auto) { }

        protected override List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Model.Ms.EnumHelper.AreaType.CMS, MvcApplication.ThemeName);
        }
        public const string POLL_KEY_DATA = "data";
        public const string POLL_KEY_SUCCESS = "success";
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

    }
}