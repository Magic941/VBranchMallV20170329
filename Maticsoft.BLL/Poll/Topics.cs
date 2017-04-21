using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Poll;

namespace Maticsoft.BLL.Poll
{
    /// <summary>
    /// 业务逻辑类Topics 的摘要说明。
    /// </summary>
    public class Topics
    {
        private readonly ITopics dal = DAPoll.CreateTopics();

        #region 成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int FormID, string Title)
        {
            return dal.Exists(FormID, Title);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Poll.Topics model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(Maticsoft.Model.Poll.Topics model)
        {
            dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(int ID)
        {
            dal.Delete(ID);
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
        public Maticsoft.Model.Poll.Topics GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        public Maticsoft.Model.Poll.Topics GetModelByCache(int ID)
        {
            string CacheKey = "TopicsModel-" + ID;
            object objModel = Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(ID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Poll.Topics)objModel;
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
        public List<Maticsoft.Model.Poll.Topics> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            List<Maticsoft.Model.Poll.Topics> modelList = new List<Maticsoft.Model.Poll.Topics>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Poll.Topics model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Poll.Topics();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    model.Title = ds.Tables[0].Rows[n]["Title"].ToString();
                    if (ds.Tables[0].Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(ds.Tables[0].Rows[n]["Type"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["FormID"].ToString() != "")
                    {
                        model.FormID = int.Parse(ds.Tables[0].Rows[n]["FormID"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["RowNum"].ToString() != "")
                    {
                        model.RowNum = int.Parse(ds.Tables[0].Rows[n]["RowNum"].ToString());
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

        public DataSet GetListByForm(int FormID)
        {
            return GetList(" FormID=" + FormID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Poll.Topics> GetModelList(int Top, int formid)
        {
            BLL.Poll.Forms bllForms = new Forms();
            Model.Poll.Forms forms = bllForms.GetModelByCache(formid);
            if (forms == null || forms.IsActive!=true)
                return null;
            DataSet ds = dal.GetList(Top, string.Format(" Type in (0,1) and  FormID={0} ",formid), "  ORDER BY ID ASC ");
            List<Maticsoft.Model.Poll.Topics> modelList = new List<Maticsoft.Model.Poll.Topics>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Poll.Topics model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Poll.Topics();
                    if (ds.Tables[0].Rows[n]["ID"].ToString() != "")
                    {
                        model.ID = int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
                    }
                    model.Title = ds.Tables[0].Rows[n]["Title"].ToString();
                    if (ds.Tables[0].Rows[n]["Type"].ToString() != "")
                    {
                        model.Type = int.Parse(ds.Tables[0].Rows[n]["Type"].ToString());
                    }
                    if (ds.Tables[0].Rows[n]["FormID"].ToString() != "")
                    {
                        model.FormID = int.Parse(ds.Tables[0].Rows[n]["FormID"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion 成员方法
    }
}