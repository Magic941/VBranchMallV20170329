using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.IDAL.SysManage;
using Maticsoft.DALFactory;
namespace Maticsoft.BLL.SysManage
{
	/// <summary>
	/// 验证邮件
	/// </summary>
	public partial class VerifyMail
	{
        private readonly IVerifyMail dal = DASysManage.CreateVerifyMail();		
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string KeyValue)
		{
			return dal.Exists(KeyValue);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Maticsoft.Model.SysManage.VerifyMail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SysManage.VerifyMail model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string KeyValue)
		{
			
			return dal.Delete(KeyValue);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string KeyValuelist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(KeyValuelist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SysManage.VerifyMail GetModel(string KeyValue)
		{
			
			return dal.GetModel(KeyValue);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SysManage.VerifyMail GetModelByCache(string KeyValue)
		{
			
			string CacheKey = "VerifyMailModel-" + KeyValue;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(KeyValue);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SysManage.VerifyMail)objModel;
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
		public List<Maticsoft.Model.SysManage.VerifyMail> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SysManage.VerifyMail> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SysManage.VerifyMail> modelList = new List<Maticsoft.Model.SysManage.VerifyMail>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SysManage.VerifyMail model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.SysManage.VerifyMail();
					if(dt.Rows[n]["UserName"]!=null && dt.Rows[n]["UserName"].ToString()!="")
					{
					model.UserName=dt.Rows[n]["UserName"].ToString();
					}
					if(dt.Rows[n]["KeyValue"]!=null && dt.Rows[n]["KeyValue"].ToString()!="")
					{
					model.KeyValue=dt.Rows[n]["KeyValue"].ToString();
					}
					if(dt.Rows[n]["CreatedDate"]!=null && dt.Rows[n]["CreatedDate"].ToString()!="")
					{
						model.CreatedDate=DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
					}
					if(dt.Rows[n]["Status"]!=null && dt.Rows[n]["Status"].ToString()!="")
					{
						model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
					}
					if(dt.Rows[n]["ValidityType"]!=null && dt.Rows[n]["ValidityType"].ToString()!="")
					{
						model.ValidityType=int.Parse(dt.Rows[n]["ValidityType"].ToString());
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
		

		#endregion  Method


	}
}

