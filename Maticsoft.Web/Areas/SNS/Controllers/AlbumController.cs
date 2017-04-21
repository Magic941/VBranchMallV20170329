using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.SNS;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewModel.SNS;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;
using Maticsoft.BLL.Ms;

namespace Maticsoft.Web.Areas.SNS.Controllers
{


    public class AlbumController : SNSControllerBase
    {

        BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();
        BLL.Members.UsersExp blluser = new BLL.Members.UsersExp();
        BLL.SNS.Products productBll = new BLL.SNS.Products();
        BLL.SNS.Photos photoBll = new Photos();
        private int _basePageSize = 6;
        private int _waterfallSize = 30;
        private int _waterfallDetailCount = 1;
        private int _commentPageSize = 8;
        public AlbumController()
        {
            this._basePageSize = FallInitDataSize;
            this._waterfallSize = FallDataSize;
            this._commentPageSize = CommentDataSize;

        }

        #region 专辑首页
        //GET: /SNS/Album/
        public ActionResult Index()
        {
            //List<ViewModel.SNS.AlbumIndex> models = bllAlbums.GetListForIndex(1);
            //循环专辑类型
            BLL.SNS.AlbumType bllAT = new BLL.SNS.AlbumType();
            List<Maticsoft.Model.SNS.AlbumType> models = bllAT.GetIndexList();

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Ablum", ApplicationKeyType.SNS);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            return View(models);
        }
        // [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult TypeIndex(int AlbumType, int? pageIndex)
        {
            BLL.SNS.AlbumType bllAlbumType = new AlbumType();
            Model.SNS.AlbumType albumType = bllAlbumType.GetModel(AlbumType);
            if (albumType == null) return RedirectToAction("Index");

            ViewBag.TypeName = albumType.TypeName;

            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("AblumList", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, albumType.TypeName });  //专辑分类名称
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;

            BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();

            //获取总条数
            toalCount = bllAlbums.GetRecordCount(AlbumType);

            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex.Value * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;

            if (toalCount < 1) return View();   //NO DATA

            PagedList<ViewModel.SNS.AlbumIndex> models = bllAlbums.GetListForPage(AlbumType, "",
                                                 startIndex, endIndex, UserAlbumDetailType).ToPagedList(
                                                    pageIndex ?? 1,
                                                    pageSize,
                                                    toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("AlbumList", models);




            return View(models);
        }

        [HttpPost]
        // [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult WaterfallAlbumListData(int AlbumType, int startIndex)
        {
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;

            BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();

            //获取总条数
            toalCount = bllAlbums.GetRecordCount(AlbumType);
            if (toalCount < 1) return new EmptyResult();   //NO DATA
            //分页获取数据
            List<ViewModel.SNS.AlbumIndex> models = bllAlbums.GetListForPage(
                AlbumType, "", startIndex, endIndex, UserAlbumDetailType);

            return View("AlbumListWaterfall", models);

        }
        #endregion

        #region 专辑详细页面
        //[OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult Details(int AlbumID, int? pageIndex)
        {

            ViewModel.SNS.PhotoList albumDetail = new ViewModel.SNS.PhotoList();
            albumDetail.AlbumModel = bllAlbums.GetModel(AlbumID); //专辑信息
            if (albumDetail.AlbumModel == null || !bllAlbums.UpdatePvCount(AlbumID))
            {
                return RedirectToAction("Index");
            }
            albumDetail.UserModel = blluser.GetUsersExpModelByCache(albumDetail.AlbumModel.CreatedUserID);// 用户信息
            if (albumDetail.UserModel == null)
            {
                return RedirectToAction("Index");
            }

            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("AblumDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, albumDetail.AlbumModel.AlbumName },     //专辑名称
                new[] { PageSetting.RKEY_CTNAME, albumDetail.UserModel.NickName },      //创建者昵称
                new[] { PageSetting.RKEY_CDES, albumDetail.AlbumModel.Description });   //专辑描述
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;

            BLL.SNS.UserAlbumDetail bllAlbumDe = new BLL.SNS.UserAlbumDetail();

            //获取总条数

            toalCount = bllAlbumDe.GetRecordCount4AlbumImgByAlbumID(AlbumID, UserAlbumDetailType);

            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex.Value * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;
            albumDetail.AlbumsList = bllAlbums.GetUserAlbumsByUserId(10, albumDetail.UserModel != null ? albumDetail.UserModel.UserID : 0);
            if (toalCount < 1) return View(albumDetail);   //NO DATA

            //分页获取数据
            albumDetail.PhotoPagedList = bllAlbumDe.GetAlbumImgListByPage(
                AlbumID, startIndex, endIndex, UserAlbumDetailType).ToPagedList(
                                                    pageIndex ?? 1,
                                                    pageSize,
                                                    toalCount);

            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (var item in albumDetail.PhotoPagedList)
            {
                if (IsStatic != "true")
                {
                    item.StaticUrl = ViewBag.BasePath + (item.Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product ? "Product/" : "Photo/") + "Detail/" + item.TargetId + "?AlbumId=" + AlbumID;
                }
                else
                {
                    string StaticUrl = "";
                    if (item.Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product)
                    {
                        Maticsoft.Model.SNS.Products productModel = productBll.GetModelByCache(item.TargetId);
                        StaticUrl = (productModel != null && !String.IsNullOrWhiteSpace(productModel.StaticUrl)) ? productModel.StaticUrl : (ViewBag.BasePath + "Product/Detail/" + item.TargetId);
                    }
                    else
                    {
                        Maticsoft.Model.SNS.Photos photoModel = photoBll.GetModelByCache((int)item.TargetId);
                        StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.TargetId + "?AlbumId=" + AlbumID);
                    }
                    item.StaticUrl = StaticUrl;
                }
            }
            #endregion
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("PhotoList", albumDetail);
            //下面是评论无刷新评论需要的数据，如果是无刷新分页，所以下面的数据没必要查询，所以放在(Request.IsAjaxRequest()判断的下面
            albumDetail.CommentPageSize = _commentPageSize;
            Maticsoft.BLL.SNS.Comments comBll = new Comments();
            albumDetail.CommentCount = comBll.GetCommentCount((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Album, AlbumID);
            if (currentUser != null && albumDetail.AlbumModel != null && albumDetail.AlbumModel.CreatedUserID == currentUser.UserID)
            {
                ViewBag.IsCurrentUser = true;
            }





            return View(albumDetail);
        }

        [HttpPost]
        // [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult PhotoListWaterfall(int albumID, int startIndex)
        {
            ViewModel.SNS.PhotoList albumDetail = new ViewModel.SNS.PhotoList();

            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;

            BLL.SNS.UserAlbumDetail bllAlbumDe = new BLL.SNS.UserAlbumDetail();

            //获取总条数
            toalCount = bllAlbumDe.GetRecordCount4AlbumImgByAlbumID(albumID, UserAlbumDetailType);
            if (toalCount < 1) return new EmptyResult();   //NO DATA

            //分页获取数据
            albumDetail.PhotoListWaterfall = bllAlbumDe.GetAlbumImgListByPage(
                albumID, startIndex, endIndex, UserAlbumDetailType);
            albumDetail.AlbumModel = bllAlbums.GetModel(albumID); //专辑信息
            if (currentUser != null && albumDetail.AlbumModel != null && albumDetail.AlbumModel.CreatedUserID == currentUser.UserID)
            {
                ViewBag.IsCurrentUser = true;
            }

            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (var item in albumDetail.PhotoListWaterfall)
            {
                if (IsStatic != "true")
                {
                    item.StaticUrl = ViewBag.BasePath + (item.Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product ? "Product/" : "Photo/") + "Detail/" + item.TargetId + "?AlbumId=" + albumID;
                }
                else
                {
                    string StaticUrl = "";
                    if (item.Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product)
                    {
                        Maticsoft.Model.SNS.Products productModel = productBll.GetModelByCache(item.TargetId);
                        StaticUrl = (productModel != null && !String.IsNullOrWhiteSpace(productModel.StaticUrl)) ? productModel.StaticUrl : (ViewBag.BasePath + "Product/Detail/" + item.TargetId);
                    }
                    else
                    {
                        Maticsoft.Model.SNS.Photos photoModel = photoBll.GetModelByCache((int)item.TargetId);
                        StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.TargetId + "?AlbumId=" + albumID);
                    }
                    item.StaticUrl = StaticUrl;
                }
            }
            #endregion

            return View(CurrentThemeViewPath + "/Photo/PhotoListWaterfall.cshtml", albumDetail);
        }



        #endregion

        #region 专辑创建
        // GET: /SNS/Album/Create
        public ActionResult Create()
        {  
            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null || CurrentUser.UserType == "AA")
            {
                #region RedirectToLogin
                string rawurl = Request.RawUrl;
                return RedirectToAction("Login", "Account", new { area = MvcApplication.MainAreaRoute.ToString(), returnUrl = Server.UrlEncode(rawurl) });
                #endregion
            }
            else
            {
                ViewBag.Title = "创建新专辑";
                CreateAlbum createAlbum = new CreateAlbum();
                Maticsoft.BLL.SNS.AlbumType albumTypeBLL = new BLL.SNS.AlbumType();
                List<Model.SNS.AlbumType> list = albumTypeBLL.GetModelListByCache(Model.SNS.EnumHelper.Status.Enabled);
                createAlbum.TypeList = new List<SelectListItem>();
                if (list != null)
                {
                    list.ForEach(xx => createAlbum.TypeList.Add(new SelectListItem
                        {
                            Text = xx.TypeName,
                            Value = xx.ID.ToString(),
                            Selected = false
                        }));
                }

                return View(createAlbum);
            }

        }

        //POST: /SNS/Album/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            ViewBag.Title = "创建新专辑";
            try
            {
                if (currentUser == null || currentUser.UserType == "AA")
                {
                    #region RedirectToLogin
                    string rawurl = Request.RawUrl;
                    return RedirectToAction("Login", "Account", new { area = MvcApplication.MainAreaRoute.ToString(), returnUrl = Server.UrlEncode(rawurl) });
                    #endregion
                }
                Model.SNS.UserAlbums model = new Model.SNS.UserAlbums();
                model.AlbumName = collection["AlbumName"];
                model.Description = collection["Description"];
                //model.Tags = collection["Tags"];
                model.CreatedNickName = CurrentUser.NickName;
                model.CreatedUserID = CurrentUser.UserID;
                int TypeId = Globals.SafeInt(collection["TypeRadio"], 0);
                BLL.SNS.UserAlbums bllUA = new BLL.SNS.UserAlbums();
                int albumID = bllUA.AddEx(model, TypeId);
                if (albumID > 0)
                {
                    Maticsoft.BLL.SNS.UserAlbumsType utBll = new UserAlbumsType();
                    Model.SNS.UserAlbumsType uatModel = new Model.SNS.UserAlbumsType();
                    uatModel.AlbumsID = albumID;
                    uatModel.AlbumsUserID = model.CreatedUserID;
                    uatModel.TypeID = TypeId;
                    utBll.Add(uatModel);
                }
                return RedirectToAction("Albums", "Profile");//显示详细
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region 专辑编辑

        //
        //GET: /SNS/Album/Edit/5 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SNS/Album/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        #endregion

        #region 获得当前用户的专辑
        /// <summary>
        /// 获得当前用户的所有专辑
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUserAlbums()
        {
            List<Maticsoft.Model.SNS.UserAlbums> list = bllAlbums.GetUserAblumsByUserID(currentUser.UserID);
            return Json(list);
        }
        #endregion

        #region 专辑页面
        /// <summary>
        /// 专辑首页输出
        /// </summary>
        /// <returns></returns>
        [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public PartialViewResult AlbumPart(int AlbumType)
        {
            BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();
            List<ViewModel.SNS.AlbumIndex> models = bllAlbums.GetListForIndex(AlbumType, 8, (int)Maticsoft.Model.SNS.EnumHelper.RecommendType.Channel, UserAlbumDetailType);
            return PartialView(models);
        }

        #endregion

        #region 收藏专辑

        public ActionResult AjaxFavAlbum(int AlbumId)
        {
            Maticsoft.BLL.SNS.UserFavAlbum FavAlbumBll = new UserFavAlbum();
            int FavId = FavAlbumBll.FavAlbum(AlbumId, currentUser.UserID);
            if (FavId > 0)
            {
                return Content(FavId.ToString());
            }
            return Content("Fail");

        }
        #endregion


        #region 取消收藏专辑

        public ActionResult AjaxUnFavAlbum(int AlbumId)
        {
            Maticsoft.BLL.SNS.UserFavAlbum FavAlbumBll = new UserFavAlbum();
            int affectRow = FavAlbumBll.UnFavAlbum(AlbumId, currentUser.UserID);
            if (affectRow > 0)
            {
                return Content(affectRow.ToString());
            }
            return Content("Fail");

        }
        #endregion

        #region 检测是否收藏此专辑
        public ActionResult AjaxCheckIsFav(int AlbumId)
        {
            if (currentUser == null)
            {
                return new EmptyResult();
            }
            Maticsoft.BLL.SNS.UserFavAlbum FavAlbumBll = new UserFavAlbum();
            bool State = FavAlbumBll.CheckIsFav(AlbumId, currentUser.UserID);
            return Content(State.ToString());

        }
        #endregion

        public ActionResult AjaxGetComments(FormCollection Fm)
        {
            int pageIndex = Maticsoft.Common.Globals.SafeInt(Fm["pageIndex"], 1);
            int AlbumId = Maticsoft.Common.Globals.SafeInt(Fm["AlbumId"], 1);
            Maticsoft.BLL.SNS.Comments comBll = new Comments();
            List<Maticsoft.Model.SNS.Comments> model = comBll.GetCommentByPage((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Album, AlbumId, ViewModel.ViewModelBase.GetStartPageIndex(_commentPageSize, pageIndex), ViewModel.ViewModelBase.GetEndPageIndex(_commentPageSize, pageIndex));
            return PartialView("CommentList", model);
        }

        #region 添加评论
        /// <summary>
        ///添加评论
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments comBll = new Comments();
            Maticsoft.Model.SNS.Comments ComModel = new Model.SNS.Comments();
            int TargetId = Common.Globals.SafeInt(Fm["TargetId"], 0);
            int Type = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Album;
            int CommentId = 0;
            string Des = Maticsoft.ViewModel.ViewModelBase.ReplaceFace(Fm["Des"]);
            ComModel.CreatedDate = DateTime.Now;
            ComModel.CreatedNickName = currentUser.NickName;
            ComModel.CreatedUserID = currentUser.UserID;
            ComModel.Description = Des;
            ComModel.HasReferUser = Des.Contains('@') ? true : false;
            ComModel.IsRead = false;
            ComModel.ReplyCount = 0;
            ComModel.TargetId = TargetId;
            ComModel.Type = Type;
            ComModel.UserIP = Request.UserHostAddress;
            if ((CommentId = comBll.AddEx(ComModel)) > 0)
            {
                ComModel.CommentID = CommentId;
                ComModel.Description = ViewModel.ViewModelBase.RegexNickName(ComModel.Description);
                List<Maticsoft.Model.SNS.Comments> list = new List<Model.SNS.Comments>();
                list.Add(ComModel);
                return PartialView("CommentList", list);

            }
            return Content("No");

        }
        #endregion


    
    }
}
