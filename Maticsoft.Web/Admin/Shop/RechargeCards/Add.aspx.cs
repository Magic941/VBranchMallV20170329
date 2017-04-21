/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
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
namespace Maticsoft.Web.Admin.Shop.RechargeCards
{
    public partial class Add : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal amount = Common.Globals.SafeDecimal(txtAmount.Text, -1);
            if (amount == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填充值卡金额");
                return;
            }
            int count = Common.Globals.SafeInt(txtCount.Text, -1);
            if (count == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填生成充值卡数量");
                return;
            }
            string preName = this.txtPreName.Text.Trim();
            preName=string.IsNullOrWhiteSpace(preName) ? "CZK" : preName;
            int numberLength = Maticsoft.Common.Globals.SafeInt(this.ddlLength.SelectedValue, 0);
            int pwdLength = Globals.SafeInt(this.ddlPwd.SelectedValue, 0);
          
            Maticsoft.Model.Shop.RechargeCards model = new Maticsoft.Model.Shop.RechargeCards();
            model.Amount = amount;
            model.CreatedDate = DateTime.Now;
            model.CreatedUserId = CurrentUser.UserID;
            model.Remark = this.txtRemark.Text;
            model.Status = 0;//0为未使用

            Maticsoft.BLL.Shop.RechargeCards.RechargeCards bll = new Maticsoft.BLL.Shop.RechargeCards.RechargeCards();
            if (bll.AddEx(model, numberLength, pwdLength, count, preName))
            {
                Common.MessageBox.ShowSuccessTip(this, "生成充值卡成功", "List.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this,"生成充值卡失败");
            }

        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
