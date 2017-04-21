using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SysManage;
using Maticsoft.Common;
namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 系统参数配置
    /// </summary>
    public class ConfigSystem
    {
        private static IConfigSystem dal = DASysManage.CreateConfigSystem();

        #region  Method

        /// <summary>
        /// Whether there is Exists
        /// </summary>
        public static bool Exists(string Keyname)
        {
            return dal.Exists(Keyname);
        }

        /// <summary>
        /// Add a record
        /// </summary>
        public static int Add(string Keyname, string Value, string Description)
        {
            return dal.Add(Keyname, Value, Description);
        }
        /// <summary>
        /// Add a record
        /// </summary>
        public static int Add(string Keyname, string Value, string Description, ApplicationKeyType KeyType)
        {
            return dal.Add(Keyname, Value, Description, KeyType);
        }

        public static void Update(int ID, string Keyname, string Value, string Description)
        {
            dal.Update(ID, Keyname, Value, Description);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public static bool Update(string Keyname, string Value, string Description)
        {
            return dal.Update(Keyname, Value, Description);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public static bool Update(string Keyname, string Value)
        {
            return dal.Update(Keyname, Value);
        }

        /// <summary>
        /// Update a record
        /// </summary>
        public static bool Update(string Keyname, string Value, ApplicationKeyType KeyType)
        {
            return dal.Update(Keyname, Value, KeyType);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public static void Delete(int ID)
        {
            dal.Delete(ID);
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        public static string GetValue(int ID)
        {
            return dal.GetValue(ID);
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        /// <param name="Keyname"></param>
        /// <returns></returns>
        public static string GetValue(string Keyname)
        {
            return dal.GetValue(Keyname);
        }

        /// <summary>
        ///  Get an object entity，From cache
        /// </summary>
        /// <param name="Keyname"></param>
        /// <returns></returns>
        public static string GetValueByCache(string Keyname)
        {
            Hashtable ht = GetHashListByCache();
            if (ht != null && ht.ContainsKey(Keyname) && ht[Keyname] != null)
            {
                return ht[Keyname].ToString();
            }
            return GetValue(Keyname);
        }

        /// <summary>
        ///  Get an object entity，From cache
        /// </summary>
        /// <param name="Keyname"></param>
        /// <returns></returns>
        public static string GetValueByCache(string Keyname, ApplicationKeyType KeyType)
        {
            Hashtable ht = GetHashListByCache(KeyType);
            if (ht != null && ht.ContainsKey(Keyname) && ht[Keyname] != null)
            {
                return ht[Keyname].ToString();
            }
            return GetValue(Keyname);
        }

        /// <summary>
        ///  Get an object entity for INT，From cache
        /// </summary>
        /// <param name="Keyname"></param>
        /// <remarks>Default -1</remarks>
        public static int GetIntValueByCache(string Keyname)
        {
            Hashtable ht = GetHashListByCache();
            if (ht != null && ht.ContainsKey(Keyname) && ht[Keyname] != null)
            {
                return Globals.SafeInt(ht[Keyname], -1);
            }
            return Globals.SafeInt(GetValue(Keyname), -1);
        }

        /// <summary>
        ///  Get an object entity for bool，From cache
        /// </summary>
        /// <param name="Keyname"></param>
        /// <remarks>Default false</remarks>
        public static bool GetBoolValueByCache(string Keyname)
        {
            Hashtable ht = GetHashListByCache();
            if (ht != null && ht.ContainsKey(Keyname) && ht[Keyname] != null)
            {
                return Globals.SafeBool(ht[Keyname], false);
            }
            return Globals.SafeBool(GetValue(Keyname), false);
        }

        /// <summary>
        ///  Get an object entity for bool，From cache
        /// </summary>
        /// <param name="Keyname"></param>
        /// <remarks>Default -1</remarks>
        public static decimal GetDecimalValueByCache(string Keyname)
        {
            Hashtable ht = GetHashListByCache();
            if (ht != null && ht.ContainsKey(Keyname) && ht[Keyname] != null)
            {
                return Globals.SafeDecimal(ht[Keyname], decimal.MinusOne);
            }
            return Globals.SafeDecimal(GetValue(Keyname), decimal.MinusOne);
        }

        /// <summary>
        /// Get an object list
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetHashList()
        {
            DataSet ds = dal.GetList("");
            Hashtable ht = new Hashtable();
            if (ds != null && (ds.Tables.Count > 0) && (ds.Tables[0] != null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Keyname = dr["Keyname"].ToString();
                    string Value = dr["Value"].ToString();
                    ht.Add(Keyname, Value);
                }
            }
            return ht;
        }

        /// <summary>
        /// Get an object list
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetHashList(ApplicationKeyType KeyType)
        {
            DataSet ds = dal.GetList(" KeyType=" + (int)KeyType);
            Hashtable ht = new Hashtable();
            if (ds != null && (ds.Tables.Count > 0) && (ds.Tables[0] != null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Keyname = dr["Keyname"].ToString();
                    string Value = dr["Value"].ToString();
                    ht.Add(Keyname, Value);
                }
            }
            return ht;
        }


        /// <summary>
        /// Get an object list，From the cache 
        /// </summary>
        public static Hashtable GetHashListByCache()
        {
            string CacheKey = "ConfigSystemHashList";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetHashList();
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(GetValue("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Hashtable)objModel;
        }

        /// <summary>
        /// Get an object list，From the cache 
        /// </summary>
        public static Hashtable GetHashListByCache(ApplicationKeyType keyType)
        {
            string CacheKey = "ConfigSystemHashList_" + keyType;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetHashList(keyType);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(GetValue("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Hashtable)objModel;
        }



        /// <summary>
        /// Query data list
        /// </summary>
        public static DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }


        #endregion  Method

        #region MethodEx

        /// <summary>
        /// 智能进行新增或更新操作
        /// </summary>
        /// <remarks>当KeyType为None时, 将Update Description字段</remarks>
        public static bool Modify(string keyname, string value, string description = "", ApplicationKeyType keyType = ApplicationKeyType.None)
        {
            if (string.IsNullOrWhiteSpace(description)) description = keyname;

            if (keyType == ApplicationKeyType.None)
            {
                if (Exists(keyname)) return Update(keyname, value, description);
                return Add(keyname, value, description) > 0;
            }

            if (Exists(keyname)) return Update(keyname, value);
            return Add(keyname, value, description, keyType) > 0;
        }

        /// <summary>
        /// clear the hashtable key by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ClearCacheByKey(string key)
        {
            try
            {
                Hashtable ht = GetHashListByCache();
                if (ht != null && ht.ContainsKey(key) && ht[key] != null)
                {
                    ht.Remove(key);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// clear the hashtable key by key and applicationkeytype and key 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ClearCacheByKeyAndType(ApplicationKeyType keyType, string key)
        {
            try
            {
                Hashtable ht = GetHashListByCache(keyType);
                if (ht != null && ht.ContainsKey(key) && ht[key] != null)
                {
                    ht.Remove(key);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void UpdateConnectionString(string connectionString)
        {
            dal.UpdateConnectionString(connectionString);
        }
        #endregion
    }
}
