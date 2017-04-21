using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Activity
{
    public partial class UpdateCount : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Activity.ActivityAward awardBll = new Maticsoft.WeChat.BLL.Activity.ActivityAward();
        private Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll = new Maticsoft.WeChat.BLL.Activity.ActivityInfo();
        private Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll = new Maticsoft.WeChat.BLL.Activity.ActivityCode();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Maticsoft.WeChat.Model.Activity.ActivityAward awardModel = awardBll.GetModel(AwardId);
                if (awardModel != null)
                {
                    this.txtName.Text = awardModel.AwardName;
                    this.txtGiftName.Text = awardModel.GiftName;
                }
            }
        }

        /// <summary>
        /// 活动编号
        /// </summary>
        protected int AwardId
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.Model.Activity.ActivityAward awardModel = awardBll.GetModel(AwardId);
            if (awardModel == null)
                return;
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetModel(awardModel.ActivityId);
            if (infoModel == null)
                return;
            int count = Common.Globals.SafeInt(this.txtCount.Text, 0);
            awardModel.Count = awardModel.Count+count;
            if (awardBll.Update(awardModel))
            {
                Random rnd = new Random();
                List<string> codeList = new List<string>();
                Maticsoft.WeChat.Model.Activity.ActivityCode codeModel = null;
                int maxValue = 10;
                for (int j = 1; j < 8; j++)
                {
                    maxValue = maxValue * 10;
                }
                for (int i = 0; i < count; i++)
                {
                    codeModel = new Maticsoft.WeChat.Model.Activity.ActivityCode();
                    int rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                    codeModel.CodeName = infoModel.PreName + DateTime.Now.ToString("MMdd") +
                                           rand.ToString();
                    while (codeList.Contains(codeModel.CodeName))
                    {
                        rand = rnd.Next(maxValue / 10 + 1, maxValue - 1);
                        codeModel.CodeName = infoModel.PreName + DateTime.Now.ToString("MMdd") +
                                          rand.ToString();
                    }
                    codeList.Add(codeModel.CodeName);
                    codeModel.ActivityName = infoModel.Name;
                    codeModel.GenerateDate = DateTime.Now;
                    codeModel.ActivityId = infoModel.ActivityId;
                    codeModel.AwardId = awardModel.AwardId;
                    codeModel.AwardName = awardModel.AwardName;
                    codeModel.EndDate = infoModel.EndDate;
                    codeModel.StartDate = infoModel.StartDate;
                    codeModel.IsPwd = infoModel.IsPwd;
                    codeModel.UserName = "";
                    codeModel.Phone = "";
                    codeModel.ActivityPwd = "";
                    codeModel.Status = 0;
                    codeModel.UserId = "";
                    codeBll.Add(codeModel);
                }
                Common.MessageBox.ShowSuccessTipScript(this, "增发成功", "window.parent.location.reload();");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        }

    }
}