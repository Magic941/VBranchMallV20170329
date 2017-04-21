/**
* ReferUsers.cs
*
* 功 能： N/A
* 类 名： ReferUsers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:51   N/A    初版
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
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using System.Text.RegularExpressions;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 提到某人的记录
	/// </summary>
	public partial class ReferUsers
	{
		private readonly IReferUsers dal=DASNS.CreateReferUsers();
		public ReferUsers()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.ReferUsers model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.ReferUsers model)
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
		public Maticsoft.Model.SNS.ReferUsers GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.ReferUsers GetModelByCache(int ID)
		{
			
			string CacheKey = "ReferUsersModel-" + ID;
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
			return (Maticsoft.Model.SNS.ReferUsers)objModel;
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
		public List<Maticsoft.Model.SNS.ReferUsers> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.ReferUsers> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.ReferUsers> modelList = new List<Maticsoft.Model.SNS.ReferUsers>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.ReferUsers model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = dal.DataRowToModel(dt.Rows[n]);
					if (model != null)
					{
						modelList.Add(model);
					}
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

		#endregion  BasicMethod
		#region ExtensionMethod
		
       /// <summary>
       /// 评论中和动态中提到某人的情况下，应该在提到某人的表中加入相应的数据
       /// </summary>
       /// <param name="Content"></param>
       /// <param name="Type"></param>
       /// <returns></returns>
        public void AddEx(string Content,Maticsoft.Model.SNS.EnumHelper.ReferType Type,int TargetId,string CreateNickName="")
        {
            Maticsoft.Model.SNS.ReferUsers ReferModel = new Model.SNS.ReferUsers();
            Maticsoft.BLL.Members.Users UserBll = new Members.Users();
            int ReferUserID;
            //转发时附加内容里提到某人功能的实现@[^@\s]*(?=[\s:：，,.。]) @"@([^,，：:\s@]+)"
            #region 通过正则匹配提到某人
            if (String.IsNullOrWhiteSpace(Content))
            {
                return;
            }
            MatchCollection matches = Regex.Matches(Content, "@[\\u4e00-\\u9fa5\\w\\-]+");
            List<string> list = new List<string>();
		    if (!string.IsNullOrEmpty(CreateNickName))
		    {
		        list.Add(CreateNickName);
		    }
		    foreach (Match item in matches)
            {

                string NickName = item.Value.Trim('@');
                if (!list.Contains(NickName)&&!string.IsNullOrEmpty(NickName)&&(ReferUserID = UserBll.GetUserIdByNickName(NickName))>0)
                {
                    list.Add(NickName);
                    ReferModel.CreatedDate = DateTime.Now;
                    ReferModel.IsRead = false;
                    ReferModel.ReferUserID = ReferUserID;
                    ReferModel.ReferNickName = NickName;
                    ReferModel.Type = (int)Type;
                    ReferModel.TagetID = TargetId;
                    Add(ReferModel);
                }
            }
           #endregion
            ////下面是给@原动态作者的通知记录
            //ReferModel.CreatedDate = DateTime.Now;
            //ReferModel.IsRead = false;
            //ReferModel.ReferUserID = OrigUserId;
            //ReferModel.ReferNickName = OrigNickName;
            //ReferModel.Type = (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post;
            //ReferModel.TagetID = PostID;
            //ReferBll.Add(ReferModel);
            //return PostID;
        
        }

        /// <summary>
        /// 更新状态为已经知道
        /// </summary>
        /// <param name="UserID"></param>
        public bool UpdateReferStateToRead(int UserID, int Type)
        {
            return dal.UpdateReferStateToRead(UserID,Type);
        
        }
        /// <summary>
        /// 得到未读的个数
        /// </summary>
        public int GetReferNotReadCountByType(int UserId, int Type)
        {
            return dal.GetReferNotReadCountByType(UserId, Type);
        }
	   #endregion
        
	}
}

