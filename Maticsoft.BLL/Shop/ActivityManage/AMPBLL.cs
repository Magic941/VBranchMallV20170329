using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.ActivityManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.ActivityManage;
using System.Text;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.BLL.Shop.ActivityManage
{
    public partial class AMPBLL
    {
        private readonly IAMP dal = DAShopAMP.CreateAMP();
        public AMPBLL()
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
        public bool Exists(int AMId, long SupplierId)
        {
            return dal.Exists(AMId, SupplierId);
        }
        public bool ExistsSup(int SupplierId)
        {
            return dal.ExistsSup(SupplierId);
        }
        //
        public bool ExistsSup(int SupplierId,int AMId)
        {
            return dal.ExistsSups(SupplierId,AMId);
        }

        public bool ExistsPro(int ProductId)
        {
            return dal.ExistsPro(ProductId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.ActivityManage.AMPModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.ActivityManage.AMPModel model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AMId, long SupplierId)
        {

            return dal.Delete(AMId, SupplierId);
        }
        public bool Delete(int SupplierId)
        {
            return dal.Delete(SupplierId);
        }
        /// <summary>
        /// 根据主商品Id进行删除
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public bool DeleteByProId(int ProductId)
        {
            return dal.DeleteByProId(ProductId);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        //public Maticsoft.Model.Shop.ActivityManage.AMPModel GetModel(int AMId, long ProductId)
        //{

        //    return dal.GetModel(AMId, ProductId);
        //}
        public Maticsoft.Model.Shop.ActivityManage.AMPModel GetModel(int AMId, long SupplierId)
        {

            return dal.GetModel(AMId, SupplierId);
        }
        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        //public Maticsoft.Model.Shop.ActivityManage.AMPModel GetModelByCache(int RuleId, long ProductId)
        //{

        //    string CacheKey = "SalesRuleProductModel-" + RuleId + ProductId;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(RuleId, ProductId);
        //            if (objModel != null)
        //            {
        //            int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch { }
        //    }
        //    return (Maticsoft.Model.Shop.ActivityManage.AMPModel)objModel;
        //}
        public Maticsoft.Model.Shop.ActivityManage.AMPModel GetModelByCache(int AMId, long SupplierId)
        {

            string CacheKey = "SalesRuleProductModel-" + AMId + SupplierId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AMId, SupplierId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.ActivityManage.AMPModel)objModel;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        public DataSet GetLists(string strWhere)
        {

            return dal.GetLists(strWhere);
        }
        /// <summary>
        /// 获得前几行数据  GetList
        /// </summary>
        public DataSet GetListAndPrice(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListAndPrice(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.ActivityManage.AMPModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        public List<Maticsoft.Model.Shop.ActivityManage.AMPModel> GetModelList()
        {
            DataSet ds = dal.GetList();
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 从视图中获得数据列表
        /// </summary>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.ActivityManage.AMPModel> GetModelLists()
        {
            DataSet ds = dal.GetLists();
            return DataTableToList(ds.Tables[0]);
        }


        public List<Maticsoft.Model.Shop.ActivityManage.AMPModel> GetModelLists(int AMId)
        {
            DataSet ds = dal.GetLists(AMId);
            return DataTable2ToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.ActivityManage.AMPModel> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.ActivityManage.AMPModel> modelList = new List<AMPModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.ActivityManage.AMPModel model;
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
        public List<Maticsoft.Model.Shop.ActivityManage.AMPModel> DataTable2ToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.ActivityManage.AMPModel> modelList = new List<AMPModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.ActivityManage.AMPModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRow2ToModel(dt.Rows[n]);
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
        public bool DeleteByRule(int AMId)
        {
            return dal.DeleteByAMId(AMId);
        }
        public Maticsoft.Model.Shop.ActivityManage.AMPModel GetModelBySupplierId(int SupplierId)
        {
            return dal.GetModelBySupplierId(SupplierId);
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="AMId"></param>
        /// <param name="categoryId"></param>
        /// <param name="pName"></param>
        /// <param name="pcode"></param>
        /// <returns></returns>
        public DataSet GetAMProducts(int AMId, string categoryId, string pName, string pcode)
        {
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" AND ProductName LIKE '%{0}%'", pName);
            }
            if (!string.IsNullOrWhiteSpace(pcode))
            {
                strWhere.AppendFormat(" AND ProductCode LIKE '%{0}%'", pcode);
            }
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                strWhere.AppendFormat("AND ProductId IN (SELECT DISTINCT ProductId FROM  Shop_ProductCategories PC WHERE (CategoryPath LIKE '{0}|%' or CategoryId={0}))", categoryId);
            }
            return dal.GetAMProducts(AMId, strWhere.ToString());
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

        /// <summary>
        /// 促销数据
        /// </summary>
        /// <param name="cartInfo"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ShoppingCartInfo GetTotalPriceAfterActivity(Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo)
        {

            //将商品按商家分开
            var query = from rec in cartInfo.Items.Where(m=>m.Selected==true) group rec by rec.SupplierId into s select new { supplier = s.Key, TotalAdjustedPrice = s.Sum(m => m.AdjustedPrice * m.Quantity), TotalSellPrice = s.Sum(m => m.SubTotal) };
            Maticsoft.Model.Shop.ActivityManage.AMPModel amp = null;
            foreach (var rec in query.ToList())
            {
                #region 查看当前商家是否有满减满折活动
                amp = GetModelBySupplierId(int.Parse(rec.supplier.ToString()));
                if (amp != null)
                {
                    DataTable dt = GetActiveRuleByAMId(amp.AMId, rec.TotalAdjustedPrice);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        
                        if (int.Parse(dt.Rows[0]["AMType"].ToString()) == 1)
                        {
                            //满折
                            var AfterPrice = Math.Round(decimal.Parse((rec.TotalAdjustedPrice * (decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()) / 100)).ToString()), 2);
                            cartInfo.SupplierPriceList.Add(new Model.CustomModel.SupplierPrice
                            {
                                SupplierId = int.Parse(rec.supplier.ToString()),
                                TotalSellPrice = AfterPrice,
                                TotalAdjustedPrice = Math.Round(decimal.Parse((rec.TotalAdjustedPrice * (decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()) / 100)).ToString()), 2),
                                ActiveType = false,
                                PreferentialType = 1,
                                AMName = dt.Rows[0]["AMName"].ToString(),
                                AMDUnitValue = decimal.Parse(dt.Rows[0]["AMDUnitValue"].ToString()),
                                AMDRateValue = decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()),
                                PreferentialValue = rec.TotalAdjustedPrice - AfterPrice
                            });
                        }
                        else
                        {
                            //满减
                            var AfterPrice = Math.Round(decimal.Parse((rec.TotalAdjustedPrice - (int.Parse(dt.Rows[0]["AMDRateValue"].ToString()))).ToString()), 2);
                            cartInfo.SupplierPriceList.Add(new Model.CustomModel.SupplierPrice
                            {
                                SupplierId = int.Parse(rec.supplier.ToString()),
                                TotalSellPrice = (rec.TotalSellPrice - int.Parse(dt.Rows[0]["AMDRateValue"].ToString())),
                                TotalAdjustedPrice = (rec.TotalAdjustedPrice - int.Parse(dt.Rows[0]["AMDRateValue"].ToString())),
                                ActiveType = false,
                                PreferentialType = 2,
                                AMName = dt.Rows[0]["AMName"].ToString(),
                                AMDUnitValue = decimal.Parse(dt.Rows[0]["AMDUnitValue"].ToString()),
                                AMDRateValue = decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()),
                                PreferentialValue = rec.TotalAdjustedPrice - AfterPrice
                            });
                        }
                    }
                    else
                    {
                        cartInfo.SupplierPriceList.Add(new Model.CustomModel.SupplierPrice
                        {
                            SupplierId = int.Parse(rec.supplier.ToString()),
                            TotalSellPrice = rec.TotalSellPrice,
                            TotalAdjustedPrice = rec.TotalAdjustedPrice
                        });
                    }

                }
                #endregion
                else
                {
                    cartInfo.SupplierPriceList.Add(new Model.CustomModel.SupplierPrice
                    {
                        SupplierId = int.Parse(rec.supplier.ToString()),
                        TotalSellPrice = rec.TotalSellPrice,
                        TotalAdjustedPrice = rec.TotalAdjustedPrice
                    });
                }

            }
            //判断是否全场满减满折
            AMBLL _amBll = new AMBLL();
            Maticsoft.Model.Shop.ActivityManage.AMModel am = _amBll.GetAllActivity(1);
            if (am != null)
            {
                var TotalSellPrice = cartInfo.SupplierPriceList.Sum(m => m.TotalSellPrice);
                var SupplierPrefer = cartInfo.SupplierPriceList.Sum(m => m.PreferentialValue);
                var TotalAdjustedPrice = cartInfo.SupplierPriceList.Sum(m => m.TotalAdjustedPrice);
                DataTable dt = GetActiveRuleByAMId(am.AMId, TotalAdjustedPrice);

                if (dt != null)
                {
                    
                    if (am.AMType == 1)
                    {
                        var AfterPrice = Math.Round(decimal.Parse((TotalAdjustedPrice * (decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()) / 100)).ToString()), 2);
                        //满折
                        cartInfo.SupplierPriceList.Add(new Model.CustomModel.SupplierPrice
                        {
                            TotalSellPrice = AfterPrice,
                            TotalAdjustedPrice = Math.Round(decimal.Parse((TotalAdjustedPrice * (decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()) / 100)).ToString()), 2),
                            ActiveType = true,
                            PreferentialType = 1,
                            AMName = dt.Rows[0]["AMName"].ToString(),
                            AMDUnitValue = decimal.Parse(dt.Rows[0]["AMDUnitValue"].ToString()),
                            AMDRateValue = decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()),
                            PreferentialValue = (TotalAdjustedPrice - AfterPrice)
                        });
                    }
                    else
                    {
                        var AfterPrice = TotalAdjustedPrice - int.Parse(dt.Rows[0]["AMDRateValue"].ToString());
                        //满减
                        cartInfo.SupplierPriceList.Add(new Model.CustomModel.SupplierPrice
                        {
                            TotalSellPrice = AfterPrice,
                            TotalAdjustedPrice = (TotalAdjustedPrice - int.Parse(dt.Rows[0]["AMDRateValue"].ToString())),
                            ActiveType = true,
                            PreferentialType = 2,
                            AMName = dt.Rows[0]["AMName"].ToString(),
                            AMDUnitValue = decimal.Parse(dt.Rows[0]["AMDUnitValue"].ToString()),
                            AMDRateValue = decimal.Parse(dt.Rows[0]["AMDRateValue"].ToString()),
                            PreferentialValue = (TotalAdjustedPrice - AfterPrice)
                        });
                    }
                }

            }
            var TotalSellPriceF = 0M;
            var TotalAdjustedPriceF = 0M;
            var Final = cartInfo.SupplierPriceList.Where(m => m.ActiveType == true).ToList();
            if (Final.Count() == 0)
            {
                TotalSellPriceF = cartInfo.SupplierPriceList.Sum(m => m.TotalSellPrice);
                TotalAdjustedPriceF = cartInfo.SupplierPriceList.Sum(m => m.TotalAdjustedPrice);
            }
            else
            {
                TotalSellPriceF = Final.First().TotalSellPrice;
                TotalAdjustedPriceF = Final.First().TotalAdjustedPrice;
            }
            cartInfo.TotalSellPrice = TotalSellPriceF;
            cartInfo.TotalAdjustedPrice = TotalAdjustedPriceF;
            return cartInfo;
        }

        public DataTable GetActiveRuleByAMId(int AMId, decimal Price)
        {
            return dal.GetActiveRuleByAMId(AMId, Price);
        }


        public Maticsoft.Model.Shop.ActivityManage.AMPModel GetRuleProduct(long productId)
        {
            string CacheKey = "GetRuleProduct-" + productId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    List<Maticsoft.Model.Shop.ActivityManage.AMPModel> ruleProducts = GetModelList(" ProductId=" + productId);
                    objModel = ruleProducts[0];
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.ActivityManage.AMPModel)objModel;

        }

        /// <summary>
        /// 获取活动优惠
        /// </summary>
        /// <param name="cartInfo"></param>
        /// <returns></returns>
        public Maticsoft.Model.Shop.Products.ShoppingCartInfo GetWholeSale(
            Maticsoft.Model.Shop.Products.ShoppingCartInfo cartInfo)
        {
            Dictionary<Maticsoft.Model.Shop.ActivityManage.AMModel, List<Maticsoft.Model.Shop.Products.ShoppingCartItem>> dictionary = new Dictionary<Model.Shop.ActivityManage.AMModel, List<ShoppingCartItem>>();
            //活动规则处理
            foreach (var cartItem in cartInfo.Items)
            {
                GetCartItem(cartItem, dictionary);
            }

            //商品总计的情况处理
            //if (dictionary != null && dictionary.Count > 0)
            //{
            //    foreach (var dic in dictionary)
            //    {
            //        Maticsoft.Model.Shop.ActivityManage.AMModel amModel = dic.Key;
            //        if (amModel != null)
            //        {
            //            cartInfo.Items.RemoveAll(c => dic.Value.Contains(c));
            //            List<Maticsoft.Model.Shop.Products.ShoppingCartItem> cartItems = GetRateValueList(amModel, amModel.AMUnit, dic.Value);
            //            foreach (var item in cartItems)
            //            {
            //                item.SaleDes = amModel.AMName;
            //                cartInfo.Items.Add(item);
            //            }
            //        }
            //    }
            //}
            return cartInfo;
        }
        //获取订单优惠值
        public void GetCartItem(Maticsoft.Model.Shop.Products.ShoppingCartItem cartItem,
            Dictionary<Maticsoft.Model.Shop.ActivityManage.AMModel, List<Maticsoft.Model.Shop.Products.ShoppingCartItem>> dictionary)
        {
            Maticsoft.Model.Shop.ActivityManage.AMPModel ampModel = GetRuleProduct(cartItem.ProductId);
            //如果没有对应规则，直接返回订单项
            if (ampModel == null)
            {
                return;
            }
            Maticsoft.BLL.Shop.ActivityManage.AMBLL amBll = new AMBLL();
            Maticsoft.Model.Shop.ActivityManage.AMModel amModel = amBll.GetModelByCache(ampModel.AMId);
            //不存在该活动规则，或者该规则不启用
            if (amModel == null || amModel.AMStatus == 0)
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
            cartItem.SaleDes = amModel.AMName;

            //单个商品模式处理
            if (amModel.AMApplyStyles == 0)
            {
                //计算商品优惠值
                //GetRateValue(amModel.AMId, amModel.AMUnit, cartItem);
            }
            //商品总计的情况处理
            else
            {
                if (dictionary.ContainsKey(amModel))
                {
                    dictionary[amModel].Add(cartItem);
                }
                else
                {
                    dictionary.Add(amModel, new List<ShoppingCartItem> { cartItem });
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

        /// <summary>
        /// 计算购物车项的优惠值
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="cartItem"></param>
        /// <returns></returns>
        /* public void GetRateValue(int ruleId, int ruleUnit,
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
         * 
        /// <summary>
        /// 根据商品ID 获取活动优惠规则以及规则项
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
            //不存在该活动规则，或者该规则不启用
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
        */
        #endregion  ExtensionMethod
    }
}