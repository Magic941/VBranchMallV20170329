/**
* GroupTopicReply.cs
*
* 功 能： N/A
* 类 名： GroupTopicReply
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:43   N/A    初版
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
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 主题回复
	/// </summary>
	public partial class GroupTopicReply
	{
		private readonly IGroupTopicReply dal=DASNS.CreateGroupTopicReply();
		public GroupTopicReply()
		{}
		#region  BasicMethod

		


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.GroupTopicReply model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.GroupTopicReply model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ReplyID)
		{
			
			return dal.Delete(ReplyID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string ReplyIDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(ReplyIDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.SNS.GroupTopicReply GetModel(int ReplyID)
		{
			
			return dal.GetModel(ReplyID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.GroupTopicReply GetModelByCache(int ReplyID)
		{
			
			string CacheKey = "GroupTopicReplyModel-" + ReplyID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ReplyID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.SNS.GroupTopicReply)objModel;
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
		public List<Maticsoft.Model.SNS.GroupTopicReply> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.GroupTopicReply> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.GroupTopicReply> modelList = new List<Maticsoft.Model.SNS.GroupTopicReply>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.GroupTopicReply model;
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

        public List<Maticsoft.Model.SNS.GroupTopicReply> GetTopicReplyByTopic(int TopicId, int StartIndex, int EndIndex)
        {

            return DataTableToList(GetListByPage(" Status=1 and TopicID =" + TopicId + "", "CreatedDate Desc", StartIndex, EndIndex).Tables[0]);
        
        }

        public int AddEx(Maticsoft.Model.SNS.GroupTopicReply TModel, long Pid)
        {
            #region 如果帖子中有商品，则提取商品的信息
            Maticsoft.Model.SNS.Products PModel = new Model.SNS.Products();
            Maticsoft.BLL.SNS.Products PBll = new Products();
            if (Pid > 0)
            {
                PModel.ProductID = Pid;
                PModel.CreateUserID = TModel.ReplyUserID;
                PModel.CreatedNickName = TModel.ReplyNickName;
                PModel.CreatedDate = DateTime.Now;
                PModel = PBll.GetProductModel(PModel);
            }
            #endregion
            //进行敏感字过滤
            if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(TModel.Description))
            {
                PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
                TModel.Status = 0;
            }
            else
            {
                TModel.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(TModel.Description);
            }

            return dal.AddEx(TModel, PModel);
        }


        public int ForwardReply(int ReplyId, string Des,int CurrentUserId,string NickName)
        {
            Maticsoft.Model.SNS.GroupTopicReply OriginModel = GetModel(ReplyId);
            Maticsoft.Model.SNS.GroupTopicReply NewModel = new Model.SNS.GroupTopicReply();
            NewModel.OriginalID = ReplyId;
            NewModel.Description = Des;
            NewModel.OrginalDes = OriginModel.Description;
            NewModel.CreatedDate = DateTime.Now;
            NewModel.GroupID = OriginModel.GroupID;
            NewModel.OrgianlNickName = OriginModel.ReplyNickName;
            NewModel.ReplyUserID = CurrentUserId;
            NewModel.TopicID = OriginModel.TopicID;
            NewModel.PhotoUrl =OriginModel.OriginalID>0?"": OriginModel.PhotoUrl;
            NewModel.ProductLinkUrl = OriginModel.OriginalID > 0 ? "" : OriginModel.ProductLinkUrl;
            NewModel.ProductName = OriginModel.OriginalID > 0 ? "" : OriginModel.ProductName;
            NewModel.ProductUrl = OriginModel.OriginalID > 0 ? "" : OriginModel.ProductUrl;
            NewModel.Price = OriginModel.OriginalID > 0 ? 0 : OriginModel.Price;
            NewModel.TargetId = OriginModel.TargetId;
            NewModel.Type = OriginModel.Type;
            NewModel.ReplyNickName = NickName;
            NewModel.Type = -1;
            int ResultId = dal.ForwardReply(NewModel);
            return ResultId;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere)
        {
            return dal.GetListEx(strWhere);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteListEx(string TopicIDlist)
        {
            return dal.DeleteListEx(TopicIDlist);
        }

        /// <summary>
        /// 批量审核
        /// </summary>
         public  bool UpdateStatusList(string IdsStr, Maticsoft.Model.SNS.EnumHelper.TopicStatus status)
        {

            return dal.UpdateStatusList(IdsStr,status);
        
        }


         /// <summary>
         /// 删除一条数据
         /// </summary>
         public bool DeleteEx(int ReplyID)
         {

             return dal.DeleteEx(ReplyID);
         }
		#endregion  ExtensionMethod
	}
}

