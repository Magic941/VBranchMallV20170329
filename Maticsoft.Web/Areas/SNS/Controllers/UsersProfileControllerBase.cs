using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Maticsoft.Model.SysManage;
using Maticsoft.Components.Setting;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Web.Components.Setting.SNS;
using Maticsoft.BLL.SNS;
namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public abstract class UsersProfileControllerBase : SNSControllerBase
    {
        #region 用到的变量
        protected Maticsoft.BLL.SNS.Posts PostsBll = new BLL.SNS.Posts();
        protected Maticsoft.Model.SNS.Posts PostsModel = new Model.SNS.Posts();
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        protected List<Maticsoft.ViewModel.SNS.Posts> list = new List<ViewModel.SNS.Posts>();
        Maticsoft.BLL.SNS.Products productBll = new Products();
        Maticsoft.BLL.SNS.UserBlog blogBll=new UserBlog();

        protected string NickName { get; set; }
        protected int UserID { get; set; }
        protected bool IsCurrentUser { set; get; }
        protected bool Activity = true;
        protected Model.SNS.EnumHelper.PostType DefaultPostType;
        protected Maticsoft.Model.Members.UsersExpModel UserModel;//如果是好友的主页，则存储好友的全部基本信息，如果是我的首页，则存储的是我的全部信息(喜欢数量等等)
        protected int FavBasePageSize = 6;
        protected int FavAllPageSize = 18;
        protected int _waterfallSize = 30;
        protected int _waterfallDetailCount = 1;
        protected int _PostPageSize = 10;
        #endregion

        public abstract bool LoadUserInfo(int UserID);
        public UsersProfileControllerBase()
        {
            this.ValidateRequest = false;
        }

        #region 动态

        public ActionResult Posts(string type, int? uid, string nickname)
        {
            #region 如果传过来的是用户的nickname，则对应相应的用户id
            Maticsoft.BLL.Members.Users UserBll = new BLL.Members.Users();
            int ID;
            if (!string.IsNullOrEmpty(nickname) && ((ID = UserBll.GetUserIdByNickName(nickname)) > 0))
            {
                uid = ID;
            }
            #endregion
            //#region 如果是当前的用户
            //if (currentUser != null && currentUser.UserID == uid)
            //{
            //    return RedirectToAction("Posts", "Profile");
            //}
            //#endregion
            ViewBag.IsCurrentUser = uid.HasValue ? false : (currentUser != null ? true : false);
            #region 进行对用户的id重写复制
            if (!LoadUserInfo(uid == null ? 0 : uid.Value) || !this.Activity)
            {
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    //SNS 主域
                    return Redirect("/Error/UserError");
                }
                return Redirect("/SNS/Error/UserError");
            }
            #endregion
            Maticsoft.BLL.SNS.AlbumType AlbumTypeBLL = new BLL.SNS.AlbumType();
            Maticsoft.ViewModel.SNS.PostsPage postPage = new ViewModel.SNS.PostsPage();
            Maticsoft.BLL.Members.PointsDetail pointBll = new BLL.Members.PointsDetail();
            postPage.Type = type;
            #region 初始化查询动态的类型
            if (!string.IsNullOrEmpty(type))
            {

                switch (type)
                {
                    case "user":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.User;
                        break;
                    case "all":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.All;
                        break;
                    case "referme":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.ReferMe;
                        break;
                    case "eachother":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.EachOther;
                        break;
                    case "photo":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.Photo;
                        break;
                    case "product":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.Product;
                        break;
                    case "video":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.Video;
                        break;
                    case "fellow":
                        DefaultPostType = Model.SNS.EnumHelper.PostType.Fellow;
                        break;
                }
            }
            else
            {
                if (this.IsCurrentUser == true)
                {
                    postPage.Type = "fellow";
                    DefaultPostType = Model.SNS.EnumHelper.PostType.Fellow;
                }
                else
                {
                    postPage.Type = "user";
                    DefaultPostType = Model.SNS.EnumHelper.PostType.User;
                }
            }
            #endregion
            postPage.PageSize = _PostPageSize;
            postPage.DataCount = PostsBll.GetCountByPostType(UserID, DefaultPostType,IncludeProduct);
            postPage.AlbumTypeList = AlbumTypeBLL.GetModelListByCache(Model.SNS.EnumHelper.Status.Enabled);
            postPage.UserID = UserID;
            postPage.Setting = Maticsoft.BLL.SNS.ConfigSystem.GetPostSetByCache();
            ViewBag.CurrentUserID = UserID;
            ViewBag.NickName = this.NickName;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            if (ViewBag.IsCurrentUser == true)
            {
                ViewBag.Title = "我的首页 - " + pageSetting.Title;
            }
            else
            {
                ViewBag.Title = this.NickName + "的首页 - " + pageSetting.Title;
            }
            #endregion
            return View("Posts", postPage);
        }
        #endregion

        #region 获取对应类型的数量

        public ActionResult GetDataCountByPageType(FormCollection fc)
        {

            this.UserID = Common.Globals.SafeInt(fc["UserID"], 0);
            this.DefaultPostType = GetDefaultPostType(fc["PostType"]);
            int DataCount = PostsBll.GetCountByPostType(UserID, DefaultPostType,IncludeProduct);
            return Content(DataCount.ToString());
        }
        #endregion

        #region 获取动态类型
        public Model.SNS.EnumHelper.PostType GetDefaultPostType(string type)
        {
            Model.SNS.EnumHelper.PostType PostType = Model.SNS.EnumHelper.PostType.User;
            if (!string.IsNullOrEmpty(type))
            {

                switch (type)
                {
                    case "user":
                        PostType = Model.SNS.EnumHelper.PostType.User;
                        break;
                    case "all":
                        PostType = Model.SNS.EnumHelper.PostType.All;
                        break;
                    case "referme":
                        PostType = Model.SNS.EnumHelper.PostType.ReferMe;
                        break;
                    case "eachother":
                        PostType = Model.SNS.EnumHelper.PostType.EachOther;
                        break;
                    case "photo":
                        PostType = Model.SNS.EnumHelper.PostType.Photo;
                        break;
                    case "product":
                        PostType = Model.SNS.EnumHelper.PostType.Product;
                        break;
                    case "video":
                        PostType = Model.SNS.EnumHelper.PostType.Video;
                        break;
                    case "fellow":
                        PostType = Model.SNS.EnumHelper.PostType.Fellow;
                        break;
                    case "blog":
                        PostType = Model.SNS.EnumHelper.PostType.Blog;
                        break;
                    default :
                        PostType = Model.SNS.EnumHelper.PostType.All;
                        break;

                }
            }
            return PostType;

        }

        #endregion

        #region AJax获取动态
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="PageIndex"></param>
        /// <returns></returns>
        public ActionResult AjaxGetPostByIndex(FormCollection Fm)
        {
            string type = Fm["type"];
            int UserId = Common.Globals.SafeInt(Fm["UserID"], 0);
            #region 动态类型
           DefaultPostType= GetDefaultPostType(type);
            #endregion

         
            int PageSize = _PostPageSize;
            int pageIndex = Common.Globals.SafeInt(Fm["pageIndex"], 0);
            list = PostsBll.GetPostByType(UserId, ViewModel.ViewModelBase.GetStartPageIndex(
                PageSize, pageIndex), ViewModel.ViewModelBase.GetEndPageIndex(PageSize, pageIndex), DefaultPostType,IncludeProduct);

            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
           
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                  
                    if (item.Post != null && item.Post.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product)
                    {
                        Maticsoft.Model.SNS.Products productModel = productBll.GetModelByCache(item.Post.TargetId);
                        if (IsStatic != "true")
                        {
                            item.Post.Description = item.Post.Description.Replace("{ProductUrl}", (ViewBag.BasePath + "Product/Detail/" + item.Post.TargetId));
                        }
                        else if (productModel!=null)
                        {
                            item.Post.Description = item.Post.Description.Replace("{ProductUrl}", (String.IsNullOrWhiteSpace(productModel.StaticUrl) ? ViewBag.BasePath + "Product/Detail/" + item.Post.TargetId : productModel.StaticUrl));
                        }
                    }
                    if (item.Post != null && item.Post.Type == (int) Maticsoft.Model.SNS.EnumHelper.PostContentType.Blog)
                    {
                        Maticsoft.Model.SNS.UserBlog blogModel = blogBll.GetModelByCache(item.Post.TargetId);
                        if (IsStatic != "true")
                        {
                            item.Post.Description = item.Post.Description.Replace("{BlogUrl}", (ViewBag.BasePath + "Blog/BlogDetail/" + item.Post.TargetId));
                        }
                        else if (blogModel != null)
                        {
                            item.Post.Description = item.Post.Description.Replace("{BlogUrl}", (String.IsNullOrWhiteSpace(blogModel.StaticUrl) ? ViewBag.BasePath + "Blog/BlogDetail/" + item.Post.TargetId : blogModel.StaticUrl));
                        }
                    }
                    if (item.OrigPost != null && item.OrigPost.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product)
                    {
                        Maticsoft.Model.SNS.Products productModel = productBll.GetModelByCache(item.OrigPost.TargetId);
                        if (IsStatic != "true")
                        {
                            item.OrigPost.Description = item.OrigPost.Description.Replace("{ProductUrl}", (ViewBag.BasePath + "Product/Detail/" + item.OrigPost.TargetId));
                        }
                        else if (productModel!=null)
                        {
                            item.OrigPost.Description = item.OrigPost.Description.Replace("{ProductUrl}", (String.IsNullOrWhiteSpace(productModel.StaticUrl) ? ViewBag.BasePath + "Product/Detail/" + item.OrigPost.TargetId : productModel.StaticUrl));
                        }
                    }

                    if (item.OrigPost != null && item.OrigPost.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Blog)
                    {
                        Maticsoft.Model.SNS.UserBlog blogModel = blogBll.GetModelByCache(item.OrigPost.TargetId);
                        if (IsStatic != "true")
                        {
                            item.OrigPost.Description = item.OrigPost.Description.Replace("{BlogUrl}", (ViewBag.BasePath + "Blog/BlogDetail/" + item.OrigPost.TargetId));
                        }
                        else if (blogModel != null)
                        {
                            item.OrigPost.Description = item.OrigPost.Description.Replace("{BlogUrl}", (String.IsNullOrWhiteSpace(blogModel.StaticUrl) ? ViewBag.BasePath + "Blog/BlogDetail/" + item.OrigPost.TargetId : blogModel.StaticUrl));
                        }
                    }
                }
            }
            #endregion
            if (currentUser != null)
            {
                ViewBag.CurrentUserID = currentUser.UserID;
            }
            return PartialView(CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", list);
        }
        #endregion

        #region 获取动态的评论

        public ActionResult AjaxGetComment(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments ComBll = new BLL.SNS.Comments();
            int PostId = Common.Globals.SafeInt(Fm["PostId"], 0);
            if (PostId > 0)
            {
                PostsModel = PostsBll.GetModel(PostId);
                List<Maticsoft.Model.SNS.Comments> list = ComBll.GetCommentByPost(PostsModel);
                if (list.Count > 0)
                {
                    return Content(jss.Serialize(list));
                }
            }
            return Content("No");
        }
        #endregion

        #region 获取评论根据positid
        public ActionResult AjaxGetCommentByPostId(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments ComBll = new BLL.SNS.Comments();
            int PostId = Common.Globals.SafeInt(Fm["PostId"], 0);
            if (PostId > 0)
            {
                PostsModel = PostsBll.GetModel(PostId);
                List<Maticsoft.Model.SNS.Comments> list = ComBll.GetCommentByPost(PostsModel);
                if (list.Count > 0)
                {
                    ViewBag.PostId = PostId;
                    return View(CurrentThemeViewPath + "/UserProfile/postCommentList.cshtml", list);
                }
            }
            return Content("No");
        }
        #endregion

        #region 检测当前用户是否登录
        public ActionResult CheckUserState()
        {
            if (currentUser != null)
            {
                return Content("Yes");
            }
            return Content("No");

        }

        public ActionResult CheckUserState4UserType()
        {
            if (currentUser != null)
            {
                return Content(currentUser.UserType == "AA" ? "Yes4AA" : "Yes");
            }
            return Content("No");

        }
        #endregion

        #region 喜欢

        /// <summary>
        /// 用户喜欢
        /// </summary>
        /// <returns></returns>
        public ActionResult Fav(int? uid, int? pageIndex)
        {
            if (!LoadUserInfo(uid == null ? 0 : uid.Value))
            {
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    //SNS 主域
                    return Redirect("/Account/Login");
                }
                return Redirect("/SNS/Account/Login");
            }
            int startIndex, endIndex, totalCount;
            pageIndex = pageIndex.HasValue ? pageIndex.Value : 1;
            startIndex = ViewModel.ViewModelBase.GetStartPageIndex(FavBasePageSize, pageIndex.Value);
            endIndex = startIndex + FavBasePageSize - 1;
            totalCount = UserModel.FavouritesCount.Value;
            ViewBag.WaterfallStartIndex = endIndex;
            ViewBag.WaterfallEndIndex = pageIndex.Value * FavAllPageSize > totalCount ? totalCount : pageIndex.Value * FavAllPageSize;
            ViewBag.UserId = this.UserID;
            ViewBag.RequestPage = this.GetType().Name == "UserController" ? "User" : "Profile";
            if (totalCount < 0)
            {
                return new EmptyResult();
            }
            Maticsoft.BLL.SNS.UserFavourite UfBll = new BLL.SNS.UserFavourite();
            Maticsoft.ViewModel.SNS.PhotoList models = new ViewModel.SNS.PhotoList();
            ViewBag.IsCurrentUser = this.IsCurrentUser;
            ViewBag.NickName = this.NickName;
            models.PhotoPagedList = UfBll.GetFavListByPage(this.UserID, "", startIndex, endIndex).ToPagedList(pageIndex ?? 1, FavBasePageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView(CurrentThemeViewPath + "/UserProfile/FavList.cshtml", models);
            }
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            if (ViewBag.IsCurrentUser == true)
            {
                ViewBag.Title = "我的喜欢 - " + pageSetting.Title;
            }
            else
            {
                ViewBag.Title = this.NickName + "的喜欢 - " + pageSetting.Title;
            }
            #endregion
            return View(CurrentThemeViewPath + "/UserProfile/FavIndex.cshtml", models);
        }

        #endregion

        #region 喜欢瀑布流
        [HttpPost]
        public ActionResult WaterfallFavListData(int UserID, int startIndex)
        {
            if (!LoadUserInfo(UserID))
            {
                return new EmptyResult();
            }
            int pageSize = FavAllPageSize;
            ViewBag.BasePageSize = FavBasePageSize;
            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount; ;
            int toalCount = 0;

            Maticsoft.BLL.SNS.UserFavourite UfBll = new BLL.SNS.UserFavourite();
            //获取总条数
            toalCount = UserModel.FavouritesCount.Value;
            if (toalCount < 1) return new EmptyResult();   //NO DATA
            Maticsoft.ViewModel.SNS.PhotoList models = new ViewModel.SNS.PhotoList();
            //分页获取数据
            models.PhotoListWaterfall = UfBll.GetFavListByPage(
                UserID, "", startIndex, endIndex);
            ViewBag.IsCurrentUser = this.IsCurrentUser;
            return View(CurrentThemeViewPath + "/UserProfile/FavListWaterfall.cshtml", models);

        }
        #endregion

        #region 获取用户的专辑
        public ActionResult Albums(int? uid, string IsFav)
        {
            if (!LoadUserInfo(uid == null ? 0 : uid.Value))
            {
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    //SNS 主域
                    return Redirect("/Account/Login");
                }
                return Redirect("/SNS/Account/Login");
            }
            Maticsoft.BLL.SNS.AlbumType AlbumTypeBLL = new BLL.SNS.AlbumType();
            BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();
            List<ViewModel.SNS.AlbumIndex> models = new List<ViewModel.SNS.AlbumIndex>();
            if (!string.IsNullOrEmpty(IsFav))
            {
                models = bllAlbums.GetUserFavAlbum(this.UserID);
                ViewBag.IsFav = true;
            }
            else
            {
                models = bllAlbums.GetListByUserId(this.UserID, UserAlbumDetailType);
            }
            #region 临时方案，
            ViewBag.AlbumTypeList = AlbumTypeBLL.GetModelListByCache(Model.SNS.EnumHelper.Status.Enabled);
            #endregion
            ViewBag.IsCurrentUser = this.IsCurrentUser;
            ViewBag.NickName = this.NickName;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            if (ViewBag.IsCurrentUser == true)
            {
                ViewBag.Title = "我的专辑 - " + pageSetting.Title;
            }
            else
            {
                ViewBag.Title = this.NickName + "的专辑 - " + pageSetting.Title;
            }
            #endregion
            return View(CurrentThemeViewPath + "/UserProfile/Albums.cshtml", models);
        }
        #endregion

        #region 用户的粉丝
        /// <summary>
        /// 用户的粉丝
        /// </summary>
        /// <returns></returns>
        public ActionResult Fans(int? uid, int? page)
        {
            if (!LoadUserInfo(uid == null ? 0 : uid.Value))
            {
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    //SNS 主域
                    return Redirect("/Account/Login");
                }
                return Redirect("/SNS/Account/Login");
            }
            Maticsoft.BLL.SNS.UserShip bllUserShip = new Maticsoft.BLL.SNS.UserShip();
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = UserModel.FansCount.HasValue ? UserModel.FansCount.Value : 0;

            PagedList<Maticsoft.Model.SNS.UserShip> FansList = null;
            int Cuid = (this.IsCurrentUser == false && currentUser != null) ? currentUser.UserID : 0;
            List<Maticsoft.Model.SNS.UserShip> list = bllUserShip.GetUsersAllFansByPage(this.UserID, startIndex, endIndex, Cuid);
            if (list != null && list.Count > 0)
            {
                FansList = new PagedList<Maticsoft.Model.SNS.UserShip>(list, page ?? 1, pagesize, toalcount);
            }
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserProfile/FansList.cshtml", FansList);
            ViewBag.IsCurrentUser = this.IsCurrentUser;
            ViewBag.UserId = this.UserID;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            if (ViewBag.IsCurrentUser == true)
            {
                ViewBag.Title = "我的粉丝 - " + pageSetting.Title;
            }
            else
            {
                ViewBag.Title = this.NickName + "的粉丝 - " + pageSetting.Title;
            }
            #endregion
            return View(CurrentThemeViewPath + "/UserProfile/Fans.cshtml", FansList);
        }
        #endregion

        #region 用户的关注
        /// <summary>
        /// 用户的关注
        /// </summary>
        /// <returns></returns>
        public ActionResult Fellows(int? uid, int? page)
        {
            if (!LoadUserInfo(uid == null ? 0 : uid.Value))
            {
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    //SNS 主域
                    return Redirect("/Account/Login");
                }
                return Redirect("/SNS/Account/Login");
            }
            Maticsoft.BLL.SNS.UserShip bllUserShip = new Maticsoft.BLL.SNS.UserShip();
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = UserModel.FellowCount.HasValue ? UserModel.FellowCount.Value : 0;
            int Cuid = (this.IsCurrentUser == false && currentUser != null) ? currentUser.UserID : 0;
            PagedList<Maticsoft.Model.SNS.UserShip> FellowsList = null;
            List<Maticsoft.Model.SNS.UserShip> list = bllUserShip.GetUsersAllFellowsByPage(this.UserID, startIndex, endIndex, Cuid);
            if (list != null && list.Count > 0)
            {
                FellowsList = new PagedList<Maticsoft.Model.SNS.UserShip>(list, page ?? 1, pagesize, toalcount);
            }
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserProfile/FellowsList.cshtml", FellowsList);
            ViewBag.IsCurrentUser = this.IsCurrentUser;
            ViewBag.UserId = this.UserID;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            if (ViewBag.IsCurrentUser == true)
            {
                ViewBag.Title = "我的关注 - " + pageSetting.Title;
            }
            else
            {
                ViewBag.Title = this.NickName + "的关注 - " + pageSetting.Title;
            }
            #endregion
            return View(CurrentThemeViewPath + "/UserProfile/Fellows.cshtml", FellowsList);
        }
        #endregion

        #region 用户小组用户功能
        public ActionResult Group(int? uid, int? page, int? Type)
        {
            if (!LoadUserInfo(uid == null ? 0 : uid.Value))
            {
                if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
                {
                    //SNS 主域
                    return Redirect("/Account/Login");
                }
                return Redirect("/SNS/Account/Login");
            }
            List<Maticsoft.Model.SNS.GroupTopics> ListTopic = new List<Model.SNS.GroupTopics>();
            Maticsoft.BLL.SNS.GroupTopics bll = new BLL.SNS.GroupTopics();
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            Type = Type.HasValue ? Type.Value : 0;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = bll.GetCountByType(this.UserID, (Maticsoft.Model.SNS.EnumHelper.UserGroupType)Type.Value);
            ListTopic = bll.GetUserRelativeTopicByType(this.UserID, (Maticsoft.Model.SNS.EnumHelper.UserGroupType)Type.Value, startIndex, endIndex);
            PagedList<Maticsoft.Model.SNS.GroupTopics> PagedTopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(ListTopic, page ?? 1, pagesize, toalcount);
            ViewBag.IsCurrentUser = this.IsCurrentUser;
            ViewBag.UserId = this.UserID;
            ViewBag.Type = (Maticsoft.Model.SNS.EnumHelper.UserGroupType)Type.Value;
            ViewBag.NickName = this.NickName;
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/UserProfile/TopicList.cshtml", PagedTopicList);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            if (ViewBag.IsCurrentUser == true)
            {
                ViewBag.Title = "我的小组 - " + pageSetting.Title;
            }
            else
            {
                ViewBag.Title = this.NickName + "的小组 - " + pageSetting.Title;
            }
            #endregion
            return View(CurrentThemeViewPath + "/UserProfile/Group.cshtml", PagedTopicList);
        }
        #endregion


        #region 检测是否是管理员操作

        public ActionResult CheckIsAdmin()
        {
            if (currentUser == null)
            {
                return Content("NoLogin");
            }
            if (currentUser.UserType != "AA")
            {
                return Content("False");
            }
            return Content("True");
        }
        #endregion


        
    }
}
