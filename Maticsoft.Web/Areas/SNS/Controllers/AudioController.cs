using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.SNS;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class AudioController : SNSControllerBase
    {
        //
        // GET: /SNS/Audio/
        private int _basePageSize = 6;
        private int _waterfallSize = 32;
        private int _waterfallDetailCount = 1;

        private int commentPagesize = 5;
        Maticsoft.BLL.SNS.Posts postBll = new Posts();
        public ActionResult Index(int? pageIndex)
        {
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Base", ApplicationKeyType.SNS);
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

            //获取总条数
            toalCount = postBll.GetRecordCount(" Status=1 and AudioUrl IS NOT NULL AND AudioUrl <>'' ");

            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex.Value * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;
            // photoList.ZuiInList = bllPhotos.GetZuiInListByCache((int)Maticsoft.Model.SNS.EnumHelper.PhotoType.ShareGoods, 3);
            if (toalCount < 1)
                return View();   //NO DATA

            //分页获取数据
            PagedList<Maticsoft.Model.SNS.Posts> AudioList = postBll.GetAudioListByPage(
                -1, startIndex, endIndex).ToPagedList(
                                                    pageIndex ?? 1,
                                                    pageSize,
                                                    toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("AudioList", AudioList);

         
            
            return View(AudioList);
        }

       
        [HttpPost]
        // [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult AudiosWaterfall(int startIndex)
        {
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;
            //获取总条数
            toalCount = postBll.GetRecordCount(" Status=1 and AudioUrl IS NOT NULL AND AudioUrl <>'' ");
            if (toalCount < 1) return new EmptyResult();   //NO DATA

            //分页获取数据
            List<Maticsoft.Model.SNS.Posts> AudioList = postBll.GetAudioListByPage(
                -1, startIndex, endIndex);

            return View(CurrentThemeViewPath + "/Audio/AudioListWaterfall.cshtml", AudioList);

        }
        /// <summary>
        /// 音频详细
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(int id)
        {
            Maticsoft.Model.SNS.Posts audioModel = postBll.GetModel(id);
            if (audioModel == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.CommentPageSize = commentPagesize;
            Maticsoft.BLL.SNS.Comments commentBll = new BLL.SNS.Comments();
            ViewBag.Commentcount = commentBll.GetCommentCount((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal, id);
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("PhotoDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, audioModel.Description ?? audioModel.CreatedNickName + "分享的音乐" });
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(audioModel);
        }

    }
}
