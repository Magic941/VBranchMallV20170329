using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Products;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppServices.BLL
{
    public partial class SKUItem
    {
        private readonly ISKUItem dal = DAShopProducts.CreateSKUItem();
        public SKUItem()
        { }
        #region  Method


 
    
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<AppServices.Models.SKUItem> DataTableToList(DataTable dt)
        {
            List<AppServices.Models.SKUItem> modelList = new List<AppServices.Models.SKUItem>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                AppServices.Models.SKUItem model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new AppServices.Models.SKUItem();
                    if (dt.Rows[n]["SkuId"] != null && dt.Rows[n]["SkuId"].ToString() != "")
                    {
                        model.SkuId = long.Parse(dt.Rows[n]["SkuId"].ToString());
                    }
                    if (dt.Rows[n]["AttributeId"] != null && dt.Rows[n]["AttributeId"].ToString() != "")
                    {
                        model.AttributeId = long.Parse(dt.Rows[n]["AttributeId"].ToString());
                    }
                    if (dt.Rows[n]["ValueId"] != null && dt.Rows[n]["ValueId"].ToString() != "")
                    {
                        model.ValueId = long.Parse(dt.Rows[n]["ValueId"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }




        #endregion  Method

        public List<AppServices.Models.SKUItem> GetSKUItemsByProductId(long productId)
        {
            DataSet ds = dal.GetSKUItem4AttrValByProductId(productId);
            return SKUItem4AVDataTableToList(ds.Tables[0]);
        }

        public List<AppServices.Models.SKUItem> GetSKUItemsBySkuId(long skuId)
        {
            DataSet ds = dal.GetSKUItem4AttrValBySkuId(skuId);
            return SKUItem4AVDataTableToList(ds.Tables[0]);
        }



        public List<AppServices.Models.SKUItem> SKUItem4AVDataTableToList(DataTable dt)
        {
            List<AppServices.Models.SKUItem> modelList = new List<AppServices.Models.SKUItem>();
            if (dt.Rows.Count < 1) return modelList;

            AppServices.Models.SKUItem model;
            foreach (DataRow dataRow in dt.Rows)
            {
                if (dataRow["SkuId"] == DBNull.Value) continue;

                model = new AppServices.Models.SKUItem();
                model.SkuId = dataRow.Field<long>("SkuId");
                model.SpecId = dataRow.Field<long>("SpecId");
                model.AttributeId = dataRow.Field<long>("AttributeId");
                model.ValueId = dataRow.Field<long>("ValueId");
                model.ImageUrl = dataRow.Field<string>("ImageUrl");
                model.ValueStr = dataRow.Field<string>("ValueStr");
                model.ProductId = dataRow.Field<long>("ProductId");

                model.AttributeName = dataRow.Field<string>("AttributeName");
                model.AB_DisplaySequence = dataRow.Field<int>("AB_DisplaySequence");
                model.UsageMode = dataRow.Field<int>("UsageMode");
                model.UseAttributeImage = dataRow.Field<bool>("UseAttributeImage");
                model.UserDefinedPic = dataRow.Field<bool>("UserDefinedPic");

                model.AV_DisplaySequence = dataRow.Field<int>("AV_DisplaySequence");
                model.AV_ValueStr = dataRow.Field<string>("AV_ValueStr");
                model.AV_ImageUrl = dataRow.Field<string>("AV_ImageUrl");

                modelList.Add(model);
            }

            return modelList;
        }

    }
}