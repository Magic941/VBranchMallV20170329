using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.Components.Filters;
using Maticsoft.Web.Components.Setting.CMS;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class ArticleController : CMSControllerBase
    {
        private const string SESSIONKEY_COMMENTDATE = "CMS_CommentDate";
        private readonly TimeSpan _commentTimeSpan = new TimeSpan(0, 0, 0, 30);

        private BLL.CMS.Content bll = new BLL.CMS.Content();
        private BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.CMS);
        private int Act_EditContent = 15; //编辑文章

        protected int _basePageSize = 30;
        

        public virtual ActionResult ArticleList(int classid = 0, int pageIndex = 1, int pageSize = 30,
                                  string viewName = "ArticleList")
        {
            int status = Common.Globals.SafeInt(Request.Params["Status"], 1);          

            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;

            //计算分页结束索引
            int endIndex = pageIndex * pageSize;
            int toalCount ;
            //获取总条数
            var result = bll.GetListByPage(classid, startIndex, endIndex, true, 5,out toalCount);
            ViewBag.Total = toalCount;

            PagedList<Maticsoft.Model.CMS.Content> lists = new PagedList<Maticsoft.Model.CMS.Content>(result, pageIndex, pageSize, toalCount);
            
            return View(viewName,lists);
        }


        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                int contentid = id.Value;
                Model.CMS.Content model = bll.GetModelExByCache(contentid);
               
                if (null != model)
                { 
                    bll.UpdatePV((int)id);
                    ViewBag.Title = Globals.HtmlDecode(model.Title);
                    if (null != WebSiteSet)
                    {
                        ViewBag.Title += "-" + Globals.HtmlDecode(WebSiteSet.WebName);
                    }
                    ViewBag.Keywords = Globals.HtmlDecode(model.Keywords);
                    ViewBag.Description = Globals.HtmlDecode(model.Summary);

                    ViewBag.Domain = WebSiteSet.BaseHost;
                    ViewBag.WebName = WebSiteSet.WebName;

                    #region 是否静态化
                    string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ArticleIsStatic");
                    string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
                    int PrevId = bll.GetPrevID(contentid);
                    int NextId = bll.GetNextID(contentid);
                    bll.UpdatePV(contentid);//更新浏览量
                    if (IsStatic != "true")
                    {
                        if (PrevId > 0)
                        {
                            if (area == "CMS")
                            {
                                ViewBag.PrevUrl = "/Article/Details/" + PrevId;
                            }
                            else
                            {
                                ViewBag.PrevUrl = "/CMS/Article/Details/" + PrevId;
                            }
                        }
                        else
                        {
                            ViewBag.PrevUrl = "";
                        }
                        if (NextId > 0)
                        {
                            if (area == "CMS")
                            {
                                ViewBag.NextUrl = "/Article/Details/" + NextId;
                            }
                            else
                            {
                                ViewBag.NextUrl = "/CMS/Article/Details/" + NextId;
                            }
                        }
                        else
                        {
                            ViewBag.NextUrl = "";
                        }
                    }
                    else
                    {
                        if (PrevId > 0)
                        {
                            ViewBag.PrevUrl = PageSetting.GetCMSUrl(PrevId);
                        }
                        else
                        {
                            ViewBag.PrevUrl = "";
                        }
                        if (NextId > 0)
                        {
                            ViewBag.NextUrl = PageSetting.GetCMSUrl(PrevId);
                        }
                        else
                        {
                            ViewBag.NextUrl = "";
                        }
                    }
                    #endregion

                    if (UserPrincipal != null
                        && currentUser != null
                        && UserPrincipal.HasPermissionID(GetPermidByActID(Act_EditContent)))
                    {
                        ViewBag.EditContent = "";
                    }
                    else
                    {
                        ViewBag.EditContent = "display:none;";
                    }
                }
                return View(model);
            }
            return View("Details");
        }

        /// <summary>
        /// 赞
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public void Support(int id)
        {
            JsonObject json = new JsonObject();
            if (Request.Cookies["UsersSupports" + id] != null && Request.Cookies["UsersSupports" + id].Value == id.ToString())
            {
                json.Accumulate("STATUS", "NOTALLOW");
            }
            else
            {
                if (bll.UpdateTotalSupport(id))
                {
                    Model.CMS.Content model = bll.GetModel(id);
                    Model.CMS.Content modelCache = bll.GetModelExByCache(id);
                    if (model != null)
                    {
                        json.Accumulate("STATUS", "SUCC");
                        json.Accumulate("TotalSupport", model.TotalSupport);
                        modelCache.TotalSupport = model.TotalSupport;   //更新缓存

                        //写入Cookie,防止重复操作“赞”。
                        HttpCookie cookie = new HttpCookie("UsersSupports" + id);
                        cookie.Value = id.ToString();
                        cookie.Expires = DateTime.MaxValue;
                        Response.AppendCookie(cookie);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "FAIL");
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "FAIL");
                }
            }
            Response.Write(json.ToString());
        }

        public ActionResult Comment(Maticsoft.Model.CMS.Comment model)
        {
            if (Session[SESSIONKEY_COMMENTDATE] != null && !string.IsNullOrEmpty(model.Description))
            {
                DateTime? commentDate = Globals.SafeDateTime(Session[SESSIONKEY_COMMENTDATE].ToString(), null);
                if (commentDate.HasValue && DateTime.Now - commentDate.Value < _commentTimeSpan)
                {
                    return Content("NOCOMMENT");
                }
            }
            model.CreatedDate = DateTime.Now;
            Maticsoft.BLL.CMS.Comment bll = new BLL.CMS.Comment();
            List<Maticsoft.Model.CMS.Comment> list = new List<Maticsoft.Model.CMS.Comment>();
            if (string.IsNullOrEmpty(model.Description))
            {
                list = bll.GetModelList("ContentId=" + model.ContentId + "");
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        item.CreatedNickName = String.IsNullOrWhiteSpace(item.CreatedNickName) ? "游客" : item.CreatedNickName;
                    }
                }
                return PartialView(list);
            }
            model.TypeID = 3;
            if (currentUser != null)
            {
                model.CreatedUserID = currentUser.UserID;
                model.CreatedNickName = currentUser.NickName;
            }

            if ((model.ContentId = bll.Add(model)) > 0)
            {
                model.CreatedNickName = String.IsNullOrWhiteSpace(model.CreatedNickName) ? "游客" : model.CreatedNickName;
                list.Add(model);
                Session[SESSIONKEY_COMMENTDATE] = DateTime.Now;
                return PartialView(list);
            }
            return Content("False");

        }
        /// <summary>
        ///浏览数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public void GetPvCount(int id)
        {
            JsonObject json = new JsonObject();
            int count = bll.UpdatePV(id);
            json.Accumulate("STATUS", "SUCC");
            json.Accumulate("DATA", count);
            Response.Write(json.ToString());
        }

        public PartialViewResult HomePartial(int classid, int top,bool isRec=false,bool isHot=false, string viewName="Home")
        {
            List<Maticsoft.Model.CMS.Content> list = bll.GetModelList(classid, top,isRec,isHot);
            return PartialView(viewName, list);
        }

        /// <summary>
        /// 上一篇 下一篇
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="currentArticleId"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult ArticleNextUpPartial(int classid, int currentArticleId, string viewName = "_NewsNextAndUp")
        {
            List<Maticsoft.Model.CMS.Content> list = bll.GetNextAndUp(classid, currentArticleId);
            return PartialView(viewName, list);
        }

    }
}