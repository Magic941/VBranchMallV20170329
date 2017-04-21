/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01 2012年10月25日 15:27:56 Rock 初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Web.UI;

namespace Maticsoft.Web.Admin.Members.UsersApprove
{
    public partial class Show : PageBaseAdmin
    {
        public string strid = "";
        protected override int Act_PageLoad { get { return 306; } } //用户管理_实名认证管理_详细页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int ApproveID = (Convert.ToInt32(strid));
                    ShowInfo(ApproveID);
                }
            }
        }

        private void ShowInfo(int ApproveID)
        {
            Maticsoft.BLL.Members.UsersApprove bll = new Maticsoft.BLL.Members.UsersApprove();
            Maticsoft.Model.Members.UsersApprove model = bll.GetModel(ApproveID);
            this.lblApproveID.Text = model.ApproveID.ToString();
            this.lblUserID.Text = GetUserName(model.UserID);
            this.lblTrueName.Text = model.TrueName;
            this.lblIDCardNum.Text = model.IDCardNum;
            this.ImageFrontView.ImageUrl= model.FrontView;
            this.ImageRearView.ImageUrl = model.RearView;
            this.lblDueDate.Text = model.DueDate.ToString();
            this.lblStatus.Text = model.Status.ToString();
            this.lblApproveUserID.Text =GetUserName( model.ApproveUserID);
            this.lblUserType.Text = model.UserType.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            this.lblApproveDate.Text = model.ApproveDate.ToString();
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        protected string GetUserName(int userId)
        {
            if (userId > 0)
            {
                BLL.Members.Users user = new BLL.Members.Users();
                Model.Members.Users model = user.GetModel(userId);
                if (model != null)
                {
                    return model.UserName;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}