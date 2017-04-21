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
	/// Enterprise
	/// </summary>
	public partial class Enterprise
	{
		private readonly IEnterprise dal=DAMs.CreateEnterprise();
		public Enterprise()
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
		public bool Exists(int EnterpriseID)
		{
			return dal.Exists(EnterpriseID);
		}

        /// <summary>
        /// 企业名称是否已存在
        /// </summary>
        public bool Exists(string Name)
        {
            return dal.Exists(Name);
        }

         /// <summary>
        /// 企业名称是否已存在
        /// </summary>
        public bool Exists(string Name, int EnterpriseID)
        {
            return dal.Exists(Name, EnterpriseID);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Ms.Enterprise model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Ms.Enterprise model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int EnterpriseID)
		{
			
			return dal.Delete(EnterpriseID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string EnterpriseIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(EnterpriseIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Ms.Enterprise GetModel(int EnterpriseID)
		{
			
			return dal.GetModel(EnterpriseID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Ms.Enterprise GetModelByCache(int EnterpriseID)
		{
			
			string CacheKey = "EnterpriseModel-" + EnterpriseID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(EnterpriseID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Ms.Enterprise)objModel;
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
		public List<Maticsoft.Model.Ms.Enterprise> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Ms.Enterprise> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Ms.Enterprise> modelList = new List<Maticsoft.Model.Ms.Enterprise>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Ms.Enterprise model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Ms.Enterprise();
					if(dt.Rows[n]["EnterpriseID"]!=null && dt.Rows[n]["EnterpriseID"].ToString()!="")
					{
						model.EnterpriseID=int.Parse(dt.Rows[n]["EnterpriseID"].ToString());
					}
					if(dt.Rows[n]["Name"]!=null && dt.Rows[n]["Name"].ToString()!="")
					{
					model.Name=dt.Rows[n]["Name"].ToString();
					}
					if(dt.Rows[n]["Introduction"]!=null && dt.Rows[n]["Introduction"].ToString()!="")
					{
					model.Introduction=dt.Rows[n]["Introduction"].ToString();
					}
					if(dt.Rows[n]["RegisteredCapital"]!=null && dt.Rows[n]["RegisteredCapital"].ToString()!="")
					{
						model.RegisteredCapital=int.Parse(dt.Rows[n]["RegisteredCapital"].ToString());
					}
					if(dt.Rows[n]["TelPhone"]!=null && dt.Rows[n]["TelPhone"].ToString()!="")
					{
					model.TelPhone=dt.Rows[n]["TelPhone"].ToString();
					}
					if(dt.Rows[n]["CellPhone"]!=null && dt.Rows[n]["CellPhone"].ToString()!="")
					{
					model.CellPhone=dt.Rows[n]["CellPhone"].ToString();
					}
					if(dt.Rows[n]["ContactMail"]!=null && dt.Rows[n]["ContactMail"].ToString()!="")
					{
					model.ContactMail=dt.Rows[n]["ContactMail"].ToString();
					}
					if(dt.Rows[n]["RegionID"]!=null && dt.Rows[n]["RegionID"].ToString()!="")
					{
						model.RegionID=int.Parse(dt.Rows[n]["RegionID"].ToString());
					}
					if(dt.Rows[n]["Address"]!=null && dt.Rows[n]["Address"].ToString()!="")
					{
					model.Address=dt.Rows[n]["Address"].ToString();
					}
					if(dt.Rows[n]["Remark"]!=null && dt.Rows[n]["Remark"].ToString()!="")
					{
					model.Remark=dt.Rows[n]["Remark"].ToString();
					}
					if(dt.Rows[n]["Contact"]!=null && dt.Rows[n]["Contact"].ToString()!="")
					{
					model.Contact=dt.Rows[n]["Contact"].ToString();
					}
					if(dt.Rows[n]["UserName"]!=null && dt.Rows[n]["UserName"].ToString()!="")
					{
					model.UserName=dt.Rows[n]["UserName"].ToString();
					}
					if(dt.Rows[n]["EstablishedDate"]!=null && dt.Rows[n]["EstablishedDate"].ToString()!="")
					{
						model.EstablishedDate=DateTime.Parse(dt.Rows[n]["EstablishedDate"].ToString());
					}
					if(dt.Rows[n]["EstablishedCity"]!=null && dt.Rows[n]["EstablishedCity"].ToString()!="")
					{
						model.EstablishedCity=int.Parse(dt.Rows[n]["EstablishedCity"].ToString());
					}
					if(dt.Rows[n]["LOGO"]!=null && dt.Rows[n]["LOGO"].ToString()!="")
					{
					model.LOGO=dt.Rows[n]["LOGO"].ToString();
					}
					if(dt.Rows[n]["Fax"]!=null && dt.Rows[n]["Fax"].ToString()!="")
					{
					model.Fax=dt.Rows[n]["Fax"].ToString();
					}
					if(dt.Rows[n]["PostCode"]!=null && dt.Rows[n]["PostCode"].ToString()!="")
					{
					model.PostCode=dt.Rows[n]["PostCode"].ToString();
					}
					if(dt.Rows[n]["HomePage"]!=null && dt.Rows[n]["HomePage"].ToString()!="")
					{
					model.HomePage=dt.Rows[n]["HomePage"].ToString();
					}
					if(dt.Rows[n]["ArtiPerson"]!=null && dt.Rows[n]["ArtiPerson"].ToString()!="")
					{
					model.ArtiPerson=dt.Rows[n]["ArtiPerson"].ToString();
					}
					if(dt.Rows[n]["EnteRank"]!=null && dt.Rows[n]["EnteRank"].ToString()!="")
					{
						model.EnteRank=int.Parse(dt.Rows[n]["EnteRank"].ToString());
					}
					if(dt.Rows[n]["EnteClassID"]!=null && dt.Rows[n]["EnteClassID"].ToString()!="")
					{
						model.EnteClassID=int.Parse(dt.Rows[n]["EnteClassID"].ToString());
					}
					if(dt.Rows[n]["CompanyType"]!=null && dt.Rows[n]["CompanyType"].ToString()!="")
					{
						model.CompanyType=int.Parse(dt.Rows[n]["CompanyType"].ToString());
					}
					if(dt.Rows[n]["BusinessLicense"]!=null && dt.Rows[n]["BusinessLicense"].ToString()!="")
					{
					model.BusinessLicense=dt.Rows[n]["BusinessLicense"].ToString();
					}
					if(dt.Rows[n]["TaxNumber"]!=null && dt.Rows[n]["TaxNumber"].ToString()!="")
					{
					model.TaxNumber=dt.Rows[n]["TaxNumber"].ToString();
					}
					if(dt.Rows[n]["AccountBank"]!=null && dt.Rows[n]["AccountBank"].ToString()!="")
					{
					model.AccountBank=dt.Rows[n]["AccountBank"].ToString();
					}
					if(dt.Rows[n]["AccountInfo"]!=null && dt.Rows[n]["AccountInfo"].ToString()!="")
					{
					model.AccountInfo=dt.Rows[n]["AccountInfo"].ToString();
					}
					if(dt.Rows[n]["ServicePhone"]!=null && dt.Rows[n]["ServicePhone"].ToString()!="")
					{
					model.ServicePhone=dt.Rows[n]["ServicePhone"].ToString();
					}
					if(dt.Rows[n]["QQ"]!=null && dt.Rows[n]["QQ"].ToString()!="")
					{
					model.QQ=dt.Rows[n]["QQ"].ToString();
					}
					if(dt.Rows[n]["MSN"]!=null && dt.Rows[n]["MSN"].ToString()!="")
					{
					model.MSN=dt.Rows[n]["MSN"].ToString();
					}
					if(dt.Rows[n]["Status"]!=null && dt.Rows[n]["Status"].ToString()!="")
					{
						model.Status=int.Parse(dt.Rows[n]["Status"].ToString());
					}
					if(dt.Rows[n]["CreatedDate"]!=null && dt.Rows[n]["CreatedDate"].ToString()!="")
					{
						model.CreatedDate=DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
					}
					if(dt.Rows[n]["CreatedUserID"]!=null && dt.Rows[n]["CreatedUserID"].ToString()!="")
					{
						model.CreatedUserID=int.Parse(dt.Rows[n]["CreatedUserID"].ToString());
					}
					if(dt.Rows[n]["UpdatedDate"]!=null && dt.Rows[n]["UpdatedDate"].ToString()!="")
					{
						model.UpdatedDate=DateTime.Parse(dt.Rows[n]["UpdatedDate"].ToString());
					}
					if(dt.Rows[n]["UpdatedUserID"]!=null && dt.Rows[n]["UpdatedUserID"].ToString()!="")
					{
						model.UpdatedUserID=int.Parse(dt.Rows[n]["UpdatedUserID"].ToString());
					}
					if(dt.Rows[n]["AgentID"]!=null && dt.Rows[n]["AgentID"].ToString()!="")
					{
						model.AgentID=int.Parse(dt.Rows[n]["AgentID"].ToString());
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

        #region NewMethod
        /// <summary>
        /// 批量处理状态
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }

        public DataSet GetEnteName(string strEnteName, int iCount)
        {
            string strWhere = "Name like '" + strEnteName + "%' AND Status=1 ";
            return dal.GetList(iCount, strWhere, "Name");
        }

        public List<Model.Ms.Enterprise> GetModelByEnterpriseName(string strEnterpriseName)
        {
            string strWhere = string.Empty;
            if (!string.IsNullOrWhiteSpace(strEnterpriseName))
            {
                strWhere = "Name = '" + strEnterpriseName + "'";
            }
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion
	}
}

