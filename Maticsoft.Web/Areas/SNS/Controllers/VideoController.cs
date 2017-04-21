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

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class VideoController : SNSControllerBase
    {
        //
        // GET: /SNS/Video/
        private int _basePageSize = 6;
        private int _waterfallSize = 32;
        private int _waterfallDetailCount = 1;
        Maticsoft.BLL.SNS.Posts postBll = new Posts();

              private int commentPagesize = 5;

              public VideoController()
        {
            this.commentPagesize = CommentDataSize;
            this._basePageSize = FallInitDataSize;
            this._waterfallSize = FallDataSize;
        }

        public ActionResult Index(int? pageIndex)
        {
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = postBll.GetRecordCount(" type=3 and Status=1");

            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex.Value * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;
            // photoList.ZuiInList = bllPhotos.GetZuiInListByCache((int)Maticsoft.Model.SNS.EnumHelper.PhotoType.ShareGoods, 3);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("VideoList", ApplicationKeyType.SNS);
            ViewBag.Title =  pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            if (toalCount < 1)
                return View();   //NO DATA
         
            //分页获取数据
            PagedList<Maticsoft.Model.SNS.Posts> VideoList = postBll.GetVideoListByPage(
                -1, startIndex, endIndex).ToPagedList(
                                                    pageIndex ?? 1,
                                                    pageSize,
                                                    toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("VideoList", VideoList);

            // photoList.ScrollList == bllPhotos.GetTopPhotoPostByType(9, (int)Maticsoft.Model.SNS.EnumHelper.PhotoType.ShareGoods);
            //#region 静态化路径
            //string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            //if (photoList.PhotoPagedList != null && photoList.PhotoPagedList.Count > 0)
            //{
            //    foreach (var item in photoList.PhotoPagedList)
            //    {
            //        if (IsStatic != "true")
            //        {
            //            item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.TargetId;
            //        }
            //        else
            //        {
            //            Maticsoft.Model.SNS.Photos photoModel = bllPhotos.GetModelByCache((int)item.TargetId);
            //            item.StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.TargetId);
            //        }
            //    }
            //}
            //#endregion

            return View(VideoList);
        }


        [HttpPost]
        // [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult VideosWaterfall(int startIndex)
        {
            ViewBag.BasePageSize = _basePageSize;
          
            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;
            //获取总条数
            toalCount = postBll.GetRecordCount(" type=3 and Status=1");
            if (toalCount < 1) return new EmptyResult();   //NO DATA

            //分页获取数据
            List<Maticsoft.Model.SNS.Posts> VideoList = postBll.GetVideoListByPage(
                -1, startIndex, endIndex);
            //#region 静态化路径
            //string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            //foreach (var item in photoList.PhotoListWaterfall)
            //{
            //    if (IsStatic != "true")
            //    {
            //        item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.TargetId;
            //    }
            //    else
            //    {
            //        Maticsoft.Model.SNS.Photos photoModel = bllPhotos.GetModelByCache((int)item.TargetId);
            //        item.StaticUrl = (photoModel != null && !String.IsNullOrWhiteSpace(photoModel.StaticUrl)) ? photoModel.StaticUrl : (ViewBag.BasePath + "Photo/Detail/" + item.TargetId);
            //    }
            //}
            //#endregion
            return View(CurrentThemeViewPath + "/Video/VideoListWaterfall.cshtml", VideoList);

        }

        public ActionResult Detail(int id)
        {
            Maticsoft.BLL.SNS.Posts postBll = new Posts();
            Maticsoft.Model.SNS.Posts videoModel = postBll.GetModel(id);
            if (videoModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CommentPageSize = commentPagesize;
            Maticsoft.BLL.SNS.Comments commentBll = new BLL.SNS.Comments();
            ViewBag.Commentcount = commentBll.GetCommentCount((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal, id);
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("VideoDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, videoModel.Description ?? videoModel.CreatedNickName + "分享的视频" });
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(videoModel);
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PartialViewResult UserInfo(int id)
        {
            Maticsoft.BLL.Members.UsersExp usersExpBll = new BLL.Members.UsersExp();
            Maticsoft.Model.Members.UsersExpModel UserModel = new Model.Members.UsersExpModel();

            UserModel = usersExpBll.GetUsersModel(id);
            if (UserModel == null)
            {
                return PartialView();
            }
            return PartialView("_UserInfo", UserModel);
        }

        public PartialViewResult TopVideo(int id,int top=5)
        {
            Maticsoft.BLL.SNS.Posts postBll=new Posts();
            List<Maticsoft.Model.SNS.Posts> listPost = postBll.GetVideoList(id, top);
            return PartialView("_TopVideo",listPost);
        }
    }
}
