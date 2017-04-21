/**
* TagType.cs
*
* 功 能： N/A
* 类 名： TagType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:59   N/A    初版
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
using System.Text;
using Maticsoft.Common;
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
namespace Maticsoft.BLL.SNS
{
	/// <summary>
	/// 标签的类型
	/// </summary>
	public partial class TagType
	{
		private readonly ITagType dal=DASNS.CreateTagType();
		public TagType()
		{}
		#region  BasicMethod

		

		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string TypeName)
		{
            return dal.Exists(TypeName);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.SNS.TagType model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.SNS.TagType model)
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
		public Maticsoft.Model.SNS.TagType GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.SNS.TagType GetModelByCache(int ID)
		{
			
			string CacheKey = "TagTypeModel-" + ID;
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
			return (Maticsoft.Model.SNS.TagType)objModel;
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
		public List<Maticsoft.Model.SNS.TagType> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.SNS.TagType> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.SNS.TagType> modelList = new List<Maticsoft.Model.SNS.TagType>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.SNS.TagType model;
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

        #region 逛宝贝和商品类别所对应的tag（含缓存）
        /// <summary>
        /// 根据商品的类别获得相应的tags
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public List<Maticsoft.ViewModel.SNS.CType> GetTagListByCid(int Cid,int Top=0)
        {
            List<Maticsoft.ViewModel.SNS.CType> CtagsList = new List<ViewModel.SNS.CType>();
            Maticsoft.BLL.SNS.Tags TagsBll = new Tags();
            List<Maticsoft.Model.SNS.TagType> TagTypeList = GetModelList(" Cid=" + Cid );
            foreach (Maticsoft.Model.SNS.TagType item in TagTypeList)
            {
                Maticsoft.ViewModel.SNS.CType TagTypeModel = new Maticsoft.ViewModel.SNS.CType();
                TagTypeModel.MTagType = item;
                List<Maticsoft.Model.SNS.Tags> TagsList = new List<Model.SNS.Tags>();
                TagsList = TagsBll.DataTableToList(TagsBll.GetList(Top, "TypeId=" + item.ID + "", "").Tables[0]);
                TagTypeModel.Taglist = TagsList;
                CtagsList.Add(TagTypeModel);
            }
            return CtagsList;
        }


        /// <summary>
        /// 根据商品的类别获得相应的tags（缓存）
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public List<Maticsoft.ViewModel.SNS.CType> GetCacheTagListByCid(int Cid)
        {
            string CacheKey = "CacheTagList-" + Cid;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetTagListByCid(Cid);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.ViewModel.SNS.CType>)objModel;
        }
        
        #endregion

        #region 首页要用到的分类所对应的标签（含缓存）

        /// <summary>
        /// For首页根据商品的类别获得相应的tags
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public List<Maticsoft.ViewModel.SNS.CType> GetTagListByCidEx(int Cid)
        {
            List<Maticsoft.ViewModel.SNS.CType> CtagsList = new List<ViewModel.SNS.CType>();
            Maticsoft.BLL.SNS.Tags TagsBll = new Tags();
            List<Maticsoft.Model.SNS.TagType> TagTypeList = GetModelList(" Cid=" + Cid + "");
            foreach (Maticsoft.Model.SNS.TagType item in TagTypeList)
            {
                Maticsoft.ViewModel.SNS.CType TagTypeModel = new Maticsoft.ViewModel.SNS.CType();
                TagTypeModel.MTagType = item;
                List<Maticsoft.Model.SNS.Tags> TagsList = new List<Model.SNS.Tags>();
                TagsList = TagsBll.DataTableToList(TagsBll.GetListByPage("TypeId=" + item.ID + "", "", 1, 5).Tables[0]);
                TagTypeModel.Taglist = TagsList;
                CtagsList.Add(TagTypeModel);
            }
            return CtagsList;
        }

        /// <summary>
        /// For首页根据商品的类别获得相应的tags（缓存）
        /// </summary>
        /// <param name="Cid"></param>
        /// <returns></returns>
        public List<Maticsoft.ViewModel.SNS.CType> GetCacheTagListByCidEx(int Cid)
        {
            string CacheKey = "CacheTagListEx-" + Cid;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetTagListByCidEx(Cid);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.ViewModel.SNS.CType>)objModel;
        }
        
        #endregion

        #endregion
        #region 扩展方法
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllListEX()
        {
            return dal.GetAllListEX();
        }
        public bool RelationSNSCate(int tagTypeId, int SNSCategoryId)
        {
            return dal.RelationSNSCate(tagTypeId, SNSCategoryId);
        }

	    /// <summary>
	    /// 获得数据列表
	    /// </summary>
	    public DataSet GetSearchList(string Keywords, int Cid = -1)
	    {
	        var sb = new StringBuilder();
	        if (!string.IsNullOrEmpty(Keywords))
	        {
	            sb.Append(" TypeName like '" + Keywords + "'");
	        }
	        if (Cid >= 0)
	        {
	            if (sb.Length > 0)
	            {
	                sb.Append(" and ");
	            }
	            sb.Append("  Cid>=0");
	        }

	        return dal.GetList(0, sb.ToString(), "");
	    }


        public  int GetTagsTypeId(string TagName)
        {

            List<Maticsoft.Model.SNS.TagType> list =GetModelList("TypeName='" + TagName + "'");
            if (list != null && list.Count > 0)
            {
                return list[0].ID;
            }
            Maticsoft.Model.SNS.TagType model = new Model.SNS.TagType();
            model.TypeName = TagName;
            model.Cid = -1;
            return Add(model);

        }

        #endregion
    }
}

