/**
* Group.cs
*
* 功 能： N/A
* 类 名： Group
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:43:07   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.WeChat.IDAL.Core;
using System.IO;
using System.Net;
using System.Text;
using Maticsoft.Json.Conversion;
using Maticsoft.Json;
namespace Maticsoft.WeChat.BLL.Core
{
    /// <summary>
    /// Group
    /// </summary>
    public partial class Group
    {
        private readonly IGroup dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (IGroup)new WeChat.SQLServerDAL.Core.Group() : null;//暂时预留
        public Group()
        { }
        #region  BasicMethod

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
        public bool Exists(int GroupId)
        {
            return dal.Exists(GroupId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.Group model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.Group model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int GroupId)
        {

            return dal.Delete(GroupId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string GroupIdlist)
        {
            return dal.DeleteList(GroupIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.Group GetModel(int GroupId)
        {

            return dal.GetModel(GroupId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.Group GetModelByCache(int GroupId)
        {

            string CacheKey = "GroupModel-" + GroupId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(GroupId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.Group)objModel;
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
        public List<Maticsoft.WeChat.Model.Core.Group> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.Group> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.Group> modelList = new List<Maticsoft.WeChat.Model.Core.Group>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.Group model;
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

        public bool Delete(string openId)
        {
            return dal.Delete(openId);
        }

        public bool GetGroups(string access_token, string openId, bool isCover)
        {
            StreamReader reader = null;
            string posturl = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token=" + access_token;
            //创建菜单
            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(posturl);
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = "GET";
                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                string content = reader.ReadToEnd();//得到结果
                //解析数据
                Maticsoft.Json.JsonObject jsonObject = JsonConvert.Import<JsonObject>(content);
                if (jsonObject["groups"]==null||String.IsNullOrWhiteSpace(jsonObject["groups"].ToString()))
                {
                    return false;
                }
                var groups = jsonObject["groups"].ToString();
                JsonArray jsonArray = JsonConvert.Import<JsonArray>(groups);
                if (isCover)
                {
                    //清除分组数据
                    Delete(openId);
                }
                Maticsoft.WeChat.Model.Core.Group group = null;
                foreach (JsonObject item in jsonArray)
                {
                    group = new Maticsoft.WeChat.Model.Core.Group();
                    group.OpenId = openId;
                    group.GroupName = item["name"].ToString();
                    Add(group);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }

        }


        public List<Maticsoft.WeChat.Model.Core.Group> GetGroupList(string openId, string keyword, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" GroupName like '%{0}%'", keyword);
            }
            DataSet ds = GetListByPage(strWhere.ToString(), "", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetCount(string openId, string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" GroupName like '%{0}%'", keyword);
            }
            return GetRecordCount(strWhere.ToString());
        }


        public List<Maticsoft.WeChat.Model.Core.Group> GetGroupList(string openId)
        {
            return GetModelList("  OpenId='"+openId+"'" );
        }

        #endregion  ExtensionMethod
    }
}

