using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Ms;
using Maticsoft.BLL.Shop.Order;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.Shipping;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Order;
using OrderItems = Maticsoft.BLL.Shop.Order.OrderItems;
using System.IO;
using System.Text;
using System.Net;
using Maticsoft.Model.Shop.Shipping;
using Newtonsoft.Json;


namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrderShip : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 446; } } //Shop_订单管理_发货页
        private Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
        private Maticsoft.BLL.Shop.Order.OrderItems itemBll = new OrderItems();
        private Maticsoft.BLL.Shop.Shipping.ShippingType typeBll = new Maticsoft.BLL.Shop.Shipping.ShippingType();
        private Maticsoft.BLL.Shop.Products.SKUInfo skuBll = new SKUInfo();
        private  Maticsoft.BLL.Ms.Regions regionBll=new Regions();
        private  Maticsoft.BLL.Shop.Order.OrderAction actionBll=new Maticsoft.BLL.Shop.Order.OrderAction();
        private BLL.Shop.Shippers shippersBll = new BLL.Shop.Shippers();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
                LoadShippers();
                LoadExpressTemp();
            }
        }

        #region 快递单打印
        #region 发货人信息
        // dropShippers
        //加载发货人
        private void LoadShippers()
        {
            List<Model.Shop.Shippers> list = shippersBll.GetModelList("");
            dropShippers.DataSource = list;
            dropShippers.DataTextField = "ShipperTag";
            dropShippers.DataValueField = "ShipperId";
            dropShippers.DataBind();
            dropShippers.Items.Insert(0, new ListItem("请选择发货标签", "0"));
            foreach (Model.Shop.Shippers item in list)
            {
                if (item.IsDefault)
                {
                    dropShippers.SelectedValue = item.ShipperId.ToString();
                    regionListShip.Region_iID = item.RegionId;
                    labelTelPhone.Text = item.TelPhone;
                    labelZipcode.Text = item.Zipcode;
                    lalelShipperName.Text = item.ShipperName;
                    break;
                }
            }
          
        }
        protected void dropShippers_Changed(object sender, EventArgs e)
        {
            Model.Shop.Shippers shipperModel = shippersBll.GetModelByCache(Globals.SafeInt(dropShippers.SelectedValue,0));
            if (shipperModel != null)
            {
                dropShippers.SelectedValue = shipperModel.ShipperId.ToString();
                regionListShip.Region_iID = shipperModel.RegionId;
                labelTelPhone.Text = shipperModel.TelPhone;
                labelZipcode.Text = shipperModel.Zipcode;
                lalelShipperName.Text = shipperModel.ShipperName;
            }
            else
            {
                dropShippers.SelectedValue ="0";
                regionListShip.Area_iID = 0;
                labelTelPhone.Text = "";
                labelZipcode.Text = "";
                lalelShipperName.Text = "";
            }

        }
        #endregion
        BLL.Shop.Sales.ExpressTemplate expresstempBll = new BLL.Shop.Sales.ExpressTemplate();
        #region 选择快递单模版
        private void LoadExpressTemp()
        {
            dropExpressTemp.DataSource = expresstempBll.GetList(" IsUse=1 ");
            dropExpressTemp.DataTextField = "ExpressName";
            dropExpressTemp.DataValueField = "XmlFile";
            dropExpressTemp.DataBind();
            dropExpressTemp.Items.Insert(0, new ListItem("请选择", ""));
        }
        
        #endregion

        #endregion

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

                this.lblTitle.Text = "正在进行订单【" + model.OrderCode + "】发货操作";
                lblOrderCode.Text = model.OrderCode;
                lblCreatedDate.Text = model.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtShipName.Text = model.ShipName;
                lblShipZipCode.Text = model.ShipZipCode;
                txtShipAddress.Text = model.ShipAddress;
                txtShipCellPhone.Text = model.ShipCellPhone;
                lblShipEmail.Text = model.ShipEmail;
                RegionList.Region_iID = model.RegionId.HasValue?model.RegionId.Value:0;
                //lblShipRegion.Text = model.ShipRegion;
                txtShipTelPhone.Text = model.ShipTelPhone;

                lblBuyerEmail.Text = model.BuyerEmail;
                lblBuyerCellPhone.Text = model.BuyerCellPhone;
                lblBuyerName.Text = model.BuyerName;

                txtShipOrderNumber.Text = model.ShipOrderNumber;


                hfWeight.Value = model.Weight.ToString();
                txtFreightActual.Text = model.FreightActual.HasValue ? model.FreightActual.Value.ToString("F") : "0.00";
                txtFreightAdjusted.Text = model.FreightAdjusted.HasValue ? model.FreightAdjusted.Value.ToString("F") : "0.00";

                //加载物流方式
                this.ddlShipType.DataSource = typeBll.GetList("");
                ddlShipType.DataTextField = "Name";
                ddlShipType.DataValueField = "ModeId";
                ddlShipType.DataBind();
                ddlShipType.Items.Insert(0, new ListItem("请选择配送方式", "0"));
                ddlShipType.SelectedValue = model.ShippingModeId.HasValue ? model.ShippingModeId.Value.ToString() : "0";

                //快递单打印
                labelCellPhone.Text = model.ShipCellPhone;
                labelShipAddress.Text = model.ShipAddress;
                labelShipEmail.Text = model.ShipEmail;
                labelShipName.Text = model.ShipName;
                labelShipTelPhone.Text = model.ShipTelPhone;
                labelShipZipCode.Text = model.ShipZipCode;
                regionLists.Region_iID = model.RegionId.HasValue?model.RegionId.Value:0;
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

        //获取库存
        protected int GetStock(object obj_sku, object obj_Id)
        {
            if (obj_sku != null)
            {
                if (!string.IsNullOrWhiteSpace(obj_sku.ToString()))
                {
                    return skuBll.GetStockBySKU(obj_sku.ToString());
                }

            }
            if (obj_Id != null)
            {
                if (!string.IsNullOrWhiteSpace(obj_Id.ToString()))
                {
                    long productId = Common.Globals.SafeLong(obj_Id.ToString(), 0);
                    return skuBll.GetStockById(productId);
                }
            }
            return 0;
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Order.OrderInfo orderModel = orderBll.GetModel(OrderId);
            int modeId = Common.Globals.SafeInt(this.ddlShipType.SelectedValue, 0);
            Maticsoft.Model.Shop.Shipping.ShippingType typeModel = typeBll.GetModelByCache(modeId);
            orderModel.ExpressCompanyName = typeModel.ExpressCompanyName;
            orderModel.ExpressCompanyAbb = typeModel.ExpressCompanyEn;
            orderModel.ShippingModeId = typeModel.ModeId;
            orderModel.ShippingModeName = typeModel.Name;
            orderModel.RealShippingModeName = typeModel.Name;
            orderModel.ShipOrderNumber = this.txtShipOrderNumber.Text;
            orderModel.FreightAdjusted = Common.Globals.SafeDecimal(txtFreightAdjusted.Text, 0);
            orderModel.FreightActual = Common.Globals.SafeDecimal(txtFreightActual.Text, 0);

            int regionId = RegionList.Region_iID;
            orderModel.RegionId = regionId;
            orderModel.ShipRegion = regionBll.GetRegionNameByRID(regionId);

            orderModel.ShipName = txtShipName.Text;
            orderModel.ShipAddress = txtShipAddress.Text;
            orderModel.ShipTelPhone = txtShipTelPhone.Text;
            orderModel.ShipCellPhone = txtShipCellPhone.Text;
            //已发货
            orderModel.ShippingStatus = (int) Maticsoft.Model.Shop.Order.EnumHelper.ShippingStatus.Shipped;
            orderModel.OrderStatus = (int) Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.Handling;



            if (orderBll.UpdateShipped(orderModel))
            {
                //添加订单日志
                Maticsoft.Model.Shop.Order.OrderAction actionModel = new Maticsoft.Model.Shop.Order.OrderAction();
                int actionCode = (int) Maticsoft.Model.Shop.Order.EnumHelper.ActionCode.SystemShipped;
                actionModel.ActionCode = actionCode.ToString();
                actionModel.ActionDate = DateTime.Now;
                actionModel.OrderCode = orderModel.OrderCode;
                actionModel.OrderId = orderModel.OrderId;
                actionModel.Remark = "发货操作";
                actionModel.UserId = CurrentUser.UserID;
                actionModel.Username = CurrentUser.NickName;
                actionBll.Add(actionModel);

                //清除缓存
                orderBll.RemoveModelInfoCache(orderModel.OrderId);

                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                Express bll = new Express();
                //检查快递单号是否存在
                DataSet ds = bll.GetList(" ExpressCode='" + txtShipOrderNumber.Text.Trim() + "'", "");
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    StringBuilder v = new StringBuilder("/order.aspx");
                    v.Append("?etype=" + typeModel.ExpressCompanyEn);
                    v.Append("&ename=" + typeModel.ExpressCompanyName);
                    v.Append("&number=" + this.txtShipOrderNumber.Text.Trim());
                    v.Append("&ordercode=" + orderModel.OrderCode);


                    string curl = HttpContext.Current.Request.Url.ToString();
                    int ft = curl.IndexOf('/');
                    int lt = curl.Substring(curl.IndexOf('/') + 2).IndexOf('/');
                    string url = curl.Substring(0, ft + 2 + lt) + v.ToString();

                    Maticsoft.Model.Shop.Shipping.SubscriptionResult model = new Model.Shop.Shipping.SubscriptionResult();
                    using (System.Net.WebClient webClient = new System.Net.WebClient())
                    {
                        var st = webClient.OpenRead(url.ToString());
                        StreamReader sr0 = new StreamReader(st);
                        var result = sr0.ReadToEnd();
                        model = Maticsoft.Model.Shop.Shipping.comm.JsonToObject<Maticsoft.Model.Shop.Shipping.SubscriptionResult>(result);
                    }

                    ViewModel.Shop.ComType com = Maticsoft.Web.Components.ExpressHelper.GetAllComType().Single(l => l.ComName == this.ddlShipType.SelectedItem.Text.Trim());


                    Shop_Express expressmodel = new Shop_Express();
                    expressmodel.ExpressCode = txtShipOrderNumber.Text.Trim();
                    expressmodel.EType = com.ComEn;
                    expressmodel.EName = this.ddlShipType.SelectedItem.Text;
                    expressmodel.OrderCode = lblOrderCode.Text;
                    expressmodel.State = model.result == "true" ? "1" : "0";
                    expressmodel.ResultV2 = "0";
                    expressmodel.UpdateTime = DateTime.Now;
                    expressmodel.AddTime = DateTime.Now;
                    expressmodel.UseSign = "0";
                    bll.Add(expressmodel);

                    if (model.result == "true")
                    {
                        MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                    }
                    else
                    {
                        MessageBox.ShowSuccessTip(this, "快递订阅失败！");
                    }
                }
                else {
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
                ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
        }

        protected void ShipType_Changed(object sender, EventArgs e)
        {
            int modeId = Common.Globals.SafeInt(this.ddlShipType.SelectedValue, 0);
            Maticsoft.Model.Shop.Shipping.ShippingType typeModel = typeBll.GetModelByCache(modeId);
            if (typeModel != null)
            {
                txtFreightActual.Text = typeBll.GetFreight(typeModel, Common.Globals.SafeInt(hfWeight.Value, 0)).ToString("F");
                txtFreightAdjusted.Text = txtFreightActual.Text;
            }
            else
            {
                txtFreightActual.Text = "0.00";
                txtFreightAdjusted.Text = "0.00";
            }
        }
    }
}