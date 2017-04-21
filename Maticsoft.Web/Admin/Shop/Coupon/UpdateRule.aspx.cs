using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.BLL.Shop.Supplier;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class UpdateRule : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 416; } } //Shop_优惠券规则管理_添加页
        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new CouponRule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowInfo();
             
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

        private void ShowInfo()
        {
            Maticsoft.Model.Shop.Coupon.CouponRule ruleModel = ruleBll.GetModel(RuleId);
            lblCpLength.Text = ruleModel.CpLength.ToString();
            lblLimitPrice.Text = ruleModel.LimitPrice.ToString("F");
            lblName.Text = ruleModel.Name;
            lblPreName.Text = ruleModel.PreName;
            lblPrice.Text = ruleModel.CouponPrice.ToString("F");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Coupon.CouponRule ruleModel = ruleBll.GetModel(RuleId);

            int sendCount = Common.Globals.SafeInt(this.txtSendCount.Text, 0);
            if (sendCount == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请填写生成数量");
                return;
            }
            if (ruleBll.UpdateEx(ruleModel, sendCount))
            {
                MessageBox.ShowSuccessTipScript(this, "生成优惠券成功！", "window.parent.location.reload();");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "生成优惠券失败");
            }
        }

    }
}