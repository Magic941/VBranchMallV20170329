using System;
using System.Collections.Generic;
using System.Text;
using Maticsoft.Model.Shop.Sales;

namespace Maticsoft.Web.Admin.Shop.ExpressTemplate.Flex
{
    public partial class GetPrintData : PageBaseAdmin
    {
        public Maticsoft.BLL.Shop.Order.Orders orderBll = new BLL.Shop.Order.Orders();
        public Maticsoft.BLL.Shop.Sales.ShipperInfo shipperBll = new BLL.Shop.Sales.ShipperInfo();
        public Maticsoft.BLL.Ms.Regions regionBll = new BLL.Ms.Regions();

        protected void Page_Load(object sender, EventArgs e)
        {
            string str = base.Request.Form["shipperId"];
            string str2 = base.Request.Form["orderId"];
            if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
            {
                int result = 0;
                if (int.TryParse(str, out result))
                {
                    ShipperInfo shipper = shipperBll.GetModel(result);
                    Model.Shop.Order.OrderInfo orderInfo = orderBll.GetModelInfo(Common.Globals.SafeLong(str2, 0));
                    if ((shipper != null) && (orderInfo != null))
                    {
                        this.WriteOrderInfo(orderInfo, shipper);
                    }
                }
            }
        }

        private void WriteOrderInfo(Model.Shop.Order.OrderInfo order, ShipperInfo shipper)
        {

            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<nodes>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>收货人-姓名</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.ShipName);
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>收货人-电话</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.ShipTelPhone + "_");
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>收货人-手机</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.ShipCellPhone + "_");
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>收货人-邮编</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.ShipZipCode + "_");
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>收货人-地址</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.ShipAddress);
            builder.AppendLine("</item>");
            if (order.RegionId.HasValue)
            {
                List<string> listRegion = regionBll.GetNameListById4Cache(order.RegionId.Value);
                if (listRegion != null)
                {
                    if (listRegion.Count > 0)
                    {
                        builder.AppendLine("<item>");
                        builder.AppendLine("<name>收货人-地区1级</name>");
                        builder.AppendFormat("<rename>{0}</rename>", listRegion[0]);
                        builder.AppendLine("</item>");
                    }
                    if (listRegion.Count > 1)
                    {
                        builder.AppendLine("<item>");
                        builder.AppendLine("<name>收货人-地区2级</name>");
                        builder.AppendFormat("<rename>{0}</rename>", listRegion[1]);
                        builder.AppendLine("</item>");
                    }
                    if (listRegion.Count > 2)
                    {
                        builder.AppendLine("<item>");
                        builder.AppendLine("<name>收货人-地区3级</name>");
                        builder.AppendFormat("<rename>{0}</rename>", listRegion[2]);
                        builder.AppendLine("</item>");
                    }
                }
            }
            if (shipper != null)
            {
                builder.AppendLine("<item>");
                builder.AppendLine("<name>发货人-姓名</name>");
                builder.AppendFormat("<rename>{0}</rename>", shipper.ShipperName);
                builder.AppendLine("</item>");
                builder.AppendLine("<item>");
                builder.AppendLine("<name>发货人-手机</name>");
                builder.AppendFormat("<rename>{0}</rename>", shipper.CellPhone + "_");
                builder.AppendLine("</item>");
                builder.AppendLine("<item>");
                builder.AppendLine("<name>发货人-电话</name>");
                builder.AppendFormat("<rename>{0}</rename>", shipper.TelPhone + "_");
                builder.AppendLine("</item>");
                builder.AppendLine("<item>");
                builder.AppendLine("<name>发货人-地址</name>");
                builder.AppendFormat("<rename>{0}</rename>", shipper.Address);
                builder.AppendLine("</item>");
                builder.AppendLine("<item>");
                builder.AppendLine("<name>发货人-邮编</name>");
                builder.AppendFormat("<rename>{0}</rename>", shipper.Zipcode + "_");
                builder.AppendLine("</item>");

                List<string> listRegion = regionBll.GetNameListById4Cache(shipper.RegionId);
                if (listRegion != null)
                {
                    if (listRegion.Count > 0)
                    {
                        builder.AppendLine("<item>");
                        builder.AppendLine("<name>发货人-地区1级</name>");
                        builder.AppendFormat("<rename>{0}</rename>", listRegion[0]);
                        builder.AppendLine("</item>");
                    }
                    if (listRegion.Count > 1)
                    {
                        builder.AppendLine("<item>");
                        builder.AppendLine("<name>发货人-地区2级</name>");
                        builder.AppendFormat("<rename>{0}</rename>", listRegion[1]);
                        builder.AppendLine("</item>");
                    }
                    if (listRegion.Count > 2)
                    {
                        builder.AppendLine("<item>");
                        builder.AppendLine("<name>发货人-地区3级</name>");
                        builder.AppendFormat("<rename>{0}</rename>", listRegion[2]);
                        builder.AppendLine("</item>");
                    }
                }
            }
            builder.AppendLine("<item>");
            builder.AppendLine("<name>订单-订单号</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.OrderId);
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>订单-总金额</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.Amount.ToString("F") + "_");
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>订单-物品总重量</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.Weight);
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>订单-备注</name>");
            builder.AppendFormat("<rename>{0}</rename>", order.Remark);
            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>订单-送货时间</name>");
            builder.AppendFormat("<rename></rename>", new object[0]);
            builder.AppendLine("</item>");
            if (order.OrderItems.Count > 0)
            {
                string str = string.Empty;
                foreach (Model.Shop.Order.OrderItems info in order.OrderItems)
                {
                    //TODO: 商品信息简要输出 BEN ADD 20131119
                    str = string.Concat(new object[] { str, "货号 ", info.SKU, " ", info.Attribute, " \x00d7", info.ShipmentQuantity, "\n" });
                }
                str = str.Replace("；", "");
                builder.AppendLine("<item>");
                builder.AppendLine("<name>订单-详情</name>");
                builder.AppendFormat("<rename>{0}</rename>", str);
                builder.AppendLine("</item>");
            }
            builder.AppendLine("<item>");
            builder.AppendLine("<name>网店名称</name>");

            BLL.SysManage.WebSiteSet webSite = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.System);
            builder.AppendFormat("<rename>{0}</rename>", webSite.WebName); //TODO: 供应商名称 BEN ADD 20131119

            builder.AppendLine("</item>");
            builder.AppendLine("<item>");
            builder.AppendLine("<name>自定义内容</name>");
            builder.AppendFormat("<rename>{0}</rename>", "null");
            builder.AppendLine("</item>");
            builder.AppendLine("</nodes>");
            base.Response.Write(builder.ToString());
        }
    }
}

