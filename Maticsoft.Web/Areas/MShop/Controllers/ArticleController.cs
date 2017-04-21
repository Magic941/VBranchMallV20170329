using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.CMS;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class ArticleController :MShopControllerBase
    {
        //
        // GET: /Mobile/Article/
       
        private BLL.CMS.ContentClass classContBll = new BLL.CMS.ContentClass();
        private BLL.CMS.Content contBll = new BLL.CMS.Content();

        /// <summary>
        /// 文章内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// [OutputCache(Duration = 2000, VaryByParam = "id")]
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                int contentid = id.Value;
                Model.CMS.Content model = contBll.GetModelByCache(contentid);
                if (null != model)
                {
                    #region SEO 优化设置
                    IPageSetting pageSetting = PageSetting.GetArticleSetting(model);
                    ViewBag.Title = pageSetting.Title;
                    ViewBag.Keywords = pageSetting.Keywords;
                    ViewBag.Description = pageSetting.Description;
                    #endregion

                    int PreId = contBll.GetPrevID(contentid, model.ClassID);
                    int NextId = contBll.GetNextID(contentid, model.ClassID);
                    contBll.UpdatePV(contentid);//更新浏览量
                    ViewBag.AClassName = classContBll.GetAClassnameById(model.ClassID);//获得此文章所属的一级栏目的栏目名称
                    ViewBag.PreUrl = PreId > 0 ? ViewBag.BasePath+"Article/Details/" + PreId : "#";
                    ViewBag.NextUrl = NextId > 0 ? ViewBag.BasePath+"Article/Details/" + NextId : "#";
                }
                return View(model);
            }
          
            return RedirectToAction("ArticleList", "Article", "MShop"); 
        }

        /// <summary>
        ///  文章列表
        /// </summary>
        /// <param name="classid">类别ID</param>
        /// <returns></returns>
        public ActionResult ArticleList(int? classid)
        {
            if (classid.HasValue)
            {
                Model.CMS.ContentClass contclassModel = classContBll.GetModelByCache(classid.Value);
                if (contclassModel!=null)
                {
                    #region SEO 优化设置
                    IPageSetting pageSetting = PageSetting.GetContentClassSetting(contclassModel);
                    ViewBag.Title = pageSetting.Title;
                    ViewBag.Keywords = pageSetting.Keywords;
                    ViewBag.Description = pageSetting.Description;
                    #endregion

                    List<Model.CMS.Content> contModel = contBll.GetModelList(classid.Value, 0);
                    return View(contModel);
                }  
            }
            return RedirectToAction("Index", "Home", "MShop");
        }

        public ActionResult Index()
        {
            BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.Shop);
            ViewBag.phone = webSiteSet.Company_Telephone;
            return View();
        }
      
    }
}
