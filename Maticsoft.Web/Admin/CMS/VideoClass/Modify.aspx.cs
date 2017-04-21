/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify
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
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.CMS.VideoClass
{
    public partial class Modify : PageBaseAdmin
    {
        Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
        protected override int Act_PageLoad { get { return 264; } }  
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
            Maticsoft.Model.CMS.VideoClass model = bll.GetModel(VideoClassID);

            if (null != model)
            {
                this.txtVideoClassName.Text = model.VideoClassName;
            }
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string videoclassname = this.txtVideoClassName.Text.Trim();

            string strErr = "";

            if (videoclassname.Length == 0)
            {
                strErr +=Resources.CMSVideo.ErrorClassNameNull+"\\n";
            }

            if (strErr != "")
            {
                MessageBox.Show(this, strErr);
                return;
            }

            Maticsoft.Model.CMS.VideoClass model = bll.GetModel(VideoClassID);
            if (null == model)
            {
                return;
            }
            model.VideoClassName = videoclassname;
          
            if (bll.Update(model))
            {
                MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                //Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "list.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
        }
    }
}
