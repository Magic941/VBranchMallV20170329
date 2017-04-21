using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Members;
using Maticsoft.BLL.Shop.Sales;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.WholeSale
{
    public partial class AddRule : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 557; } } //Shop_批发规则管理_添加页
        Maticsoft.BLL.Members.UserRank rankBll = new UserRank();
        Maticsoft.BLL.Shop.Sales.SalesRule ruleBll = new BLL.Shop.Sales.SalesRule();
        Maticsoft.BLL.Shop.Sales.SalesItem itemBll = new SalesItem();
        Maticsoft.BLL.Shop.Sales.SalesUserRank userRankBll = new SalesUserRank();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //获取普通会员等级
                DataSet ds = rankBll.GetList(" RankType=0");
                this.ChkUserRank.DataSource = ds;
                ChkUserRank.DataTextField = "Name";
                ChkUserRank.DataValueField = "RankId";
                ChkUserRank.DataBind();
                //经销商等级  没有值先隐藏
                //this.ChkDealerRank.DataSource = rankBll.GetList(" RankType=1");
                //ChkDealerRank.DataTextField = "Name";
                //ChkDealerRank.DataValueField = "RankId";
                //ChkDealerRank.DataBind();

            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Sales.SalesRule ruleModel = new Model.Shop.Sales.SalesRule();
            ruleModel.RuleName = this.txtRuleName.Text;
            ruleModel.RuleMode = Common.Globals.SafeInt(this.radMode.SelectedValue, 0);
            ruleModel.RuleUnit = Common.Globals.SafeInt(this.radUnit.SelectedValue, 0);
            ruleModel.Status = Common.Globals.SafeInt(this.radStatus.SelectedValue, 0);
            ruleModel.CreatedDate = DateTime.Now;
            ruleModel.CreatedUserID = CurrentUser.UserID;
            string hfItems = this.hfItems.Value;
            if (String.IsNullOrWhiteSpace(hfItems))
            {
                MessageBox.ShowFailTip(this, "请填写优惠规则项");
                return;
            }
            //添加批发规则
            int ruleId = ruleBll.Add(ruleModel);
            if (ruleId > 0)
            {
                //添加规则的优惠项
                var ItemList = hfItems.Split(',');
                int itemtype = Common.Globals.SafeInt(this.radItemType.SelectedValue, 0);
                foreach (var item in ItemList)
                {
                    Maticsoft.Model.Shop.Sales.SalesItem itemModel = new Model.Shop.Sales.SalesItem();
                    itemModel.ItemType = itemtype;
                    itemModel.UnitValue = Common.Globals.SafeInt(item.Split('|')[0], 0);
                    itemModel.RateValue = Common.Globals.SafeInt(item.Split('|')[1], 0);
                    itemModel.RuleId = ruleId;
                    itemBll.Add(itemModel);
                }
                //添加规则的对应的用户等级和经销商等级
                for (int i = 0; i < ChkUserRank.Items.Count; i++)
                {
                    if (ChkUserRank.Items[i].Selected)
                    {
                        Maticsoft.Model.Shop.Sales.SalesUserRank userRankModel = new Model.Shop.Sales.SalesUserRank();
                        userRankModel.RankId = Common.Globals.SafeInt(ChkUserRank.Items[i].Value, 0);
                        userRankModel.RuleId = ruleId;
                        userRankBll.Add(userRankModel);
                    }
                }
                //for (int i = 0; i < ChkDealerRank.Items.Count; i++)
                //{
                //    if (ChkDealerRank.Items[i].Selected)
                //    {
                //        Maticsoft.Model.Shop.Sales.SalesUserRank userRankModel = new Model.Shop.Sales.SalesUserRank();
                //        userRankModel.RankId = Common.Globals.SafeInt(ChkUserRank.Items[i].Value, 0);
                //        userRankModel.RuleId = ruleId;
                //        userRankBll.Add(userRankModel);
                //    }
                //}
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }

        }
    }
}