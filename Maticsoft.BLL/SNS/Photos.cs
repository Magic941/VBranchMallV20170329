/**
* Photos.cs
*
* 功 能： N/A
* 类 名： Photos
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:47   N/A    初版
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
using System.Linq;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using Maticsoft.Model.SNS;
using System.Web;
using System.IO;
using System.Text;

namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 照片
    /// </summary>
    public partial class Photos
    {
        private readonly IPhotos dal = DASNS.CreatePhotos();
        private readonly Comments commentBll = new Comments();

        public Photos()
        { }

        #region BasicMethod
        public bool Exists(int PhotoId)
        {

            return dal.Exists(PhotoId);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Photos model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Photos model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PhotoID)
        {
            return dal.Delete(PhotoID);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PhotoIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(PhotoIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Photos GetModel(int PhotoID)
        {
            return dal.GetModel(PhotoID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Photos GetModelByCache(int PhotoID)
        {
            string CacheKey = "PhotosModel-" + PhotoID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(PhotoID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.Photos)objModel;
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
        public List<Maticsoft.Model.SNS.Photos> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Photos> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Photos> modelList = new List<Maticsoft.Model.SNS.Photos>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Photos model;
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
        /// 获得专辑下的图片数据列表
        /// </summary>
        public List<PostContent> GetPhotoListByPage(int categoryId, string orderby, int startIndex, int endIndex)
        {
            List<PostContent> imgList = new List<PostContent>();
            DataSet ds = dal.GetListByPage(string.Format(" Status=1 and CategoryId = {0} ", categoryId), orderby, startIndex, endIndex);
            List<string> commonIds = new List<string>();
            Maticsoft.BLL.SNS.Comments commentBll = new Comments();
            if (ds != null && ds.Tables.Count > 0)
            {
                PostContent model;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    model = new PostContent();
                    model.Type = (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo;
                    model.TargetId = Convert.ToInt32(dr["PhotoID"]);
                    model.TargetName = dr["PhotoName"].ToString();
                    model.TargetDescription = dr["Description"] != null ? dr["Description"].ToString() : "";
                    model.CommentCount = Convert.ToInt32(dr["CommentCount"]);
                    model.FavouriteCount = Convert.ToInt32(dr["FavouriteCount"]);
                    model.ThumbImageUrl = dr["ThumbImageUrl"].ToString();
                    model.TopCommentsId = dr["TopCommentsId"] != null ? dr["TopCommentsId"].ToString() : "";
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
                        List<Model.SNS.Comments> List = commentList.FindAll(xx => (xx.TargetId == img.TargetId && xx.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo));
                        if (List == null) return;
                        img.CommentList = List;
                    });
                }
            }
            return imgList;
        }

        public List<PostContent> GetCachePhotoListByPage(int categoryId, string orderby, int startIndex, int endIndex)
        {
            string CacheKey = "CachePhotoListByPage" + categoryId + orderby + startIndex + endIndex;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetPhotoListByPage(categoryId, orderby, startIndex, endIndex);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<PostContent>)objModel;
        }

        /// <summary>
        /// 更新pvcount
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public bool UpdatePvCount(int pid)
        {
            return dal.UpdatePvCount(pid);
        }

        #region 暂时代码 后期更新

        public List<Maticsoft.Model.SNS.Photos> GetTopPhotoList(int Top,int Type,Maticsoft.Model.SNS.EnumHelper.RecommendType mode=Maticsoft.Model.SNS.EnumHelper.RecommendType.Home)
        {
            string HomeGetValueType = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("HomeGetValueType");
            string Sql = "";
            if (HomeGetValueType != null && HomeGetValueType == ((int)Maticsoft.Model.SNS.EnumHelper.RecommendType.Home).ToString())
            {
                Sql = "IsRecomend=" + (int)mode;
            }
            if (Type != -1)
            {
                if (!String.IsNullOrWhiteSpace(Sql))
                {
                    Sql += " and ";
                }
                Sql += "  Type=" + Type;
            }
            return DataTableToList(GetList(Top,Sql, "PhotoID desc").Tables[0]);
        }

        #endregion 暂时代码 后期更新

        /// <summary>
        /// 删除一条数据（事务删除）
        /// </summary>
        public bool DeleteEX(int PhotoID)
        {
            return dal.DeleteEX(PhotoID);
        }

        /// <summary>
        /// 批量删除数据（事务删除）
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        public bool DeleteListEX(string PhotoIds)
        {
            return dal.DeleteListEX(PhotoIds);
        }

        /// <summary>
        /// 批量转移分类
        /// </summary>
        /// <param name="ProductIds"></param>
        /// <returns></returns>
        public bool UpdateCateList(string PhotoIds, int CateId)
        {
            return dal.UpdateCateList(PhotoIds, CateId);
        }

        public List<Maticsoft.Model.SNS.Photos> GetTopPhotoPostByType(int top, int CategoryId)
        {
            return DataTableToList(GetListByPage(" Status=1 and  CategoryId=" + CategoryId + "", "CreatedDate desc", 0, top).Tables[0]);
        }
        public List<Maticsoft.Model.SNS.Photos> GetRecPhoto(int categoryId, int top,
                                                            Maticsoft.Model.SNS.EnumHelper.RecommendType mode =
                                                                Maticsoft.Model.SNS.EnumHelper.RecommendType.Home)
        {
            int recType = (int) mode;
            DataTable dataTable =
                GetListByPage(string.Format(" Status =1 and CategoryId={0} and IsRecomend={1}", categoryId, recType), "CreatedDate desc", 0, top).Tables[0];
            return DataTableToList(dataTable);
        }
        /// <summary>
        /// 根据图片的类型和id获得其相应的信息，包括图片拥护者的信息和图片所在专辑的信息
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public Maticsoft.ViewModel.SNS.TargetDetail GetPhotoAssistionInfo(int pid,int type=-1)
        {
            Maticsoft.ViewModel.SNS.TargetDetail DetailModel = new ViewModel.SNS.TargetDetail();
            Maticsoft.BLL.Members.UsersExp UserEx = new Members.UsersExp();
            Maticsoft.BLL.SNS.UserAlbums UserAlbumBll = new UserAlbums();
            Maticsoft.BLL.SNS.UserAlbumDetail DetailAlbumBll = new UserAlbumDetail();
            DetailModel.Photo = GetModel(pid);
            DetailModel.UserModel = UserEx.GetUsersExpModel(DetailModel.Userid);
            DetailModel.UserAlums = UserAlbumBll.GetUserAlbum((int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo, DetailModel.TargetId, DetailModel.Userid);
            if (DetailModel.UserAlums != null)
            {
                DetailModel.CovorImageList = DetailAlbumBll.GetThumbImageByAlbum(DetailModel.UserAlums.AlbumID,type);
            }
            return DetailModel;
        }

        public List<Maticsoft.Model.SNS.Photos> GetRecommandByPid(int pid)
        {
            Maticsoft.Model.SNS.Photos model = GetModel(pid);
            List<Maticsoft.Model.SNS.Photos> list = new List<Model.SNS.Photos>();
            List<string> commonIds = new List<string>();
            if (model != null)
            {
                list = DataTableToList((GetListByPage("Type=" + model.Type + "", "CommentCount desc", 1, 24)).Tables[0]);

                var CommentIdIdList = (from item in list where !string.IsNullOrEmpty(item.TopCommentsId) select item.TopCommentsId).Distinct().ToArray();
                string CommentIdString = string.Join(",", CommentIdIdList);
                List<Model.SNS.Comments> commentList = commentBll.GetCommentByIds(CommentIdString, 1);
                if (commentList != null)
                {
                    //加载评论数据
                    list.ForEach(img =>
                    {
                        List<Model.SNS.Comments> List = commentList.FindAll(xx => (xx.TargetId == img.PhotoID && xx.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo));
                        if (List == null) return;
                        img.commmentList = List;
                    });
                }
                return list;
            }
            return null;
        }

        public List<Maticsoft.Model.SNS.ZuiInPhoto> GetZuiInList(int CategoryId, int Top)
        {
            List<Maticsoft.Model.SNS.ZuiInPhoto> list = new List<ZuiInPhoto>();
            DataSet ds = dal.GetZuiInList(CategoryId, Top);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    Maticsoft.Model.SNS.ZuiInPhoto model = new Maticsoft.Model.SNS.ZuiInPhoto();
                    model.AlbumsCount = Convert.ToInt32(dr["AlbumsCount"]);
                    model.FansCount = Convert.ToInt32(dr["FansCount"]);
                    model.NickName = dr["NickName"].ToString();
                    model.PhotoUrl = dr["PhotoUrl"].ToString();
                    model.PhotoId = Convert.ToInt32(dr["PhotoId"]);
                    model.UserId = Convert.ToInt32(dr["UserId"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public List<Maticsoft.Model.SNS.ZuiInPhoto> GetZuiInListByCache(int CategoryId, int Top)
        {
            string CacheKey = "GetZuiInListByCache" + CategoryId + Top;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetZuiInList(CategoryId, Top);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.ZuiInPhoto>)objModel;
        }

        /// <summary>
        /// 点击推荐动作，自动审核（否则推荐动作无意义）
        /// </summary>
        /// <param name="PhotoID"></param>
        /// <param name="Recomend"></param>
        /// <returns></returns>
        public bool UpdateRecomend(int PhotoID, int Recomend)
        {
            return dal.UpdateRecomend(PhotoID, Recomend);
        }
        /// <summary>
        /// 点击推荐动作，自动审核（否则推荐动作无意义）
        /// </summary>
        /// <param name="PhotoIds"></param>
        /// <param name="Recomend"></param>
        /// <returns></returns>
        public bool UpdateRecomendList(string PhotoIds, int Recomend)
        {
            return dal.UpdateRecomendList(PhotoIds, Recomend);
        }

        public bool UpdateStatus(int PhotoID, int Status)
        {
            return dal.UpdateStatus(PhotoID, Status);
        }

        public bool UpdateRecommandState(int id, int State)
        {
            return dal.UpdateRecommandState(id, State);
        }

        /// <summary>
        /// 根据专辑ID获取该用户自定义上传的照片路径
        /// </summary>
        /// <param name="ablumId">专辑ID</param>
        /// <returns>结果集</returns>
        public List<Model.SNS.Photos> UserUploadPhotoList(int ablumId)
        {
            DataSet ds = dal.UserUploadPhoto(ablumId);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
               return DataTableToList(ds.Tables[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 删除图片ids
        /// </summary>
        public DataSet DeleteListEx(string Ids, out int Result,bool IsSendMess=false,int SendUserID=1)
        {
            List<int> UserIdList = GetPhotoUserIds(Ids);
            DataSet ds= dal.DeleteListEx(Ids,out Result);
            if (Result > 0 && IsSendMess)
            {
               
                Maticsoft.BLL.Members.SiteMessage siteBll=new SiteMessage();
                foreach (var userId in UserIdList)
                {
                    siteBll.AddMessageByUser(SendUserID, userId, "图片删除", "您的图片涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
                }
              
            }
            return ds;
        }





        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCountEx(string strWhere, int CateId)
        {
            return dal.GetRecordCountEx(strWhere, CateId);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere, int CateId)
        {
            return dal.GetListEx(strWhere, CateId);
        }

        public DataSet GetListByPageEx(string strWhere, int CateId, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPageEx(strWhere, CateId, orderby, startIndex, endIndex);
        }



        #region 搜索商品得到条数和相关的数据
        /// <summary>
        /// 搜索数据的条数
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public int GetSearchCountByQ(string q)
        {
            return GetRecordCount(" Status =1 and Tags Like '%" + q + "%' or  Description like '%" + q + "%'");
        }


        /// <summary>
        ///根据关键字搜索
        /// </summary>
        public List<PostContent> GetListByKeyWord(string q, string orderby, int startIndex, int endIndex,string area="")
        {
            List<Maticsoft.Model.SNS.Photos> Photolist = new List<Maticsoft.Model.SNS.Photos>();
            //必须进行审核过滤
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Status =1 ");
            if (!string.IsNullOrEmpty(q))
            {
                strSql.Append( " and (Tags Like '%" + q + "%' or  Description like '%" + q + "%')");
            }
            if (!string.IsNullOrEmpty(area))
            {
                strSql.Append(" and (PhotoAddress like '%" + area + "%' )");
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
                    orderby = "CommentCount";
                    break;
                default:
                    orderby = "FavouriteCount";
                    break;
            }
            DataSet ds = dal.GetListByPage(strSql.ToString(), orderby, startIndex, endIndex);
            List<PostContent> imgList = new List<PostContent>();
            List<string> commonIds = new List<string>();
            Maticsoft.BLL.SNS.Comments commentBll = new Comments();
            if (ds != null && ds.Tables.Count > 0)
            {
                PostContent model;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    model = new PostContent();
                    model.Type = (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo;
                    model.TargetId = Convert.ToInt32(dr["PhotoID"]);
                    model.TargetName = dr["PhotoName"].ToString();
                    model.TargetDescription = dr["Description"] != null ? dr["Description"].ToString() : "";
                    model.CommentCount = Convert.ToInt32(dr["CommentCount"]);
                    model.FavouriteCount = Convert.ToInt32(dr["FavouriteCount"]);
                    model.ThumbImageUrl = dr["ThumbImageUrl"].ToString();
                    model.TopCommentsId = dr["TopCommentsId"] != null ? dr["TopCommentsId"].ToString() : "";
                    model.StaticUrl = dr["StaticUrl"] != null ? dr["StaticUrl"].ToString() : ""; 
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
                        List<Model.SNS.Comments> List = commentList.FindAll(xx => (xx.TargetId == img.TargetId && xx.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo));
                        if (List == null) return;
                        img.CommentList = List;
                    });
                }
            }
            return imgList;

        }

        public static string MoveImage(string ImageUrl, string savePath, string saveThumbsPath)
        {
            try
            {
                if (BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
                {
                    return ImageUrl + "|" + ImageUrl;
                }
                if (!string.IsNullOrEmpty(ImageUrl))
                {
                 
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(savePath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(savePath));

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath(saveThumbsPath)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath(saveThumbsPath));

                    List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                        Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS);

                    string imgname = ImageUrl.Substring(ImageUrl.LastIndexOf("/") + 1);
                    string destImage = "";
                    string originalUrl = "";
                    string thumbUrl = saveThumbsPath + imgname;
                    //首先移动原图片
          
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, ""))))
                    {
                        originalUrl = String.Format(savePath + imgname,"");
                        System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, "")), HttpContext.Current.Server.MapPath(originalUrl));

                    }
                    if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                    {
                        foreach (var thumbSize in ThumbSizeList)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                            {
                                destImage = String.Format(thumbUrl, thumbSize.ThumName);
                                System.IO.File.Move(HttpContext.Current.Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName)), HttpContext.Current.Server.MapPath(destImage));

                            }
                        }
                    }
                    return originalUrl + "|" + thumbUrl;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return "";
        }

        public string GetThumByPhotoID(int id)
        {

            Maticsoft.Model.SNS.Photos model = GetModel(id);
            if (model != null)
            {
                return model.ThumbImageUrl;

            }
            return "";
        }

        public static string CreateIDCode()
        {
            DateTime Time1 = DateTime.Now.ToUniversalTime();
            DateTime Time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = Time1 - Time2;   //span就是两个日期之间的差额   
            string t = span.TotalMilliseconds.ToString("0");
            return t;
        }
        #endregion

        /// <summary>
        /// 获取需要静态化的商品数据(或者图片重新生成)
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public List<int> GetListToReGen( string strWhere)
        {
            DataSet ds = dal.GetListToReGen(strWhere);
            List<int> PhotoIdList = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["PhotoID"] != null && ds.Tables[0].Rows[n]["PhotoID"].ToString() != "")
                    {
                        PhotoIdList.Add(int.Parse(ds.Tables[0].Rows[n]["PhotoID"].ToString()));
                    }
                }
            }
            return PhotoIdList;
        }

        /// <summary>
        /// 更新静态页面地址
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="staticUrl"></param>
        /// <returns></returns>
        public bool UpdateStaticUrl(int photoId, string staticUrl)
        {
            return dal.UpdateStaticUrl(photoId, staticUrl);
        }

        /// <summary>
        /// 得到上一个PhotoId
        /// </summary>
        public int GetPrevID(int PhotoId, int albumId=-1)
        {
            return dal.GetPrevID(PhotoId, albumId);
        }

        /// <summary>
        /// 得到下一个PhotoId
        /// </summary>
        public int GetNextID(int PhotoId, int albumId=-1)
        {
            return dal.GetNextID(PhotoId, albumId);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetCountEx(int type, int categoryId, string address)
        {
            return dal.GetCountEx(type, categoryId, address);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="albumtype"></param>
        /// <param name="orderby"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<PostContent> GetPhotoListByPage(int type, int categoryId, string address, string orderby, int startIndex, int endIndex)
        {
            List<PostContent> imgList = new List<PostContent>();
            DataSet ds = dal.GetListByPageEx(type, categoryId, address, orderby, startIndex, endIndex);
            List<string> commonIds = new List<string>();
            Maticsoft.BLL.SNS.Comments commentBll = new Comments();
            if (ds != null && ds.Tables.Count > 0)
            {
                PostContent model;
                DataTable dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    model = new PostContent();
                    model.Type = (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo;
                    model.TargetId = Convert.ToInt32(dr["PhotoID"]);
                    model.TargetName = dr["PhotoName"].ToString();
                    model.TargetDescription = dr["Description"] != null ? dr["Description"].ToString() : "";
                    model.CommentCount = Convert.ToInt32(dr["CommentCount"]);
                    model.FavouriteCount = Convert.ToInt32(dr["FavouriteCount"]);
                    model.ThumbImageUrl = dr["ThumbImageUrl"].ToString();
                    model.TopCommentsId = dr["TopCommentsId"] != null ? dr["TopCommentsId"].ToString() : "";
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
                        List<Model.SNS.Comments> List = commentList.FindAll(xx => (xx.TargetId == img.TargetId && xx.Type == (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo));
                        if (List == null) return;
                        img.CommentList = List;
                    });
                }
            }
            return imgList;
        }

        public List<PostContent> GetPhotoListByPageCache(int type, int categoryId, string address, string orderby, int startIndex, int endIndex)
        {
            string CacheKey = "GetPhotoListByPageCache" + type + categoryId + address + orderby + startIndex + endIndex;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetPhotoListByPage(type, categoryId, address, orderby, startIndex, endIndex);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<PostContent>)objModel;
        }

        /// <summary>
        /// 获取照片用户ID
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<int> GetPhotoUserIds(string ids)
        {
            DataSet ds = dal.GetPhotoUserIds(ids);
            List<int> UserIdList = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["CreatedUserID"] != null && ds.Tables[0].Rows[n]["CreatedUserID"].ToString() != "")
                    {
                        UserIdList.Add(int.Parse(ds.Tables[0].Rows[n]["CreatedUserID"].ToString()));
                    }
                }
            }
            return UserIdList;
        }

        #endregion ExtensionMethod
    }
}