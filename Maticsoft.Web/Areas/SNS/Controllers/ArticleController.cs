using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.CMS;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class ArticleController : SNSControllerBase
    {
        BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.SNS);
        Maticsoft.BLL.CMS.Comment comBll = new BLL.CMS.Comment();
        Maticsoft.BLL.CMS.Content contentBll = new Content();
        private Maticsoft.BLL.CMS.ContentClass contentClassBll = new ContentClass();
        private int commentPagesize = 3;
        //
        // GET: /SNS/Article/Column/5

        public ActionResult Index(int classId, int? page)
        {
            List<Maticsoft.Model.CMS.ContentClass> AllClass = Maticsoft.BLL.CMS.ContentClass.GetAllClass();
            Maticsoft.Model.CMS.ContentClass CurrentClass = AllClass.FirstOrDefault(c => c.ClassID == classId);
            if (CurrentClass != null)
            {
                ViewBag.ClassName = CurrentClass.ClassName;                
                //if (!string.IsNullOrWhiteSpace(CurrentClass.Meta_Title))
                //    ViewBag.Title = CurrentClass.Meta_Title;
                //else
                //    ViewBag.Title = CurrentClass.ClassName + "-" + WebSiteSet.WebTitle;

                //if (!string.IsNullOrWhiteSpace(CurrentClass.Meta_Keywords))
                //    ViewBag.Keywords = CurrentClass.Meta_Keywords;
                //else
                //    ViewBag.Keywords = CurrentClass.ClassName + "-" + WebSiteSet.KeyWords;

                //if (!string.IsNullOrWhiteSpace(CurrentClass.Meta_Description))
                //    ViewBag.Description = CurrentClass.Meta_Description;
                //else
                //    ViewBag.Description = CurrentClass.ClassName + "-" + WebSiteSet.Description;

            }
            #region SEO 优化设置
            IPageSetting pageSetting = Maticsoft.Web.Components.Setting.CMS.PageSetting.GetContentClassSetting(CurrentClass);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 20;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;

           
            //总记录数
            int totalcount = 0;

            totalcount = contentBll.GetRecordCount(" State=0  and ClassID=" + classId);
            ViewBag.TotalCount = totalcount;
            if (totalcount == 0)
                return View();
            PagedList<Maticsoft.Model.CMS.Content> BlogList = new PagedList<Maticsoft.Model.CMS.Content>(
                contentBll.GetList(classId, startIndex, endIndex, "")
                , page ?? 1, pagesize, totalcount);

            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/Article/ArticleList.cshtml", BlogList);


            return View(CurrentThemeViewPath + "/Article/Index.cshtml", BlogList);
        }

        public ActionResult Column(int id=1)
        {
           // int id = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("SNSHelpCenter"), 1);
            BLL.CMS.ContentClass bll = new BLL.CMS.ContentClass();
            Model.CMS.ContentClass model = bll.GetModelByCache(id);
            return View(model);
        }

        //
        // GET: /SNS/Article/ArticleList/5

        public ActionResult ArticleLeft(int id=1)
        {
           // int id = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("SNSHelpCenter"), 1);
            BLL.CMS.Content bll = new BLL.CMS.Content();
            List<Model.CMS.Content> list = bll.GetModelList(id);
            return View(list);
        }

        //
        // GET: /SNS/Article/Details/5

        public ActionResult Details(int id)
        {
            BLL.CMS.Content bll = new BLL.CMS.Content();
            Model.CMS.Content model = bll.GetModelExByCache(id);
            if (null != model)
            {
                ViewBag.ArticleId = id;
                ViewBag.Title = Globals.HtmlDecode(model.Title);
                if (null != WebSiteSet)
                {
                    ViewBag.Title += "-" + Globals.HtmlDecode(WebSiteSet.WebName);
                }
                //更新PV
                bll.UpdatePV(id);
                ViewBag.Keywords = Globals.HtmlDecode(model.Keywords);
                ViewBag.Description = Globals.HtmlDecode(model.Summary);
            }
            return View(model);
        }

        public ActionResult ArticleDetail(int id)
        {
            if (!contentBll.Exists(id))
            {
                return RedirectToAction("Index", "Error");
            }
            Maticsoft.Model.CMS.Content content = contentBll.GetModelByCache(id);
            
            #region SEO 优化设置
            IPageSetting pageSetting = Maticsoft.Web.Components.Setting.CMS.PageSetting.GetArticleSetting(content);
            //pageSetting.Replace(
            //    new[] { PageSetting.RKEY_CTNAME, content.ClassName},
            //     new[] { PageSetting.RKEY_CNAME, content.Title},
            //      new[] { PageSetting.RKEY_CATEID, content.ClassID.ToString() },
            //       new[] { PageSetting.RKEY_CID, content.ContentID.ToString() }); //ctname,cname,cateid,cid
            ViewBag.Title =pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords ;
            ViewBag.Description = pageSetting.Description;
            #endregion
            return View(content);
        }

        public PartialViewResult ArticlePart(int ClassId, string viewName = "_ArticlePart", int top = 10, int ContentId = -1)
        {
            List<Maticsoft.Model.CMS.Content> contentList = contentBll.GetMoreList(ClassId, ContentId, top);
            return PartialView(viewName, contentList);
        }
        public PartialViewResult HotArticle(Maticsoft.Model.CMS.EnumHelper.ContentRec mode = Maticsoft.Model.CMS.EnumHelper.ContentRec.Hot, int ClassId = 0, string viewName = "_HotArticle", int top = -1)
        {
            List<Maticsoft.Model.CMS.Content> contentList = contentBll.GetRecList(ClassId, mode, top);
            string className = contentClassBll.GetClassnameById(ClassId);
            ViewBag.className = className ?? "暂无栏目名称";
            return PartialView(viewName, contentList);
        }

        public PartialViewResult HotComment(int ClassId = 0, string viewName = "_HotComment", int top = -1)
        {
            List<Maticsoft.Model.CMS.Content> contentList = contentBll.GetHotComList(ClassId, top);
            return PartialView(viewName, contentList);
        }

      

        #region Ajax 方法
        public ActionResult AjaxGetPvCount(int id)
        {
            int count = 0;
            count = contentBll.UpdatePV(id);
            return Content(count.ToString());
        }

        public ActionResult AjaxCount(int ContentId)
        {
            Maticsoft.Model.CMS.Content contentModel = contentBll.GetModel(ContentId);
            return Content(contentModel.TotalFav + "|" + contentModel.TotalComment);
        }
        public ActionResult AjaxFavCount(int ContentId)
        {
            if (Request.Cookies["ContentFav" + ContentId] != null && Request.Cookies["ContentFav" + ContentId].Value == ContentId.ToString())
            {
                return Content("Repeat");
            }
            if (contentBll.UpdateFav(ContentId))
            {
                //写入Cookie,防止重复操作“赞”。
                HttpCookie cookie = new HttpCookie("ContentFav" + ContentId);
                cookie.Value = ContentId.ToString();
                cookie.Expires = DateTime.MaxValue;
                Response.AppendCookie(cookie);
                return Content("Yes");
            }
            return Content("No");
        }

        #region 添加评论
        /// <summary>
        ///添加评论
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            if (currentUser == null)
            {
                return null;
            }

            Maticsoft.Model.CMS.Comment ComModel = new Model.CMS.Comment();
            int ContentId = Common.Globals.SafeInt(Fm["ContentId"], 0);

            int CommentId = 0;

            string Des = Maticsoft.Common.Globals.HtmlEncode(Fm["Des"]);
            Des = ViewModel.ViewModelBase.ReplaceFace(Des);
            ComModel.CreatedDate = DateTime.Now;
            ComModel.CreatedNickName = currentUser.NickName;
            ComModel.CreatedUserID = currentUser.UserID;
            ComModel.Description = Des;
            ComModel.IsRead = false;
            ComModel.ReplyCount = 0;
            ComModel.ContentId = ContentId;
            ComModel.TypeID = (int)Maticsoft.Model.CMS.EnumHelper.CommentType.Content;
            ComModel.State = true;
            if ((CommentId = comBll.AddEx(ComModel)) > 0)
            {
                ComModel.ID = CommentId;
                // ComModel.Description = ViewModel.ViewModelBase.RegexNickName(ComModel.Description);
                List<Maticsoft.Model.CMS.Comment> list = new List<Model.CMS.Comment>();
                //是否含有审核词
                if (!Maticsoft.BLL.Settings.FilterWords.ContainsModWords(ComModel.Description))
                {
                    list.Add(ComModel);
                }
                return PartialView("_ArticleComment", list);
            }
            return Content("No");

        }
        #endregion



        public ActionResult AjaxGetComments(int ContentId, int? PageIndex)
        {
            int StartIndex = Maticsoft.ViewModel.ViewModelBase.GetStartPageIndex(commentPagesize, PageIndex.Value);
            int EndIndex = Maticsoft.ViewModel.ViewModelBase.GetEndPageIndex(commentPagesize, PageIndex.Value);

            List<Maticsoft.Model.CMS.Comment> CommentList = comBll.GetComments(ContentId, StartIndex, EndIndex);
            return PartialView("_ArticleComment", CommentList);

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
