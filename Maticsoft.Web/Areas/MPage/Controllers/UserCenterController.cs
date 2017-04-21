using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Shop.Order;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Json;
using Maticsoft.Model.Shop;
using Maticsoft.Model.Shop.Order;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class UserCenterController : MPageControllerBaseUser
    {
        //
        // GET: /Mobile/UserCenter/
        private BLL.Members.PointsDetail detailBll = new BLL.Members.PointsDetail();
        private BLL.Members.SiteMessage bllSM = new BLL.Members.SiteMessage();
        private BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
        public ActionResult Index()
        {
            Maticsoft.Model.Members.UsersExpModel usersModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (usersModel != null)
            {
               // Maticsoft.BLL.Members.SiteMessage msgBll = new BLL.Members.SiteMessage();
               // ViewBag.privatecount = msgBll.GetReceiveMsgNotReadCount(currentUser.UserID, -1);//未读私信的条数
               // Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
               // ViewBag.Unpaid = orderBll.GetPaymentStatusCounts(CurrentUser.UserID, (int)EnumHelper.PaymentStatus.Unpaid);//未支付订单数
                #region SEO 优化设置
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title = "个人中心";//+ pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion
                return View(usersModel);
            }
            return Redirect(ViewBag.BasePath+"l/a");
        }
        #region 用户个人资料

        public ActionResult Personal()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "个人资料";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (null != model)
            {
                return View(model);
            }
            return Redirect(ViewBag.BasePath+"a/l");
           // return RedirectToAction("Login", "Account", new { id = 1, viewname = "url" });//去登录
        }
        #endregion 用户个人资料

        #region 用户密码

        public ActionResult ChangePassword()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "修改密码"; //+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        #endregion 用户密码

        #region 检查用户原密码

        /// <summary>
        ///检查用户原密码
        /// </summary>
        [HttpPost]
        public void CheckPassword(FormCollection collection)
        {
                JsonObject json = new JsonObject();
                string password = collection["Password"];
                if (!string.IsNullOrWhiteSpace(password))
                {
                    SiteIdentity SID = new SiteIdentity(CurrentUser.UserName);
                    if (SID.TestPassword(password.Trim()) == 0)
                    {
                        json.Accumulate("STATUS", "ERROR");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "OK");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "UNDEFINED");
                }
                Response.Write(json.ToString());
        }

        #endregion 检查用户原密码

        #region 更新用户密码

        /// <summary>
        /// 更新用户密码
        /// </summary>
        [HttpPost]
        public void UpdateUserPassword(FormCollection collection)
        {
                JsonObject json = new JsonObject();
                string newpassword = collection["NewPassword"];
                string confirmpassword = collection["ConfirmPassword"];
                if (!string.IsNullOrWhiteSpace(newpassword) && !string.IsNullOrWhiteSpace(confirmpassword))
                {
                    if (newpassword.Trim() != confirmpassword.Trim())
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                    else
                    {
                        currentUser.Password = AccountsPrincipal.EncryptPassword(newpassword);
                        if (currentUser.Update())
                        {
                            json.Accumulate("STATUS", "UPDATESUCC");
                        }
                        else
                        {
                            json.Accumulate("STATUS", "UPDATEFAIL");
                        }
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "UNDEFINED");
                }
                Response.Write(json.ToString());
        }

        #endregion 更新用户密码

        #region 更新用户信息

        /// <summary>
        /// 更新用户信息
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public void UpdateUserInfo(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
            model.TelPhone = collection["TelPhone"];
            string birthday = collection["Birthday"];
            if (!string.IsNullOrWhiteSpace(birthday) && PageValidate.IsDateTime(birthday))
            {
                model.Birthday = Globals.SafeDateTime(birthday, DateTime.Now);
            }
            else
            {
                model.Birthday = null;
            }
            model.Constellation = collection["Constellation"]; //星座
            model.PersonalStatus = collection["PersonalStatus"]; //职业
            model.Singature = collection["Singature"];
            model.Address = collection["Address"];
            User currentUser = new Maticsoft.Accounts.Bus.User(CurrentUser.UserID);
            currentUser.Sex = collection["Sex"];
            currentUser.Email = collection["Email"];
            currentUser.NickName = collection["NickName"];
            currentUser.Phone = collection["Phone"];
            if (currentUser.Update() && userEXBll.UpdateUsersExp(model))
            {
                json.Accumulate("STATUS", "SUCC");
            }
            else
            {
                json.Accumulate("STATUS", "FAIL");
            }
            Response.Write(json.ToString());
    }

        #endregion 更新用户信息

        #region 检查用户输入的昵称是否被其他用户使用

        /// <summary>
        ///检查用户输入的昵称是否被其他用户使用
        /// </summary>
        [HttpPost]
        public void CheckNickName(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            if (HttpContext.User.Identity.IsAuthenticated && CurrentUser != null && CurrentUser.UserType != "AA")
            {
                string nickname = collection["NickName"];
                if (!string.IsNullOrWhiteSpace(nickname))
                {
                    BLL.Members.Users bll = new BLL.Members.Users();
                    if (bll.ExistsNickName(CurrentUser.UserID, nickname))
                    {
                        json.Accumulate("STATUS", "EXISTS");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "OK");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "NOTNULL");
                }
                Response.Write(json.ToString());
            }
            else
            {
                json.Accumulate("STATUS", "NOTNULL");
                Response.Write(json.ToString());
            }
        }

        #endregion 检查用户输入的昵称是否被其他用户使用
        

        #region 发站内信

        /// <summary>
        /// 发站内信
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMessage()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "发信息";//+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            ViewBag.Name = Request.Params["name"];
            return View("SendMessage");
        }

        #endregion 发站内信

        #region 发送站内信息

        /// <summary>
        /// 发送站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void SendMsg(FormCollection collection)
        {

            JsonObject json = new JsonObject();
            string nickname = Common.InjectionFilter.Filter(collection["NickName"]);
            string title = Common.InjectionFilter.Filter(collection["Content"]);
            string content = Common.InjectionFilter.Filter(collection["Content"]);
            if (string.IsNullOrWhiteSpace(nickname))
            {
                json.Accumulate("STATUS", "NICKNAMENULL");
            }
            else if (string.IsNullOrWhiteSpace(title))
            {
                json.Accumulate("STATUS", "TITLENULL");
            }
            else if (string.IsNullOrWhiteSpace(content))
            {
                json.Accumulate("STATUS", "CONTENTNULL");
            }
            else
            {
                BLL.Members.Users bll = new BLL.Members.Users();
                if (bll.ExistsNickName(nickname))
                {
                    int ReceiverID = bll.GetUserIdByNickName(nickname);
                    Maticsoft.Model.Members.SiteMessage modeSiteMessage = new Maticsoft.Model.Members.SiteMessage();
                    modeSiteMessage.Title = title;
                    modeSiteMessage.Content = content;
                    modeSiteMessage.SenderID = CurrentUser.UserID;
                    modeSiteMessage.ReaderIsDel = false;
                    modeSiteMessage.ReceiverIsRead = false;
                    modeSiteMessage.SenderIsDel = false;
                    modeSiteMessage.ReceiverID = ReceiverID;
                    modeSiteMessage.SendTime = DateTime.Now;
                    if (bllSM.Add(modeSiteMessage) > 0)
                    {
                        json.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "NICKNAMENOTEXISTS");
                }
            }
            Response.Write(json.ToString());
        }

        #endregion 发送站内信息

        #region 回复站内信息

        /// <summary>
        /// 回复站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void ReplyMsg(int ReceiverID, string Title, string Content)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.SiteMessage modeSiteMessage = new Maticsoft.Model.Members.SiteMessage();
            modeSiteMessage.Title = Title;
            modeSiteMessage.Content = Content;
            modeSiteMessage.SenderID = CurrentUser.UserID;
            modeSiteMessage.ReaderIsDel = false;
            modeSiteMessage.ReceiverIsRead = false;
            modeSiteMessage.SenderIsDel = false;
            modeSiteMessage.ReceiverID = ReceiverID;
            modeSiteMessage.SendTime = DateTime.Now;
            if (bllSM.Add(modeSiteMessage) > 0)
                json.Accumulate("STATUS", "SUCC");
            else
                json.Accumulate("STATUS", "FAIL");
            Response.Write(json.ToString());
        }

        #endregion 回复站内信息

        #region 删除站内信息

        /// <summary>
        /// 删除收到的站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void DelReceiveMsg(int MsgID)
        {
            JsonObject json = new JsonObject();
            if (bllSM.SetReceiveMsgToDelById(MsgID) > 0)
                json.Accumulate("STATUS", "SUCC");
            else
                json.Accumulate("STATUS", "FAIL");
            Response.Write(json.ToString());
        }

        #endregion 删除站内信息

        #region 读取站内信息
        /// <summary>
        /// 读取站内信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ReadMsg(int? MsgID)
        {
            if (MsgID.HasValue)
            {
                Model.Members.SiteMessage siteModel = bllSM.GetModelByCache(MsgID.Value);
                if (siteModel != null)
                {
                    if (siteModel.SenderID == -1)
                        siteModel.SenderUserName = "管理员";//senderid为-1 的消息是管理员所发
                    else
                    {
                        Model.Members.UsersExpModel userexpmodel = null;
                        if (siteModel.SenderID.HasValue)
                            userexpmodel = userEXBll.GetUsersExpModelByCache(siteModel.SenderID.Value);//得到发送者的昵称
                        if (userexpmodel != null)
                            siteModel.SenderUserName = userexpmodel.NickName;
                    }
                  


                    if (siteModel.ReceiverIsRead == false)
                        bllSM.SetReceiveMsgAlreadyRead(siteModel.ID);//如果是消息状态是未读的，则改变消息状态
                    return View(siteModel);
                }
            }
            return RedirectToAction("Inbox","UserCenter");
        }
        #endregion 读取站内信息

        #region 收件箱

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Inbox()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "收件箱";//+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public PartialViewResult InboxList(int? page, string viewName = "_InboxList")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "发件箱";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            ViewBag.inboxpage = page;
            int pagesize = 7;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllReceiveMsgListByMvcPage(CurrentUser.UserID, pagesize, page.Value);
            //foreach (Maticsoft.Model.Members.SiteMessage item in list)
            //{
            //    if (item.ReceiverIsRead == false)
            //    {
            //        bllSM.SetReceiveMsgAlreadyRead(item.ID);
            //    }
            //}
            if (Request.IsAjaxRequest())
                return PartialView(viewName, list);
            return PartialView(viewName, list);
        }

        #endregion 收件箱

        #region 发件箱

        /// <summary>
        /// 发件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Outbox(int? page)
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "发件箱" + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            int pagesize = 8;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSendMsgListByMvcPage(CurrentUser.UserID, pagesize, page.Value);
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserCenter/_OutboxList.cshtml", list);
            return View(CurrentThemeViewPath + "/UserCenter/OutBox.cshtml", list);
        }

        #endregion 发件箱

     
        #region 订单列表
        public ActionResult Orders(string viewName = "Orders")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的订单-订单明细";// + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }
        public PartialViewResult OrderList(int pageIndex = 1, string viewName = "_OrderList")
        {
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
            Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();

            int _pageSize = 8;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            string where = " BuyerID=" + CurrentUser.UserID +
#if true //方案二 统一提取主订单, 然后加载子订单信息 在View中根据订单支付状态和是否有子单对应展示
                //主订单
                           " AND OrderType=1";
#else   //方案一 提取数据时 过滤主/子单数据 View中无需对应 [由于不够灵活此方案作废]
                           //主订单 无子订单
                           " AND ((OrderType = 1 AND HasChildren = 0) " +
                           //子订单 已支付 或 货到付款/银行转账 子订单
                           "OR (OrderType = 2 AND (PaymentStatus > 1 OR (PaymentGateway='cod' OR PaymentGateway='bank')) ) " +
                           //主订单 有子订单 未支付的主订单 非 货到付款/银行转账 子订单
                           "OR (OrderType = 1 AND HasChildren = 1 AND PaymentStatus < 2 AND PaymentGateway<>'cod' AND PaymentGateway<>'bank'))";
#endif

            //获取总条数
            toalCount = orderBll.GetRecordCount(where);
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<OrderInfo> orderList = orderBll.GetListByPageEX(where, "", startIndex, endIndex);
            if (orderList != null && orderList.Count > 0)
            {
                foreach (OrderInfo item in orderList)
                {
                    //有子订单 已支付 或 货到付款/银行转账 子订单 - 加载子单
                    if (item.HasChildren && (item.PaymentStatus > 1 || (item.PaymentGateway == "cod" || item.PaymentGateway == "bank")))
                    {
                        item.SubOrders = orderBll.GetModelList(" ParentOrderId=" + item.OrderId);
                        item.SubOrders.ForEach(
                            info => info.OrderItems = itemBll.GetModelList(" OrderId=" + info.OrderId));
                    }
                    else
                    {
                        item.OrderItems = itemBll.GetModelList(" OrderId=" + item.OrderId);
                    }
                }
            }
            PagedList<Maticsoft.Model.Shop.Order.OrderInfo> lists = new PagedList<Maticsoft.Model.Shop.Order.OrderInfo>(orderList, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }

        #region 辅助方法

        public static string GetOrderType(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            string str = "";
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
            EnumHelper.OrderMainStatus orderType = orderBll.GetOrderType(paymentGateway,
                                    orderStatus,
                                    paymentStatus,
                                    shippingStatus);
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
            return str;
        }

        #endregion
        #region Ajax方法
        [HttpPost]
        public ActionResult CancelOrder(FormCollection Fm)
        {
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Orders();
            long orderId = Common.Globals.SafeLong(Fm["OrderId"], 0);
            Maticsoft.Model.Shop.Order.OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
            if (orderInfo == null || orderInfo.BuyerID != currentUser.UserID)
                return Content("False");

            if (Maticsoft.BLL.Shop.Order.OrderManage.CancelOrder(orderInfo, currentUser))
                return Content("True");
            return Content("False");
        }
        #endregion
        #endregion

        #region 收藏列表
        public ActionResult MyFavor(string viewName = "MyFavor")
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "我的收藏";//+ pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(viewName);
        }

        public PartialViewResult FavorList(int pageIndex = 1, string viewName = "_FavorList")
        {
            Maticsoft.BLL.Shop.Favorite favoBll = new BLL.Shop.Favorite();
            StringBuilder strBuilder = new StringBuilder();
            // strBuilder.AppendFormat(" UserId ={0}  and  SaleStatus in ( {1},{2} ) ", CurrentUser.UserID, (int )ProductSaleStatus.InStock, (int )ProductSaleStatus.OnSale);
            strBuilder.AppendFormat(" favo.UserId ={0} and favo.Type= {1} ", CurrentUser.UserID, (int)FavoriteEnums.Product);

            int _pageSize = 4;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = favoBll.GetRecordCount(" UserId =" + CurrentUser.UserID + " and Type=" + (int)FavoriteEnums.Product);//获取总条数 
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Maticsoft.ViewModel.Shop.FavoProdModel> favoList = favoBll.GetFavoriteProductListByPage(strBuilder.ToString(), startIndex, endIndex);
            PagedList<Maticsoft.ViewModel.Shop.FavoProdModel> lists = new PagedList<Maticsoft.ViewModel.Shop.FavoProdModel>(favoList, pageIndex, _pageSize, toalCount);
            if (Request.IsAjaxRequest())
                return PartialView(viewName, lists);
            return PartialView(viewName, lists);
        }
        /// <summary>
        /// 移除收藏项
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult RemoveFavorItem(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ItemId"]))
            {
                string itemId = Fm["ItemId"];
                Maticsoft.BLL.Shop.Favorite favoBll = new BLL.Shop.Favorite();
                int favoriteId = Common.Globals.SafeInt(itemId, 0);
                if (favoBll.Delete(favoriteId))
                    return Content("Ok");
            }
            return Content("No");
        }
        #endregion

        #region Ajax方法
        /// <summary>
        /// 加入收藏
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddFav(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                int productId = Common.Globals.SafeInt(Fm["ProductId"], 0);
                Maticsoft.BLL.Shop.Favorite favBll = new BLL.Shop.Favorite();
                //是否已经收藏
                if (favBll.Exists(productId, currentUser.UserID, 1))
                {
                    return Content("Rep");
                }
                Maticsoft.Model.Shop.Favorite favMode = new Maticsoft.Model.Shop.Favorite();
                favMode.CreatedDate = DateTime.Now;
                favMode.TargetId = productId;
                favMode.Type = 1;
                favMode.UserId = currentUser.UserID;
                return favBll.Add(favMode) > 0 ? Content("True") : Content("False");
            }
            return Content("False");
        }
        /// <summary>
        /// 添加咨询
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddConsult(FormCollection Fm)
        {
            if (!String.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                int productId = Common.Globals.SafeInt(Fm["ProductId"], 0);
                string content = Common.InjectionFilter.SqlFilter(Fm["Content"]);
                Maticsoft.BLL.Shop.Products.ProductConsults consultBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
                Maticsoft.Model.Shop.Products.ProductConsults consultMode = new Maticsoft.Model.Shop.Products.ProductConsults();
                consultMode.CreatedDate = DateTime.Now;
                consultMode.TypeId = 0;
                consultMode.Status = 0;
                consultMode.UserId = currentUser.UserID;
                consultMode.UserName = currentUser.NickName;
                consultMode.UserEmail = currentUser.Email;
                consultMode.IsReply = false;
                consultMode.Recomend = 0;
                consultMode.ProductId = productId;
                consultMode.ConsultationText = content;
                return consultBll.Add(consultMode) > 0 ? Content("True") : Content("False");
            }
            return Content("False");
        }
        #endregion
    }
}
