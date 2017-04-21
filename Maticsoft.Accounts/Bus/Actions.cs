using System;
using System.Collections;
using System.Data;
using Maticsoft.Accounts.IData;

namespace Maticsoft.Accounts.Bus
{
    /// <summary>
    /// 功能行为
    /// </summary>
    public class Actions
    {
        private IData.IActions dal = PubConstant.IsSQLServer ? (IActions)new Data.Actions() : new MySqlData.Actions();

        public bool Exists(string Description)
        {
            return dal.Exists(Description);
        }
        /// <summary>
        /// Add a record
        /// </summary>
        public int Add(string Description)
        {
            return dal.Add(Description);
        }

        /// <summary>
        /// Add a record,include perimission
        /// </summary>
        public int Add(string Description, int PermissionID)
        {
            return dal.Add(Description, PermissionID);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public void Update(int ActionID, string Description)
        {
            dal.Update(ActionID, Description);
        }

        /// <summary>
        /// Update a record, include permission
        /// </summary>
        public void Update(int ActionID, string Description, int PermissionID)
        {
            dal.Update(ActionID, Description, PermissionID);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int ActionID)
        {
            dal.Delete(ActionID);
        }

        /// <summary>
        /// Get Description
        /// </summary>
        public string GetDescription(int ActionID)
        {
            return dal.GetDescription(ActionID);
        }

        /// <summary>
        /// Query data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        #region expand

        /// <summary>
        /// Relevance between ActionID and PermissionID
        /// </summary>
        public void AddPermission(int ActionID, int PermissionID)
        {
            dal.AddPermission(ActionID, PermissionID);
        }
        /// <summary>
        /// 批量增加权限设置
        /// </summary>
        /// <param name="ActionIDs"></param>
        /// <param name="PermissionID"></param>
        public void AddPermission(string ActionIDs, int PermissionID)
        {
            dal.AddPermission(ActionIDs, PermissionID);
        }
        /// <summary>
        /// Clear relevance
        /// </summary>        
        public void ClearPermissions(int ActionID)
        {
            dal.ClearPermissions(ActionID);
        }

        /// <summary>
        /// Get an object list
        /// </summary>
        /// <returns></returns>
        public Hashtable GetHashList()
        {
            DataSet ds = dal.GetList("");
            Hashtable ht = new Hashtable();
            if ((ds.Tables.Count > 0) && (ds.Tables[0] != null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Keyname = dr["ActionID"].ToString();
                    string Value = dr["PermissionID"].ToString();
                    ht.Add(Keyname, Value);
                }
            }
            return ht;
        }

        /// <summary>
        /// Get an object list，From the cache
        /// </summary>
        public Hashtable GetHashListByCache()
        {
            string CacheKey = "ActionsPermissionHashList";
            object objModel = DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetHashList();
                    if (objModel != null)
                    {
                        int CacheTime = ConfigHelper.GetConfigInt("CacheTime");
                        DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Hashtable)objModel;
        }


        #endregion

    }
}
