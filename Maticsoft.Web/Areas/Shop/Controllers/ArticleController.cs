using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.CMS;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    public class ArticleController : ShopControllerBase
    {
        //
        // GET: /Shop/Common/
        BLL.CMS.Content contentBll = new BLL.CMS.Content();
        BLL.CMS.ContentClass contentclassBll = new BLL.CMS.ContentClass();

     

       #region 文章 
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
                Model.CMS.Content model = contentBll.GetModelByCache(contentid);
                if (null != model)
                {
                    #region SEO 优化设置
                    //IPageSetting pageSetting = PageSetting.GetPageSetting("CMS", Model.SysManage.ApplicationKeyType.CMS);
                    //pageSetting.Replace(
                    //    new[] { PageSetting.RKEY_CNAME, model.Title },   //文章标题
                    //    new[] { PageSetting.RKEY_CID, model.ContentID.ToString() });   //文章ID
                    IPageSetting pageSetting = PageSetting.GetArticleSetting(model);
                    ViewBag.Title = pageSetting.Title;
                    ViewBag.Keywords = pageSetting.Keywords;
                    ViewBag.Description = pageSetting.Description;
                    #endregion
                    contentBll.UpdatePV(contentid);//更新浏览量
                    ViewBag.AClassName = contentclassBll.GetAClassnameById(model.ClassID);//获得此文章所属的一级栏目的栏目名称
                    return View(model);
                }
            }
            return Redirect("/Home");
        }

        public PartialViewResult LeftMenu(int classid, string viewName = "_LeftMenu")
        {
            Model.CMS.ContentClass classmodel;
            List<Maticsoft.Model.CMS.ContentClass> list = contentclassBll.GetModelList(classid, out  classmodel);
            if (classmodel != null)
            {
                ViewBag.AclassName = classmodel.ClassName;
                ViewBag.AclassId = classmodel.ClassID;
            }         
            return PartialView(viewName,list);
        }
        //文章列表
        public PartialViewResult ContentTitleList(int classid, string viewName = "_ContentTitleList")
        {
            List<Maticsoft.Model.CMS.Content> list = contentBll.GetModelList(classid, 0);
            return PartialView(viewName, list);
        }      
        #endregion
       
        
    }
}
