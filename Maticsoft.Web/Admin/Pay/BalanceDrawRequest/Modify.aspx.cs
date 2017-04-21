/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify.cs
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
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Pay.BalanceDrawRequest
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 685; } } //Shop_提现管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (JournalNumber > 0)
                {
                    ShowInfo();
                }
                else
                {
                    hidColse.Value = "close";
                    MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！");
                }
            }
        }
        private int JournalNumber
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
	
	private void ShowInfo()
	{
		Maticsoft.BLL.Pay.BalanceDrawRequest bll=new BLL.Pay.BalanceDrawRequest();
		Maticsoft.Model.Pay.BalanceDrawRequest model=bll.GetModel(JournalNumber);
        if (model != null)
        {
            this.lblRequestTime.Text = model.RequestTime.ToString("yyyy-MM-dd HH:mm:ss");
            this.lblAmount.Text = model.Amount.ToString("F");
            this.lblUserID.Text = new BLL.Members.Users().GetUserName(model.UserID);
            this.lblTrueName.Text = model.TrueName;
            this.lblBankName.Text = model.BankName;
            this.lblBankCard.Text = model.BankCard;
            this.radioCardType.SelectedValue = model.CardTypeID.ToString();
            this.dropStatus.SelectedValue = model.RequestStatus.ToString();
            this.txtRemark.Text = model.Remark;
        }
        else
        {
            hidColse.Value = "close";
            MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！");
        }

	}

    #region 保存
    public void btnSave_Click(object sender, EventArgs e)
    {
        string strErr = ""; 
        if (this.txtRemark.Text.Trim().Length >2000)
        {
            strErr += "备注过长！\\n";
        }
        if (strErr != "")
        {
            MessageBox.Show(this, strErr);
            return;
        }
        int RequestStatus = int.Parse(dropStatus.SelectedValue);
        string Remark = this.txtRemark.Text;
        Maticsoft.BLL.Pay.BalanceDrawRequest bll = new  BLL.Pay.BalanceDrawRequest();
        Maticsoft.Model.Pay.BalanceDrawRequest model = bll.GetModelByCache(JournalNumber);
        if (model != null)
        {
            model.RequestStatus = RequestStatus;
            model.Remark = Remark;
            if (bll.Update(model))
            {
                hidColse.Value = "close";
                DataCache.DeleteCache("BalanceDrawRequestModel-" + JournalNumber);//清除缓存
                MessageBox.ShowSuccessTip(this, "保存成功！");
            }
            else
            {
                MessageBox.ShowFailTip(this, "保存失败！");
            }

        }
        else
        {
            hidColse.Value = "close";
            MessageBox.ShowServerBusyTip(this, "该记录不存在或已被删除！");
        }


    }
    #endregion

    public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
