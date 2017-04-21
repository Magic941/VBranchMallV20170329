using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using Maticsoft.Model;
using Maticsoft.Services;
using Newtonsoft.Json;
using System.Transactions;
using Maticsoft.Model.CMS;

namespace Maticsoft.BLL.Shop.Card
{
    public class UserCardLogic
    {
        public string baseuri = System.Configuration.ConfigurationManager.AppSettings["CardURL"];
        //BLL.SysManage.ConfigSystem.GetValueByCache("CardURL");

        #region 会员卡激活相关
        /// <summary>
        /// 获取卡的信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="pwd"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop_Card CheckCardInfo(string cardNo, string pwd, out string message)
        {
            message = "";
            var helper = new APIHelper(baseuri);
            if (!string.IsNullOrEmpty(cardNo) && !string.IsNullOrEmpty(pwd))
            {
                var result = helper.GetCardInfo(cardNo, pwd);
                Maticsoft.Model.Shop_Card card = JsonConvert.DeserializeObject<Maticsoft.Model.Shop_Card>(result);

                if (card != null)
                {
                    if (card.CardStatus == 4)
                    {
                        message = card.CREATER;
                        return null;
                    }
                    else
                    {
                        //card.cardtype
                        if (!string.IsNullOrEmpty(card.CardSelfType.InsurantClauseFileName1))
                            card.CardSelfType.InsurantClauseFileName1 = baseuri + card.CardSelfType.InsurantClauseFileName1;
                        if (!string.IsNullOrEmpty(card.CardSelfType.InsurantClauseFileName2))
                            card.CardSelfType.InsurantClauseFileName2 = baseuri + card.CardSelfType.InsurantClauseFileName2;
                        if (!string.IsNullOrEmpty(card.CardSelfType.InsurantClauseFileName3))
                            card.CardSelfType.InsurantClauseFileName3 = baseuri + card.CardSelfType.InsurantClauseFileName3;
                        if (!string.IsNullOrEmpty(card.CardSelfType.InsurantClauseFileName4))
                            card.CardSelfType.InsurantClauseFileName4 = baseuri + card.CardSelfType.InsurantClauseFileName4;
                        return card;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取用户的激活信息
        /// </summary>
        public object GetActiveUserInfos(string cardNo, string pwd)
        {
            var helper = new APIHelper(baseuri);
            if (!string.IsNullOrEmpty(cardNo) && !string.IsNullOrEmpty(pwd))
            {
                var result = helper.GetActiveUserInfos(cardNo, pwd);
                Maticsoft.Services.ErrorLogTxt.GetInstance("激活卡查询").Write("查询结果字符为！<" + result+">");
                if (result == "null" || result.IndexOf("未将对象引用设置到对象的实例") >= 0)
                {
                    return null;
                }
                if (result.IndexOf("LPNumber") >= 0)
                {
                    var card = JsonConvert.DeserializeObject<Maticsoft.Model.V_DriveCardInfo>(result);

                    if (card != null)
                    {
                        //保单文件路径
                        card.InsureOrderFileUrl = baseuri + card.InsureOrderFileUrl;
                        return card;
                    }

                }
                else
                {
                    ErrorLogTxt.GetInstance("卡信息查询结果").Write(result);

                    Maticsoft.Model.Shop_CardUserInfo card = JsonConvert.DeserializeObject<Maticsoft.Model.Shop_CardUserInfo>(result);

                    if (card != null)
                    {
                        //保单文件路径
                        card.InsureOrderFileUrl = baseuri + card.InsureOrderFileUrl;
                        return card;
                    }
                }

            }
            return null;

        }

        /// <summary>
        /// 获取卡类型的信息
        /// </summary>
        /// <param name="cardTypeNo"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop_CardType CheckCardTypeInfo(string cardTypeNo, out string message)
        {
            message = "";
            var helper = new APIHelper(baseuri);
            if (!string.IsNullOrEmpty(cardTypeNo))
            {
                var x = helper.GetCardTypeInfo(cardTypeNo);
                Maticsoft.Model.Shop_CardType card = JsonConvert.DeserializeObject<Maticsoft.Model.Shop_CardType>(x);
                if (card != null)
                {
                    if (card.TypeStatus == 4)
                    {
                        message = card.CREATER;
                        return null;
                    }
                    else
                    {
                        return card;
                    }
                }
            }
            return null;
        }


        public List<string> GetCardBatch()
        {
            var helper = new APIHelper(baseuri);
            var x = helper.GetCardBatch();
            return x;
        }

        /// <summary>
        /// 判断卡是否激活
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns>string</returns>
        public string CheckCardActive(string cardNo)
        {
            var helper = new APIHelper(baseuri);
            var x = helper.CheckCardIsActive(cardNo);
            return x;
        }


        /// <summary>
        /// 一张卡一个账号，方便管理
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public string ActiveUserInfo(Maticsoft.Model.Shop_CardUserInfo userinfo, bool Instead = false)
        {
            TransactionOptions tOpt = new TransactionOptions();
            //设置TransactionOptions模式
            tOpt.IsolationLevel = IsolationLevel.Serializable;
            // 设置超时间隔为2分钟，默认为60秒
            tOpt.Timeout = new TimeSpan(0, 2, 0);
            var helper = new APIHelper(baseuri);

            using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.RequiresNew, tOpt))
            {
                var card = new Shop_CardUserInfo();

                var x = helper.ActiveCard(userinfo);
                //ErrorLogTxt.GetInstance("卡激活后日志").Write(x);
                //测试事务完整性
                //int z = 0;
                //int y = 10;
                //int q = y / z;
                if (x.ToLower().Trim().Contains("ok"))
                {
                    //保留原密码                   
                    var pwd = userinfo.PasswordOrigin;
                    //卡系统密码已保存不需要再加密
                    //userinfo.Password = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userinfo.Password, "md5").ToLower();

                    Maticsoft.Accounts.Bus.User u = new Maticsoft.Accounts.Bus.User();
                    u.UserName = userinfo.CardNo;
                    u.UserType = "UU";
                    u.User_dateCreate = DateTime.Now;
                    u.Activity = true;
                    u.Email = userinfo.Email;
                    u.Phone = userinfo.Moble;
                    u.NickName = userinfo.Name;
                    u.TrueName = userinfo.Name;
                    u.Password = Maticsoft.Accounts.Bus.AccountsPrincipal.EncryptPassword(pwd);
                    u.User_iCreator = u.UserID;
                    card.AddUser4Card(u, userinfo);

                    tsCope.Complete();
                    return "1";
                }
                return x;

            }
        }


        /// <summary>
        /// 激活驾车卡
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public string ActiveUserInfo2(Maticsoft.Model.DriveCardInfoModel userinfo)
        {
            TransactionOptions tOpt = new TransactionOptions();
            //设置TransactionOptions模式
            tOpt.IsolationLevel = IsolationLevel.Serializable;
            // 设置超时间隔为2分钟，默认为60秒
            tOpt.Timeout = new TimeSpan(0, 2, 0);
            var helper = new APIHelper(baseuri);

            using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.RequiresNew, tOpt))
            {
                var card = new Shop_CardUserInfo();

                var x = helper.ActiveCard2(userinfo);

                if (x.ToLower().Trim().Contains("ok"))
                {
                    foreach (var card2 in userinfo.Cards)
                    {
                        //保留原密码                   
                        var pwd = card2.Password;
                        var pwdOk = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, "md5").ToLower();
                        Maticsoft.Accounts.Bus.User u = new Maticsoft.Accounts.Bus.User();
                        u.UserName = card2.CardNo;
                        u.UserType = "UU";
                        u.User_dateCreate = DateTime.Now;
                        u.Activity = true;

                        u.Phone = userinfo.Mobile;
                        u.NickName = userinfo.Name;
                        u.TrueName = userinfo.Name;
                        u.Password = Maticsoft.Accounts.Bus.AccountsPrincipal.EncryptPassword(pwd);
                        u.User_iCreator = u.UserID;

                        //前端激活信息
                        var userinfo2 = new Maticsoft.Model.Shop_CardUserInfo();
                        userinfo2.ActiveDate = DateTime.Now;
                        userinfo2.CardNo = card2.CardNo;
                        userinfo2.Password = pwdOk;
                        userinfo2.PasswordOrigin = pwd;
                        userinfo2.BackPerson = userinfo.SalesName;
                        userinfo2.CardId = userinfo.CardID != "" ? userinfo.CardID : userinfo.EnterpriseCode;
                        userinfo2.CardTypeNo = userinfo.CardTypeNo;
                        userinfo2.UserName = card2.CardNo;
                        card.AddUser4Card(u, userinfo2);
                    }
                    tsCope.Complete();
                    return "1";
                }
                return x;

            }
        }
        #endregion

        #region 会员中心相关
        /// <summary>
        /// 根据用户名获取此用户的会员卡信息
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public List<Model.Shop_CardUserInfo> GetCardInfo(string username)
        {
            var card = new Shop_CardUserInfo();
            var y = card.GetModelListByName(username);
            return y;
        }




        public string GetDefaultCardNo(string username)
        {
            var card = new Shop_CardUserInfo();
            var x = card.GetDefaultCardNo(username);
            return x;
        }

        public int GetDefaultCardsysID(string username)
        {
            var card = new Shop_CardUserInfo();
            var x = card.GetDefaultCardsysID(username);
            return x;
        }

        //设置默认卡
        public bool SetDefaultCard(string cardNo, string username)
        {
            if (!string.IsNullOrEmpty(cardNo) && !string.IsNullOrEmpty(username))
            {
                var card = new Shop_CardUserInfo();
                return card.SetDefaultCard(username, cardNo);
            }
            return false;
        }

        //判断是否是好邻会员
        public bool IsCardMember(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var list = GetCardInfo(username);
                foreach (var item in list)
                {
                    //item.CardNo;
                }

            }
            return false;
        }
        #endregion


        public bool CheckUserName(string username)
        {
            var u = new Accounts.Bus.User();
            return u.HasUserByUserName(username);
        }


        public bool CheckUserMember(string username)
        {
            var result = GetCardInfo(username);
            if (result.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool UpdateUserName(string oldusername, string newusername)
        {
            TransactionOptions tOpt = new TransactionOptions();
            //设置TransactionOptions模式
            tOpt.IsolationLevel = IsolationLevel.ReadCommitted;
            // 设置超时间隔为2分钟，默认为60秒
            tOpt.Timeout = new TimeSpan(0, 2, 0);

            var user = new Account.User();
            // var helper = new APIHelper(baseuri);

            Shop_CardUserInfo card = new Shop_CardUserInfo();

            // var result =  card.GetDefaultCardNo(oldusername);
            var result = card.GetModelListByName(oldusername);

            if (result.Count > 0)
            {
                //using (TransactionScope tsCope = new TransactionScope(TransactionScopeOption.RequiresNew, tOpt))
                //{
                var x = user.UpdateUserName4Accounts(oldusername, newusername);

                //  var y = helper.UpdateUserName(oldusername, newusername);

                //if (x && y.Trim().Contains("Ok"))
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                // }
                return x;
            }
            else
            {
                return user.UpdateUserName(oldusername, newusername);
            }
        }

        public List<JobTypeModel> GetJobTypes(string insuranceCompanyCode)
        {
            var helper = new APIHelper(baseuri);
            var result = helper.GetJobTypes(insuranceCompanyCode);
            var allitems = JsonConvert.DeserializeObject<List<JobTypeModel>>(result);

            return allitems;

        }

    }
}
