/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：GradeConfig.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/11/12 14:54:12
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using Maticsoft.Common;

namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 会员等级
    /// </summary>
    public partial class GradeConfig
    {
        private readonly IGradeConfig dal = DASNS.CreateGradeConfig();

        public GradeConfig()
        { }

        #region Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int GradeID)
        {
            return dal.Exists(GradeID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.GradeConfig model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.GradeConfig model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GradeID)
        {
            return dal.Delete(GradeID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string GradeIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(GradeIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.GradeConfig GetModel(int GradeID)
        {
            return dal.GetModel(GradeID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.GradeConfig GetModelByCache(int GradeID)
        {
            string CacheKey = "GradeConfigModel-" + GradeID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GradeID);
                    if (objModel != null)
                    {
                      int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.GradeConfig)objModel;
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
        public List<Maticsoft.Model.SNS.GradeConfig> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.GradeConfig> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GradeConfig> modelList = new List<Maticsoft.Model.SNS.GradeConfig>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.GradeConfig model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.SNS.GradeConfig();
                    if (dt.Rows[n]["GradeID"] != null && dt.Rows[n]["GradeID"].ToString() != "")
                    {
                        model.GradeID = int.Parse(dt.Rows[n]["GradeID"].ToString());
                    }
                    if (dt.Rows[n]["GradeName"] != null && dt.Rows[n]["GradeName"].ToString() != "")
                    {
                        model.GradeName = dt.Rows[n]["GradeName"].ToString();
                    }
                    if (dt.Rows[n]["MinRange"] != null && dt.Rows[n]["MinRange"].ToString() != "")
                    {
                        model.MinRange = int.Parse(dt.Rows[n]["MinRange"].ToString());
                    }
                    if (dt.Rows[n]["MaxRange"] != null && dt.Rows[n]["MaxRange"].ToString() != "")
                    {
                        model.MaxRange = int.Parse(dt.Rows[n]["MaxRange"].ToString());
                    }
                    if (dt.Rows[n]["Remark"] != null && dt.Rows[n]["Remark"].ToString() != "")
                    {
                        model.Remark = dt.Rows[n]["Remark"].ToString();
                    }
                    modelList.Add(model);
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

       

        #endregion Method

        /// <summary>
        /// 根据用户分数获取等级
        /// </summary>
        /// <param name="grades">用户分数</param>
        /// <returns></returns>
        public string GetUserLevel(int? grades)
        {
            if (grades.HasValue)
            {
                Maticsoft.Model.SNS.GradeConfig model = dal.GetUserLevel(grades);
                if (model != null)
                {
                    return model.GradeName;
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }
        }
    }
}