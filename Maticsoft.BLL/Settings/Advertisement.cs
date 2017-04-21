/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Advertisement.cs
// 文件功能描述：
// 
// 创建标识： [孙鹏]  2012/05/31 14:04:16
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Settings;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Settings;
namespace Maticsoft.BLL.Settings
{
    /// <summary>
    /// 广告管理
    /// </summary>
    public partial class Advertisement
    {
        private readonly IAdvertisement dal = DASettings.CreateAdvertisement();
        public Advertisement()
        { }
        #region  Method
        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxSequence()
        {
            return dal.GetMaxSequence();
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Settings.Advertisement model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Settings.Advertisement model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int AdvertisementId)
        {

            return dal.Delete(AdvertisementId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string AdvertisementIdlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(AdvertisementIdlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Settings.Advertisement GetModel(int AdvertisementId)
        {
            return dal.GetModel(AdvertisementId);
        }

          /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Settings.Advertisement GetModelByAdvPositionId(int AdvPositionId)
        {
            return dal.GetModelByAdvPositionId(AdvPositionId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Settings.Advertisement GetModelByCache(int AdvertisementId)
        {

            string CacheKey = "AdvertisementModel-" + AdvertisementId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AdvertisementId);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Settings.Advertisement)objModel;
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
        public List<Maticsoft.Model.Settings.Advertisement> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return dal.DataTableToList(ds.Tables[0]);
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

        public DataSet GetTransitionImg(int Aid, int ContentType, int? Num)
        {
            return dal.GetTransitionImg(Aid, ContentType, Num);
        }

        public DataSet GetTransitionImgByCache(int Aid, int ContentType, int? Num)
        {
            string CacheKey = "GetTransitionImgByCache-" + Aid + ContentType+(Num.HasValue?Num.Value:0);
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetTransitionImg(Aid, ContentType, Num);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }

        public DataSet SelectInfoByContentType(int ContentType)
        {
            return dal.SelectInfoByContentType(ContentType);
        }

        /// <summary>
        /// 文字广告
        /// </summary>
        public string CreateTextTag(int Aid, int ContentType)
        {
            DataSet ds = GetTransitionImgByCache(Aid, ContentType, null);
            if (ds != null)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //
                    System.Text.StringBuilder TagStr = new System.Text.StringBuilder();
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        //TagStr.Append("<tr><td align=\"left\" valign=\"top\">");
                        //TagStr.Append("<a href=\"");
                        //TagStr.Append(item["NavigateUrl"]);
                        //TagStr.Append(" \" target=\"_blank\" class=\"fonttitle\" style=\"margin-top: 5px;text-decoration:underline;\">");
                        //TagStr.Append("<b id=\"Color_title\">" + item["AlternateText"] + "</b></A></td>");
                        //TagStr.Append("</tr>");
                        //TagStr.Append("<li><a href=\"" + item["NavigateUrl"] + "\" target=\"_blank\"> " + item["AlternateText"] + "</a> </li>");
                        TagStr.Append("<li><a href=\"" + item["NavigateUrl"] + "\" target=\"_blank\"> " + item["AlternateText"] + "</a></li>");
                    }
                    return TagStr.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 图片广告
        /// </summary>
        public string CreatePicTag(int Aid, int ContentType, bool type, int? Num,int? snsAD)
        {
            DataSet ds = GetTransitionImgByCache(Aid, ContentType, Num);
            if (ds != null)
            {
                System.Text.StringBuilder TagStr = new System.Text.StringBuilder();
                DataTable dt = ds.Tables[0];
                if (type)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        TagStr.Append("<td><a target=\"_blank\" href=\"" + item["NavigateUrl"] + "\"><img src=\"" + item["FileUrl"] + "\" style=\"border: none;\" width=\"" + item["Width"] + "\" height=\"" + item["Height"] + "\"/></a></td>");
                    }
                    return TagStr.ToString();
                }
                else
                {
                    if (snsAD.HasValue && snsAD.Value == 3)
                    {
                        if (dt.Rows == null || dt.Rows.Count == 0)
                        {
                            return "";
                        }
                        System.Text.StringBuilder lunbo = new System.Text.StringBuilder();
                           System.Text.StringBuilder au = new System.Text.StringBuilder();
                             System.Text.StringBuilder conau = new System.Text.StringBuilder();
                        int i = 0;
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            if (i == 0)
                            {
                                lunbo.Append("<td class=\"active\" id=\"t" + i + "\" onmouseover=\"Mea(" + i +
                                            ");clearAuto();\" onmouseout=\"setAuto();\"valign=\"middle\" align=\"center\"> </td>");
                                au.Append("<div style=\"display: block\"><a target=\"_blank\" href=\"" + item["NavigateUrl"] +
                                   "\" > <img src=\"" + item["FileUrl"] + "\" /></a></div>");
                                conau.Append("<div > <div class=\" conau_left\"><div class=\"p2\"><a target=\"_blank\" href=\"" +
                                      item["NavigateUrl"] + "\">" + item["AlternateText"] + "</a></div></div> </div>");
                            }
                            else
                            {
                                lunbo.Append("<td width=\"8\"></td><td class=\"bg\" id=\"t" + i + "\" onmouseover=\"Mea(" + i +
                                            ");clearAuto();\" onmouseout=\"setAuto();\"valign=\"middle\" align=\"center\"> </td> ");
                                au.Append("<div style=\"display: none\"><a  target=\"_blank\" href=\"" + item["NavigateUrl"] +
                                       "\" > <img src=\"" + item["FileUrl"] + "\" /></a></div>");
                                conau.Append("<div style=\"display: none\"> <div class=\" conau_left\"><div class=\"p2\"><a target=\"_blank\" href=\"" +
                                                item["NavigateUrl"] + "\">" + item["AlternateText"] + "</a></div></div> </div>");
                            }
                          
                            i++;
                        }
                        TagStr.Append(
                            "   <div id=\"focus\" total=\""+i+"\"><div class=\"lunbo\"><table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"left\"><tr>");
                        TagStr.Append(lunbo)
                              .Append("  </tr> </table></div>")
                              .Append("<div id=\"au\">")
                              .Append(au)
                              .Append("  </div><div id=\"no\"> </div><div id=\"conau\">");
                        TagStr.Append(conau).Append("  </div> </div>");
                        return TagStr.ToString();
                    }
                    if (dt.Rows.Count > 1)
                    {
                        foreach (DataRow item in ds.Tables[0].Rows)
                        {
                            if (snsAD.HasValue && snsAD == 2)
                            {
                                TagStr.Append(" <li><a href=\"" + item["FileUrl"] + "\">");
                                TagStr.Append("<img border=\"0\" src=\"" + item["FileUrl"] + "\"");
                                TagStr.Append("width=\"" + item["Width"] + "\" height=\"" +(Convert.ToInt32( item["Height"])-56) + "\"  ></a></li>");
                            }
                            else
                            {
                                TagStr.Append(" <li><a target=\"_blank\" href=\"" + item["NavigateUrl"] + "\">");
                                TagStr.Append("<img border=\"0\" src=\"" + item["FileUrl"] + "\"");
                                TagStr.Append("width=\"" + item["Width"] + "\" height=\"" + item["Height"] + "\"  ></a></li>");
                            }
                        }
                        return TagStr.ToString();
                    }
                    else if (dt.Rows.Count == 1)
                    {
                        TagStr.Append(" <tr><td><a target=\"_blank\" href=\"" + dt.Rows[0]["NavigateUrl"] + "\">");
                        TagStr.Append("<img border=\"0\" src=\"" + dt.Rows[0]["FileUrl"] + "\"");
                        TagStr.Append("width=\"" + dt.Rows[0]["Width"] + "\" height=\"" + dt.Rows[0]["Height"] + "\"  ></a></td></tr>");
                        return TagStr.ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Flash广告
        /// </summary>
        public string CreateFlashTag(int Aid, int ContentType)
        {
            DataSet ds = GetTransitionImgByCache(Aid, ContentType, null);
            if (ds != null)
            {
                System.Text.StringBuilder TagStr = new System.Text.StringBuilder();
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 1)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        TagStr.Append(" <li><a target=\"_blank\" href=\"" + item["NavigateUrl"] + "\">");
                        TagStr.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\"");
                        TagStr.Append("width=\"" + item["Width"] + "\" height=\"" + item["Height"] + "\"  >");
                        TagStr.Append("<param name=\"wmode\" value=\"opaque\" /><param name=\"quality\" value=\"high\" />");
                        TagStr.Append("<param name=\"movie\" value=\"" + item["FileUrl"] + "\" />");
                        TagStr.Append("<embed src=\"" + item["FileUrl"] + "\" allowfullscreen=\"true\" quality=\"high\" width=\"" + item["Width"] + "\" height=\"" + item["Height"] + "\" align=\"middle\" wmode=\"transparent\" allowscriptaccess=\"always\" type=\"application/x-shockwave-flash\"></embed></object></a></li>");
                    }
                    return TagStr.ToString();
                }
                else if (dt.Rows.Count == 1)
                {
                    TagStr.Append(" <tr><td><a target=\"_blank\" href=\"" + dt.Rows[0]["NavigateUrl"] + "\">");
                    TagStr.Append("<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9,0,28,0\"");
                    TagStr.Append("width=\"" + dt.Rows[0]["Width"] + "\" height=\"" + dt.Rows[0]["Height"] + "\"  ></a></li>");
                    TagStr.Append("<param name=\"wmode\" value=\"opaque\" /><param name=\"quality\" value=\"high\" />");
                    TagStr.Append("<param name=\"movie\" value=\"" + dt.Rows[0]["FileUrl"] + "\" />");
                    TagStr.Append("<embed src=\"" + dt.Rows[0]["FileUrl"] + "\" allowfullscreen=\"true\" quality=\"high\" width=\"" + dt.Rows[0]["Width"] + "\" height=\"" + dt.Rows[0]["Height"] + "\" align=\"middle\" wmode=\"transparent\" allowscriptaccess=\"always\" type=\"application/x-shockwave-flash\"></embed></object></td></tr>");
                    return TagStr.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 自定义广告代码
        /// </summary>
        public string CreateCodeTag(int Aid, int ContentType)
        {
            DataSet ds = GetTransitionImgByCache(Aid, ContentType, null);
            if (ds != null)
            {
                System.Text.StringBuilder TagStr = new System.Text.StringBuilder();
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        TagStr.Append(item["AdvHtml"]);
                    }
                    return TagStr.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        /// <summary>
        /// 自定义广告位代码
        /// </summary>
        public string GetDefindCode(int Aid)
        {
            DataSet ds = dal.GetDefindCode(Aid);
            if (ds != null)
            {
                System.Text.StringBuilder TagStr = new System.Text.StringBuilder();
                DataTable dt = ds.Tables[0];
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        TagStr.Append(item["AdvHtml"]);
                    }
                    return TagStr.ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 广告位下是否存在广告
        /// </summary>
        public int IsExist(int AdvPositionId, int contentType)
        {
            return dal.IsExist(AdvPositionId, contentType);
        }

        public List<int> GetContentType(int AdPositionId)
        {
            DataSet ds = dal.GetContentType(AdPositionId);

            List<int> list = null;
            if (ds != null)
            {
                 list = new List<int>();
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    list.Add(Convert.ToInt32(item["ContentType"]));
                }
            }
            return list;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Settings.Advertisement> GetModelList(int Aid)
        {
            int? Top = 3;
            string strWhere = string.Format(" State={0} AND ContentType={1}  AND  AdvPositionId={2}",1,1, Aid);
            DataSet ds = dal.GetList(Top,strWhere,"");
            return dal.DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 根据广告位 获取广告内容
        /// </summary>
        /// <param name="Aid"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Settings.Advertisement> GetListByAidCache(int Aid,int top=0)
        {

            string CacheKey = "GetListByAidCache-" + Aid;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    DataSet ds = dal.GetList(top, string.Format("  State=1 AND   AdvPositionId={0}", Aid), " Sequence  ");

                    objModel = dal.DataTableToList(ds.Tables[0]);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.Settings.Advertisement>)objModel;
        }

        public bool UpdateSeq(int seq, int advId)
        {
            return dal.UpdateSeq(seq, advId);
        }
    }
}

