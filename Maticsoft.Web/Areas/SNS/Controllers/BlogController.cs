using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.SNS;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class BlogController : SNSControllerBase
    {
        //
        // GET: /SNS/Blog/
        private  Maticsoft.BLL.SNS.UserBlog blogBll=new UserBlog();
        public ActionResult Index(int? page)
        {
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int totalcount = 0;

            #region SEO 信息
            IPageSetting pageSetting = PageSetting.GetPageSetting("BlogList", ApplicationKeyType.SNS);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            totalcount = blogBll.GetRecordCount("  Status=1 ");
            ViewBag.TotalCount = totalcount;
            if (totalcount == 0)
                return View();
            PagedList<Maticsoft.Model.SNS.UserBlog> BlogList = new PagedList<Maticsoft.Model.SNS.UserBlog>(
                blogBll.GetUserBlogPage(" Status=1"," CreatedDate desc", startIndex, endIndex)
                , page ?? 1, pagesize, totalcount);

         

            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/Blog/BlogList.cshtml", BlogList);
          

            return View( CurrentThemeViewPath + "/Blog/Index.cshtml",BlogList);
        }
        //文章博客详细页
        public ActionResult BlogDetail(int id)
        {
            if (!blogBll.Exists(id))
            {
                return RedirectToAction("Index", "Error");
            }
            Maticsoft.Model.SNS.UserBlog userBlog = blogBll.GetModelByCache(id);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetBlogDetailSetting(userBlog);
            pageSetting.Replace(
                  new[] { PageSetting.RKEY_CATEID, userBlog.Title.ToString() },
                   new[] { PageSetting.RKEY_CID, userBlog.BlogID.ToString() }); //ctname,cname,cateid,cid
            ViewBag.Title = pageSetting.Title;// userBlog.Title + "-" + userBlog.UserName + "_分享的文章";
            ViewBag.Keywords = pageSetting.Keywords;// pageSetting.Keywords + "," + userBlog.Title;
            ViewBag.Description = pageSetting.Description;// +"," + userBlog.Title;
            #endregion
            return View(userBlog);
        }

        #region 分部视图
        public PartialViewResult BlogPart(string viewName = "_BlogPart", int top=-1,int UserId=-1,int BlogId=-1)
        {
           List<Maticsoft.Model.SNS.UserBlog> blogList =blogBll.GetMoreList(UserId, BlogId, top);
           return PartialView(viewName, blogList);
        }
        public PartialViewResult HotBlog(string viewName = "_HotBlog", int top = -1)
        {
            List<Maticsoft.Model.SNS.UserBlog> blogList = blogBll.GetHotBlogList( top);
           return PartialView(viewName, blogList);
        }

        /// <summary>
        /// 获取最新评论
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public PartialViewResult NewComment(string viewName = "_NewComment", int top = 15)
        {
            Maticsoft.BLL.SNS.Comments commentBll=new Comments();
            List<Maticsoft.Model.SNS.Comments> blogCommentList = commentBll.GetBlogComment(" type=4 and Status=1", " CreatedDate desc",top);
            //过滤HTML 标签
            if (blogCommentList.Count > 0)
            {
                foreach (var commentse in blogCommentList)
                {
                    commentse.Description = NoHTML(commentse.Description);
                }
            }
            
            return PartialView(viewName, blogCommentList);
        }

        /// <summary>
        /// 获取最新评论
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public PartialViewResult ActiveUser(string viewName = "_ActiveUser", int top = 15)
        {
            List<Maticsoft.Model.SNS.UserBlog> userBlogList = blogBll.GetActiveUser(top);
            return PartialView(viewName, userBlogList);
        }


       
        #endregion 


        #region Ajax 方法
        public ActionResult AjaxGetPvCount( int id)
        {
            int count = 0;
            if (blogBll.UpdatePvCount(id))
            {
                count = blogBll.GetPvCount(id);
            }
            return Content(count.ToString());
        }

        public ActionResult AjaxCount(int BlogId)
        {
            Maticsoft.Model.SNS.UserBlog blogModel = blogBll.GetModel(BlogId);
            return Content(blogModel.TotalFav + "|" + blogModel.TotalComment);
        }
        public ActionResult AjaxFavCount(int BlogId)
        {
            if (Request.Cookies["UsersBlogFav" + BlogId] != null && Request.Cookies["UsersBlogFav" + BlogId].Value == BlogId.ToString())
            {
                return Content("Repeat"); 
            }
            if (blogBll.UpdateFavCount(BlogId))
            {
                //写入Cookie,防止重复操作“赞”。
                HttpCookie cookie = new HttpCookie("UsersBlogFav" + BlogId);
                cookie.Value = BlogId.ToString();
                cookie.Expires = DateTime.MaxValue;
                Response.AppendCookie(cookie);
                return Content("Yes");
            }
            return Content("No");
        }
        
        #endregion 


        #region 辅助方法
        ///   <summary>
        ///   去除HTML标记
        ///   </summary>
        ///   <param   name="NoHTML">包括HTML的源码   </param>
        ///   <returns>已经去除后的文字</returns>
        public string NoHTML(string Htmlstring)
        {

            //删除脚本
            Htmlstring = Regex.Replace(Htmlstring, @"<script[^>]*?>.*?</script>", "",
                                       RegexOptions.IgnoreCase);
            //删除HTML
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", "",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", "",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "   ",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9",
                                       RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", "",
                                       RegexOptions.IgnoreCase);

            Htmlstring.Replace("<", "");
            Htmlstring.Replace(">", "");
            Htmlstring.Replace("\r\n", "");
            Htmlstring = System.Web.HttpContext.Current.Server.HtmlEncode(Htmlstring).Trim();
            return Htmlstring;
        }
        #endregion
    }
}
