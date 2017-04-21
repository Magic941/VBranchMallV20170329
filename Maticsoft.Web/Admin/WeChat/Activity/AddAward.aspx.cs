using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Data;

namespace Maticsoft.Web.Admin.WeChat.Activity
{
    public partial class AddAward : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Activity.ActivityAward awardBll = new Maticsoft.WeChat.BLL.Activity.ActivityAward();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //如果设置获奖类型为 优惠券，就获取所有的所有可用的优惠券类型
                if (AwardType == 1)
                {
                    trGiftName.Visible=false;
                    trCount.Visible=false;
                    Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new BLL.Shop.Coupon.CouponRule();
                    DataSet ds = ruleBll.GetList(" Status=1");
                     this.ddlCoupon.DataSource = ds;
                     this.ddlCoupon.DataTextField = "Name";
                     this.ddlCoupon.DataValueField = "RuleId";
                     this.ddlCoupon.DataBind();
                     this.ddlCoupon.Items.Insert(0, new ListItem("请选择", "0"));
                }
                else
                {
                    trCoupon.Visible=false;
                }
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
        /// 奖品类型
        /// </summary>
        protected int AwardType
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
            Maticsoft.WeChat.Model.Activity.ActivityAward awardModel = new Maticsoft.WeChat.Model.Activity.ActivityAward();
            string name = txtName.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Common.MessageBox.ShowFailTip(this, "请填写奖品类型");
                return;
            }
            string giftName=this.txtGiftName.Text;
            if (String.IsNullOrWhiteSpace(giftName) && AwardType==0)
            {
                Common.MessageBox.ShowFailTip(this, "请填写奖品名称");
                return;
            }
            int count = Common.Globals.SafeInt(this.txtCount.Text, 0);
            if (count == 0 && AwardType == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请填写奖品数量");
                return;
            }     
            int ruleId=Common.Globals.SafeInt(ddlCoupon.SelectedValue,0);
            if(ruleId==0&&AwardType==1)
            {
               Common.MessageBox.ShowFailTip(this, "请选择优惠券活动");
                return;
            }
            awardModel.ActivityId = ActivityId;
            awardModel.AwardDesc = this.txtAwardDesc.Text;
            awardModel.AwardName = name;
            awardModel.Count = count;
            awardModel.GiftName = AwardType == 1 ? ddlCoupon.SelectedItem.Text : giftName;
            awardModel.TargetId = ruleId;
            if (awardBll.Add(awardModel) > 0)
            {
                Common.MessageBox.ShowSuccessTipScript(this, "添加成功", "window.parent.location.reload();");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        }

    }
}