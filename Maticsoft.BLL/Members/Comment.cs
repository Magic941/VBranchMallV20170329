using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Comment;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Comment;
namespace Maticsoft.BLL.Comment
{
	/// <summary>
	/// Comment
	/// </summary>
	public partial class Comment
	{
        private readonly IComment dal = DAMembers.CreateComment();
		public Comment()
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
		public int  Add(Maticsoft.Model.Comment.Comment model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Comment.Comment model)
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
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Comment.Comment GetModel(int ID)
		{
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Comment.Comment GetModelByCache(int ID)
		{
			
			string CacheKey = "CommentModel-" + ID;
			object objModel = DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Model.Comment.Comment)objModel;
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
		public List<Maticsoft.Model.Comment.Comment> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Comment.Comment> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Comment.Comment> modelList = new List<Maticsoft.Model.Comment.Comment>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Comment.Comment model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Comment.Comment();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["ContentId"]!=null && dt.Rows[n]["ContentId"].ToString()!="")
					{
						model.ContentId=int.Parse(dt.Rows[n]["ContentId"].ToString());
					}
					if(dt.Rows[n]["Description"]!=null && dt.Rows[n]["Description"].ToString()!="")
					{
					model.Description=dt.Rows[n]["Description"].ToString();
					}
					if(dt.Rows[n]["CreatedDate"]!=null && dt.Rows[n]["CreatedDate"].ToString()!="")
					{
						model.CreatedDate=DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
					}
					if(dt.Rows[n]["CreatedUserID"]!=null && dt.Rows[n]["CreatedUserID"].ToString()!="")
					{
						model.CreatedUserID=int.Parse(dt.Rows[n]["CreatedUserID"].ToString());
					}
					if(dt.Rows[n]["ReplyCount"]!=null && dt.Rows[n]["ReplyCount"].ToString()!="")
					{
						model.ReplyCount=int.Parse(dt.Rows[n]["ReplyCount"].ToString());
					}
					if(dt.Rows[n]["ParentID"]!=null && dt.Rows[n]["ParentID"].ToString()!="")
					{
						model.ParentID=int.Parse(dt.Rows[n]["ParentID"].ToString());
					}
					if(dt.Rows[n]["TypeID"]!=null && dt.Rows[n]["TypeID"].ToString()!="")
					{
						model.TypeID=int.Parse(dt.Rows[n]["TypeID"].ToString());
					}
					if(dt.Rows[n]["State"]!=null && dt.Rows[n]["State"].ToString()!="")
					{
						if((dt.Rows[n]["State"].ToString()=="1")||(dt.Rows[n]["State"].ToString().ToLower()=="true"))
						{
						model.State=true;
						}
						else
						{
							model.State=false;
						}
					}
					if(dt.Rows[n]["IsRead"]!=null && dt.Rows[n]["IsRead"].ToString()!="")
					{
						if((dt.Rows[n]["IsRead"].ToString()=="1")||(dt.Rows[n]["IsRead"].ToString().ToLower()=="true"))
						{
						model.IsRead=true;
						}
						else
						{
							model.IsRead=false;
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
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}


        public bool UpdateStates(string strList, bool bResult)
		{
		    return dal.UpdateStates(strList, bResult);
		}

		#endregion  Method
	}
}

