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
    public partial class AddGifts : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 557; } } //Shop_活动规则管理_添加页
        Maticsoft.BLL.Shop.ActivityManage.AMBLL amBLL = new AMBLL();
        Maticsoft.BLL.Shop.ActivityManage.AMDetailBLL amdBLL = new AMDetailBLL();
        //Maticsoft.BLL.Shop.Sales.SalesItem itemBll = new SalesItem();
        //Maticsoft.BLL.Shop.Sales.SalesUserRank userRankBll = new SalesUserRank();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.ActivityManage.AMModel ammodel = new Model.Shop.ActivityManage.AMModel();
            ammodel.AMType = Common.Globals.SafeInt(this.AMType.SelectedValue, 0);
            ammodel.AMName = this.txtAMName.Text;   //活动名称
            ammodel.AMLabel = this.txtAMLable.Text;    //活动标签
            ammodel.AMStartDate = DateTime.Parse(this.txtAMStartdate.Text); //开始时间

            ammodel.AMEndDate = DateTime.Parse(this.txtAMEnddate.Text);  //结束时间
            ammodel.AMUnit = Common.Globals.SafeInt(this.AMUnit.SelectedValue, 0); //规则单位
            ammodel.AMFreeShipment = 0;//是否包邮
            ammodel.AMApplyStyles = Common.Globals.SafeInt(this.AMApplyStyles.SelectedValue, 0); //应用方式 ，1 全场商品 ，2 单个商家，3 单个商品
            ammodel.AMStatus = Common.Globals.SafeInt(this.AMStatus.SelectedValue, 0); // 是否启用
            ammodel.AMUserId = CurrentUser.UserID.ToString(); //用户
            ammodel.AMCreateDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            string hfItems = this.hfItems.Value;
            if (String.IsNullOrWhiteSpace(hfItems))
            {
                MessageBox.ShowFailTip(this, "请填写活动优惠规则项");
                return;
            }
            //添加活动规则
            int AMId = amBLL.Add(ammodel);
            if (AMId > 0)
            {
                //添加规则的优惠项
                var amList = hfItems.Split(',');
                int amType = Common.Globals.SafeInt(this.AMType.SelectedValue, 0);
                foreach (var item in amList)
                {
                    Maticsoft.Model.Shop.ActivityManage.AMDetailModel AMDModel = new Model.Shop.ActivityManage.AMDetailModel();

                    AMDModel.AMDUnitValue = Common.Globals.SafeInt(item.Split('|')[0], 0);
                    AMDModel.AMDRateValue = Common.Globals.SafeInt(item.Split('|')[1], 0);
                    AMDModel.AMId = AMId;
                    amdBLL.Add(AMDModel);
                }

                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }

        }

        protected void AddZhu_Click(object sender, EventArgs e)
        {

        }
    }
}