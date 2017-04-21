using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.Payment.Handler;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class PayResultController : ShopControllerBase
    {
        BLL.Shop.Order.Orders orderManage = new BLL.Shop.Order.Orders();
        BLL.Shop.Order.OrderItems orderItemManage = new BLL.Shop.Order.OrderItems();
        private BLL.Pay.RechargeRequest rechargeBll = new BLL.Pay.RechargeRequest();

        #region Success
        public ActionResult Success(string viewName = "Success")
        {
            ErrorLogTxt.GetInstance("支付返回结果页").Write("支付成功!");
            string orderIdStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID] as string;
            string statusStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS] as string;

            if (string.IsNullOrWhiteSpace(orderIdStr) ||
                string.IsNullOrWhiteSpace(statusStr))
                return Redirect("/");

            long orderId = Common.Globals.SafeLong(orderIdStr, -1);

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

            //判断是否需要支付
            ViewBag.HasPay = (orderInfo.PaymentGateway != "cod" && orderInfo.PaymentGateway != "bank");

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = (ViewBag.HasPay ? "支付成功 " : "订单提交成功 ") + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(viewName);
        } 
        #endregion

        #region Fail
        public ActionResult Fail(string viewName = "Fail")
        {
            ErrorLogTxt.GetInstance("支付返回结果页").Write("支付失败!");
            string orderIdStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID] as string;
            string statusStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS] as string;

            if (string.IsNullOrWhiteSpace(statusStr))
                return Redirect("/");

            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID);
            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS);

            if (!string.IsNullOrWhiteSpace(orderIdStr))
            {
                long orderId = Common.Globals.SafeLong(orderIdStr, -1);
                if (orderId < 1)
                    return Content("ERROR_NOTSAFEORDERID");

                Model.Shop.Order.OrderInfo orderInfo = orderManage.GetModel(orderId);
                Web.LogHelp.AddErrorLog("Shop >> PaymentFail >> OrderId[" + orderId + "] Status[" + statusStr + "]",
                    statusStr, "Shop >> PaymentReturnHandler >> Redirect >> PayController");

                ViewBag.OrderId = orderInfo.OrderId;
            }

            ViewBag.PayStatus = statusStr;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "支付失败 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(viewName);
        } 
        #endregion

        #region MFail
        /// <summary>
        /// 支付方式错误 手机下单只能在手机支付，pc端下单只能在pc端支付
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult MFail(string viewName = "MFail")
        {
            string orderIdStr = Session[Handlers.Shop.Pay.SendPaymentHandler.KEY_ORDERID] as string;
            if (string.IsNullOrWhiteSpace(orderIdStr))
                return Redirect("/");
            Session.Remove(Handlers.Shop.Pay.SendPaymentHandler.KEY_ORDERID);

            if (!string.IsNullOrWhiteSpace(orderIdStr))
            {
                long orderId = Common.Globals.SafeLong(orderIdStr, -1);
                if (orderId < 1)
                    return Content("ERROR_NOTSAFEORDERID");

                Model.Shop.Order.OrderInfo orderInfo = orderManage.GetModel(orderId);
                if (orderInfo == null)
                {
                    return Redirect("/");
                }
                ViewBag.OrderId = orderInfo.OrderId;
                ViewBag.OrderCode = orderInfo.OrderCode;
            }

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "支付失败 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        } 
        #endregion

        #region RechargeSuccess
        public ActionResult RechargeSuccess(string viewName = "RechargeSuccess")
        {
            string rechargeIdStr = Session[Handlers.Shop.Pay.RechargeReturnHandler.KEY_RECHARGEID] as string;
            string statusStr = Session[Handlers.Shop.Pay.RechargeReturnHandler.KEY_STATUS] as string;

            if (string.IsNullOrWhiteSpace(rechargeIdStr) ||
                string.IsNullOrWhiteSpace(statusStr))
                return Redirect("/");

            long rechargeId = Common.Globals.SafeLong(rechargeIdStr, -1);

            if (statusStr.ToLower() != "success")
                return Content("ERROR_NOSUCCESS");

            if (rechargeId < 1)
                return Content("ERROR_NOTSAFEORDERID");

            Session.Remove(Handlers.Shop.Pay.RechargeReturnHandler.KEY_RECHARGEID);
            Session.Remove(Handlers.Shop.Pay.RechargeReturnHandler.KEY_STATUS);

            Model.Pay.RechargeRequest rechargeModel = rechargeBll.GetModel(rechargeId);
            //充值号
            ViewBag.RechargeId = rechargeModel.RechargeId;
            //充值金额
            ViewBag.RechargeBlance = rechargeModel.RechargeBlance;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "充值成功 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        } 
        #endregion

        #region RechargeFail
        public ActionResult RechargeFail(string viewName = "RechargeFail")
        {
            string rechargeIdStr = Session[Handlers.Shop.Pay.RechargeReturnHandler.KEY_RECHARGEID] as string;
            string statusStr = Session[Handlers.Shop.Pay.RechargeReturnHandler.KEY_STATUS] as string;


            if (string.IsNullOrWhiteSpace(statusStr))
                return Redirect("/");

            Session.Remove(Handlers.Shop.Pay.RechargeReturnHandler.KEY_RECHARGEID);
            Session.Remove(Handlers.Shop.Pay.RechargeReturnHandler.KEY_STATUS);

            if (!string.IsNullOrWhiteSpace(rechargeIdStr))
            {
                long rechargeId = Common.Globals.SafeLong(rechargeIdStr, -1);
                if (rechargeId < 1)
                    return Content("ERROR_NOTSAFEORDERID");
                LogHelp.AddErrorLog("Shop >> RechargeFail >> RechargeId[" + rechargeId + "] Status[" + statusStr + "]",
                    statusStr, "Shop >> RechargeReturnHandler >> Redirect >> PayController");

                ViewBag.RechargeId = rechargeId;
            }
            ViewBag.PayStatus = statusStr;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "充值失败 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        } 
        #endregion

        #region NoBalance
        public ActionResult NoBalance(string viewName = "NoBalance")
        {
            string orderIdStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID] as string;
            string statusStr = Session[Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS] as string;

            if (string.IsNullOrWhiteSpace(statusStr))
                return Redirect("/");

            if (statusStr.ToLower() != "nobalance")
                return Redirect("/");

            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_ORDERID);
            Session.Remove(Handlers.Shop.Pay.PaymentReturnHandler.KEY_STATUS);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "余额不足 " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            if (!string.IsNullOrWhiteSpace(orderIdStr))
            {
                long orderId = Common.Globals.SafeLong(orderIdStr, -1);
                if (orderId < 1)
                    return Content("ERROR_NOTSAFEORDERID");

                Model.Shop.Order.OrderInfo orderInfo = orderManage.GetModel(orderId);
                ViewBag.Amount = orderInfo.Amount;
                ViewBag.OrderId = orderInfo.OrderId;
                ViewBag.OrderCode = orderInfo.OrderCode;
            }
            else
            {
                ViewBag.Amount = decimal.Zero;
                ViewBag.OrderId = "N/A";
                ViewBag.OrderCode = "N/A";
            }
            if (CurrentUser != null)
            {
                BLL.Members.UsersExp userExpManage = new BLL.Members.UsersExp();
                ViewBag.Balance = userExpManage.GetUserBalance(CurrentUser.UserID);
            }
            else
            {
                ViewBag.Balance = decimal.Zero;
            }
            ViewBag.PayStatus = statusStr;
            return View(viewName);
        } 
        #endregion
    }
}
