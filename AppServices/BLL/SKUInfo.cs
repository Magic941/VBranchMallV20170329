using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppServices.BLL
{
    public class SKUInfo
    {
        private readonly ISKUInfo dal = DAShopProducts.CreateSKUInfo();
        public List<AppServices.Models.SKUInfo> GetProductSkuInfo(long productId)
        {
            DataSet ds = dal.PrductsSkuInfo(productId);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ProductSkuDataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }
        #region ProductSkuDataTableToList
        public List<AppServices.Models.SKUInfo> ProductSkuDataTableToList(DataTable dt)
        {
            List<AppServices.Models.SKUInfo> modelList = new List<AppServices.Models.SKUInfo>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                AppServices.Models.SKUInfo model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new AppServices.Models.SKUInfo();
                    if (dt.Rows[n]["SkuId"] != null && dt.Rows[n]["SkuId"].ToString() != "")
                    {
                        model.SkuId = long.Parse(dt.Rows[n]["SkuId"].ToString());
                    }
                    if (dt.Rows[n]["ProductId"] != null && dt.Rows[n]["ProductId"].ToString() != "")
                    {
                        model.ProductId = long.Parse(dt.Rows[n]["ProductId"].ToString());
                    }
                    if (dt.Rows[n]["SKU"] != null && dt.Rows[n]["SKU"].ToString() != "")
                    {
                        model.SKU = dt.Rows[n]["SKU"].ToString();
                    }
                    if (dt.Rows[n]["Weight"] != null && dt.Rows[n]["Weight"].ToString() != "")
                    {
                        model.Weight = int.Parse(dt.Rows[n]["Weight"].ToString());
                    }
                    if (dt.Rows[n]["Stock"] != null && dt.Rows[n]["Stock"].ToString() != "")
                    {
                        model.Stock = int.Parse(dt.Rows[n]["Stock"].ToString());
                    }
                    if (dt.Rows[n]["AlertStock"] != null && dt.Rows[n]["AlertStock"].ToString() != "")
                    {
                        model.AlertStock = int.Parse(dt.Rows[n]["AlertStock"].ToString());
                    }
                    if (dt.Rows[n]["CostPrice"] != null && dt.Rows[n]["CostPrice"].ToString() != "")
                    {
                        model.CostPrice = decimal.Parse(dt.Rows[n]["CostPrice"].ToString());
                    }
                    if (dt.Rows[n]["CostPrice2"] != null && dt.Rows[n]["CostPrice2"].ToString() != "")
                    {
                        model.CostPrice2 = decimal.Parse(dt.Rows[n]["CostPrice2"].ToString());
                    }
                    if (dt.Rows[n]["SalePrice"] != null && dt.Rows[n]["SalePrice"].ToString() != "")
                    {
                        model.SalePrice = decimal.Parse(dt.Rows[n]["SalePrice"].ToString());
                    }
                    if (dt.Rows[n]["Upselling"] != null && dt.Rows[n]["Upselling"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Upselling"].ToString() == "1") || (dt.Rows[n]["Upselling"].ToString().ToLower() == "true"))
                        {
                            model.Upselling = true;
                        }
                        else
                        {
                            model.Upselling = false;
                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
        #endregion
    }
}