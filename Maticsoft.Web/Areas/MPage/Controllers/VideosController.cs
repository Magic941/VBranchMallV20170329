using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class VideosController : MPageControllerBase
    {
        //
        // GET: /Mobile/Videos/
        #region 视频
        public ActionResult VideosList(int pageIndex = 1, string viewName = "VideosList")
        {
            int _pageSize = 6;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            //获取总条数
            int toalCount = 0;
            BLL.CMS.Video videoBll = new BLL.CMS.Video();
            List<Model.CMS.Video> videoList = videoBll.GetListByPage(startIndex, endIndex, out toalCount);
            PagedList<Model.CMS.Video> lists = new PagedList<Model.CMS.Video>(videoList, pageIndex, _pageSize, toalCount);
            ViewBag.pageIndex = pageIndex;//当前页码
            ViewBag.totalPage = (int)Math.Ceiling(toalCount / (double)_pageSize);//总页码
            return View(viewName, lists);
        }
      
        public ActionResult VideoDetail(int vid = -1, string viewName = "VideoDetail")
        {
            BLL.CMS.Video videoBll = new BLL.CMS.Video();
            Model.CMS.Video videoModel = videoBll.GetModelByCache(vid);
            return View(viewName, videoModel);
        }

        #endregion
       

    }
}
