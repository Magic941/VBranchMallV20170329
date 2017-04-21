/**
* ShippingRegionGroups.cs
*
* 功 能： N/A
* 类 名： ShippingRegionGroups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/8 18:17:33   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Maticsoft.DBUtility;
using Maticsoft.IDAL.Shop;
using System.Collections.Generic;

//Please add references
namespace Maticsoft.SQLServerDAL.Shop.Shipping
{
    /// <summary>
    /// 数据访问类:ShippingRegionGroups
    /// </summary>
    public partial class ShippingRegionGroups : IDAL.Shop.Shipping.IShippingRegionGroups
    {
        public ShippingRegionGroups()
        { }
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return DbHelperSQL.GetMaxID("GroupId", "Shop_ShippingRegionGroups");
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GroupId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Shop_ShippingRegionGroups");
            strSql.Append(" where GroupId=@GroupId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupId", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.Shop.Shipping.ShippingRegionGroups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Shop_ShippingRegionGroups(");
            strSql.Append("ModeId,Price,AddPrice)");
            strSql.Append(" values (");
            strSql.Append("@ModeId,@Price,@AddPrice)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@ModeId", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Money,8),
                    new SqlParameter("@AddPrice", SqlDbType.Money,8)};
            parameters[0].Value = model.ModeId;
            parameters[1].Value = model.Price;
            parameters[2].Value = model.AddPrice;

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
        public bool Update(Model.Shop.Shipping.ShippingRegionGroups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Shop_ShippingRegionGroups set ");
            strSql.Append("ModeId=@ModeId,");
            strSql.Append("Price=@Price,");
            strSql.Append("AddPrice=@AddPrice");
            strSql.Append(" where GroupId=@GroupId");
            SqlParameter[] parameters = {
                    new SqlParameter("@ModeId", SqlDbType.Int,4),
                    new SqlParameter("@Price", SqlDbType.Money,8),
                    new SqlParameter("@AddPrice", SqlDbType.Money,8),
                    new SqlParameter("@GroupId", SqlDbType.Int,4)};
            parameters[0].Value = model.ModeId;
            parameters[1].Value = model.Price;
            parameters[2].Value = model.AddPrice;
            parameters[3].Value = model.GroupId;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ShippingRegionGroups ");
            strSql.Append(" where GroupId=@GroupId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupId", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupId;

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
        public bool DeleteList(string GroupIdlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Shop_ShippingRegionGroups ");
            strSql.Append(" where GroupId in (" + GroupIdlist + ")  ");
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
        public Model.Shop.Shipping.ShippingRegionGroups GetModel(int GroupId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 GroupId,ModeId,Price,AddPrice from Shop_ShippingRegionGroups ");
            strSql.Append(" where GroupId=@GroupId");
            SqlParameter[] parameters = {
                    new SqlParameter("@GroupId", SqlDbType.Int,4)
            };
            parameters[0].Value = GroupId;

            Model.Shop.Shipping.ShippingRegionGroups model = new Model.Shop.Shipping.ShippingRegionGroups();
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
        /// 得到一个对象实体
        /// </summary>
        public Model.Shop.Shipping.ShippingRegionGroups DataRowToModel(DataRow row)
        {
            Model.Shop.Shipping.ShippingRegionGroups model = new Model.Shop.Shipping.ShippingRegionGroups();
            if (row != null)
            {
                if (row["GroupId"] != null && row["GroupId"].ToString() != "")
                {
                    model.GroupId = int.Parse(row["GroupId"].ToString());
                }
                if (row["ModeId"] != null && row["ModeId"].ToString() != "")
                {
                    model.ModeId = int.Parse(row["ModeId"].ToString());
                }
                if (row["Price"] != null && row["Price"].ToString() != "")
                {
                    model.Price = decimal.Parse(row["Price"].ToString());
                }
                if (row["AddPrice"] != null && row["AddPrice"].ToString() != "")
                {
                    model.AddPrice = decimal.Parse(row["AddPrice"].ToString());
                }
                if (row.Table.Columns.Contains("RegionIds"))
                {
                    model.RegionIds = row.Field<string>("RegionIds").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
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
            strSql.Append("select GroupId,ModeId,Price,AddPrice ");
            strSql.Append(" FROM Shop_ShippingRegionGroups ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
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
            strSql.Append(" GroupId,ModeId,Price,AddPrice ");
            strSql.Append(" FROM Shop_ShippingRegionGroups ");
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
            strSql.Append("select count(1) FROM Shop_ShippingRegionGroups ");
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
                strSql.Append("order by T.GroupId desc");
            }
            strSql.Append(")AS Row, T.*  from Shop_ShippingRegionGroups T ");
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
            parameters[0].Value = "Shop_ShippingRegionGroups";
            parameters[1].Value = "GroupId";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod

        /// <summary>
        /// 清空配送地区价格
        /// </summary>
        public bool ClearShippingRegionGroups(int modeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("DELETE Shop_ShippingRegionGroups WHERE ModeId = @ModeId; ");
            strSql.Append("DELETE Shop_ShippingRegions WHERE ModeId = @ModeId ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ModeId", SqlDbType.Int,4)
            };
            parameters[0].Value = modeId;
            return (DbHelperSQL.ExecuteSql(strSql.ToString(), parameters) > 0);
        }

        /// <summary>
        /// 保存配送地区价格
        /// </summary>
        public bool SaveShippingRegionGroups(List<Model.Shop.Shipping.ShippingRegionGroups> list)
        {
            int groupId;
            List<string> listKey = new List<string>();
            List<string> listRegionIds;

            SQLServerDAL.Shop.Shipping.ShippingRegions shippingRegManage = new ShippingRegions();

            foreach (Model.Shop.Shipping.ShippingRegionGroups regionGroup in list)
            {
                listRegionIds = regionGroup.RegionIds.ToList();
                listRegionIds.RemoveAll(listKey.Contains);
                if (listRegionIds.Count < 1) continue;

                listKey.AddRange(listRegionIds);
                groupId = Add(regionGroup);

                foreach (string regionId in listRegionIds)
                {
                    shippingRegManage.Add(new Model.Shop.Shipping.ShippingRegions
                    {
                        GroupId = groupId,
                        ModeId = regionGroup.ModeId,
                        RegionId = Common.Globals.SafeInt(regionId, -1)
                    });
                }
            }
            return true;
        }

        /// <summary>
        /// 获取配送地区价格
        /// </summary>
        public DataSet GetShippingRegionGroups(int modeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT  *
      , ( SELECT    STUFF(( SELECT  ',' + CONVERT(NVARCHAR, RegionId)
                            FROM    Shop_ShippingRegions
                            WHERE   RG.GroupId = GroupId
                          FOR
                            XML PATH('')
                          ), 1, 1, '')
        ) RegionIds
FROM    Shop_ShippingRegionGroups RG
WHERE RG.ModeId = @ModeId
        ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ModeId", SqlDbType.Int,4)
            };
            parameters[0].Value = modeId;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.Shop.Shipping.ShippingRegionGroups GetShippingRegion(int modeId,int topRegionId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT TOP 1 *
FROM    Shop_ShippingRegionGroups RG
WHERE   EXISTS ( SELECT *
                 FROM   Shop_ShippingRegions
                 WHERE  GroupId = RG.GroupId
                        AND ModeId = @ModeId
                        AND RegionId = @TopRegionId )
        ");
            SqlParameter[] parameters = {
                    new SqlParameter("@ModeId", SqlDbType.Int,4),
                    new SqlParameter("@TopRegionId", SqlDbType.Int,4)
            };
            parameters[0].Value = modeId;
            parameters[1].Value = topRegionId;

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

        //SELECT  STUFF(( SELECT  ',' + CONVERT(NVARCHAR, RegionId)
        //                FROM    Ms_Regions
        //                WHERE   Depth = 1
        //              FOR
        //                XML PATH('')
        //              ), 1, 1, '')

        #endregion  ExtensionMethod
    }
}

