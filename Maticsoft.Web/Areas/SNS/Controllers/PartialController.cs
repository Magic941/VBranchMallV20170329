using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Settings;
using Maticsoft.Json;
using Maticsoft.BLL.Ms;
using Maticsoft.BLL.SNS;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using System;
using Categories = Maticsoft.Model.SNS.Categories;
using EnumHelper = Maticsoft.Model.SNS.EnumHelper;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class PartialController : SNSControllerBase
    {
        private Maticsoft.BLL.SNS.Posts PostsBll = new BLL.SNS.Posts();
        protected List<Maticsoft.ViewModel.SNS.Posts> list = new List<ViewModel.SNS.Posts>();
        protected Maticsoft.BLL.SNS.UserShip bllUserShip = new BLL.SNS.UserShip();
        private Maticsoft.BLL.SNS.Categories cateBll = new Maticsoft.BLL.SNS.Categories();

        Maticsoft.BLL.SNS.Comments ComBll = new BLL.SNS.Comments();
        private int commentPagesize = 3;

        //
        // GET: /SNS/Partial/
        //int UserID = 67;

        public PartialController()
        {
            this.ValidateRequest = false;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region 网站头部
        /// <summary>
        /// 网站头部的分部视图
        /// </summary>
        /// <returns></returns>
        public ActionResult Header()
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.SNS);
            ViewBag.Logo = webSiteSet.LogoPath;
            ViewBag.WebName = webSiteSet.WebName;
            ViewBag.Domain = webSiteSet.WebSite_Domain;

            if (currentUser != null)
            {
                int syscount = 0;
                List<Maticsoft.Model.SNS.MsgTip> list = new List<Model.SNS.MsgTip>();
                Maticsoft.BLL.Members.SiteMessage msgBll = new BLL.Members.SiteMessage();
                Maticsoft.BLL.SNS.ReferUsers refBll = new BLL.SNS.ReferUsers();
                syscount = msgBll.GetSystemMsgNotReadCount(currentUser.UserID, -1, currentUser.UserType);
                if (syscount > 0)
                {
                    Maticsoft.Model.SNS.MsgTip sysmodel = new Model.SNS.MsgTip();
                    sysmodel.Count = syscount;
                    sysmodel._MsgType = (int)Maticsoft.Model.SNS.EnumHelper.MsgType.System;
                    list.Add(sysmodel);
                }
                int privatecount = 0;
                privatecount = msgBll.GetReceiveMsgNotReadCount(currentUser.UserID, -1);
                if (privatecount > 0)
                {
                    Maticsoft.Model.SNS.MsgTip privatemodel = new Model.SNS.MsgTip();
                    privatemodel.Count = privatecount;
                    privatemodel._MsgType = (int)Maticsoft.Model.SNS.EnumHelper.MsgType.Private;
                    list.Add(privatemodel);
                }
                int referecount = 0;
                referecount = refBll.GetReferNotReadCountByType(currentUser.UserID, (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post);
                if (referecount > 0)
                {
                    Maticsoft.Model.SNS.MsgTip refermodel = new Model.SNS.MsgTip();
                    refermodel.Count = referecount;
                    refermodel._MsgType = (int)Maticsoft.Model.SNS.EnumHelper.MsgType.Refer;
                    list.Add(refermodel);
                }
                ViewBag.Current = currentUser;
                ViewBag.Pointer = Common.Globals.SafeInt(Request.QueryString["pointer"], 0);
                return View("_UserHeader", list);
            }
            return View("_Header");
        }
        #endregion 网站头部

        #region 网站底部
        /// <summary>
        /// 网站底部的分部视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Footer()
        {
            return PartialView("_Footer");
        }

        #endregion 网站底部

        #region  友情链接
        /// <summary>
        /// 网站底部的分部视图
        /// </summary>
        /// <returns></returns>
        public ViewResult FLink(int FLinkType = 1, int FLinkTop = 8, string ViewName = "_FLink")
        {
            BLL.Settings.FriendlyLink bll = new BLL.Settings.FriendlyLink();
            List<Maticsoft.Model.Settings.FriendlyLink> list = bll.GetModelList(FLinkTop, FLinkType);
            return View(ViewName, list);
        }
        #endregion

        #region 网站导航

        /// <summary>
        /// 导航的分部视图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult Navigation()
        {
            Maticsoft.BLL.Settings.MainMenus meneBll = new BLL.Settings.MainMenus();
            List<Maticsoft.Model.Settings.MainMenus> NavList = meneBll.GetMenusByArea(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, MvcApplication.ThemeName);
            return PartialView("_Navigation", NavList);
        }

        #endregion 网站导航

        #region 获取用户的信息

        /// <summary>
        /// 用户中心及其导航条
        /// </summary>
        /// <returns></returns>
        public PartialViewResult UserInfo(int uid = -1, string nickname = "")
        {
            Maticsoft.BLL.Members.Users UserBll = new BLL.Members.Users();
            Maticsoft.Model.Members.UsersExpModel UserModel = new Model.Members.UsersExpModel();
            Maticsoft.BLL.Ms.Regions RegionBll = new BLL.Ms.Regions();
            Maticsoft.BLL.SNS.Star starManage = new BLL.SNS.Star();
            int ID;
            if (!string.IsNullOrEmpty(nickname) && ((ID = UserBll.GetUserIdByNickName(nickname)) > 0))
            {
                uid = ID;
            }
            //是否是发表动态页面
            ViewBag.IsPost = uid == -1;
            ViewBag.IsCurrentUser = false;
            uid = uid > -1 ? uid : currentUser.UserID;
            if (currentUser != null && uid == currentUser.UserID)
            {
                ViewBag.IsCurrentUser = true;
            }


            UserModel = GetUserModel(uid);
            string strAddress = RegionBll.GetRegionNameByRID(Common.Globals.SafeInt(UserModel.Address, 0));
            if (strAddress.Contains("北京北京"))
            {
                strAddress = strAddress.Replace("北京北京", "北京");
            }
            else if (strAddress.Contains("上海上海"))
            {
                strAddress = strAddress.Replace("上海上海", "上海");
            }
            else if (strAddress.Contains("重庆重庆"))
            {
                strAddress = strAddress.Replace("重庆重庆", "重庆");
            }
            else if (strAddress.Contains("天津天津"))
            {
                strAddress = strAddress.Replace("天津天津", "天津");
            }
            UserModel.Address = string.IsNullOrEmpty(UserModel.Address) ? "暂未设置" : strAddress;
            //是否是达人
            ViewBag.IsStar = starManage.IsStar(uid);
            //用户等级
            BLL.SNS.GradeConfig manage = new BLL.SNS.GradeConfig();
            ViewBag.Level = manage.GetUserLevel(UserModel.Points);

            return PartialView("_UserInfo", UserModel);
        }

        public Maticsoft.Model.Members.UsersExpModel GetUserModel(int UserID)
        {
            Maticsoft.BLL.Members.UsersExp UserExBll = new BLL.Members.UsersExp();
            Maticsoft.Model.Members.UsersExpModel UserExModel = new Model.Members.UsersExpModel();
            UserExModel = UserExBll.GetUsersModel(UserID);
            return UserExModel;
        }

        #endregion 获取用户的信息

        #region 关注某人

        /// <summary>
        /// 进行关注某人
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxFellow(FormCollection Fm)
        {
            int FellowUserId = Common.Globals.SafeInt(Fm["FellowUserId"], 0);
            if (FellowUserId == 0 || currentUser == null)
            {
                return Content("No");
            }
            if (bllUserShip.FellowUser(currentUser.UserID, FellowUserId))
            {
                return Content("Ok");
            }
            return Content("No");
        }

        #endregion 关注某人

        #region 取消关注

        /// <summary>
        ///取消关注
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxUnFellow(FormCollection Fm)
        {
            int UnFellowUserId = Common.Globals.SafeInt(Fm["UnFellowUserId"], 0);
            if (UnFellowUserId == 0 || currentUser == null)
            {
                return Content("No");
            }
            if (bllUserShip.UnFellowUser(currentUser.UserID, UnFellowUserId))
            {
                return Content("Ok");
            }
            return Content("No");
        }

        #endregion 取消关注

        #region 用户中心右侧小组和粉丝

        /// <summary>
        /// 用户中心右边信息的分布试图
        /// </summary>
        /// <returns></returns>
        public PartialViewResult ProfileLeft(int? uid)
        {
            Maticsoft.BLL.SNS.UserShip shipBll = new BLL.SNS.UserShip();
            Maticsoft.BLL.Members.UsersExp userBll = new BLL.Members.UsersExp();
            Maticsoft.Model.Members.UsersExpModel userModel = new Model.Members.UsersExpModel();
            Maticsoft.BLL.SNS.Groups groupBll = new BLL.SNS.Groups();
            int UserId = uid.HasValue ? uid.Value : (currentUser != null ? currentUser.UserID : 0);
            List<Maticsoft.Model.SNS.UserShip> ListShip = shipBll.GetToListByFansPage(UserId, "", 0, 9);
            List<Maticsoft.Model.SNS.Groups> ListGroupJoin = groupBll.GetUserJoinGroup(UserId, 9);
            List<Maticsoft.Model.SNS.Groups> ListGroupCreate = groupBll.GetModelList("CreatedUserId=" + UserId + "");
            userModel = userBll.GetUsersExpModel(UserId);
            ViewBag.FansCount = userModel != null ? userModel.FansCount : 0;
            ViewBag.IsCurrentUser = uid.HasValue ? false : (currentUser != null ? true : false);
            ViewBag.UserId = UserId;
            Maticsoft.ViewModel.SNS.ProfileLeft model = new ViewModel.SNS.ProfileLeft();
            model.joingroupList = ListGroupJoin;
            model.shipList = ListShip;
            model.creategroupList = ListGroupCreate;
            return PartialView("_ProfileLeft", model);
        }

        #endregion 用户中心右侧小组和粉丝

        #region 个人中心右侧信息

        /// <summary>
        /// 个人中心右侧信息
        /// </summary>
        /// <returns></returns>
        public PartialViewResult SelfRight()
        {
            Maticsoft.BLL.Members.UsersExp userBll = new BLL.Members.UsersExp();
            Maticsoft.Model.Members.UsersExpModel userModel = new Model.Members.UsersExpModel();
            Maticsoft.BLL.SNS.Groups groupBll = new BLL.SNS.Groups();
            BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();
            Maticsoft.ViewModel.SNS.SelfRight model = new ViewModel.SNS.SelfRight();
            model.MyGroups = groupBll.GetUserJoinGroup(currentUser.UserID, 9);

            Maticsoft.BLL.Ms.Regions RegionBll = new BLL.Ms.Regions();
            Maticsoft.BLL.SNS.Star starManage = new BLL.SNS.Star();


            model.UserInfo = GetUserModel(currentUser.UserID);
            string strAddress = RegionBll.GetRegionNameByRID(Common.Globals.SafeInt(model.UserInfo.Address, 0));
            if (strAddress.Contains("北京北京"))
            {
                strAddress = strAddress.Replace("北京北京", "北京");
            }
            else if (strAddress.Contains("上海上海"))
            {
                strAddress = strAddress.Replace("上海上海", "上海");
            }
            else if (strAddress.Contains("重庆重庆"))
            {
                strAddress = strAddress.Replace("重庆重庆", "重庆");
            }
            else if (strAddress.Contains("天津天津"))
            {
                strAddress = strAddress.Replace("天津天津", "天津");
            }
            model.UserInfo.Address = string.IsNullOrEmpty(model.UserInfo.Address) ? "暂未设置" : strAddress;
            Maticsoft.BLL.SNS.AlbumType AlbumTypeBLL = new BLL.SNS.AlbumType();

            model.MyAlbum = bllAlbums.GetListByUserId(currentUser.UserID, UserAlbumDetailType);

            return PartialView("_SelfRight", model);
        }

        #endregion 用户中心右侧小组和粉丝

        #region 动态分页

        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult AjaxGetPostByIndex(FormCollection Fm)
        {
            Model.SNS.EnumHelper.PostType PostType = Model.SNS.EnumHelper.PostType.All;
            string type = Fm["type"];
            int UserId = Common.Globals.SafeInt(Fm["UserID"], 0);
            if (!string.IsNullOrEmpty(type))
            {
                switch (type)
                {
                    case "user":
                        PostType = Model.SNS.EnumHelper.PostType.User;
                        break;

                    case "fellow":
                        PostType = Model.SNS.EnumHelper.PostType.Fellow;
                        break;

                    case "referme":
                        PostType = Model.SNS.EnumHelper.PostType.ReferMe;
                        break;
                    default:
                        PostType = Model.SNS.EnumHelper.PostType.All;
                        break;
                }
            }
            string PageSizeStr = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PostPageSize");
            int PageSize = Common.Globals.SafeInt(PageSizeStr, 10);
            int pageIndex = Common.Globals.SafeInt(Fm["pageIndex"], 0);
            list = PostsBll.GetPostByType(UserId, ViewModel.ViewModelBase.GetStartPageIndex(PageSize, pageIndex), ViewModel.ViewModelBase.GetEndPageIndex(PageSize, pageIndex), PostType, IncludeProduct);
            return PartialView("LoadPostData", list);
        }

        #endregion 动态分页

        #region 添加关注

        /// <summary>
        /// 添加关注
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public void AddAttention(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction("Login");//去登录
                return;
            }
            JsonObject json = new JsonObject();
            string passiveuserid = collection["PassiveUserID"];
            if (!string.IsNullOrWhiteSpace(passiveuserid))
            {
                if (PageValidate.IsNumberSign(passiveuserid) && bllUserShip.AddAttention(CurrentUser.UserID, int.Parse(passiveuserid)))
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
                json.Accumulate("STATUS", "FAIL");
            }
            Response.Write(json.ToString());
        }

        #endregion 添加关注

        #region 取消关注

        /// <summary>
        /// 取消关注
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public void CancelAttention(FormCollection collection)
        {
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
            {
                RedirectToAction("Login");//去登录
                return;
            }
            JsonObject json = new JsonObject();
            string passiveuserid = collection["PassiveUserID"];
            if (!string.IsNullOrWhiteSpace(passiveuserid))
            {
                if (PageValidate.IsNumberSign(passiveuserid) && bllUserShip.CancelAttention(CurrentUser.UserID, int.Parse(passiveuserid)))
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
                json.Accumulate("STATUS", "FAIL");
            }
            Response.Write(json.ToString());
        }

        #endregion 取消关注

        #region 用户浮动层

        public ActionResult AjaxUserInfo(int? UserID, string NickName)
        {
            Maticsoft.BLL.Members.Users UserBll = new BLL.Members.Users();
            Maticsoft.BLL.Members.UsersExp UserExBll = new BLL.Members.UsersExp();
            Maticsoft.Model.Members.UsersExpModel UserExModel = new Model.Members.UsersExpModel();
            int ID;
            if (!string.IsNullOrEmpty(NickName))
            {
                if ((ID = UserBll.GetUserIdByNickName(NickName)) > 0)
                {
                    UserID = ID;
                }
                else
                {
                    return View("_AjaxUserInfo", UserExModel);
                }
            }
            if (UserID.HasValue)
            {
                UserExModel = UserExBll.GetUsersModel(UserID.Value);
                Maticsoft.BLL.Ms.Regions RegionBll = new BLL.Ms.Regions();
                string strAddress = RegionBll.GetRegionNameByRID(Common.Globals.SafeInt(UserExModel.Address, 0));
                if (strAddress.Contains("北京北京"))
                {
                    strAddress = strAddress.Replace("北京北京", "北京");
                }
                else if (strAddress.Contains("上海上海"))
                {
                    strAddress = strAddress.Replace("上海上海", "上海");
                }
                else if (strAddress.Contains("重庆重庆"))
                {
                    strAddress = strAddress.Replace("重庆重庆", "重庆");
                }
                else if (strAddress.Contains("天津天津"))
                {
                    strAddress = strAddress.Replace("天津天津", "天津");
                }
                UserExModel.Address = string.IsNullOrEmpty(UserExModel.Address) ? "暂未设置" : strAddress;
            }
            Maticsoft.BLL.SNS.UserShip shipBll = new BLL.SNS.UserShip();
            if (currentUser != null)
            {
                if (currentUser.UserID == UserID.Value)
                {
                    ViewBag.IsSelf = true;
                }
                else
                {
                    if (shipBll.Exists(currentUser.UserID, UserID.Value))
                    {
                        ViewBag.IsFellow = true;
                    }
                }
            }
            return View("_AjaxUserInfo", UserExModel);
        }

        #endregion 用户浮动层
         
        #region 三方登录相关

        public ActionResult AjaxLogin()
        {
            //三方登录Key
            return View();
        }
      

        #endregion 三方登录相关  


        #region 省份信息和热门城市
        public ActionResult Province()
        {
            //三方登录Key
            Maticsoft.BLL.Ms.Regions regionBll = new Regions();
            List<Maticsoft.Model.Ms.Regions> regionList = regionBll.GetProvinceList();
            return PartialView("_Province", regionList);
        }


        public ActionResult HotCity()
        {
            //三方登录Key
            Maticsoft.BLL.Ms.RegionRec recBll = new RegionRec();
            List<Maticsoft.Model.Ms.RegionRec> regionRecList = recBll.GetRecCityList(0);
            return PartialView("_HotCity", regionRecList);
        }
        #endregion

        public ActionResult WeiBoBind()
        {
            Maticsoft.BLL.Members.UserBind userBind = new BLL.Members.UserBind();
            if (currentUser != null)
            {
                Maticsoft.ViewModel.UserCenter.UserBindList model = userBind.GetListEx(currentUser.UserID);
                return View("_WeiBoBind", model);
            }
            return new EmptyResult();
        }

        //
        // GET: /SNS/Article/ArticleList/5

        public ActionResult ArticleList(int id=1)
        {
           // int id = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("SNSHelpCenter"), 1);
            BLL.CMS.Content bll = new BLL.CMS.Content();
            List<Model.CMS.Content> list = bll.GetModelList(id);
            return View(list);
        }


        public ActionResult StarDPI(int? UserId)
        {
            if (UserId.HasValue)
            {
                Maticsoft.BLL.Members.UsersExp UserBll = new BLL.Members.UsersExp();
                Maticsoft.Model.Members.UsersExpModel UserModel = UserBll.GetUsersExpModelByCache(UserId.Value);
                if (UserModel != null && UserModel.IsUserDPI)
                {
                    ViewBag.IsUserDPI = true;
                }
                else
                {
                    ViewBag.IsUserDPI = false;
                }
            }
            else
            {
                ViewBag.IsUserDPI = false;
            }
            return View("_StarDPI");
        }

        public ActionResult UserRecommand()
        {

            return View();
        }


        /// <summary>
        /// SNS用户中心，左侧菜单
        /// </summary>
        /// <returns></returns>
        public ActionResult LeftMenu()
        {
            if (CurrentUser == null)
            {
                return RedirectToAction(ViewBag.BasePath + "Account/Login");//去登录
            }
            else
            {
                ViewBag.UserInfo = CurrentUser;
                return View();
            }
        }

        public ActionResult Search(string ViewName = "_Search")
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.SNS);
            ViewBag.Logo = webSiteSet.LogoPath;
            return View(ViewName);
        }

        #region Aiax 方法
        public ActionResult GetCurrentUser()
        {
            if (currentUser == null)
            {
                return Content("No");
            }
            else
            {
                Maticsoft.BLL.Members.SiteMessage msgBll = new BLL.Members.SiteMessage();
                Maticsoft.BLL.SNS.ReferUsers refBll = new BLL.SNS.ReferUsers();
                int syscount = msgBll.GetSystemMsgNotReadCount(currentUser.UserID, -1, currentUser.UserType);
                int privatecount = msgBll.GetReceiveMsgNotReadCount(currentUser.UserID, -1);
                int referecount = refBll.GetReferNotReadCountByType(currentUser.UserID, (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post);
                string name = String.IsNullOrWhiteSpace(currentUser.NickName) ? currentUser.UserName : currentUser.NickName;
                return Content(name + "|" + currentUser.UserID + "|" + syscount + "|" + privatecount + "|" + referecount);
            }
        }


        #region 添加评论
        /// <summary>
        ///添加评论
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            if (currentUser == null)
            {
                return null;
            }
            Maticsoft.Model.SNS.Comments ComModel = new Model.SNS.Comments();
            int TargetId = Common.Globals.SafeInt(Fm["TargetId"], 0);
            int CommentType = 0;
            switch (Fm["Type"])
            {
                case "Video":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal;
                    break;
                case "Product":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product;
                    break;
                case "Photo":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo;
                    break;
                case "Blog":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Blog;
                    break;
                default:
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal;
                    break;
            }
            int CommentId = 0;

            string Des = Maticsoft.Common.Globals.HtmlEncode(Fm["Des"]);
            Des = ViewModel.ViewModelBase.ReplaceFace(Des);
            ComModel.CreatedDate = DateTime.Now;
            ComModel.CreatedNickName = currentUser.NickName;
            ComModel.CreatedUserID = currentUser.UserID;
            ComModel.Description = Des;
            ComModel.HasReferUser = Des.Contains("@") ? true : false;
            ComModel.IsRead = false;
            ComModel.ReplyCount = 0;
            ComModel.TargetId = TargetId;
            ComModel.Type = CommentType;
            ComModel.UserIP = Request.UserHostAddress;
            if ((CommentId = ComBll.AddEx(ComModel)) > 0)
            {
                ComModel.CommentID = CommentId;
                // ComModel.Description = ViewModel.ViewModelBase.RegexNickName(ComModel.Description);
                List<Maticsoft.Model.SNS.Comments> list = new List<Model.SNS.Comments>();
                //是否含有审核词
                if (!Maticsoft.BLL.Settings.FilterWords.ContainsModWords(ComModel.Description))
                {
                    list.Add(ComModel);
                }
                return PartialView("_TargetComment", list);

            }
            return Content("No");

        }
        #endregion

        #region 获取评论
        public ActionResult AjaxGetCommentsByType(string type, int pid, int? PageIndex)
        {
            int StartIndex = Maticsoft.ViewModel.ViewModelBase.GetStartPageIndex(commentPagesize, PageIndex.Value);
            int EndIndex = Maticsoft.ViewModel.ViewModelBase.GetEndPageIndex(commentPagesize, PageIndex.Value);
            int CommentType = 0;
            switch (type)
            {
                case "Video":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal;
                    break;
                case "Product":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product;
                    break;
                case "Photo":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo;
                    break;
                case "Blog":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Blog;
                    break;
                default:
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal;
                    break;
            }

            List<Maticsoft.Model.SNS.Comments> CommentList = ComBll.GetCommentByPage(CommentType, pid, StartIndex, EndIndex);
            return PartialView("_TargetComment", CommentList);

        }
        #endregion

        #region 获取评论数
        public ActionResult AjaxGetCommentsCount(string type, int pid)
        {
            int CommentType = 0;
            switch (type)
            {
                case "Video":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal;
                    break;
                case "Product":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product;
                    break;
                case "Photo":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo;
                    break;
                case "Blog":
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Blog;
                    break;
                default:
                    CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal;
                    break;
            }

            int count = ComBll.GetCommentCount(CommentType, pid);
            return Content(count.ToString());

        }
        #endregion


        #endregion
        #region 百度分享脚本
        public ActionResult BaiduShare()
        {
            ViewBag.BaiduUid = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("BaiduShareUserId");
            return View("_BaiduShare");
        }
        #endregion

        public PartialViewResult CategoryList(int Cid = 0, int Top = 5, int Type = 0, string ViewName = "_SonList")
        {
            List<Maticsoft.Model.SNS.Categories> CategoryList = cateBll.GetChildList(Cid, Top, Type);
            return PartialView(ViewName, CategoryList);
        }

        #region 社会化媒体登录
        public PartialViewResult MediaLogin(string ViewName = "_MediaLogin")
        {
            return PartialView(ViewName);
        }
        #endregion

        #region 广告位

        public PartialViewResult IndexAd(int id = 22, string ViewName = "_IndexAd")
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Advertisement();
            List<Maticsoft.Model.Settings.Advertisement> list = bll.GetListByAidCache(id);
            return PartialView(ViewName, list);
        }

        public PartialViewResult ImageAd(int id = 23, string ViewName = "_ImageAd1")
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Advertisement();
            List<Maticsoft.Model.Settings.Advertisement> list = bll.GetListByAidCache(id);
            return PartialView(ViewName, list);
        }

        public PartialViewResult AdDetail(int id, string ViewName = "_AdDetail")
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Advertisement();
            List<Maticsoft.Model.Settings.Advertisement> list = bll.GetListByAidCache(id);
            return PartialView(ViewName, list);
        }

        #endregion

        #region 判断是否含有禁用词
         [ValidateInput(false)]
        public ActionResult ContainsDisWords()
        {
            string desc = Request.Params["Desc"];
            return Maticsoft.BLL.Settings.FilterWords.ContainsDisWords(desc) ? Content("True") : Content("False");
        }
        #endregion


        #region 图片
        public PartialViewResult PhotoPart(int Top = 5, string viewName = "_PhotoPart")
        {
            Maticsoft.BLL.SNS.Photos photoBll = new Photos();
            List<Maticsoft.Model.SNS.Photos> photoList = photoBll.GetTopPhotoList(Top, -1);
            List<Maticsoft.Model.SNS.Photos> list = new List<Model.SNS.Photos>();
            
            return PartialView(viewName, photoList);
        }
        #endregion

        #region 热门关键字
        public PartialViewResult HotKeyword(string ViewName = "_HotKeyword")
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.SNS);
            ViewBag.Logo = webSiteSet.LogoPath;
            Maticsoft.BLL.SNS.HotWords keywordBll = new HotWords();
            List<Maticsoft.Model.SNS.HotWords> keywords = keywordBll.GetModelList("");
            return PartialView(ViewName, keywords);
        }
        #endregion

    }
}