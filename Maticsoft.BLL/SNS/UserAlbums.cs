/**
* UserAlbums.cs
*
* 功 能： N/A
* 类 名： UserAlbums
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:01   N/A    初版
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

namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 用户专辑
    /// </summary>
    public partial class UserAlbums
    {
        private readonly IUserAlbums dal = DASNS.CreateUserAlbums();

        public UserAlbums()
        { }

        #region BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CreatedUserID, string AlbumName)
        {
            return dal.Exists(CreatedUserID, AlbumName);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.UserAlbums model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.UserAlbums model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AlbumID)
        {
            return dal.Delete(AlbumID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AlbumIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(AlbumIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.UserAlbums GetModel(int AlbumID)
        {
            return dal.GetModel(AlbumID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.UserAlbums GetModelByCache(int AlbumID)
        {
            string CacheKey = "UserAlbumsModel-" + AlbumID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AlbumID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.UserAlbums)objModel;
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
        public List<Maticsoft.Model.SNS.UserAlbums> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.UserAlbums> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserAlbums> modelList = new List<Maticsoft.Model.SNS.UserAlbums>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.UserAlbums model;
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

        #endregion BasicMethod

        #region ExtensionMethod

        /// <summary>
        /// 根据类型获得首页推荐的专辑
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public List<ViewModel.SNS.AlbumIndex> GetListForIndex(int TypeID,int Top, int RecommandType=-1,int type=-1)
        {
            List<ViewModel.SNS.AlbumIndex> albumlist = new List<ViewModel.SNS.AlbumIndex>();
            DataSet ds = dal.GetListForIndex(TypeID, Top, "FavouriteCount desc",RecommandType);

            List<Model.SNS.UserAlbums> models = DataTableToList(ds.Tables[0]);
            UserAlbumDetail bllUA = new UserAlbumDetail();
            ViewModel.SNS.AlbumIndex album;
            foreach (Model.SNS.UserAlbums model in models)
            {
                album = new ViewModel.SNS.AlbumIndex(model);
                album.TopImages = bllUA.GetThumbImageByAlbum(model.AlbumID,type);
                albumlist.Add(album);
            }
            return albumlist;
        }

        /// <summary>
        /// 根据类型获得首页推荐的专辑（少于9个不能推荐）此代码后期和上面的代码合并
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public List<ViewModel.SNS.AlbumIndex> GetListForIndexEx(int TypeID,int type=-1)
        {
            List<ViewModel.SNS.AlbumIndex> albumlist = new List<ViewModel.SNS.AlbumIndex>();
            DataSet ds = dal.GetListForIndexEx(TypeID, 8, "FavouriteCount desc");

            List<Model.SNS.UserAlbums> models = DataTableToList(ds.Tables[0]);
            UserAlbumDetail bllUA = new UserAlbumDetail();
            ViewModel.SNS.AlbumIndex album;
            foreach (Model.SNS.UserAlbums model in models)
            {
                album = new ViewModel.SNS.AlbumIndex(model);
                album.TopImages = bllUA.GetThumbImageByAlbum(model.AlbumID,type);
                albumlist.Add(album);
            }
            return albumlist;
        }

        /// <summary>
        /// 获取用户的专辑
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns></returns>
        public List<ViewModel.SNS.AlbumIndex> GetListByUserId(int UserId,int type=-1)
        {
            List<ViewModel.SNS.AlbumIndex> albumlist = new List<ViewModel.SNS.AlbumIndex>();

            List<Model.SNS.UserAlbums> models = GetModelList("CreatedUserID=" + UserId + "");
            UserAlbumDetail bllUA = new UserAlbumDetail();
            ViewModel.SNS.AlbumIndex album;
            foreach (Model.SNS.UserAlbums model in models)
            {
                album = new ViewModel.SNS.AlbumIndex(model);
                album.TopImages = bllUA.GetThumbImageByAlbum(model.AlbumID,type);
                albumlist.Add(album);
            }
            return albumlist;
        }

        /// <summary>
        /// 根据类型获得分页的专辑
        /// </summary>
        public int GetRecordCount(int TypeID)
        {
            return dal.GetRecordCount(TypeID);
        }

        /// <summary>
        /// 根据类型获得分页的专辑
        /// </summary>
        public int GetCountByKeyWard(string KeyWord)
        {
            string sql = "";
            if (!string.IsNullOrEmpty(KeyWord))
            {
                sql = "AlbumName like '%" + KeyWord + "%'";
            }
            return dal.GetRecordCount(sql);
        }

        /// <summary>
        /// 根据类型获得分页的专辑 type  -1表示全部 0:表示只获取图片 1：只获取商品
        /// </summary>
        public List<ViewModel.SNS.AlbumIndex> GetListForPage(int TypeID, string orderby, int startIndex, int endIndex,int type=-1)
        {
            List<ViewModel.SNS.AlbumIndex> albumlist = new List<ViewModel.SNS.AlbumIndex>();
            DataSet ds = dal.GetListForPage(TypeID, orderby, startIndex, endIndex);

            List<Model.SNS.UserAlbums> models = DataTableToList(ds.Tables[0]);
            UserAlbumDetail bllUA = new UserAlbumDetail();
            ViewModel.SNS.AlbumIndex album;
            foreach (Model.SNS.UserAlbums model in models)
            {
                album = new ViewModel.SNS.AlbumIndex(model);
                album.TopImages = bllUA.GetThumbImageByAlbum(model.AlbumID, type);
                albumlist.Add(album);
            }
            return albumlist;
        }

        /// <summary>
        /// 根据类型获得分页的专辑（少于9图片不提取）  后期需合并和优化
        /// </summary>
        public List<ViewModel.SNS.AlbumIndex> GetListForPageEx(int TypeID, string orderby, int startIndex, int endIndex,int type=-1)
        {
            List<ViewModel.SNS.AlbumIndex> albumlist = new List<ViewModel.SNS.AlbumIndex>();
            DataSet ds = dal.GetListForPageEx(TypeID, orderby, startIndex, endIndex);
            List<Model.SNS.UserAlbums> models = DataTableToList(ds.Tables[0]);
            UserAlbumDetail bllUA = new UserAlbumDetail();
            ViewModel.SNS.AlbumIndex album;
            foreach (Model.SNS.UserAlbums model in models)
            {
                album = new ViewModel.SNS.AlbumIndex(model);
                album.TopImages = bllUA.GetThumbImageByAlbum(model.AlbumID,type);
                albumlist.Add(album);
            }
            return albumlist;
        }

        /// <summary>
        ///根据关键子搜索
        /// </summary>
        public List<ViewModel.SNS.AlbumIndex> GetListByKeyWord(string KeyWord, string orderby, int startIndex, int endIndex,int type=-1)
        {
            List<ViewModel.SNS.AlbumIndex> albumlist = new List<ViewModel.SNS.AlbumIndex>();
            string sql = "";
            if (!string.IsNullOrEmpty(KeyWord))
            {
                sql = "AlbumName like '%" + KeyWord + "%'";
            }

            switch (orderby)
            {
                case "popular":
                    orderby = "FavouriteCount";
                    break;

                case "new":
                    orderby = "CreatedDate";
                    break;

                case "hot":
                    orderby = "CommentsCount";
                    break;
                default:
                    orderby = "FavouriteCount";
                    break;
            }
            DataSet ds = dal.GetListByPage(sql, orderby, startIndex, endIndex);
            List<Model.SNS.UserAlbums> models = DataTableToList(ds.Tables[0]);
            UserAlbumDetail bllUA = new UserAlbumDetail();
            ViewModel.SNS.AlbumIndex album;
            foreach (Model.SNS.UserAlbums model in models)
            {
                album = new ViewModel.SNS.AlbumIndex(model);
                album.TopImages = bllUA.GetThumbImageByAlbum(model.AlbumID,type);
                albumlist.Add(album);
            }
            return albumlist;
        }

        /// <summary>
        /// 获取用户收藏的专辑
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<ViewModel.SNS.AlbumIndex> GetUserFavAlbum(int UserId,int type=-1)
        {
            List<ViewModel.SNS.AlbumIndex> albumlist = new List<ViewModel.SNS.AlbumIndex>();
            DataSet ds = dal.GetUserFavAlbum(UserId);
            List<Model.SNS.UserAlbums> models = DataTableToList(ds.Tables[0]);
            UserAlbumDetail bllUA = new UserAlbumDetail();
            ViewModel.SNS.AlbumIndex album;
            foreach (Model.SNS.UserAlbums model in models)
            {
                album = new ViewModel.SNS.AlbumIndex(model);
                album.TopImages = bllUA.GetThumbImageByAlbum(model.AlbumID,type);
                albumlist.Add(album);
            }
            return albumlist;
        }

        /// <summary>
        /// 或的某个用户的专辑
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.UserAlbums> GetUserAblumsByUserID(int UserID)
        {
            return GetModelList("CreatedUserID=" + UserID + "");
        }

        /// 或的top n 某个用户的专辑
        public List<Maticsoft.Model.SNS.UserAlbums> GetUserAlbumsByUserId(int top, int UserID)
        {
            return DataTableToList(dal.GetListByPage("CreatedUserID=" + UserID + "", "PhotoCount desc", 0, 9).Tables[0]);
        }

        /// <summary>
        /// 根据用户专辑中的一张图片获得其专辑的相应信息
        /// </summary>
        /// <param name="type">图片的类型</param>
        /// <param name="pid">id</param>
        /// <param name="UserId">用户id</param>
        /// <returns></returns>
        public Maticsoft.Model.SNS.UserAlbums GetUserAlbum(int type, int pid, int UserId)
        {
            return dal.GetUserAlbum(type, pid, UserId);
        }

        /// <summary>
        /// 更新部分数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateEx(Maticsoft.Model.SNS.UserAlbums model)
        {
            return dal.UpdateEx(model);
        }

        /// <summary>
        /// 删除一条数据,包括专辑下面的商品或者图片都级联删除掉
        /// </summary>
        public bool DeleteEx(int AlbumID, int UserId)
        {
            Maticsoft.BLL.SNS.UserAlbumsType utBll = new UserAlbumsType();

            //暂时的逻辑是每个专辑只有一个类型，表的设计上是一个专辑可以支持多个类型
            List<Maticsoft.Model.SNS.UserAlbumsType> list = utBll.GetModelList("AlbumsID=" + AlbumID + " and AlbumsUserID=" + UserId + "");
            if (list.Count > 0)
            {
                return dal.DeleteEx(AlbumID, list[0].TypeID, list[0].AlbumsUserID.Value);
            }
            else
            {
                return dal.DeleteEx(AlbumID, 0, UserId);
            }
        }

        /// <summary>
        /// 增加专辑
        /// </summary>
        /// <param name="model"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public int AddEx(Maticsoft.Model.SNS.UserAlbums model, int TypeId)
        {
            return dal.AddEx(model, TypeId);
        }

        //
        public bool UpdatePhotoCount()
        {
            return dal.UpdatePhotoCount();
        }

        /// <summary>
        /// 更改pvcount
        /// </summary>
        /// <returns></returns>
        public bool UpdatePvCount(int AlbumId)
        {
            return dal.UpdatePvCount(AlbumId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSearchList(string Keywords)
        {
            return dal.GetList(0, string.Format(" AlbumName like '%{0}%'", Keywords), "");
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetUserAblumSearchList(string Keywords)
        {
            return dal.GetList(0, Keywords, "");
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool UpdateIsRecommand(int IsRecommand, string IdList)
        {
            return dal.UpdateIsRecommand(IsRecommand, IdList);
        }

        /// <summary>
        /// 根据专辑ID删除专辑信息
        /// </summary>
        /// <param name="albumId">专辑ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteAblumAction(int albumId)
        {
            return dal.DeleteAblumAction(albumId);
        }

        /// <summary>
        /// 更新专辑推荐状态
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <param name="Recommand">推荐状态</param>
        /// <returns>执行结果 True OR False</returns>
        public bool UpdateRecommand(int ablumId, Model.SNS.EnumHelper.RecommendType recommendType)
        {
            return dal.UpdateRecommand(ablumId, recommendType);
        }
        /// <summary>
        /// 更新专辑评论的数量
        /// </summary>
        /// <param name="ablumId">专辑ID</param>

        public bool UpdateCommentCount(int ablumId)
        {
            return dal.UpdateCommentCount(ablumId);
        }

      
        #endregion ExtensionMethod
    }
}