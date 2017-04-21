/**
* Comments.cs
*
* 功 能： N/A
* 类 名： Comments
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:41   N/A    初版
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
    /// 用户评论
    /// </summary>
    public partial class Comments
    {
        private readonly IComments dal = DASNS.CreateComments();

        public Comments()
        { }

        #region BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Comments model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Comments model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CommentID)
        {
            return dal.Delete(CommentID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string CommentIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(CommentIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Comments GetModel(int CommentID)
        {
            return dal.GetModel(CommentID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Comments GetModelByCache(int CommentID)
        {
            string CacheKey = "CommentsModel-" + CommentID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(CommentID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.Comments)objModel;
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
        public List<Maticsoft.Model.SNS.Comments> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Comments> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Comments> modelList = new List<Maticsoft.Model.SNS.Comments>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Comments model;
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
        ///  /// 增加一条新的评论，给对应的type的评论数量相应的加1；
        /// </summary>
        /// <param name="ComModel"></param>
        /// <returns></returns>
        public int AddEx(Maticsoft.Model.SNS.Comments ComModel)
        {
            //增加一条评论，通过事务的方式对post和相应的photo以及product表中的commentcount都相应的增加1
            //过滤敏感词
            //进行敏感字过滤
            if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(ComModel.Description))
            {
                ComModel.Status = 0;
            }
            else
            {
                ComModel.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(ComModel.Description);
            }
            int CommentID = dal.AddEx(ComModel);
            switch (ComModel.Type)
            {
                case (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product:
                    Maticsoft.Model.SNS.Products pModel = new Model.SNS.Products();
                    Maticsoft.BLL.SNS.Products pBll = new Products();

                    pModel = pBll.GetModel(ComModel.TargetId);
                    if (!string.IsNullOrEmpty(pModel.TopCommentsId))
                    {
                        string[] ids = pModel.TopCommentsId.Split(',');
                        if (ids.Length < 3)
                        {
                            pModel.TopCommentsId = pModel.TopCommentsId + "," + CommentID;
                        }
                        if (ids.Length >= 3)
                        {
                            pModel.TopCommentsId = CommentID + "," + ids[0] + "," + ids[1];
                        }
                    }
                    else
                    {
                        pModel.TopCommentsId = CommentID.ToString(); ;
                    }
                    pBll.Update(pModel);
                    break;
                case (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo:
                    Maticsoft.Model.SNS.Photos photoModel = new Model.SNS.Photos();
                    Maticsoft.BLL.SNS.Photos photoBll = new Photos();
                    photoModel = photoBll.GetModel(ComModel.TargetId);
                    if (!string.IsNullOrEmpty(photoModel.TopCommentsId))
                    {
                        string[] ids = photoModel.TopCommentsId.Split(',');
                        if (ids.Length < 3)
                        {
                            photoModel.TopCommentsId = photoModel.TopCommentsId + "," + CommentID;
                        }
                        if (ids.Length >= 3)
                        {
                            photoModel.TopCommentsId = CommentID + "," + ids[0] + "," + ids[1];
                        }
                    }
                    else
                    {
                        photoModel.TopCommentsId = CommentID.ToString(); ;
                    }
                    photoBll.Update(photoModel);
                    break;
                case (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Album:
                    Maticsoft.BLL.SNS.UserAlbums albumsBll = new UserAlbums();
                    albumsBll.UpdateCommentCount(ComModel.TargetId);
                    break;
                case (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Normal:
                    Maticsoft.BLL.SNS.Posts postBll = new Posts();
                    postBll.UpdateCommentCount(ComModel.TargetId);
                    break;
                case (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Blog:
                    Maticsoft.BLL.SNS.UserBlog blogBll = new UserBlog();
                    Maticsoft.BLL.SNS.Posts postBll3 = new Posts();
                    blogBll.UpdateCommentCount(ComModel.TargetId);
                    postBll3.UpdateCommentCount(ComModel.TargetId);
                    break;
                default:
                    Maticsoft.BLL.SNS.Posts postBll2 = new Posts();
                    postBll2.UpdateCommentCount(ComModel.TargetId);
                    break;


            }


            Maticsoft.BLL.SNS.ReferUsers ReferBll = new ReferUsers();

            ///把评论的内容做@提到某人的处理，如果@某人了 则在相应的@表中的记录中加1;
            ReferBll.AddEx(ComModel.Description, Maticsoft.Model.SNS.EnumHelper.ReferType.Comment, CommentID);
            return CommentID;
        }

        public List<Maticsoft.Model.SNS.Comments> GetCommentByPost(Maticsoft.Model.SNS.Posts Post)
        {
            int Type = Post.Type.Value;
            if (Type == 3)
            {
                Type = 0;
            }
            int TargetId = (Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal ? Post.PostID : Post.TargetId);
            List<Maticsoft.Model.SNS.Comments> list = GetModelList(" TargetId=" + TargetId + " and Type=" + Type + "");
            List<Maticsoft.Model.SNS.Comments> list2 = new List<Model.SNS.Comments>();
            foreach (Maticsoft.Model.SNS.Comments item in list)
            {
                item.Description = ViewModel.ViewModelBase.RegexNickName(item.Description);
                list2.Add(item);
            }
            return list2;
        }

        /// <summary>
        ///分页获取先关的评论的数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Comments> GetCommentByPage(int type, int TargetId, int StartIndex, int EndIndex)
        {
            return DataTableToList(GetListByPage(" Status=1 and  Type=" + type + " and TargetId=" + TargetId + "", "CommentID desc", StartIndex, EndIndex).Tables[0]);
        }

        /// <summary>
        ///得到符合条件的评论的数量 用与分页用
        /// </summary>

        /// <returns></returns>
        public int GetCommentCount(int type, int TargetId)
        {
            return GetRecordCount(" Status=1 and Type=" + type + " and TargetId=" + TargetId + "");
        }

        public List<Maticsoft.Model.SNS.Comments> GetCommentByIds(string IdStr, int Type)
        {
            List<Maticsoft.Model.SNS.Comments> list = new List<Model.SNS.Comments>();
            if (!string.IsNullOrEmpty(IdStr))
            {
                list = GetModelList("CommentID in (" + IdStr + ")");
            }
            return list;
        }

        /// <summary>
        /// 缓存获取相应的评论（For商品和图片）
        /// </summary>
        /// <param name="IdStr"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Comments> GetCacheCommentByIds(string IdStr, int Type)
        {
            string CacheKey = "CacheCommentIds-" + IdStr + Type;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetCommentByIds(IdStr, Type);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.Comments>)objModel;
        }

        /// <summary>
        /// 专辑评论信息
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <returns></returns>
        public DataSet AblumComment(int ablumId, string strWhere)
        {
            return dal.AblumComment(ablumId, strWhere);
        }

        /// <summary>
        /// 删除专辑评论
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <param name="commentId">评论ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteComment(int ablumId, int commentId)
        {
            return dal.DeleteComment(ablumId, commentId);
        }


        /// <summary>
        /// 删除n条数据
        /// </summary>
        public bool DeleteListEx(string CommentIDlist)
        {
            return dal.DeleteListEx(CommentIDlist);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Comments> GetBlogComment(string strWhere,string orderBy,int top=-1)
        {
            DataSet ds = GetList(top, strWhere, orderBy);
            List<Maticsoft.Model.SNS.Comments> CommentList = DataTableToList(ds.Tables[0]);
            if (CommentList != null && CommentList.Count > 0)
            {
                Maticsoft.BLL.SNS.UserBlog userBlogBll=new UserBlog();
            foreach (var comment in CommentList)
            {
              comment.UserBlog=  userBlogBll.GetModelByCache(comment.TargetId);
            }
            }
            return CommentList;
        }


        #endregion ExtensionMethod
    }
}