using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.Shop;
using System.Text.RegularExpressions;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class PayWeChatController : MShopControllerBase
    {
        BLL.Shop.Order.Orders orderManage = new BLL.Shop.Order.Orders();
        BLL.Shop.Order.OrderItems orderItemManage = new BLL.Shop.Order.OrderItems();

        #region Pay
        public ActionResult Pay(string viewName = "Pay")
        {
            string orderIdStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID] as string;
            string statusStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS] as string;

            if (string.IsNullOrWhiteSpace(orderIdStr) ||
                string.IsNullOrWhiteSpace(statusStr))
            {
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Web.Areas.MShop.Controllers.PayWeChatController__Pay__参数异常,跳转至首页,orderIdStr:" + orderIdStr + ",statusStr:" + statusStr);
                return Redirect(ViewBag.BasePath);
            }

            long orderId = Common.Globals.SafeLong(GetNumberInt(orderIdStr), -1);

            if (statusStr.ToLower() != "success")
                return Content("ERROR_NOSUCCESS");

            if (orderId < 1)
                return Content("ERROR_NOTSAFEORDERID");

            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID);
            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS);

            Model.Shop.Order.OrderInfo orderInfo = orderManage.GetModel(orderId);
            ViewBag.OrderId = orderInfo.OrderId;
            //订单编号
            ViewBag.OrderCode = orderInfo.OrderCode;
            //收货人
            ViewBag.ShipName = orderInfo.ShipName;
            //项目数量
            ViewBag.ItemsCount = orderItemManage.GetOrderItemCountByOrderId(orderId);
            //应付金额
            ViewBag.OrderAmount = orderInfo.Amount;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "微信支付";
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }
        #endregion

        #region Fail
        public ActionResult Fail(string viewName = "Fail")
        {
            string orderIdStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID] as string;
            string statusStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS] as string;

            if (string.IsNullOrWhiteSpace(statusStr))
            {
                Maticsoft.Common.ErrorLogTxt.GetInstance("微信支付异常日志").Write("Maticsoft.Web.Areas.MShop.Controllers.PayWeChatController__Fail__参数异常,跳转至首页,statusStr:" + statusStr);
                return Redirect(ViewBag.BasePath);
            }

            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID);
            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS);

            if (!string.IsNullOrWhiteSpace(orderIdStr))
            {
                long orderId = Common.Globals.SafeLong(GetNumberInt(orderIdStr), -1);
                if (orderId < 1)
                    return Content("ERROR_NOTSAFEORDERID");

                Model.Shop.Order.OrderInfo orderInfo = orderManage.GetModel(orderId);
                Web.LogHelp.AddErrorLog("Mobile >> PaymentFail >> OrderId[" + orderId + "] Status[" + statusStr + "]",
                    statusStr, "Mobile >> PaymentReturnHandler >> Redirect >> PayController");

                ViewBag.OrderId = orderInfo.OrderId;
            }
            ViewBag.PayStatus = statusStr;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "支付失败";
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        } 
        #endregion
        #region 获取字符串中的数字
        /// <summary>
        /// 获取字符串中的数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public long GetNumberInt(string str)
        {
            long result = 0;
            if (str != null && str != string.Empty)
            {
                // 正则表达式剔除非数字字符（不包含小数点.） 
                str = Regex.Replace(str, "\\D+", "");

                // 如果是数字，则转换为decimal类型 
                if (Regex.IsMatch(str, @"^\d*$"))
                {
                    result = long.Parse(str);
                }
            }
            return result;
        }
        #endregion
    }
}

