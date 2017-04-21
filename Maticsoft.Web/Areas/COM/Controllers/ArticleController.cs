using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /COM/Article/
        private BLL.CMS.Content contBll = new BLL.CMS.Content();
        private BLL.CMS.ContentClass classContBll = new BLL.CMS.ContentClass();

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// [OutputCache(Duration = 2000, VaryByParam = "id")]
        public ActionResult Detail(int id)
        {
            Model.CMS.Content model = contBll.GetModelByCache(id);
            if (null != model)
            {
                contBll.UpdatePV(id);//更新浏览量
                ViewBag.AClassName = classContBll.GetAClassnameById(model.ClassID);//获得此文章所属的一级栏目的栏目名称
                return View(model);
            }
            return View();
        }

    }
}
