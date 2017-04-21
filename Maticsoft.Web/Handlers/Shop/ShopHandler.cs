/**
* ShopHandler.cs
*
* 功 能： [N/A]
* 类 名： ShopHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/27 13:12:07  Rock    初版
* V0.02  2012/6/14 20:09:05  Ben     1. 新增Json模式
* 　　　　　　　　　　　　　　　　　 2. 产品类型相关操作
* 　　　　　　　　　　　　　　　　　 3. 品牌json版相关操作
* 　　　　　　　　　　　　　　　　　 4. 属性/规格相关操作
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
using Maticsoft.BLL.Shop.Sales;
using Maticsoft.Json;
using Maticsoft.BLL.Shop.Package;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Handlers.Shop
{
    public class ShopHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        public const string SHOP_KEY_STATUS = "STATUS";
        public const string SHOP_KEY_DATA = "DATA";

        public const string SHOP_STATUS_SUCCESS = "SUCCESS";
        public const string SHOP_STATUS_FAILED = "FAILED";
        public const string SHOP_STATUS_ERROR = "ERROR";

        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有产品相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            string msg = "";
            try
            {
                switch (action)
                {
                    #region 属性/规格

                    case "GetAttributesList":
                        GetAttributesList(context);
                        break;

                    case "EditValue":
                        EditValue(context);
                        break;

                    #endregion 属性/规格

                    #region 产品类型

                    case "GetProductTypesKVList":
                        GetProductTypesKVList(context);
                        break;

                    #endregion 产品类型

                    #region 品牌

                    case "GetBrandsKVList":
                        GetBrandsKVList(context);
                        break;

                    case "GetBrandsList":
                        GetBrandsInfo(context);
                        break;

                    case "SearchBrandsList":
                        SearchBrandsInfo(context);
                        break;

                    case "DeleteBrands":
                        DeleteBrands(context);
                        break;

                    case "DeleteImage":
                        DeleteImage(context);
                        break;

                    #endregion 品牌

                    #region 店铺分类

                    case "GetChildNode":
                        GetChildNode(context);
                        break;

                    case "GetDepthNode":
                        GetDepthNode(context);
                        break;

                    case "GetParentNode":
                        GetParentNode(context);
                        break;

                    case "IsExistedProduct":
                        IsExistedProduct(context);
                        break;

                    #endregion 店铺分类

                    #region 礼品分类

                    case "GetGiftChildNode":
                        GetGiftChildNode(context);
                        break;

                    case "GetGiftDepthNode":
                        GetGiftDepthNode(context);
                        break;

                    case "GetGiftParentNode":
                        GetGiftParentNode(context);
                        break;

                    case "IsExistedGift":
                        IsExistedGift(context);
                        break;

                    #endregion 礼品分类

                    #region 社区商品分类

                    case "GetSNSChildNode":
                        GetSNSChildNode(context);
                        break;

                    case "GetSNSDepthNode":
                        GetSNSDepthNode(context);
                        break;

                    case "GetSNSParentNode":
                        GetSNSParentNode(context);
                        break;

                    case "IsExistedSNSCate":
                        IsExistedSNSCate(context);
                        break;

                    #endregion 社区商品分类

                    #region 淘宝商品分类

                    case "GetTaoBaoChildNode":
                        GetTaoBaoChildNode(context);
                        break;

                    case "GetTaoBaoDepthNode":
                        GetTaoBaoDepthNode(context);
                        break;

                    case "GetTaoBaoParentNode":
                        GetTaoBaoParentNode(context);
                        break;

                    case "IsExistedTaoBaoCate":
                        IsExistedTaoBaoCate(context);
                        break;

                    #endregion 淘宝商品分类

                    case "ProductInfo":
                        ProductInfo(context);
                        break;

                    case "LoadExistAttributes":
                        LoadExistAttributes(context);
                        break;

                    case "LoadAttributesvalues":
                        LoadAttributesvalues(context);
                        break;

                    case "ProductSkuInfo":
                        ProductSkuInfo(context);
                        break;

                    case "ProductSkusInfo":
                        ProductSkusInfo(context);
                        break;

                    case "ProductAccessoriesManage":
                        ProductAccessoriesManage(context);
                        break;

                    case "ProductAccessoriesValues":
                        ProductAccessoriesValues(context);
                        break;

                    case "RelatedProductFactory":
                        RelatedProductFactory(context);
                        break;

                    case "ProductIamges":
                        ProductIamges(context);
                        break;

                    case "GetPackage":
                        GetPackageNode(context);
                        break;

                    case "IsExistSkuCode":
                        IsExistSkuCode(context);
                        break;

                    #region 添加、删除商品推荐

                    case "InsertProductStationMode":
                        msg = InsertProductStationMode(context);
                        break;

                    case "RemoveProductStationMode":
                        msg = RemoveProductStationMode(context);
                        break;

                    case "SEORelation":
                        SEORelation(context);
                        break;

                    #endregion 添加、删除商品推荐

                    #region 添加、删除免邮单品
                    case "InsertFreeFreight":
                        InsertFreeFreight(context);
                        break;
                    case "RemoveFreeFreight": RemoveFreeFreight(context); break;
                    #endregion

                    #region  添加，删除批发规则商品
                    case "AddRuleProduct":
                        msg = AddRuleProduct(context);
                        break;

                    case "DeleteRuleProduct":
                        msg = DeleteRuleProduct(context);
                        break;
                    #endregion

                    #region 添加、删除活动商品
                    case "AddAMProduct":
                        msg = AddAMProduct(context);
                        break;
                    case "DeleteAMProduct":
                        msg = DeleteAMProduct(context);
                        break;
                    #endregion
                    #region 店铺商品分类
                    case "GetSuppCateNode":
                        GetSuppCateNode(context);
                        return;
                    #endregion

                    #region 豪礼大派送
                    case "":
                        break;
                    #endregion

                    #region 修改试用好代状态
                    case "UpdateGoodUserProbation":
                        msg = UpdateGoodUserProbation(context);
                        break;
                    #endregion

                    #region 修改已选商品排序
                    case "UpdatestationModeSequence":
                        msg=UpdatestationModeSequence(context);
                        break;
                    #endregion

                    default:
                        break;
                }
                context.Response.Write(msg);
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                json.Put(SHOP_KEY_DATA, ex.Message);
                context.Response.Write(json.ToString());
            }
        }

        #endregion IHttpHandler 成员

        #region SEO关联链接

        private void SEORelation(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string IsCMS = context.Request.Params["IsCMS"];
            string IsShop = context.Request.Params["IsShop"];
            string IsSNS = context.Request.Params["IsSNS"];
            string IsComment = context.Request.Params["IsComment"];
            System.Text.StringBuilder strWhere = new System.Text.StringBuilder();
            strWhere.Append(" IsActive=1 ");
            if (!string.IsNullOrWhiteSpace(IsCMS) && bool.Parse(IsCMS))
            {
                strWhere.AppendFormat(" AND IsCMS=1 ");
            }
            if (!string.IsNullOrWhiteSpace(IsShop) && bool.Parse(IsShop))
            {
                strWhere.AppendFormat(" AND IsShop=1 ");
            }
            if (!string.IsNullOrWhiteSpace(IsSNS) && bool.Parse(IsSNS))
            {
                strWhere.AppendFormat(" AND IsSNS=1 ");
            }
            if (!string.IsNullOrWhiteSpace(IsComment) && bool.Parse(IsComment))
            {
                strWhere.AppendFormat(" AND IsComment=1 ");
            }
            if (!string.IsNullOrWhiteSpace(strWhere.ToString()))
            {
                BLL.Settings.SEORelation manage = new BLL.Settings.SEORelation();
                List<Model.Settings.SEORelation> list = manage.GetModelList(strWhere.ToString());
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(info => data.Add(new JsonObject(new string[] { "KeyName", "LinkURL" }, new object[] { info.KeyName, info.LinkURL })));

                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        #endregion SEO关联链接

        #region 检查商品编码是否存在

        private void IsExistSkuCode(HttpContext context)
        {
            string SKUCode = context.Request.Form["SKUCode"];
            long pid = Globals.SafeLong(context.Request.Form["pid"], -1);
            JsonObject json = new JsonObject();
            if (!string.IsNullOrWhiteSpace(SKUCode))
            {
                BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
                if (manage.Exists(SKUCode, pid))
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
            }
            context.Response.Write(json.ToString());
        }

        #endregion 检查商品编码是否存在

        #region 添加、删除商品推荐

        private BLL.Shop.Products.ProductStationMode productStationModeBLL = new BLL.Shop.Products.ProductStationMode();

        private string InsertProductStationMode(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int type = Convert.ToInt32(context.Request.Form["Type"]);
            int floor = Convert.ToInt32(context.Request.Form["floor"]);
            int sort = Convert.ToInt32(context.Request.Form["Sort"]);



            int GoodTypeID = Convert.ToInt32(context.Request.Form["GoodType"]);//商品分类编号

            if (productStationModeBLL.Exists(productId, type))
            {
                json.Put(SHOP_KEY_STATUS, "Presence");
                return json.ToString();
            }

            Maticsoft.BLL.Shop.Products.ProductInfo productBll = new BLL.Shop.Products.ProductInfo();

            Maticsoft.BLL.Shop.Products.ProductStationMode StationBll = new BLL.Shop.Products.ProductStationMode();


            ProductStationMode PStationMode = StationBll.GetProductStationModel(productId, type);

            if (PStationMode != null)
            {
                productBll.UpdateRecommend(productId, 1); //修改商品
            }
            else if (PStationMode == null)
            {
                ProductStationMode productStationMode = new ProductStationMode();
                productStationMode.ProductId = productId;
                productStationMode.DisplaySequence = productStationModeBLL.GetRecordCount(string.Empty) == 0 ? 1 : productStationModeBLL.GetRecordCount(string.Empty) + 1;
                productStationMode.Type = type;
                productStationMode.Floor = floor;
                productStationMode.Sort = sort;
                productStationMode.GoodTypeID = GoodTypeID;
                if (productStationModeBLL.Add(productStationMode) > 0)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, "Approve");
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, "NODATA");
                    return json.ToString();
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }

        //删除
        private string RemoveProductStationMode(HttpContext context)
        {
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int type = Convert.ToInt32(context.Request.Form["Type"]);
            JsonObject json = new JsonObject();

            Maticsoft.BLL.Shop.Products.ProductInfo productBll = new BLL.Shop.Products.ProductInfo();
            if (productId > 0)
            {
                productBll.UpdateRecommend(productId, 0); //删除商品
            }

            if (productStationModeBLL.Delete(productId, type))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }

        #endregion 添加、删除商品推荐

        #region 添加、删除免邮单品
        BLL.Shop.Shipping.Shop_freefreight _FreeBll = new BLL.Shop.Shipping.Shop_freefreight();
        private string InsertFreeFreight(HttpContext context)
        {
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int type = Convert.ToInt32(context.Request.Form["Type"]);
            int quantity = Convert.ToInt32(context.Request.Form["Quantity"]);
            DateTime start = Convert.ToDateTime(context.Request.Form["StartDate"]);
            DateTime end = Convert.ToDateTime(context.Request.Form["EndDate"]);
            int UserId = Convert.ToInt32(context.Request.Form["UserId"]);

            Model.Shop.Shipping.Shop_freefreight FreeFreight = new Model.Shop.Shipping.Shop_freefreight() { StartDate = start, EndDate = end, ProductId = productId, FreeType = 2, Quantity = quantity, createdate = DateTime.Now, createrid = UserId };
            if (_FreeBll.Add(FreeFreight) > 0)
            {
                return "success";
            }
            else
            {
                return "fail";
            }
        }

        private string RemoveFreeFreight(HttpContext context)
        {
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int type = Convert.ToInt32(context.Request.Form["Type"]);
            if (_FreeBll.Delete(string.Format(" ProductId={0} and FreeType={1}", productId, type)))
            {
                return "suceess";
            }
            else
            {
                return "fail";
            }
        }
        #endregion

        #region 添加、删除批发规则商品

        private BLL.Shop.Sales.SalesRuleProduct ruleProductBll = new SalesRuleProduct();

        private string AddRuleProduct(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int ruleId = Convert.ToInt32(context.Request.Form["RuleId"]);
            string productName = context.Request.Form["ProductName"];

            if (ruleProductBll.Exists(ruleId, productId))
            {
                json.Put(SHOP_KEY_STATUS, "Presence");
                return json.ToString();
            }
            Maticsoft.Model.Shop.Sales.SalesRuleProduct ruleProductModel = new Model.Shop.Sales.SalesRuleProduct();
            ruleProductModel.ProductId = productId;
            ruleProductModel.RuleId = ruleId;
            ruleProductModel.ProductName = productName;

            if (ruleProductBll.Add(ruleProductModel))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, "Approve");
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }

        //删除
        private string DeleteRuleProduct(HttpContext context)
        {
            int productId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int ruleId = Convert.ToInt32(context.Request.Form["RuleId"]);
            JsonObject json = new JsonObject();
            if (ruleProductBll.Delete(ruleId, productId))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }

        #endregion 添加、删除商品推荐

        #region 添加、删除活动商品

        private BLL.Shop.ActivityManage.AMPBLL ampBll = new BLL.Shop.ActivityManage.AMPBLL();

        private string AddAMProduct(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int ProductId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int AMId = Convert.ToInt32(context.Request.Form["AMId"]);
            string ProductName = context.Request.Form["ProductName"];

            if (ampBll.Exists(AMId, ProductId))
            {
                json.Put(SHOP_KEY_STATUS, "Presence");
                return json.ToString();
            }
            Maticsoft.Model.Shop.ActivityManage.AMPModel ampModel = new Model.Shop.ActivityManage.AMPModel();
            ampModel.ProductId = ProductId;
            ampModel.AMId = AMId;
            ampModel.ProductName = ProductName;

            if (ampBll.Add(ampModel))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, "Approve");
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }

        //删除
        private string DeleteAMProduct(HttpContext context)
        {
            int ProductId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int AMId = Convert.ToInt32(context.Request.Form["AMId"]);
            JsonObject json = new JsonObject();
            if (ampBll.Delete(AMId, ProductId))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }

        #endregion 添加、删除商品推荐

        #region 根据商品ID获取商品信息

        #region 获取商品相关配件信息

        private void ProductAccessoriesValues(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(strPid))
            {
                long pid = Globals.SafeLong(strPid, -1);
                BLL.Shop.Products.AccessoriesValue manage = new BLL.Shop.Products.AccessoriesValue();
                List<Model.Shop.Products.AccessoriesValue> list = null;//TODO  //manage.AccessoriesByProductId(pid);
                if (list != null && list.Count > 0)
                {
                    System.Text.StringBuilder strAccValues = new System.Text.StringBuilder();
                    list.ForEach(info =>
                    {
                        strAccValues.Append(info.SKU);
                        strAccValues.Append(",");
                    });
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, strAccValues.ToString());
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        #endregion 获取商品相关配件信息

        #region 获取单个商品的相关商品信息

        private void RelatedProductFactory(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(strPid))
            {
                long pid = Globals.SafeLong(strPid, -1);
                BLL.Shop.Products.RelatedProduct manage = new BLL.Shop.Products.RelatedProduct();
                List<Model.Shop.Products.RelatedProduct> list = manage.GetModelList(pid);
                if (list != null && list.Count > 0)
                {
                    System.Text.StringBuilder strReleatedInfo = new System.Text.StringBuilder();
                    list.ForEach(info =>
                    {
                        strReleatedInfo.Append(info.RelatedId);
                        strReleatedInfo.Append(",");
                    });
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, strReleatedInfo.ToString());
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        #endregion 获取单个商品的相关商品信息

        #region 商品配件基础信息

        private void ProductAccessoriesManage(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            string strtype = context.Request.Params["actype"];
            if (!string.IsNullOrWhiteSpace(strPid))
            {
                long pid = Globals.SafeLong(strPid, -1);
                int type = Globals.SafeInt(strtype, -1);
                BLL.Shop.Products.ProductAccessorie manage = new BLL.Shop.Products.ProductAccessorie();
                List<Model.Shop.Products.ProductAccessorie> list = manage.GetModelList(pid, type);
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(info => data.Add(new JsonObject(new string[] { "ProductId", "AccessoriesId", "Name", "MaxQuantity", "MinQuantity", "DiscountType", "DiscountAmount", "Stock" },
                        new object[] { info.ProductId, info.AccessoriesId, info.Name, info.MaxQuantity, info.MinQuantity, info.DiscountType, info.DiscountAmount, info.Stock })));
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        #endregion 商品配件基础信息

        #region 商品规格值信息

        private void ProductSkuInfo(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(strPid))
            {
                long pid = Globals.SafeLong(strPid, -1);
                BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
                List<Model.Shop.Products.SKUInfo> list = manage.GetProductSkuInfo(pid);
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(info => data.Add(new JsonObject(
                        new string[] { "SkuId", "ProductId", "SKU", "Weight", "Stock", "AlertStock", "CostPrice", "SalePrice", "Upselling" },
                        new object[] { info.SkuId, info.ProductId, info.SKU, info.Weight, info.Stock, info.AlertStock, info.CostPrice, info.SalePrice, info.Upselling })));
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void ProductSkusInfo(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            string strSpecs = context.Request.Params["specs"];

            IList<string[]> lstSpec = new List<string[]>();
            #region 解析参数
            // 将 a_b|c_d|e_f .... 格式的数据解析到集合中
            if (!string.IsNullOrEmpty(strSpecs))
            {
                if (strSpecs.IndexOf("|") >= 0)
                {
                    string[] tempArr = strSpecs.Split("|".ToCharArray());
                    foreach (string item in tempArr)
                    {
                        if (item.IndexOf("_") >= 0)
                        {
                            lstSpec.Add(item.Split("_".ToCharArray()));
                        }
                    }
                }
                else
                {
                    if (strSpecs.IndexOf("_") >= 0)
                    {
                        lstSpec.Add(strSpecs.Split("_".ToCharArray()));
                    }
                }
            }
            #endregion

            // 解析产品ID
            long pid = 0;
            if (!string.IsNullOrWhiteSpace(strPid))
            {
                pid = Globals.SafeLong(strPid, -1);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                context.Response.Write(json.ToString());
            }

            StringBuilder sbWhere = new StringBuilder();
            if (pid > 0) sbWhere.Append("ProductId = " + pid);
            if (lstSpec.Count > 0)
            {
                if (sbWhere.Length > 0) sbWhere.Append(" And ");
                sbWhere.Append("SkuId IN (");
                sbWhere.Append("Select DISTINCT sr.SkuId From (");
                for (int i = 0; i < lstSpec.Count; i++)
                {
                    if (i > 0) sbWhere.Append(" UNION ALL ");
                    sbWhere.Append("SELECT ProductId,SpecId FROM shop_skuitems WHERE ProductId =" + pid + " AND AttributeId = " + (string.IsNullOrEmpty(lstSpec[i][0]) ? "0" : lstSpec[i][0]) + " AND ValueId = " + (string.IsNullOrEmpty(lstSpec[i][1]) ? "0" : lstSpec[i][1]));
                }
                sbWhere.Append(") tmp ");
                sbWhere.Append("LEFT JOIN Shop_SKURelation sr ON sr.ProductId = tmp.ProductId AND sr.SpecId = tmp.SpecId ");
                sbWhere.Append("GROUP BY sr.SkuId ");
                sbWhere.Append("HAVING COUNT(sr.SkuId) = " + lstSpec.Count);
                sbWhere.Append(")");
            }

            BLL.Shop.Products.SKUInfo manage = new BLL.Shop.Products.SKUInfo();
            List<Model.Shop.Products.SKUInfo> list = manage.GetProductSkuInfo(sbWhere.ToString());
            if (list != null && list.Count > 0)
            {
                JsonArray data = new JsonArray();
                list.ForEach(info => data.Add(new JsonObject(
                    new string[] { "SkuId", "ProductId", "SKU", "Weight", "Stock", "AlertStock", "CostPrice","CostPrice2", "SalePrice", "Upselling" },
                    new object[] { info.SkuId, info.ProductId, info.SKU, info.Weight, info.Stock, info.AlertStock, info.CostPrice, info.CostPrice2, info.SalePrice, info.Upselling })));
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }

            context.Response.Write(json.ToString());
        }

        #endregion 商品规格值信息

        private void ProductIamges(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
            if (!string.IsNullOrWhiteSpace(strPid))
            {

                long pid = Globals.SafeLong(strPid, -1);
                BLL.Shop.Products.ProductImage manage = new BLL.Shop.Products.ProductImage();
                //商品的其它图片
                List<Model.Shop.Products.ProductImage> list = manage.GetModelList(pid);
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(info => data.Add(new JsonObject(
                        new string[] { "ProductImageId", "ProductId", "ImageUrl", "ThumbnailUrl1", "ThumbnailUrl2" },
                        new object[] { info.ProductImageId, info.ProductId, info.ImageUrl, info.ThumbnailUrl1, info.ThumbnailUrl2 }
                        )));
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }

            json.Put("PicServerUrl", picServerUrl);
            //转化成字符串
            context.Response.Write(json.ToString());
        }

        private void LoadAttributesvalues(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(strPid))
            {
                long pid = Globals.SafeLong(strPid, -1);
                BLL.Shop.Products.SKUItem manage = new BLL.Shop.Products.SKUItem();
                List<Model.Shop.Products.SKUItem> list = manage.AttributeValueInfo(pid);
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(info => data.Add(new JsonObject(new string[] { "SpecId", "AttributeId", "ValueId", "ImageUrl", "ValueStr", "UserDefinedPic" }, new object[] { info.SpecId, info.AttributeId, info.ValueId, info.ImageUrl, info.ValueStr, info.UserDefinedPic })));

                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
            json.Put("PicServerUrl", picServerUrl);

            context.Response.Write(json.ToString());
        }

        private void ProductInfo(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
            if (!string.IsNullOrWhiteSpace(strPid))
            {


                long pid = Globals.SafeLong(strPid, -1);
                BLL.Shop.Products.ProductInfo manage = new BLL.Shop.Products.ProductInfo();
                List<Model.Shop.Products.ProductInfo> list = manage.GetModelList(pid);
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(info => data.Add(new JsonObject(
                        new string[] { "CategoryId", "TypeId", "ProductId", "BrandId", "ProductName", "ProductCode", "EnterpriseId", "RegionId", "ShortDescription", "Unit", "Description", "Title", "Meta_Description", "Meta_Keywords", "DisplaySequence", "MarketPrice", "HasSKU", "ImageUrl", "ThumbnailUrl1", "MaxQuantity", "MinQuantity", "SaleStatus", "SaleCounts" },
                        new object[] { info.CategoryId, info.TypeId, info.ProductId, info.BrandId, info.ProductName, info.ProductCode, info.SupplierId, info.RegionId, info.ShortDescription, info.Unit, info.Description, info.Meta_Title, info.Meta_Description, info.Meta_Keywords, info.DisplaySequence, info.MarketPrice, info.HasSKU, info.ImageUrl, info.ThumbnailUrl1, info.MaxQuantity, info.MinQuantity, info.SaleStatus, info.SaleCounts }
                        )));

                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            json.Put("PicServerUrl", picServerUrl);
            context.Response.Write(json.ToString());
        }

        private void LoadExistAttributes(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strPid = context.Request.Params["pid"];
            if (!string.IsNullOrWhiteSpace(strPid))
            {
                long pid = Globals.SafeLong(strPid, -1);
                BLL.Shop.Products.AttributeInfo manage = new BLL.Shop.Products.AttributeInfo();
                List<Model.Shop.Products.AttributeHelper> list = manage.ProductAttributeInfo(pid);
                if (list != null && list.Count > 0)
                {
                    JsonArray data = new JsonArray();
                    list.ForEach(
                        info =>
                            data.Add(new JsonObject(new string[] { "AttributeId", "ValueId", "UsageMode", "ValueStr" },
                                new object[] { info.AttributeId, info.ValueId, info.UsageMode, info.ValueStr })));
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    json.Put(SHOP_KEY_DATA, data);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        #endregion 根据商品ID获取商品信息

        #region 店铺分类

        private BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();

        private void IsExistedProduct(HttpContext context)
        {
            string CategoryIdStr = context.Request.Params["CategoryId"];
            int cateId = Globals.SafeInt(CategoryIdStr, -2);
            JsonObject json = new JsonObject();
            if (cateBll.IsExistedProduce(cateId))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = cateBll.GetCategorysByParentIdDs(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(SHOP_KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetPackageNode(HttpContext context)
        {
            Maticsoft.BLL.Shop.Package.Package bll = new Package();
            int CategoryId = Globals.SafeInt(context.Request.Params["id"], 0);
            string keyword = context.Request.Params["q"];
            JsonObject json = new JsonObject();
            StringBuilder sb = new StringBuilder();
            if (CategoryId > 0)
            {
                sb.Append(" CategoryId =" + CategoryId + "");
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                if (sb.Length > 0)
                {
                    sb.Append(" and ");
                }
                sb.Append(" Name like '%" + keyword + "%'");
            }
            DataSet ds = null;
            if (sb.Length > 0)
            {
                ds = bll.GetList(sb.ToString());
            }
            if (ds == null || ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate("DATA", ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.Shop.Products.CategoryInfo> list;
            if (nodeId > 0)
            {
                Model.Shop.Products.CategoryInfo model = cateBll.GetModel(nodeId);
                list = cateBll.GetCategorysByDepth(model.Depth);
            }
            else
            {
                list = cateBll.GetCategorysByDepth(1);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryId, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = cateBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.Shop.Products.CategoryInfo model = cateBll.GetModel(ParentId);
                if (model != null)
                {
                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentCategoryId=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentCategoryId=" + strList[i - 1]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }

        #endregion 店铺分类

        #region 礼品分类

        private BLL.Shop.Gift.GiftsCategory giftCateBll = new BLL.Shop.Gift.GiftsCategory();

        private void IsExistedGift(HttpContext context)
        {
            string CategoryIdStr = context.Request.Params["CategoryId"];
            int cateId = Globals.SafeInt(CategoryIdStr, -2);
            JsonObject json = new JsonObject();
            if (giftCateBll.IsExistedGift(cateId))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetGiftChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = giftCateBll.GetCategorysByParentId(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(SHOP_KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetGiftDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.Shop.Gift.GiftsCategory> list;
            if (nodeId > 0)
            {
                Model.Shop.Gift.GiftsCategory model = giftCateBll.GetModel(nodeId);
                list = giftCateBll.GetCategorysByDepth(model.Depth);
            }
            else
            {
                list = giftCateBll.GetCategorysByDepth(1);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryID, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetGiftParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = giftCateBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.Shop.Gift.GiftsCategory model = giftCateBll.GetModel(ParentId);
                if (model != null)
                {
                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentCategoryId=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentCategoryId=" + strList[i - 1]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }

        #endregion 礼品分类

        #region 社区商品分类

        private Maticsoft.BLL.SNS.Categories SNSCateBll = new BLL.SNS.Categories();

        private void IsExistedSNSCate(HttpContext context)
        {
            string CategoryIdStr = context.Request.Params["CategoryId"];
            int cateId = Globals.SafeInt(CategoryIdStr, -2);
            JsonObject json = new JsonObject();
            if (SNSCateBll.IsExistedCate(cateId))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetSNSChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = SNSCateBll.GetCategorysByParentId(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(SHOP_KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetSNSDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.SNS.Categories> list;
            if (nodeId > 0)
            {
                Maticsoft.Model.SNS.Categories model = SNSCateBll.GetModel(nodeId);
                list = SNSCateBll.GetCategorysByDepth(model.Depth, 0);
            }
            else
            {
                list = SNSCateBll.GetCategorysByDepth(1, 0);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryId, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetSNSParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = SNSCateBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Maticsoft.Model.SNS.Categories model = SNSCateBll.GetModel(ParentId);
                if (model != null)
                {
                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentId=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentId=" + strList[i - 1]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }

        #endregion 社区商品分类

        #region 淘宝商品分类

        private Maticsoft.BLL.SNS.CategorySource taoBaoCateBll = new BLL.SNS.CategorySource();

        private void IsExistedTaoBaoCate(HttpContext context)
        {
            string CategoryIdStr = context.Request.Params["CategoryId"];
            int cateId = Globals.SafeInt(CategoryIdStr, -2);
            JsonObject json = new JsonObject();
            if (taoBaoCateBll.IsExistedCate(cateId))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetTaoBaoChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = taoBaoCateBll.GetCategorysByParentId(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(SHOP_KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetTaoBaoDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.SNS.CategorySource> list;
            if (nodeId > 0)
            {
                Maticsoft.Model.SNS.CategorySource model = taoBaoCateBll.GetModel(3, nodeId);
                list = taoBaoCateBll.GetCategorysByDepth(model.Depth);
            }
            else
            {
                list = taoBaoCateBll.GetCategorysByDepth(1);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.CategoryId, info.Name }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetTaoBaoParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = taoBaoCateBll.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Maticsoft.Model.SNS.CategorySource model = taoBaoCateBll.GetModel(3, ParentId);
                if (model != null)
                {
                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i <= strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentId=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentId=" + strList[i - 1]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }

        #endregion 淘宝商品分类

        #region 属性/规格

        private void EditValue(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strValue = context.Request.Form["ValueId"];
            if (!string.IsNullOrWhiteSpace(strValue))
            {
                long ValueId = Convert.ToInt64(strValue);
                BLL.Shop.Products.SKUItem skuBll = new BLL.Shop.Products.SKUItem();
                bool skuResult = skuBll.Exists(null, null, ValueId);
                BLL.Shop.Products.ProductAttribute productAttBll = new BLL.Shop.Products.ProductAttribute();
                bool productResult = productAttBll.Exists(null, null, ValueId);
                if (skuResult || productResult)
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                }
                else
                {
                    BLL.Shop.Products.ProductType ptBll = new BLL.Shop.Products.ProductType();
                    if (ptBll.DeleteManage(null, null, ValueId))
                    {
                        json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                    }
                    else
                    {
                        json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    }
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
            }
            context.Response.Write(json.ToString());
        }

        private void GetAttributesList(HttpContext context)
        {
            JsonObject json = new JsonObject();
            BLL.Shop.Products.AttributeInfo manage = new BLL.Shop.Products.AttributeInfo();
            string dataMode = context.Request.Form["DataMode"];
            int productTypeId = Globals.SafeInt(context.Request.Form["ProductTypeId"], -1);
            if (string.IsNullOrWhiteSpace(dataMode) || productTypeId < 1)
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_ERROR);
                context.Response.Write(json.ToString());
                return;
            }
            SearchType searchType;
            if (dataMode == "0")
            {
                searchType = SearchType.ExtAttribute;
            }
            else
            {
                searchType = SearchType.Specification;
            }
            List<Model.Shop.Products.AttributeInfo> list = manage.GetAttributeInfoList(productTypeId, searchType);
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "AttributeId", "AttributeName", "AttributeUsageMode", "AttributeValues", "UserDefinedPic" },
                    new object[]
                        {
                            info.AttributeId, info.AttributeName, info.UsageMode,
                            info.AttributeValues,info.UserDefinedPic

                            //info.UsageMode == 2 ? new List<AttributeValue>() : info.AttributeValues
                        }
                    )));
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(SHOP_KEY_DATA, data);
            context.Response.Write(json.ToString());
        }

        #endregion 属性/规格

        #region 产品类型

        private void GetProductTypesKVList(HttpContext context)
        {
            JsonObject json = new JsonObject();
            BLL.Shop.Products.ProductType manage = new BLL.Shop.Products.ProductType();
            List<Model.Shop.Products.ProductType> list = manage.GetProductTypes();
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "TypeId", "TypeName" },
                    new object[] { info.TypeId, info.TypeName }
                    )));
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(SHOP_KEY_DATA, data);
            context.Response.Write(json.ToString());
        }

        #endregion 产品类型

        #region 品牌

        private void GetBrandsKVList(HttpContext context)
        {
            JsonObject json = new JsonObject();
            BLL.Shop.Products.BrandInfo manage = new BLL.Shop.Products.BrandInfo();
            List<Model.Shop.Products.BrandInfo> list;
            int productTypeId = Globals.SafeInt(context.Request.Form["ProductTypeId"], -1);
            if (productTypeId < 1)
                list = manage.GetBrands();
            else
                list = manage.GetModelListByProductTypeId(productTypeId);
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "BrandId", "BrandName" },
                    new object[] { info.BrandId, info.BrandName }
                    )));
            json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
            json.Put(SHOP_KEY_DATA, data);
            context.Response.Write(json.ToString());
        }

        /// <summary>
        /// 删除选中的品牌
        /// </summary>
        private void DeleteBrands(HttpContext context)
        {
            JsonObject json = new JsonObject();
            string strID = context.Request.Params["idList"];
            if (!string.IsNullOrWhiteSpace(strID))
            {
                BLL.Shop.Products.BrandInfo bll = new BLL.Shop.Products.BrandInfo();
                BLL.Shop.Products.ProductInfo productManage = new BLL.Shop.Products.ProductInfo();
                BLL.Shop.Products.ProductTypeBrand ProductTypeBandManage = new BLL.Shop.Products.ProductTypeBrand();
                int bid = Common.Globals.SafeInt(strID, 0);
                if (productManage.ExistsBrands(bid))
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    json.Put(SHOP_KEY_DATA, "该品牌正在使用中！");
                }

                if (bll.DeleteList(strID))
                {
                    ProductTypeBandManage.Delete(null, bid);
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                }
                else
                {
                    json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                    json.Put(SHOP_KEY_DATA, "系统忙，请稍后再试！");
                }
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_FAILED);
                json.Put(SHOP_KEY_DATA, "系统忙，请稍后再试！");
            }
            context.Response.Write(json.ToString());
        }

        private void DeleteImage(HttpContext context)
        {
            string vId = context.Request.Form["ValueId"];
            int valueId = Globals.SafeInt(vId, 0);
            BLL.Shop.Products.AttributeValue bll = new BLL.Shop.Products.AttributeValue();
            if (bll.DeleteImage(valueId))
            {
                context.Response.Write(SHOP_STATUS_SUCCESS);
            }
            else
            {
                context.Response.Write(SHOP_STATUS_FAILED);
            }
        }

        /// <summary>
        /// 获取品牌列表
        /// </summary>
        private void GetBrandsInfo(HttpContext context)
        {
            string ProductTypeId = context.Request.Params["ProductTypeId"];
            string strPi = context.Request.Params["pageIndex"];
            string tabNum = context.Request.Params["TabNum"];
            int num = Globals.SafeInt(tabNum, 0);
            var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
            int intPi = 0;
            if (!int.TryParse(strPi, out intPi))//将字符串页码 转成 整型页码，如果失败，设置页码为1
            {
                intPi = 1;
            }
            int intPz = Globals.SafeInt(context.Request.Params["pageSize"], 1);
            int rowCount = 0;
            int pageCount = 0;
            if (!string.IsNullOrWhiteSpace(ProductTypeId))
            {
                Maticsoft.BLL.Shop.Products.BrandInfo bll = new Maticsoft.BLL.Shop.Products.BrandInfo();
                System.Collections.Generic.List<Model.Shop.Products.BrandInfo> list = null;
                if (num == 0)
                {
                    list = bll.GetListByProductTypeId(out rowCount, out pageCount, int.Parse(ProductTypeId), intPi, intPz, 1);
                }
                else
                {
                    list = bll.GetListByProductTypeId(out rowCount, out pageCount, int.Parse(ProductTypeId), intPi, intPz, 1);
                }

                JsonObject json = new JsonObject();
                JsonArray data = new JsonArray();
                list.ForEach(info => data.Add(
                    new JsonObject(
                        new string[] { "BrandId", "BrandName", "DisplaySequence", "Logo", "Description" },
                        new object[]
                        {
                            info.BrandId, info.BrandName, info.DisplaySequence, info.Logo,
                            InjectionFilter.HtmlFilter(info.Description)
                        }
                        )));
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
                json.Put("PicServerUrl", picServerUrl);
                json.Put("rowCount", rowCount);
                json.Put("pageCount", pageCount);
                context.Response.Write(json.ToString());
            }
        }

        /// <summary>
        /// 品牌查询
        /// </summary>
        /// <param name="context"></param>
        private void SearchBrandsInfo(HttpContext context)
        {
            string ProductTypeId = context.Request.Params["ProductTypeId"];
            string strPi = context.Request.Params["pageIndex"];
            string tabNum = context.Request.Params["TabNum"];
            string brandName = context.Request.Params["BrandName"];
            string brandSpell = context.Request.Params["BrandSpell"];
            int num = Globals.SafeInt(tabNum, 0);
            var picServerUrl = BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
            int intPi = 0;
            if (!int.TryParse(strPi, out intPi))//将字符串页码 转成 整型页码，如果失败，设置页码为1
            {
                intPi = 1;
            }
            int intPz = Globals.SafeInt(context.Request.Params["pageSize"], 1);
            int rowCount = 0;
            int pageCount = 0;
            if (!string.IsNullOrWhiteSpace(ProductTypeId))
            {
                Maticsoft.BLL.Shop.Products.BrandInfo bll = new Maticsoft.BLL.Shop.Products.BrandInfo();
                System.Collections.Generic.List<Model.Shop.Products.BrandInfo> list = null;
                string strWhere = string.Empty;
                if (!string.IsNullOrEmpty(brandName.ToString().Trim()))
                {
                    strWhere += (string.IsNullOrEmpty(strWhere) ? string.Empty : " and ") + "BrandName like '%" + brandName.ToString().Trim() + "%'";
                }
                if (!string.IsNullOrEmpty(brandSpell.ToString().Trim()))
                {
                    strWhere += (string.IsNullOrEmpty(strWhere) ? string.Empty : " and ") + "BrandSpell like '%" + brandSpell.ToString().Trim() + "%'";
                }
                list = bll.GetModelList(strWhere);
                rowCount = list.Count;
                pageCount = (int)Math.Ceiling(0.1 * rowCount / intPz);
                JsonObject json = new JsonObject();
                JsonArray data = new JsonArray();
                list.ForEach(info => data.Add(
                    new JsonObject(
                        new string[] { "BrandId", "BrandName", "DisplaySequence", "Logo", "Description" },
                        new object[]
                        {
                            info.BrandId, info.BrandName, info.DisplaySequence, info.Logo,
                            InjectionFilter.HtmlFilter(info.Description)
                        }
                        )));
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, data);
                json.Put("PicServerUrl", picServerUrl);
                json.Put("rowCount", rowCount);
                json.Put("pageCount", pageCount);
                context.Response.Write(json.ToString());
            }
        }

        #endregion 品牌
        #region 店铺商品分类

        private BLL.Shop.Supplier.SupplierCategories suppcateBll = new BLL.Shop.Supplier.SupplierCategories();
        private void GetSuppCateNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            int suppId = Globals.SafeInt(context.Request.Params["SuppId"], 0);
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = suppcateBll.GetList(string.Format(" ParentCategoryId = {0} and SupplierId={1}", parentId, suppId));
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate(SHOP_KEY_DATA, ds.Tables[0]);
            context.Response.Write(json.ToString());
        }
        //private void GetSuppCateParentNode(HttpContext context)
        //{
        //    int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
        //    JsonObject json = new JsonObject();
        //    DataSet ds = cateBll.GetList("");
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        DataTable dt = ds.Tables[0];
        //        Model.Shop.Supplier.SupplierCategories model = suppcateBll.GetModel(ParentId);
        //        if (model != null)
        //        {
        //            string[] strList = model.Path.TrimEnd('|').Split('|');
        //            if (strList.Length > 0)
        //            {
        //                List<DataRow[]> list = new List<DataRow[]>();
        //                for (int i = 0; i <= strList.Length; i++)
        //                {
        //                    DataRow[] dsParent = null;
        //                    if (i == 0)
        //                    {
        //                        dsParent = dt.Select("ParentCategoryId=0");
        //                    }
        //                    else
        //                    {
        //                        dsParent = dt.Select("ParentCategoryId=" + strList[i - 1]);
        //                    }
        //                    if (dsParent.Length > 0)
        //                    {
        //                        list.Add(dsParent);
        //                    }
        //                }
        //                json.Accumulate("STATUS", "OK");
        //                json.Accumulate("DATA", list);
        //                json.Accumulate("PARENT", strList);
        //            }
        //            else
        //            {
        //                json.Accumulate("STATUS", "NODATA");
        //                context.Response.Write(json.ToString());
        //                return;
        //            }
        //        }
        //    }

        //    context.Response.Write(json.ToString());
        //}

        #endregion 店铺分类

        #region 修改试用状态
        private string UpdateGoodUserProbation(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int UserID = Convert.ToInt32(context.Request.Form["UserID"]);
            int Valuenumber = Convert.ToInt32(context.Request.Form["Valuenumber"]);

            Maticsoft.BLL.Members.UsersExp userBll = new BLL.Members.UsersExp();
            Maticsoft.Model.Members.UsersExpModel usermodel = new Model.Members.UsersExpModel();

            usermodel.UserID = UserID;
            usermodel.Probation = Valuenumber;


            if (userBll.UpdateGoodUsersProbation(usermodel))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, "Approve");
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }
        #endregion

        #region 修改已选商品排序
        private string UpdatestationModeSequence(HttpContext context)
        {
            JsonObject json = new JsonObject();
            int ProductId = Convert.ToInt32(context.Request.Form["ProductId"]);
            int Sequence = Convert.ToInt32(context.Request.Form["Sequence"]);
            int StationId = Convert.ToInt32(context.Request.Form["StationId"]);

            BLL.Shop.Products.ProductStationMode stationModeBll = new BLL.Shop.Products.ProductStationMode();
            Model.Shop.Products.ProductStationMode stationMode = new Model.Shop.Products.ProductStationMode();

            stationMode.StationId = StationId;
            stationMode.ProductId = ProductId;
            stationMode.Sort = Sequence;

            if (stationModeBll.Update2(stationMode))
            {
                json.Put(SHOP_KEY_STATUS, SHOP_STATUS_SUCCESS);
                json.Put(SHOP_KEY_DATA, "Approve");
            }
            else
            {
                json.Put(SHOP_KEY_STATUS, "NODATA");
                return json.ToString();
            }
            return json.ToString();
        }
        #endregion


    }
}