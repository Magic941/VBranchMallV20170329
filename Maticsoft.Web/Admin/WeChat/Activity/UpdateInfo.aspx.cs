using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.IO;

namespace Maticsoft.Web.Admin.WeChat.Activity
{
    public partial class UpdateInfo : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll = new Maticsoft.WeChat.BLL.Activity.ActivityInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
            }
        }
        /// <summary>
        /// 活动编号
        /// </summary>
        protected int ActivityId
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
        /// <summary>
        /// 显示信息
        /// </summary>
        private void ShowInfo()
        {
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetModel(ActivityId);
            if (infoModel != null)
            {
                this.txtDayCount.Text = infoModel.DayTotal.ToString();
                this.txtEachCount.Text = infoModel.EachCount.ToString();
                this.txtEndDate.Text = infoModel.EndDate.ToString("yyyy-MM-dd");
                this.txtPreName.Text = infoModel.PreName;
                this.txtProbability.Text = infoModel.Probability.ToString("F");
                this.txtStartDate.Text = infoModel.StartDate.ToString("yyyy-MM-dd");
                this.txtSummary.Text = infoModel.Summary;
                this.txtUserTotal.Text = infoModel.UserTotal.ToString();
                this.rblLimitType.SelectedValue = infoModel.LimitType.ToString();
                this.rblStatus.SelectedValue = infoModel.Status.ToString();
                this.txtName.Text = infoModel.Name;
                this.hfPath.Value = infoModel.ImageUrl;
                this.rblType.SelectedValue = infoModel.AwardType.ToString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetModel(ActivityId);
            string name = txtName.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Common.MessageBox.ShowFailTip(this, "请填写活动名称");
                return;
            }
            int eachCount = Common.Globals.SafeInt(this.txtEachCount.Text, 0);
            if (eachCount == 0)
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

            infoModel.Name = name;
            infoModel.IsPwd = chkIsPwd.Checked;
            infoModel.PreName = txtPreName.Text;
            infoModel.StartDate = chkNoDate.Checked ? DateTime.Now : Common.Globals.SafeDateTime(txtStartDate.Text, DateTime.Now);
            infoModel.Probability = probability;
            infoModel.Summary = txtSummary.Text;
            infoModel.UserTotal = Common.Globals.SafeInt(this.txtUserTotal.Text, 0);
            infoModel.LimitType = Common.Globals.SafeInt(this.rblLimitType.SelectedValue, 0);
            infoModel.EachCount = eachCount;
            infoModel.ImageUrl = saveImg;
            infoModel.DayTotal = Common.Globals.SafeInt(this.txtDayCount.Text, 0);
            infoModel.Status = Common.Globals.SafeInt(rblStatus.SelectedValue, 0);  
            infoModel.EndDate = chkNoDate.Checked ? DateTime.MaxValue : Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.MaxValue);
            infoModel.PwdLength = Common.Globals.SafeInt(ddlPwd.SelectedValue, 0);
            if (infoBll.Update(infoModel))
            {
                Common.MessageBox.ShowSuccessTip(this, "操作成功 ", "InfoList.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        }

    }
}