using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.Shop;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class ErrorController : MShopControllerBase
    {
        //
        // GET: /Mobile/Error/

        public ActionResult Index()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "出错啦 - " + ViewBag.SiteName;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        public ActionResult TurnOff()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "该功能已关闭 - " + ViewBag.SiteName;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        public ActionResult UserError()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.Title = "用户不存在 - " + ViewBag.SiteName;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        public ActionResult FailLink()
        {
            return View();
        }
        

    }
}
