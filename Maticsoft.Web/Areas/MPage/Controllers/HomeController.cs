using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.Components.Setting;
using Maticsoft.Model.JLT;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Web.Components.Setting.Shop;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class HomeController : MPageControllerBase
    {
        //
        // GET: /Mobile/Home/
        private Maticsoft.BLL.Shop.Products.ProductInfo productBll = new Maticsoft.BLL.Shop.Products.ProductInfo();

        public ActionResult Index()
        {   
            #region SEO 优化设置
            ApplicationKeyType applicationKey = ApplicationKeyType.CMS;
            //判断当前的主路由
            switch (MvcApplication.MainAreaRoute)
            {
                case AreaRoute.Shop:
                    applicationKey = ApplicationKeyType.Shop;
                    break;
                case AreaRoute.SNS:
                    applicationKey = ApplicationKeyType.SNS;
                    break;
                default:
                    applicationKey = ApplicationKeyType.CMS;
                    break;
            }
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", applicationKey);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        #region 商品列表
        public PartialViewResult ProductList(int Cid, Maticsoft.Model.Shop.Products.ProductRecType RecType = ProductRecType.IndexRec, int Top = 10, string viewName = "_ProductList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            Maticsoft.Model.Shop.Products.CategoryInfo categoryInfo = cateList.FirstOrDefault(c => c.CategoryId == Cid);
            if (categoryInfo != null)
            {
                ViewBag.CategoryName = categoryInfo.Name;
            }
            List<Maticsoft.Model.Shop.Products.ProductInfo> productList = productBll.GetProductRecList(RecType, Cid, Top);
            return PartialView(viewName, productList);
        }
        #endregion

        public PartialViewResult CategoryList(int Cid = 0, int Top = 10, string ViewName = "_CategoryList")
        {
            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList = Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> categoryInfos = cateList.Where(c => c.ParentCategoryId == Cid).ToList();
            return PartialView(ViewName, categoryInfos);
        }

        public PartialViewResult NewsList(string viewName, int ClassID, int Top)
        {
            BLL.CMS.Content conBll = new BLL.CMS.Content();
            List<Model.CMS.Content> list = conBll.GetModelList(ClassID, Top);
            //ViewBag.contentclassName = contentclassBll.GetClassnameById(ClassID);
            return PartialView(viewName, list);
        }


        /// <summary>
        /// 考勤
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public ActionResult Attendance(int userId = 0)
        {
            Maticsoft.BLL.JLT.AttendanceType typeBll = new Maticsoft.BLL.JLT.AttendanceType();
            List<Maticsoft.Model.JLT.AttendanceType> typeList = typeBll.GetAllType();
            ViewBag.UserId = userId;
            ViewBag.Title = "考勤提交";
            return View(typeList);
        }
        #region Ajax 方法
        public ActionResult AjaxAddAttendance(FormCollection Fm)
        {
            Maticsoft.Model.JLT.UserAttendance model = new UserAttendance();
            Maticsoft.BLL.JLT.UserAttendance attendanceBll = new BLL.JLT.UserAttendance();
            int userId = Common.Globals.SafeInt(Fm["UserId"], 0);
            string latitude = Fm["Latitude"];
            string longitude = Fm["Longitude"];
            int typeId = Common.Globals.SafeInt(Fm["TypeId"], 0);

            Maticsoft.Accounts.Bus.User user = new User(userId);
            model.UserID = userId;
            model.Score = 0;
            model.Status = 1;
            model.Latitude = latitude;
            model.Longitude = longitude;
            model.TypeID = typeId;
            model.TrueName = user.TrueName;
            model.UserName = user.UserName;
            model.CreatedDate = DateTime.Now;
            model.AttendanceDate = DateTime.Now.Date;
            return attendanceBll.Add(model) > 0 ? Content("True") : Content("False");
        }
        #endregion

        #region  wap首页 
        // GET: /Mobile/Wap1Partial/
        //首页
        private BLL.CMS.Content contBll = new BLL.CMS.Content();
        #region wap首页  
        public PartialViewResult _Index(string viewName = "_Index")
        {
            ViewBag.MPageLogo = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Logo");
            ViewBag.WebPowerBy = BLL.SysManage.ConfigSystem.GetValueByCache("WebPowerBy"); 
            return PartialView(viewName);
        }
        public PartialViewResult IndexNewsList(int top, bool HasImageurl = false, int classID = -1, string viewName = "_IndexNewsList",int topclass=3)
        {
            List<Maticsoft.Model.CMS.Content> contlist = contBll.GetModelListEx(top, classID, HasImageurl, topclass);
            return PartialView(viewName, contlist);
        }
        public PartialViewResult RecVideos(int top, string viewName = "_IndexVideosList")
        {
            BLL.CMS.Video videoBll = new BLL.CMS.Video();
            List<Model.CMS.Video> videoList = videoBll.GetRecModelList(top);
            return PartialView(viewName, videoList);
        }
        #endregion
        #endregion
    }
}
