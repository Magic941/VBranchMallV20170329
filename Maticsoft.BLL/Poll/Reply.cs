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
	/// 业务逻辑类Reply 的摘要说明。
	/// </summary>
	public class Reply
	{
        private readonly IReply dal = DAPoll.CreateReply();

		#region  成员方法

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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Poll.Reply model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(Maticsoft.Model.Poll.Reply model)
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
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Poll.Reply GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中。
		/// </summary>
		public Maticsoft.Model.Poll.Reply GetModelByCache(int ID)
		{
			
			string CacheKey = "ReplyModel-" + ID;
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
			return (Maticsoft.Model.Poll.Reply)objModel;
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
		public List<Maticsoft.Model.Poll.Reply> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			List<Maticsoft.Model.Poll.Reply> modelList = new List<Maticsoft.Model.Poll.Reply>();
			int rowsCount = ds.Tables[0].Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Poll.Reply model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Poll.Reply();
					if(ds.Tables[0].Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(ds.Tables[0].Rows[n]["ID"].ToString());
					}
					if(ds.Tables[0].Rows[n]["TopicID"].ToString()!="")
					{
						model.TopicID=int.Parse(ds.Tables[0].Rows[n]["TopicID"].ToString());
					}
					model.ReContent=ds.Tables[0].Rows[n]["ReContent"].ToString();
					if(ds.Tables[0].Rows[n]["ReTime"].ToString()!="")
					{
						model.ReTime=DateTime.Parse(ds.Tables[0].Rows[n]["ReTime"].ToString());
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

		#endregion  成员方法
	}
}

