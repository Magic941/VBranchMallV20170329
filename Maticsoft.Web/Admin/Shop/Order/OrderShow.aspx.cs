using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Ms;
using Maticsoft.BLL.Shop.Order;
using Maticsoft.Common;
using System.Data;
using System.Text;

using Maticsoft.Model;
using Maticsoft.BLL.Shop.Card;
using System.Collections;
using Maticsoft.Model.Shop.Shipping;

namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrderShow : PageBaseAdmin
    {
        public List<Maticsoft.Model.Shop.Shipping.LastData> LastDataModel = new List<LastData>();
        protected override int Act_PageLoad { get { return 445; } } //Shop_订单管理_详细页
        private int Act_UpdateOrderAmount = 694;  //Shop_订单管理_修改订单应付金额

        private Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
        private Maticsoft.BLL.Shop.Order.OrdersHistory historyBll = new OrdersHistory();
        private Maticsoft.BLL.Shop.Order.OrderItems itemBll = new OrderItems();
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new OrderAction();
        private Maticsoft.BLL.Shop.Order.OrderRemark remarkBll = new OrderRemark();
        private Maticsoft.BLL.Ms.Regions regionBll = new Regions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }


        #region 订单Id
        /// <summary>
        /// 订单Id
        /// </summary>
        public int OrderId
        {
            get
            {
                return Maticsoft.Common.Globals.SafeInt(Request.Params["orderId"], 0);
            }
        }
        #endregion

        #region 订单状态
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStatus
        {
            get
            {
                return Maticsoft.Common.Globals.SafeInt(Request.Params["type"], 0);
            }
        }
        #endregion

        private void ShowInfo()
        {
            if (OrderStatus == -1)
            {
                Maticsoft.Model.Shop.Order.OrdersHistory Historymodel = historyBll.GetModelByCache(OrderId);
                if (Historymodel != null)
                {
                    this.lblTitle.Text = "你正在查看订单【" + Historymodel.OrderCode + "】的详细信息";
                    //收货人信息
                    this.txtShipName.Text = Historymodel.ShipName;
                    txtShipZipCode.Text = Historymodel.ShipZipCode;
                    txtShipAddress.Text = Historymodel.ShipAddress;
                    txtShipCellPhone.Text = Historymodel.ShipCellPhone;
                    lblShipEmail.Text = Historymodel.ShipEmail;
                    // txtShipRegion.Text = Historymodel.ShipRegion;
                    RegionList.Region_iID = Historymodel.RegionId.HasValue ? Historymodel.RegionId.Value : 0;
                    txtShipTelPhone.Text = Historymodel.ShipTelPhone;
                    //买家信息
                    lblBuyerEmail.Text = Historymodel.BuyerEmail;
                    lblBuyerCellPhone.Text = Historymodel.BuyerCellPhone;
                    lblBuyerName.Text = Historymodel.BuyerName;
                    //订单价格信息
                    lblDiscountAdjusted.Text = Historymodel.DiscountAdjusted.HasValue
                                                   ? Historymodel.DiscountAdjusted.Value.ToString("F")
                                                   : "0";
                    lblFreightAdjusted.Text = Historymodel.FreightAdjusted.HasValue
                        ? Historymodel.FreightAdjusted.Value.ToString("F") : "0";
                    lblOrderTotal.Text = Historymodel.OrderTotal.ToString("F");
                    lblAmount.Text = Historymodel.Amount.ToString("F");
                    lblCouponAmount.Text = (Historymodel.OrderTotal - Historymodel.Amount).ToString("F");
                    //订单其它信息
                    lblPaymentTypeName.Text = Historymodel.PaymentTypeName;
                    lblRealShippingModeName.Text = Historymodel.RealShippingModeName;
                    lblPoint.Text = Historymodel.OrderPoint.ToString();
                    lblExpressCompanyName.Text = Historymodel.ExpressCompanyName;
                    lblWeight.Text = Historymodel.Weight.ToString();

                    lblShipOrderNumber.Text = string.IsNullOrWhiteSpace(Historymodel.ShipOrderNumber) ? "无" : Historymodel.ShipOrderNumber;

                    txtRemark.Text = Historymodel.Remark;
                    hfOrderMainStatus.Value = "9";


                    if (!string.IsNullOrWhiteSpace(Historymodel.ShipOrderNumber))
                    {
                        Maticsoft.BLL.Shop.Shipping.Express bll = new BLL.Shop.Shipping.Express();
                        List<Maticsoft.Model.Shop.Shipping.Shop_Express> list = bll.GetListModel("ExpressCode='" + Historymodel.ShipOrderNumber + "'", "UpdateTime",0);
                        if (list.Count > 0)
                        {
                            LastDataModel = comm.JsonToObject<List<Maticsoft.Model.Shop.Shipping.LastData>>(list[0].ExpressContent.ToString());
                        }
                    }
                }
            }
            else
            {
                Maticsoft.Model.Shop.Order.OrderInfo model = orderBll.GetModelByCache(OrderId);
                if (model != null)
                {
                    this.lblTitle.Text = "正在查看订单【" + model.OrderCode + "】的详细信息";
                    this.txtShipName.Text = model.ShipName;
                    txtShipZipCode.Text = model.ShipZipCode;
                    txtShipAddress.Text = model.ShipAddress;
                    txtShipCellPhone.Text = model.ShipCellPhone;
                    lblShipEmail.Text = model.ShipEmail;
                    // txtShipRegion.Text = Historymodel.ShipRegion;
                    RegionList.Region_iID = model.RegionId.HasValue ? model.RegionId.Value : 0;
                    txtShipTelPhone.Text = model.ShipTelPhone;

                    lblBuyerEmail.Text = model.BuyerEmail;
                    lblBuyerCellPhone.Text = model.BuyerCellPhone;
                    lblBuyerName.Text = model.BuyerName;

                    //订单价格信息
                    lblDiscountAdjusted.Text = model.DiscountAdjusted.HasValue
                                                   ? model.DiscountAdjusted.Value.ToString("F")
                                                   : "0";
                    lblFreightAdjusted.Text = model.FreightAdjusted.HasValue
                        ? model.FreightAdjusted.Value.ToString("F") : "0";
                    lblOrderTotal.Text = model.OrderTotal.ToString("F");
                    lblAmount.Text = model.Amount.ToString("F");
                    lblCouponAmount.Text = (model.OrderTotal - model.Amount).ToString("F");
                    //订单其它信息
                    lblPaymentTypeName.Text = model.PaymentTypeName;
                    lblRealShippingModeName.Text = model.RealShippingModeName;
                    lblPoint.Text = model.OrderPoint.ToString();
                    lblExpressCompanyName.Text = model.ExpressCompanyName;
                    lblWeight.Text = model.Weight.ToString();

                    lblShipOrderNumber.Text = string.IsNullOrWhiteSpace(model.ShipOrderNumber) ? "无" : model.ShipOrderNumber;

                    txtRemark.Text = model.Remark;
                    hfOrderMainStatus.Value = ((int)orderBll.GetOrderType(model.PaymentGateway, model.OrderStatus,
                                                                    model.PaymentStatus, model.ShippingStatus)).ToString();

                    if (model.OrderStatus > -1 && model.OrderStatus < 2 && model.PaymentStatus < 2 &&
                        (GetPermidByActID(Act_UpdateOrderAmount) == -1 || UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateOrderAmount))))
                    {
                        txtAmount.Visible = true;
                        imgModifyAmount.Visible = true;
                        lnkSaveAmount.Visible = true;
                        gridView_Action.Columns[2].Visible = true;
                    }


                    if (!string.IsNullOrWhiteSpace(model.ShipOrderNumber))
                    {
                        Maticsoft.BLL.Shop.Shipping.Express bll = new BLL.Shop.Shipping.Express();
                        List<Maticsoft.Model.Shop.Shipping.Shop_Express> list = bll.GetListModel("ExpressCode='" + model.ShipOrderNumber + "'", "UpdateTime",0);
                        if (list.Count > 0)
                        {
                            LastDataModel = comm.JsonToObject<List<Maticsoft.Model.Shop.Shipping.LastData>>(list[0].ExpressContent.ToString());
                        }
                    }
                }
            }

        }

        public void BindData()
        {
            gridView.DataSetSource = itemBll.GetListByCache(" OrderId=" + OrderId);
        }

        public void BindAction()
        {
            gridView_Action.DataSetSource = actionBll.GetList(" OrderId=" + OrderId);
        }

        public void BindRemark()
        {
            gridView_Remark.DataSetSource = remarkBll.GetList(" OrderId=" + OrderId);
        }

        //public void BindExpress()
        //{
        //    gridView_Express.DataSource = Maticsoft.Web.Components.ExpressHelper.GetExpress(OrderId);
        //    gridView_Express.DataBind();
        //}

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_Action_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView_Action.PageIndex = e.NewPageIndex;
            gridView_Action.OnBind();
        }
        protected void gridView_Remark_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView_Remark.PageIndex = e.NewPageIndex;
            gridView_Remark.OnBind();
        }


        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Order.OrderInfo orderModel = orderBll.GetModel(OrderId);
            if (orderModel == null)
                return;

            int regionId = RegionList.Region_iID;
            orderModel.RegionId = regionId;
            orderModel.ShipRegion = regionBll.GetRegionNameByRID(regionId);
            orderModel.ShipName = txtShipName.Text;
            orderModel.ShipAddress = txtShipAddress.Text;
            orderModel.ShipTelPhone = txtShipTelPhone.Text;
            orderModel.ShipCellPhone = txtShipCellPhone.Text;
            orderModel.ShipZipCode = txtShipZipCode.Text;


            if (orderBll.Update(orderModel))
            {
                //加操作日志
                Maticsoft.Model.Shop.Order.OrderAction actionModel = new Model.Shop.Order.OrderAction();
                int actionCode = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemUpdateShip;
                actionModel.ActionCode = actionCode.ToString();
                actionModel.ActionDate = DateTime.Now;
                actionModel.OrderCode = orderModel.OrderCode;
                actionModel.OrderId = orderModel.OrderId;
                actionModel.Remark = "修改收货信息";
                actionModel.UserId = CurrentUser.UserID;
                actionModel.Username = CurrentUser.NickName;
                actionBll.Add(actionModel);
                //清除缓存
                orderBll.RemoveModelInfoCache(orderModel.OrderId);
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label skuitemval= e.Row.FindControl("skuitemval") as Label;
                //DataRowView drv = (DataRowView)e.Row.DataItem;
               // skuitemval.Text = GetSkusItemAttributeValues(drv["sku"].ToString(),long.Parse(drv["ProductId"].ToString()));
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
        }

        public string GetSkusItemAttributeValues(string Sku, long ProductId)
        {
            StringBuilder sb = new StringBuilder();
            DataSet ds= itemBll.GetProductSkuItemAttributeValues(Sku,ProductId);
            if(ds!=null)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr=ds.Tables[0].Rows[i];
                    if (i == 0)
                    {
                        sb.Append(string.Format("{0}-{1}", dr["AttributeName"].ToString(), dr["ValueStr"].ToString()));
                    }
                    else
                    {
                        sb.Append(string.Format("<br/>{0}-{1}", dr["AttributeName"].ToString(), dr["ValueStr"].ToString()));
                    }
                }
            }
            else
            {
                return "";
            }
           
            return sb.ToString();
        }


        //修改订单备注
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (OrderStatus == -1)
            {
                Maticsoft.Model.Shop.Order.OrdersHistory Historymodel = historyBll.GetModelByCache(OrderId);
                if (Historymodel != null)
                {
                    Historymodel.Remark = txtRemark.Text;
                    if (historyBll.Update(Historymodel))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                    }
                }
            }
            else
            {
                Maticsoft.Model.Shop.Order.OrderInfo model = orderBll.GetModelByCache(OrderId);
                if (model != null)
                {
                    model.Remark = txtRemark.Text;
                    if (orderBll.Update(model))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "操作失败！");
                    }
                }
            }
        }

        /// <summary>
        /// 获得订单追踪状态码信息
        /// </summary>
        /// <param name="actionCode"></param>
        /// <returns></returns>
        protected string GetActionCode(object actionCode)
        {
            if (actionCode == null)
            {
                return "";
            }
            return OrderAction.GetActionCode(actionCode.ToString());
        }

        protected void lnkSaveAmount_OnClick(object sender, EventArgs e)
        {
            if (CurrentUser.UserType != "AA") return;
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateOrderAmount)) &&
                GetPermidByActID(Act_UpdateOrderAmount) != -1)
            {
                MessageBox.ShowFailTip(this, "您已无权修改应付金额, 操作失败！");
                return;
            }

            Maticsoft.Model.Shop.Order.OrderInfo model = orderBll.GetModel(OrderId);
            if (model == null) return;

            if (model.OrderStatus <= -1 || model.OrderStatus >= 2 || model.PaymentStatus >= 2)
            {
                MessageBox.ShowFailTip(this, "订单已被支付/完成, 操作失败！");
                return;
            }

            decimal newAmount = Globals.SafeDecimal(txtAmount.Text, -1);
            if (newAmount < 0) return;

            decimal oldAmount = model.Amount;
            model.Amount = newAmount;

            if (oldAmount == newAmount) return;

            if (orderBll.Update(model))
            {
                //加操作日志
                Maticsoft.Model.Shop.Order.OrderAction actionModel = new Model.Shop.Order.OrderAction();
                int actionCode = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemUpdateAmount;
                actionModel.ActionCode = actionCode.ToString();
                actionModel.ActionDate = DateTime.Now;
                actionModel.OrderCode = model.OrderCode;
                actionModel.OrderId = model.OrderId;
                actionModel.Remark = string.Format("应付金额由 {0} 变更为 {1}", oldAmount.ToString("F"),newAmount.ToString("F"));
                actionModel.UserId = CurrentUser.UserID;
                actionModel.Username = CurrentUser.NickName;
                actionBll.Add(actionModel);
                //清除缓存
                orderBll.RemoveModelInfoCache(model.OrderId);
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
        }

    }
}