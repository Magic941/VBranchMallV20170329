using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using Maticsoft.Common;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class RssController : CMSControllerBase
    {
        //
        // GET: /CMS/Rss/
        private BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.CMS);

        public ActionResult List()
        {
            if (null == WebSiteSet)
            {
                return View();
            }
            SyndicationFeed feed =
       new SyndicationFeed(Globals.HtmlDecode(WebSiteSet.WebTitle) + "-" + Globals.HtmlDecode(WebSiteSet.WebName),
                           Globals.HtmlDecode(WebSiteSet.Description),
                           new Uri(Globals.HtmlDecode(WebSiteSet.BaseHost)),
                           "Maticsoft",
                           DateTime.Now);

            BLL.CMS.Content bll = new BLL.CMS.Content();

            DataSet ds = bll.GetRssList();
            if (!DataSetTools.DataSetIsNull(ds))
            {
                List<SyndicationItem> items = new List<SyndicationItem>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string id = Globals.SafeString(dr["ContentID"], "");

                    //SyndicationItem item = new SyndicationItem(Globals.HtmlDecode(Globals.SafeString(dr["Title"], "")),Globals.HtmlDecode(Globals.SafeString(dr["Summary"], "")), new Uri(string.Format(HttpContext.Request.Url.Host+"/Article/Details/{0}", id)), id, Globals.SafeDateTime(Globals.SafeString(dr["CreatedDate"], ""), DateTime.Now));
                    SyndicationItem item = new SyndicationItem(Globals.HtmlDecode(Globals.SafeString(dr["Title"], "")), Globals.SafeString(dr["Description"], ""), new Uri(string.Format(Globals.HtmlDecode(WebSiteSet.BaseHost) + "/Article/Details/{0}", id)), id, Globals.SafeDateTime(Globals.SafeString(dr["CreatedDate"], ""), DateTime.Now));
                    items.Add(item);
             
                }
                feed.Items = items;
            }
            return new RssActionResult() { Feed = feed };
        }
    }
}