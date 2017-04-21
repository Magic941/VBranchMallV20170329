using System.Web.Mvc;
using Maticsoft.Common;
using System.Collections.Generic;
using System.Linq;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class AboutController : CMSControllerBase
    {
        private BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.CMS);
        private BLL.CMS.Content bll = new BLL.CMS.Content();
        /// <summary>
        /// 关于我们
        /// </summary>
        /// <param name="menuClassId"></param>
        /// <returns></returns>
        public ActionResult Index(int curContentId=0,int menuClassId=56)
        {
            ViewBag.Title = "关于我们";
            if (null != WebSiteSet)
            {
                List<Maticsoft.Model.CMS.Content> list = bll.GetModelList(menuClassId, 10, true,false);
                if (list != null&&list.Count>0)
                {
                    var cur = list.FirstOrDefault(a => a.ContentID == curContentId);
                    if (cur == null)
                        cur = list.First();
                    ViewBag.Keywords = Globals.HtmlDecode(WebSiteSet.KeyWords);
                    ViewBag.Description = Globals.HtmlDecode(WebSiteSet.Description);
                    ViewBag.NavContent = list;
                    ViewBag.CurContentId = cur.ContentID;
                    ViewBag.CurTitle = cur.Title;
                    ViewBag.CurContent = cur.Description;
                }
            }
            return View();
        }
    }
}