using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SysManage;
namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 错误日志
    /// </summary>
    public class ErrorLog
    {
        private static IErrorLog dal = DASysManage.CreateErrorLog();
        
        #region  Method
        /// <summary>
        /// Add a record
        /// </summary>
        public static int Add(Maticsoft.Model.SysManage.ErrorLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public static void Update(Maticsoft.Model.SysManage.ErrorLog model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }
        public static void Delete(string IDList)
        {
            dal.Delete(IDList);
        }
        /// <summary>
        /// 删除某一日期之前的数据
        /// </summary>
        /// <param name="dtDateBefore">日期</param>
        public static void DeleteByDate(DateTime dtDateBefore)
        {
            dal.DeleteByDate(dtDateBefore);
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        public static Maticsoft.Model.SysManage.ErrorLog GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// Get an object entity，From the cache
        /// </summary>
        public static Maticsoft.Model.SysManage.ErrorLog GetModelByCache(int ID)
        {

            string CacheKey = "ErrorLogModel-" + ID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SysManage.ErrorLog)objModel;
        }

        /// <summary>
        /// Query data list
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// Query top lines of data 
        /// </summary>
        public static DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// Query data list
        /// </summary>
        public static List<Maticsoft.Model.SysManage.ErrorLog> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// Query data list
        /// </summary>
        public static List<Maticsoft.Model.SysManage.ErrorLog> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SysManage.ErrorLog> modelList = new List<Maticsoft.Model.SysManage.ErrorLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SysManage.ErrorLog model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.SysManage.ErrorLog();
                    if (dt.Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(dt.Rows[n]["ID"].ToString());
                    }
                    if (dt.Rows[n]["OPTime"].ToString() != "")
                    {
                        model.OPTime = DateTime.Parse(dt.Rows[n]["OPTime"].ToString());
                    }
                    model.Url = dt.Rows[n]["Url"].ToString();
                    model.Loginfo = dt.Rows[n]["Loginfo"].ToString();
                    model.StackTrace = dt.Rows[n]["StackTrace"].ToString();
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// Query data list
        /// </summary>
        public static DataSet GetAllList()
        {
            return GetList(-1,"","ID desc");
        }

        /// <summary>
        /// Paging data list
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  Method


    }
}
