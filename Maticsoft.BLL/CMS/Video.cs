/**
* Video.cs
*
* 功 能： 
* 类 名： Video
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/22 16:28:49  蒋海滨    初版
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
using Maticsoft.Model.CMS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.CMS;
namespace Maticsoft.BLL.CMS
{
    /// <summary>
    /// 视频发布
    /// </summary>
    public partial class Video
    {
        private readonly IVideo dal = DACMS.CreateVideo();
        public Video()
        { }
        #region  Method

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int VideoID)
        {
            return dal.Exists(VideoID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.CMS.Video model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.CMS.Video model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int VideoID)
        {

            return dal.Delete(VideoID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string VideoIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(VideoIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Video GetModel(int VideoID)
        {

            return dal.GetModel(VideoID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.CMS.Video GetModelByCache(int VideoID)
        {

            string CacheKey = "VideoModel-" + VideoID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(VideoID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.CMS.Video)objModel;
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
        public List<Maticsoft.Model.CMS.Video> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Video> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.CMS.Video> modelList = new List<Maticsoft.Model.CMS.Video>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.CMS.Video model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.CMS.Video();
                    if (dt.Rows[n]["VideoID"] != null && dt.Rows[n]["VideoID"].ToString() != "")
                    {
                        model.VideoID = int.Parse(dt.Rows[n]["VideoID"].ToString());
                    }
                    if (dt.Rows[n]["Title"] != null && dt.Rows[n]["Title"].ToString() != "")
                    {
                        model.Title = dt.Rows[n]["Title"].ToString();
                    }
                    if (dt.Rows[n]["Description"] != null && dt.Rows[n]["Description"].ToString() != "")
                    {
                        model.Description = dt.Rows[n]["Description"].ToString();
                    }
                    if (dt.Rows[n]["AlbumID"] != null && dt.Rows[n]["AlbumID"].ToString() != "")
                    {
                        model.AlbumID = int.Parse(dt.Rows[n]["AlbumID"].ToString());
                    }
                    if (dt.Rows[n]["CreatedUserID"] != null && dt.Rows[n]["CreatedUserID"].ToString() != "")
                    {
                        model.CreatedUserID = int.Parse(dt.Rows[n]["CreatedUserID"].ToString());
                    }
                    if (dt.Rows[n]["CreatedDate"] != null && dt.Rows[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(dt.Rows[n]["CreatedDate"].ToString());
                    }
                    if (dt.Rows[n]["LastUpdateUserID"] != null && dt.Rows[n]["LastUpdateUserID"].ToString() != "")
                    {
                        model.LastUpdateUserID = int.Parse(dt.Rows[n]["LastUpdateUserID"].ToString());
                    }
                    if (dt.Rows[n]["LastUpdateDate"] != null && dt.Rows[n]["LastUpdateDate"].ToString() != "")
                    {
                        model.LastUpdateDate = DateTime.Parse(dt.Rows[n]["LastUpdateDate"].ToString());
                    }
                    if (dt.Rows[n]["Sequence"] != null && dt.Rows[n]["Sequence"].ToString() != "")
                    {
                        model.Sequence = int.Parse(dt.Rows[n]["Sequence"].ToString());
                    }
                    if (dt.Rows[n]["VideoClassID"] != null && dt.Rows[n]["VideoClassID"].ToString() != "")
                    {
                        model.VideoClassID = int.Parse(dt.Rows[n]["VideoClassID"].ToString());
                    }
                    if (dt.Rows[n]["IsRecomend"] != null && dt.Rows[n]["IsRecomend"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsRecomend"].ToString() == "1") || (dt.Rows[n]["IsRecomend"].ToString().ToLower() == "true"))
                        {
                            model.IsRecomend = true;
                        }
                        else
                        {
                            model.IsRecomend = false;
                        }
                    }
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "")
                    {
                        model.ImageUrl = dt.Rows[n]["ImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["ThumbImageUrl"] != null && dt.Rows[n]["ThumbImageUrl"].ToString() != "")
                    {
                        model.ThumbImageUrl = dt.Rows[n]["ThumbImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["NormalImageUrl"] != null && dt.Rows[n]["NormalImageUrl"].ToString() != "")
                    {
                        model.NormalImageUrl = dt.Rows[n]["NormalImageUrl"].ToString();
                    }
                    if (dt.Rows[n]["TotalTime"] != null && dt.Rows[n]["TotalTime"].ToString() != "")
                    {
                        model.TotalTime = int.Parse(dt.Rows[n]["TotalTime"].ToString());
                    }
                    if (dt.Rows[n]["TotalComment"] != null && dt.Rows[n]["TotalComment"].ToString() != "")
                    {
                        model.TotalComment = int.Parse(dt.Rows[n]["TotalComment"].ToString());
                    }
                    if (dt.Rows[n]["TotalFav"] != null && dt.Rows[n]["TotalFav"].ToString() != "")
                    {
                        model.TotalFav = int.Parse(dt.Rows[n]["TotalFav"].ToString());
                    }
                    if (dt.Rows[n]["TotalUp"] != null && dt.Rows[n]["TotalUp"].ToString() != "")
                    {
                        model.TotalUp = int.Parse(dt.Rows[n]["TotalUp"].ToString());
                    }
                    if (dt.Rows[n]["Reference"] != null && dt.Rows[n]["Reference"].ToString() != "")
                    {
                        model.Reference = int.Parse(dt.Rows[n]["Reference"].ToString());
                    }
                    if (dt.Rows[n]["Tags"] != null && dt.Rows[n]["Tags"].ToString() != "")
                    {
                        model.Tags = dt.Rows[n]["Tags"].ToString();
                    }
                    if (dt.Rows[n]["VideoUrl"] != null && dt.Rows[n]["VideoUrl"].ToString() != "")
                    {
                        model.VideoUrl = dt.Rows[n]["VideoUrl"].ToString();
                    }
                    if (dt.Rows[n]["UrlType"] != null && dt.Rows[n]["UrlType"].ToString() != "")
                    {
                        model.UrlType = int.Parse(dt.Rows[n]["UrlType"].ToString());
                    }
                    if (dt.Rows[n]["VideoFormat"] != null && dt.Rows[n]["VideoFormat"].ToString() != "")
                    {
                        model.VideoFormat = dt.Rows[n]["VideoFormat"].ToString();
                    }
                    if (dt.Rows[n]["Domain"] != null && dt.Rows[n]["Domain"].ToString() != "")
                    {
                        model.Domain = dt.Rows[n]["Domain"].ToString();
                    }
                    if (dt.Rows[n]["Grade"] != null && dt.Rows[n]["Grade"].ToString() != "")
                    {
                        model.Grade = int.Parse(dt.Rows[n]["Grade"].ToString());
                    }
                    if (dt.Rows[n]["Attachment"] != null && dt.Rows[n]["Attachment"].ToString() != "")
                    {
                        model.Attachment = dt.Rows[n]["Attachment"].ToString();
                    }
                    if (dt.Rows[n]["Privacy"] != null && dt.Rows[n]["Privacy"].ToString() != "")
                    {
                        model.Privacy = int.Parse(dt.Rows[n]["Privacy"].ToString());
                    }
                    if (dt.Rows[n]["State"] != null && dt.Rows[n]["State"].ToString() != "")
                    {
                        model.State = int.Parse(dt.Rows[n]["State"].ToString());
                    }
                    if (dt.Rows[n]["Remark"] != null && dt.Rows[n]["Remark"].ToString() != "")
                    {
                        model.Remark = dt.Rows[n]["Remark"].ToString();
                    }
                    if (dt.Rows[n]["PvCount"] != null && dt.Rows[n]["PvCount"].ToString() != "")
                    {
                        model.PvCount = int.Parse(dt.Rows[n]["PvCount"].ToString());
                    }
                    modelList.Add(model);
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

        #endregion  Method

        #region MethodEx

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListEx(string strWhere ,string orderby)
        {
            return dal.GetListEx(strWhere, orderby);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.CMS.Video GetModelEx(int VideoID)
        {
            return dal.GetModelEx(VideoID);
        }
        #region 批量处理
        /// <summary>
        /// 批量处理
        /// </summary>
        /// <param name="IDlist"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public bool UpdateList(string IDlist, string strWhere)
        {
            return dal.UpdateList(IDlist, strWhere);
        }
        #endregion
        /// <summary>
        /// 得到最大顺序
        /// </summary>
        public int GetMaxSequence()
        {
            return dal.GetMaxSequence();
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public List<Model.CMS.Video> GetListByPage(int startIndex, int endIndex, out int toalCount)
        {
            toalCount = GetRecordCount(" State=5  ");
            DataSet ds= dal.GetListByPage(" State=5 ", " VideoID desc ", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得推荐数据数据列表
        /// </summary>
        public List<Maticsoft.Model.CMS.Video> GetRecModelList(int top)
        {
            DataSet ds = dal.GetList(top, " State=5 and IsRecomend=1 ", " VideoID desc ");
            return DataTableToList(ds.Tables[0]);
        }
        #endregion
    }
}

