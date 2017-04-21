using Maticsoft.BLL.Products.Lucene;
using Maticsoft.Model.Shop.Products.Lucene;
using Maticsoft.ViewModel.Shop;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;
using Webdiyer.WebControls.Mvc;

namespace ProductLuceneTool.Controllers
{
    public class SearchController : ApiController
    {

       
        /// <summary>
        /// 根据关键字搜索信息,  全局关键字,好邻自营的排前面
        /// </summary>
        /// <param name="keyWords">关键字</param>
        /// <param name="sortMode">排序方式：默认按搜索最大关联排序、销量正排、销量反排序、价格正排、价格反排序</param>
        /// <param name="priceRange">价格范围</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public ProductSearchResult SearchInMall(string keyWords, ProductIndexEnum.EnumSearchSortType sortMode, string priceRange, int pageIndex = 1, int pageSize = 30)
        {
            var p = ProductIndexManager.productIndex.SearchInMall(keyWords,sortMode,priceRange, pageIndex,pageSize);
            p.ErrMsg = "检索关键字为[" + keyWords + "]商品检索成功,检索数量为：" + p.SearchCount;
           
            return p;
        }
        /// <summary>
        /// 在指定的分类下搜索 
        /// </summary>
        /// <param name="categoryId">分类搜索编号</param>
        /// <param name="keyWords">分类下的关键字</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <returns></returns>
        [HttpGet]
        public ProductSearchResult SearchInCategory(int categoryId, string keyWords, int pageIndex = 1, int pageSize = 30)
        {
            var p = ProductIndexManager.productIndex.SearchInCategory(categoryId,keyWords, pageIndex, pageSize);
            p.ErrMsg = "检索关键字为[" + keyWords + "]商品检索成功,检索数量为：" + p.SearchCount;
            return p;
        }

        /// <summary>
        /// 店铺内搜索 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <param name="keyWords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ProductSearchResult SearchInStore(int storeId, string keyWords, int pageIndex = 1, int pageSize = 30)
        {
             var p = ProductIndexManager.productIndex.SearchInStore(storeId,keyWords, pageIndex,pageSize);
            p.ErrMsg = "检索关键字为[" + keyWords + "]商品检索成功,检索数量为：" + p.SearchCount;
            return p;
        }

        /// <summary>
        /// 搜索团购, 基本是全搜索 
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="categoryId"></param>
        /// <param name="keyWords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ProductSearchResult SearchInGroupBuy(int groupbuyId, string keyWords, int pageIndex = 1, int pageSize = 30)
        {
            //var p = ProductIndexManager.productIndex.SearchInMall(keyWords, pageIndex, pageSize);
            //p.ErrMsg = "检索关键字为[" + keyWords + "]商品检索成功,检索数量为：" + p.SearchCount;
            //return p;
            return null;
        }

        [HttpGet]
        public ProductIndexAPIBaseModel ProProductIndex()
        {
            ProductIndexAPIBaseModel m = new ProductIndexAPIBaseModel();
            m.ErrCode = 1;
            m.ErrMsg = "索引生成成功";
            var result = ProductIndexManager.productIndex.CreateProductIndexFromDataBase();
            return m;
        }

        /// <summary>
        /// 编号问题
        /// </summary>
        /// <param name="productId">商品系统编辑</param>
        /// <param name="actionType">操作类型</param>
        /// <returns></returns>
        [HttpGet]
        public ProductIndexAPIBaseModel ProductIndexPro(long productId, ProductIndexEnum.EnumProductIndexAction actionType)
        {
            var result = new ProductIndexAPIBaseModel();
            result.ErrCode = 1;
            switch(actionType)
            {
                case ProductIndexEnum.EnumProductIndexAction.Add:
                    {
                        var p = new Maticsoft.BLL.Shop.Products.ProductInfo();
                        var dataset = p.GetListALL("sp.ProductId=" + productId);
                        var allproducts = p.ProductDataTableToListAll(dataset.Tables[0]);
                        if (allproducts != null && allproducts.Count > 0)
                        {
                            ProductIndexManager.productIndex.Add(allproducts[0]);
                            result.ErrMsg = "商品编号为【" + productId + "】新增成功";
                        }
                        else
                        {
                            result.ErrCode = -3;
                            result.ErrMsg = "商品编号为【" + productId + "】读取失败，新增失败";
                        }
                        break;
                    }
                case ProductIndexEnum.EnumProductIndexAction.Update:
                    {
                        var p = new Maticsoft.BLL.Shop.Products.ProductInfo();
                        var dataset = p.GetListALL(" sp.ProductId=" + productId);
                        var allproducts = p.ProductDataTableToListAll(dataset.Tables[0]);
                        if (allproducts != null && allproducts.Count > 0)
                        {
                            ProductIndexManager.productIndex.Mod(allproducts[0]);
                            result.ErrMsg = "商品编号为【" + productId + "】更新成功";
                        }
                        else
                        {
                            result.ErrCode = -3;
                            result.ErrMsg = "商品编号为【" + productId + "】读取失败，修改失败";
                        }

                       
                    }
                    break;
                case ProductIndexEnum.EnumProductIndexAction.Delete:
                    ProductIndexManager.productIndex.Del(productId);
                    result.ErrMsg = "商品编号为【" + productId + "】删除成功";
                    break;

            }

            return result;
        }

    }
}