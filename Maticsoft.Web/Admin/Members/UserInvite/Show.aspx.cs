/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  
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

namespace Maticsoft.Web.Admin.Members.UserInvite
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 689; } }//邀请管理_详细页
        public string strid = "";
        private int InviteId
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (InviteId > 0)
                {
                    ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！", "list.aspx");
                }

            }
        }

        private void ShowInfo()
        {
            Maticsoft.BLL.Members.UserInvite bll = new BLL.Members.UserInvite();
            Maticsoft.Model.Members.UserInvite model = bll.GetModel(InviteId);
            if (null != model)
            {
                this.lblInviteId.Text = model.InviteId.ToString();
                this.lblUserId.Text = model.UserId.ToString();
                this.lblUserNick.Text = model.UserNick;
                this.lblInviteUserId.Text = model.InviteUserId.ToString();
                this.lblInviteNick.Text = model.InviteNick;
                this.lblIsRebate.Text = model.IsRebate ? "是" : "否";
                this.lblIsNew.Text = model.IsNew ? "是" : "否";
                this.lblCreatedDate.Text = model.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                this.lblRemark.Text = model.Remark;
                this.lblRebateDesc.Text = model.RebateDesc;
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！", "list.aspx");
            }
        }




        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
