using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Activity
{
    public partial class AddInfo : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll = new Maticsoft.WeChat.BLL.Activity.ActivityInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            
            }
        }
        /// <summary>
        /// 活动类型
        /// </summary>
        protected int Type
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    id = Globals.SafeInt(Request.Params["type"], 0);
                }
                return id;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = new Maticsoft.WeChat.Model.Activity.ActivityInfo();
            string name = txtName.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Common.MessageBox.ShowFailTip(this, "请填写活动名称");
                return;
            }
            int eachCount=Common.Globals.SafeInt(this.txtEachCount.Text,0);
            if (eachCount==0)
            {
                Common.MessageBox.ShowFailTip(this, "请填写每个人参与次数");
                return;
            }
            decimal probability = Common.Globals.SafeDecimal(this.txtProbability.Text, 0);
            if (probability == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请填写获奖几率");
                return;
            }
        
            if ((String.IsNullOrWhiteSpace(txtStartDate.Text) || String.IsNullOrWhiteSpace(txtEndDate.Text)) && !chkNoDate.Checked)
            {
                Common.MessageBox.ShowFailTip(this, "请选择活动时间");
                return;
            }
            #region  图片处理
            string savePath = "/Upload/WeChat/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            //移动图片 
            string tempImg = this.hfPath.Value;
            string imgname = tempImg.Substring(tempImg.LastIndexOf("/") + 1);
            string saveImg = tempImg;
            if (!String.IsNullOrWhiteSpace(tempImg) && tempImg.Contains("/Upload/Temp"))
            {
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(tempImg, "N_"))))
                {
                    string originalUrl = String.Format(savePath + imgname, "N_");
                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(tempImg, "N_")), HttpContext.Current.Server.MapPath(originalUrl));
                }
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(tempImg, "T_"))))
                {
                    string originalUrl = String.Format(savePath + imgname, "T_");
                    System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(tempImg, "T_")), HttpContext.Current.Server.MapPath(originalUrl));
                }
                saveImg = savePath + imgname;
            }

            #endregion 
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            infoModel.Name = name;
            infoModel.IsPwd = chkIsPwd.Checked ;
            infoModel.PreName = txtPreName.Text;
            infoModel.StartDate = chkNoDate.Checked ? DateTime.Now : Common.Globals.SafeDateTime(txtStartDate.Text, DateTime.Now);
            infoModel.Status = 0;
            infoModel.OpenId = openId;
            infoModel.Probability = probability;
            infoModel.Summary = txtSummary.Text;
            infoModel.UserTotal = Common.Globals.SafeInt(this.txtUserTotal.Text, 0);
            infoModel.LimitType = Common.Globals.SafeInt(this.rblLimitType.SelectedValue, 0);
            infoModel.EachCount = eachCount;
            infoModel.DayTotal = Common.Globals.SafeInt(this.txtDayCount.Text, 0);
            infoModel.CreatedDate = DateTime.Now;
            infoModel.CreatedUserId = CurrentUser.UserID;
            infoModel.AwardType = Common.Globals.SafeInt(rblType.SelectedValue, 0);
            infoModel.ImageUrl = saveImg;
            infoModel.Type = Type;
            infoModel.EndDate = chkNoDate.Checked ? DateTime.MaxValue : Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.MaxValue);
            infoModel.PwdLength = Common.Globals.SafeInt(ddlPwd.SelectedValue, 0);
            if (infoBll.Add(infoModel)>0)
            {
                Common.MessageBox.ShowSuccessTip(this, "添加成功", "InfoList.aspx?type="+Type);
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        }

    }
}