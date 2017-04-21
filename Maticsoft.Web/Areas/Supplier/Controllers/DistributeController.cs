using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Shop.Products;
using Webdiyer.WebControls.Mvc;
using ProductInfo = Maticsoft.Model.Shop.Products.ProductInfo;

namespace Maticsoft.Web.Areas.Supplier.Controllers
{
    public class DistributeController : SupplierControllerBase
    {
        //
        // GET: /Supplier/Distribute/

        public ActionResult GetSkuList(int pageIndex=1,int pageSize=15,string viewName="SkuList",string ajaxViewName="_skuList")
        {
            BLL.Shop.Products.SKUInfo skuInfoBll=new SKUInfo();
            int startIndex = pageIndex > 0 ? (pageIndex - 1)*pageSize + 1 : 0;
            int endIndex = pageIndex*pageSize;
            int dataCount = 0;
            List<Model.Shop.Products.SKUInfo> pSkuInfosList = skuInfoBll.GetSKU4AttrVal(null, null, null, null,
                                                                                        startIndex, endIndex,out  dataCount,0);
           
            if (pSkuInfosList != null)
            {
                PagedList<Maticsoft.Model.Shop.Products.SKUInfo> pagedList = new PagedList<Maticsoft.Model.Shop.Products.SKUInfo>(pSkuInfosList, pageIndex, pageSize, dataCount);
                if (Request.IsAjaxRequest())
                {
                    return View(ajaxViewName,pagedList);
                }
                return View(viewName, pagedList);
            }
            return View();
        }

    }
}
