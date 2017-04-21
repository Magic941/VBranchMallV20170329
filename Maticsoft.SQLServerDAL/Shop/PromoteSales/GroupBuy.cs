/**
* GroupBuy.cs
*
* 功 能： N/A
* 类 名： GroupBuy
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/14 15:51:55   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Shop.PromoteSales;
using Maticsoft.DBUtility;
using System.Collections.Generic;//Please add references
namespace Maticsoft.SQLServerDAL.Shop.PromoteSales
{
    /// <summary>
    /// 数据访问类:GroupBuy
    /// </summary>
    public partial class GroupBuy : IGroupBuy
    {
        public GroupBuy()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("GroupBuyId", "Shop_GroupBuy");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GroupBuyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_GroupBuy");
            strSql.Append(" where GroupBuyId=@GroupBuyId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupBuyId", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupBuyId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        public int BulkInsert2ShopGroup(List<Maticsoft.Model.Shop.PromoteSales.GroupBuy> GroupBuyList)
        {
            List<CommandInfo> CommandList = new List<CommandInfo>();
            foreach (var item in GroupBuyList)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Shop_GroupBuy(");
                strSql.Append("ProductId,Sequence,FinePrice,StartDate,EndDate,MaxCount,GroupCount,BuyCount,Price,Status,Description,RegionId,ProductName,ProductCategory,GroupBuyImage,CategoryId,CategoryPath,GroupBase,PromotionType,PromotionLimitQu)");
                strSql.Append(" values (");
                strSql.Append("@ProductId,@Sequence,@FinePrice,@StartDate,@EndDate,@MaxCount,@GroupCount,@BuyCount,@Price,@Status,@Description,@RegionId,@ProductName,@ProductCategory,@GroupBuyImage,@CategoryId,@CategoryPath,@GroupBase,@PromotionType,@PromotionLimitQu)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@Sequence", SqlDbType.Int,4),
                    new SqlParameter("@FinePrice", SqlDbType.Money,8),
                    new SqlParameter("@StartDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@MaxCount", SqlDbType.Int,4),
                    new SqlParameter("@GroupCount", SqlDbType.Int,4),
                    new SqlParameter("@BuyCount", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Money,8),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Description", SqlDbType.Text),
                    new SqlParameter("@RegionId",SqlDbType.Int) ,
                      new SqlParameter("@ProductName",SqlDbType.NVarChar) ,
                        new SqlParameter("@ProductCategory",SqlDbType.NVarChar) ,
                          new SqlParameter("@GroupBuyImage",SqlDbType.NVarChar) ,
                                        new SqlParameter("@CategoryId",SqlDbType.Int) ,
                      new SqlParameter("@CategoryPath",SqlDbType.NVarChar),
                                        new SqlParameter("@GroupBase",SqlDbType.Int),
                                        new SqlParameter("@PromotionType",SqlDbType.Int,4),
                                        new SqlParameter("@PromotionLimitQu",SqlDbType.Int,4)
                                        };
                parameters[0].Value = item.ProductId;
                parameters[1].Value = item.Sequence;
                parameters[2].Value = item.FinePrice;
                parameters[3].Value = item.StartDate;
                parameters[4].Value = item.EndDate;
                parameters[5].Value = item.MaxCount;
                parameters[6].Value = item.GroupCount;
                parameters[7].Value = item.BuyCount;
                parameters[8].Value = item.Price;
                parameters[9].Value = item.Status;
                parameters[10].Value = item.Description;
                parameters[11].Value = item.RegionId;
                parameters[12].Value = item.ProductName;
                parameters[13].Value = item.ProductCategory;
                parameters[14].Value = item.GroupBuyImage;
                parameters[15].Value = item.CategoryId;
                parameters[16].Value = item.CategoryPath;
                parameters[17].Value = item.GroupBase;
                parameters[18].Value = item.PromotionType;
                parameters[19].Value = item.PromotionLimitQu;
                CommandList.Add(new CommandInfo(strSql.ToString(), parameters,EffentNextType.ExcuteEffectRows));
            }
            return DbHelperSQL.ExecuteSqlTran(CommandList);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Shop.PromoteSales.GroupBuy model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_GroupBuy(");
            strSql.Append("ProductId,Sequence,FinePrice,StartDate,EndDate,MaxCount,GroupCount,BuyCount,Price,Status,Description,RegionId,ProductName,ProductCategory,GroupBuyImage,CategoryId,CategoryPath,GroupBase,PromotionType,PromotionLimitQu,LeastbuyNum)");
            strSql.Append(" values (");
            strSql.Append("@ProductId,@Sequence,@FinePrice,@StartDate,@EndDate,@MaxCount,@GroupCount,@BuyCount,@Price,@Status,@Description,@RegionId,@ProductName,@ProductCategory,@GroupBuyImage,@CategoryId,@CategoryPath,@GroupBase,@PromotionType,@PromotionLimitQu,@LeastbuyNum)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                    new SqlParameter("@Sequence", SqlDbType.Int,4),
                    new SqlParameter("@FinePrice", SqlDbType.Money,8),
                    new SqlParameter("@StartDate", SqlDbType.DateTime),
                    new SqlParameter("@EndDate", SqlDbType.DateTime),
                    new SqlParameter("@MaxCount", SqlDbType.Int,4),
                    new SqlParameter("@GroupCount", SqlDbType.Int,4),
                    new SqlParameter("@BuyCount", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Money,8),
                    new SqlParameter("@Status", SqlDbType.Int,4),
                    new SqlParameter("@Description", SqlDbType.Text),
                    new SqlParameter("@RegionId",SqlDbType.Int) ,
                      new SqlParameter("@ProductName",SqlDbType.NVarChar) ,
                        new SqlParameter("@ProductCategory",SqlDbType.NVarChar) ,
                          new SqlParameter("@GroupBuyImage",SqlDbType.NVarChar) ,
                                        new SqlParameter("@CategoryId",SqlDbType.Int) ,
                      new SqlParameter("@CategoryPath",SqlDbType.NVarChar),
                                        new SqlParameter("@GroupBase",SqlDbType.Int),
                                        new SqlParameter("@PromotionType",SqlDbType.Int,4),
                                        new SqlParameter("@PromotionLimitQu",SqlDbType.Int,4),
                                        new SqlParameter("@LeastbuyNum",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.Sequence;
            parameters[2].Value = model.FinePrice;
            parameters[3].Value = model.StartDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.MaxCount;
            parameters[6].Value = model.GroupCount;
            parameters[7].Value = model.BuyCount;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.RegionId;
            parameters[12].Value = model.ProductName;
            parameters[13].Value = model.ProductCategory;
            parameters[14].Value = model.GroupBuyImage;
            parameters[15].Value = model.CategoryId;
            parameters[16].Value = model.CategoryPath;
            parameters[17].Value = model.GroupBase;
            parameters[18].Value = model.PromotionType;
            parameters[19].Value = model.PromotionLimitQu;
            parameters[20].Value = model.LeastbuyNum;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.PromoteSales.GroupBuy model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_GroupBuy set ");

            strSql.Append("ProductId=@ProductId,");
            strSql.Append("Sequence=@Sequence,");
            strSql.Append("FinePrice=@FinePrice,");
            strSql.Append("StartDate=@StartDate,");
            strSql.Append("EndDate=@EndDate,");
            strSql.Append("MaxCount=@MaxCount,");
            strSql.Append("GroupCount=@GroupCount,");
            strSql.Append("BuyCount=@BuyCount,");
            strSql.Append("Price=@Price,");
            strSql.Append("Status=@Status,");
            strSql.Append("Description=@Description,");
            strSql.Append("RegionId=@RegionId,");
            strSql.Append("ProductName=@ProductName,");
            strSql.Append("ProductCategory=@ProductCategory,");
            strSql.Append("GroupBuyImage=@GroupBuyImage,");
            strSql.Append("CategoryId=@CategoryId,");
            strSql.Append("CategoryPath=@CategoryPath,");
            strSql.Append("GroupBase= @GroupBase, ");
            strSql.Append("PromotionLimitQu=@PromotionLimitQu, ");
            strSql.Append("PromotionType=@PromotionType,");
            strSql.Append("LeastbuyNum=@LeastbuyNum");
            strSql.Append(" where GroupBuyId=@GroupBuyId");
            SqlParameter[] parameters = {
                new SqlParameter("@ProductId", SqlDbType.BigInt,8),
                new SqlParameter("@Sequence", SqlDbType.Int,4),
                new SqlParameter("@FinePrice", SqlDbType.Money,8),
                new SqlParameter("@StartDate", SqlDbType.DateTime),
                new SqlParameter("@EndDate", SqlDbType.DateTime),
                new SqlParameter("@MaxCount", SqlDbType.Int,4),
                new SqlParameter("@GroupCount", SqlDbType.Int,4),
                new SqlParameter("@BuyCount", SqlDbType.Int,4),
                new SqlParameter("@Price", SqlDbType.Money,8),
                new SqlParameter("@Status", SqlDbType.Int,4),
                new SqlParameter("@Description", SqlDbType.Text),
                new SqlParameter("@RegionId",SqlDbType.Int), 
                new SqlParameter("@ProductName",SqlDbType.NVarChar) ,
                new SqlParameter("@ProductCategory",SqlDbType.NVarChar) ,
                new SqlParameter("@GroupBuyImage",SqlDbType.NVarChar),
                new SqlParameter("@CategoryId",SqlDbType.Int) ,
                new SqlParameter("@CategoryPath",SqlDbType.NVarChar) ,
                new SqlParameter("@GroupBuyId", SqlDbType.Int,4),
                new SqlParameter("@GroupBase",SqlDbType.Int),
                new SqlParameter("@PromotionLimitQu",SqlDbType.Int,4),
                new SqlParameter("@PromotionType",SqlDbType.Int,4),
                new SqlParameter("@LeastbuyNum",SqlDbType.Int,4)
                                        };
            parameters[0].Value = model.ProductId;
            parameters[1].Value = model.Sequence;
            parameters[2].Value = model.FinePrice;
            parameters[3].Value = model.StartDate;
            parameters[4].Value = model.EndDate;
            parameters[5].Value = model.MaxCount;
            parameters[6].Value = model.GroupCount;
            parameters[7].Value = model.BuyCount;
            parameters[8].Value = model.Price;
            parameters[9].Value = model.Status;
            parameters[10].Value = model.Description;
            parameters[11].Value = model.RegionId;
            parameters[12].Value = model.ProductName;
            parameters[13].Value = model.ProductCategory;
            parameters[14].Value = model.GroupBuyImage;
            parameters[15].Value = model.CategoryId;
            parameters[16].Value = model.CategoryPath;
            parameters[17].Value = model.GroupBuyId;
            parameters[18].Value = model.GroupBase;
            parameters[19].Value = model.PromotionLimitQu;
            parameters[20].Value = model.PromotionType;
            parameters[21].Value = model.LeastbuyNum;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Insert2GroupBuy(List<string> GroupBuyIdList, DateTime StartDate, DateTime EndDate)
        {
            List<CommandInfo> CommandList = new List<CommandInfo>();
            foreach (var item in GroupBuyIdList)
            {
                if (item != "")
                {
                    string sql = string.Format(@"INSERT INTO dbo.Shop_GroupBuy
                                                    ( ProductId ,
                                                      Sequence ,
                                                      FinePrice ,
                                                      StartDate ,
                                                      EndDate ,
                                                      MaxCount ,
                                                      GroupCount ,
                                                      BuyCount ,
                                                      Price ,
                                                      Status ,
                                                      Description ,
                                                      RegionId ,
                                                      ProductName ,
                                                      ProductCategory ,
                                                      GroupBuyImage ,
                                                      CategoryId ,
                                                      CategoryPath ,
                                                      PromotionLimitQu ,
                                                      GroupBase ,
                                                      PromotionType
                                                    )
                                                    select
                                                    xProductId,
                                                    1,
                                                    xFinePrice,
                                                    '{0}',
                                                    '{1}',
                                                    1,
                                                    xGroupCount,
                                                    0,
                                                    xPrice,
                                                    0,
                                                    xDescription,
                                                    xRegionId,
                                                    xProductName,
                                                    xProductCategory,
                                                    xGroupBuyImage,
                                                    T.CategoryId,
                                                    xCategoryPath,
                                                    xPromotionLimitQu,
                                                    1,
                                                    xPromotionType from (select ROW_NUMBER() over(order by x.GroupBuyId) as RowID, x.GroupBuyId xGroupBuyId,x.ProductId xProductId,x.Sequence xSequence,x.FinePrice xFinePrice,x.StartDate xStartDate,x.EndDate xEndDate,x.MaxCount xMaxCount,x.GroupCount xGroupCount,x.BuyCount xBuyCount,x.Price xPrice,x.Status xStatus,x.Description xDescription,x.RegionId xRegionId,x.ProductName xProductName,x.ProductCategory xProductCategory,x.GroupBuyImage xGroupBuyImage,x.CategoryId xCategoryId,x.CategoryPath xCategoryPath,x.PromotionLimitQu xPromotionLimitQu,x.GroupBase xGroupBase,x.PromotionType xPromotionType,y.*,z.SkuId zSkuId,z.SKU zSKU,z.Weight zWeight,z.Stock zStock,z.AlertStock zAlertStock,z.CostPrice zCostPrice,z.SalePrice zSalePrice,z.Upselling zUpselling from (select ROW_NUMBER() over(partition by ProductId order by GroupBuyId) RowID,* from Shop_GroupBuy) x left join Shop_Products y on x.ProductId = y.ProductId left join (select * from (select ROW_NUMBER() over(partition by ProductId order by SkuId) Row, * from Shop_SKUs) S where S.Row=1) z on x.ProductId = z.ProductId where x.RowID=1 and x.PromotionType=1 and y.SaleStatus=1) T where T.ProductId={2}", StartDate, EndDate, item);
                    CommandList.Add(new CommandInfo(sql, null, EffentNextType.ExcuteEffectRows));
                }
            }
            return DbHelperSQL.ExecuteSqlTran(CommandList);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupBuyId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_GroupBuy ");
            strSql.Append(" where GroupBuyId=@GroupBuyId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupBuyId", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupBuyId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string GroupBuyIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_GroupBuy ");
            strSql.Append(" where GroupBuyId in (" + GroupBuyIdlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GetModel(int GroupBuyId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GroupBuyId,ProductId,Sequence,FinePrice,StartDate,EndDate,MaxCount,GroupCount,BuyCount,Price,Status,Description,RegionId,ProductName,ProductCategory,GroupBuyImage,CategoryId,CategoryPath,GroupBase,PromotionType,PromotionLimitQu,LeastbuyNum from Shop_GroupBuy ");
            strSql.Append(" where GroupBuyId=@GroupBuyId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupBuyId", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupBuyId;

            Maticsoft.Model.Shop.PromoteSales.GroupBuy model = new Maticsoft.Model.Shop.PromoteSales.GroupBuy();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        public Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory DataRow2Model(DataRow row)
        {
            Maticsoft.Model.Shop.PromoteSales.GroupBuyHistory model = new Model.Shop.PromoteSales.GroupBuyHistory();
            if (row!=null)
            {
                if (row["Amount"] != null && row["Amount"].ToString() != "")
                {
                    model.Amount = decimal.Parse(row["Amount"].ToString());
                }

                if (row["UserName"] != null && row["UserName"].ToString() != "")
                {
                    model.UserName = row["UserName"].ToString();
                }

                if (row["ProductName"] != null && row["ProductName"].ToString() != "")
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["CreatedDate"] != null && row["CreatedDate"].ToString() != "")
                {
                    model.OrderDate = DateTime.Parse(row["CreatedDate"].ToString());
                }
                if (row.Table.Columns.Contains("BuyCount"))
                {
                    if (row["BuyCount"] != null && row["BuyCount"].ToString() != "")
                    {
                        model.SaleCount = int.Parse(row["BuyCount"].ToString());
                    }
                }
            }

            return model;
        }



        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.PromoteSales.GroupBuy DataRowToModel(DataRow row)
        {
            Maticsoft.Model.Shop.PromoteSales.GroupBuy model = new Maticsoft.Model.Shop.PromoteSales.GroupBuy();
            if (row != null)
            {
                if (row["GroupBuyId"] != null && row["GroupBuyId"].ToString() != "")
                {
                    model.GroupBuyId = int.Parse(row["GroupBuyId"].ToString());
                }
                if (row["ProductId"] != null && row["ProductId"].ToString() != "")
                {
                    model.ProductId = long.Parse(row["ProductId"].ToString());
                }
                if (row["Sequence"] != null && row["Sequence"].ToString() != "")
                {
                    model.Sequence = int.Parse(row["Sequence"].ToString());
                }
                if (row["FinePrice"] != null && row["FinePrice"].ToString() != "")
                {
                    model.FinePrice = decimal.Parse(row["FinePrice"].ToString());
                }
                if (row["StartDate"] != null && row["StartDate"].ToString() != "")
                {
                    model.StartDate = DateTime.Parse(row["StartDate"].ToString());
                }
                if (row["EndDate"] != null && row["EndDate"].ToString() != "")
                {
                    model.EndDate = DateTime.Parse(row["EndDate"].ToString());
                }
                if (row["MaxCount"] != null && row["MaxCount"].ToString() != "")
                {
                    model.MaxCount = int.Parse(row["MaxCount"].ToString());
                }
                if (row["GroupCount"] != null && row["GroupCount"].ToString() != "")
                {
                    model.GroupCount = int.Parse(row["GroupCount"].ToString());
                }
                if (row["BuyCount"] != null && row["BuyCount"].ToString() != "")
                {
                    model.BuyCount = int.Parse(row["BuyCount"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["Status"] != null && row["Status"].ToString() != "")
                {
                    model.Status = int.Parse(row["Status"].ToString());
                }
                if (row["Description"] != null)
                {
                    model.Description = row["Description"].ToString();
                }
                if (row["RegionId"] != null)
                {
                    model.RegionId = Common.Globals.SafeInt(row["RegionId"], -1);
                }
                //,ProductName,ProductCategory,GroupBuyImage
                if (row["ProductName"] != null)
                {
                    model.ProductName = row["ProductName"].ToString();
                }
                if (row["ProductCategory"] != null)
                {
                    model.ProductCategory = row["ProductCategory"].ToString();
                }
                if (row["GroupBuyImage"] != null)
                {
                    model.GroupBuyImage = row["GroupBuyImage"].ToString();
                }
                if (row["CategoryId"] != null)
                {
                    model.CategoryId = Common.Globals.SafeInt(row["CategoryId"], 0);
                }
                if (row["CategoryPath"] != null)
                {
                    model.CategoryPath = row["CategoryPath"].ToString();
                }
                if (row["GroupBase"] != null)
                {
                    model.GroupBase = Common.Globals.SafeInt(row["GroupBase"], 0);
                }
                if (row.Table.Columns.Contains("Subhead"))
                {
                    if (row["Subhead"] != null)
                    {
                        model.Subhead = row["Subhead"].ToString();
                    }
                }
                //if (row["Subhead"] != null)
                //{
                //    model.Subhead = row["Subhead"].ToString();
                //}
                if (row.Table.Columns.Contains("MarketPrice"))
                {
                    if (row["MarketPrice"] != null)
                    {
                        model.MarketPrice = decimal.Parse(row["MarketPrice"].ToString());
                    }
                }
                if (row.Table.Columns.Contains("PromotionType"))
                {
                    if (row["PromotionType"] != null)
                    {
                        model.PromotionType = Common.Globals.SafeInt(row["PromotionType"], 0);
                    }
                }
                if (row.Table.Columns.Contains("PromotionLimitQu"))
                {
                    if (row["PromotionLimitQu"] != null)
                    {
                        model.PromotionLimitQu = Common.Globals.SafeInt(row["PromotionLimitQu"], 1);
                    }
                }
                if (row.Table.Columns.Contains("GroupBase"))
                {
                    if (row["GroupBase"] != null)
                    {
                        model.GroupBase = Common.Globals.SafeInt(row["GroupBase"], 1);
                    }
                }
                if (row.Table.Columns.Contains("LeastbuyNum"))
                {
                    if (row["LeastbuyNum"] != null)
                    {
                        model.LeastbuyNum = Common.Globals.SafeInt(row["LeastbuyNum"], 1);
                    }
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select GroupBuyId,ProductId,Sequence,FinePrice,StartDate,EndDate,MaxCount,GroupCount,BuyCount,Price,Status,Description,RegionId,ProductName,ProductCategory,GroupBuyImage,CategoryId,CategoryPath,GroupBase,PromotionType,PromotionLimitQu,LeastbuyNum ");
            //strSql.Append(" from Shop_GroupBuy where EndDate>= GETDATE() and status=1 "); 
            strSql.Append(" from Shop_GroupBuy where CONVERT(varchar(10),EndDate)>= CONVERT(varchar(10),GETDATE()) and status=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by (BuyCount+GroupBase) desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取今天0点开始的数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListToday(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select aa.GroupBuyId,aa.ProductId,aa.Sequence,aa.FinePrice,aa.StartDate,aa.EndDate,aa.MaxCount,aa.GroupCount,aa.BuyCount,aa.Price,aa.Status,aa.Description,aa.RegionId,aa.ProductName,aa.ProductCategory,aa.GroupBuyImage,aa.CategoryId,aa.CategoryPath,aa.GroupBase,aa.PromotionType,aa.PromotionLimitQu,aa.LeastbuyNum,bb.MarketPrice ");
            strSql.Append(" from Shop_GroupBuy aa inner join Shop_Products bb on aa.ProductId=bb.ProductID  where  aa.status=1 and bb.SaleStatus=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by (aa.BuyCount+aa.GroupBase) desc ");
            return DbHelperSQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" GroupBuyId,ProductId,Sequence,FinePrice,StartDate,EndDate,MaxCount,GroupCount,BuyCount,Price,Status,Description,RegionId,ProductName,ProductCategory,GroupBuyImage,CategoryId,CategoryPath,GroupBase,PromotionType,PromotionLimitQu,LeastbuyNum ");
            strSql.Append(" FROM Shop_GroupBuy ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Shop_GroupBuy ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.GroupBuyId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_GroupBuy T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@tblName", SqlDbType.VarChar, 255),
                    new SqlParameter("@fldName", SqlDbType.VarChar, 255),
                    new SqlParameter("@PageSize", SqlDbType.Int),
                    new SqlParameter("@PageIndex", SqlDbType.Int),
                    new SqlParameter("@IsReCount", SqlDbType.Bit),
                    new SqlParameter("@OrderType", SqlDbType.Bit),
                    new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "Shop_GroupBuy";
            parameters[1].Value = "GroupBuyId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod


        public int MaxSequence()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MAX(Sequence) AS Sequence FROM Shop_GroupBuy");
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public bool IsExists(long ProductId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_GroupBuy");
            strSql.Append(" where ProductId=@ProductId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductId", SqlDbType.BigInt)
            };
            parameters[0].Value = ProductId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public int EditStatus()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_GroupBuy set Status = 0 where Status =1 and EndDate<GETDATE()");

            return DbHelperSQL.ExecuteSql(strSql.ToString());
        }


        public bool UpdateStatus(string ids, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_GroupBuy set ");
            strSql.Append("Status=@Status");
            strSql.Append(" where GroupBuyId in (" + ids + ")  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@Status", SqlDbType.Int,4)
                                        };
            parameters[0].Value = status;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新购买数量
        /// </summary>
        /// <param name="buyId"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool UpdateBuyCount(int buyId, int count)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_GroupBuy set ");
            strSql.Append("BuyCount=BuyCount+@BuyCount");
            strSql.Append(" where GroupBuyId =@GroupBuyId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@BuyCount", SqlDbType.Int,4),
                        new SqlParameter("@GroupBuyId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = count;
            parameters[1].Value = buyId;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataSet GetListByPage(string strWhere, int cid, int regionId, string orderby, int startIndex, int endIndex, int promotionType)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.GroupBuyId desc");
            }

            if (regionId > 0)//有选择地区
            {
                strSql.Append(")AS Row, T.*,S.Subhead , S.MarketPrice  from Shop_GroupBuy T,dbo.Shop_Products S ");
                // strSql.AppendFormat(" where  (R.ParentId={0} or R.RegionId={0}) And R.RegionId=T.RegionId AND S.ProductId=T.ProductId", regionId);
                strSql.Append(" where   S.ProductId=T.ProductId");
                if (promotionType == 0)
                {
                    strSql.Append(" And  T.Status = 1 AND T.EndDate>=GETDATE()  AND T.StartDate<=GETDATE() ");
                }
                else
                {
                    strSql.Append(" And  T.Status = 1");
                }
            }
            else
            {
                strSql.Append(")AS Row, T.*  from Shop_GroupBuy T  ");
                strSql.Append(" where   T.Status = 1 AND T.EndDate>=GETDATE()  AND T.StartDate<=GETDATE() ");
            }

            if (cid > 0)//有cid不是默认过来的
            {
                // strSql.AppendFormat(" And    (CategoryPath LIKE   '{0}|%' ", cid);
                strSql.AppendFormat(" And    (CategoryPath LIKE   '%|{0}' ", cid);
                strSql.AppendFormat(" OR T.CategoryId = {0})", cid);
                //strSql.AppendFormat("  And  T.ProductCategory='{0}'", cate);
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append("  And  " + strWhere );
            }
            strSql.AppendFormat("AND T.PromotionType={0}", promotionType);
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1} ORDER BY TT.StartDate desc", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        public int GetCount(string strWhere, int regionId, bool IsForMembers)
        {
            StringBuilder strSql = new StringBuilder();
            // strSql.Append("select count(1) FROM  Shop_GroupBuy T,Ms_Regions R  ");
            strSql.Append("select count(1) FROM  Shop_GroupBuy T  "); //去掉区域
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                //  strSql.Append("And R.RegionId=T.RegionId");
            }
            else
            {
                // strSql.Append("where R.RegionId=T.RegionId");
            }
            if (IsForMembers)
            {
                strSql.AppendFormat(" AND T.PromotionType=1");
            }
            else
            {
                strSql.AppendFormat(" AND T.PromotionType=0");
            }

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        /// <summary>
        /// 好礼大派送
        /// </summary>
        /// <param name="curDate"></param>
        public int GetCountHaoLi(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select count(1) FROM  Shop_GroupBuy T  "); //去掉区域
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }

            strSql.AppendFormat(" AND T.PromotionType=1");

            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }

        public DataSet GetCategory(string strWhere)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("   select T.*,P.subhead ,P.MarketPrice from Shop_GroupBuy T INNER JOIN dbo.Shop_Products P ON T.ProductId = P.ProductId    ");
            sb.Append(" where GroupBuyId=(");
            sb.Append("select min(GroupBuyId) from Shop_GroupBuy");
            sb.Append("  where T.CategoryId=CategoryId)");
            //sb.Append(" And ");
            //sb.Append("   T.Status = 1 AND T.EndDate>=GETDATE()  AND T.StartDate<=GETDATE() ");
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                sb.AppendFormat(" And  {0}", strWhere);
            }
            return DbHelperSQL.Query(sb.ToString());
        }

        #region 为了取团购分类数据
        public DataSet GetGroupbyCategory(string strWhere)
        {
            SqlParameter[] parameters = { };
            return DbHelperSQL.RunProcedure("SP_Shop_Categories_GroupBuy", parameters, "Categories_GroupBu");
        }
        #endregion

        #region 获取团购热销产品 top 10
        public DataSet GetGroupBuyHot()
        {
            string sql = @"SELECT top 10 GroupBuyId,ProductId,Price,BuyCount,GroupBase,ProductName,GroupBuyImage FROM Shop_GroupBuy where EndDate>=GETDATE()
ORDER BY (GroupBase+BuyCount)  DESC";
            return DbHelperSQL.Query(sql);
        }
        #endregion


        public Maticsoft.Model.Shop.PromoteSales.GroupBuy GetPromotionLimitQu(int productId, int PromotionType = 1)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GroupBuyId,ProductId,Sequence,FinePrice,StartDate,EndDate,MaxCount,GroupCount,BuyCount,Price,Status,Description,RegionId,ProductName,ProductCategory,GroupBuyImage,CategoryId,CategoryPath,GroupBase,PromotionType,PromotionLimitQu,LeastbuyNum from Shop_GroupBuy ");
            strSql.Append(" where ProductId=@productId");
            strSql.Append(" AND PromotionType=@PromotionType");
            strSql.Append(" AND GETDATE() <=EndDate");
            strSql.Append(" AND GETDATE() >=StartDate");
            strSql.Append(" AND Status = 1");
            SqlParameter[] parameters = {
                    new SqlParameter("@productId", SqlDbType.Int,4),
                    new SqlParameter("@PromotionType",SqlDbType.Int,4)
            };
            parameters[0].Value = productId;
            parameters[1].Value = PromotionType;

            Maticsoft.Model.Shop.PromoteSales.GroupBuy model = new Maticsoft.Model.Shop.PromoteSales.GroupBuy();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 这个更新方法有问题，应该根据团购编号来操作
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public bool UpdateGroupBuyCountByProductID(long productid, int qty)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_GroupBuy set ");
            strSql.Append("BuyCount=BuyCount+@qty");
            strSql.Append(" where ProductID =@productid ");
            SqlParameter[] parameters = {
                    new SqlParameter("@qty", SqlDbType.Int,4),
                        new SqlParameter("@productid", SqlDbType.BigInt,8)
                                        };
            parameters[0].Value = qty;
            parameters[1].Value = productid;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public DataSet GetGroupBuyLimt4Hour()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" GroupBuyId,ProductId,Sequence,FinePrice,StartDate,EndDate,MaxCount,GroupCount,BuyCount,Price,Status,Description,RegionId,ProductName,ProductCategory,GroupBuyImage,CategoryId,CategoryPath,GroupBase,PromotionType,PromotionLimitQu,LeastbuyNum ");
            strSql.Append(" FROM Shop_GroupBuy WHERE PromotionType=1 AND DateDiff(hh,StartDate,GETDATE())=0 AND Status=1   ");
            return DbHelperSQL.Query(strSql.ToString());
        }




        public DataSet GetGroupbuyHistory(int groupbuyid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT a.Amount,b.UserName,a.CreatedDate,c.ProductName FROM dbo.Shop_Orders a  ");
            strSql.Append(" INNER JOIN dbo.Accounts_Users b ON a.BuyerID =b.UserID ");
            strSql.Append(" INNER JOIN dbo.Shop_GroupBuy c ON a.GroupBuyId =c.GroupBuyId");
            strSql.Append(" WHERE a.GroupBuyId ="+ groupbuyid );
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetGroupBuyListByPage(int groupbuyid, int startIndex, int endIndex)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from (  ");
            sql.Append(" select *, ROW_NUMBER() OVER(Order by a.CreatedDate DESC ) AS RowNumber from dbo.v_ShopGroupHistory as a ");
            sql.Append(" ) as b WHERE b.GroupBuyId=" + groupbuyid + " AND RowNumber BETWEEN " + startIndex + " and " + endIndex);
            return DbHelperSQL.Query(sql.ToString());
        }



        #endregion  ExtensionMethod
    }
}

