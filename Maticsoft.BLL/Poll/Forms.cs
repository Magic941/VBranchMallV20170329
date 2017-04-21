using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Poll;

namespace Maticsoft.BLL.Poll
{
    /// <summary>
    /// 业务逻辑类Forms 的摘要说明。
    /// </summary>
    public class Forms
    {
        private readonly IForms dal = DAPoll.CreateForms();

        #region 成员方法

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
        public bool Exists(int FormID)
        {
            return dal.Exists(FormID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Poll.Forms model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public int Update(Maticsoft.Model.Poll.Forms model)
        {
           return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int FormID)
        {
            dal.Delete(FormID);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ClassIDlist"></param>
        /// <returns></returns>
        public bool DeleteList(string ClassIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(ClassIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Poll.Forms GetModel(int FormID)
        {
            return dal.GetModel(FormID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Maticsoft.Model.Poll.Forms GetModelByCache(int FormID)
        {
            string CacheKey = "FormsModel-" + FormID;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(FormID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Poll.Forms)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Poll.Forms> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.Forms> modelList = new List<Maticsoft.Model.Poll.Forms>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Poll.Forms model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Poll.Forms();
                    if (ds.Tables[0].Rows[n]["FormID"].ToString() != "")
                    {
                        model.FormID = int.Parse(ds.Tables[0].Rows[n]["FormID"].ToString());
                    }
                    model.Name = ds.Tables[0].Rows[n]["Name"].ToString();
                    model.Description = ds.Tables[0].Rows[n]["Description"].ToString();
                    if (ds.Tables[0].Rows[0]["IsActive"] != null && ds.Tables[0].Rows[0]["IsActive"].ToString() != "")
                    {
                        if ((ds.Tables[0].Rows[0]["IsActive"].ToString() == "1") || (ds.Tables[0].Rows[0]["IsActive"].ToString().ToLower() == "true"))
                        {
                            model.IsActive = true;
                        }
                        else
                        {
                            model.IsActive = false;
                        }
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
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion 成员方法
    }
}