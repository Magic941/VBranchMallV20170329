using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.SNS;
using Maticsoft.Model.SysManage;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;
using Maticsoft.BLL.Ms;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class PhotoController : SNSControllerBase
    {
        private int commentPagesize = 5;
        private int _basePageSize = 6;
        private int _waterfallSize = 32;
        private int _waterfallDetailCount = 1;
        private BLL.SNS.Photos bllPhotos = new BLL.SNS.Photos();
        private  Maticsoft.BLL.SNS.Categories cateBll=new Categories();
        public PhotoController()
        {
            this.commentPagesize = CommentDataSize;
            this._basePageSize = FallInitDataSize;
            this._waterfallSize = FallDataSize;

        }

        //
        // GET: /SNS/Photo/

        //
        // GET: /SNS/ShareGoods/
        public ActionResult Index(int type, int categoryId, string address, string orderby, int? pageIndex,string vName="Index")
        {
            ViewModel.SNS.PhotoList photoList = new ViewModel.SNS.PhotoList();
            photoList.CategoryInfo = cateBll.GetModelByCache(categoryId);
            //if (photoList.CategoryInfo == null)
            //{
            //    return RedirectToAction("Index", "Error");
            //}
            int pageSize =  _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            if (address == "all")
            {
                address = "";
            }
            string cateName;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;
            if (categoryId > 0)
            {
                cateName = cateBll.GetTopNameByCid(categoryId) ?? "全部分类";
            }
            else
            {
                cateName = "全部分类";
            }
            ViewBag.CateName = cateName;
            //获取总条数
            toalCount = bllPhotos.GetCountEx(type, categoryId, address);

            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex.Value * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;
            // photoList.ZuiInList = bllPhotos.GetZuiInListByCache((int)Maticsoft.Model.SNS.EnumHelper.PhotoType.ShareGoods, 3);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPhotoListSetting(categoryId, ApplicationKeyType.SNS);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            switch (orderby)
            {
                case "hot":
                    orderby = "CommentCount desc";
                    ViewBag.Title += " 最热";
                    break;
                case "popular":
                    orderby = "FavouriteCount desc";
                    ViewBag.Title += " 喜欢";
                    break;
                default: //new
                    orderby = "";
                    ViewBag.Title += " 最新";
                    break;
            }

            if (toalCount < 1)
                return View(photoList);   //NO DATA

            //分页获取数据
            photoList.PhotoPagedList = bllPhotos.GetPhotoListByPageCache(
                type, categoryId, address, orderby, startIndex, endIndex).ToPagedList(
                                                    pageIndex ?? 1,
                                                    pageSize,
                                                    toalCount);
            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if (photoList.PhotoPagedList != null && photoList.PhotoPagedList.Count > 0)
            {
                foreach (var item in photoList.PhotoPagedList)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.TargetId;
                    }
                    else
                    {
                        Maticsoft.Model.SNS.Photos photoModel = bllPhotos.GetModelByCache((int)item.TargetId);
                        item.StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.TargetId);
                    }
                }
            }
            if (photoList.ZuiInList != null && photoList.ZuiInList.Count > 0)
            {
                foreach (var item in photoList.ZuiInList)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.PhotoId;
                    }
                    else
                    {
                        Maticsoft.Model.SNS.Photos photoModel = bllPhotos.GetModelByCache(item.PhotoId);
                        item.StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.PhotoId);
                    }
                }
            }
            #endregion

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("PhotoList", photoList);

            // photoList.ScrollList == bllPhotos.GetTopPhotoPostByType(9, (int)Maticsoft.Model.SNS.EnumHelper.PhotoType.ShareGoods);



            return View(vName, photoList);
        }
        
        public ActionResult PhotoIndex(int categoryId=0, int top = 3, string viewName = "PhotoIndex")
        {
            List<Maticsoft.Model.SNS.Photos> photoList = bllPhotos.GetTopPhotoPostByType(top, categoryId);
            return View(viewName,photoList);
        }
        [HttpPost]
        // [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult PhotosWaterfall(int type, int categoryId, string address, string orderby, int startIndex)
        {
            ViewModel.SNS.PhotoList photoList = new ViewModel.SNS.PhotoList();
            ViewBag.BasePageSize = _basePageSize;
            if (address == "all")
            {
                address = "";
            }
            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;
            //获取总条数
            toalCount = bllPhotos.GetCountEx(type, categoryId, address);
            if (toalCount < 1) return new EmptyResult();   //NO DATA

            switch (orderby)
            {
                case "hot":
                    orderby = "CommentCount desc";
                    break;
                case "popular":
                    orderby = "FavouriteCount desc";
                    break;
                default:
                    orderby = "";
                    break;
            }
            //分页获取数据
            photoList.PhotoListWaterfall = bllPhotos.GetPhotoListByPageCache(
               type, categoryId, address, orderby, startIndex, endIndex);
            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (var item in photoList.PhotoListWaterfall)
            {
                if (IsStatic != "true")
                {
                    item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.TargetId;
                }
                else
                {
                    Maticsoft.Model.SNS.Photos photoModel = bllPhotos.GetModelByCache((int)item.TargetId);
                    item.StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.TargetId);
                }
            }
            #endregion
            return View("PhotoListWaterfall", photoList);

        }

        public ActionResult ScrollPhotos()
        {
            List<Maticsoft.Model.SNS.Photos> list = bllPhotos.GetTopPhotoPostByType(9, (int)Maticsoft.Model.SNS.EnumHelper.PhotoType.ShareGoods);

            #region 静态化路径
            if (list != null && list.Count > 0)
            {
                string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                foreach (var item in list)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.PhotoID;
                    }
                    else
                    {
                        Maticsoft.Model.SNS.Photos photoModel = bllPhotos.GetModelByCache(item.PhotoID);
                        item.StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.PhotoID);
                    }
                }
            }
            #endregion

            return View(list);
        }

        public ActionResult Detail(int pid)
        {
            Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
            if (!photoBll.Exists(pid))
            {
                return RedirectToAction("Index", "Error");
            }
            int albumId = Common.Globals.SafeInt(Request.Params["AlbumId"], -1);
            Maticsoft.ViewModel.SNS.TargetDetail DetailModel = new ViewModel.SNS.TargetDetail();
            Maticsoft.BLL.SNS.PhotoTags tagsBll = new BLL.SNS.PhotoTags();
            Maticsoft.BLL.SNS.Comments commentBll = new BLL.SNS.Comments();
            DetailModel = photoBll.GetPhotoAssistionInfo(pid, UserAlbumDetailType);
            Maticsoft.BLL.SNS.UserFavourite ufBll = new BLL.SNS.UserFavourite();
            DetailModel.RecommandPhoto = photoBll.GetRecommandByPid(pid);
            DetailModel.PhotoTagList = tagsBll.GetHotTags(16);
            DetailModel.FavUserList = ufBll.GetFavUserByTargetId(pid, (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo, 8);
            DetailModel.CommentPageSize = commentPagesize;
            if (DetailModel.Photo == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int PrevId = photoBll.GetPrevID(pid, albumId);
            int NextId = photoBll.GetNextID(pid, albumId);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("PhotoDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, DetailModel.Sharedes ?? DetailModel.Nickname + "分享的图片" });   //图片分享内容
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");

            if (IsStatic != "true")
            {
                ViewBag.PrevUrl = PrevId > 0 ? ViewBag.BasePath + "Photo/Detail/" + PrevId : "#";
                ViewBag.NextUrl = NextId > 0 ? ViewBag.BasePath + "Photo/Detail/" + NextId : "#";
            }
            else
            {
                ViewBag.PrevUrl = PrevId > 0 ? PageSetting.GetPhotoUrl(PrevId) : "#";
                ViewBag.NextUrl = NextId > 0 ? PageSetting.GetPhotoUrl(NextId) : "#";
            }

            foreach (var item in DetailModel.RecommandPhoto)
            {
                if (IsStatic != "true")
                {
                    item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.PhotoID;
                }
                else
                {
                    item.StaticUrl = (String.IsNullOrWhiteSpace(item.StaticUrl) ? ViewBag.BasePath + "Photo/Detail/" + item.PhotoID : item.StaticUrl);
                }
            }
            #endregion

            return View(DetailModel);
        }

        public PartialViewResult PhotoCatePart(int categoryId, int top, Maticsoft.Model.SNS.EnumHelper.RecommendType mode = Maticsoft.Model.SNS.EnumHelper.RecommendType.Home,string viewName = "_PhotoCatePart")
        {
            Maticsoft.BLL.SNS.Photos PhotoBll = new BLL.SNS.Photos();
            List<Maticsoft.Model.SNS.Photos> PhotoList = PhotoBll.GetRecPhoto(categoryId,top,mode);
            if (PhotoList != null && PhotoList.Count > 0)
            {
                string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
                foreach (var item in PhotoList)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.PhotoID;
                    }
                    else
                    {
                        item.StaticUrl = (String.IsNullOrWhiteSpace(item.StaticUrl) ? ViewBag.BasePath + "Photo/Detail/" + item.PhotoID : item.StaticUrl);
                    }
                }
            }
            return PartialView(viewName, PhotoList);

        }

        #region Ajax 方法
        public ActionResult GetCount(FormCollection Fm)
        {
            if (String.IsNullOrWhiteSpace(Fm["PhotoId"]))
            {
                return Content("No");
            }
            else
            {
                int PhotoId = Common.Globals.SafeInt(Fm["PhotoId"], 0);
                Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
                Maticsoft.BLL.SNS.Comments commentBll = new BLL.SNS.Comments();
                Maticsoft.BLL.SNS.UserFavourite ufBll = new BLL.SNS.UserFavourite();
                int favcount = ufBll.GetFavCountByTargetId(PhotoId, (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo);
                int commmentcount = commentBll.GetCommentCount((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo, PhotoId);
                //更新浏览数
                photoBll.UpdatePvCount(PhotoId);
                var model = photoBll.GetPhotoAssistionInfo(PhotoId, UserAlbumDetailType);
                return Content(model.Favouritecount + "|" + model.PvCount + "|" + commmentcount + "|" + favcount);
            }
        }

        public ActionResult GetListCounts(FormCollection Fm)
        {
            if (String.IsNullOrWhiteSpace(Fm["PhotoIds"]))
            {
                return Content("No");
            }
            else
            {
                string PhotoIds = Fm["PhotoIds"];
                var PhotoId_Arry = PhotoIds.Split(',');
                Maticsoft.BLL.SNS.Comments commentBll = new BLL.SNS.Comments();
                Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
                string result = "";
                int i = 0;
                foreach (var item in PhotoId_Arry)
                {
                    int PhotoId = Common.Globals.SafeInt(item, 0);
                    var model = photoBll.GetPhotoAssistionInfo(PhotoId, UserAlbumDetailType);
                    int commmentcount = commentBll.GetCommentCount((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo, PhotoId);
                    if (i == 0)
                    {
                        result = PhotoId + "," + model.Favouritecount + "," + commmentcount;
                    }
                    else
                    {
                        result = result + "|" + PhotoId + "," + model.Favouritecount + "," + commmentcount;
                    }
                    i++;
                }
                return Content(result);
            }
        }

        #endregion

    }
}
