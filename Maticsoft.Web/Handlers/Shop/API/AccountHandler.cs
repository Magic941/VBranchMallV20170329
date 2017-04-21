/**
* UserHandler.cs
*
* 功 能： 商城 API
* 类 名： UserHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/24 17:04:23  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.Members;
using Maticsoft.Json;
using Maticsoft.Json.RPC;
using Maticsoft.Model.Members;
using Maticsoft.Web.Handlers.API;
using System.Text;
using Maticsoft.Model.Shop;
using System.Collections.Generic;
using Webdiyer.WebControls.Mvc;
using System.Web.Security;
using Maticsoft.Common;

namespace Maticsoft.Web.Handlers.Shop.API
{
    public partial class ShopHandler
    {
        #region 用户登录

        [JsonRpcMethod("Login", Idempotent = false)]
        [JsonRpcHelp("用户登录")]
        public JsonObject Login(string UserName, string Password)
        {
            if (string.IsNullOrWhiteSpace(UserName))
                return new Result(ResultStatus.Failed,
                    Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));

            try
            {
                AccountsPrincipal userPrincipal = AccountsPrincipal.ValidateLogin(UserName, Password);
                //登录失败，请确认用户名或密码是否正确。
                if (userPrincipal == null)
                {
                    LogHelp.AddUserLog(UserName, "", "登录失败!", Request);
                    return new Result(ResultStatus.Failed, Result.FormatFailed("40", "登录失败，请确认用户名或密码是否正确。"));
                }

                User currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                //您非普通用户，您没有权限使用接口系统！
                if (currentUser.UserType != "UU")
                {
                    return new Result(ResultStatus.Failed,
                        Result.FormatFailed(ERROR_CODE_UNAUTHORIZED, ERROR_MSG_UNAUTHORIZED));
                }

                Context.User = userPrincipal;
                //密码错误！
                if (((SiteIdentity)User.Identity).TestPassword(Password) == 0)
                {
                    LogHelp.AddUserLog(UserName, "", "密码错误！", Request);
                    return new Result(ResultStatus.Failed, Result.FormatFailed("42", "密码错误！"));
                }
                //对不起，该帐号已被冻结，请联系管理员！
                if (!currentUser.Activity)
                {
                    return new Result(ResultStatus.Failed, Result.FormatFailed("44", "对不起，该帐号已被冻结，请联系管理员！"));
                }
                //登录成功
                LogHelp.AddUserLog(currentUser.UserName, currentUser.UserType, "登录成功", Request);
                return new Result(ResultStatus.Success, GetUserInfo4Json(currentUser));
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message),
                    ex.StackTrace, Request);
                return new Result(ResultStatus.Error, ex);
            }
        }

        #endregion

        #region 注册用户
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Password">密码</param>
        /// <param name="NickName">昵称</param>
        /// <param name="TrueName">真实姓名</param>
        /// <param name="Email">邮箱</param>
        /// <param name="Phone">手机</param>
        /// <param name="MobileDI">设备信息标识</param>
        /// <param name="MobileDMID">识设备型号及平台ID</param>
        /// <returns>新用户ID</returns>
        [JsonRpcMethod("Register", Idempotent = false)]
        [JsonRpcHelp("注册用户")]
        public JsonObject Register(string UserName, string Password, string NickName,
            string TrueName, string Email, string Phone, string MobileDI, string MobileDMID)
        {
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password)) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));

            Maticsoft.Accounts.Bus.User user = new User();
            user.UserName = UserName;
            user.TrueName = TrueName;
            user.Email = Email;
            user.Phone = Phone;
            user.NickName = NickName;

            user.Sex = "1";
            user.EmployeeID = -1;
            user.DepartmentID = "-1";
            user.Activity = true;
            user.UserType = "UU";
            user.Style = 1;
            user.User_dateCreate = DateTime.Now;
            user.User_dateValid = DateTime.Now;
            user.User_cLang = "zh-CN";

            try
            {
                user.Password = AccountsPrincipal.EncryptPassword(Password);
                user.UserID = user.Create();
                if (user.UserID == -100)
                {
                    //用户已存在
                    return new Result(ResultStatus.Failed, Result.FormatFailed("101", "用户已存在!"));
                }
                //添加用户角色
                user.AddToRole(BLL.SysManage.ConfigSystem.GetIntValueByCache("DefaultEmpRoleID"));
                UsersExp bllUsersExp = new UsersExp();
                bllUsersExp.AddUsersExp(new UsersExpModel
                {
                    UserID = user.UserID,
                    LastAccessTime = DateTime.Now,
                    LastLoginTime = DateTime.Now,
                    LastPostTime = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
                return new Result(ResultStatus.Error, ex);
            }
            return new Result(ResultStatus.Success, user.UserID);
        }
        #endregion

        #region 用户名是否存在
        [JsonRpcMethod("HasUserByUserName", Idempotent = true)]
        [JsonRpcHelp("用户名是否存在")]
        public JsonObject HasUserByUserName(string UserName)
        {
            if (string.IsNullOrWhiteSpace(UserName)) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            Maticsoft.Accounts.Bus.User bllUser = new User();
            return Result.HasResult(bllUser.HasUserByUserName(UserName));
        }
        #endregion

        #region 昵称是否存在
        [JsonRpcMethod("HasUserByNickName", Idempotent = true)]
        [JsonRpcHelp("昵称是否存在")]
        public JsonObject HasUserByNickName(string NickName)
        {
            if (string.IsNullOrWhiteSpace(NickName)) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            Maticsoft.Accounts.Bus.User bllUser = new User();
            return Result.HasResult(bllUser.HasUserByNickName(NickName));
        }
        #endregion

        #region 获取个人信息
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="UserId">用户ID</param>
        /// <returns>用户信息</returns>
        [JsonRpcMethod("GetUserInfo", Idempotent = false)]
        [JsonRpcHelp("获取个人信息")]
        public JsonObject GetUserInfo(int UserId)
        {
            //超级管理员信息保护 过滤UserId=1用户
            if (UserId < 2) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            try
            {
                //TODO: 用户不存在 未对应
                return new Result(ResultStatus.Success,
                    GetUserInfo4Json(new User(UserId)));
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
                return new Result(ResultStatus.Error, ex);
            }
        }

        private JsonObject GetUserInfo4Json(User userInfo)
        {
            BLL.Members.UsersExp bll = new UsersExp();
            UsersExpModel user = bll.GetUsersModel(userInfo.UserID);
            if (userInfo == null || string.IsNullOrWhiteSpace(userInfo.UserType)) return null;
            JsonObject json = new JsonObject();
            json.Put("userId", user.UserID);
            json.Put("userName", user.UserName);
            json.Put("trueName", user.TrueName);
            json.Put("phone", user.Phone);
            json.Put("level", user.UserType);
            //json.Put("departmentID", userInfo.DepartmentID);
            json.Put("nickName", user.NickName);
            json.Put("headImage", "/Upload/User/Gravatar/" + (user.UserID) + ".jpg?id=" + DateTime.Now);
            json.Put("point", user.Points);

            return json;
        }
        #endregion

        #region 更新个人信息
        [JsonRpcMethod("UpdateUserInfo", Idempotent = false)]
        [JsonRpcHelp("更新个人信息")]
        public JsonObject UpdateUserInfo(int UserId, string Password, string NewPassword,
            string Phone, string Email, string NickName)
        {
            if (UserId < 2) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            try
            {
                Maticsoft.Accounts.Bus.User user = new User(UserId);
                //NO DATA
                if (string.IsNullOrWhiteSpace(user.UserType)) return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_NODATA, ERROR_MSG_NODATA));
                //修改密码
                if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(NewPassword))
                {
                    //验证旧密码
                    SiteIdentity siteIdentity = new SiteIdentity(UserId);
                    if (siteIdentity.TestPassword(Password) == 0)
                    {
                        return new Result(ResultStatus.Failed, Result.FormatFailed("101", "当前密码不正确, 更新个人信息失败!"));
                    }
                    //非普通用户禁用接口
                    if (user.UserType != "UU")
                    {
                        return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_UNAUTHORIZED, ERROR_MSG_UNAUTHORIZED));
                    }
                    user.Password = AccountsPrincipal.EncryptPassword(NewPassword);
                }
                //user.TrueName = TrueName;
                //if (Sex.HasValue) user.Sex = Sex.ToString();
                user.Email = Email;
                user.Phone = Phone;
                user.NickName = NickName;

                return new Result(user.Update());
            }
            catch (Exception ex)
            {
                LogHelp.AddErrorLog(string.Format(ERROR_MSG_LOG, Request.Headers[REQUEST_HEADER_METHOD], ex.Message), ex.StackTrace, Request);
                return new Result(ResultStatus.Error, ex);
            }
        }
        #endregion

        #region 我的收藏
        [JsonRpcMethod("FavorList", Idempotent = false)]
        [JsonRpcHelp("我的收藏")]
        public JsonObject FavorList(int pageIndex = 1,int pageNum=10,int UserID=-1)
        {
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            if (pageNum == 0)
            {
                pageNum = 10;
            }
            if (UserID < 1)
            {
                return new Result(ResultStatus.Failed, Result.FormatFailed(ERROR_CODE_ARGUMENT, ERROR_MSG_ARGUMENT));
            }
            Maticsoft.BLL.Shop.Favorite favoBll = new BLL.Shop.Favorite();
            StringBuilder strBuilder = new StringBuilder();
            // strBuilder.AppendFormat(" UserId ={0}  and  SaleStatus in ( {1},{2} ) ", CurrentUser.UserID, (int )ProductSaleStatus.InStock, (int )ProductSaleStatus.OnSale);
            strBuilder.AppendFormat(" favo.UserId ={0} and favo.Type= {1} ", UserID, (int)FavoriteEnums.Product);
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageNum + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * pageNum;
            int toalCount = favoBll.GetRecordCount(" UserId =" + UserID + " and Type=" + (int)FavoriteEnums.Product);//获取总条数 
            JsonObject json;
            JsonArray array = new JsonArray();
            JsonObject result = new JsonObject();
            if (toalCount < 1)
            {
                return new Result(ResultStatus.Success, null);
            }
            result.Put("list_count", toalCount);

            List<Maticsoft.ViewModel.Shop.FavoProdModel> favoList = favoBll.GetFavoriteProductListByPage(strBuilder.ToString(), startIndex, endIndex);
            PagedList<Maticsoft.ViewModel.Shop.FavoProdModel> lists = new PagedList<Maticsoft.ViewModel.Shop.FavoProdModel>(favoList, pageIndex, pageNum, toalCount);
            foreach (Maticsoft.ViewModel.Shop.FavoProdModel item in lists)
            {
                json = new JsonObject();
                json.Put("id", item.ProductId);
                json.Put("name", item.ProductName);
                json.Put("pic", Maticsoft.Web.Components.FileHelper.GeThumbImage(item.ThumbnailUrl1, "T175X228_"));
                array.Add(json);
            }
            result.Put("productlist", array);
            return new Result(ResultStatus.Success, result);
        }

        #endregion

        #region 退出
        [JsonRpcMethod("LogOut", Idempotent = false)]
        [JsonRpcHelp("退出")]
        public JsonObject LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove(Globals.SESSIONKEY_USER);
            Session.Clear();
            Session.Abandon();
            return new Result(ResultStatus.Success,"Success");
        }
        #endregion
    }
}