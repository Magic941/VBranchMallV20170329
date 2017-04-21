/**
* Star.cs
*
* 功 能： N/A
* 类 名： Star
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:55   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using System.Web;

namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 达人
    /// </summary>
    public partial class Star
    {
        private readonly IStar dal = DASNS.CreateStar();

        public Star()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在重复记录
        /// </summary>
        public bool Exists(int UserID, int TypeID)
        {
            return dal.Exists(UserID, TypeID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Star model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Star model)
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
        public Maticsoft.Model.SNS.Star GetModel(int ID)
        {
            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Star GetModelByCache(int ID)
        {
            string CacheKey = "StarModel-" + ID;
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
            return (Maticsoft.Model.SNS.Star)objModel;
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
        public List<Maticsoft.Model.SNS.Star> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Star> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Star> modelList = new List<Maticsoft.Model.SNS.Star>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Star model;
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

        #endregion BasicMethod

        #region ExtensionMethod

        /// <summary>
        /// 根据类型获得分页的达人
        /// </summary>
        public List<ViewModel.SNS.ViewStar> GetListForPage(int TypeId, string orderby, int startIndex, int endIndex, int CurrentUserId)
        {
            List<ViewModel.SNS.ViewStar> starRankList = new List<ViewModel.SNS.ViewStar>();
            DataSet ds = new DataSet();
            if (TypeId == 0)
            {
                ds = dal.GetListByPage(" Status=1", orderby, startIndex, endIndex);
            }
            else
            {
                ds = dal.GetListByPage("Status=1 and TypeId=" + TypeId, orderby, startIndex, endIndex);
            }

            List<Model.SNS.Star> models = DataTableToList(ds.Tables[0]);
            Maticsoft.BLL.Members.UsersExp userExpBll = new Members.UsersExp();
            ViewModel.SNS.ViewStar star;
            foreach (Model.SNS.Star model in models)
            {
                star = new ViewModel.SNS.ViewStar(model);
                Model.Members.UsersExpModel UsersExpModel = userExpBll.GetUsersExpModel(model.UserID);
                if (UsersExpModel != null)
                {
                    star.FansCount = UsersExpModel.FansCount.Value;
                    star.FavouritesCount = UsersExpModel.FavoritedCount.Value;
                    star.ProductsCount = UsersExpModel.ProductsCount.Value;
                    star.Singature = UsersExpModel.Singature;
                }
                starRankList.Add(star);
                if (CurrentUserId != 0)
                {
                    Maticsoft.BLL.SNS.UserShip bllUserShip = new Maticsoft.BLL.SNS.UserShip();
                    List<Maticsoft.Model.SNS.UserShip> shipList = bllUserShip.GetModelList("ActiveUserID = " + CurrentUserId + "");
                    starRankList.ForEach(item => item.IsFellow = bllUserShip.UserIsFellow(item.UserID, shipList));
                }
            }
            return starRankList;
        }

        /// <summary>
        /// 根据类型获取数量
        /// </summary>
        public int GetCountByType(int TypeId)
        {
            if (TypeId == 0)
            {
                return dal.GetRecordCount(" ");
            }
            else
            {
                return dal.GetRecordCount(" TypeId=" + TypeId);
            }
        }

        public bool UpdateStateList(string IDlist, int status)
        {
            return dal.UpdateStateList(IDlist, status);
        }

        /// <summary>
        /// 新晋达人排行
        /// </summary>
        /// <param name="StarType"></param>
        /// <returns></returns>
        public List<ViewModel.SNS.ViewStar> GetStarNewList(int StarType,int Top=10)
        {
            List<ViewModel.SNS.ViewStar> starRankList = new List<ViewModel.SNS.ViewStar>();
            DataSet ds = new DataSet();
            if (StarType == 0)
            {
                ds = dal.GetList(Top, "   Status=1", " CreatedDate desc");
            }
            else
            {
                ds = dal.GetList(Top, "   Status=1 and TypeID=" + StarType, " CreatedDate desc");
            }
            List<Model.SNS.Star> models = DataTableToList(ds.Tables[0]);
            Maticsoft.BLL.Members.UsersExp userExpBll = new Members.UsersExp();
            ViewModel.SNS.ViewStar starRank;
            foreach (Model.SNS.Star model in models)
            {
                starRank = new ViewModel.SNS.ViewStar(model);
                Model.Members.UsersExpModel UsersExpModel = userExpBll.GetUsersExpModel(model.UserID);
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
        /// 是否是达人
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public bool IsStar(int userId)
        {
            return dal.IsStar(userId);
        }

        /// <summary>
        /// 查询用户申请的达人信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        public DataSet StarName(int userId)
        {
            return dal.StarName(userId);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteListEx(string IDlist)
        {    DataSet ds=new DataSet();
            if( dal.DeleteList(IDlist,out ds))
            {
                List<Maticsoft.Model.SNS.Star> List;
                if (ds != null)
                {
                    List = DataTableToList(ds.Tables[0]);
                    if (List != null)
                    {
                        foreach (Maticsoft.Model.SNS.Star item in List)
                        {
                            if (!string.IsNullOrEmpty(item.UserGravatar))
                            {
                                Common.FileManage.DeleteFile(HttpContext.Current.Server.MapPath(item.UserGravatar));
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        public bool IsExists(int userId, int typeId)
        {
            return dal.IsExists(userId, typeId);
        }
        #endregion ExtensionMethod
    }
}