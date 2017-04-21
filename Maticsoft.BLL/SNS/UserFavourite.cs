/**
* UserFavourite.cs
*
* 功 能： N/A
* 类 名： UserFavourite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:04   N/A    初版
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
using System.Text;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 用户的喜欢
	/// </summary>
	public partial class UserFavourite
	{
		private readonly IUserFavourite dal=DASNS.CreateUserFavourite();
        private readonly Comments commentBll = new Comments();
		public UserFavourite()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(int CreatedUserID, int Type, int TargetID)
		{
			return dal.Exists(CreatedUserID, Type, TargetID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.UserFavourite model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.UserFavourite model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int FavouriteID)
		{
			
			return dal.Delete(FavouriteID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string FavouriteIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(FavouriteIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.UserFavourite GetModel(int FavouriteID)
		{
			
			return dal.GetModel(FavouriteID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.UserFavourite GetModelByCache(int FavouriteID)
		{
			
			string CacheKey = "UserFavouriteModel-" + FavouriteID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(FavouriteID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.UserFavourite)objModel;
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
		public List<Maticsoft.Model.SNS.UserFavourite> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.UserFavourite> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.UserFavourite> modelList = new List<Maticsoft.Model.SNS.UserFavourite>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.UserFavourite model;
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
		#region  ExtensionMethod
        public bool AddEx(Maticsoft.Model.SNS.UserFavourite FavModel,int TopicId, int ReplyId)
        {
            return dal.AddEx(FavModel,TopicId,ReplyId);
        
        }

        #region 根据用户得到其相应的喜欢 分页
        /// <summary>
        ///根据用户得到其相应的喜欢
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<PostContent> GetFavListByPage(int UserId, string orderby, int startIndex, int endIndex)
        {
            List<PostContent> targetList = new List<PostContent>();
            List<Maticsoft.Model.SNS.Comments> comList = new List<Model.SNS.Comments>();
            DataSet ds = dal.GetFavListByPage(UserId, "", startIndex, endIndex);
            List<string> commonIds = new List<string>();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    PostContent model = new PostContent();
                    model.TargetId = Convert.ToInt32(dr["TargetID"]);
                    model.TargetName = dr["TargetName"].ToString();
                    model.TargetDescription = dr["Description"] != null ? dr["Description"].ToString() : "";
                    model.CommentCount = Convert.ToInt32(dr["CommentCount"]);
                    model.FavouriteCount = Convert.ToInt32(dr["FavouriteCount"]);
                    model.ThumbImageUrl = dr["ThumbImageUrl"].ToString();
                    model.Price = Convert.ToDecimal(dr["Price"]);
                    model.Type =((int)dr["Type"]==0?(int)Maticsoft.Model.SNS.EnumHelper.FavoriteType.Photo:(int)Maticsoft.Model.SNS.EnumHelper.FavoriteType.Product);
                    model.TopCommentsId = dr["TopCommentsId"] != null ? dr["TopCommentsId"].ToString() : "";
                    if (!string.IsNullOrEmpty(model.TopCommentsId))
                    {
                        commonIds.Add(model.TopCommentsId);
                    } 
                    targetList.Add(model);
                  
                }
                //获取评论数据
                List<Model.SNS.Comments> commentList = commentBll.GetCommentByIds(string.Join(",", commonIds).TrimEnd(','), 1);
                if (commentList != null)
                {
                    //加载评论数据
                    targetList.ForEach(img =>
                    {
                        List<Model.SNS.Comments> List = commentList.FindAll(xx => (xx.TargetId == img.TargetId && xx.Type ==(img.Type==(int)Maticsoft.Model.SNS.EnumHelper.FavoriteType.Photo ? (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo : (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product)));
                        if (List == null) return;
                        img.CommentList = List;
                    });
                }
            }
            return targetList;
        } 
        #endregion

        #region 删除喜欢
        /// <summary>
        /// 删除喜欢
        /// </summary>
        /// <param name="TargetId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool DeleteEx(int UserId,int TargetId, int Type)
        {

            return dal.DeleteEx(UserId, TargetId, Type);

        } 
        #endregion

        /// <summary>
        ///得到商品或图片被喜欢的数量
        /// </summary>
        /// <param name="TagetId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public int GetFavCountByTargetId(int TargetId, int Type)
        {
            return GetRecordCount("TargetID=" + TargetId + " and Type=" + Type + ""); ;
        }

        /// <summary>
        ///得到商品或图片被喜欢人员的信息
        /// </summary>
        /// <param name="TagetId"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserFavourite> GetFavUserByTargetId(int TargetId, int Type,int Top)
        {
            return DataTableToList(GetListByPage("TargetID=" + TargetId + " and Type=" + Type + "", "", 1, Top).Tables[0]);
        }
		#endregion  ExtensionMethod
	}
}

