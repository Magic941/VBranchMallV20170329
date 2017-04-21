using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.PostMsg
{
    public partial class AddPostMsg : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.PostMsg postMsgBll = new Maticsoft.WeChat.BLL.Core.PostMsg();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        #region 编号

        /// <summary>
        /// 编号
        /// </summary>
        protected int RuleId
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
            Maticsoft.WeChat.Model.Core.PostMsg postMsgModel = new Maticsoft.WeChat.Model.Core.PostMsg();
            postMsgModel.Description = this.tName.Text;
            postMsgModel.RuleId = RuleId;
            postMsgModel.MsgType = "text";
            postMsgModel.CreateTime = DateTime.Now;
            if (postMsgBll.Add(postMsgModel) > 0)
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "$('#txtResetLoad', window.parent.document).click();");
                //Response.Redirect("GroupList.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "操作失败！");
            }
        }
    }
}