using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Maticsoft.Common;
using System.Data;

namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrderComplete : PageBaseAdmin
    {

        public Maticsoft.BLL.Shop.Account.User userBLL = new BLL.Shop.Account.User();
        public Maticsoft.BLL.Shop.Order.OrderReturnGoods returnGoodsBLL = new BLL.Shop.Order.OrderReturnGoods();
        public Maticsoft.BLL.Shop.Order.OrderReturnGoodsItem returnGoodsItemBLL = new BLL.Shop.Order.OrderReturnGoodsItem();
        public Maticsoft.BLL.Shop.Order.OrderItems orderItemsBLL = new BLL.Shop.Order.OrderItems();
        public Maticsoft.BLL.Shop.Order.Orders orderBll = new BLL.Shop.Order.Orders();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["returnItemId"] != null)
                {
                    DisplayDetail(long.Parse(Request["returnItemId"].ToString()));
                }
            }
        }
        //退货商品详细信息
        private void DisplayDetail(long returnItemId)
        {
            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnGoodsItem = returnGoodsItemBLL.GetModel(returnItemId);
            if (returnGoodsItem != null)
            {
                Maticsoft.Model.Shop.Order.OrderReturnGoods returnGoods = returnGoodsBLL.GetModel(returnGoodsItem.ReturnId.Value);
                Maticsoft.Model.Shop.Order.OrderItems orderItems = orderItemsBLL.GetModel(returnGoodsItem.OrderItemId.Value);
                lbProductName.Text = returnGoodsItem.ProductName;
                productimg.Visible = false;
                if (!string.IsNullOrEmpty(orderItems.ThumbnailsUrl))
                {
                    productimg.ImageUrl = orderItems.ThumbnailsUrl;
                    productimg.Visible = true;
                }
                lbQuntity.Text = returnGoodsItem.Quantity.ToString();
                lbSellPrice.Text = returnGoodsItem.AdjustedPrice.Value.ToString("F");
                lbReducePrice.Text = (returnGoodsItem.SellPrice.Value - returnGoodsItem.AdjustedPrice.Value).ToString("F");
                int LogisticStatus = int.Parse(returnGoods.LogisticStatus.ToString());
                int RefundStatus = int.Parse(returnGoods.RefundStatus.ToString());
                if (LogisticStatus == 0 && RefundStatus <3)
                {
                    lbReturnStauts.Text = "买家未发货"; 
                }
                else if (LogisticStatus == 1 && RefundStatus<3)
                {
                    lbReturnStauts.Text = "买家已发货";
                }
                else if (LogisticStatus == 2 && RefundStatus < 3)
                {
                    lbReturnStauts.Text = "卖家已收货";
                }
                else if (LogisticStatus == 2 && RefundStatus == 3)
                {
                    lbReturnStauts.Text = "卖家退款货";
                }
                else if (LogisticStatus == 2 && RefundStatus == 4)
                {
                    lbReturnStauts.Text = "卖家拒绝退款";
                }
                else {
                    lbReturnStauts.Text = "卖家拒绝退货";
                }
                int buyid = int.Parse(returnGoodsItem.UserID.ToString());
                Maticsoft.Model.Members.Users user =  userBLL.GetUsersInfo(buyid);
                lbBuyerName.Text = user.UserName.ToString();
                lbSupplierName.Text = returnGoodsItem.Suppliername;
                lbReturnAmounts.Text = (returnGoodsItem.AdjustedPrice.Value * returnGoodsItem.Quantity.Value).ToString("F");
                lbAmountsActual.Text = returnGoods.AmountActual.ToString() == null || returnGoods.AmountActual.ToString() == "" ? (returnGoodsItem.AdjustedPrice.Value * returnGoodsItem.Quantity.Value).ToString("F") : returnGoods.AmountActual.Value.ToString("F");
                lbReturnReason.Text = returnGoods.ReturnReason;
                lbReturnDescription.Text = returnGoods.ReturnDescription;
                lbReturnApplyTime.Text = returnGoodsItem.CreateTime.ToString();
                lbReturnApproveTime.Text = returnGoods.ApproveTime.ToString();
                lbReturnApproveUser.Text = returnGoods.ApprovePeason;
                lbReturnAccountUser.Text = returnGoods.AccountPeason;
                lbReturnAccountTime.Text = returnGoods.AccountTime.ToString();
                lbID.Text = returnGoods.Id.ToString();
                if (returnGoods.Information == null)
                {
                    lbInformation.Text = "";
                }
                else
                {
                    lbInformation.Text = returnGoods.Information.ToString();
                    //快递公司名称
                    BLL.Shop.Sales.ExpressTemplate expresstempBll = new BLL.Shop.Sales.ExpressTemplate();
                    DataSet ds = expresstempBll.GetList(" IsUse=1 and ExpressId= " + lbInformation.Text);
                    if (ds != null)
                    {
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            System.Data.DataRow dr = ds.Tables[0].Rows[i];
                            lbInformation.Text = dr["ExpressName"].ToString();
                        }
                    }

                }
                if (returnGoods.ExpressNO == null)
                {
                    lbInformation.Text = "";
                }
                else
                {
                    lbExpressNO.Text = returnGoods.ExpressNO.ToString();
                }

                StringBuilder sb = new StringBuilder();
                foreach (var imgurl in returnGoods.Attachment.Split(','))
                {
                    sb.Append(string.Format(" <li><img src='{0}' width=\"120\" /> </li>", Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl") + imgurl));
                }
                imgdiv.InnerHtml = sb.ToString();
            }

        }

        #region 确认收货
        //protected void btnPass_OnClick(object sender, EventArgs e)
        //{
        //    Maticsoft.Model.Shop.Order.OrderReturnGoods model = new Maticsoft.Model.Shop.Order.OrderReturnGoods();

        //    model.LogisticStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.LogisticStatus.Receipt;//发货状态
        //    model.ApproveRemark = txtReturnreasonREJ.Text;
        //    model.ApproveTime = DateTime.Now;//审核时间
        //    model.Id = long.Parse(lbID.Text);


        //    if (returnGoodsBLL.Detail(model))
        //    {
        //        MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
        //    }
        //    else
        //    {
        //        MessageBox.ShowFailTip(this, "操作失败！");
        //    }
        //}
        #endregion

        #region 确认收货
        protected void btnPass_OnClick(object sender, EventArgs e)
        {

            var returnItemId = long.Parse(Request["returnItemId"].ToString());
            Maticsoft.Model.Shop.Order.OrderReturnGoods model = new Model.Shop.Order.OrderReturnGoods();
            Maticsoft.Model.Shop.Order.Shop_ReturnOrderAction returnAction = new Model.Shop.Order.Shop_ReturnOrderAction();
            Maticsoft.BLL.Shop.Order.Shop_ReturnOrderAction ActionBll = new BLL.Shop.Order.Shop_ReturnOrderAction();
            Maticsoft.Model.Shop.Order.OrderItems orderitemmodel = new Model.Shop.Order.OrderItems();
            
            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnItem = returnGoodsItemBLL.GetModel(returnItemId);
            Maticsoft.Model.Shop.Order.OrderInfo orderinfo = orderBll.GetModel(returnItem.OrderId.Value);
            model.LogisticStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.LogisticStatus.Receipt; //确认收货
            model.Refuseremark = "";//处理拒收理由为空
            model.ApproveRemark = "确认收货";
            model.ApprovePeason = CurrentUser.UserName;
            model.ApproveTime = DateTime.Now;//时间
            model.Id = long.Parse(lbID.Text);


            returnAction.ReturnOrderId = model.Id;
            returnAction.ReturnOrderCode = returnItem.ReturnOrderCode;
            returnAction.UserId = CurrentUser.UserID;
            returnAction.UserName = CurrentUser.UserName.ToString();
            returnAction.ActionCode = ((int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.ReceiptReturnGoods).ToString();
            returnAction.ActionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            returnAction.Remark = "商家确认收货";
            orderitemmodel = orderItemsBLL.GetModel(returnItem.OrderItemId.Value);
            orderitemmodel.ReturnQty = int.Parse(returnItem.Quantity.ToString());
            orderitemmodel.ReturnStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus.Handding;//退货状态为处理中
            //orderitemmodel.ReturnOrderType = (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.Goods;
            orderitemmodel.ItemId = returnItem.OrderItemId.Value;

            orderinfo.ShippingStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.ShippingStatus.ConfirmShip;
            if (returnGoodsBLL.RefuseReturnGoods(model) && ActionBll.Add(returnAction) > 0 && orderItemsBLL.UpdateStatus(orderitemmodel) && orderBll.Update(orderinfo))
            {
                MessageBox.ShowSuccessTipScript(this, "确认收货成功！", "window.parent.location.reload();");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
        }
        #endregion

        #region 拒绝通过
        protected void btnRefusal_OnClick(object sender, EventArgs e)
        {
            var returnItemId = long.Parse(Request["returnItemId"].ToString());
            Maticsoft.Model.Shop.Order.OrderReturnGoods model = new Model.Shop.Order.OrderReturnGoods();
            Maticsoft.Model.Shop.Order.Shop_ReturnOrderAction returnAction = new Model.Shop.Order.Shop_ReturnOrderAction();
            Maticsoft.BLL.Shop.Order.Shop_ReturnOrderAction ActionBll = new BLL.Shop.Order.Shop_ReturnOrderAction();
            Maticsoft.Model.Shop.Order.OrderItems orderitemmodel = new Model.Shop.Order.OrderItems();
            Maticsoft.Model.Shop.Order.OrderInfo orderinfo = new Model.Shop.Order.OrderInfo();
            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnItem = returnGoodsItemBLL.GetModel(returnItemId);
            //model.ReturnRemark = txtReturnRemark.Text;//备注
            model.Refuseremark = txtReturnreasonREJ.Text;//拒绝理由
            model.LogisticStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.LogisticStatus.storage;//拒绝收货
            model.ApproveRemark = "审核未通过";
            model.ApprovePeason = CurrentUser.UserName;//审核
            model.ApproveTime = DateTime.Now;//审核时间
            model.Id = long.Parse(lbID.Text);

            returnAction.ReturnOrderId = model.Id;
            returnAction.ReturnOrderCode = returnItem.ReturnOrderCode;
            returnAction.UserId = CurrentUser.UserID;
            returnAction.UserName = CurrentUser.UserName.ToString();
            returnAction.ActionCode = ((int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.RefuseReturnGoods).ToString();
            returnAction.ActionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            returnAction.Remark = "商家拒收退货";

            orderitemmodel.ReturnQty = int.Parse(returnItem.Quantity.ToString());
            orderitemmodel.ReturnStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus.Refuse;//退货状态为拒绝
            //orderitemmodel.ReturnOrderType = (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.Goods;
            orderitemmodel.ItemId = returnItem.OrderItemId.Value;

            // 付款完成后将订单状态从"锁定"改为"处理中"
            orderinfo = orderBll.GetModel((long)returnItem.OrderId);
            if (null != orderinfo)
            {
                orderinfo.OrderStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.Handling;
            }

            if (returnGoodsBLL.RefuseReturnGoods(model) && ActionBll.Add(returnAction) > 0 && orderItemsBLL.UpdateStatus(orderitemmodel) && orderBll.Update(orderinfo))
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
        }
        #endregion

    }
}