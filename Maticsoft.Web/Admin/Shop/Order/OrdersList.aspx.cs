using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Order;
using Orders = Maticsoft.BLL.Shop.Order.Orders;
using OrdersHistory = Maticsoft.BLL.Shop.Order.OrdersHistory;
using System.Text.RegularExpressions;

namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrdersList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 443; } } //Shop_订单管理_列表页
        protected new int Act_UpdateData = 444;    //Shop_订单管理_编辑数据
        public Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
        public Maticsoft.BLL.Shop.Order.OrdersHistory historyBll = new OrdersHistory();
        public Maticsoft.BLL.Shop.Order.OrderAction actionBll = new BLL.Shop.Order.OrderAction();
        public int Type = 0;
        public Maticsoft.BLL.Shop.Order.OrderItems itemBll = new BLL.Shop.Order.OrderItems();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Type = OrderStatus;
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

                //获取是否开启相关选项卡
                this.hfPaying.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_Paying");
                this.hfPreHandle.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_PreHandle");
                this.hfCancel.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_Cancel");

                this.hfLocking.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_Locking");
                this.hfPreConfirm.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_PreConfirm");
                this.hfHandling.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_Handling");

                this.hfShipping.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_Shipping");
                this.hfShiped.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_Shiped");
                this.hfSuccess.Value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_OrderList_Complete");
                BindSupplier();
            }
        }

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView


        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();

            if (OrderStatus > 0)
            {
                strWhere.Append(orderBll.GetWhereByStatus(OrderStatus));
            }
            if (!string.IsNullOrWhiteSpace(this.txtOrderCode.Text.Trim()))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" OrderCode like '%{0}%'", InjectionFilter.QuoteFilter(txtOrderCode.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(this.txtShipName.Text.Trim()))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShipName like '%{0}%'", InjectionFilter.QuoteFilter(txtShipName.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(this.txtBuyerName.Text.Trim()))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" BuyerName like '%{0}%'", InjectionFilter.QuoteFilter(txtBuyerName.Text.Trim()));
            }
            if (dropPaymentStatus.SelectedValue != "-1")
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" PaymentStatus = {0}", dropPaymentStatus.SelectedValue);
            }
            if (dropShippingStatus.SelectedValue != "-1")
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ShippingStatus = {0}", dropShippingStatus.SelectedValue);
            }
            if (PageValidate.IsDateTime(this.txtCreatedDateEnd.Text.Trim()) && PageValidate.IsDateTime(this.txtCreatedDateStart.Text.Trim()))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" CreatedDate between  '{0}' and  '{1}' ", InjectionFilter.QuoteFilter(txtCreatedDateStart.Text.Trim()), InjectionFilter.QuoteFilter(this.txtCreatedDateEnd.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(this.txtOrderID.Text.Trim()))
            {
                Int64 orderid = GetNumberInt(this.txtOrderID.Text.Trim());
                if (orderid > 0)
                {
                    if (strWhere.Length > 1)
                    {
                        strWhere.Append(" and ");
                    }
                    strWhere.AppendFormat(" T.OrderId= '{0}' ", orderid);
                }
            }
            if (!string.IsNullOrWhiteSpace(this.txtAddress.Text.Trim()))
            {
                string txtAddress = this.txtAddress.Text.Trim();
                if (txtAddress !="")
                {
                    if (strWhere.Length > 1)
                    {
                        strWhere.Append(" and ");
                    }
                    strWhere.AppendFormat(" T.ShipAddress like '%{0}%' ", txtAddress);
                }
            }
            int supplierId = Common.Globals.SafeInt(this.ddlSupplier.SelectedValue, 0);
            if (supplierId == 0) supplierId = SupplierId;
            if (supplierId > 0)
            {
                if (strWhere.Length > 1) strWhere.Append(" and ");
                strWhere.AppendFormat(" SupplierId = {0}", supplierId);
            }
            if (supplierId == -1)
            {
                if (strWhere.Length > 1) strWhere.Append(" and ");
                strWhere.AppendFormat(" SupplierId IS NULL");
            }

            if (strWhere.Length > 1) strWhere.Append(" and ");

            //主订单，包含子订单，但只显示主订单
            //strWhere.AppendFormat(" (ParentOrderId =-1 ) " +

            //主订单 无子订单
            strWhere.AppendFormat(" ((OrderType = 1 AND HasChildren = 0) " +
                //子订单 已支付 或 货到付款/银行转账
            "OR (OrderType = 2 AND (PaymentStatus > 1 OR (PaymentGateway='cod' OR PaymentGateway='bank')) ) " +
                //主订单 有子订单 未支付的主订单 非 货到付款/银行转账 子订单
            "OR (OrderType = 1 AND HasChildren = 1 AND PaymentStatus < 2 AND PaymentGateway<>'cod' AND PaymentGateway<>'bank'))");

             gridView.DataSetSource = orderBll.GetList2(-1, strWhere.ToString(), "CreatedDate desc");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #region 获取字符串中的数字
        /// <summary>
        /// 获取字符串中的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Int64 GetNumberInt(string str)
        {
            Int64 result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace(str, "\\D+", "");

                // 如果是数字，则转换为decimal类型 
                if (Regex.IsMatch(str, @"^\d*$"))
                {
                    result = Int64.Parse(str);
                }
            }
            return result;
        }
        #endregion


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

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    LinkButton linkComplete = (LinkButton)e.Row.FindControl("linkComplete");
                    linkComplete.Visible = false;
                    LinkButton linkCancel = (LinkButton)e.Row.FindControl("linkCancel");
                    linkCancel.Visible = false;
                    LinkButton linkReturn = (LinkButton)e.Row.FindControl("linkReturn");
                    linkReturn.Visible = false;
                }
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

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (orderBll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
        }


        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //取消订单
            if (e.CommandName == "CancelOrder")
            {
                object[] arg = e.CommandArgument.ToString().Split(',');  //注意是单引号
                long orderId = Common.Globals.SafeLong(arg[0].ToString(), 0);
                Maticsoft.Model.Shop.Order.OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
                if (Maticsoft.BLL.Shop.Order.OrderManage.CancelOrder(orderInfo, CurrentUser))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败，请稍候再试！");
                }
            }
            if (e.CommandName == "Success")
            {
                object[] arg = e.CommandArgument.ToString().Split(',');  //注意是单引号
                long orderId = Common.Globals.SafeLong(arg[0].ToString(), 0);
                string orderCode = arg[1].ToString();
                OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
                if (BLL.Shop.Order.OrderManage.CompleteOrder(orderInfo, CurrentUser))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败，请稍候再试！");
                }

            }
            if (e.CommandName == "Pay")
            {
                object[] arg = e.CommandArgument.ToString().Split(',');  //注意是单引号
                long orderId = Common.Globals.SafeLong(arg[0].ToString(), 0);
                string orderCode = arg[1].ToString();

                OrderInfo orderInfo = orderBll.GetModel(orderId);
                if (orderInfo != null && BLL.Shop.Order.OrderManage.PayForOrder(orderInfo, "", CurrentUser))
                {
                    MessageBox.ShowSuccessTip(this, "操作成功！");
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, "操作失败，请稍候再试！");
                }
            }
            gridView.OnBind();

        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #endregion


        protected int SupplierId
        {
            get
            {
                int supplierId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["sid"]))
                {
                    supplierId = Common.Globals.SafeInt(Request.Params["sid"], 0);
                }
                return supplierId;
            }
        }

        /// <summary>
        /// 供应商
        /// </summary>
        private void BindSupplier()
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo infoBll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = infoBll.GetList("  Status = 1 ");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlSupplier.DataSource = ds;
                this.ddlSupplier.DataTextField = "Name";
                this.ddlSupplier.DataValueField = "SupplierId";
                this.ddlSupplier.DataBind();
            }
            this.ddlSupplier.Items.Insert(0, new ListItem("平　台", "-1"));
            this.ddlSupplier.Items.Insert(0, new ListItem("全　部", string.Empty));
            this.ddlSupplier.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            if (SupplierId != 0)
            {
                ddlSupplier.SelectedValue = SupplierId.ToString();
            }
            else
            {
                ddlSupplier.SelectedIndex = 0;
            }
        }
        /// <summary>
        /// 获取子菜单
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetOrderChild(object target)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append("");


            return "";
        }


        #region 获取状态
        /// <summary>
        /// 获取订单状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetOrderStatus(object target)
        {
            // -4 系统锁定   | -3 后台锁定 | -2 用户锁定 | -1 死单（取消） | 0 未处理 | 1 进行中 |2 完成  
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "-4":
                        str = "系统锁定";
                        break;
                    case "-3":
                        str = "后台锁定";
                        break;
                    case "-2":
                        str = "用户锁定";
                        break;
                    case "-1":
                        str = "死单（取消）";
                        break;
                    case "0":
                        str = "未处理";
                        break;
                    case "1":
                        str = "进行中";
                        break;
                    case "2":
                        str = "完成";
                        break;
                    default:
                        str = "未知状态";
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取发货状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetShippingStatus(object target)
        {
            //  配送状态 0 未发货 | 1 打包中 | 2 已发货 | 3 已确认收货 | 4 拒收退货中 | 5 拒收已退货
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "未发货";
                        break;
                    case "1":
                        str = "打包中";
                        break;
                    case "2":
                        str = "已发货";
                        break;
                    case "3":
                        str = "已确认收货";
                        break;
                    case "4":
                        str = "拒收退货中";
                        break;
                    case "5":
                        str = "拒收已退货";
                        break;
                    default:
                        str = "未知状态";
                        break;
                }
            }
            return str;
        }


        protected string GetOrderType(object paymentGateway_obj, object orderStatus_obj, object paymentStatus_obj, object shippingStatus_obj)
        {
            // 1 等待买家付款   | 2 等待发货 | 3 已发货 | 4 退款中 | 5 成功订单 | 6 已退款 |7 已退货  |8 已关闭  
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(paymentGateway_obj) && !StringPlus.IsNullOrEmpty(orderStatus_obj) && !StringPlus.IsNullOrEmpty(paymentStatus_obj) && !StringPlus.IsNullOrEmpty(shippingStatus_obj))
            {
                EnumHelper.OrderMainStatus orderType = orderBll.GetOrderType(paymentGateway_obj.ToString(),
                                        Common.Globals.SafeInt(orderStatus_obj.ToString(), 0),
                                        Common.Globals.SafeInt(paymentStatus_obj.ToString(), 0),
                                        Common.Globals.SafeInt(shippingStatus_obj.ToString(), 0));
                switch (orderType)
                {
                    //  订单组合状态 1 等待付款   | 2 等待处理 | 3 取消订单 | 4 订单锁定 | 5 等待付款确认 | 6 正在处理 |7 配货中  |8 已发货 |9  已完成
                    case EnumHelper.OrderMainStatus.Paying:
                        str = "等待付款";
                        break;
                    case EnumHelper.OrderMainStatus.PreHandle:
                        str = "等待处理";
                        break;
                    case EnumHelper.OrderMainStatus.Cancel:
                        str = "取消订单";
                        break;
                    case EnumHelper.OrderMainStatus.Locking:
                        str = "订单锁定";
                        break;
                    case EnumHelper.OrderMainStatus.PreConfirm:
                        str = "等待付款确认";
                        break;
                    case EnumHelper.OrderMainStatus.Handling:
                        str = "正在处理";
                        break;
                    case EnumHelper.OrderMainStatus.Shipping:
                        str = "配货中";
                        break;
                    case EnumHelper.OrderMainStatus.Shiped:
                        str = "已发货";
                        break;
                    case EnumHelper.OrderMainStatus.Complete:
                        str = "已完成";
                        break;
                    default:
                        str = "未知状态";
                        break;
                }
            }
            return str;
        }

        #endregion

        #region 导出订单

        /// <summary>
        /// 新的导出订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExportNew_Click(object sender, EventArgs e)
        {
            BindData();
            DataSet dataSet = gridView.DataSetSource as DataSet;
            if (Common.DataSetTools.DataSetIsNull(dataSet))
            {
                MessageBox.ShowServerBusyTip(this, "抱歉, 当前没有可以导出的数据!");
                return;
            }
            DataTable dataTable = new DataTable();


            dataTable.Columns.Add(new DataColumn("订单编号", typeof(string)));
            dataTable.Columns.Add(new DataColumn("主订单编号", typeof(string)));
            dataTable.Columns.Add(new DataColumn("团购", typeof(string)));
            dataTable.Columns.Add(new DataColumn("订单状态", typeof(string)));

            dataTable.Columns.Add(new DataColumn("收货姓名", typeof(string)));
            dataTable.Columns.Add(new DataColumn("收货地址", typeof(string)));
            dataTable.Columns.Add(new DataColumn("联系电话", typeof(string)));
            dataTable.Columns.Add(new DataColumn("邮编", typeof(string)));
            dataTable.Columns.Add(new DataColumn("商品金额", typeof(string)));
            dataTable.Columns.Add(new DataColumn("优惠金额", typeof(string)));
            dataTable.Columns.Add(new DataColumn("付款金额", typeof(string)));
            dataTable.Columns.Add(new DataColumn("实际运费金额", typeof(string)));
            //dataTable.Columns.Add(new DataColumn("发货类型", typeof(string))); //发货类型
            dataTable.Columns.Add(new DataColumn("快递公司", typeof(string))); //快递公司
            dataTable.Columns.Add(new DataColumn("快递单号", typeof(string))); //快递公司
            dataTable.Columns.Add(new DataColumn("发货日期", typeof(string))); //快递公司

            dataTable.Columns.Add(new DataColumn("下单时间", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("支付时间", typeof(string)));
            dataTable.Columns.Add(new DataColumn("提货商品内容", typeof(string)));
            dataTable.Columns.Add(new DataColumn("提货商品规格", typeof(string)));
            dataTable.Columns.Add(new DataColumn("提货商品数量", typeof(string)));
            dataTable.Columns.Add(new DataColumn("购买商品折后单价", typeof(string)));
            dataTable.Columns.Add(new DataColumn("订单备注", typeof(string)));
            dataTable.Columns.Add(new DataColumn("供应商", typeof(string)));
            dataTable.Columns.Add(new DataColumn("供应商备注", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SKU", typeof(string)));
            dataTable.Columns.Add(new DataColumn("链接", typeof(string)));


            DataRow tmpRow;
            Maticsoft.BLL.Ms.Regions regionBll = new Maticsoft.BLL.Ms.Regions();
            Maticsoft.BLL.Shop.Supplier.SupplierInfo supplierbll = new BLL.Shop.Supplier.SupplierInfo();

            string remark;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                List<OrderItems> items = GetOrderItemsList(row.Field<long>("OrderId"));
                int k = 1;
                foreach (OrderItems item in items)
                {
                    tmpRow = dataTable.NewRow();

                  
                    tmpRow["订单编号"] = row.Field<string>("OrderCode");

                    tmpRow["订单状态"] = GetOrderType(row.Field<string>("PaymentGateway"), row["OrderStatus"], row["PaymentStatus"], row["ShippingStatus"]);
                   
                    tmpRow["收货姓名"] = row.Field<string>("ShipName");
                    tmpRow["收货地址"] = regionBll.GetRegionNameByRID(row.Field<int>("RegionId")) + " " +
                        row.Field<string>("ShipAddress");
                    tmpRow["联系电话"] = row.Field<string>("ShipCellPhone");
                    tmpRow["邮编"] = row.Field<string>("ShipZipCode");
                    tmpRow["商品金额"] = (item.AdjustedPrice*item.Quantity).ToString("F");
                    tmpRow["优惠金额"] = row.Field<decimal>("CouponAmount").ToString("F");
                    tmpRow["付款金额"] = row.Field<decimal>("Amount").ToString("F");
                    tmpRow["主订单编号"] ="";
                    tmpRow["团购"] = "否";
                    if (Int32.Parse(row["ParentOrderId"].ToString()) > 1)
                    {
                        Model.Shop.Order.OrderInfo orderinfo = orderBll.GetModel(Int32.Parse(row["ParentOrderId"].ToString()));
                        if (orderinfo.GroupBuyId.HasValue)
                        {
                            tmpRow["团购"] = orderinfo.GroupBuyId.Value > 0?"是":"否";
                        }
                        tmpRow["主订单编号"] =orderinfo.OrderCode;
                        tmpRow["付款金额"] = orderinfo.Amount.ToString("F");
                    }
                    tmpRow["实际运费金额"] = row.Field<decimal>("FreightActual").ToString("F");
                    //tmpRow["发货类型"] = row.Field<int>("OrderSendType").ToString() == "0" ? "正常" : "预售";
                    tmpRow["快递公司"] = row.Field<string>("ExpressCompanyName");
                    tmpRow["快递单号"] = row.Field<string>("ShipOrderNumber");

                    tmpRow["发货日期"] = GetActiveDate(row.Field<long>("OrderId"), 104);

                    //TODO: 没有付款时间, 只有创建时间和最后操作时间
                    tmpRow["下单时间"] = row.Field<DateTime>("CreatedDate");
                    tmpRow["支付时间"] = GetActiveDate(row.Field<long>("OrderId"), 102);

                    tmpRow["提货商品内容"] = item.Name+"*"+item.Quantity;
                    tmpRow["提货商品规格"] = item.Attribute;
                    tmpRow["提货商品数量"] = item.Quantity;
                    tmpRow["购买商品折后单价"] = item.AdjustedPrice;

                    remark = row.Field<string>("Remark") + " | " + row.Field<string>("Remark");
                    tmpRow["订单备注"] = remark == " | " ? "" : remark;
                    tmpRow["供应商"] = item.SupplierName;

                    if (Convert.IsDBNull(item.SupplierId))
                    {
                        tmpRow["供应商备注"] = supplierbll.GetModel(Common.Globals.SafeInt(item.SupplierId, 0)).Remark;
                    }
                    else
                    {
                        tmpRow["供应商备注"] = "";
                    }

                    tmpRow["SKU"] = item.SKU;
                    tmpRow["链接"] = "http://www.zhenhaolin.com/Product/Detail/" + item.ProductId;
                    dataTable.Rows.Add(tmpRow);
                }
            }
            DataSetToExcel(dataTable,"新一键导出");
        }
        public string GetActiveDate(long orderId, int ActionCode)
        {
            string sqlwhere = string.Format("orderid={0} and ActionCode={1} ", orderId, ActionCode);
            DataSet ds = actionBll.GetList(sqlwhere);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["ActionDate"].ToString();
                }
            }
            return "";
        }
        private List<OrderItems> GetOrderItemsList(long orderId)
        {
            List<OrderItems> list = orderItemManage.GetModelListByCache(" OrderId=" + orderId);
            return list;
        }

        BLL.Shop.Order.OrderItems orderItemManage = new BLL.Shop.Order.OrderItems();
        private string GetProductContent(long orderId)
        {
            StringBuilder sb = new StringBuilder();
            List<OrderItems> list = orderItemManage.GetModelListByCache(" OrderId=" + orderId);
            if (list == null || list.Count < 1) return string.Empty;
            list.ForEach(info => sb.AppendFormat("{0} x{1} |", info.Name, info.Quantity));
            return sb.ToString().TrimEnd('|');
        }

        private void DataSetToExcel(DataTable data,string fileName)
        {
            Response.Clear();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                string nowDate = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.HSSF.UserModel.HSSFSheet sheet = (NPOI.HSSF.UserModel.HSSFSheet)workbook.CreateSheet(string.Format("导出订单_{0}",nowDate));
                NPOI.HSSF.UserModel.HSSFCellStyle cellStyle = (NPOI.HSSF.UserModel.HSSFCellStyle)workbook.CreateCellStyle();
                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;
                cellStyle.FillPattern = NPOI.SS.UserModel.FillPatternType.SOLID_FOREGROUND;
                NPOI.SS.UserModel.IFont f = workbook.CreateFont();
                f.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
                cellStyle.SetFont(f);
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.MEDIUM;
                cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
                NPOI.HSSF.UserModel.HSSFRow headerRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(0);
                headerRow.Height = 600;

                //headerRow.RowStyle.FillBackgroundColor = 13;
                foreach (DataColumn column in data.Columns)
                {
                    NPOI.HSSF.UserModel.HSSFCell Cell = (NPOI.HSSF.UserModel.HSSFCell)headerRow.CreateCell(column.Ordinal);
                    Cell.SetCellValue(column.ColumnName);
                    Cell.CellStyle = cellStyle;
                }
                int rowIndex = 1;
                foreach (DataRow row in data.Rows)
                {
                    NPOI.HSSF.UserModel.HSSFRow dataRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in data.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    dataRow = null;
                    rowIndex++;
                }
                //自动调整列宽
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                workbook.Write(ms);
                headerRow = null;
                sheet = null;
                workbook = null;
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + fileName + "_{0}.xls", nowDate));
                Response.BinaryWrite(ms.ToArray());
                ms.Close();
                ms.Dispose();
            }
            Response.End();
        }


        private void DataSetToExcel4Finance(DataTable data,string fileName)
        {
            Response.Clear();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                string nowDate = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.HSSF.UserModel.HSSFSheet sheet = (NPOI.HSSF.UserModel.HSSFSheet)workbook.CreateSheet(string.Format("导出订单_{0}", nowDate));
                NPOI.HSSF.UserModel.HSSFCellStyle cellStyle = (NPOI.HSSF.UserModel.HSSFCellStyle)workbook.CreateCellStyle();
                cellStyle.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LIGHT_BLUE.index;
                cellStyle.FillPattern = NPOI.SS.UserModel.FillPatternType.SOLID_FOREGROUND;
                NPOI.SS.UserModel.IFont f = workbook.CreateFont();
                f.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
                cellStyle.SetFont(f);
                cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.MEDIUM;
                cellStyle.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;
                NPOI.HSSF.UserModel.HSSFRow headerRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(0);
                headerRow.Height = 600;

                //headerRow.RowStyle.FillBackgroundColor = 13;
                foreach (DataColumn column in data.Columns)
                {
                    if (column.ColumnName == "Tag")
                    {
                        continue;
                    }
                    NPOI.HSSF.UserModel.HSSFCell Cell = (NPOI.HSSF.UserModel.HSSFCell)headerRow.CreateCell(column.Ordinal);
                    Cell.SetCellValue(column.ColumnName);
                    Cell.CellStyle = cellStyle;
                }
                int rowIndex = 1;

                NPOI.HSSF.UserModel.HSSFCellStyle Style = (NPOI.HSSF.UserModel.HSSFCellStyle)workbook.CreateCellStyle();
                Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.YELLOW.index;
                Style.FillPattern = NPOI.SS.UserModel.FillPatternType.SOLID_FOREGROUND;
                NPOI.SS.UserModel.IFont f2 = workbook.CreateFont();
                f.Color = NPOI.HSSF.Util.HSSFColor.WHITE.index;
                Style.SetFont(f2);
                Style.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.CENTER;

                bool tag = false;
                foreach (DataRow row in data.Rows)
                {
                    if (row["Tag"].ToString() == "1")
                    {
                        tag = true;
                    }

                    NPOI.HSSF.UserModel.HSSFRow dataRow = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in data.Columns)
                    {
                        if (column.ColumnName == "Tag")
                        {
                            continue;
                        }

                        NPOI.HSSF.UserModel.HSSFCell Cell = (NPOI.HSSF.UserModel.HSSFCell)dataRow.CreateCell(column.Ordinal);
                        Cell.SetCellValue(row[column].ToString());
                        if (tag)
                        {
                            Cell.CellStyle = Style;
                        }

                    }
                    tag = false;
                    dataRow = null;
                    rowIndex++;
                }
                //自动调整列宽
                for (int i = 0; i < data.Columns.Count; i++)
                {
                    sheet.AutoSizeColumn(i);
                }
                workbook.Write(ms);
                headerRow = null;
                sheet = null;
                workbook = null;
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + fileName + "_{0}.xls", nowDate));
                Response.BinaryWrite(ms.ToArray());
                ms.Close();
                ms.Dispose();
            }
            Response.End();
        }
        #endregion

        protected void btnExportOrderDetail_Click(object sender, EventArgs e)
        {
            BindData();
            DataSet dataSet = gridView.DataSetSource as DataSet;
            if (Common.DataSetTools.DataSetIsNull(dataSet))
            {
                MessageBox.ShowServerBusyTip(this, "抱歉, 当前没有可以导出的数据!");
                return;
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("主订单号（拆分前）", typeof(string)));
            dataTable.Columns.Add(new DataColumn("子订单号（拆分后）", typeof(string)));
            dataTable.Columns.Add(new DataColumn("主订单ID", typeof(Int64)));
            dataTable.Columns.Add(new DataColumn("订单日期", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("商品名称", typeof(string)));
            dataTable.Columns.Add(new DataColumn("销售定价", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("成本价", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("支付方式", typeof(string)));
            dataTable.Columns.Add(new DataColumn("实际收款（不含运费）", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("优惠券", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("运费", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("实际付款（含运费）", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("付款日期", typeof(DateTime)));


            DataRow tmpRow;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                //获取单明列表
                DataSet ds = itemBll.GetListByCache(" OrderId=" + row.Field<Int64>("OrderId") + " Order by SupplierId ");
                tmpRow = dataTable.NewRow();
                string str_ProductName = string.Empty;
                decimal d_SalePrice = 0;
                decimal d_CostPrice = 0;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str_ProductName = "@" + dr["Name"] + "/" + dr["SKU"] + "/" + dr["Quantity"];
                    d_SalePrice += Convert.ToDecimal(dr["AdjustedPrice"]) * Convert.ToInt32(dr["Quantity"]);
                    d_CostPrice += Convert.ToDecimal(dr["CostPrice"]) * Convert.ToInt32(dr["Quantity"]);
                }

                decimal nofreight = decimal.Round(d_SalePrice, 2);
                tmpRow["成本价"] = decimal.Round(d_CostPrice, 2);
                tmpRow["销售定价"] = nofreight;
                tmpRow["主订单号（拆分前）"] = row.Field<Int16>("OrderType") == 1 ? row.Field<string>("OrderCode") : row.Field<string>("ParentOrderCode");
                tmpRow["子订单号（拆分后）"] = row.Field<Int16>("OrderType") == 1 ? "-" : row.Field<string>("OrderCode");
                decimal freight = 0;
                freight = (row.Field<decimal>("FreightAdjusted"));
                tmpRow["主订单ID"] = row.Field<Int64>("ParentOrderId") == -1 ? row.Field<Int64>("OrderId") : row.Field<Int64>("ParentOrderId");
                tmpRow["订单日期"] = row.Field<DateTime>("CreatedDate");
                tmpRow["支付方式"] = row.Field<string>("PaymentTypeName");
                tmpRow["运费"] = decimal.Round(freight, 2);
                tmpRow["商品名称"] = str_ProductName;
                tmpRow["优惠券"] = decimal.Round(row.Field<decimal>("CouponAmount"), 2);
                if (row["PayDate"].ToString().Trim() != "")
                {
                    tmpRow["付款日期"] = row.Field<DateTime>("PayDate");
                }
                tmpRow["实际付款（含运费）"] = decimal.Round(row.Field<decimal>("Amount"), 2);
                tmpRow["实际收款（不含运费）"] = decimal.Round((row.Field<decimal>("Amount") - row.Field<decimal>("FreightAdjusted")), 2);
                dataTable.Rows.Add(tmpRow);
            }
            DataSetToExcel(dataTable,"订单明细");
        }

        protected void btnExportProductDetail_Click(object sender, EventArgs e)
        {
            BindData();
            DataSet dataSet = gridView.DataSetSource as DataSet;
            if (Common.DataSetTools.DataSetIsNull(dataSet))
            {
                MessageBox.ShowServerBusyTip(this, "抱歉, 当前没有可以导出的数据!");
                return;
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("主订单号（拆分前）", typeof(string)));
            dataTable.Columns.Add(new DataColumn("子订单号（拆分后）", typeof(string)));
            dataTable.Columns.Add(new DataColumn("主订单ID", typeof(Int64)));
            dataTable.Columns.Add(new DataColumn("订单日期", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("供应商名称", typeof(string)));
            dataTable.Columns.Add(new DataColumn("商品名称", typeof(string)));
            dataTable.Columns.Add(new DataColumn("商品货号", typeof(string)));
            dataTable.Columns.Add(new DataColumn("商品数量", typeof(int)));
            dataTable.Columns.Add(new DataColumn("销售定价", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("成本价", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("支付方式", typeof(string)));
            dataTable.Columns.Add(new DataColumn("实际收款（不含运费）", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("优惠券", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("运费", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("实际付款（含运费）", typeof(decimal)));
            dataTable.Columns.Add(new DataColumn("付款日期", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("客户名称", typeof(string)));
            dataTable.Columns.Add(new DataColumn("客户地址", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Tag", typeof(string)));


            DataRow tmpRow;
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                //获取单明列表
                DataSet ds = itemBll.GetListByCache(" OrderId=" + row.Field<Int64>("OrderId") + " Order by SupplierId ");
                //查看子订单是否有不同商家，不同的需要标记颜色
                //bool tag = orderBll.GetProductTypeCount(row.Field<Int64>("OrderId"));
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    tmpRow = dataTable.NewRow();
                    tmpRow["Tag"] = row.Field<Int16>("OrderType") == 1 ? 0 : 1;
                    tmpRow["子订单号（拆分后）"] = row.Field<Int16>("OrderType") == 1 ? "-" : row.Field<string>("OrderCode");

                    decimal nofreight = decimal.Round((dr.Field<decimal>("AdjustedPrice") * dr.Field<int>("Quantity")), 2);
                    tmpRow["成本价"] = decimal.Round(dr.Field<decimal>("CostPrice") * dr.Field<int>("Quantity"), 2);
                    tmpRow["销售定价"] = nofreight;
                    tmpRow["主订单号（拆分前）"] = row.Field<Int16>("OrderType") == 1 ? row.Field<string>("OrderCode") : row.Field<string>("ParentOrderCode");
                    decimal freight = 0;
                    freight = (row.Field<decimal>("FreightAdjusted"));
                    tmpRow["主订单ID"] = row.Field<Int64>("ParentOrderId") == -1 ? row.Field<Int64>("OrderId") : row.Field<Int64>("ParentOrderId");
                    tmpRow["订单日期"] = row.Field<DateTime>("CreatedDate");
                    tmpRow["支付方式"] = row.Field<string>("PaymentTypeName");
                    tmpRow["运费"] = decimal.Round(freight, 2);
                    tmpRow["客户名称"] = row.Field<string>("ShipName");
                    tmpRow["客户地址"] = row.Field<string>("ShipAddress");
                    tmpRow["供应商名称"] = dr["SupplierName"];
                    tmpRow["商品名称"] = dr["Name"];
                    tmpRow["商品货号"] = dr["SKU"];
                    tmpRow["商品数量"] = dr["Quantity"];

                    decimal percent = 0;
                    if (row.Field<decimal>("OrderTotal") == 0)
                    {
                        percent = 1;
                    }
                    else
                    {
                        percent = nofreight / row.Field<decimal>("OrderTotal");
                    }

                    if (row["PayDate"].ToString().Trim() != "")
                    {
                        tmpRow["付款日期"] = row.Field<DateTime>("PayDate");
                    }

                    if (nofreight != 0)
                    {
                        decimal coupon = percent * row.Field<decimal>("CouponAmount");

                        if (coupon <= nofreight)
                        {
                            tmpRow["优惠券"] = decimal.Round(coupon, 2);
                        }
                        else
                        {
                            tmpRow["优惠券"] = decimal.Round(nofreight, 2);
                        }
                    }
                    else
                    {
                        tmpRow["优惠券"] = 0.0;

                    }
                    tmpRow["实际付款（含运费）"] = decimal.Round((row.Field<decimal>("Amount") - row.Field<decimal>("FreightAdjusted")) * percent + freight, 2);
                    tmpRow["实际收款（不含运费）"] = decimal.Round((row.Field<decimal>("Amount") - row.Field<decimal>("FreightAdjusted")) * percent, 2);
                    dataTable.Rows.Add(tmpRow);
                }
            }

            DataSetToExcel4Finance(dataTable,"商品明细");
        }
    }
}