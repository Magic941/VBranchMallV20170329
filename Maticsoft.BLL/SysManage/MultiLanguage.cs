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
    /// 多语言数据管理
    /// </summary>
    public class MultiLanguage
    {
        //根据编号，语言代码，获取语言信息， 从缓存中
        
        private readonly IMultiLanguage dal = DASysManage.CreateMultiLanguage();
		
		#region  Method
		/// <summary>
        /// Whether there is Exists
        /// </summary>
        public bool Exists(string MultiLang_iField, int MultiLang_iPKValue, string MultiLang_cLang)
        {
            return dal.Exists(MultiLang_iField, MultiLang_iPKValue, MultiLang_cLang);
        }        
        /// <summary>
        /// Add a record
        /// </summary>
        public int Add(string MultiLang_iField, int MultiLang_iPKValue, string MultiLang_cLang, string MultiLang_cValue)
        {
            return dal.Add(MultiLang_iField, MultiLang_iPKValue, MultiLang_cLang, MultiLang_cValue);
        }
        /// <summary>
        /// Update MultiLang_cValue
        /// </summary>
        public void Update(int MultiLang_iID, string MultiLang_cValue)
        {
            dal.Update(MultiLang_iID, MultiLang_cValue);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int MultiLang_iID)
        {
            dal.Delete(MultiLang_iID);
        }
        
        /// <summary>
        /// Get an object entity
        /// </summary>
        public string GetLangValue(int MultiLang_iID)
        {
            return dal.GetModel(MultiLang_iID);
        }

        /// <summary>
        /// Get an object entity
        /// </summary>
        public string GetLangValue(string MultiLang_iField, int MultiLang_iPKValue, string MultiLang_cLang)
        {
            return dal.GetModel( MultiLang_iField,  MultiLang_iPKValue,  MultiLang_cLang);
        }
        /// <summary>
        /// Get an Lang Value，From the cache
        /// </summary>
        public string GetLangValueByCache(int MultiLang_iID)
        {
            string CacheKey = "MultiLangModel-" + MultiLang_iID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(MultiLang_iID);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }


        /// <summary>
        /// Query data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        
        #endregion



        #region   查询指定字段的，指定语言的 所有值数据

        /// <summary>
        /// 查询指定字段的，指定语言的 所有值数据
        /// </summary>
        public DataSet GetValueListByLang(string MultiLang_iField, string MultiLang_cLang)
        {
            return dal.GetValueListByLang(MultiLang_iField, MultiLang_cLang);
        }


        /// <summary>
        /// 查询指定字段的，指定语言的 所有值数据
        /// </summary>
        /// <returns></returns>
        public Hashtable GetHashValueListByLang(string MultiLang_iField, string MultiLang_cLang)
        {
            DataSet ds = dal.GetValueListByLang(MultiLang_iField, MultiLang_cLang);
            Hashtable ht = new Hashtable();
            if ((ds.Tables.Count > 0) && (ds.Tables[0] != null))
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string Keyname = dr["MultiLang_iPKValue"].ToString();
                    string Value = dr["MultiLang_cValue"].ToString();
                    ht.Add(Keyname, Value);
                }
            }
            return ht;
        }

        /// <summary>
        /// Get an object list，From the cache
        /// </summary>
        public Hashtable GetHashValueListByLangCache(string MultiLang_iField, string MultiLang_cLang)
        {
            string CacheKey = "HashValueListByLang" + MultiLang_iField + MultiLang_cLang;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetHashValueListByLang(MultiLang_iField, MultiLang_cLang);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Hashtable)objModel;
        }

        #endregion

        #region

        /// <summary>
        /// 查询指定字段，指定值的所有 语言数据
        /// </summary>
        public DataSet GetLangListByValue(string MultiLang_iField, int MultiLang_iPKValue)
        {
            return dal.GetLangListByValue(MultiLang_iField, MultiLang_iPKValue);
        }
      
        

        /// <summary>
        /// Query top lines of data 
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
               

		/// <summary>
		/// Query data list
		/// </summary>
		public DataSet GetAllList()
		{
			return GetList("");
		}



        /// <summary>
        /// Get all Multi Lang value，From the cache
        /// </summary>
        public DataSet GetAllListByCache()
        {
            string CacheKey = "GetAllListMultiLang";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetList("");
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return (DataSet)objModel;
        }
		#endregion  Method

        public DataSet GetLanguageList()
        {
            return dal.GetLanguageList();
        }
        public DataSet GetLanguageListByCache()
        {
            string CacheKey = "GetLanguageList";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetLanguageList();
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return (DataSet)objModel;
        }
        /// <summary>
        ///  得到语言名称 
        /// </summary>
        /// <param name="Language_cCode"></param>
        /// <returns></returns>
        public string GetLanguageNameByCache(string Language_cCode)
        {
            string CacheKey = "Language-" + Language_cCode;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetLanguageName(Language_cCode);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }
        /// <summary>
        /// 默认语言
        /// </summary>
        /// <returns></returns>
        public string GetDefaultLangCodeByCache()
        {
            string CacheKey = "DefaultLanguageCode";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetDefaultLangCode();
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch
                { }
            }
            return objModel.ToString();
        }
    }
}
