/**
* GroupTopics.cs
*
* 功 能： N/A
* 类 名： GroupTopics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:43   N/A    初版
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
using System.Web;
using System.IO;
namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 小组话题
    /// </summary>
    public partial class GroupTopics
    {
        private readonly IGroupTopics dal = DASNS.CreateGroupTopics();
        public GroupTopics()
        { }
        #region  BasicMethod

        public bool Exists(int TopicID)
        {
            return dal.Exists(TopicID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.GroupTopics model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.GroupTopics model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int TopicID)
        {

            return dal.Delete(TopicID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string TopicIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(TopicIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.GroupTopics GetModel(int TopicID)
        {

            return dal.GetModel(TopicID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.GroupTopics GetModelByCache(int TopicID)
        {

            string CacheKey = "GroupTopicsModel-" + TopicID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(TopicID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.GroupTopics)objModel;
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
        public List<Maticsoft.Model.SNS.GroupTopics> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.GroupTopics> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.GroupTopics> modelList = new List<Maticsoft.Model.SNS.GroupTopics>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.GroupTopics model;
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
        /// 删除一条数据
        /// </summary>
        public bool DeleteEx(int TopicID)
        {
            return dal.DeleteEx(TopicID);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetListEx(int Top, string strWhere, string filedOrder)
        {
            return dal.GetListEx(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public List<Maticsoft.Model.SNS.GroupTopics> GetList4Model(int top, string strWhere, string filedOrder)
        {
            return DataTableToList(dal.GetListEx(top, strWhere, filedOrder).Tables[0]);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetSearchList(string Keywords)
        {
            string strWhere = string.Format(" Title like '%{0}%'", Keywords);
            string filedOrder = " TopicID DESC";
            return dal.GetListEx(0, strWhere, filedOrder);
        }
        #region 增加一条话题
        public int AddEx(Maticsoft.Model.SNS.GroupTopics TModel, long Pid)
        {
            #region 如果帖子中有商品，则提取商品的信息
            Maticsoft.Model.SNS.Products PModel = new Model.SNS.Products();
            Maticsoft.BLL.SNS.Products PBll = new Products();
            if (Pid > 0)
            {
                PModel.ProductID = Pid;
                PModel.CreateUserID = TModel.CreatedUserID;
                PModel.CreatedNickName = TModel.CreatedNickName;
                PModel.CreatedDate = DateTime.Now;
                PModel = PBll.GetProductModel(PModel);
            }
            #endregion
            string Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_product");
            PModel.Status = Status == "0" ? (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
            bool IsCheck = BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Check_GroupTopic");
            TModel.Status = IsCheck ? 0 : 1;

            bool isFilter = false;
            //进行敏感字过滤
            if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(TModel.Description))
            {
                PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
                TModel.Status = 0;
                isFilter = true;
            }
            else
            {
                TModel.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(TModel.Description, out isFilter);
            }
            if (isFilter)
            {
                //达到上限禁用用户
                if (BanUserCheck(TModel.CreatedUserID)) return -2;
            }
            #region 获取小组的名称
            Maticsoft.BLL.SNS.Groups Gbll = new Maticsoft.BLL.SNS.Groups();
            Maticsoft.Model.SNS.Groups Gmodel = new Maticsoft.Model.SNS.Groups();
            Gmodel = Gbll.GetModel(TModel.GroupID);
            TModel.GroupName = Gmodel.GroupName;

            #endregion
            return dal.AddEx(TModel, PModel);
        }
        #endregion

        #region 根据用户获取其相应帖子
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.GroupTopics> GetTopicByUserId(int UserId, int top)
        {
            List<Maticsoft.Model.SNS.GroupTopics> list = new List<Model.SNS.GroupTopics>();
            return DataTableToList(GetListByPage("CreatedUserID=" + UserId + "", "ReplyCount desc", 0, top).Tables[0]);
        }
        #endregion

        #region 分页得到数据
        public List<Maticsoft.Model.SNS.GroupTopics> GetTopicListPageByGroup(int GroupId, int StartIndex, int EndIndex, bool IsRecommand)
        {
            List<Maticsoft.Model.SNS.GroupTopics> list = new List<Model.SNS.GroupTopics>();
            string sql = " Status=1 and GroupID=" + GroupId + "";

            if (IsRecommand == true)
            {
                sql += " and IsRecomend=1";
            }
            list = DataTableToList(GetListByPage(sql, "CreatedDate Desc", StartIndex, EndIndex).Tables[0]);
            return list;
        }
        #endregion

        #region 得到推荐帖子的数量
        public int GetRecommandCount(int GroupId)
        {

            return GetRecordCount(" Status=1 and GroupID=" + GroupId + " and IsRecomend=1");

        }
        #endregion

        #region 得到最新的帖子
        public List<Maticsoft.Model.SNS.GroupTopics> GetNewTopListByGroup(int GroupId, int top)
        {
            return DataTableToList(GetListByPage(" Status=1 and GroupID=" + GroupId, "CreatedDate Desc", 0, top).Tables[0]);
        }
        #endregion

        #region 得到最热帖子
        public List<Maticsoft.Model.SNS.GroupTopics> GetHotListByGroup(int GroupId, int top)
        {
            string strWhere = " Status=1 ";
            if (GroupId > 0)
            {
                strWhere += " and GroupID=" + GroupId;
            }
            DataSet ds = dal.GetList(top, strWhere, "ReplyCount Desc");
            return DataTableToList(ds.Tables[0]);
        }
        #endregion

        #region 更加用户中心小组需要的不同类型得到相应的分页数据
        public List<Maticsoft.Model.SNS.GroupTopics> GetUserRelativeTopicByType(int UserId, Maticsoft.Model.SNS.EnumHelper.UserGroupType Type, int StartIndex, int EndIndex)
        {
            List<Maticsoft.Model.SNS.GroupTopics> List = new List<Model.SNS.GroupTopics>();
            string Sql = GetWhereByType(UserId, Type);

            return DataTableToList(GetListByPage(Sql, "CreatedDate Desc", StartIndex, EndIndex).Tables[0]);

        }
        #endregion

        #region 根据相应的条件生成相应的sql语句
        public string GetWhereByType(int UserId, Maticsoft.Model.SNS.EnumHelper.UserGroupType Type)
        {
            string Sql = "GroupID in(select GroupID from SNS_GroupUsers where UserID=" + UserId + ")";
            switch (Type)
            {
                case Maticsoft.Model.SNS.EnumHelper.UserGroupType.UserGroup:
                    Sql = "GroupID in(select GroupID from SNS_GroupUsers where UserID=" + UserId + ")";
                    break;
                case Maticsoft.Model.SNS.EnumHelper.UserGroupType.UserFav:
                    Sql = "TopicID in(select TopicID from SNS_GroupTopicFav where CreatedUserID=" + UserId + ")";
                    break;
                case Maticsoft.Model.SNS.EnumHelper.UserGroupType.UserPostTopic:
                    Sql = "CreatedUserID=" + UserId + "";
                    break;
                case Maticsoft.Model.SNS.EnumHelper.UserGroupType.UserReply:
                    Sql = "TopicID in (SELECT DISTINCT TopicID FROM SNS_GroupTopicReply WHERE  ReplyUserID=" + UserId + ")";
                    break;
            }

            return Sql;
        }
        #endregion

        #region 相应类型的数量
        public int GetCountByType(int UserId, Maticsoft.Model.SNS.EnumHelper.UserGroupType Type)
        {
            string Sql = GetWhereByType(UserId, Type);

            return GetRecordCount(Sql);

        }
        #endregion

        #region 搜索小组帖子

        public List<Maticsoft.Model.SNS.GroupTopics> SearchTopicByKeyWord(int StartIndex, int EndIndex, string q, int GroupId, string orderby)
        {
            switch (orderby)
            {
                case "newreply":
                    orderby = "CreatedDate desc";
                    break;
                case "newpost":
                    orderby = "LastPostTime desc";
                    break;
                default:
                    orderby = "CreatedDate desc";
                    break;
            }
            List<Maticsoft.Model.SNS.GroupTopics> list = new List<Model.SNS.GroupTopics>();
            string sql = " Status=1 and (Title like '%" + q + "%' or Description like '%" + q + "%') ";
            if (GroupId > 0)
            {
                sql += " and GroupID=" + GroupId + "";
            }
            list = DataTableToList(GetListByPage(sql, orderby, StartIndex, EndIndex).Tables[0]);
            return list;
        }
        public int GetCountByKeyWord(string q, int GroupId = -1)
        {
            string sql = " Status=1 and (Title like '%" + q + "%' or Description like '%" + q + "%') ";
            if (GroupId > 0)
            {
                sql += " and GroupID=" + GroupId + "";
            }
            return GetRecordCount(sql);

        }
        #endregion

        #region 更新状态

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="IdsStr">id集合</param>
        /// <param name="status">要更改的状态</param>
        /// <returns></returns>
        public bool UpdateStatusList(string IdsStr, Maticsoft.Model.SNS.EnumHelper.TopicStatus status)
        {
            return dal.UpdateStatusList(IdsStr, status);
        }
        #endregion

        #region 删除主题
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteListEx(string TopicIDlist)
        {

            string[] TopicIDs = TopicIDlist.Split(',');
            string imageUrl = "";
            if (TopicIDs.Length > 0)
            {
                foreach (string i in TopicIDs)
                {
                    int TopicId = Common.Globals.SafeInt(i, 0);
                    if (!dal.DeleteEx(TopicId, out imageUrl))
                    {
                        return false;
                    }
                    FileManage.DeleteFile(HttpContext.Current.Server.MapPath(imageUrl));
                }
            }
            return true;
        }




        #endregion


        /// <summary>
        /// 更新推荐状态
        /// </summary>
        public bool UpdateRecommand(int TopicId, int Recommand)
        {
            return dal.UpdateRecommand(TopicId, Recommand);
        }


        public bool UpdateAdminRecommand(int TopicId, bool IsAdmin)
        {
            return dal.UpdateAdminRecommand(TopicId, IsAdmin);
        }

        /// <summary>
        /// 更新pvcount
        /// </summary>
        public bool UpdatePVCount(int TopicID)
        {
            return dal.UpdatePVCount(TopicID);
        }

        public static string CreateIDCode()
        {
            DateTime Time1 = DateTime.Now.ToUniversalTime();
            DateTime Time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = Time1 - Time2;   //span就是两个日期之间的差额   
            string t = span.TotalMilliseconds.ToString("0");
            return t;
        }

        public List<Maticsoft.Model.SNS.GroupTopics> GetRecTopics(int Top = -1)
        {
            DataSet ds = GetList(Top, "Status=1 and IsAdminRecommend=1", " Sequence");
            return DataTableToList(ds.Tables[0]);
        }

        #endregion  ExtensionMethod

        #region SNS连续发帖冻结帐号
        /// <summary>
        /// SNS连续发帖禁用数
        /// </summary>
        public static int BanTopicCount
        {
            get { return BLL.SysManage.ConfigSystem.GetIntValueByCache("SNS_BAN_TOPIC_COUNT"); }
        }
        /// <summary>
        /// SNS连续发帖时长(分钟)
        /// </summary>
        public static int BanTopicTime
        {
            get { return BLL.SysManage.ConfigSystem.GetIntValueByCache("SNS_BAN_TOPIC_TIME"); }
        }

        public const string KEY_BAN = "SNS_BAN_TOPIC_USERID-{0}";

        /// <summary>
        /// 检测并冻结用户
        /// </summary>
        /// <returns>是否已冻结 false未冻结</returns>
        public static bool BanUserCheck(int userId)
        {
            if (userId < 1) return false;
            if (BanTopicCount < 1 || BanTopicTime < 1) return false;

            string key = string.Format(KEY_BAN, userId);
            //获取用户发帖count
            int count = Globals.SafeInt(DataCache.GetCache(key), 0);
            //自增 并重置过期时间
            DataCache.SetCache(key, ++count, DateTime.Now.AddMinutes(BanTopicTime), TimeSpan.Zero);
            if (count > BanTopicCount)
            {
                Maticsoft.Accounts.Bus.User userManage = new Maticsoft.Accounts.Bus.User();
                userManage.UpdateActivity(userId, false);
                return true;
            }
            return false;
        }
        #endregion
    }
}


