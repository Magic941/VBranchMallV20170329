using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class SearchController : MPageControllerBase
    {
        //
        // GET: /Shop/Search/
                #region 全局变量
        private BLL.CMS.Content contBll = new BLL.CMS.Content();
        #endregion
 
        #region 搜索
        //搜索
        public ActionResult Search(string viewName = "Index")
        {
            ViewBag.tel = BLL.SysManage.ConfigSystem.GetValueByCache("WeChat_MPage_Phone");
            return View(viewName);
        }
        /// <summary>
        /// 提交搜索
        /// </summary>
        /// <param name="menuid"></param>
        /// <param name="kw"></param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public ActionResult SearchList(int menuid = -1, string kw = "", int pageIndex = 1, int topclass = 3, string viewName = "_SearchList")
        {
            kw = InjectionFilter.Filter(kw);
            if (menuid > 0 && !String.IsNullOrWhiteSpace(kw))
            {
                int _pageSize = 6;
                //计算分页起始索引
                int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;
                //计算分页结束索引
                int endIndex = pageIndex * _pageSize;
                //获取总条数
                int toalCount = 0;
                List<Model.CMS.Content> contList = contBll.GetListByPageEx(menuid, startIndex, endIndex, kw, topclass, out toalCount);
                PagedList<Model.CMS.Content> lists = new PagedList<Model.CMS.Content>(contList, pageIndex, _pageSize, toalCount);
                ViewBag.pageIndex = pageIndex;//当前页码
                ViewBag.totalPage = (int)Math.Ceiling(toalCount / (double)_pageSize);//总页码
                ViewBag.menuid = menuid;
                ViewBag.keywords = kw;
                return View(viewName, lists);
            }
            return View(viewName);
        }
        #endregion


    }
}
