/**
* Add.cs
*
* 功 能： 新增图片
* 类 名： Add
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 21:48:37  伍伟    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.Photo
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 236; } } //CMS_图片管理_添加页
        protected string strUserId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BLL.CMS.PhotoAlbum albumBll = new BLL.CMS.PhotoAlbum();
                this.DropAlbum.DataSource = albumBll.GetList("");
                this.DropAlbum.DataTextField = "AlbumName";
                this.DropAlbum.DataValueField = "AlbumID";
                this.DropAlbum.DataBind();
                this.DropAlbum.SelectedValue = this.AlbumId.ToString();

                strUserId = CurrentUser.UserID.ToString();
            }
        }

        public int AlbumId
        {
            get
            {
                int iAlbumId = 0;
                if (Request.Params["AlbumID"] != null && PageValidate.IsNumber(Request.Params["AlbumID"]))
                {
                    iAlbumId = int.Parse(Request.Params["AlbumID"]);
                }
                return iAlbumId;
            }
        }
    }
}
