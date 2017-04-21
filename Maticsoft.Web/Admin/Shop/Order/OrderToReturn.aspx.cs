using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Maticsoft.Model.Shop.Order;
using System.Data;

namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrderToReturn : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 443; } } //Shop_订单管理_列表页
        protected new int Act_UpdateData = 444;    //Shop_订单管理_编辑数据
        public Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
        public Maticsoft.BLL.Shop.Account.User userBLL = new BLL.Shop.Account.User();
        public Maticsoft.BLL.Members.Users memberBLL = new BLL.Members.Users();
        public Maticsoft.BLL.Shop.Order.OrderReturnGoods returnGoodsBLL = new BLL.Shop.Order.OrderReturnGoods();
        public Maticsoft.BLL.Shop.Order.OrderReturnGoodsItem returnGoodsItemBLL = new BLL.Shop.Order.OrderReturnGoodsItem();
        public int Type = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
                this.orderId.Value = "0";
                if (Request["orderId"] != null)
                {
                    this.orderId.Value = Request["orderId"].ToString();
                }
            }
        }


        public void BindData()
        {
            // -1  全部， 0 未审核 1 未发货，2已发货，3未退款   4退款成功RefundStatus
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" 1=1 ");
            if (SelecReturnStatus == -1)
            {
            }
            else if (SelecReturnStatus == 0)
            {
                strWhere.AppendFormat(" and Status=0 ");
            }
            else if (SelecReturnStatus == 1)
            {
                strWhere.AppendFormat("  and Status=1 and LogisticStatus=0 and ReturnGoodsType=1 ");
            }
            else if (SelecReturnStatus == 2)
            {
                strWhere.AppendFormat("  and Status=1 and LogisticStatus=1 and ReturnGoodsType=1 ");
            }
            else if (SelecReturnStatus == 3)
            {
                strWhere.AppendFormat(" and Status = 1 and RefundStatus=0 ");
            }
            else if (SelecReturnStatus == 4)
            {
                strWhere.AppendFormat(" and Status = 2 and RefundStatus=3 ");
            }
            else if (SelecReturnStatus == 5)
            {
                strWhere.AppendFormat(" and Status = -3 or LogisticStatus =4 or RefundStatus=4 ");
            }
            if (OrderId != 0)
            {
                strWhere.AppendFormat("  and  OrderId={0} ", OrderId);
            }
            if (txtOrderCode.Text != "")
            {
                strWhere.AppendFormat(" and b.OrderCode like '%" + txtOrderCode.Text.ToString() + "%' ");
            }
            if (txtReturnCode.Text != "")
            {
                strWhere.AppendFormat(" and b.ReturnOrderCode like '%" + txtReturnCode.Text.ToString() + "%' ");
            }
            DataSet ds = returnGoodsItemBLL.GetList(-1, strWhere.ToString(), " a.Id DESC ");
            if (null != ds && ds.Tables.Count > 0 && !ds.Tables[0].Columns.Contains("ShippingStatus"))
            {
                DataColumn newColumn = new DataColumn("ShippingStatus");
                newColumn.DataType = System.Type.GetType("System.Int32");
                ds.Tables[0].Columns.Add(newColumn);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    row["ShippingStatus"] = orderBll.GetModel(int.Parse(row["OrderId"].ToString())).ShippingStatus;
                }
            }
            gridView.DataSetSource = ds;

            gridView.DataBind();

        }

        public long OrderId
        {
            get
            {
                return Maticsoft.Common.Globals.SafeInt(this.orderId.Value, 0);
            }
        }
        public int SelecReturnStatus
        {
            get
            {
                return Maticsoft.Common.Globals.SafeInt(this.dropReturnStatus.SelectedValue, -1);
            }
        }




        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();

        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        /*
        /// <summary>
        /// 确认收货
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Logistic")
            {
                string[] obj = e.CommandArgument.ToString().Split(',');
                long returnId = Common.Globals.SafeLong(obj[0], 0);
                Maticsoft.Model.Shop.Order.OrderReturnGoods returnModel = returnGoodsBLL.GetModel(returnId);

                returnModel.Status = (int)EnumHelper.Status.Complete;
                returnModel.LogisticStatus = (int)EnumHelper.LogisticStatus.Receipt;
                returnModel.RefundStatus = (int)EnumHelper.RefundStatus.UnRefund;

                if (returnGoodsBLL.Update(returnModel))
                {
                    Shop_ReturnOrderAction actionModel = new Shop_ReturnOrderAction();
                    actionModel.ReturnOrderId = returnModel.Id;
                    actionModel.ReturnOrderCode = returnModel.OrderCode;
                    actionModel.UserId = CurrentUser.UserID;
                    actionModel.UserName = CurrentUser.UserName.ToString();
                    actionModel.ActionDate = DateTime.Now;
                    actionModel.ActionCode = "SHCZ" + DateTime.Now.ToString("yyyyMMddhhMMss");
                    actionModel.Remark = "收货操作";
                    returnGoodsBLL.ReturnAccount(Model.Shop.Order.EnumHelper.ReturnAccountStatus.ReturnSuccess, returnId, CurrentUser.UserName, "确认已收货");
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                    gridView.OnBind();

                }
                else
                {
                    MessageBox.ShowFailTip(this, "操作失败!");
                }

            }
            //if (e.CommandName == "ApprovePass")
            //{
            //    object obj = e.CommandArgument;
            //    long returnId = Common.Globals.SafeLong(obj, 0);
            //    returnGoodsBLL.ApproveReturnOrder(Model.Shop.Order.EnumHelper.ReturnApproveStatus.Pass, returnId, CurrentUser.UserName, "审核通过");
            //    MessageBox.ShowSuccessTip(this, "操作成功！");
            //    gridView.OnBind();
            //}
            //if (e.CommandName == "ApproveNoPass")
            //{
            //    object obj = e.CommandArgument;
            //    long returnId = Common.Globals.SafeLong(obj, 0);
            //    returnGoodsBLL.ApproveReturnOrder(Model.Shop.Order.EnumHelper.ReturnApproveStatus.NoPass, returnId, CurrentUser.UserName, "审核不通过");
            //    MessageBox.ShowSuccessTip(this, "操作成功！");
            //    gridView.OnBind();
            //}
            //if (e.CommandName == "returnAmount")
            //{
            //    object obj = e.CommandArgument;
            //    long returnId = Common.Globals.SafeLong(obj, 0);
            //    returnGoodsBLL.ReturnAccount(Model.Shop.Order.EnumHelper.ReturnAccountStatus.ReturnSuccess, returnId, CurrentUser.UserName, "退款成功");
            //    MessageBox.ShowSuccessTip(this, "操作成功！");
            //    gridView.OnBind();
            //}
        }
        */
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }


        /// <summary>
        ///获取订单的 退货状态
        /// </summary>
        /// <param name="returnId"></param>
        /// <returns></returns>
        public string GetReturnStatus(object returnId)
        {
            long id = Common.Globals.SafeLong(returnId, 0);

            Maticsoft.Model.Shop.Order.OrderReturnGoods model = returnGoodsBLL.GetModel(id);
            object Status = model.Status;
            object LogisticStatus = model.LogisticStatus;
            object RefundStatus = model.RefundStatus;
            if (Status == null)
            {
                return "待审核";
            }
            else if (Status.ToString() == "0")
            {
                return "待审核";
            }
            else if (Status.ToString() == "-3")
            {
                return "审核不通过";
            }
            if (Status.ToString() == "1" && LogisticStatus.ToString() == "0")
            {
                return "未发货";
            }
            if (Status.ToString() == "1" && LogisticStatus.ToString() == "1")
            {
                return "已发货";
            }
            if (Status.ToString() == "1" && LogisticStatus.ToString() == "2")
            {
                return "已收货";
            }
            if (Status.ToString() == "1" && LogisticStatus.ToString() == "2" && RefundStatus.ToString() == "1")
            {
                return "未退款";
            }
            if (Status.ToString() == "1" && LogisticStatus.ToString() == "2" && RefundStatus.ToString() == "2")
            {
                return "已退款";
            }
            if (Status.ToString() == "1" && LogisticStatus.ToString() == "4" && RefundStatus.ToString() == "0")
            {
                return "拒收退货";
            }
            return "待审核";
        }

        #region  方法
        /// <summary>
        /// 获取订单的 退货状态
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="LogisticStatus"></param>
        /// <returns></returns>
        protected string GetMainStatusStr(int Status, int RefundStatus, int LogisticStatus, int ReturnGoodsType, int ShippingStatus)
        {
            EnumHelper.MainStatus type = GetMainStatus(Status, RefundStatus, LogisticStatus, ReturnGoodsType ,ShippingStatus);
            switch (type)
            {
                case EnumHelper.MainStatus.Auditing:
                    return "等待审核";
                case EnumHelper.MainStatus.Cancel:
                    return "已取消";
                case EnumHelper.MainStatus.Refuse:
                    return "审核未通过";
                case EnumHelper.MainStatus.Handling:
                    return "等待发货";//正在处理
                case EnumHelper.MainStatus.Packing:
                    return "已发货";//取货中
                case EnumHelper.MainStatus.Returning:
                    return "已收货";//返程中
                case EnumHelper.MainStatus.WaitingRefund:
                    return "等待退款";
                case EnumHelper.MainStatus.Refunding:
                    return "退款完成";
                case EnumHelper.MainStatus.Complete:
                    return "已完成";
                case EnumHelper.MainStatus.Return:
                    return "拒收退货";
                case EnumHelper.MainStatus.storage:
                    return "客户确定收货";
                case EnumHelper.MainStatus.journey:
                    return "等待用户确认收货";
                case EnumHelper.MainStatus.RefuseRepair:
                    return "拒绝维修";
                case EnumHelper.MainStatus.RefuseAdjustable:
                    return "拒绝调货";
            }
            return "";
        }

        /// <summary>
        ///  获取组合状态
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="LogisticStatus"></param>
        /// <returns></returns>
        public EnumHelper.MainStatus GetMainStatus(int Status, int RefundStatus, int LogisticStatus, int ReturnGoodsType, int ShippingStatus)
        {
            EnumHelper.MainStatus type = EnumHelper.MainStatus.Auditing;
            #region 未审核
            //等待审核
            if (Status == (int)EnumHelper.Status.UnHandle)
            {
                return type = EnumHelper.MainStatus.Auditing;
            }

            //取消申请
            if (Status == (int)EnumHelper.Status.Cancel &&
                LogisticStatus == (int)EnumHelper.RefundStatus.UnRefund)
            {
                return type = EnumHelper.MainStatus.Cancel;
            }
            #endregion
            #region 审核未通过
            //拒绝
            if (Status == (int)EnumHelper.Status.Refuse)
            {
                return type = EnumHelper.MainStatus.Refuse;
            }
            #endregion
            #region 审核 发货
            if (Status == (int)EnumHelper.Status.Handling && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Goods)
            {
                //等待发货 - 未发货 - 等待处理
                if (LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery)
                {
                    return type = EnumHelper.MainStatus.Handling;
                }
                //已发货
                if (LogisticStatus == (int)EnumHelper.LogisticStatus.Shipped)
                {
                    return type = EnumHelper.MainStatus.Packing;
                }
                //已收货
                if (LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt)
                {
                    return type = EnumHelper.MainStatus.Returning;
                }
                ////拒收退货 +欧阳+
                if (LogisticStatus == (int)EnumHelper.LogisticStatus.storage)
                {
                    return type = EnumHelper.MainStatus.Return;
                }
            }
            #endregion
            #region 审核 维修  调货
            //已维修 发货
            if (Status == (int)EnumHelper.Status.Handling && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.Returnjourney && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Goods)
            {
                return type = EnumHelper.MainStatus.journey;
            }
            //拒绝维修
            if (Status == (int)EnumHelper.Status.Handling && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.RefuseRepair && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Goods)
            {
                return type = EnumHelper.MainStatus.RefuseRepair;
            }
            //拒绝调货
            if (Status == (int)EnumHelper.Status.Handling && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.RefuseAdjustable && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Goods)
            {
                return type = EnumHelper.MainStatus.RefuseAdjustable;
            }
            //客户确认收货
            if (Status == (int)EnumHelper.Status.Handling && RefundStatus == (int)EnumHelper.RefundStatus.UnRefund && LogisticStatus == (int)EnumHelper.LogisticStatus.storage && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Goods)
            {
                return type = EnumHelper.MainStatus.storage;
            }
            //直接申请退款 ->已经退款
            if (Status == (int)EnumHelper.Status.Complete && RefundStatus == (int)EnumHelper.RefundStatus.Refunds && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Money)
            {
                return type = EnumHelper.MainStatus.Refunding;
            }
            if (Status == (int)EnumHelper.Status.Complete && RefundStatus == (int)EnumHelper.RefundStatus.Refunds && LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Goods)
            {
                return type = EnumHelper.MainStatus.Refunding;
            }
            #endregion
            #region 已审核并确认收货才进行退款 或者 已审核,卖家未发货,买家已付款
            if ((Status == (int)EnumHelper.Status.Handling && LogisticStatus == (int)EnumHelper.LogisticStatus.Receipt) || (LogisticStatus == (int)EnumHelper.LogisticStatus.NODelivery && Status == (int)EnumHelper.Status.Handling && ReturnGoodsType == (int)EnumHelper.ReturnGoodsType.Money && ShippingStatus == (int)EnumHelper.ShippingStatus.UnShipped))
            {
                ////正在处理
                //if (RefundStatus == (int)EnumHelper.RefundStatus.UnRefund)
                //{
                //    return type = EnumHelper.MainStatus.Handling;
                //}
                //等待退款
                if (RefundStatus == (int)EnumHelper.RefundStatus.UnRefund)
                {
                    return type = EnumHelper.MainStatus.WaitingRefund;
                }
                //退款中
                if (RefundStatus == (int)EnumHelper.RefundStatus.Refunding)
                {
                    return type = EnumHelper.MainStatus.Refunding;
                }
                //已完成
                if (RefundStatus == (int)EnumHelper.RefundStatus.Refunds)
                {
                    return type = EnumHelper.MainStatus.Complete;
                }
            }
            return type;
            #endregion
        }


        #endregion

        /// <summary>
        /// 只有审核成功了才会显示该退款操作
        /// </summary>
        /// <param name="approveStatus"></param>
        /// <param name="accountStatus"></param>
        /// <returns></returns>
        public bool GetApproveStatus(object approveStatus, object accountStatus)
        {
            if (approveStatus == null || approveStatus == DBNull.Value)
            {
                return false;
            }
            if (accountStatus.ToString() == "2" && accountStatus != null && accountStatus != DBNull.Value && accountStatus.ToString() != "2")
            {
                return true;
            }
            return false;
        }

        public string GetUserNameById(object userIdStr)
        {
            if (userIdStr != null && userIdStr != DBNull.Value)
            {
                int userId = Common.Globals.SafeInt(userIdStr, 0);
                Model.Members.Users userModel = memberBLL.GetModel(userId);
                if (userModel != null)
                {
                    return userModel.NickName;
                }
            }
            return "";
        }

    }
}