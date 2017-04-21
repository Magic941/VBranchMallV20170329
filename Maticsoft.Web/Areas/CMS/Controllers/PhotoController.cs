using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using System.Data;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class PhotoController : CMSControllerBase
    {
        private int _basePageSize = 8;
        private int _waterfallSize = 32;
        private int _waterfallDetailCount = 1;
        //
        // GET: /CMS/Photo/
        public ActionResult Index(int? pageIndex, int? photoClassId)
        {
            ViewBag.Title = "图片";
            Maticsoft.BLL.CMS.Photo bllPhoto = new BLL.CMS.Photo();
            Maticsoft.ViewModel.CMS.Photo models = new ViewModel.CMS.Photo();

            Maticsoft.BLL.CMS.PhotoClass bllPhotoClass = new BLL.CMS.PhotoClass();
            models.PhotoClassList = bllPhotoClass.GetTopList(10, "Depth=1", "Sequence");
            ViewBag.PhotoClassId = photoClassId;


            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;

            string classWhere = photoClassId.HasValue ? "ClassID=" + photoClassId.Value : "";

            //总记录数
            toalCount = bllPhoto.GetRecordCount(classWhere);//瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex.Value * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;

            List<Maticsoft.Model.CMS.Photo> modelList = bllPhoto.GetListModelByPage(classWhere, "PhotoID DESC", startIndex, endIndex);

            models.PhotoPagedList = new PagedList<Model.CMS.Photo>(modelList, pageIndex ?? 1, pageSize, toalCount);

            if (Request.IsAjaxRequest())
            {
                return PartialView("PhotoList", models);
            }
            return View(models);
        }

        public ActionResult PhotoListWaterfall(int startIndex, int? photoClassId)
        {
            Maticsoft.BLL.CMS.Photo bll = new BLL.CMS.Photo();
            Maticsoft.ViewModel.CMS.Photo models = new ViewModel.CMS.Photo();
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;

            string classWhere = photoClassId.HasValue ? "ClassID=" + photoClassId.Value : "";

            //获取总条数
            toalCount = bll.GetRecordCount(classWhere);
            if (toalCount < 1) return new EmptyResult();   //NO DATA

            //分页获取数据
            models.PhotoListWaterfall = bll.GetListModelByPage(classWhere, "PhotoID DESC", startIndex, endIndex);

            return View(models);
        }

        public ActionResult AjaxLikePhoto(int PhotoId)
        {  
            Maticsoft.BLL.CMS.Photo bll = new BLL.CMS.Photo();
            Maticsoft.Model.CMS.Photo models = new Maticsoft.Model.CMS.Photo();
            if (!bll.Exists(PhotoId)) return Content("False");
            models = bll.GetModel(PhotoId);
            models.FavouriteCount = models.FavouriteCount + 1;
            if (bll.Update(models)) return Content("True");
            return Content("False");
        }

        public ActionResult Detail(int id)
        {
            Maticsoft.BLL.CMS.Photo bll = new BLL.CMS.Photo();
            Maticsoft.Model.CMS.Photo models = new Maticsoft.Model.CMS.Photo();
            models = bll.GetModel(id);
            if (models == null) return new EmptyResult();
            List<Maticsoft.Model.CMS.Photo> list = bll.GetListAroundPhotoId(10, id, models.ClassID);
            return View(list);
        }
    }
}
