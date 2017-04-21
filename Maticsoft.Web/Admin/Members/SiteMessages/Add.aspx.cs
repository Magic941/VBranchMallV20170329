using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.SiteMessages
{
    public partial class Add : PageBaseAdmin
    {
        Maticsoft.BLL.Members.Users UserBll = new BLL.Members.Users();
        Maticsoft.BLL.Members.SiteMessage SiteMsgBll = new BLL.Members.SiteMessage();
        Maticsoft.Model.Members.SiteMessage SitemMsgModel = new Model.Members.SiteMessage();
        Maticsoft.Accounts.Bus.UserType UserType = new Maticsoft.Accounts.Bus.UserType();
        protected override int Act_PageLoad { get { return 302; } }//客服管理_站内信_发消息页
        public int SystemUserId= -1;  //系统消息使用的UserId
        protected void Page_Load(object sender, EventArgs e)
        {  
            
            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                    return;
                }
                this.BindToChkList();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        { 
          
            string Title = this.txtTitle.Text;
            string Content = this.txtContent.Text;
            DateTime SendTime = DateTime.Now;
            SitemMsgModel.Title = Title;
            SitemMsgModel.Content = Content;
            SitemMsgModel.SenderID = this.SystemUserId;
            SitemMsgModel.SendTime = SendTime;
            SitemMsgModel.ReaderIsDel = false;
            SitemMsgModel.ReceiverIsRead = false;
            SitemMsgModel.SenderIsDel = false;
            if (txtContent.Text.Length <= 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入内容");
                return;
            }
            //群发用户类型
            if (rbUserType.Checked)
            {
                string userType = this.ddlUserType.SelectedValue;
                List<Maticsoft.Model.Members.Users> userList = UserBll.GetModelList("UserType= '" + userType + "'");
                if (userList != null && userList.Count > 0)
                {
                    foreach (var user in userList)
                    {
                        SitemMsgModel.ReceiverID = user.UserID;
                        SitemMsgModel.MsgType = user.UserType;
                        SiteMsgBll.Add(SitemMsgModel);
                    }
                }
            }
            if (rbUser.Checked)
            {
                if (String.IsNullOrWhiteSpace(txtUser.Text))
                {
                    MessageBox.ShowFailTip(this, "请输入制定用户昵称或者邮箱" );
                    return;
                }
                string[] users = txtUser.Text.Split(';');
                Maticsoft.Model.Members.Users userModel = null;
                foreach (string user in users)
                {
                    userModel = UserBll.GetModel(user);

                    if (userModel!= null)
                    {
                        SitemMsgModel.ReceiverID = userModel.UserID;
                        SiteMsgBll.Add(SitemMsgModel);
                    }

                
                   
                   
                }
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "发送成功！", "add.aspx");

        }

        private void BindToChkList()
        {

            this.ddlUserType.DataSource = UserType.GetAllList();
            this.ddlUserType.DataTextField = "Description";
            this.ddlUserType.DataValueField = "UserType";
            this.DataBind();
        
        }
        public void UserType_Changed(object sender, EventArgs e)
        {
            if (rbUser.Checked)
            {
                txtUser.Enabled = true;
                ddlUserType.Enabled = false;
            }
            else
            {
                txtUser.Enabled = false;
                ddlUserType.Enabled = true;
            }
        }

        
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

    }
}
