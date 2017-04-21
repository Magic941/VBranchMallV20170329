using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Members;
using Maticsoft.BLL.Shop.ActivityManage;
using Maticsoft.Common;
using System.Data.SqlTypes;

namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class EditGifts : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 558; } } //Shop_批发规则管理_编辑页

        Maticsoft.BLL.Shop.ActivityManage.AMBLL amBll = new BLL.Shop.ActivityManage.AMBLL();
        Maticsoft.BLL.Shop.ActivityManage.AMDetailBLL amdetailBll = new BLL.Shop.ActivityManage.AMDetailBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo(AMId);
            }
        }

        public int AMId
        {
            get
            {
                int AMId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    AMId = Globals.SafeInt(Request.Params["id"], 0);
                }
                return AMId;
            }
        }
        private void ShowInfo(int AMId)
        {
            //活动信息
            Maticsoft.Model.Shop.ActivityManage.AMModel amModel = amBll.GetModel(AMId);
            this.txtAMName.Text = amModel.AMName;
            this.txtAMLable.Text = amModel.AMLabel;
            this.txtAMStartdate.Text = amModel.AMStartDate.ToString("yyyy-MM-dd HH:mm:ss");
            this.txtAMEnddate.Text = amModel.AMEndDate.ToString("yyyy-MM-dd HH:mm:ss");
            //this.AMFreeShipment.SelectedValue = amModel.AMFreeShipment.ToString();
            this.AMApplyStyles.SelectedValue = amModel.AMApplyStyles.ToString();
            this.AMStatus.SelectedValue = amModel.AMStatus.ToString();
            this.AMUnit.SelectedValue = amModel.AMUnit.ToString();
            this.AMType.SelectedValue = amModel.AMType.ToString();

            //规则项信息
            List<Maticsoft.Model.Shop.ActivityManage.AMDetailModel> itemList = amdetailBll.GetModelList(" AMId=" + AMId);
            string hfItems = "";
            if (itemList != null && itemList.Count > 0)
            {
                //this.AMType.SelectedValue = itemList[0].AMType.ToString();
                foreach (var item in itemList)
                {
                    if (String.IsNullOrWhiteSpace(hfItems))
                    {
                        hfItems = item.AMDUnitValue + "|" + item.AMDRateValue;
                    }
                    else
                    {
                        hfItems = hfItems + "," + item.AMDUnitValue + "|" + item.AMDRateValue;
                    }
                }
            }
            this.hfItems.Value = hfItems;

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.ActivityManage.AMModel ammodel = amBll.GetModel(AMId);
            ammodel.AMType = Common.Globals.SafeInt(this.AMType.SelectedValue, 0);  //活动类型 0 折扣，1 减价
            ammodel.AMName = txtAMName.Text;   //活动名称
            ammodel.AMLabel = txtAMLable.Text;    //活动标签
            ammodel.AMStartDate = DateTime.Parse(txtAMStartdate.Text); //开始时间 "yyyy-MM-dd HH:mm:ss"
            ammodel.AMEndDate = DateTime.Parse(txtAMEnddate.Text);  //结束时间
            ammodel.AMUnit = Common.Globals.SafeInt(this.AMUnit.SelectedValue, 0); //规则单位
            //ammodel.AMFreeShipment = Common.Globals.SafeInt(this.AMFreeShipment.SelectedValue, 1);//是否包邮
            ammodel.AMApplyStyles = Common.Globals.SafeInt(this.AMApplyStyles.SelectedValue, 0); //应用方式，1 全场 ，2 商家 3 单品
            ammodel.AMStatus = Common.Globals.SafeInt(this.AMStatus.SelectedValue, 0); // 是否启用
            ammodel.AMUserId = CurrentUser.UserID.ToString(); //用户

            string hfItems = this.hfItems.Value;
            if (String.IsNullOrWhiteSpace(hfItems))
            {
                MessageBox.ShowFailTip(this, "请填写活动优惠规则项");
                return;
            }
            //编辑活动规则

            if (amBll.Update(ammodel))
            {
                //编辑规则的优惠项   （先删除以前的，然后重新添加）
                var ItemList = hfItems.Split(',');
                //int itemtype = Common.Globals.SafeInt(this.radItemType.SelectedValue, 0);
                amdetailBll.DeleteByAMId(ammodel.AMId);
                foreach (var item in ItemList)
                {
                    Maticsoft.Model.Shop.ActivityManage.AMDetailModel amdetailModel = new Model.Shop.ActivityManage.AMDetailModel();
                    //amdetailModel.AMDType = itemtype;
                    amdetailModel.AMDUnitValue = Common.Globals.SafeInt(item.Split('|')[0], 0);
                    amdetailModel.AMDRateValue = Common.Globals.SafeInt(item.Split('|')[1], 0);
                    amdetailModel.AMId = ammodel.AMId;
                    amdetailBll.Add(amdetailModel);
                }

                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }

        }
    }
}