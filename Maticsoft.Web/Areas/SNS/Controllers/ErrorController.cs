using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.SNS;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class ErrorController :SNSControllerBase
    {
        //
        // GET: /SNS/Error/
        public ActionResult Index()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.SNS);
            ViewBag.Title = "出错啦 - " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        public ActionResult TurnOff()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.SNS);
            ViewBag.Title = "该功能已关闭 - " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

        public ActionResult UserError()
        {
            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.SNS);
            ViewBag.Title = "用户不存在 - " + pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View();
        }

    }
}
