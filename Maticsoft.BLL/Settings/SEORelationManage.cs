/**
* SEORelationManage.cs
*
* 功 能： SEO关联链接处理--前台页面使用
* 类 名： SEORelationManage
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/15 13:55:22  Rock    初版
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
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.BLL.Settings
{
    public static class SEORelationManage
    {
        private static SEORelation manage = new SEORelation();

        /// <summary>
        /// SEO关联链接
        /// </summary>
        /// <param name="value">源文本信息</param>
        /// <param name="isIgnoreCase">是否区分大小写</param>
        /// <param name="IsCMS">CMS</param>
        /// <param name="IsShop">Shop</param>
        /// <param name="IsSNS">SNS</param>
        /// <param name="IsComment">Comment</param>
        /// <param name="FlagId">标识ID</param>
        /// <param name="FlagTitle">缓存标识名称 可以是 CMS，Shop，SNS，Comment</param>
        /// <returns>添加链接之后的文本</returns>
        public static string FilterStr(string value, bool isIgnoreCase, bool IsCMS, bool IsShop, bool IsSNS, bool IsComment, long FlagId, string FlagTitle)
        {
            string returnValue = value;
            if (string.IsNullOrWhiteSpace(returnValue))
            {
                return returnValue;
            }

            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" IsActive=1 ");

            string CacheKey = string.Format("SEORelationList-{0}-{1}", FlagTitle, FlagId);
            if (IsCMS)
            {
                strWhere.AppendFormat(" AND IsCMS=1 ");
            }
            if (IsShop)
            {
                strWhere.AppendFormat(" AND IsShop=1 ");
            }
            if (IsSNS)
            {
                strWhere.AppendFormat(" AND IsSNS=1 ");
            }
            if (IsComment)
            {
                strWhere.AppendFormat(" AND IsComment=1 ");
            }

            object obj = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (obj == null)
            {
                List<Maticsoft.Model.Settings.SEORelation> list = GetModelList(strWhere.ToString());
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (isIgnoreCase)
                        {
                            //不区分大小写
                            returnValue = System.Text.RegularExpressions.Regex.Replace(returnValue, list[i].KeyName, string.Format("<a href='" + list[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", list[i].KeyName), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        }
                        else
                        {
                            //区分大小写
                            returnValue = returnValue.Replace(list[i].KeyName, string.Format("<a href='" + list[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", list[i].KeyName));
                        }
                    }
                }
                int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                Maticsoft.Common.DataCache.SetCache(CacheKey, returnValue, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                return returnValue;
            }
            else
            {
                return (string)obj;
            }
        }

        /// <summary>
        /// SEO关联链接
        /// </summary>
        /// <param name="value">源数据信息</param>
        /// <param name="isIgnoreCase">是否区分大小写</param>
        /// <param name="model">搜索条件</param>
        /// <returns>替换后的数据信息</returns>
        public static string FilterStr(string value, bool isIgnoreCase, Model.Settings.SEORelation model)
        {
            string returnValue = value;
            if (string.IsNullOrWhiteSpace(returnValue))
            {
                return returnValue;
            }
            string strWhere = StrWhere(model);
            List<Maticsoft.Model.Settings.SEORelation> list = GetModelList(strWhere);
            if (list != null && list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (isIgnoreCase)
                    {
                        //不区分大小写
                        returnValue = System.Text.RegularExpressions.Regex.Replace(returnValue, list[i].KeyName, string.Format("<a href='" + list[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", list[i].KeyName), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    }
                    else
                    {
                        //区分大小写
                        returnValue = returnValue.Replace(list[i].KeyName, string.Format("<a href='" + list[i].LinkURL + "' target='_blank' title='{0}'>{0}</a>", list[i].KeyName));
                    }
                }
            }
            return returnValue;
        }

        private static string StrWhere(Model.Settings.SEORelation model)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" IsActive=1 ");
            if (model != null)
            {
                if (model.IsCMS)
                {
                    strWhere.AppendFormat(" AND IsCMS=1 ");
                }
                if (model.IsShop)
                {
                    strWhere.AppendFormat(" AND IsShop=1 ");
                }
                if (model.IsSNS)
                {
                    strWhere.AppendFormat(" AND IsSNS=1 ");
                }
                if (model.IsComment)
                {
                    strWhere.AppendFormat(" AND IsComment=1 ");
                }
                return strWhere.ToString();
            }
            else
            {
                return strWhere.ToString();
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        private static List<Maticsoft.Model.Settings.SEORelation> GetModelList(string strWhere)
        {
            string CacheKey = "SEORelationList";
            object obj = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (obj == null)
            {
                DataSet ds = manage.GetList("");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                    Maticsoft.Common.DataCache.SetCache(CacheKey, ds, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    DataRow[] drs = ds.Tables[0].Select(strWhere);
                    return DataTableToList(drs);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                DataSet ds = (DataSet)obj;
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] drs = ds.Tables[0].Select(strWhere);
                    return DataTableToList(drs);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        private static List<Maticsoft.Model.Settings.SEORelation> DataTableToList(DataRow[] dr)
        {
            List<Maticsoft.Model.Settings.SEORelation> modelList = new List<Maticsoft.Model.Settings.SEORelation>();
            int rowsCount = dr.Length;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Settings.SEORelation model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Settings.SEORelation();
                    if (dr[n]["RelationID"] != null && dr[n]["RelationID"].ToString() != "")
                    {
                        model.RelationID = int.Parse(dr[n]["RelationID"].ToString());
                    }
                    if (dr[n]["KeyName"] != null && dr[n]["KeyName"].ToString() != "")
                    {
                        model.KeyName = dr[n]["KeyName"].ToString();
                    }
                    if (dr[n]["LinkURL"] != null && dr[n]["LinkURL"].ToString() != "")
                    {
                        model.LinkURL = dr[n]["LinkURL"].ToString();
                    }
                    if (dr[n]["IsCMS"] != null && dr[n]["IsCMS"].ToString() != "")
                    {
                        if ((dr[n]["IsCMS"].ToString() == "1") || (dr[n]["IsCMS"].ToString().ToLower() == "true"))
                        {
                            model.IsCMS = true;
                        }
                        else
                        {
                            model.IsCMS = false;
                        }
                    }
                    if (dr[n]["IsShop"] != null && dr[n]["IsShop"].ToString() != "")
                    {
                        if ((dr[n]["IsShop"].ToString() == "1") || (dr[n]["IsShop"].ToString().ToLower() == "true"))
                        {
                            model.IsShop = true;
                        }
                        else
                        {
                            model.IsShop = false;
                        }
                    }
                    if (dr[n]["IsSNS"] != null && dr[n]["IsSNS"].ToString() != "")
                    {
                        if ((dr[n]["IsSNS"].ToString() == "1") || (dr[n]["IsSNS"].ToString().ToLower() == "true"))
                        {
                            model.IsSNS = true;
                        }
                        else
                        {
                            model.IsSNS = false;
                        }
                    }
                    if (dr[n]["IsComment"] != null && dr[n]["IsComment"].ToString() != "")
                    {
                        if ((dr[n]["IsComment"].ToString() == "1") || (dr[n]["IsComment"].ToString().ToLower() == "true"))
                        {
                            model.IsComment = true;
                        }
                        else
                        {
                            model.IsComment = false;
                        }
                    }
                    if (dr[n]["CreatedDate"] != null && dr[n]["CreatedDate"].ToString() != "")
                    {
                        model.CreatedDate = DateTime.Parse(dr[n]["CreatedDate"].ToString());
                    }
                    if (dr[n]["IsActive"] != null && dr[n]["IsActive"].ToString() != "")
                    {
                        if ((dr[n]["IsActive"].ToString() == "1") || (dr[n]["IsActive"].ToString().ToLower() == "true"))
                        {
                            model.IsActive = true;
                        }
                        else
                        {
                            model.IsActive = false;
                        }
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }
}