/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.CMS.VideoClass
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 265; } }  
		protected void Page_Load(object sender, EventArgs e)
		{
            if (!this.Page.IsPostBack)
            {
                ShowInfo();
            }
		}

        public int VideoClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
            Maticsoft.Model.CMS.VideoClass model = bll.GetModel(VideoClassID);
            if (null != model)
            {
                this.lblVideoClassName.Text = model.VideoClassName;
                if (model.ParentID.HasValue)
                {
                    this.lblParentID.Text = GetVideoClassNameByParentID(model.ParentID);
                }
                this.lblSequence.Text = model.Sequence.ToString();
                this.lblPath.Text = model.Path;
                this.lblDepth.Text = model.Depth.ToString();
            }
        }

        #region 根据ParentID得到一个对象实体
        /// <summary>
        /// 根据ParentID得到一个对象实体
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoClassNameByParentID(object target)
        {
            Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
            string videoClassName = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string str = target.ToString();
                if (PageValidate.IsNumber(str))
                {
                    Maticsoft.Model.CMS.VideoClass model = bll.GetModelByParentID(int.Parse(str));
                    if (null != model)
                    {
                        videoClassName = model.VideoClassName;
                    }
                }
            }
            return videoClassName;
        }
        #endregion
    }
}
