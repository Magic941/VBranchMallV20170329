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
    public partial class UpdateRule : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 558; } } //Shop_批发规则管理_编辑页
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
                //经销商等级
                this.ChkDealerRank.DataSource = rankBll.GetList(" RankType=1");
                ChkDealerRank.DataTextField = "Name";
                ChkDealerRank.DataValueField = "RankId";
                ChkDealerRank.DataBind();
                ShowInfo(RuleId);
     
            }
        }

        private void ShowInfo(int RuleId)
        {
            //获取选中的会员等级
            List<Maticsoft.Model.Shop.Sales.SalesUserRank> userRanks = userRankBll.GetModelList(" RuleId=" + RuleId);
            List<int> rankList = userRanks.Select(c => c.RankId).ToList();
            for (int i = 0; i < ChkUserRank.Items.Count; i++)
            {
                if (rankList.Contains(Common.Globals.SafeInt(ChkUserRank.Items[i].Value, 0)))
                {
                    ChkUserRank.Items[i].Selected = true;
                }
            }
            for (int i = 0; i < ChkDealerRank.Items.Count; i++)
            {
                if (rankList.Contains(Common.Globals.SafeInt(ChkDealerRank.Items[i].Value, 0)))
                {
                    ChkDealerRank.Items[i].Selected = true;
                }
            }
            //规则信息
            Maticsoft.Model.Shop.Sales.SalesRule ruleModel= ruleBll.GetModel(RuleId);
            this.txtRuleName.Text = ruleModel.RuleName;
            this.radMode.SelectedValue = ruleModel.RuleMode.ToString();
            this.radStatus.SelectedValue = ruleModel.Status.ToString();
            this.radUnit.SelectedValue = ruleModel.RuleUnit.ToString();

            //规则项信息
            List<Maticsoft.Model.Shop.Sales.SalesItem> itemList = itemBll.GetModelList(" RuleId=" + RuleId);
            string hfItems = "";
            if (itemList != null && itemList.Count > 0)
            {
                this.radItemType.SelectedValue = itemList[0].ItemType.ToString();
                foreach (var item in itemList)
                {
                    if (String.IsNullOrWhiteSpace(hfItems))
                    {
                        hfItems = item.UnitValue + "|" + item.RateValue;
                    }
                    else
                    {
                        hfItems =hfItems+","+ item.UnitValue + "|" + item.RateValue;
                    }
                }
            }
            this.hfItems.Value = hfItems;

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Sales.SalesRule ruleModel = ruleBll.GetModel(RuleId);
            ruleModel.RuleName = this.txtRuleName.Text;
            ruleModel.RuleMode = Common.Globals.SafeInt(this.radMode.SelectedValue, 0);
            ruleModel.RuleUnit = Common.Globals.SafeInt(this.radUnit.SelectedValue, 0);
            ruleModel.Status = Common.Globals.SafeInt(this.radStatus.SelectedValue, 0);
            ruleModel.CreatedUserID = CurrentUser.UserID;
            string hfItems = this.hfItems.Value;
            if (String.IsNullOrWhiteSpace(hfItems))
            {
                MessageBox.ShowFailTip(this, "请填写优惠规则项");
                return;
            }
            //编辑批发规则
            
            if ( ruleBll.Update(ruleModel))
            {
                //编辑规则的优惠项   （先删除以前的，然后重新添加）
                var ItemList = hfItems.Split(',');
                int itemtype = Common.Globals.SafeInt(this.radItemType.SelectedValue, 0);
                itemBll.DeleteByRuleId(ruleModel.RuleId);
                foreach (var item in ItemList)
                {
                    Maticsoft.Model.Shop.Sales.SalesItem itemModel = new Model.Shop.Sales.SalesItem();
                    itemModel.ItemType = itemtype;
                    itemModel.UnitValue = Common.Globals.SafeInt(item.Split('|')[0], 0);
                    itemModel.RateValue = Common.Globals.SafeInt(item.Split('|')[1], 0);
                    itemModel.RuleId = ruleModel.RuleId;
                    itemBll.Add(itemModel);
                }
                //编辑规则的对应的用户等级和经销商等级  （先删除以前的，然后重新添加）
                userRankBll.DeleteByRuleId(ruleModel.RuleId);
                for (int i = 0; i < ChkUserRank.Items.Count; i++)
                {
                    if (ChkUserRank.Items[i].Selected)
                    {
                        Maticsoft.Model.Shop.Sales.SalesUserRank userRankModel = new Model.Shop.Sales.SalesUserRank();
                        userRankModel.RankId = Common.Globals.SafeInt(ChkUserRank.Items[i].Value, 0);
                        userRankModel.RuleId = ruleModel.RuleId;
                        userRankBll.Add(userRankModel);
                    }
                }
                for (int i = 0; i < ChkDealerRank.Items.Count; i++)
                {
                    if (ChkDealerRank.Items[i].Selected)
                    {
                        Maticsoft.Model.Shop.Sales.SalesUserRank userRankModel = new Model.Shop.Sales.SalesUserRank();
                        userRankModel.RankId = Common.Globals.SafeInt(ChkUserRank.Items[i].Value, 0);
                        userRankModel.RuleId = ruleModel.RuleId; ;
                        userRankBll.Add(userRankModel);
                    }
                }
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }

        }

        public int RuleId
        {
            get
            {
                int ruleId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    ruleId = Globals.SafeInt(Request.Params["id"], 0);
                }
                return ruleId;
            }
        }
    }
}