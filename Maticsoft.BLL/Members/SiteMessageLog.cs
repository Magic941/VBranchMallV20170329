using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
namespace Maticsoft.BLL.Members
{
	/// <summary>
	/// 站内信日志
	/// </summary>
	public partial class SiteMessageLog
	{
        private readonly ISiteMessageLog dal = DAMembers.CreateSiteMessageLog();
		public SiteMessageLog()
		{}
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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Members.SiteMessageLog model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Members.SiteMessageLog model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Members.SiteMessageLog GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Members.SiteMessageLog GetModelByCache(int ID)
		{
			
			string CacheKey = "SiteMessageLogModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Members.SiteMessageLog)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Members.SiteMessageLog> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Members.SiteMessageLog> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Members.SiteMessageLog> modelList = new List<Maticsoft.Model.Members.SiteMessageLog>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Members.SiteMessageLog model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Members.SiteMessageLog();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["MessageID"]!=null && dt.Rows[n]["MessageID"].ToString()!="")
					{
						model.MessageID=int.Parse(dt.Rows[n]["MessageID"].ToString());
					}
					if(dt.Rows[n]["MessageType"]!=null && dt.Rows[n]["MessageType"].ToString()!="")
					{
					model.MessageType=dt.Rows[n]["MessageType"].ToString();
					}
					if(dt.Rows[n]["MessageState"]!=null && dt.Rows[n]["MessageState"].ToString()!="")
					{
					model.MessageState=dt.Rows[n]["MessageState"].ToString();
					}
					if(dt.Rows[n]["ReceiverID"]!=null && dt.Rows[n]["ReceiverID"].ToString()!="")
					{
						model.ReceiverID=int.Parse(dt.Rows[n]["ReceiverID"].ToString());
					}
					if(dt.Rows[n]["Ext1"]!=null && dt.Rows[n]["Ext1"].ToString()!="")
					{
					model.Ext1=dt.Rows[n]["Ext1"].ToString();
					}
					if(dt.Rows[n]["Ext2"]!=null && dt.Rows[n]["Ext2"].ToString()!="")
					{
					model.Ext2=dt.Rows[n]["Ext2"].ToString();
					}
					if(dt.Rows[n]["ReceiverUserName"]!=null && dt.Rows[n]["ReceiverUserName"].ToString()!="")
					{
					model.ReceiverUserName=dt.Rows[n]["ReceiverUserName"].ToString();
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method
	}
}

