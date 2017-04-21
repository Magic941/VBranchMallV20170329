using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class AlinkController : MPageControllerBase
    {
        //
        // GET: /Mobile/WapAlink/

        #region 友情链接
        /// <summary>
        /// 友情链接
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult Alinks(int top, string viewName = "Alinks")
        {
            BLL.Settings.FriendlyLink bll = new BLL.Settings.FriendlyLink();
            List<Model.Settings.FriendlyLink> list = bll.GetModelList(top, 0);//0 为图片连接  1为文字连接
            return View(viewName, list);
        }

        public ActionResult AlinkDetail(int aid = -1, string viewName = "AlinkDetail")
        {
            BLL.Settings.FriendlyLink bll = new BLL.Settings.FriendlyLink();
            Model.Settings.FriendlyLink friendModel = bll.GetModelByCache(aid);
            return View(viewName, friendModel);
        }
        #endregion

    }
}
