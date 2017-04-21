using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppServices.BLL
{
    public class ProductImage
    {
        private readonly IProductImage dal = DAShopProducts.CreateProductImage();
        public List<AppServices.Models.ProductImage> GetModelList(long productId)
        {

            DataSet ds = dal.GetList(string.Format(" ProductId={0}", productId));
            if (ds != null && ds.Tables.Count > 0)
            {
                return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AppServices.Models.ProductImage> DataTableToList(DataTable dt)
        {
            List<AppServices.Models.ProductImage> modelList = new List<AppServices.Models.ProductImage>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                AppServices.Models.ProductImage model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new AppServices.Models.ProductImage();
                    if (dt.Rows[n]["ProductImageId"] != null && dt.Rows[n]["ProductImageId"].ToString() != "")
                    {
                        model.ProductImageId = int.Parse(dt.Rows[n]["ProductImageId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "")
                    {
                        model.ThumbnailUrl1 = dt.Rows[n]["ThumbnailUrl1"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl2"] != null && dt.Rows[n]["ThumbnailUrl2"].ToString() != "")
                    {
                        model.ThumbnailUrl2 = dt.Rows[n]["ThumbnailUrl2"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl3"] != null && dt.Rows[n]["ThumbnailUrl3"].ToString() != "")
                    {
                        model.ThumbnailUrl3 = dt.Rows[n]["ThumbnailUrl3"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl4"] != null && dt.Rows[n]["ThumbnailUrl4"].ToString() != "")
                    {
                        model.ThumbnailUrl4 = dt.Rows[n]["ThumbnailUrl4"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl5"] != null && dt.Rows[n]["ThumbnailUrl5"].ToString() != "")
                    {
                        model.ThumbnailUrl5 = dt.Rows[n]["ThumbnailUrl5"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl6"] != null && dt.Rows[n]["ThumbnailUrl6"].ToString() != "")
                    {
                        model.ThumbnailUrl6 = dt.Rows[n]["ThumbnailUrl6"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl7"] != null && dt.Rows[n]["ThumbnailUrl7"].ToString() != "")
                    {
                        model.ThumbnailUrl7 = dt.Rows[n]["ThumbnailUrl7"].ToString();
                    }
                    if (dt.Rows[n]["ThumbnailUrl8"] != null && dt.Rows[n]["ThumbnailUrl8"].ToString() != "")
                    {
                        model.ThumbnailUrl8 = dt.Rows[n]["ThumbnailUrl8"].ToString();
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

    }
}