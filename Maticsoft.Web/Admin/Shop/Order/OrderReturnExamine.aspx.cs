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
    public partial class OrderReturnExamine : PageBaseAdmin
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
                int Status = int.Parse(returnGoods.Status.ToString());
                if (RefundStatus < 2 && Status == 0 && returnGoods.ReturnGoodsType == 2)
                {
                    lbReturnStauts.Text = "退款等待审核";
                }
                else if (LogisticStatus == 0 && RefundStatus < 3 && Status == 0 && returnGoods.ReturnGoodsType==1)
                {
                    lbReturnStauts.Text = "退货等待审核";
                }
                else if (LogisticStatus == 0 && RefundStatus < 3 && Status == 1 && returnGoods.ReturnGoodsType == 1)
                {
                    lbReturnStauts.Text = "买家未发货";
                }
                else if (LogisticStatus == 1 && RefundStatus < 3 && returnGoods.ReturnGoodsType == 1)
                {
                    lbReturnStauts.Text = "买家已发货";
                }
                else if (LogisticStatus == 2 && RefundStatus < 3 && returnGoods.ReturnGoodsType == 1)
                {
                    lbReturnStauts.Text = "卖家已收货";
                }
                else if (LogisticStatus == 2 && RefundStatus == 3)
                {
                    lbReturnStauts.Text = "卖家已退款";
                }
                else if (LogisticStatus == 2 && RefundStatus == 4)
                {
                    lbReturnStauts.Text = "卖家拒绝退款";
                }
                else if (LogisticStatus == 0 && RefundStatus == 4 && returnGoods.ReturnGoodsType == 2)
                {
                    lbReturnStauts.Text = "卖家拒绝退款"; //申请退款的拒绝退款
                }
                else
                {
                    lbReturnStauts.Text = "卖家拒绝退货";
                }
                int ReturnGoodsType = returnGoods.ReturnGoodsType;
                liReturnType.SelectedValue = ReturnGoodsType.ToString();
                int buyid = int.Parse(returnGoodsItem.UserID.ToString());
                Maticsoft.Model.Members.Users user = userBLL.GetUsersInfo(buyid);
                lbBuyerName.Text = user.UserName.ToString();
                lbSupplierName.Text = returnGoodsItem.Suppliername;
                lbReturnAmounts.Text = (returnGoodsItem.AdjustedPrice.Value * returnGoodsItem.Quantity.Value).ToString("F");
                lbAmountActual.Text = (returnGoodsItem.AdjustedPrice.Value * returnGoodsItem.Quantity.Value).ToString("F"); //没有邮费
                lbReturnReason.Text = returnGoods.ReturnReason;
                lbReturnDescription.Text = returnGoods.ReturnDescription;
                lbReturnApplyTime.Text = returnGoodsItem.CreateTime.ToString();
                lbReturnApproveTime.Text = returnGoods.ApproveTime.ToString();
                lbReturnApproveUser.Text = returnGoods.ApprovePeason;
                lbReturnAccountUser.Text = returnGoods.AccountPeason;
                lbReturnAccountTime.Text = returnGoods.AccountTime.ToString();
                lbID.Text = returnGoods.Id.ToString();
                if (returnGoods.Information == null || returnGoods.Information=="")
                {
                    lbInformation.Text = "";
                }
                else
                {
                    lbInformation.Text = returnGoods.Information.ToString();
                    //快递公司名称
                    BLL.Shop.Sales.ExpressTemplate expresstempBll = new BLL.Shop.Sales.ExpressTemplate();
                    DataSet ds = expresstempBll.GetList(" IsUse=1 and ExpressId=" + lbInformation.Text + "");
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

        #region 审核通过
        protected void btnPass_OnClick(object sender, EventArgs e)
        {
            if (txtReturnreasonREJ.Text.Length > 0)
            {
                MessageBox.Show(this,"竟然审核通过为何要填写拒绝理由呢?");
                return;
            }
            var returnItemId = long.Parse(Request["returnItemId"].ToString());
            Maticsoft.Model.Shop.Order.OrderReturnGoods model = new Maticsoft.Model.Shop.Order.OrderReturnGoods();
            Maticsoft.Model.Shop.Order.OrderItems orderitemmodel = new Model.Shop.Order.OrderItems();
            
            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnItem = returnGoodsItemBLL.GetModel(returnItemId);
            Maticsoft.Model.Shop.Order.OrderInfo orderinfo = orderBll.GetModel(returnItem.OrderId.Value);
            Maticsoft.Model.Shop.Order.Shop_ReturnOrderAction returnAction = new Model.Shop.Order.Shop_ReturnOrderAction();
            Maticsoft.BLL.Shop.Order.Shop_ReturnOrderAction ActionBll = new BLL.Shop.Order.Shop_ReturnOrderAction();
            orderitemmodel = orderItemsBLL.GetModel(returnItem.OrderItemId.Value);
            if (int.Parse(liReturnType.SelectedValue) == 1)
            {
                
                
                ///修改 ReturnQty  renturnstatus
                model.AmountActual = Convert.ToDecimal(this.lbAmountActual.Text);
                model.ReturnRemark = txtReturnRemark.Text;//备注
                model.PickName = txtShipName.Text;//取货人
                int regionId = RegionList.Region_iID;
                model.PickRegionId = regionId; //市区Id
                model.ReturnAddress = txtShipAddress.Text;//退货地址
                model.PickZipCode = txtShipZipCode.Text; //邮编
                model.PickCellPhone = txtShipCellPhone.Text; //座机
                model.ReturnTelphone = txtShipTelPhone.Text;//手机
                model.PickEmail = txtShipEmail.Text;//邮箱
                model.Status = (int)Maticsoft.Model.Shop.Order.EnumHelper.Status.Handling;//申请状态
                model.ApproveRemark = "审核通过";
                model.ApprovePeason = CurrentUser.UserName;//审核
                model.ApproveTime = DateTime.Now;//审核时间
                model.Id = long.Parse(lbID.Text);
                //model.eturnGoodsType = int.Parse(liReturnType.SelectedValue);

                returnAction.ReturnOrderId = model.Id;
                returnAction.ReturnOrderCode = returnItem.ReturnOrderCode;
                returnAction.UserId = CurrentUser.UserID;
                returnAction.UserName = CurrentUser.UserName.ToString() ;
                returnAction.ActionCode = ((int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.ApproveReturnGoodsPass).ToString();
                returnAction.ActionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                returnAction.Remark = "退货审核(通过)";

                orderitemmodel.ReturnQty = int.Parse(returnItem.Quantity.ToString());
                orderitemmodel.ReturnStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus.Handding;//退货状态为处理中
                orderitemmodel.ReturnOrderType = (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.Goods;
                orderitemmodel.ItemId = returnItem.OrderItemId.Value;

                orderinfo.HasReturn = true;
                orderinfo.OrderId = orderitemmodel.OrderId;

                if (returnGoodsBLL.Detail(model) && ActionBll.Add(returnAction) > 0 && orderItemsBLL.UpdateStatus(orderitemmodel) && orderBll.OrderReturnUpdate(orderinfo.OrderId))
                {
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                }

            }
            else {
                model.AmountActual = Convert.ToDecimal(this.lbAmountActual.Text);
                model.ReturnRemark = txtReturnRemark.Text;//备注
                
                model.Status = (int)Maticsoft.Model.Shop.Order.EnumHelper.Status.Handling;//申请状态
                model.ApproveRemark = "审核通过";
                model.ApprovePeason = CurrentUser.UserName;//审核
                model.ApproveTime = DateTime.Now;//审核时间
                model.Id = long.Parse(lbID.Text);
                //model.ReturnGoodsType = int.Parse(liReturnType.SelectedValue);

                returnAction.ReturnOrderId = model.Id;
                returnAction.ReturnOrderCode = returnItem.ReturnOrderCode;
                returnAction.UserId = CurrentUser.UserID;
                returnAction.UserName = CurrentUser.UserName.ToString();
                returnAction.ActionCode = ((int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.ApproveReturnGoodsPass).ToString();
                returnAction.ActionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                returnAction.Remark = "退款审核(通过)";

                orderitemmodel.ReturnQty = int.Parse(returnItem.Quantity.ToString());
                orderitemmodel.ReturnStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus.Handding;//退货状态为处理中
                orderitemmodel.ReturnOrderType = (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.Money;
                orderitemmodel.ItemId = returnItem.OrderItemId.Value;
                
                orderinfo.HasReturn = true;
                orderinfo.OrderId = orderitemmodel.OrderId;
                if (returnGoodsBLL.Detail(model) && ActionBll.Add(returnAction) > 0 && orderItemsBLL.UpdateStatus(orderitemmodel) && orderBll.OrderReturnUpdate(orderinfo.OrderId))
                {
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
            }

            
            
        }
        #endregion

        #region 拒绝通过
        protected void btnRefusal_OnClick(object sender, EventArgs e)
        {
            var returnItemId = long.Parse(Request["returnItemId"].ToString());
            Maticsoft.Model.Shop.Order.OrderReturnGoods model = new Maticsoft.Model.Shop.Order.OrderReturnGoods();
            Maticsoft.Model.Shop.Order.OrderItems orderitemmodel = new Model.Shop.Order.OrderItems();
            Maticsoft.Model.Shop.Order.OrderInfo orderinfo = new Model.Shop.Order.OrderInfo();
            Maticsoft.Model.Shop.Order.OrderReturnGoodsItem returnItem = returnGoodsItemBLL.GetModel(returnItemId);

            Maticsoft.Model.Shop.Order.Shop_ReturnOrderAction returnAction = new Model.Shop.Order.Shop_ReturnOrderAction();
            Maticsoft.BLL.Shop.Order.Shop_ReturnOrderAction ActionBll = new BLL.Shop.Order.Shop_ReturnOrderAction();

            if (int.Parse(liReturnType.SelectedValue) == (int)Maticsoft.Model.Shop.Order.EnumHelper.ReturnGoodsType.Goods)
            {
                
                model.ReturnRemark = txtReturnRemark.Text;//备注
                model.RefuseReason = txtReturnreasonREJ.Text;//拒绝理由
                model.Status = (int)Maticsoft.Model.Shop.Order.EnumHelper.Status.Refuse;//申请状态
                model.ApproveRemark = "审核未通过";
                model.ApprovePeason = CurrentUser.UserName;//审核
                model.ApproveTime = DateTime.Now;//审核时间
                model.Id = long.Parse(lbID.Text);

                returnAction.ReturnOrderId = model.Id;
                returnAction.ReturnOrderCode = returnItem.ReturnOrderCode;
                returnAction.UserId = CurrentUser.UserID;
                returnAction.UserName = CurrentUser.UserName.ToString();
                returnAction.ActionCode = ((int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.ApproveReturnGoodsRefuse).ToString();
                returnAction.ActionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                returnAction.Remark = "退货审核(拒绝)";

                orderitemmodel.ReturnQty = int.Parse(returnItem.Quantity.ToString());
                orderitemmodel.ReturnStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus.Refuse; ;//退货状态为拒绝
                orderitemmodel.ReturnOrderType = int.Parse(liReturnType.SelectedValue);
                orderitemmodel.ItemId = returnItem.OrderItemId.Value;

                // 无论是"退款"还是"退货",审核拒绝通过时,都应该取消订单锁定状态,改为"处理中"
                orderinfo = orderBll.GetModel(0 == returnItem.OrderId ? 0 : (long)returnItem.OrderId);
                if(null != orderinfo) orderinfo.OrderStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.Handling;

                if (returnGoodsBLL.Detail_Update(model) && ActionBll.Add(returnAction) > 0 && orderItemsBLL.UpdateStatus(orderitemmodel) && orderBll.Update(orderinfo))
                {
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
            }
            else {
                model.ReturnRemark = txtReturnRemark.Text;//备注
                model.RefuseReason = txtReturnreasonREJ.Text;//拒绝理由
                model.Status = (int)Maticsoft.Model.Shop.Order.EnumHelper.Status.Refuse;//申请状态
                model.ApproveRemark = "审核未通过";
                model.ApprovePeason = CurrentUser.UserName;//审核
                model.ApproveTime = DateTime.Now;//审核时间
                model.Id = long.Parse(lbID.Text);

                returnAction.ReturnOrderId = model.Id;
                returnAction.ReturnOrderCode = returnItem.ReturnOrderCode;
                returnAction.UserId = CurrentUser.UserID;
                returnAction.UserName = CurrentUser.UserName.ToString();
                returnAction.ActionCode = ((int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.ApproveReturnRefundRefuse).ToString();
                returnAction.ActionDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                returnAction.Remark = "退款审核(拒绝)";

                orderitemmodel.ReturnQty = int.Parse(returnItem.Quantity.ToString());
                orderitemmodel.ReturnStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.Returnstatus.Refuse;//退款状态为拒绝
                orderitemmodel.ReturnOrderType = int.Parse(liReturnType.SelectedValue);
                orderitemmodel.ItemId = returnItem.OrderItemId.Value;

                // 无论是"退款"还是"退货",审核拒绝通过时,取消订单锁定状态,改为"处理中"
                orderinfo = orderBll.GetModel(0 == returnItem.OrderId ? 0 : (long)returnItem.OrderId);
                if (null != orderinfo) orderinfo.OrderStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.Handling;

                if (returnGoodsBLL.Detail_Update(model) && ActionBll.Add(returnAction) > 0 && orderItemsBLL.UpdateStatus(orderitemmodel) && orderBll.Update(orderinfo))
                {
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败！");
                }
            }
            

        }
        #endregion

    }
}