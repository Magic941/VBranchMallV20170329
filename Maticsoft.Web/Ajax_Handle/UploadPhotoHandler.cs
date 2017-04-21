/**
* UploadPhotoHandler.cs
*
* 功 能： [N/A]
* 类 名： UploadPhotoHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/4 17:28:22  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Maticsoft.Common;

namespace Maticsoft.Web.Ajax_Handle
{
    public class UploadPhotoHandler : UploadImageHandlerBase
    {
        protected UploadPhotoHandler()
        {
            base.makeThumbnailMode = MakeThumbnailMode.Auto;
        }

        protected override Size GetThumbImageSize()
        {
            return new Size( Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageWidth"),0),Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageHeight"),0));
        }

        protected override Size GetNormalImageSize()
        {
            return new Size(Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageWidth"), 0), Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("NormalImageHeight"), 0));
        }

        protected override void ProcessSub(HttpContext context, string fileName)
        {
            HttpRequest Request = context.Request;

            if (string.IsNullOrWhiteSpace(Request.Params["album"])) return;
            string albumId = Request.Params["album"];

            if (string.IsNullOrWhiteSpace(Request.Params["userId"])) return;
            string userId = Request.Params["userId"];

            string folder = @context.Request.Params["folder"];

            BLL.CMS.Photo bll = new BLL.CMS.Photo();
            HttpPostedFile file = context.Request.Files["Filedata"];
            Model.CMS.Photo model = new Model.CMS.Photo
            {
                PhotoName = file.FileName,
                ImageUrl = folder + "/" + fileName,
                Description = "",
                AlbumID = int.Parse(albumId),
                State = 1,
                CreatedUserID = int.Parse(userId),
                CreatedDate = DateTime.Now,
                PVCount = 0,
                ClassID = 1,
                ThumbImageUrl = folder + "/" + "T_" + fileName,
                NormalImageUrl = folder + "/" + "N_" + fileName,
                Sequence = bll.GetMaxSequence(),
                IsRecomend = false,
                CommentCount = 0,
                Tags = ""
            };

            int iId = bll.Add(model);

            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            context.Response.Write(iId.ToString());
        }
    }
}