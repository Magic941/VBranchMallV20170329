using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Services;
using Maticsoft.Model.Shop.Coupon;
using Maticsoft.Accounts.Bus;
using Maticsoft.Model.Members;


namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class CouponSendOut : PageBaseAdmin
    {
        APIHelper AHelper = new APIHelper(System.Configuration.ConfigurationManager.AppSettings["CardURL"]);
        public readonly Maticsoft.BLL.Shop.Coupon.CouponClass _couponBLL = new BLL.Shop.Coupon.CouponClass();
        public readonly Maticsoft.BLL.Shop_CouponRuleExt _couponExBLL = new BLL.Shop_CouponRuleExt();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定批次号
                List<string> batchList = AHelper.GetCardBatch();
                if (batchList.Count > 0)
                {
                    ddlBatch.DataSource = batchList;
                    ddlBatch.DataBind();
                }
                //绑定优惠券类别
                List<CouponClass> couponList = _couponBLL.GetModelList("");
                if (couponList.Count > 0)
                {
                    ddlRule.DataSource = couponList;
                    ddlRule.DataValueField = "ClassId";
                    ddlRule.DataTextField = "Name";
                    ddlRule.DataBind();
                }
                BindData();
                UserName.Text = string.IsNullOrWhiteSpace(CurrentUser.TrueName) ? CurrentUser.UserName : CurrentUser.TrueName; ;
            }

        }

        public void BindData()
        {
            //List<Shop_CouponRuleExt> CouponRuleList = _couponExBLL.GetModelList("");
            var ds = _couponExBLL.GetList();
            this.gv_CouponList.DataSource = ds;
            this.gv_CouponList.DataKeyNames = new string[] { "id" };
            this.gv_CouponList.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string batchId = this.ddlBatch.SelectedValue.Replace('"', ' ').Trim();
                string ClassId = this.ddlRule.SelectedValue;
                string Quantity = this.txtQuantity.Text.Trim();
                _couponExBLL.Add(new Shop_CouponRuleExt { batchID = batchId, CouponCount = int.Parse(Quantity), ClassID = int.Parse(ClassId) });
                BindData();
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('添加成功');</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + ex + "');</script>");
            }
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.Shop.Coupon.CouponInfo info = new BLL.Shop.Coupon.CouponInfo();
            Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
            string useremail = UserName.Text;
            string couponCode = CouponCode.Text;
            //var user = new AccountsPrincipal(useremail);
            var id = userBll.GetUserIdByUserEmail(useremail);
            if (id != 0)
            {
                info.BindCoupon(couponCode, id);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('无此用户');</script>");
            }
        }

        protected void gv_CouponList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            var id = this.gv_CouponList.DataKeys[e.RowIndex].Value.ToString();
            _couponExBLL.Delete(int.Parse(id));
            this.BindData();
        }

    }
}