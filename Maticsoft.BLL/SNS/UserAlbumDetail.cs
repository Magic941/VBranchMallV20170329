/**
* UserAlbumDetail.cs
*
* 功 能： N/A
* 类 名： UserAlbumDetail
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:00   N/A    初版
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
    /// 用户专辑详情
    /// </summary>
    public partial class UserAlbumDetail
    {
        private readonly IUserAlbumDetail dal = DASNS.CreateUserAlbumDetail();
        public UserAlbumDetail()
        { }

        #region  BasicMethod



        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AlbumID, int TargetID, int Type)
        {
            return dal.Exists(AlbumID, TargetID, Type);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.UserAlbumDetail model)
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
        public bool Delete(int AlbumID, int TargetID, int Type)
        {

            return dal.Delete(AlbumID, TargetID, Type);
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
        public Maticsoft.Model.SNS.UserAlbumDetail GetModel(int ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.UserAlbumDetail GetModelByCache(int ID)
        {
            string CacheKey = "UserAlbumDetailModel-" + ID;
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
            return (Maticsoft.Model.SNS.UserAlbumDetail)objModel;
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
        public List<Maticsoft.Model.SNS.UserAlbumDetail> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.UserAlbumDetail> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.UserAlbumDetail> modelList = new List<Maticsoft.Model.SNS.UserAlbumDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.UserAlbumDetail model;
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

        /// <summary>
        /// 获得专辑的推荐产品照片
        /// </summary>
        public List<string> GetThumbImageByAlbum(int AlbumID,int type=-1)
        {
            return dal.GetThumbImageByAlbum(AlbumID,type);
        }

        ///// <summary>
        ///// 获得专辑下的图片数据列表
        ///// </summary>
        //public List<ViewModel.SNS.AlbumImages> GetRecordCountByAlbumID(int albumID)
        //{
        //    List<ViewModel.SNS.AlbumImages> imgList = new List<ViewModel.SNS.AlbumImages>();
        //    DataSet ds = dal.GetImgList(AlbumID);
        //    if (ds != null && ds.Tables.Count > 0)
        //    {
        //        ViewModel.SNS.AlbumImages model = new ViewModel.SNS.AlbumImages();
        //        DataTable dt = ds.Tables[0];
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            model.TargetID = Convert.ToInt32(dr["TargetID"]);
        //            model.TargetName = dr["TargetName"].ToString();
        //            model.CommentCount = Convert.ToInt32(dr["TargetID"]);
        //            model.FavouriteCount = Convert.ToInt32(dr["TargetID"]);
        //            model.ThumbImageUrl = dr["ThumbImageUrl"].ToString();
        //            model.Price = Convert.ToDecimal(dr["Price"]);
        //            imgList.Add(model);
        //        }
        //    }
        //    return imgList;
        //}

        /// <summary>
        /// 获得专辑下的图片记录总数 -1表示全部 0:表示只获取图片 1：只获取商品
        /// </summary>
        public int GetRecordCount4AlbumImgByAlbumID(int albumID,int type=-1 )
        {
            return dal.GetRecordCount4AlbumImgByAlbumID(albumID, type);
        }

        /// <summary>
        /// 获得专辑下的图片数据列表 -1表示全部 0:表示只获取图片 1：只获取商品
        /// </summary>
        public List<PostContent> GetAlbumImgListByPage(int albumID, int startIndex, int endIndex,int type=-1)
        {
            List<PostContent> imgList = new List<PostContent>();
            DataSet ds = dal.GetAlbumImgListByPage(albumID, "", startIndex, endIndex, type);
            List<string> commonIds = new List<string>();
            Maticsoft.BLL.SNS.Comments commentBll = new Comments();
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
                    model.Price = Maticsoft.Common.Globals.SafeDecimal(dr["Price"].ToString(),-1);
                    model.Type = (int)dr["Type"];
                    model.TopCommentsId =dr["TopCommentsId"] != null ? dr["TopCommentsId"].ToString() : "";
                    imgList.Add(model);
                    if (!string.IsNullOrEmpty(model.TopCommentsId))
                    {
                        commonIds.Add(model.TopCommentsId);
                    } 
                }
                //获取评论数据
                List<Model.SNS.Comments> commentList = commentBll.GetCommentByIds(string.Join(",", commonIds).TrimEnd(','), 1);
                if (commentList != null)
                {
                    //加载评论数据
                    imgList.ForEach(img =>
                    {
                        List<Model.SNS.Comments> List = commentList.FindAll(xx => (xx.TargetId == img.TargetId && xx.Type == (img.Type == (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo ? (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo : (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product)));
                        if (List == null) return;
                        img.CommentList = List;
                    });
                }
            }
            return imgList;
        }
        /// <summary>
        /// 增加一条数据，给相应图片的数量增加一
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddEx(Maticsoft.Model.SNS.UserAlbumDetail model)
        {
            return dal.AddEx(model);
        }
        /// <summary>
        /// 删除专辑中的图片
        /// </summary>
        /// <param name="AlbumID"></param>
        /// <param name="TargetId"></param>
        /// <param name="Type"></param>
        public bool DeleteEx(int AlbumID, int TargetId, int Type)
        {
        
         return dal.DeleteEx(AlbumID,TargetId,Type);
        }
        #endregion  ExtensionMethod
    }
}

