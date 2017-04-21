using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Settings;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class PartialController : MPageControllerBase
    {
        //
        // GET: /Mobile/Partial/

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Footer(string viewName = "_Footer")
        {
            if (currentUser!=null)
            {
                ViewBag.usernickname = currentUser.NickName;//用户已登录
            }
            return PartialView(viewName);
        }

        #region 广告位
        public PartialViewResult AdDetail(int id, string ViewName = "_IndexAd")
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Advertisement();
            List<Maticsoft.Model.Settings.Advertisement> list = bll.GetListByAidCache(id);
            return PartialView(ViewName, list);
        }

        /// <summary>
        /// 小广告
        /// </summary>
        /// <param name="AdvPositionId"></param>
        /// <returns></returns>
        public PartialViewResult AD(int AdvPositionId, string viewName = "_AD")
        {
            BLL.Settings.Advertisement bllAdvertisement = new BLL.Settings.Advertisement();
            Model.Settings.Advertisement model = bllAdvertisement.GetModelByAdvPositionId(AdvPositionId);
            return PartialView(viewName, model);
        }
        #endregion

        #region 菜单导航
        public PartialViewResult Navigation(string viewName = "_Navigation")
        {
            return PartialView(viewName);
        }
        #endregion 

        #region Wap模版菜单导航
        public PartialViewResult NavBar(string viewName = "_NavBar")
        {
            ViewBag.tel = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Phone");
            ViewBag.FormID= BLL.SysManage.ConfigSystem.GetIntValueByCache("System_Poll_FormID");
            return PartialView(viewName);
        }
        public PartialViewResult FooterNav(string viewName = "_FooterNav")
        {
           ViewBag.tel = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Phone");
           return PartialView(viewName);
        }
        #endregion
    }
}
