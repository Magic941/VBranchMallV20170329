/**
* SocialControllerBase.cs
*
* 功 能： 社会化授权/登录基类
* 类 名： SocialControllerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/28 18:26:17   huhy    初版
* V0.02  2013/7/24 14:29:50   Ben     更正 自动重试 的跳转路由为[动态]当前区域
* V0.03  2013/8/22 21:20:16   Ben     新增 腾讯微博 API
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.OAuth.Json;
using Maticsoft.OAuth.Rest.Client;
using Maticsoft.OAuth.v2;

namespace Maticsoft.Web.Controllers
{
    public abstract class SocialControllerBase : Web.Controllers.ControllerBase
    {
        /*
Social_SinaAppId
Social_SinaSercet

Social_QQAppId
Social_QQSercet

Social_TencentAppId
Social_TencentSercet
         */
        private const string SESSION_KEY_OAUTH2STATE = "OAuth2CallbackState";
        private const string SESSION_KEY_OAUTH2TRY = "OAuth2CallbackTry";
        private readonly string STATE = "maticsoft" + DateTime.Now.ToString("yyyyMMddHHmmssffff");

        #region AppKey
        private string SinaAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaAppId");
        private string SinaSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_SinaSercet");

        private string QQAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQAppId");
        private string QQSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_QQSercet");

        private string TencentAppId = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentAppId");
        private string TencentSercet = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Social_TencentSercet");
        #endregion

        #region RedirectUrl

        private readonly string _domain = "http://" + Common.Globals.DomainFullName;

        private string RedirectSinaUrl
        {
            get { return _domain + "/social/sinacallback"; }
        }

        private string RedirectQQUrl
        {
            get { return _domain + "/social/qqcallback"; }
        }

        private string RedirectTencentUrl
        {
            get { return _domain + "/social/tencentcallback"; }
        }

        #endregion

        #region Authorize

        #region Tencent

        public ActionResult Tencent()
        {
            ////退出登录用户
            //System.Web.Security.FormsAuthentication.SignOut();
            //Session.Remove(Globals.SESSIONKEY_USER);

            if (string.IsNullOrWhiteSpace(TencentAppId))
            {
                return Content("该网站尚未启用腾讯微博登录");
            }

            IOAuth2ServiceProvider<OAuth.Tencent.Weibo.IWeibo> weiboProvider =
                new OAuth.Tencent.Weibo.WeiboServiceProvider(TencentAppId, TencentSercet);

            OAuth2Parameters parameters = new OAuth2Parameters
            {
                RedirectUrl = RedirectTencentUrl,
                State = STATE
            };
            Session[SESSION_KEY_OAUTH2STATE] = STATE;
            return Redirect(
                weiboProvider.OAuthOperations.BuildAuthorizeUrl(
                    GrantType.AuthorizationCode, parameters));
        }

        #endregion

        #region QQ

        public ActionResult QQ()
        {
            ////退出登录用户
            //System.Web.Security.FormsAuthentication.SignOut();
            //Session.Remove(Globals.SESSIONKEY_USER);

            if (string.IsNullOrWhiteSpace(QQAppId))
            {
                return Content("该网站尚未启用QQ登录");
            }

            IOAuth2ServiceProvider<OAuth.Tencent.QQ.IQConnect> connectProvider =
                new OAuth.Tencent.QQ.QConnectServiceProvider(QQAppId, QQSercet);

            OAuth2Parameters parameters = new OAuth2Parameters
            {
                RedirectUrl = RedirectQQUrl,
                Scope = "get_user_info,add_t,add_pic_t",
                State = STATE
            };
            Session[SESSION_KEY_OAUTH2STATE] = STATE;
            return Redirect(
                connectProvider.OAuthOperations.BuildAuthorizeUrl(
                    GrantType.AuthorizationCode, parameters));
        }

        #endregion

        #region Sina

        public ActionResult Sina()
        {
            ////退出登录用户
            //System.Web.Security.FormsAuthentication.SignOut();
            //Session.Remove(Globals.SESSIONKEY_USER);

            if (string.IsNullOrWhiteSpace(SinaAppId))
            {
                return Content("该网站尚未启用新浪微博登录");
            }

            IOAuth2ServiceProvider<OAuth.Sina.IWeibo> weiboProvider =
                new OAuth.Sina.WeiboServiceProvider(SinaAppId, SinaSercet);
            OAuth2Parameters parameters = new OAuth2Parameters
            {
                RedirectUrl = RedirectSinaUrl,
                //forcelogin=true,
                State = STATE
            };

            return Redirect(
                weiboProvider.OAuthOperations.BuildAuthorizeUrl(
                    GrantType.AuthorizationCode, parameters));
        }

        #endregion

        #endregion

        #region Callback

        #region CheckState

        private bool CheckSessionState(string state)
        {
            string stateSession = Session[SESSION_KEY_OAUTH2STATE] as string;
            Session.Remove(SESSION_KEY_OAUTH2STATE);
            return !string.IsNullOrWhiteSpace(stateSession) && stateSession == state;
        }

        #endregion

        #region TencentCallback

        public ActionResult TencentCallback(string code, string state, string openid, string openkey)
        {
            if (string.IsNullOrWhiteSpace(code) || !CheckSessionState(state)) return Redirect("/");

            IOAuth2ServiceProvider<OAuth.Tencent.Weibo.IWeibo> weiboProvider =
                new OAuth.Tencent.Weibo.WeiboServiceProvider(TencentAppId, TencentSercet);

            AccessGrant accessGrant;
            try
            {
                accessGrant = weiboProvider.OAuthOperations.ExchangeForAccessAsync(
                    code, RedirectTencentUrl, null).Result;
            }
            catch (AggregateException ex)
            {
                if (Session[SESSION_KEY_OAUTH2TRY] == null)
                {
                    Session[SESSION_KEY_OAUTH2TRY] = 3;
                }

                int index = Globals.SafeInt(Session[SESSION_KEY_OAUTH2TRY].ToString(), -1);
                HttpResponseException responseEx = ex.InnerExceptions[0].InnerException as HttpResponseException;
                if (responseEx != null)
                    LogHelp.AddErrorLog(responseEx.GetResponseBodyAsString(),
                        responseEx.StackTrace,
                        "OAuth2 TencentCallback HttpResponseException Try:" + index);

                //自动重试 3次
                if (index > 0)
                {
                    Session[SESSION_KEY_OAUTH2TRY] = --index;
                    return RedirectToAction("Tencent", "Social", new
                    {
                        area = MvcApplication.GetCurrentAreaRoute(ControllerContext).ToString()
                    });
                }
                Session.Remove(SESSION_KEY_OAUTH2TRY);
                return Redirect("/");
            }
            //成功后删除重试Session
            Session.Remove(SESSION_KEY_OAUTH2TRY);

            //根据openid|openkey|clientip重新构造
            accessGrant = new AccessGrant(accessGrant, new[] { openid, openkey, Globals.ClientIP });

            //加载用户信息
            OAuth.Tencent.Weibo.IWeibo qqClient = weiboProvider.GetApi(accessGrant);

            JsonValue userInfoJson;
            try
            {
                userInfoJson = qqClient.GetUserProfileAsync().Result;
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message,
                    ex.StackTrace, "OAuth2 TencentCallback GetUserProfileAsync Exception");
                return Redirect("/");
            }
            if (userInfoJson == null)
            {
                string msg = "GetUserProfileAsync: userInfoJson IS NULL";
                LogHelp.AddErrorLog(msg, msg, "OAuth2 TencentCallback");
                return Redirect("/");
            }

            //DONE: 保存恶劣的OpenId|OpenKey
            string userIdOAuth = accessGrant.ExtraData[0] + "|" + accessGrant.ExtraData[1];
            string nickNameOAuth = userInfoJson.GetValue("data").GetValue<string>("name");
            //TODO: 用户头像

            return CallbackUserInfo(Model.Members.Enum.MediaType.Tencent,
                accessGrant, userIdOAuth, nickNameOAuth, null);
        }

        #endregion

        #region QQCallback

        public ActionResult QQCallback(string code, string state)
        {
            if (string.IsNullOrWhiteSpace(code) || !CheckSessionState(state)) return Redirect("/");

            IOAuth2ServiceProvider<OAuth.Tencent.QQ.IQConnect> connectProvider =
                new OAuth.Tencent.QQ.QConnectServiceProvider(QQAppId, QQSercet);

            AccessGrant accessGrant;
            try
            {
                accessGrant = connectProvider.OAuthOperations.ExchangeForAccessAsync(
                    code, RedirectQQUrl, null).Result;
            }
            catch (AggregateException ex)
            {
                if (Session[SESSION_KEY_OAUTH2TRY] == null)
                {
                    Session[SESSION_KEY_OAUTH2TRY] = 3;
                }

                int index = Globals.SafeInt(Session[SESSION_KEY_OAUTH2TRY].ToString(), -1);
                HttpResponseException responseEx = ex.InnerExceptions[0].InnerException as HttpResponseException;
                if (responseEx != null)
                    LogHelp.AddErrorLog(responseEx.GetResponseBodyAsString(),
                        responseEx.StackTrace,
                        "OAuth2 QQCallback HttpResponseException Try:" + index);

                //自动重试 3次
                if (index > 0)
                {
                    Session[SESSION_KEY_OAUTH2TRY] = --index;
                    return RedirectToAction("QQ", "Social", new
                    {
                        area = MvcApplication.GetCurrentAreaRoute(ControllerContext).ToString()
                    });
                }
                Session.Remove(SESSION_KEY_OAUTH2TRY);
                return Redirect("/");
            }
            //成功后删除重试Session
            Session.Remove(SESSION_KEY_OAUTH2TRY);

            //加载用户信息
            OAuth.Tencent.QQ.IQConnect qqClient = connectProvider.GetApi(accessGrant);
            JsonValue userInfoJson;
            try
            {
                userInfoJson = qqClient.GetUserProfileAsync().Result;
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message,
                    ex.StackTrace, "OAuth2 QQCallback GetUserProfileAsync Exception");
                return Redirect("/");
            }
            if (userInfoJson == null)
            {
                string msg = "GetUserProfileAsync: userInfoJson IS NULL";
                LogHelp.AddErrorLog(msg, msg, "OAuth2 QQCallback");
                return Redirect("/");
            }

            //DONE: 保存恶劣的OpenId
            string userIdOAuth = accessGrant.ExtraData[0];
            string nickNameOAuth = userInfoJson.GetValue<string>("nickname");

            return CallbackUserInfo(Model.Members.Enum.MediaType.QZone,
                accessGrant, userIdOAuth, nickNameOAuth, null);
        }

        #endregion

        #region SinaCallback

        public ActionResult SinaCallback(string code)
        {
            IOAuth2ServiceProvider<OAuth.Sina.IWeibo> weiboProvider =
                new OAuth.Sina.WeiboServiceProvider(SinaAppId, SinaSercet);

            if (string.IsNullOrWhiteSpace(code)) return Redirect("/");
            AccessGrant accessGrant;
            try
            {
                accessGrant = weiboProvider.OAuthOperations.ExchangeForAccessAsync(
                    code, RedirectSinaUrl, null).Result;
            }
            catch (AggregateException ex)
            {
                if (Session[SESSION_KEY_OAUTH2TRY] == null)
                {
                    Session[SESSION_KEY_OAUTH2TRY] = 3;
                }

                int index = Globals.SafeInt(Session[SESSION_KEY_OAUTH2TRY].ToString(), -1);
                HttpResponseException responseEx = ex.InnerExceptions[0].InnerException as HttpResponseException;
                if (responseEx != null)
                    LogHelp.AddErrorLog(responseEx.GetResponseBodyAsString(),
                        responseEx.StackTrace,
                        "OAuth2 SinaCallback HttpResponseException Try:" + index);
                //自动重试 3次
                if (index > 0)
                {
                    Session[SESSION_KEY_OAUTH2TRY] = --index;
                    return RedirectToAction("Sina", "Social", new
                    {
                        area = MvcApplication.GetCurrentAreaRoute(ControllerContext).ToString()
                    });
                }
                else
                {
                    Session.Remove(SESSION_KEY_OAUTH2TRY);
                }
                return Redirect("/");
            }

            //加载用户信息
            OAuth.Sina.IWeibo weiboClient = weiboProvider.GetApi(accessGrant);
            JsonValue userInfoJson;
            try
            {
                userInfoJson = weiboClient.GetUserProfileAsync().Result;
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(ex.Message,
                    ex.StackTrace, "OAuth2 SinaCallback GetUserProfileAsync Exception");
                return Redirect("/");
            }

            if (userInfoJson == null)
            {
                string msg = "GetUserProfileAsync: userInfoJson IS NULL";
                LogHelp.AddErrorLog(msg, msg, "OAuth2 SinaCallback");
                return Redirect("/");
            }
            long userIdOAuth = userInfoJson.GetValue<long>("id");
            string nickNameOAuth = userInfoJson.GetValue<string>("name");
            return CallbackUserInfo(Model.Members.Enum.MediaType.Sina,
                accessGrant, userIdOAuth.ToString(), nickNameOAuth, null);
        }

        #endregion

        #region TaoBaoCallback

        public ActionResult TaoBaoCallback()
        {
            Session["TaoBao_Session_Key"] = Request.Params["top_session"];
            if (currentUser != null && currentUser.UserType == "AA")
            {
                return Redirect("/Admin/Ms/TaoData/GetTaoList.aspx");
            }
            return Redirect("/");
        }

        #endregion

        #region 创建/加载用户

        private ActionResult CallbackUserInfo(Model.Members.Enum.MediaType mediaType, AccessGrant accessGrant,
            string userIdOAuth,
            string nickNameOAuth, string emailOAuth)
        {

            //如果是已登录用户 就是用户帐号绑定功能
            if (CurrentUser != null)
            {
                //判重 
                Maticsoft.BLL.Members.UserBind BindBll = new BLL.Members.UserBind();
                Maticsoft.Model.Members.UserBind BindModel = new Model.Members.UserBind();
                BindModel.MediaID = (int)mediaType;
                BindModel.MediaNickName = nickNameOAuth;
                BindModel.MediaUserID = userIdOAuth.ToString();
                BindModel.TokenAccess = accessGrant.AccessToken;
                BindModel.UserId = CurrentUser.UserType == "AA" ? -1 : CurrentUser.UserID;
                BindModel.TokenAccess = accessGrant.AccessToken;
                BindModel.TokenExpireTime = accessGrant.ExpireTime;
                BindModel.Comment = true;
                BindModel.iHome = true;
                BindModel.GroupTopic = true;
                if (BindBll.AddEx(BindModel))
                {
                    if (currentUser.UserType == "AA")
                    {
                        return Redirect("/Admin/Accounts/UserBind.aspx");
                    }
                    return RedirectToUserBind();
                }
                return Redirect("/");
            }
            //构造新的用户名
            string username = string.Format("{0}_{1}", mediaType.ToString(), userIdOAuth);
            string password = username + SinaSercet;
            Maticsoft.BLL.Members.Users memberManage = new BLL.Members.Users();
            Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User();
            //该用户已存在就直接登录
            if (user.HasUserByUserName(username))
            {
                User User = new User(username);
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                Session[Globals.SESSIONKEY_USER] = User;
                Session["Style"] = User.Style;
                //登录加积分
                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                pointBll.AddPoints(1, User.UserID, "登录操作");

                if (Session["returnPage"] != null)
                {
                    string returnpage = Session["returnPage"].ToString();
                    Session["returnPage"] = null;
                    return Redirect(returnpage);
                }
                //登录之后跳转到个人信息页
                return RedirectToHome();
            }

            //NikeName重复处理 使用随机+六位处理
            User userManage = new User();
            string nickName = nickNameOAuth;
            while (userManage.HasUserByNickName(nickName))
            {
                //如果已存在 追加随机六位
                nickName = nickNameOAuth + "_" + Common.Globals.GenRandomCodeFor6();
            }

            //先注册，然后直接登录
            user.UserName = username;
            user.Email = emailOAuth;
            user.Password = AccountsPrincipal.EncryptPassword(password);
            user.Activity = true;
            user.UserType = "UU";
            //user.EmployeeID = 1; //1:为三方登陆的用户
            user.NickName = nickName;
            user.Style = 1;
            user.User_dateCreate = DateTime.Now;
            user.User_cLang = "zh-CN";
            int userId = user.Create();
            if (userId > 0)
            {
                //添加用户扩展表数据
                BLL.Members.UsersExp ue = new BLL.Members.UsersExp();
                ue.UserID = userId;
                ue.Email = emailOAuth;
                ue.Gravatar = string.Format("/{0}/User/Gravatar/{1}", MvcApplication.UploadFolder, userId);
                ue.BirthdayVisible = 0;
                ue.BirthdayIndexVisible = false;
                ue.ConstellationVisible = 0;
                ue.ConstellationIndexVisible = false;
                ue.NativePlaceVisible = 0;
                ue.NativePlaceIndexVisible = false;
                ue.RegionId = 0;
                //ue.Address = "['', '','']";
                ue.AddressVisible = 0;
                ue.AddressIndexVisible = false;
                ue.BodilyFormVisible = 0;
                ue.BodilyFormIndexVisible = false;
                ue.BloodTypeVisible = 0;
                ue.BloodTypeIndexVisible = false;
                ue.MarriagedVisible = 0;
                ue.MarriagedIndexVisible = false;
                ue.PersonalStatusVisible = 0;
                ue.PersonalStatusIndexVisible = false;
                ue.LastAccessIP = "";
                ue.LastAccessTime = DateTime.Now;
                ue.LastLoginTime = DateTime.Now;
                ue.LastPostTime = DateTime.Now;
                if (!ue.AddUsersExp(ue))
                {
                    memberManage.Delete(userId);
                    BLL.Members.UsersExp expBll = new BLL.Members.UsersExp();
                    expBll.DeleteUsersExp(userId);
                    return Redirect("/");
                }
                User User = new User(username);
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, false);
                Session[Globals.SESSIONKEY_USER] = User;
                Session["Style"] = User.Style;

                //注册成功后是否自动绑定
                string IsBind = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_Register_IsBind");
                if (IsBind == "1")
                {
                    Maticsoft.BLL.Members.UserBind BindBll = new BLL.Members.UserBind();
                    Maticsoft.Model.Members.UserBind BindModel = new Model.Members.UserBind();
                    BindModel.MediaID = (int)mediaType;
                    BindModel.MediaNickName = nickNameOAuth;
                    BindModel.MediaUserID = userIdOAuth;
                    BindModel.TokenAccess = accessGrant.AccessToken;
                    BindModel.TokenExpireTime = accessGrant.ExpireTime;
                    BindModel.UserId = userId;
                    BindModel.TokenAccess = accessGrant.AccessToken;
                    BindModel.TokenExpireTime = accessGrant.ExpireTime;
                    BindModel.Comment = true;
                    BindModel.iHome = true;
                    BindModel.GroupTopic = true;
                    if (!BindBll.AddEx(BindModel))
                    {
                        return Redirect("/");
                    }
                }

                //头像
                string defaultGravatar = BLL.SysManage.ConfigSystem.GetValueByCache("DefaultGravatar");
                defaultGravatar = string.IsNullOrEmpty(defaultGravatar)
                    ? "/Upload/User/Gravatar/Default.jpg"
                    : defaultGravatar;
                string targetGravatarFile = BLL.SysManage.ConfigSystem.GetValueByCache("TargetGravatarFile");
                targetGravatarFile = string.IsNullOrEmpty(targetGravatarFile)
                    ? "/Upload/User/Gravatar/"
                    : targetGravatarFile;
                string path = ControllerContext.HttpContext.Server.MapPath("/");
                if (System.IO.File.Exists(path + defaultGravatar))
                {
                    System.IO.File.Copy(path + defaultGravatar, path + targetGravatarFile + User.UserID + ".jpg", true);
                }
                //注册加积分
                Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
                pointBll.AddPoints(2, userId, "注册成功");

                //创建默认专辑
                Maticsoft.Model.SNS.UserAlbums albumsModel = new Model.SNS.UserAlbums();
                Maticsoft.BLL.SNS.UserAlbums albumsBll = new BLL.SNS.UserAlbums();
                Maticsoft.BLL.SNS.UserShip shipBll = new BLL.SNS.UserShip();
                albumsModel.AlbumName = "默认专辑";
                albumsModel.CreatedDate = DateTime.Now;
                albumsModel.CreatedNickName = User.NickName;
                albumsModel.CreatedUserID = User.UserID;
                albumsBll.AddEx(albumsModel, 1);

                //自动给是粉丝
                shipBll.GiveUserFellow(User.UserID);
                //登录之后跳转到首页
                return RedirectToHome();
            }
            return Redirect("/");
        }

        #endregion

        #endregion


        public abstract ActionResult RedirectToUserBind();
        public abstract ActionResult RedirectToHome();

        #region 发布微博测试接口 - 已测试通过

        //        public ActionResult UpdateStatus(string msg)
        //        {
        //            Maticsoft.BLL.Members.UserBind userBind = new BLL.Members.UserBind();
        //            ViewModel.UserCenter.UserBindList userBindList = userBind.GetListEx(-1);
        //            if (userBindList == null) return new EmptyResult();

        //            if (userBindList.TenCent != null)
        //            {
        //#if true //正式代码时应使用 switch
        //                //腾讯微博API
        //                IOAuth2ServiceProvider<OAuth.Tencent.Weibo.IWeibo> weiboProvider =
        //                    new OAuth.Tencent.Weibo.WeiboServiceProvider(TencentAppId, TencentSercet);

        //                string[] openIdKeys = userBindList.TenCent.MediaUserID.Split(new[] { '|' },
        //                    StringSplitOptions.RemoveEmptyEntries);

        //                if (openIdKeys.Length < 2) throw new ArgumentNullException(" OpenIdKeys is NULL !");

        //                //加载用户信息
        //                OAuth.Tencent.Weibo.IWeibo weiboClient = weiboProvider.GetApi(
        //                    new AccessGrant(userBindList.TenCent.TokenAccess,
        //                       new[] { openIdKeys[0], openIdKeys[1], Globals.ClientIP }));
        //#else
        //                //新浪API
        //                IOAuth2ServiceProvider<OAuth.Sina.IWeibo> weiboProvider =
        //                    new OAuth.Sina.WeiboServiceProvider(SinaAppId, SinaSercet);
        //                //加载用户信息
        //                OAuth.Sina.IWeibo weiboClient = weiboProvider.GetApi(
        //                    new AccessGrant(userBindList.Sina.TokenAccess,
        //                        userBindList.Sina.MediaUserID));
        //                        //QQ登录API
        //                        IOAuth2ServiceProvider<OAuth.Tencent.QQ.IQConnect> connectProvider =
        //                       new OAuth.Tencent.QQ.QConnectServiceProvider(QQAppId, QQSercet);
        //                        OAuth.Tencent.QQ.IQConnect weiboClient = connectProvider.GetApi(
        //                            new AccessGrant(userBindList.QZone.TokenAccess,
        //                                            userBindList.QZone.MediaUserID)); 
        //#endif
        //                try
        //                {
        //                    #region 发表文字内容

        //                    weiboClient.UpdateStatusAsync(msg).Wait();

        //                    #endregion

        //                    #region 发表文字+图片内容
        //                    //System.Net.WebClient webclient = new System.Net.WebClient();
        //                    //webclient.DownloadFile("", "");   //如果是云存储图片, 下载到本地后获取流

        //                    //接口使用FileInfo 接收图片对象, 因为此对象包含文件名和路径
        //                    //weiboClient.UploadStatusAsync(msg,
        //                    //    new System.IO.FileInfo(@"D:\Ben\UserData\Pictures\AD\CMS\ad1.gif")).Wait();

        //                    #endregion
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogHelp.AddErrorLog(ex.Message,
        //                        ex.StackTrace, "OAuth2 UpdateStatus Exception");
        //                    return new EmptyResult();
        //                }
        //            }
        //            return new EmptyResult();
        //        }

        #endregion
    }
}
