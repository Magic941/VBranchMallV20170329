using System.Collections.Generic;
using System.Web.Mvc;
using Maticsoft.Common;
using Webdiyer.WebControls.Mvc;
using model = Maticsoft.Model.CMS.Content;
using Maticsoft.Web.Components.Setting.CMS;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class SearchController : CMSControllerBase
    {
        private BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.CMS);

        [ValidateInput(false)]
        public ActionResult Article(int? id, string keywords, int? page)
        {
            ViewBag.Keywords = Globals.HtmlDecode(keywords);
            if (null != WebSiteSet)
            {
                ViewBag.Title = Globals.HtmlDecode(WebSiteSet.WebTitle) + "-" + Globals.HtmlDecode(WebSiteSet.WebName);
                ViewBag.Description = Globals.HtmlDecode(WebSiteSet.Description);
            }

            ViewBag.Domain = WebSiteSet.BaseHost;
            ViewBag.WebName = WebSiteSet.WebName;

            ViewBag.HotWordss = keywords;
            if (null == keywords)
            {
                return View();
            }
            if (keywords.Length > 25)
            {
                //ViewBag.Message = "Toolong";
                return View();
            }

            BLL.CMS.Content bllContent = new BLL.CMS.Content();
            int pagesize = 10;

            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;

            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;

            //计算分页结束索引
            int endIndex = page.Value * pagesize;

            //总记录数
            int toalCount = bllContent.GetRecordCount(id, keywords);
            PagedList<model> ArticleList = null;
            List<model> list = bllContent.GetList(id, startIndex, endIndex, keywords);

            #region 是否静态化
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ArticleIsStatic");
            List<Maticsoft.Model.CMS.Content> contentList = new List<model>();
            string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    if (IsStatic == "true")
                    {
                        item.SeoUrl = PageSetting.GetCMSUrl(item.ContentID);
                    }
                    else
                    {
                        if (area == "CMS")
                        {
                            item.SeoUrl = "/Article/Details/" + item.ContentID;
                        }
                        else
                        {
                            item.SeoUrl = "/CMS/Article/Details/" + item.ContentID;
                        }
                    }
                    contentList.Add(item);
                }
            }
            #endregion

            if (contentList != null && contentList.Count > 0)
            {
                ArticleList = new PagedList<model>(contentList, page ?? 1, pagesize, toalCount);
            }

            if (Request.IsAjaxRequest())
                return PartialView("~/Areas/CMS/Themes/Default/Views/Partial/UCjQuerySearchList.cshtml", ArticleList);

            return View(ArticleList);
        }
    }
}