using Maticsoft.Model.Shop.Products.Lucene;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Maticsoft.Services
{
    /// <summary>
    /// 商品搜索 API
    /// </summary>
    public class ProductSearchAPI
    {
        private string SearchInMallURI = "/api/Search/SearchInMall";
        private string SearchInCategoryURI = "/api/Search/SearchInCategory";
        private string SearchInStoreURI = "/api/Search/SearchInStore";
        private string SearchInGroupBuyURI = "/api/Search/SearchInGroupBuy";

        private string ProductIndexProURI = "/api/Search/ProductIndexPro";
        //  private static JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        private string SearchURI;

        public ProductSearchAPI(string searchURI)
        {
            if (!string.IsNullOrEmpty(searchURI))
                SearchURI = searchURI;
            else
                SearchURI = "127.0.0.1";
        }
        /// <summary>
        /// 商城内部搜索
        /// </summary>
        /// <param name="keyWords"></param>
        /// <param name="sortMode"></param>
        /// <param name="priceRange"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ProductSearchResult SearchInMall(string keyWords, ProductIndexEnum.EnumSearchSortType sortMode, string priceRange, int pageIndex = 1, int pageSize = 30)
        {

            var uri = "http://" + SearchURI + SearchInMallURI;
            try
            {
                StringBuilder v = new StringBuilder();
                v.Append("?keyWords=").Append(keyWords).Append("&");
                v.Append("sortMode=").Append(sortMode).Append("&");
                v.Append("priceRange=").Append(priceRange).Append("&");
                v.Append("pageIndex=").Append(pageIndex).Append("&");
                v.Append("pageSize=").Append(pageSize);

                //byte[] postdata = Encoding.ASCII.GetBytes(v.ToString());

                using (WebClient webClient = new WebClient())
                {
                    var st = webClient.OpenRead(uri + v.ToString());
                    StreamReader sr = new StreamReader(st);
                    var result = sr.ReadToEnd();

                    return (ProductSearchResult)JsonConvert.DeserializeObject(result, typeof(ProductSearchResult));
                    // javascriptConvert.DeserializeObject(output, typeof(Product));
                    // return javaScriptSerializer.Deserialize<ProductSearchResult>(result);

                }
            }
            catch (Exception exception)
            {

                throw new Exception("商城搜索异常发生" + exception.Message);

            }

        }


        /// <summary>
        /// 商品索引远程更新、删除、新增、更新. 
        /// 由消息队列来调用
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="actionType"></param>
        /// <returns></returns>
        public ProductIndexAPIBaseModel UpdateProductIndex(long productId, ProductIndexEnum.EnumProductIndexAction actionType)
        {
            var uri = "http://" + SearchURI + ProductIndexProURI;

            StringBuilder v = new StringBuilder();
            v.Append("?productId=").Append(productId).Append("&");
            v.Append("actionType=").Append(actionType);

            using (WebClient webClient = new WebClient())
            {
                var st = webClient.OpenRead(uri + v.ToString());
                StreamReader sr = new StreamReader(st);
                var result = sr.ReadToEnd();

                return (ProductSearchResult)JsonConvert.DeserializeObject(result, typeof(ProductSearchResult));

            }
        }
    }
}
