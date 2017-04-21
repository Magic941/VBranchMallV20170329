using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.CMS;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class ErrorController : MPageControllerBase
    {
        //
        // GET: /Mobile/Error/

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
            ViewBag.Title = "出错啦 - " + ViewBag.SiteName;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        public ActionResult TurnOff()
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
            ViewBag.Title = "该功能已关闭 - " + ViewBag.SiteName;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        public ActionResult UserError()
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
            ViewBag.Title = "用户不存在 - " + ViewBag.SiteName;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

    }
}
