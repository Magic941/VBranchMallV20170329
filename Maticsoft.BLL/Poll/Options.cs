using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Poll;
using Maticsoft.Model;
using Maticsoft.Common;
namespace Maticsoft.BLL.Poll
{
	/// <summary>
	/// 业务逻辑类Options 的摘要说明。
	/// </summary>
	public class Options
	{
        private readonly IOptions dal = DAPoll.CreateOptions();
		#region  成员方法

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int TopicID, string Name)
		{
            return dal.Exists(TopicID, Name);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Poll.Options model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Maticsoft.Model.Poll.Options model)
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
		public Maticsoft.Model.Poll.Options GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Maticsoft.Model.Poll.Options GetModelByCache(int ID)
		{
			
			string CacheKey = "OptionsModel-" + ID;
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
				catch{}
			}
			return (Maticsoft.Model.Poll.Options)objModel;
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
		public List<Maticsoft.Model.Poll.Options> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			List<Maticsoft.Model.Poll.Options> modelList = new List<Maticsoft.Model.Poll.Options>();
			int rowsCount = ds.Tables[0].Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Poll.Options model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Poll.Options();
					if(ds.Tables[0].Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
					}
					model.Name=ds.Tables[0].Rows[n]["Name"].ToString();
					if(ds.Tables[0].Rows[n]["TopicID"].ToString()!="")
					{
						model.TopicID=int.Parse(ds.Tables[0].Rows[n]["TopicID"].ToString());
					}
					if(ds.Tables[0].Rows[n]["isChecked"].ToString()!="")
					{
						model.isChecked=int.Parse(ds.Tables[0].Rows[n]["isChecked"].ToString());
					}
					if(ds.Tables[0].Rows[n]["SubmitNum"].ToString()!="")
					{
						model.SubmitNum=int.Parse(ds.Tables[0].Rows[n]["SubmitNum"].ToString());
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

        public DataSet GetListByTopic(int TopicID)
        {
            return GetList(" TopicID=" + TopicID);
        }

        
        /// <summary>
        /// 得到问卷投票统计
        /// </summary>
        /// <param name="FormID">问卷编号</param>
        /// <returns></returns>
        public DataSet GetCountList(int FormID)
        {
            return dal.GetCountList(FormID);
        }

	    /// <summary>
	    /// 得到问卷投票统计
	    /// </summary>
	    /// <param name="strwhere"></param>
	    /// <returns></returns>
	    public List<Model.Poll.Options> GetCountList(string strwhere)
	    {
	        DataSet ds= dal.GetCountList(strwhere);
            List<Maticsoft.Model.Poll.Options> modelList = new List<Maticsoft.Model.Poll.Options>();
            int rowsCount = ds.Tables[0].Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Poll.Options model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Poll.Options();
                    model.Name = ds.Tables[0].Rows[n]["Name"].ToString();
                    if (ds.Tables[0].Rows[n]["TopicID"].ToString() != "")
                    {
                        model.TopicID = int.Parse(ds.Tables[0].Rows[n]["TopicID"].ToString());
                    }  
                    if (ds.Tables[0].Rows[n]["SubmitNum"].ToString() != "")
                    {
                        model.SubmitNum = int.Parse(ds.Tables[0].Rows[n]["SubmitNum"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList; 
	    }

	    #endregion  成员方法
	}
}

