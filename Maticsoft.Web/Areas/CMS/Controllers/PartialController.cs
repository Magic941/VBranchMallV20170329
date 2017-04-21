using System.Collections.Generic;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components.Setting.CMS;

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    public class PartialController : CMSControllerBase
    {
        private BLL.Settings.Advertisement bllAdvertisement = new BLL.Settings.Advertisement();
        private BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.CMS);


        public ActionResult PV(string viewName)
        {
            return View(viewName);
        }


        /// <summary>
        /// 页眉
        /// </summary>
        /// <returns></returns>
        public ActionResult Header()
        {
            if (WebSiteSet != null)
            {
                ViewBag.Logo = WebSiteSet.LogoPath;
                ViewBag.WebName = WebSiteSet.WebName;
            }
            return View("Header");
        }

        /// <summary>
        /// 页脚
        /// </summary>
        /// <returns></returns>
        public ActionResult Footer()
        {
            BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.CMS);
            return View("Footer", WebSiteSet);
        }

        /// <summary>
        /// 友情链接
        /// </summary>
        /// <returns></returns>
        public ActionResult FriendLink()
        {
            BLL.Settings.FriendlyLink bll = new BLL.Settings.FriendlyLink();

            List<Model.Settings.FriendlyLink> list = bll.GetModelList(10, 1);
            return View(list);
        }

        /// <summary>
        /// 热门文章
        /// </summary>
        /// <returns></returns>
        public ActionResult HotArticles()
        {
            BLL.CMS.Content bll = new BLL.CMS.Content();
            List<Model.CMS.Content> list = bll.GetModelList();
            List<Maticsoft.Model.CMS.Content> contentList = new List<Maticsoft.Model.CMS.Content>();
            if (list == null) return View(contentList);
            #region 是否静态化
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ArticleIsStatic");
            string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
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

            #endregion
            return View(contentList);
        }

        /// <summary>
        /// 主菜单
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult MainMenu(int? id)
        {
            if (id.HasValue && id > 0)
            {
                ViewBag.MaticsoftId = id.Value;
            }
            BLL.Settings.MainMenus bll = new BLL.Settings.MainMenus();
            List<Model.Settings.MainMenus> list = bll.GetMenusByArea(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS);
            return View(list);
        }

        /// <summary>
        /// 首页大广告
        /// </summary>
        /// <returns></returns>
        public ActionResult ADRotator(int AdvPositionId)
        {
            List<Model.Settings.Advertisement> list = bllAdvertisement.GetModelList(AdvPositionId);
            bllAdvertisement.GetDefindCode(AdvPositionId);
            return View(list);
        }

        /// <summary>
        /// 自定义广告代码
        /// </summary>
        public ActionResult ADDefindCode(int AdvPositionId)
        {
            return Content(bllAdvertisement.GetDefindCode(AdvPositionId));
        }

        /// <summary>
        /// 小广告
        /// </summary>
        /// <param name="AdvPositionId"></param>
        /// <returns></returns>
        public ActionResult AD(int AdvPositionId)
        {
            Model.Settings.Advertisement model = bllAdvertisement.GetModelByAdvPositionId(AdvPositionId);
            return View(model);
        }

        /// <summary>
        /// 分享脚本
        /// </summary>
        /// <returns></returns>
        public ActionResult ShareScript()
        {
            return View("ShareScript");
        }

        /// <summary>
        /// 右侧评论
        /// </summary>
        public ActionResult Comments(int top)
        {
            BLL.CMS.Comment bllComment = new BLL.CMS.Comment();
            List<Model.CMS.Comment> list = bllComment.GetModelList(top, "", " ID desc");
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.CreatedNickName = string.IsNullOrWhiteSpace(item.CreatedNickName) ? "游客" : item.CreatedNickName;
                }
            }
            return View(list);
        }
    }
}