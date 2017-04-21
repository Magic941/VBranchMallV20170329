using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace AppServices.Controllers
{
    public class UserCenterController : ApiController
    {
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";

        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";
        public const string SHOP_STATUS_ISNULL = "ISNULL";
        public const string TOTALCOUNT = "TOTALCOUNT";
        public const string SHOP_KEY_MESSAGE = "MESSAGE";

        private readonly Maticsoft.BLL.Members.PointsDetail detailBll = new Maticsoft.BLL.Members.PointsDetail();
        private readonly Maticsoft.BLL.Members.SiteMessage bllSM = new Maticsoft.BLL.Members.SiteMessage();
        private readonly Maticsoft.BLL.Members.UsersExp userEXBll = new Maticsoft.BLL.Members.UsersExp();
        private readonly Maticsoft.BLL.Members.Users userBll = new Maticsoft.BLL.Members.Users();
        private readonly Maticsoft.BLL.Members.UserBind userBind = new Maticsoft.BLL.Members.UserBind();
        private readonly Maticsoft.BLL.Shop.Order.Orders _orderManage = new Maticsoft.BLL.Shop.Order.Orders();
        private readonly Maticsoft.BLL.Pay.RechargeRequest rechargeBll = new Maticsoft.BLL.Pay.RechargeRequest();
        private readonly Maticsoft.BLL.Pay.BalanceDrawRequest balanDrawBll = new Maticsoft.BLL.Pay.BalanceDrawRequest();
        private readonly Maticsoft.BLL.Pay.BalanceDetails balanDetaBll = new Maticsoft.BLL.Pay.BalanceDetails();
        private readonly Maticsoft.BLL.Members.UserInvite inviteBll = new Maticsoft.BLL.Members.UserInvite();
        private readonly Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new Maticsoft.BLL.Shop.Coupon.CouponRule();
        private Maticsoft.BLL.Shop.Order.OrderAction actionBll = new Maticsoft.BLL.Shop.Order.OrderAction();
        private readonly Maticsoft.BLL.Shop.Gift.ExchangeDetail exchangeBll = new Maticsoft.BLL.Shop.Gift.ExchangeDetail();
        private readonly Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new Maticsoft.BLL.Shop.Coupon.CouponInfo();
        private readonly Maticsoft.BLL.Shop_CardUserInfo carduserInfoBll = new Maticsoft.BLL.Shop_CardUserInfo();
        private readonly Maticsoft.BLL.Shop_CardType _cardTypeBLL = new Maticsoft.BLL.Shop_CardType();
        private readonly Maticsoft.BLL.Shop.Order.OrderReturnGoods returnGoodsBLL = new Maticsoft.BLL.Shop.Order.OrderReturnGoods();
        private readonly Maticsoft.BLL.Shop.Order.OrderReturnGoodsItem returnGoodsItemBLL = new Maticsoft.BLL.Shop.Order.OrderReturnGoodsItem();
        private readonly Maticsoft.BLL.Shop.Order.OrderItems orderitemBll = new Maticsoft.BLL.Shop.Order.OrderItems();


        #region 用户个人资料

        public JsonObject Personal(int UserID)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.UsersExpModel info = userEXBll.GetUsersModel(UserID);
            if (info != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, info);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion 用户个人资料

        #region 更新用户信息

        /// <summary>
        /// 更新用户信息
        /// </summary>
        [HttpPost]
        public JsonObject UpdateUserInfo(Maticsoft.Model.Members.Users userinfo)
        {
            JsonObject json = new JsonObject();
            if (userinfo.UserID < 0 || userinfo==null)
            {
                json.Put(SHOP_KEY_STATUS,SHOP_STATUS_ERROR);
                return json;
            }
            else
            {
                #region 修改具体内容

                #endregion
                json.Put(SHOP_KEY_STATUS,SHOP_STATUS_SUCCESS);
                return json;
            }
        }

        #endregion 更新用户信息

        #region 修改用户头像

        /// <summary>
        /// 修改用户头像
        /// </summary>
        [HttpPost]
        public JsonObject Gravatar(string Gravatar, int UserID)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.UsersExpModel model = new Maticsoft.Model.Members.UsersExpModel();
            try
            {
                if (UserID > 0)
                {
                    model = userEXBll.GetUsersModel(UserID);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                    return json;
                }
                if (model == null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                    return json;
                }
                else
                {
                  
                    if (null != model)
                    {
                        model.Gravatar = Gravatar;
                        if (userEXBll.UpdateUsersExp(model))//更新扩展信息  ,后期将更新头像独立一个方法。
                        {
                            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                        }
                        else
                        {
                            json.Put(SHOP_KEY_STATUS,SHOP_STATUS_FAILED);
                        }
                    }
                    else
                    {
                        json.Put(SHOP_KEY_STATUS,SHOP_STATUS_ISNULL);
                    }
                }
            }
            catch
            {
                json.Put(SHOP_KEY_STATUS,SHOP_STATUS_ERROR);
            }
            return json;
        }
   
        #endregion 修改用户头像

        #region 检查用户原密码

        /// <summary>
        ///检查用户原密码
        /// </summary>
        [HttpPost]
        public JsonObject CheckPassword(string Password, string UserName)
        {
         
                JsonObject json = new JsonObject();
                string password = Password;
                if (!string.IsNullOrWhiteSpace(password))
                {
                    SiteIdentity SID = new SiteIdentity(UserName);
                    if (SID.TestPassword(password.Trim()) == 0)
                    {
                        var pwd = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(password, "md5").ToLower();
                        Maticsoft.BLL.Shop_CardUserInfo card = new Maticsoft.BLL.Shop_CardUserInfo();
                        var result = card.GetModelListByName(UserName).Any(p => p.Password == pwd);
                        if (result)
                        {
                            json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                        }
                        else
                        {
                            json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                        }
                    }
                    else
                    {
                        json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    }
                }
                else
                {
                    json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
                return json;
              
            
        }

        #endregion 检查用户原密码

        #region 更新用户密码

        /// <summary>
        /// 更新用户密码
        /// </summary>
        [HttpPost]
        public JsonObject UpdateUserPassword(string NewPassword, string ConfirmPassword, int UserName)
        {
          
                JsonObject json = new JsonObject();
                string newpassword = NewPassword;
                string confirmpassword =ConfirmPassword;
                if (!string.IsNullOrWhiteSpace(newpassword) && !string.IsNullOrWhiteSpace(confirmpassword))
                {
                    if (newpassword.Trim() != confirmpassword.Trim())
                    {
                        json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    }
                    else
                    {
                        //currentUser.Password = AccountsPrincipal.EncryptPassword(newpassword);
                        //if (currentUser.Update())
                        //{
                        //    json.Accumulate("STATUS", "UPDATESUCC");
                        //}
                        //else
                        //{
                        //    json.Accumulate("STATUS", "UPDATEFAIL");
                        //}
                    }
                }
                else
                {
                    json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
                return json;
              
            
        }

        #endregion 更新用户密码

        #region 检查用户输入的昵称是否被其他用户使用

        /// <summary>
        ///检查用户输入的昵称是否被其他用户使用
        /// </summary>
        [HttpPost]
        public JsonObject CheckNickName(string NickName, int UserID)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.Users info = userBll.GetModel(UserID);
            if (info != null && info.UserType != "AA")
            {
                string nickname = NickName;
                if (!string.IsNullOrWhiteSpace(nickname))
                {

                    if (userBll.ExistsNickName(info.UserID, nickname))
                    {
                        json.Accumulate(SHOP_KEY_STATUS, "EXISTS");
                    }
                    else
                    {
                        json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    }
                }
                else
                {
                    json.Accumulate(SHOP_KEY_STATUS,SHOP_STATUS_ISNULL);
                }
               
            }
            else
            {
                json.Accumulate(SHOP_KEY_STATUS,SHOP_STATUS_ISNULL);
                
            }
            return json;
        }

        #endregion 检查用户输入的昵称是否被其他用户使用

        #region 积分明细

        public JsonObject PointsDetail(int UserID, int pageIndex = 1, int pagesize = 15)
        {
            JsonObject json = new JsonObject();
            //首页用户数据
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
            if (userModel==null)
            {
                json.Put(SHOP_KEY_STATUS,SHOP_STATUS_ISNULL);
                return json;
            }
            int _pageSize = pagesize;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = detailBll.GetRecordCount(" UserID=" + userModel.UserID);
            if (toalCount < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;//NO DATA
            }
            List<Maticsoft.Model.Members.PointsDetail> detailList = detailBll.GetListByPageEX("UserID=" + userModel.UserID, " ", startIndex, endIndex);
            if (detailList != null && detailList.Count > 0)
            {
                foreach (var item in detailList)
                {
                    item.RuleName = GetRuleName(item.RuleId);
                }
            }

            if (detailList != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, detailList);
                json.Put(TOTALCOUNT, toalCount);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }

        public string GetRuleName(int RuleId)
        {
            if (RuleId == -1)
            {
                return "积分消费";
            }
            Maticsoft.BLL.Members.PointsRule ruleBll = new Maticsoft.BLL.Members.PointsRule();
            return ruleBll.GetRuleName(RuleId);
        }

        #endregion 积分明细

        #region 积分兑换明细

        public JsonObject Exchanges(int pageIndex = 1, int pagesize = 15, int UserID=0)
        {
            JsonObject json = new JsonObject();
         
            //首页用户数据
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
            if (userModel == null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
          
            int _pageSize = pagesize;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = exchangeBll.GetRecordCount(" UserID=" + userModel.UserID);
            if (toalCount < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            List<Maticsoft.Model.Shop.Gift.ExchangeDetail> detailList = exchangeBll.GetListByPageEX("UserID=" + userModel.UserID, " CreatedDate desc", startIndex, endIndex);

            if (detailList != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, detailList);
                json.Put(TOTALCOUNT, toalCount);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }


        public JsonObject CouponRule(int top = 4)
        {
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.Shop.Coupon.CouponRule> ruleList = ruleBll.GetModelList(" Type=1 and Status=1");
           
            if (ruleList != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, ruleList);
              
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
     
        #endregion 积分兑换明细

        #region 我的优惠券

        public JsonObject MyCoupon(int pageIndex = 1, int Status = 1, int UserID=0)
        {
            JsonObject json = new JsonObject();
          
            int status = Maticsoft.Common.Globals.SafeInt(Status, 1);

            int _pageSize = 10;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = infoBll.GetRecordCount(String.Format(" UserID={0} and Status={1}", UserID, status));

       
            if (toalCount < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoList = infoBll.GetListByPageEX(String.Format(" UserID={0} and Status={1}", UserID, status), " GenerateTime desc", startIndex, endIndex);
            Maticsoft.BLL.Shop.Coupon.CouponClass classBll = new Maticsoft.BLL.Shop.Coupon.CouponClass();
            foreach (var Info in infoList)
            {
                Maticsoft.Model.Shop.Coupon.CouponClass classModel = classBll.GetModelByCache(Info.ClassId);
                Info.ClassName = classModel == null ? "" : classModel.Name;
            }
            if (infoList != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, infoList);
                json.Put(TOTALCOUNT, toalCount);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }


        #endregion

        #region 检查用户输入的昵称是否存在

        /// <summary>
        ///检查用户输入的昵称是否存在
        /// </summary>
        [HttpPost]
        public JsonObject ExistsNickName(string NickName)
        {
            JsonObject json = new JsonObject();
                string nickname = NickName;
                if (!string.IsNullOrWhiteSpace(nickname))
                {

                    if (userBll.ExistsNickName(nickname))
                    {
                        json.Accumulate("STATUS", "EXISTS");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NOTEXISTS");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "NOTNULL");
                }
                return json;
             
        }

        #endregion 检查用户输入的昵称是否存在

        #region 发送站内信息

        /// <summary>
        /// 发送站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonObject SendMsg(string NickName, string Content, string Title, int? UserID)
        {

            JsonObject json = new JsonObject();
            string nickname = Maticsoft.Common.InjectionFilter.Filter(NickName);
            string title = Maticsoft.Common.InjectionFilter.Filter(Title);
            string content = Maticsoft.Common.InjectionFilter.Filter(Content);
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

                if (userBll.ExistsNickName(nickname))
                {
                    int ReceiverID = userBll.GetUserIdByNickName(nickname);
                    Maticsoft.Model.Members.SiteMessage modeSiteMessage = new Maticsoft.Model.Members.SiteMessage();
                    modeSiteMessage.Title = title;
                    modeSiteMessage.Content = content;
                    modeSiteMessage.SenderID = UserID;
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
            return json;
        }

        #endregion 发送站内信息

        #region 回复站内信息

        /// <summary>
        /// 回复站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonObject ReplyMsg(int ReceiverID, string Title, string Content, int UserID)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.SiteMessage modeSiteMessage = new Maticsoft.Model.Members.SiteMessage();
            modeSiteMessage.Title = Title;
            modeSiteMessage.Content = Content;
            modeSiteMessage.SenderID = UserID;
            modeSiteMessage.ReaderIsDel = false;
            modeSiteMessage.ReceiverIsRead = false;
            modeSiteMessage.SenderIsDel = false;
            modeSiteMessage.ReceiverID = ReceiverID;
            modeSiteMessage.SendTime = DateTime.Now;
            if (bllSM.Add(modeSiteMessage) > 0)
                json.Accumulate("STATUS", "SUCC");
            else
                json.Accumulate("STATUS", "FAIL");
            return json;
        }

        #endregion 回复站内信息

        #region 删除站内信息

        /// <summary>
        /// 删除收到的站内信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonObject DelReceiveMsg(int MsgID,int UserID=0)
        {
            JsonObject json = new JsonObject();
            if (bllSM.SetReceiveMsgToDelById(MsgID) > 0)
                json.Accumulate("STATUS", "SUCC");
            else
                json.Accumulate("STATUS", "FAIL");
            return json;
        }

        #endregion 删除站内信息

        #region 读取站内信息
        /// <summary>
        /// 读取站内信息
        /// </summary>
        /// <returns></returns>
        public JsonObject ReadMsg(int? MsgID,int UserID)
        {
            JsonObject json = new JsonObject();
            if (MsgID.HasValue)
            {
                Maticsoft.Model.Members.SiteMessage siteModel = bllSM.GetModelByCache(MsgID.Value);
                if (siteModel != null &&
                    ((siteModel.ReceiverID.HasValue && siteModel.ReceiverID.Value == UserID) ||
                     (siteModel.SenderID.HasValue && siteModel.SenderID.Value == UserID)))
                {
                    if (siteModel.ReceiverIsRead == false)
                        bllSM.SetReceiveMsgAlreadyRead(siteModel.ID);
                    json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Accumulate(SHOP_KEY_DATA, siteModel);
                }
            }
            else
            {
                json.Accumulate(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }
        #endregion 读取站内信息

        #region 收件箱

    

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public JsonObject InboxList(int pageIndex, int PageSize = 7, int UserID = 0)
        {
            JsonObject json = new JsonObject();
           
          
            int _pageSize = PageSize;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = bllSM.GetAllReceiveMsgCount(UserID, -1);
            if (toalCount < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            List<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllReceiveMsgListByPage(UserID, -1, startIndex, endIndex);

            if (list != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, list);
                json.Put(TOTALCOUNT, toalCount);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }

        #endregion 收件箱

        #region 发件箱

        /// <summary>
        /// 发件箱
        /// </summary>
        /// <returns></returns>
        public JsonObject Outbox(int pageIndex, int PageSize = 10, int UserID=0)
        {
           

            JsonObject json = new JsonObject();


            int _pageSize = PageSize;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = bllSM.GetSendMsgCount(UserID);
            if (toalCount < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            List<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSendMsgListByPage(UserID, startIndex, endIndex);

            if (list != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, list);
                json.Put(TOTALCOUNT, toalCount);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }

        #endregion 发件箱

        #region 系统信息

        /// <summary>
        /// 系统信息
        /// </summary>
        /// <returns></returns>
        public JsonObject SysInfo(int pageIndex, int PageSize = 10, int UserID = 0)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.Users info = userBll.GetModel(UserID);
           
            int _pageSize = PageSize;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = bllSM.GetAllSystemMsgCount(info.UserID, -1, info.UserType);
            if (toalCount < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            List<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSystemMsgListByPage(info.UserID, -1, info.UserType, startIndex,endIndex);

            if (list != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, list);
                json.Put(TOTALCOUNT, toalCount);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
            }
            return json;
        }

        #endregion 系统信息

        #region 收货地址
        public JsonObject ShippAddressList(int UserID)
        {
            JsonObject json = new JsonObject();
            Maticsoft.BLL.Shop.Shipping.ShippingAddress addressManage = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
            List<Maticsoft.Model.Shop.Shipping.ShippingAddress> list = addressManage.GetModelList(" UserId=" + UserID);

            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(SHOP_KEY_DATA, list);
            return json;
        }

        public JsonObject ShippAddress(int id = -1, int UserID=0)
        {
            JsonObject json = new JsonObject();
       

            Maticsoft.BLL.Shop.Shipping.ShippingAddress addressManage = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = new Maticsoft.Model.Shop.Shipping.ShippingAddress();
            if (id > 0) model = addressManage.GetModel(id);

            if (model != null && model.UserId != UserID)
            {
                json.Put(SHOP_KEY_STATUS,SHOP_STATUS_ERROR);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, model);
            }
            return json;
        }

        [HttpPost]
        public JsonObject SaveShippAddress(Maticsoft.Model.Shop.Shipping.ShippingAddress model,int UserID)
        {
            JsonObject json = new JsonObject();

            if (model == null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }

            Maticsoft.BLL.Shop.Shipping.ShippingAddress addressManage = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
            //Update
            if (model.ShippingId > 0)
            {
                if (addressManage.Update(model))
                {
                    json.Put(SHOP_KEY_STATUS,SHOP_STATUS_SUCCESS);
                    return json;
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    return json;
                }
            }
            //Add
            model.UserId = UserID;
            model.ShippingId = addressManage.Add(model);
            if (model.ShippingId > 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                return json;
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                return json;
            }
        }

        [HttpPost]
        public JsonObject DelShippAddress(int id,int UserID)
        {
            JsonObject json = new JsonObject();
            if (id < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            Maticsoft.BLL.Shop.Shipping.ShippingAddress addressManage = new Maticsoft.BLL.Shop.Shipping.ShippingAddress();
            Maticsoft.Model.Shop.Shipping.ShippingAddress model = addressManage.GetModel(id);
            if (model != null && UserID == model.UserId)
            {
                if (addressManage.Delete(id))
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    return json;
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    return json;
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                return json;
            }
            
        }
        #endregion

        #region 查看订单明细

        /// <summary>
        /// 查看订单明细
        /// </summary>
        public JsonObject OrderInfo(long id = -1,int UserID=0)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Shop.Order.OrderInfo orderModel = _orderManage.GetModelInfo(id);
            //Safe
            if (orderModel == null || orderModel.BuyerID != UserID)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                return json;
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA,orderModel );
                return json;
            }
           
        }
        /// <summary>
        /// 订单操作记录
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        public JsonObject OrderAction(long OrderId = -1)
        {
            JsonObject json = new JsonObject();
            Maticsoft.BLL.Shop.Order.OrderAction actionBll = new Maticsoft.BLL.Shop.Order.OrderAction();
            List<Maticsoft.Model.Shop.Order.OrderAction> actionList = actionBll.GetModelList(" OrderId=" + OrderId);
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(SHOP_KEY_DATA, actionList);
            return json;
        }

        #endregion

        #region Ajax方法
        /// <summary>
        /// 移除收藏项
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public JsonObject RemoveFavorItem(int FavoriteId)
        {
            JsonObject json = new JsonObject();
            if (FavoriteId>0)
            {
              
                Maticsoft.BLL.Shop.Favorite favoBll = new Maticsoft.BLL.Shop.Favorite();
                int favoriteId = Maticsoft.Common.Globals.SafeInt(FavoriteId, 0);
                if (favoBll.Delete(favoriteId))
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    return json;
                }
            }
            json.Put(SHOP_KEY_STATUS,SHOP_STATUS_FAILED);
            return json;
        }



        /// <summary>
        /// 加入收藏
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public JsonObject AjaxAddFav(int ProductId,int UserID)
        {
            JsonObject json = new JsonObject();
            if (ProductId>0 && UserID>0 )
            {
                int productId = ProductId;
                Maticsoft.BLL.Shop.Favorite favBll = new Maticsoft.BLL.Shop.Favorite();
                //是否已经收藏
                if (favBll.Exists(productId, UserID, 1))
                {
                    json.Put(SHOP_KEY_STATUS, "IsExists");
                    return json;
                }
                Maticsoft.Model.Shop.Favorite favMode = new Maticsoft.Model.Shop.Favorite();
                favMode.CreatedDate = DateTime.Now;
                favMode.TargetId = productId;
                favMode.Type = 1;
                favMode.UserId = UserID;
                if (favBll.Add(favMode) > 0)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    return json;
                }
               
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;
        }
        /// <summary>
        /// 添加咨询
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public JsonObject AjaxAddConsult(int UserID, int ProductID, string Content)
        {
            JsonObject json = new JsonObject();

            if (ProductID>0 && UserID>0 && !string.IsNullOrEmpty(Content))
            {
                Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
                Maticsoft.BLL.Shop.Products.ProductConsults consultBll = new Maticsoft.BLL.Shop.Products.ProductConsults();
                Maticsoft.Model.Shop.Products.ProductConsults consultMode = new Maticsoft.Model.Shop.Products.ProductConsults();
                consultMode.CreatedDate = DateTime.Now;
                consultMode.TypeId = 0;
                consultMode.Status = 0;
                consultMode.UserId = userModel.UserID;
                consultMode.UserName = userModel.NickName;
                consultMode.UserEmail = userModel.Email;
                consultMode.IsReply = false;
                consultMode.Recomend = 0;
                consultMode.ProductId = ProductID;
                consultMode.ConsultationText = Content;
                if (consultBll.Add(consultMode) > 0)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    return json;
                }
               
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;
        }
   

        #endregion

        #region 充值
        /// <summary>
        /// 余额 
        /// </summary>
        public JsonObject Balance(int UserID)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
            if (userModel != null)
            {
                decimal Balance = userEXBll.GetUserBalance(userModel.UserID);
                decimal BalanceDraw = balanDrawBll.GetBalanceDraw(userModel.UserID);
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                String jsonStr = "{\"Balance\":Balance,\"BalanceDraw\":BalanceDraw}";
                json.Put(SHOP_KEY_DATA,jsonStr);
                return json;
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;
        }
        /// <summary>
        ///  收支列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public JsonObject BalanceDetList(int pageIndex = 1, int pageSize=10, int UserID = 0)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
            if (userModel != null)
            {
                int _pageSize = pageSize;

                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;
                int toalCount = balanDetaBll.GetRecordCount(" UserId =" + userModel.UserID);//获取总条数 
                if (toalCount < 1)
                {
                    json.Put(SHOP_KEY_STATUS,SHOP_STATUS_ISNULL);
                    return json;
                  
                }
                List<Maticsoft.Model.Pay.BalanceDetails> list = balanDetaBll.GetListByPage(" UserId = " + userModel.UserID, startIndex, endIndex);
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(TOTALCOUNT, toalCount);
                json.Put(SHOP_KEY_DATA, list);
                return json;
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;
        }
        /// <summary>
        /// 充值明细
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public JsonObject RechargeList(int pageIndex = 1,int pageSize=10, int UserID = 0)
        {
            JsonObject json = new JsonObject();
              Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
              if (userModel != null)
              {
                  int _pageSize = pageSize;

                  //计算分页起始索引
                  int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                  //计算分页结束索引
                  int endIndex = pageIndex * _pageSize;
                  int toalCount = rechargeBll.GetRecordCount(" UserId =" + userModel.UserID);//获取总条数 
                  if (toalCount < 1)
                  {
                      json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                      return json;
                    
                  }
                  List<Maticsoft.Model.Pay.RechargeRequest> list = rechargeBll.GetRechargeListByPage(" UserId= " + userModel.UserID, startIndex, endIndex);
                
                  json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                  json.Put(TOTALCOUNT, toalCount);
                  json.Put(SHOP_KEY_DATA, list);
                  return json;
              }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;

        }
        /// <summary>
        ///  提现列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public JsonObject DrawDetList(int pageIndex = 1, int pageSize = 10, int UserID = 0, int conditionID = 1)
        {
              JsonObject json = new JsonObject();
              Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
              if (userModel != null)
              {
                  //condition 1:最近三月 2：最近一月 3：最近一周
                  DateTime CurrentDate = DateTime.Now;
                  DateTime AfterDate = new DateTime();
                  switch (conditionID)
                  {
                      case 1: AfterDate = CurrentDate.AddMonths(-3); break;
                      case 2: AfterDate = CurrentDate.AddMonths(-1); break;
                      case 3: AfterDate = CurrentDate.AddDays(-7); break;
                      default:
                          break;
                  }

                  int _pageSize = 10;

                  //计算分页起始索引
                  int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

                  //计算分页结束索引
                  int endIndex = pageIndex * _pageSize;
                  int toalCount = balanDrawBll.GetRecordCount(" RequestType=1 AND UserId =" + userModel.UserID + " AND RequestTime>='" + AfterDate + "'");//获取总条数 
                  if (toalCount < 1)
                  {
                      json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                      return json;
                  }
                  List<Maticsoft.Model.Pay.BalanceDrawRequest> list = balanDrawBll.GetListByPage(" RequestType=1 AND UserId= " + userModel.UserID + " AND RequestTime>='" + AfterDate + "'", startIndex, endIndex);
                  json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                  json.Put(TOTALCOUNT, toalCount);
                  json.Put(SHOP_KEY_DATA, list);
                  return json;
              }
              json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
              return json;
        }
      
        /// <summary>
        /// 提交充值
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public JsonObject AjaxRecharge(int UserID, int payid, decimal rechargmoney)
        {
             JsonObject json = new JsonObject();
              Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
              if (userModel != null && rechargmoney > 0 && payid>0)
              {
                  
                    
                      decimal rechMoney = rechargmoney;
                      if (payid > 0 && rechMoney > 0)
                      {
                          #region 充值比例计算
                          decimal rechargeRadio = Maticsoft.BLL.SysManage.ConfigSystem.GetDecimalValueByCache("Shop_RechargeRatio");
                          decimal money = rechMoney;
                          if (rechargeRadio > decimal.MinusOne)
                          {
                              money = Math.Round(rechMoney / rechargeRadio, 2);
                          }
                          #endregion

                          Maticsoft.Model.Pay.RechargeRequest rechModel = new Maticsoft.Model.Pay.RechargeRequest();
                          Maticsoft.Payment.Model.PaymentModeInfo paymodel = Maticsoft.Payment.BLL.PaymentModeManage.GetPaymentModeById(payid);
                          if (paymodel == null)
                          {
                              json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                              json.Put(SHOP_KEY_MESSAGE, "付款方式为空");
                              return json;
                            
                          }
                          rechModel.RechargeBlance = money;
                          rechModel.PaymentGateway = paymodel.Gateway;
                          rechModel.PaymentTypeId = payid;
                          rechModel.Status = 0;
                          rechModel.TradeDate = DateTime.Now;
                          rechModel.Tradetype = 1;
                          rechModel.UserId = userModel.UserID;
                          long rechCode = rechargeBll.Add(rechModel);
                          if (rechCode > 0)
                          {
                              json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                              return json;
                          }
                      }
                  
              }
              json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
              return json;
        }
        /// <summary>
        /// Maticsoft.Model.Pay.RechargeRequest 返回充值请求对象
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public JsonObject RechargeConfirm(int? id)
        {
            JsonObject json = new JsonObject();
            if (id.HasValue)
            {
           
                Maticsoft.Model.Pay.RechargeRequest rechmodel = rechargeBll.GetModelByCache(id.Value);
                if (rechmodel != null)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, rechmodel);
                    return json;
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    json.Put(SHOP_KEY_MESSAGE, "此充值信息为空");
                    return json;
                }
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;
        }
        [HttpPost]
        public JsonObject DelRecharge(long id)
        {
            JsonObject json = new JsonObject();
          
            if (id <= 0)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                return json;
            }
            Maticsoft.Model.Pay.RechargeRequest model = rechargeBll.GetModel(id);
            if (model != null && model.Status == 0)
            {
                if (rechargeBll.Delete(id))
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    return json;
                }
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;
        }

        #endregion

        #region 提现
        /// <summary>
        /// 返回账号余额
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public JsonObject Draw(int UserID)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
            if (userModel != null)
            {
                decimal Balance = userEXBll.GetUserBalance(userModel.UserID);
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put("Balance",Balance);
                return json;
                
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            return json;
        }

        /// <summary>
        /// 申请提现 ajax请求
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public JsonObject AjaxDraw(int typeid, string bankcard, decimal amount, int UserID, string TrueName, string BankName)
        {
            JsonObject json = new JsonObject();
             Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
             if (userModel != null)
             {
                 json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                 json.Put(SHOP_KEY_MESSAGE, "不存在此用户信息");
                 return json;
             }
            if (amount <= 0 || typeid <= 0 || String.IsNullOrWhiteSpace(bankcard))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                return json;
            }
            string trueName = "";
            string bankName = "";
            if (typeid == 1) //帐号类型为银行卡
            {
                trueName = InjectionFilter.Filter(TrueName);
                bankName = InjectionFilter.Filter(BankName);
                if (String.IsNullOrWhiteSpace(trueName) || String.IsNullOrWhiteSpace(bankName))
                {
                    json.Put(SHOP_KEY_STATUS,SHOP_STATUS_FAILED);
                    json.Put(SHOP_KEY_MESSAGE, "银行卡号和开户名不能为空");
                    return json;
                }
            }
            if (amount > userEXBll.GetUserBalance(userModel.UserID))//提现金额大于余额
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                json.Put(SHOP_KEY_MESSAGE, "提现金额大于余额");
                return json;
            
            }
            Maticsoft.Model.Pay.BalanceDrawRequest balanDrawModel = new Maticsoft.Model.Pay.BalanceDrawRequest();
            balanDrawModel.Amount = amount;
            balanDrawModel.BankCard = bankcard;
            balanDrawModel.CardTypeID = typeid;
            balanDrawModel.RequestStatus = 1;
            balanDrawModel.RequestTime = DateTime.Now;
            if (typeid == 1)
            {
                balanDrawModel.BankName = bankName;
                balanDrawModel.TrueName = trueName;
            }
            balanDrawModel.UserID = userModel.UserID;
            if (balanDrawBll.AddEx(balanDrawModel))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                return json;
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                return json;
            }
        }


        #endregion

        #region  物流信息
        public JsonObject ExpressList(string ecode)
        {
            JsonObject json = new JsonObject();
            Maticsoft.BLL.Shop.Shipping.Express bll = new Maticsoft.BLL.Shop.Shipping.Express();
            List<Maticsoft.Model.Shop.Shipping.LastData> model = new List<Maticsoft.Model.Shop.Shipping.LastData>();
            List<Maticsoft.Model.Shop.Shipping.Shop_Express> list = bll.GetListModel("ExpressCode='" + ecode + "'", "UpdateTime", 0);
            if (list != null && list.Count > 0)
            {
                model = Maticsoft.Model.Shop.Shipping.comm.JsonToObject<List<Maticsoft.Model.Shop.Shipping.LastData>>(list[0].ExpressContent.ToString());
            }
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(SHOP_KEY_DATA, model);
            return json;
           
        }
        #endregion

        #region 订单列表
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">nopay 未付款 all 已付款 </param>
        /// <param name="pageIndex"></param>
        /// <param name="pagesize"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public JsonObject OrderList(string type = "all", int pageIndex = 1, int pagesize = 10, int UserID=0)
        {
            JsonObject json = new JsonObject();
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
            Maticsoft.BLL.Shop.Order.OrderItems itemBll = new Maticsoft.BLL.Shop.Order.OrderItems();
              Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
              if (userModel != null)
              {
                  json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                  json.Put(SHOP_KEY_MESSAGE, "此用户不存在");
                  return json;
              }
            int _pageSize = pagesize;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            string where = "OrderOptType=1 and  BuyerID=" + userModel.UserID +
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

            if (type == "nopay")
            {
                where = where + string.Format(" and PaymentStatus={0} and OrderStatus={1} ", (int)Maticsoft.Model.Shop.Order.EnumHelper.PaymentStatus.Unpaid, (int)Maticsoft.Model.Shop.Order.EnumHelper.OrderStatus.UnHandle);
            }
            //获取总条数
            toalCount = orderBll.GetRecordCount(where);
            if (toalCount < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ISNULL);
                
                return json;
            }
            List<Maticsoft.Model.Shop.Order.OrderInfo> orderList = orderBll.GetListByPageEX(where, "", startIndex, endIndex);
            if (orderList != null && orderList.Count > 0)
            {
                foreach (Maticsoft.Model.Shop.Order.OrderInfo item in orderList)
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

            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(TOTALCOUNT,toalCount );
            json.Put(SHOP_KEY_DATA, orderList);
            return json;
        }

    
        #region 辅助方法

        public static string GetOrderType(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            string str = "";
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
            Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus orderType = orderBll.GetOrderType(paymentGateway,
                                    orderStatus,
                                    paymentStatus,
                                    shippingStatus);
            switch (orderType)
            {
                //  订单组合状态 1 等待付款   | 2 等待处理 | 3 取消订单 | 4 订单锁定 | 5 等待付款确认 | 6 正在处理 |7 配货中  |8 已发货 |9  已完成
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Paying:
                    str = "等待付款";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreHandle:
                    str = "等待处理";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Cancel:
                    str = "取消订单";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Locking:
                    str = "订单锁定";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.PreConfirm:
                    str = "等待付款确认";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Handling:
                    str = "正在处理";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shipping:
                    str = "配货中";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Shiped:
                    str = "已发货";
                    break;
                case Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus.Complete:
                    str = "已完成";
                    break;
                default:
                    str = "未知状态";
                    break;
            }
            return str;
        }
        public static Maticsoft.Model.Shop.Order.EnumHelper.OrderMainStatus GetOrderMainStatus(string paymentGateway, int orderStatus, int paymentStatus, int shippingStatus)
        {
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
            return orderBll.GetOrderType(paymentGateway,
                                    orderStatus,
                                    paymentStatus,
                                    shippingStatus);
        }

        #endregion
        #region Ajax方法
        [HttpPost]
        public JsonObject CancelOrder(int UserID, long OrderId)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
            if (userModel != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                json.Put(SHOP_KEY_MESSAGE, "此用户不存在");
                return json;
            }
            Maticsoft.BLL.Shop.Order.Orders orderBll = new Maticsoft.BLL.Shop.Order.Orders();
            long orderId = OrderId;
            Maticsoft.Model.Shop.Order.OrderInfo orderInfo = orderBll.GetModelInfo(orderId);
            if (orderInfo == null || orderInfo.BuyerID != userModel.UserID)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                json.Put(SHOP_KEY_MESSAGE, "非法操作");
                return json;
            }
            Maticsoft.Accounts.Bus.User userinfo=new Maticsoft.Accounts.Bus.User();
            userinfo.UserID=userModel.UserID;
            userinfo.UserName=userModel.UserName;
            userinfo.UserType=userModel.UserType;
            if (Maticsoft.BLL.Shop.Order.OrderManage.CancelOrder(orderInfo, userinfo))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                return json;
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                return json;
            }
        }
        [HttpPost]//完成订单
        public JsonObject CompleteOrder(int UserID, long OrderId)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserID);
            if (userModel != null)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                json.Put(SHOP_KEY_MESSAGE, "此用户不存在");
                return json;
            }
           
            long orderId =OrderId;
            Maticsoft.Model.Shop.Order.OrderInfo orderInfo = _orderManage.GetModelInfo(orderId);
            Maticsoft.Accounts.Bus.User userinfo = new Maticsoft.Accounts.Bus.User();
            userinfo.UserID = userModel.UserID;
            userinfo.UserName = userModel.UserName;
            userinfo.UserType = userModel.UserType;
            if (orderInfo == null || orderInfo.BuyerID != userModel.UserID)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                json.Put(SHOP_KEY_MESSAGE, "非法操作");
                return json;
            }
            if (Maticsoft.BLL.Shop.Order.OrderManage.CompleteOrder(orderInfo, userinfo))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                return json;
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                return json;
            }
        }
        #endregion
        #endregion

     

    }
}