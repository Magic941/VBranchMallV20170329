using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;

namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrderReturnDetail : PageBaseAdmin
    {
        public Maticsoft.BLL.Shop.Account.User userBLL = new BLL.Shop.Account.User();
        public Maticsoft.BLL.Shop.Order.OrderReturnGoods returnGoodsBLL = new BLL.Shop.Order.OrderReturnGoods();
        public Maticsoft.BLL.Shop.Order.OrderReturnGoodsItem returnGoodsItemBLL = new BLL.Shop.Order.OrderReturnGoodsItem();
        public Maticsoft.BLL.Shop.Order.OrderItems orderItemsBLL = new BLL.Shop.Order.OrderItems();
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
                //lbReturnStauts.Text= "";
                //lbBuyerName.Text = "";
                int LogisticStatus = int.Parse(returnGoods.LogisticStatus.ToString());
                int RefundStatus = int.Parse(returnGoods.RefundStatus.ToString());
                int Status = int.Parse(returnGoods.Status.ToString());
                if (RefundStatus < 2 && Status == 0 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 2)
                {
                    lbReturnStauts.Text = "退款等待审核";
                }
                else if (RefundStatus < 2 && Status == 0 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 1)
                {
                    lbReturnStauts.Text = "退货等待审核";
                }
                else if (RefundStatus < 2 && Status == 1 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 2)
                {
                    lbReturnStauts.Text = "等待退款";
                }
                else if ( Status==1 && LogisticStatus == 0 && RefundStatus < 3 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 1)
                {
                    lbReturnStauts.Text = "买家未发货";
                }
                else if (Status==1 && LogisticStatus == 1 && RefundStatus < 3 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 1)
                {
                    lbReturnStauts.Text = "买家已发货";
                }
                else if (Status == 1 &&  LogisticStatus == 2 && RefundStatus < 3 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 1)
                {
                    lbReturnStauts.Text = "卖家已收货";
                }
                else if ((Status == 2 && LogisticStatus == 2 && RefundStatus == 3) || (LogisticStatus == 0 && RefundStatus == 3 && Status == 2 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 2))
                {
                    lbReturnStauts.Text = "卖家已退款";
                }
                else if ((int.Parse(returnGoods.ReturnGoodsType.ToString()) == 1 && RefundStatus == 4) || (Status == -3 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 2))
                {
                    lbReturnStauts.Text = "卖家拒绝退款";
                }
                else if ((Status == -3 && int.Parse(returnGoods.ReturnGoodsType.ToString()) == 1)|| LogisticStatus==4 && Status==2)
                {
                    lbReturnStauts.Text = "卖家拒绝退货";
                }
                else
                {
                    lbReturnStauts.Text = "处理中";
                }
                int buyid = int.Parse(returnGoodsItem.UserID.ToString());
                Maticsoft.Model.Members.Users user = userBLL.GetUsersInfo(buyid);
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

                if (returnGoods.Information == null || returnGoods.Information =="")
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
    }
}