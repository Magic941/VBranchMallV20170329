using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.CMS;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class ArticleController :MPageControllerBase
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
                    return View(model);
                }
            }
            return RedirectToAction("ArticleList", "Article", "MPage"); 
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
            return RedirectToAction("Index", "Home", "MPage");
        }


        #region Wap模版
        //
        // GET: /Mobile/WapArticle/
        /// <summary>
        /// 新闻咨询
        /// </summary>
        /// <param name="classID">栏目ID</param>
        /// <param name="viewname"></param>
        /// <param name="topclass">前几个分类</param>
        /// <returns></returns>
        public ActionResult News(int? classID, string viewName = "News", int topclass = 3)
        {
            string classname;
            List<Model.CMS.ContentClass> contModel = classContBll.GetModelList(topclass, classID, out classname);
            ViewBag.ClassName = classname;
            ViewBag.classID = classID;
            ViewBag.tel = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Phone");
            return View(viewName, contModel);
        }
        /// <summary>
        /// 新闻咨询 中的新闻列表
        /// </summary>
        /// <param name="top">前几条</param>
        /// <param name="classID">栏目ID</param>
        /// <param name="viewname"></param>
        /// <param name="cname">ClassName</param>
        /// <returns></returns>
        public PartialViewResult NewsContent(int top, int classID, bool HasImageurl = false, string viewName = "_NewsContent", string cname = "")
        {
            List<Model.CMS.Content> contList = contBll.GetModelList(classID, top, HasImageurl);
            ViewBag.ClassName = cname;
            return PartialView(viewName, contList);
        }
        /// <summary>
        /// 新闻咨询 列表  二级
        /// </summary>
        /// <param name="classID">子栏目ID</param>
        /// <param name="viewname"></param>
        /// <returns></returns>
        public ActionResult NewsList(int classID = -1, int pageIndex = 1, string viewName = "NewsList")
        {
            ViewBag.classID = classID;
            ViewBag.classname = classContBll.GetClassnameById(classID);
            int Aclassid = -1;
            ViewBag.AclassName = classContBll.GetAClassnameById(classID, out Aclassid);
            ViewBag.AclassID = Aclassid;
            ViewBag.newslistaction = "NewsList";//Action的名字
            int _pageSize = 6;

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            //获取总条数
            int toalCount = contBll.GetRecordCount("  State=0  and ClassID= " + classID);

            ViewBag.pageIndex = pageIndex;//当前页码
            ViewBag.totalPage = (int)Math.Ceiling(toalCount / (double)_pageSize);//总页码
            if (toalCount < 1)
            {
                return PartialView(viewName);//NO DATA
            }
            List<Model.CMS.Content> contList = contBll.GetListByPage(classID, startIndex, endIndex);
            PagedList<Model.CMS.Content> lists = new PagedList<Model.CMS.Content>(contList, pageIndex, _pageSize, toalCount);
            return View(viewName, lists);
        }
        /// <summary>
        /// 新闻咨询 列表   同上面的action使用同一个view 只是提取的数据不同
        /// </summary>
        /// <param name="classID">根栏目ID</param>
        /// <param name="viewname"></param>
        /// <param name="topclass">前几个栏目</param>
        /// <returns></returns>
        public ActionResult NewsListTwo(int classID = -1, int pageIndex = 1, int topclass = 3, bool HasImageurl =false, string viewName = "NewsList")
        {
            int Aclassid = -1;
            ViewBag.AclassName = classContBll.GetAClassnameById(classID, out Aclassid);
            ViewBag.AclassID = Aclassid;
            ViewBag.classID = classID;
            ViewBag.classname = classContBll.GetClassnameById(classID);
            ViewBag.topclass = topclass;
            //ViewBag.classname = "Null";
            ViewBag.newslistaction = "NewsListTwo";//Action的名字
            int _pageSize = 6;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            //获取总条数
            int toalCount = 0;
            List<Model.CMS.Content> contList = contBll.GetListByPage(classID, startIndex, endIndex, HasImageurl, topclass, out toalCount);
            PagedList<Model.CMS.Content> lists = new PagedList<Model.CMS.Content>(contList, pageIndex, _pageSize, toalCount);
            ViewBag.pageIndex = pageIndex;//当前页码
            ViewBag.totalPage = (int)Math.Ceiling(toalCount / (double)_pageSize);//总页码
            return View(viewName, lists);
        }
        /// <summary>
        /// 新闻咨询详细页   
        /// </summary>
        /// <param name="ContID">文章ID</param>
        /// <param name="viewname"></param>
        /// <returns></returns>
        public ActionResult NewsDetail(int? ContID, string viewName = "NewsDetail")
        {
            string className = "";
            Model.CMS.Content conteModel = contBll.GetModelByCache(ContID, out className);
            ViewBag.ClassName = className;
            ViewBag.tel = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Phone");
            int Aclassid = -1;
            if (conteModel != null)
            {
                ViewBag.AclassName = classContBll.GetAClassnameById(conteModel.ClassID, out Aclassid);
                ViewBag.AclassID = Aclassid;
            }
            return View(viewName, conteModel);
        }
        /// <summary>
        /// 类别为单文章的  
        /// </summary>
        /// <param name="classID">classID 单文章</param>
        /// <param name="viewname"></param>
        /// <returns></returns>
        public ActionResult SingleArticleDetail(int classID = -1, string viewName = "NewsDetail")
        {
            string className = "";
            if (classID > 0)
            {
                Model.CMS.Content conteModel = contBll.GetModelByClassIDByCache(classID, out className);
                ViewBag.ClassName = className;
                ViewBag.tel = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Phone");
                return View(viewName, conteModel);
            }
            return View(viewName);
        }

        /// <summary>
        /// 更新赞数
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxSupport(FormCollection Fm)
        {
            int contid = Common.Globals.SafeInt(Fm["contid"], 0);
            if (contid > 0)
            {
                return contBll.UpdateTotalSupport(contid) ? Content("true") : Content("false");
            }
            return Content("false");
        }
        #endregion
      
    }
}
