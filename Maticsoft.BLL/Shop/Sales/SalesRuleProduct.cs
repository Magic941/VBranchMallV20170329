/**
* SalesRuleProduct.cs
*
* 功 能： N/A
* 类 名： SalesRuleProduct
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/8 18:54:58   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Sales;
using Maticsoft.Model.Shop.Products;
using Maticsoft.ViewModel.Shop;

namespace Maticsoft.BLL.Shop.Sales
{
    /// <summary>
    /// SalesRuleProduct
    /// </summary>
    public partial class SalesRuleProduct
    {
        private readonly ISalesRuleProduct dal = DAShopSales.CreateSalesRuleProduct();
        public SalesRuleProduct()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int RuleId, long ProductId)
        {
            return dal.Exists(RuleId, ProductId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Sales.SalesRuleProduct model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Sales.SalesRuleProduct model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int RuleId, long ProductId)
        {

            return dal.Delete(RuleId, ProductId);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.Sales.SalesRuleProduct GetModel(int RuleId, long ProductId)
        {

            return dal.GetModel(RuleId, ProductId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.Sales.SalesRuleProduct GetModelByCache(int RuleId, long ProductId)
        {

            string CacheKey = "SalesRuleProductModel-" + RuleId + ProductId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(RuleId, ProductId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Sales.SalesRuleProduct)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Sales.SalesRuleProduct> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.Sales.SalesRuleProduct> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.Sales.SalesRuleProduct> modelList = new List<Maticsoft.Model.Shop.Sales.SalesRuleProduct>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.Sales.SalesRuleProduct model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 删除 数据
        /// </summary>
        public bool DeleteByRule(int RuleId)
        {
            return dal.DeleteByRule(RuleId);
        }


        public DataSet GetRuleProducts(int ruleId, string categoryId, string pName)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE'%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat("AND ProductId IN (SELECT DISTINCT ProductId FROM  Shop_ProductCategories PC WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}))", categoryId);
            }
            return dal.GetRuleProducts(ruleId, strWhere.ToString());
        }

        /// <summary>
        /// 批量删除数据 （这个是联合主键，需要采用特殊的方式处理）
        /// </summary>
        /// <param name="idlist"></param>
        /// <returns></returns>
        public bool DeleteList(string idlist)
        {
            var ids_arr = idlist.Split(',');
            bool IsSuccess = true;
            foreach (var idStr in ids_arr)
            {
                if (!String.IsNullOrWhiteSpace(idStr))
                {
                    int ruleId = Common.Globals.SafeInt(idStr.Split('|')[0], 0);
                    long productId = Common.Globals.SafeLong(idStr.Split('|')[1], 0);
                    if (!Delete(ruleId, productId))
                    {
                        IsSuccess = false;
                    }
                }
            }
            return IsSuccess;
        }


        public Maticsoft.Model.Shop.Sales.SalesRuleProduct GetRuleProduct(long productId)
        {
            string CacheKey = "GetRuleProduct-" + productId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    List<Maticsoft.Model.Shop.Sales.SalesRuleProduct> ruleProducts = GetModelList(" ProductId=" + productId);
                    if (ruleProducts != null)
                    {
                        if (ruleProducts.Count > 0)
                        {
                            objModel = ruleProducts[0];
                        }
                    }
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.Sales.SalesRuleProduct)objModel;

        }

        /// <summary>
        /// 获取批发优惠
        /// </summary>
        /// <param name="cartInfo"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ShoppingCartInfo GetWholeSale(
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo)
        {
            Dictionary<Maticsoft.Model.Shop.Sales.SalesRule, List<Maticsoft.Model.Shop.Products.ShoppingCartItem>> dictionary = new Dictionary<Model.Shop.Sales.SalesRule, List<ShoppingCartItem>>();
            //批发规则处理
            foreach (var cartItem in cartInfo.Items)
            {
                GetCartItem(cartItem, dictionary);
            }

            //商品总计的情况处理
            if (dictionary != null && dictionary.Count > 0)
            {
                foreach (var dic in dictionary)
                {
                    Maticsoft.Model.Shop.Sales.SalesRule ruleModel = dic.Key;
                    if (ruleModel != null)
                    {
                        cartInfo.Items.RemoveAll(c => dic.Value.Contains(c));
                        List<Maticsoft.Model.Shop.Products.ShoppingCartItem> cartItems = GetRateValueList(ruleModel.RuleId, ruleModel.RuleUnit, dic.Value);
                        foreach (var item in cartItems)
                        {
                            item.SaleDes = ruleModel.RuleName;
                            cartInfo.Items.Add(item);
                        }
                    }
                }
            }
            return cartInfo;
        }
        //获取订单优惠值
        public void GetCartItem(Maticsoft.Model.Shop.Products.ShoppingCartItem cartItem,
            Dictionary<Maticsoft.Model.Shop.Sales.SalesRule, List<Maticsoft.Model.Shop.Products.ShoppingCartItem>> dictionary)
        {
            Maticsoft.Model.Shop.Sales.SalesRuleProduct RuleProductModel = GetRuleProduct(cartItem.ProductId);
            //如果没有对应规则，直接返回订单项
            if (RuleProductModel == null)
            {
                return;
            }
            Maticsoft.BLL.Shop.Sales.SalesRule ruleBll = new SalesRule();
            Maticsoft.Model.Shop.Sales.SalesRule RuleModel = ruleBll.GetModelByCache(RuleProductModel.RuleId);
            //不存在该批发规则，或者该规则不启用
            if (RuleModel == null || RuleModel.Status == 0)
            {
                return;
            }

            //TODO: 优惠规则未与等级关联
            //检测会员等级限制
            //if (RankIsLimit(RuleModel.RuleId, cartItem.UserId))
            //{
            //    return;
            //}

            //优惠名称 暂时使用规则名称
            cartItem.SaleDes = RuleModel.RuleName;

            //单个商品模式处理
            if (RuleModel.RuleMode == 0)
            {
                //计算商品优惠值
                GetRateValue(RuleModel.RuleId, RuleModel.RuleUnit, cartItem);
            }
            //商品总计的情况处理
            else
            {
                if (dictionary.ContainsKey(RuleModel))
                {
                    dictionary[RuleModel].Add(cartItem);
                }
                else
                {
                    dictionary.Add(RuleModel, new List<ShoppingCartItem> { cartItem });
                }
            }
        }

        /// <summary>
        /// 商品总计的情况处理
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="ruleUnit"></param>
        /// <param name="cartItems"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Products.ShoppingCartItem> GetRateValueList(int ruleId, int ruleUnit,
                                                                                     List<Maticsoft.Model.Shop.Products.ShoppingCartItem> cartItems)
        {
            Maticsoft.BLL.Shop.Sales.SalesItem itemBll = new SalesItem();
            List<Maticsoft.Model.Shop.Sales.SalesItem> itemList = itemBll.GetModelList(" RuleId=" + ruleId);
            //不存在该优惠项，直接返回
            if (itemList == null || itemList.Count == 0)
            {
                return cartItems;
            }
            Maticsoft.Model.Shop.Sales.SalesItem salesItem = null;
            //规则单位 为 “个”的情况
            if (ruleUnit == 0)
            {
                //计算数量之和
                int quantity = cartItems.Select(c => c.Quantity).Sum();
                salesItem = GetItemByQuantity(itemList, quantity);
            }
            //规则单位 为 “元”的情况
            if (ruleUnit == 1)
            {
                decimal totalSellPrice = cartItems.Select(c => c.SubTotal).Sum();
                salesItem = GetItemByTotalPrice(itemList, totalSellPrice);
            }
            if (salesItem == null)
            {
                return cartItems;
            }

            foreach (var cartItem in cartItems)
            {
                //优惠类型
                switch (itemList[0].ItemType)
                {
                    case 0: //打折
                        cartItem.AdjustedPrice = cartItem.SellPrice * salesItem.RateValue / 100;
                        break;
                    case 1: //减价
                        cartItem.AdjustedPrice = (cartItem.SellPrice * cartItem.Quantity - salesItem.RateValue) /
                                                 cartItem.Quantity;
                        break;
                    case 2: //固定价
                        cartItem.AdjustedPrice = cartItem.SellPrice - salesItem.RateValue;
                        break;
                    default:
                        cartItem.AdjustedPrice = cartItem.SellPrice * salesItem.RateValue / 100;
                        break;
                }
            }
            return cartItems;
        }

        //会员等级是否限制
        public bool RankIsLimit(int ruleId, int userid)
        {
            Maticsoft.BLL.Shop.Sales.SalesUserRank userRankBll = new SalesUserRank();
            List<Maticsoft.Model.Shop.Sales.SalesUserRank> userRankList = userRankBll.GetModelList(" RuleId=" + ruleId);
            //如果没有等级条件，就没有限制
            if (userRankList == null || userRankList.Count == 0)
            {
                return true;
            }
            //获取用户等级
            Maticsoft.BLL.Members.UsersExp usersExpBll = new UsersExp();
            int rankId = usersExpBll.GetUserRankId(userid);
            return !userRankList.Select(c => c.RankId).Contains(rankId);
        }

        /// <summary>
        /// 计算购物车项的优惠值
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        public void GetRateValue(int ruleId, int ruleUnit,
            Maticsoft.Model.Shop.Products.
                ShoppingCartItem cartItem)
        {
            Maticsoft.BLL.Shop.Sales.SalesItem itemBll = new SalesItem();
            List<Maticsoft.Model.Shop.Sales.SalesItem> itemList = itemBll.GetModelList(" RuleId=" + ruleId);
            //不存在该优惠项，直接返回
            if (itemList == null || itemList.Count == 0)
            {
                return;
            }
            Maticsoft.Model.Shop.Sales.SalesItem salesItem = null;
            //规则单位 为 “个”的情况
            if (ruleUnit == 0)
            {
                salesItem = GetItemByQuantity(itemList, cartItem.Quantity);
            }
            //规则单位 为 “元”的情况
            if (ruleUnit == 1)
            {
                decimal totalSellPrice = cartItem.SellPrice * cartItem.Quantity;
                salesItem = GetItemByTotalPrice(itemList, totalSellPrice);
            }
            if (salesItem == null)
            {
                return;
            }
            //优惠类型
            switch (itemList[0].ItemType)
            {
                case 0: //打折
                    cartItem.AdjustedPrice = cartItem.SellPrice * salesItem.RateValue / 100;
                    break;
                case 1: //减价
                    cartItem.AdjustedPrice = (cartItem.SellPrice * cartItem.Quantity - salesItem.RateValue) /
                                             cartItem.Quantity;
                    break;
                case 2: //固定价
                    cartItem.AdjustedPrice = cartItem.SellPrice - salesItem.RateValue;
                    break;
                default:
                    cartItem.AdjustedPrice = cartItem.SellPrice * salesItem.RateValue / 100;
                    break;
            }
        }

        /// <summary>
        /// 根据数量条件获取最优优惠项
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Sales.SalesItem GetItemByQuantity(List<Maticsoft.Model.Shop.Sales.SalesItem> itemList,
                                                                  int Quantity)
        {
            //先对优惠项进行排序
            itemList = itemList.OrderByDescending(c => c.UnitValue).ToList();
            Maticsoft.Model.Shop.Sales.SalesItem itemModel = null;
            foreach (var salesItem in itemList)
            {
                if (Quantity >= salesItem.UnitValue)
                {
                    itemModel = salesItem;
                    break;
                }
            }
            return itemModel;
        }
        /// <summary>
        ///  根据总价条件获取最优优惠项
        /// </summary>
        /// <param name="itemList"></param>
        /// <param name="TotalPrice"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Sales.SalesItem GetItemByTotalPrice(
            List<Maticsoft.Model.Shop.Sales.SalesItem> itemList,
           decimal TotalPrice)
        {
            //先对优惠项进行排序
            itemList = itemList.OrderByDescending(c => c.UnitValue).ToList();
            Maticsoft.Model.Shop.Sales.SalesItem itemModel = null;
            foreach (var salesItem in itemList)
            {
                if (TotalPrice >= salesItem.UnitValue)
                {
                    itemModel = salesItem;
                    break;
                }
            }
            return itemModel;
        }
        /// <summary>
        /// 根据商品ID 获取批发优惠规则以及规则项
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Maticsoft.ViewModel.Shop.SalesModel GetSalesRule(long productId, int userId)
        {
            Maticsoft.ViewModel.Shop.SalesModel salesModel=new SalesModel();
            Maticsoft.Model.Shop.Sales.SalesRuleProduct RuleProductModel = GetRuleProduct(productId);
            //如果没有对应规则，直接返回订单项
            if (RuleProductModel == null)
            {
                return salesModel;
            }
            Maticsoft.BLL.Shop.Sales.SalesRule ruleBll = new SalesRule();
            Maticsoft.Model.Shop.Sales.SalesRule RuleModel = ruleBll.GetModelByCache(RuleProductModel.RuleId);
            //不存在该批发规则，或者该规则不启用
            if (RuleModel == null || RuleModel.Status == 0)
            {
                return salesModel;
            }

            //TODO: 优惠规则未与等级关联
            ////检测会员等级限制
            //if (RankIsLimit(RuleModel.RuleId, userId))
            //{
            //    return salesModel;
            //}
            Maticsoft.BLL.Shop.Sales.SalesItem item=new BLL.Shop.Sales.SalesItem();
            salesModel.SalesRule = RuleModel;
            salesModel.SalesItems = item.GetModelList(" RuleId=" + RuleModel.RuleId);
            return salesModel;
        }


        public Maticsoft.ViewModel.Shop.SalesModel GetSalesRuleByCache(long productId, int userId)
        {
            string CacheKey = "GetSalesRuleByCache-" + productId + userId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetSalesRule(productId, userId);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.ViewModel.Shop.SalesModel)objModel;
        }

        #endregion  ExtensionMethod
    }
}

