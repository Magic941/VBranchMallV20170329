using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.WeChat.BLL.Core;

namespace Maticsoft.Web.Admin.WeChat.PostMsg
{
    public partial class UpdateRule : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 643; } } //移动营销_智能客服设置管理_编辑页
        Maticsoft.WeChat.BLL.Core.KeyRule ruleBll=new KeyRule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Maticsoft.WeChat.Model.Core.KeyRule ruleModel = ruleBll.GetModel(RuleId);
                if (ruleModel != null)
                {
                    this.tName.Text = ruleModel.Name;
                    this.tDesc.Text = ruleModel.Remark;
                }
            }

        }

        public int RuleId
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
         //编辑规则
        protected void btnSave_Click(object sender, EventArgs e)
        {
             string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            Maticsoft.WeChat.Model.Core.KeyRule ruleModel = ruleBll.GetModel(RuleId);
            ruleModel.Name = this.tName.Text;
            ruleModel.Remark = this.tDesc.Text;
            ruleModel.OpenId = openId;
            if (ruleBll.Update(ruleModel))
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败！");
            }

        }
    }
}