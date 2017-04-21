using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;
using Maticsoft.Accounts.Bus;
using Maticsoft.BLL.SNS;
using Maticsoft.Common;
using Maticsoft.Common.Video;
using Webdiyer.WebControls.Mvc;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Collections.Generic;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class UserCenterController : SNSUserControllerBase
    {
        private BLL.Members.UsersExp bllUE = new BLL.Members.UsersExp();
        private Maticsoft.BLL.Members.PointsDetail detailBll = new BLL.Members.PointsDetail();
        private Maticsoft.BLL.Members.SiteMessage bllSM = new BLL.Members.SiteMessage();
        private Maticsoft.BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
        private Maticsoft.BLL.Members.UserBind userBind = new BLL.Members.UserBind();

        #region 用户个人资料

        public ActionResult Personal()
        {
            ViewBag.Title = "个人资料";
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                 return RedirectToAction("Login", "Account");//去登录
            }
            else
            {
                Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
                if (null != model)
                {
                    return View(model);
                }
                else
                {
                     return RedirectToAction("Login", "Account");//去登录
                }
            }
        }
        public ActionResult Index()
        {
            return RedirectToAction("Personal");
        }
        #endregion 用户个人资料

        #region 更新用户信息

        /// <summary>
        /// 更新用户信息
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public void UpdateUserInfo(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
                Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
                if (null == model)
                {
                    RedirectToAction("Login");//去登录
                }
                else
                {
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
                    model.Constellation = collection["Constellation"];//星座
                    model.PersonalStatus = collection["PersonalStatus"];//职业
                    model.Singature = collection["Singature"];
                    model.Address = collection["Address"];
                    User currentUser = new Maticsoft.Accounts.Bus.User(CurrentUser.UserID);
                    currentUser.Sex = collection["Sex"];
                    currentUser.Email = collection["Email"];
                    currentUser.NickName = collection["NickName"];
                    currentUser.Phone = collection["Phone"];
                    if (currentUser.Update() && bllUE.UpdateUsersExp(model))
                    {
                        json.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                    Response.Write(json.ToString());
                }
            }
        }

        #endregion 更新用户信息

        #region 用户头像

        public ActionResult Gravatar()
        {
            ViewBag.Title = "修改头像";
            if (CurrentUser == null)
            {
                  return RedirectToAction("Login", "Account");//去登录
            }
            else
            {
                ViewBag.UserID = CurrentUser.UserID;
                return View("Gravatar");
            }
        }



        #endregion 用户头像

        #region 修改用户头像

        /// <summary>
        /// 修改用户头像
        /// </summary>
        [HttpPost]
        public ActionResult Gravatar(FormCollection collection)
        {
            ViewBag.Title = "修改头像";
            try
            {
                if (CurrentUser == null)
                {
                    return RedirectToAction("Login", "Account");//去登录
                }
                else
                {
                    Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
                    if (null != model)
                    {
                        model.Gravatar = collection["Gravatar"];
                        if (bllUE.UpdateUsersExp(model))//更新扩展信息  ,后期将更新头像独立一个方法。
                        {
                            return RedirectToAction("Personal");
                        }
                        else
                        {
                            return RedirectToAction("Personal");
                        }
                    }
                    else
                    {
                         return RedirectToAction("Login", "Account");//去登录
                    }
                }
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public void CutGravatar(FormCollection collection)
        {
            string savePath = "/" + MvcApplication.UploadFolder + "/User/Gravatar/";  //头像保存路径
            // string savePath = "/Upload/User/Gravatar/";
            if (String.IsNullOrWhiteSpace(collection["x"]) || String.IsNullOrWhiteSpace(collection["y"]) || String.IsNullOrWhiteSpace(collection["w"]) || String.IsNullOrWhiteSpace(collection["h"]) || String.IsNullOrWhiteSpace(collection["filename"]))
                return;
            int x = (int)Common.Globals.SafeDecimal(collection["x"], 0);//坐标点有可能是浮点型 比如 27.5
            int y = (int)Common.Globals.SafeDecimal(collection["y"], 0);
            int w = (int)Common.Globals.SafeDecimal(collection["w"], 0);
            int h = (int)Common.Globals.SafeDecimal(collection["h"], 0);
            string filename = collection["filename"];//Request["UploadPhoto"];
            int UserId = currentUser.UserID;
            try
            {
                byte[] image = Crop(filename, w, h, x, y);
                if (!Directory.Exists(Server.MapPath(savePath)))
                {
                    Directory.CreateDirectory(Server.MapPath(savePath));
                }
                FileStream f = new FileStream(Server.MapPath(savePath + UserId + ".jpg"), FileMode.Create);
                f.Write(image, 0, image.Length);
                f.Close();
                Response.Write("success");
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        public byte[] Crop(string Img, int Width, int Height, int X, int Y)
        {
            try
            {
                using (var OriginalImage = new Bitmap(Server.MapPath(Img)))
                {
                    using (var bmp = new Bitmap(Width, Height, OriginalImage.PixelFormat))
                    {
                        bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);
                        using (Graphics Graphic = Graphics.FromImage(bmp))
                        {
                            Graphic.SmoothingMode = SmoothingMode.AntiAlias;
                            Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, Width, Height), X, Y, Width, Height,
                                              GraphicsUnit.Pixel);
                            var ms = new MemoryStream();
                            bmp.Save(ms, OriginalImage.RawFormat);
                            return ms.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                throw (Ex);
            }
        }
        #endregion 修改用户头像

        #region 用户密码

        public ActionResult ChangePassword()
        {
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
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
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
        }

        #endregion 检查用户原密码

        #region 检查用户输入的昵称是否被其他用户使用

        /// <summary>
        ///检查用户输入的昵称是否被其他用户使用
        /// </summary>
        [HttpPost]
        public void CheckNickName(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
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
        }

        #endregion 检查用户输入的昵称是否被其他用户使用

        #region 更新用户密码

        /// <summary>
        /// 更新用户密码
        /// </summary>
        [HttpPost]
        public void UpdateUserPassword(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
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
        }

        #endregion 更新用户密码

        #region 用户域名

        /// <summary>
        /// 用户域名
        /// </summary>
        /// <returns></returns>
        public ActionResult Domain()
        {
            ViewBag.Title = "个性域名 - " + (ViewBag.SiteName);
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                  return RedirectToAction("Login", "Account");//去登录
            }
            Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
            if (null != model)
            {
                return View(model);
            }
            else
            {
                  return RedirectToAction("Login", "Account");//去登录
            }
        }

        #endregion 用户域名

        #region 更新用户域名

        /// <summary>
        /// 更新用户域名
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public void UpdateUserDomain(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
                Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
                if (null == model)
                {
                    RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
                }
                else
                {
                    model.HomePage = collection["Domain"];
                    if (bllUE.UpdateUsersExp(model))
                    {
                        json.Accumulate("STATUS", "SUCC");
                    }
                    else
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                    Response.Write(json.ToString());
                }
            }
        }

        #endregion 更新用户域名

        #region 检查用户输入的昵称是否存在

        /// <summary>
        ///检查用户输入的昵称是否存在
        /// </summary>
        [HttpPost]
        public void ExistsNickName(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
                string nickname = collection["NickName"];
                if (!string.IsNullOrWhiteSpace(nickname))
                {
                    BLL.Members.Users bll = new BLL.Members.Users();
                    if (bll.ExistsNickName(nickname))
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
                Response.Write(json.ToString());
            }
        }

        #endregion 检查用户输入的昵称是否存在

        #region 发站内信

        /// <summary>
        /// 发站内信
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMessage()
        {
            ViewBag.Title = "发信息";
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                  return RedirectToAction("Login", "Account");//去登录
            }
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
            ViewBag.Title = "发信息";
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                JsonObject json = new JsonObject();
                string nickname = collection["NickName"];
                string title = collection["Title"];
                string content = collection["Content"];
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
        }

        #endregion 发送站内信息

        #region 收件箱

        /// <summary>
        /// 收件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Inbox(int? page)
        {
            ViewBag.Title = "收件箱";
            if (CurrentUser == null)
            {
                  return RedirectToAction("Login", "Account");//去登录
            }

            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            int pagesize = 7;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllReceiveMsgListByMvcPage(CurrentUser.UserID, -1, pagesize, page.Value);
            foreach (Maticsoft.Model.Members.SiteMessage item in list)
            {
                if (item.ReceiverIsRead == false)
                {
                    bllSM.SetReceiveMsgAlreadyRead(item.ID);
                }
            }
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserCenter/InboxList.cshtml", list);

            return View(CurrentThemeViewPath + "/UserCenter/InBox.cshtml", list);
        }

        #endregion 收件箱

        #region 系统信息

        /// <summary>
        /// 系统信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SysInfo(int? page)
        {
            ViewBag.Title = "系统消息";
            if (CurrentUser == null)
            {
                  return RedirectToAction("Login", "Account");//去登录
            }

            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            int pagesize = 7;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSystemMsgListByMvcPage(CurrentUser.UserID, -1, currentUser.UserType, pagesize, page.Value);
            foreach (Maticsoft.Model.Members.SiteMessage item in list)
            {
                if (item.ReceiverIsRead == false)
                {
                    bllSM.SetSystemMsgStateToAlreadyRead(item.ID, currentUser.UserID, currentUser.UserType);
                }
            }
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/SysInfoList.cshtml", list);

            return View(CurrentThemeViewPath + "/UserCenter/SysInfo.cshtml", list);
        }

        #endregion 系统信息

        #region 发件箱

        /// <summary>
        /// 发件箱
        /// </summary>
        /// <returns></returns>
        public ActionResult Outbox(int? page)
        {
            ViewBag.Title = "发件箱";
            if (CurrentUser == null)
            {
                  return RedirectToAction("Login", "Account");//去登录
            }

            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            int pagesize = 10;
            PagedList<Maticsoft.Model.Members.SiteMessage> list = bllSM.GetAllSendMsgListByMvcPage(CurrentUser.UserID, pagesize, page.Value);
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserCenter/OutboxList.cshtml", list);

            return View(CurrentThemeViewPath + "/UserCenter/OutBox.cshtml", list);
        }

        #endregion 发件箱

        #region 积分明细

        public ActionResult PointsDetail(int pageIndex = 1)
        {
            ViewBag.Title = "积分明细";
            //首页用户数据
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (userEXModel != null)
            {
                ViewBag.UserInfo = userEXModel;
            }
            int _pageSize = 15;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = detailBll.GetRecordCount(" UserID=" + CurrentUser.UserID);
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
              List<Maticsoft.Model.Members.PointsDetail> detailList=detailBll.GetListByPageEX("UserID=" + CurrentUser.UserID, "", startIndex, endIndex);
            if(detailList!=null&&detailList.Count>0)
            {
                foreach(var item in detailList)
                {
                    item.RuleName = GetRuleName(item.RuleId);
                }
            }
            PagedList<Maticsoft.Model.Members.PointsDetail> lists = new PagedList<Maticsoft.Model.Members.PointsDetail>(detailList, pageIndex, _pageSize, toalCount);
            return View(lists);
        }

        public string GetRuleName(int RuleId)
        {
            Maticsoft.BLL.Members.PointsRule ruleBll = new BLL.Members.PointsRule();
            return ruleBll.GetRuleName(RuleId);
        }

        #endregion 积分明细

        #region 用户绑定

        public ActionResult TestWeiBo()
        {
            return View();
        }

        public ActionResult UserBind()
        {
            ViewBag.Title = "会员中心—帐号绑定";
            Maticsoft.ViewModel.UserCenter.UserBindList model = userBind.GetListEx(currentUser.UserID);
            return View(model);
        }

        [HttpPost]
        public void CancelBind(FormCollection collection)
        {
            if (!String.IsNullOrWhiteSpace(collection["BindId"]))
            {
                Response.ContentType = "application/text";
                int bindId = Common.Globals.SafeInt(collection["BindId"], 0);
                if (userBind.Delete(bindId))
                {
                    Response.Write("success");
                }
            }
        }

        #endregion 用户绑定

        #region 用户实名认证

        /// <summary>
        /// 用户实名认证
        /// </summary>
        /// <returns></returns>
        public ActionResult UserApprove(int? Id)
        {
            ViewBag.Title = "实名认证";

            BLL.Members.UsersApprove manage = new BLL.Members.UsersApprove();
            Model.Members.UsersApprove model = new Model.Members.UsersApprove();
            model = manage.GetModelByUserID(CurrentUser.UserID);
            if (model != null)
            {
                return Redirect(string.Format("/UserCenter/SubmitSucc/{0}", model.Status));
            }

            if (Id.HasValue)
            {
                if (Session["USERAPPROVE"] != null)
                {
                    model = (Model.Members.UsersApprove)Session["USERAPPROVE"];
                }
                ViewBag.UserID = CurrentUser.UserID;
                return View(model);
            }
            else
            {
                Session["USERAPPROVE"] = null;
                ViewBag.UserID = CurrentUser.UserID;
                model = new Model.Members.UsersApprove();
                model.UserID = CurrentUser.UserID;
                return View(model);
            }
        }

        #endregion 用户实名认证

        #region 用户提交实名认证信息

        /// <summary>
        /// 用户提交实名认证信息
        /// </summary>
        /// <param name="collection"></param>
        [HttpPost]
        public void UserApprove(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            string trueName = collection["TrueName"];
            string IdCard = collection["IdCardVal"];
            string perImage = collection["hiddenIdCardPreImage"];
            string nexImage = collection["hiddenIdCardNeImage"];
            if (!string.IsNullOrWhiteSpace(trueName) && !string.IsNullOrWhiteSpace(IdCard) && !string.IsNullOrWhiteSpace(perImage) && !string.IsNullOrWhiteSpace(nexImage))
            {
                Model.Members.UsersApprove model = new Model.Members.UsersApprove();
                model.UserID = CurrentUser.UserID;
                model.TrueName = trueName;
                model.IDCardNum = IdCard;
                model.FrontView = string.Format(perImage, "");
                model.RearView = string.Format(nexImage, "");
                model.Status = 0;
                model.UserType = 0;
                model.CreatedDate = DateTime.Now;
                Session["USERAPPROVE"] = model;
                json.Accumulate("STATUS", "SUCCESS");
            }
            else
            {
                json.Accumulate("STATUS", "FAILE");
            }
            Response.Write(json.ToString());
        }

        #endregion 用户提交实名认证信息

        #region 确认提交认证信息
        public ActionResult SubmitApprove()
        {
            ViewBag.Title = "实名认证--确认并提交";
            Model.Members.UsersApprove model = new Model.Members.UsersApprove();
            if (Session["USERAPPROVE"] != null)
            {
                model = (Model.Members.UsersApprove)Session["USERAPPROVE"];
                if (model != null)
                {
                    return View(model);
                }
                else
                {
                    return RedirectToAction("UserApprove", "UserCenter");
                   
                }
            }
            else
            {
                return RedirectToAction("UserApprove", "UserCenter");
            }
        }

        [HttpPost]
        public void SubmitApprove(FormCollection collection)
        {
            JsonObject json = new JsonObject();
            if (Session["USERAPPROVE"] != null)
            {
                BLL.Members.UsersApprove manage = new BLL.Members.UsersApprove();
                Model.Members.UsersApprove model = new Model.Members.UsersApprove();
                model = (Model.Members.UsersApprove)Session["USERAPPROVE"];

                //上传的图片移动文件夹
                string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));
                string ImageFile = "/Upload/SNS/Images/ApproveImage/";
                ArrayList imageList = new ArrayList();

                imageList.Add(model.FrontView.Replace(tempFile, ""));
                imageList.Add(model.RearView.Replace(tempFile, ""));

                model.FrontView = model.FrontView.Replace(tempFile, ImageFile);
                model.RearView = model.RearView.Replace(tempFile, ImageFile);
                if (manage.Add(model) > 0)
                {
                    Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);
                    Session["USERAPPROVE"] = null;
                    json.Accumulate("STATUS", "SUCCESS");
                }
                else
                {
                    json.Accumulate("STATUS", "FAILE");
                }
            }
            else
            {
                json.Accumulate("STATUS", "FAILE");
            }
            Response.Write(json.ToString());
        }
        #endregion

        #region 认证信息提交成功
        /// <summary>
        /// 认证信息提交成功
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult SubmitSucc(int? Id)
        {
            ViewBag.Title = "实名认证";
            if (Id.HasValue)
            {
                if (Id.Value == 1)
                {
                    ViewBag.Desc = "恭喜，您已通过实名认证";
                }
                else if (Id.Value == 0)
                {
                    ViewBag.Desc = "恭喜，您已成功提交实名认证信息，请耐心等待网站审核！";
                }
                else
                {
                    ViewBag.Falid = false;
                    ViewBag.Desc = "对不起，您提交实名认证信息不符相关规定，请重新提交！";
                }
            }
            else
            {
                Session["USERAPPROVE"] = null;
                ViewBag.Desc = "恭喜，您已成功提交实名认证信息，请耐心等待网站审核！";
            }
            return View();
        }
        #endregion

        #region 重新提交认证信息
        /// <summary>
        /// 重新提交认证信息
        /// </summary>
        [HttpPost]
        public void SubmitSucc()
        {
            ViewBag.Title = "实名认证";
            JsonObject json = new JsonObject();
            BLL.Members.UsersApprove manage = new BLL.Members.UsersApprove();
            if (manage.DeleteByUserId(CurrentUser.UserID))
            {
                json.Accumulate("STATUS", "SUCCESS");
            }
            else
            {
                json.Accumulate("STATUS", "FAILE");
            }
            Response.Write(json.ToString());
        }
        #endregion

        #region 检测视频方法


        #region 检测视频地址是否正确

        /// <summary>
        /// 更新用户域名
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public void CheckVideoUrl(FormCollection collection)
        {
            //如果是正确的，那就是视频的原始地址。
            string url = collection["VideoUrl"];
            JsonObject json = new JsonObject();

            if (string.IsNullOrWhiteSpace(url))
            {
                //视频地址不能为空！
                json.Accumulate("STATUS", "NotNull");
            }
            else
            {
                int videotype = GetType(url);
                if (videotype == 0)
                {
                    //粘贴的视频地址错误！
                    json.Accumulate("STATUS", "Error");
                }
                else
                {
                    if (videotype == 1)
                    {
                        YouKuInfo info = VideoHelper.GetYouKuInfo(url);
                        if (null != info)
                        {
                            json.Accumulate("STATUS", "Succ");

                            //视频播放地址
                            json.Accumulate("VideoUrl", string.Format("http://player.youku.com/player.php/sid/{0}/v.swf", info.VidEncoded));

                            //视频图片
                            json.Accumulate("ImageUrl", info.Logo);
                        }
                        else
                        {
                            //粘贴的视频地址错误！
                            json.Accumulate("STATUS", "Error");
                        }
                    }

                    if (videotype == 2)
                    {
                        Ku6Info info = VideoHelper.GetKu6Info(url);
                        if (null != info)
                        {
                            json.Accumulate("STATUS", "Succ");

                            //视频播放地址
                            json.Accumulate("VideoUrl", info.flash);

                            //视频图片
                            json.Accumulate("ImageUrl", info.coverurl);
                        }
                        else
                        {
                            //粘贴的视频地址错误！
                            json.Accumulate("STATUS", "Error");
                        }
                    }
                }
            }
            Response.Write(json.ToString());
            return;
        }

        #endregion 检测视频地址是否正确

        #region 获取视频类型

        /// <summary>
        /// 获取视频类型
        /// </summary>
        public int GetType(string url)
        {
            int type = 0;
            if (VideoHelper.IsYouKuVideoUrl(url))
            {
                type = 1;//优库视频地址
            }
            if (VideoHelper.IsKu6VideoUrl(url))
            {
                type = 2;//酷6视频
            }
            return type;
        }

        #endregion 获取视频类型

        #endregion 检测视频方法

        public ActionResult UserRecommand()
        {
            return View();
        }

        #region 个人标签

        public ActionResult Tags()
        {
            ViewBag.Title = "个人标签设置";
            if (currentUser != null)
            {
                Maticsoft.BLL.SNS.Tags bTags = new Tags();
                Maticsoft.BLL.SNS.TagType bTagType = new TagType();
                List<Maticsoft.Model.SNS.Tags> list = bTags.GetModelList("TypeId=" + bTagType.GetTagsTypeId("用户标签") + "");
                Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
                ViewBag.UserTags = model.Remark;
                return View(list);
            }
            return new EmptyResult();
        }
        public ActionResult AjaxAddTags(FormCollection fc)
        {
            string strTag = fc["tags"];
            if (currentUser != null && !string.IsNullOrEmpty(strTag))
            {
                Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
                model.Remark = string.IsNullOrEmpty(model.Remark) ? strTag : model.Remark + "," + strTag;
                if (bllUE.UpdateUsersExp(model))
                {
                    return Content("Ok");
                }
            }
            return new EmptyResult();
        }

        public ActionResult AjaxDeleTags(FormCollection fc)
        {
            string strTag = fc["tags"];
            if (currentUser != null && !string.IsNullOrEmpty(strTag))
            {
                Model.Members.UsersExpModel model = bllUE.GetUsersModel(CurrentUser.UserID);
                IEnumerable<string> TagsQuery =
                    from tags in model.Remark.Split(',')
                    where tags != strTag
                    select tags;
                model.Remark = string.Join(",", TagsQuery);
                if (bllUE.UpdateUsersExp(model))
                {
                    return Content("Ok");
                }
            }
            return new EmptyResult();
        }


        #endregion

    }
}
