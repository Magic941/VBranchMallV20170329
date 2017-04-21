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

using System.Web;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Model.Settings;

namespace Maticsoft.Web.Handlers
{
    public class UploadNormalImgHandler : UploadImageHandlerBase
    {
        protected UploadNormalImgHandler() : base(MakeThumbnailMode.Auto) { }

        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            context.Response.Write("1|" + uploadPath + "{0}" + fileName);
        }

        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            //保存临时原图
            file.SaveAs(uploadPath + fileName);
        }

    }
}