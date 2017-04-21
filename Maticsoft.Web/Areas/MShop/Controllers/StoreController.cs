using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class StoreController : MShopControllerBase
    {
        //
        // GET: /Shop/Supplier/
        private readonly BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo(); 
        private readonly BLL.Shop.Supplier.SupplierInfo suppinfoBll = new BLL.Shop.Supplier.SupplierInfo();
        public ActionResult Index(int suppId = 0, int cid = 0, string mod = "hot", string viewName = "Index")
        {
            Model.Shop.Supplier.SupplierInfo suppinfoModel = suppinfoBll.GetModelByCache(suppId);
            if (suppinfoModel != null && suppinfoModel.Status == 1 && suppinfoModel.StoreStatus == 1)
            {
                #region RouteDataParam
                string dataParam = "{";
                foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
                {
                    dataParam += item.Key + ":'" + item.Value + "',";
                }
                dataParam = dataParam.TrimEnd(',') + "}";
                ViewBag.DataParam = dataParam;
                #endregion

                int top = BLL.Shop.Supplier.SupplierConfig.GetIntValueByCache(suppId, "MoblieIndexProdCount");
                int actTop = top> 0 ? top : 8;//读取商家自己设置的top值 
                List<Model.Shop.Products.ProductInfo> list = productManage.GetSuppProductsList(actTop, cid, suppId, mod,"","");
                #region SEO 优化设置
                IPageSetting pageSetting = PageSetting.GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
                ViewBag.Title = suppinfoModel.ShopName + "-" + pageSetting.Title;
                ViewBag.Keywords = pageSetting.Keywords;
                ViewBag.Description = pageSetting.Description;
                #endregion
                return View(viewName, list);
            }
            return RedirectToAction("Index", "Home", new { area = "MShop" });
        }

        #region List
        public ActionResult List(int suppId = 0, int cid = 0, string mod = "hot",int pvn=0, string ky = "",
            int p = 1, string viewName = "List", string ajaxViewName = "_PageProductList")
        {
            Model.Shop.Supplier.SupplierInfo suppinfoModel = suppinfoBll.GetModelByCache(suppId);
            if (suppinfoModel != null && suppinfoModel.Status == 1 && suppinfoModel.StoreStatus == 1)//
            {
                if (pvn ==  1 )//此值用于标识商品列表显示的样式
                {
                    ajaxViewName = "_PageProdList_Image";
                }

                #region RouteDataParam
                string dataParam = "{";
                foreach (KeyValuePair<string, object> item in Request.RequestContext.RouteData.Values)
                {
                    dataParam += item.Key + ":'" + item.Value + "',";
                }
                dataParam = dataParam.TrimEnd(',') + "}";
                ViewBag.DataParam = dataParam;
                #endregion

                int _pageSize =16;

                //计算分页起始索引
                int startIndex = p > 1 ? (p - 1) * _pageSize + 1 : 0;

                //计算分页结束索引
                int endIndex = p * _pageSize;

                //获取总条数
                int toalCount = productManage.GetSuppProductsCount(cid, suppId, ky, "");
                ViewBag.ToalCount = toalCount;
               
                if (toalCount < 1) return View(viewName); //NO DATA
                List<Model.Shop.Products.ProductInfo> list = productManage.GetSuppProductsListEx(cid, suppId, ky, "", mod, startIndex, endIndex);
                //分页获取数据
                PagedList<Model.Shop.Products.ProductInfo> pageList = list.ToPagedList(p, _pageSize, toalCount);
                //检测Ajax请求, 进行无刷新分页
                if (Request.IsAjaxRequest())
                    return PartialView(ajaxViewName, pageList);
                return View(viewName, pageList);
            }
            return RedirectToAction("Index", "Home", new { area = "MShop" });
        }
        #endregion

        public PartialViewResult RecProd(int suppid, int type = 0, int top = 10, string viewName = "_IndexRecProdList")
        {
            List<Model.Shop.Products.ProductInfo> list = productManage.GetSuppRecList(suppid, type);
            return PartialView(viewName, list);
        }

        #region Header
        public PartialViewResult Header(int suppId = 0, string viewName = "_Header")
        {
            Model.Shop.Supplier.SupplierInfo suppinfoModel = suppinfoBll.GetModelByCache(suppId);
          
            if (suppinfoModel != null)
            {
                string address = new BLL.Ms.Regions().GetAddress(suppinfoModel.RegionId);
                if (address != "暂未设置")
                {
                    address += suppinfoModel.Address;
                }
                suppinfoModel.Address = address;
                return PartialView(viewName, suppinfoModel);
            }
            return PartialView(viewName);
        }

        #endregion

        public ActionResult Introduction(int suppId = 0, string viewName = "Introduction")
        {
            Model.Shop.Supplier.SupplierInfo suppinfoModel = suppinfoBll.GetModelByCache(suppId);
            return View(viewName, suppinfoModel);
        }
    }
}
