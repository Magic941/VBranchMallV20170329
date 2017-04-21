using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.User
{
    public partial class BindUser : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 652; } } //移动营销_微信用户管理_绑定用户页
        private Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
        Maticsoft.WeChat.BLL.Core.User wUserBll = new Maticsoft.WeChat.BLL.Core.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddUser.DataSource = userBll.GetModelList("Activity='True'");
                ddUser.DataTextField = "UserName";
                ddUser.DataValueField = "UserID";
                ddUser.DataBind();
            }
        }
        #region 编号

        /// <summary>
        /// 编号
        /// </summary>
        protected int UserId
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
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.Model.Core.User userModel = wUserBll.GetModel(UserId);
            int userId = Common.Globals.SafeInt(ddUser.SelectedValue, 0);
            Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User(userId);
            userModel.UserId = userId;
            userModel.NickName = user.NickName;
            if (wUserBll.Update(userModel))
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                //Response.Redirect("GroupList.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "操作失败！");
            }
        }
    }
}