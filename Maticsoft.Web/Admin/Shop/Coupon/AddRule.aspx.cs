using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.BLL.Shop.Supplier;

namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class AddRule : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 416; } } //Shop_优惠券规则管理_添加页
        private  Maticsoft.BLL.Shop.Coupon.CouponClass classBll=new CouponClass();
        private  Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll=new CouponRule();
        private  Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierBll=new SupplierInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //获取是否隐藏分类和商家
                this.hfCategory.Value = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_CouponRule_IsCategory");
                hfSupplier.Value = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_CouponRule_IsSupplier");

                //获取优惠券分类
                this.ddlClass.DataSource = classBll.GetList(" Status=1");
                ddlClass.DataTextField = "Name";
                ddlClass.DataValueField = "ClassId";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0,new ListItem("请选择","0"));
                //获取商家
                this.ddlSupplier.DataSource = supplierBll.GetList("Status=1");
                ddlSupplier.DataTextField = "Name";
                ddlSupplier.DataValueField = "SupplierId";
                ddlSupplier.DataBind();
                ddlSupplier.Items.Insert(0, new ListItem("请选择", "0"));
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
          Maticsoft.Model.Shop.Coupon.CouponRule ruleModel=new Model.Shop.Coupon.CouponRule();
            string name = txtName.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Common.MessageBox.ShowFailTip(this,"请填写优惠券名称");
                return;
            }
            decimal limitPrice = Common.Globals.SafeDecimal(txtLimitPrice.Text, -1);
            if (limitPrice == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写最低消费金额");
                return;
            }
            decimal price = Common.Globals.SafeDecimal(txtPrice.Text, -1);
            if (price==-1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写消费券面值");
                return;
            }
            if ((String.IsNullOrWhiteSpace(txtStartDate.Text) || String.IsNullOrWhiteSpace(txtEndDate.Text))&&!chkNoDate.Checked)
            {
                Common.MessageBox.ShowFailTip(this, "请选择优惠券使用时间");
                return;
            }
            int count = Common.Globals.SafeInt(txtSendCount.Text,0);
            if (count == 0 && !chkExchange.Checked)
            {
                Common.MessageBox.ShowFailTip(this, "请填写生成数量");
                return;
            }
            int point = Common.Globals.SafeInt(txtPoint.Text, 0);
            if (point == 0 && chkExchange.Checked)
            {
                Common.MessageBox.ShowFailTip(this, "请填写兑换所需积分");
                return;
            }
            ruleModel.Name = name;
            ruleModel.NeedPoint = chkExchange.Checked ? point : 0;
            ruleModel.IsPwd = chkIsPwd.Checked ? 1 : 0;
            ruleModel.IsReuse = chkIsReuse.Checked ? 1 : 0;
            ruleModel.LimitPrice = limitPrice;
            ruleModel.PreName =Common.InjectionFilter.SqlFilter(txtPreName.Text);
            ruleModel.SendCount = chkExchange.Checked ? 0 : count;
            ruleModel.StartDate =chkNoDate.Checked?DateTime.Now: Common.Globals.SafeDateTime(txtStartDate.Text, DateTime.Now);
            ruleModel.Status = chkStatus.Checked ? 1 : 0;
            ruleModel.SupplierId = Common.Globals.SafeInt(ddlSupplier.SelectedValue, 0);
            ruleModel.CategoryId = Common.Globals.SafeInt(ddlCateList.SelectedValue, 0);
            ruleModel.ClassId = Common.Globals.SafeInt(ddlClass.SelectedValue, 0);
            ruleModel.CouponPrice = price;
            ruleModel.CreateDate = DateTime.Now;
            ruleModel.CreateUserId = CurrentUser.UserID;
            ruleModel.Type = chkExchange.Checked ? 1 : 0;
            ruleModel.EndDate = chkNoDate.Checked?DateTime.MaxValue:Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.MaxValue);
            ruleModel.CpLength = Common.Globals.SafeInt(ddlLength.SelectedValue, 0);
            ruleModel.PwdLength = Common.Globals.SafeInt(ddlPwd.SelectedValue, 0);
            ruleModel.UseType = Convert.ToInt16(ddlUseType.SelectedValue);
            ruleModel.AutoState = Convert.ToInt16(ddlAutoType.SelectedValue);
            if (ruleBll.AddEx(ruleModel))
            {
                Common.MessageBox.ShowSuccessTip(this, "生成优惠券成功", "CouponList.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "生成优惠券失败");
            }
        }

    }
}