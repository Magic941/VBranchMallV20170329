/**
* StarRank.cs
*
* 功 能： N/A
* 类 名： StarRank
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:56   N/A    初版
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
	/// 达人排行
	/// </summary>
	public partial class StarRank
	{
		private readonly IStarRank dal=DASNS.CreateStarRank();
		public StarRank()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        //public int GetMaxId()
        //{
        //    return dal.GetMaxId();
        //}

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
        public int Add(Maticsoft.Model.SNS.StarRank model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.StarRank model)
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
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.StarRank GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.StarRank GetModelByCache(int ID)
        {

            string CacheKey = "StarRankModel-" + ID;
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
                catch { }
            }
            return (Maticsoft.Model.SNS.StarRank)objModel;
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.StarRank> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.StarRank> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.StarRank> modelList = new List<Maticsoft.Model.SNS.StarRank>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.StarRank model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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

        public List<ViewModel.SNS.StarRank> HotStarList(int top=4)
        {
            List<ViewModel.SNS.StarRank> starRankList = new List<ViewModel.SNS.StarRank>();
            DataSet ds= dal.GetList(top, " IsRecommend='true'", " Sequence");//明星达人取四条数据
            List<Model.SNS.StarRank> models = DataTableToList(ds.Tables[0]);
            Maticsoft.BLL.Members.UsersExp userExpBll = new Members.UsersExp();
            ViewModel.SNS.StarRank starRank;
            foreach (Model.SNS.StarRank model in models)
            {
                starRank = new ViewModel.SNS.StarRank(model);
                Model.Members.UsersExpModel UsersExpModel = userExpBll.GetUsersExpModel(model.UserId);
                if (UsersExpModel != null)
                {
                    starRank.FansCount = UsersExpModel.FansCount.Value;
                    starRank.FavouritesCount = UsersExpModel.FavoritedCount.Value;
                    starRank.ProductsCount = UsersExpModel.ProductsCount.Value;
                    starRank.Singature = UsersExpModel.Singature;
                    starRank.IsFellow = UsersExpModel.IsFellow;
                }
                starRankList.Add(starRank);
            }
            return starRankList;
        }
        ///获取类型下的新晋达人
        public List<ViewModel.SNS.StarRank> GetStarNewList(int StarType)
        {
            List<ViewModel.SNS.StarRank> starRankList = new List<ViewModel.SNS.StarRank>();
             DataSet ds =new DataSet();
            if (StarType == 0)
            {
               ds= dal.GetList(10, " RankType=1 and  Status=1", " Sequence");
            }
            else
            {
                ds = dal.GetList(10, "  RankType=1 and  Status=1 and TypeID=" + StarType, " Sequence");
            }
            List<Model.SNS.StarRank> models = DataTableToList(ds.Tables[0]);
            Maticsoft.BLL.Members.UsersExp userExpBll = new Members.UsersExp();
            ViewModel.SNS.StarRank starRank;
            foreach (Model.SNS.StarRank model in models)
            {
                starRank = new ViewModel.SNS.StarRank(model);
                Model.Members.UsersExpModel UsersExpModel = userExpBll.GetUsersExpModel(model.UserId);
                if (UsersExpModel != null)
                {
                    starRank.FansCount = UsersExpModel.FansCount.Value;
                    starRank.FavouritesCount = UsersExpModel.FavoritedCount.Value;
                    starRank.ProductsCount = UsersExpModel.ProductsCount.Value;
                    starRank.Singature = UsersExpModel.Singature;
                    starRank.IsFellow = UsersExpModel.IsFellow;
                }
                starRankList.Add(starRank);
            }
            return starRankList;
        }
        ///获取类型下的达人总排行
        public List<ViewModel.SNS.StarRank> GetStarRankList(int StarType,int top=10)
        {
            List<ViewModel.SNS.StarRank> starRankList = new List<ViewModel.SNS.StarRank>();
            DataSet ds = new DataSet();
            if (StarType == 0)
            {
                ds = dal.GetList(top, " RankType=0 and  Status=1", " Sequence");
            }
            else
            {
                ds = dal.GetList(top, "  RankType=0 and  Status=1 and TypeID=" + StarType, " Sequence");
            }
            List<Model.SNS.StarRank> models = DataTableToList(ds.Tables[0]);
            Maticsoft.BLL.Members.UsersExp userExpBll = new Members.UsersExp();
            ViewModel.SNS.StarRank starRank;
            foreach (Model.SNS.StarRank model in models)
            {
                starRank = new ViewModel.SNS.StarRank(model);
                Model.Members.UsersExpModel UsersExpModel = userExpBll.GetUsersExpModel(model.UserId);
                if (UsersExpModel != null)
                {
                    starRank.FansCount = UsersExpModel.FansCount.Value;
                    starRank.FavouritesCount = UsersExpModel.FavoritedCount.Value;
                    starRank.ProductsCount = UsersExpModel.ProductsCount.Value;
                    starRank.Singature = UsersExpModel.Singature;
                    starRank.IsFellow = UsersExpModel.IsFellow;
                }
                starRankList.Add(starRank);
            }
            return starRankList;
        }
        /// <summary>
        /// 添加晒货达人排行
        /// </summary>
        /// <returns></returns>
        public bool AddShareProductRank()
        {
            return dal.AddShareProductRank();
        }

        /// <summary>
        /// 添加明细达人排行
        /// </summary>
        /// <returns></returns>
        public bool AddHotStarRank()
        {
            return dal.AddHotStarRank();
        }

        /// <summary>
        /// 添加搭配达人排行
        /// </summary>
        /// <returns></returns>
        public bool AddCollocationRank()
        {
            return dal.AddCollocationRank();
        }

        /// <summary>
        /// 批量审核通过
        /// </summary>
        /// <param name="IDlist"></param>
        /// <returns></returns>
        public bool UpdateStateList(string IDlist)
        {
            return dal.UpdateStateList(IDlist);
        }

		#endregion  ExtensionMethod
	}
}

