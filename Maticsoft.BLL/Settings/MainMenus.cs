/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。 
//
// 文件名：WebMenuConfig.cs
// 文件功能描述：网站菜单导航业务逻辑层
// 
// 创建标识：2012年5月23日 16:30:09
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Settings;
namespace Maticsoft.BLL.Settings
{
	/// <summary>
	/// 导航菜单
	/// </summary>
	public partial class MainMenus
	{
        private readonly IMainMenus dal = DASettings.CreateMainMenus();
        public MainMenus()
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
        public bool Exists(int MenuID)
        {
            return dal.Exists(MenuID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Settings.MainMenus model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Settings.MainMenus model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int MenuID)
        {

            return dal.Delete(MenuID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string MenuIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(MenuIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Settings.MainMenus GetModel(int MenuID)
        {

            return dal.GetModel(MenuID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Settings.MainMenus GetModelByCache(int MenuID)
        {

            string CacheKey = "WebMenuConfigModel-" + MenuID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(MenuID);
                    if (objModel != null)
                    {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Settings.MainMenus)objModel;
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
        public List<Maticsoft.Model.Settings.MainMenus> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Settings.MainMenus> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Settings.MainMenus> modelList = new List<Maticsoft.Model.Settings.MainMenus>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Settings.MainMenus model;
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

        #region 扩展方法
        public List<Maticsoft.Model.Settings.MainMenus> GetMenusByArea(Maticsoft.Model.Ms.EnumHelper.AreaType area,string theme="")
        {
            string strWhere = " IsUsed=1 and NavArea=" + (int) area;
            if (!String.IsNullOrWhiteSpace(theme))
            {
                strWhere += " and (NavTheme='" + theme + "' or NavTheme='')";
            }
            return GetModelList(strWhere+" order by Sequence");
        }

        public List<Maticsoft.Model.Settings.MainMenus> GetMenusByAreaByCacle(Maticsoft.Model.Ms.EnumHelper.AreaType area, string theme = "")
        {
            string CacheKey = "GetMenusByAreaByCacle-" + area + theme;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetMenusByArea(area,theme);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Settings.MainMenus>)objModel;
      
        }
        #endregion 
    }
}

