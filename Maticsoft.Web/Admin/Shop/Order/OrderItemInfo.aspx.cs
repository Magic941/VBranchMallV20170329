using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Order;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrderItemInfo : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 446; } } //Shop_订单管理_发货页
        private Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
        private Maticsoft.BLL.Shop.Order.OrderItems itemBll = new OrderItems();
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new Maticsoft.BLL.Shop.Order.OrderAction();
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

            Maticsoft.Model.Shop.Order.OrderInfo model = orderBll.GetModel(OrderId);
            if (model != null)
            {

                this.lblTitle.Text = "正在进行订单【" + model.OrderCode + "】配货操作";
                lblOrderCode.Text = model.OrderCode;
                lblCreatedDate.Text = model.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");


                lblBuyerEmail.Text = model.BuyerEmail;
                lblBuyerCellPhone.Text = model.BuyerCellPhone;
                lblBuyerName.Text = model.BuyerName;

                lblShipTypeName.Text = model.ShippingModeName;

            }
        }

        public void BindData()
        {
            gridView.DataSetSource = itemBll.GetListByCache(" OrderId=" + OrderId);
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }


        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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



        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Order.OrderInfo orderModel = orderBll.GetModel(OrderId);

            ////查询出主表的信息
            //Maticsoft.Model.Shop.Order.OrderInfo orderM = orderBll.GetOrderInfoModel(orderModel.ParentOrderId);

            //orderM.ShippingStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.ShippingStatus.Packing;
            //orderM.OrderStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.Handling;

            //已配货
            orderModel.ShippingStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.ShippingStatus.Packing;
            orderModel.OrderStatus = (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.Handling;

            if (orderBll.Update(orderModel))
            {
                //添加订单日志
                Maticsoft.Model.Shop.Order.OrderAction actionModel = new Maticsoft.Model.Shop.Order.OrderAction();
                int actionCode = (int)Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemPacking;
                actionModel.ActionCode = actionCode.ToString();
                actionModel.ActionDate = DateTime.Now;
                actionModel.OrderCode = orderModel.OrderCode;
                actionModel.OrderId = orderModel.OrderId;
                actionModel.Remark = "配货操作";
                actionModel.UserId = CurrentUser.UserID;
                actionModel.Username = CurrentUser.NickName;
                actionBll.Add(actionModel);

                //清除缓存
                orderBll.RemoveModelInfoCache(orderModel.OrderId);

                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                //MessageBox.ResponseScript(this,);
                //Page.ClientScript.RegisterClientScriptBlock();
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
        }

    }
}