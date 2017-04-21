/**
* EntryForm.cs
*
* 功 能： 
* 类 名： EntryForm
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Ms;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Ms;
namespace Maticsoft.BLL.Ms
{
    /// <summary>
    /// 报名表
    /// </summary>
    public partial class EntryForm
    {
        private readonly IEntryForm dal = DAMembers.CreateEntryForm();
        public EntryForm()
        { }

        #region  Method

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
        public bool Exists(int Id)
        {
            return dal.Exists(Id);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string UserName)
        {
            return dal.Exists(UserName);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Ms.EntryForm model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Ms.EntryForm model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Updates(Maticsoft.Model.Ms.EntryForm model)
        {
            return dal.Updates(model);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int Id)
        {

            return dal.Delete(Id);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string Idlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(Idlist, 0));
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Ms.EntryForm GetModel(int Id)
        {

            return dal.GetModel(Id);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Ms.EntryForm GetModelByCache(int Id)
        {

            string CacheKey = "EntryFormModel-" + Id;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(Id);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Ms.EntryForm)objModel;
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
        public List<Maticsoft.Model.Ms.EntryForm> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Ms.EntryForm> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Ms.EntryForm> modelList = new List<Maticsoft.Model.Ms.EntryForm>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Ms.EntryForm model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Ms.EntryForm();
                    if (dt.Rows[n]["Id"] != null && dt.Rows[n]["Id"].ToString() != "")
                    {
                        model.Id = int.Parse(dt.Rows[n]["Id"].ToString());
                    }
                    if (dt.Rows[n]["UserName"] != null && dt.Rows[n]["UserName"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserName"].ToString();
                    }
                    if (dt.Rows[n]["Age"] != null && dt.Rows[n]["Age"].ToString() != "")
                    {
                        model.Age = int.Parse(dt.Rows[n]["Age"].ToString());
                    }
                    if (dt.Rows[n]["Email"] != null && dt.Rows[n]["Email"].ToString() != "")
                    {
                        model.Email = dt.Rows[n]["Email"].ToString();
                    }
                    if (dt.Rows[n]["TelPhone"] != null && dt.Rows[n]["TelPhone"].ToString() != "")
                    {
                        model.TelPhone = dt.Rows[n]["TelPhone"].ToString();
                    }
                    if (dt.Rows[n]["Phone"] != null && dt.Rows[n]["Phone"].ToString() != "")
                    {
                        model.Phone = dt.Rows[n]["Phone"].ToString();
                    }
                    if (dt.Rows[n]["QQ"] != null && dt.Rows[n]["QQ"].ToString() != "")
                    {
                        model.QQ = dt.Rows[n]["QQ"].ToString();
                    }
                    if (dt.Rows[n]["MSN"] != null && dt.Rows[n]["MSN"].ToString() != "")
                    {
                        model.MSN = dt.Rows[n]["MSN"].ToString();
                    }
                    if (dt.Rows[n]["HouseAddress"] != null && dt.Rows[n]["HouseAddress"].ToString() != "")
                    {
                        model.HouseAddress = dt.Rows[n]["HouseAddress"].ToString();
                    }
                    if (dt.Rows[n]["CompanyAddress"] != null && dt.Rows[n]["CompanyAddress"].ToString() != "")
                    {
                        model.CompanyAddress = dt.Rows[n]["CompanyAddress"].ToString();
                    }
                    if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    if (dt.Rows[n]["Sex"] != null && dt.Rows[n]["Sex"].ToString() != "")
                    {
                        model.Sex = dt.Rows[n]["Sex"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["remark"] != null && dt.Rows[n]["remark"].ToString() != "")
                    {
                        model.Remark = dt.Rows[n]["remark"].ToString();
                    }
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["ApplyType"] != null && dt.Rows[n]["ApplyType"].ToString() != "")
                    {
                        model.ApplyType = int.Parse(dt.Rows[n]["ApplyType"].ToString());
                    }
                    if (dt.Rows[n]["AppStatus"] != null && dt.Rows[n]["AppStatus"].ToString() != "")
                    {
                        model.AppStatus = int.Parse(dt.Rows[n]["AppStatus"].ToString());
                    }
                    if (dt.Rows[n]["AppName"] != null && dt.Rows[n]["AppName"].ToString() != "")
                    {
                        model.AppName = dt.Rows[n]["AppName"].ToString();
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

        #endregion  Method

        #region 扩展方法
        /// <summary>
        /// 批量处理
        /// </summary>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }

        /// <summary>
        /// 根据用户名查询出当前用户是否申请好邻体验店
        /// </summary>
        /// <returns></returns>
        public Maticsoft.Model.Ms.EntryForm GetByUserNameModel(string username)
        {

            return dal.GetByUserNameModel(username);
        }

        #endregion
    }
}

