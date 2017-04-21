using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
using Maticsoft.Model.Shop.Order;
namespace Maticsoft.BLL.Members
{
    /// <summary>
    /// Users
    /// </summary>
    public partial class Users
    {
        private readonly IUsers dal = DAMembers.CreateUsers();
        public Users()
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
        public bool Exists(int UserID)
        {
            return dal.Exists(UserID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.Members.Users model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Members.Users model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新一条数据 申请好代
        /// </summary>
        public bool UpdateApplyAgentHao(Maticsoft.Model.Members.Users model)
        {
            return dal.UpdateApplyAgentHao(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int UserID)
        {

            return dal.Delete(UserID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string UserIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(UserIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Members.Users GetModel(int UserID)
        {

            return dal.GetModel(UserID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.Users GetModelByCache(int UserID)
        {

            string CacheKey = "UsersModel-" + UserID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(UserID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.Users)objModel;
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
        public List<Maticsoft.Model.Members.Users> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.Members.Users> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.Users> modelList = new List<Maticsoft.Model.Members.Users>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.Users model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Members.Users();
                    if (dt.Rows[n]["UserID"] != null && dt.Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    }
                    if (dt.Rows[n]["UserName"] != null && dt.Rows[n]["UserName"].ToString() != "")
                    {
                        model.UserName = dt.Rows[n]["UserName"].ToString();
                    }
                    if (dt.Rows[n]["Password"] != null && dt.Rows[n]["Password"].ToString() != "")
                    {
                        model.Password = (byte[])dt.Rows[n]["Password"];
                    }
                    if (dt.Rows[n]["TrueName"] != null && dt.Rows[n]["TrueName"].ToString() != "")
                    {
                        model.TrueName = dt.Rows[n]["TrueName"].ToString();
                    }
                    if (dt.Rows[n]["Sex"] != null && dt.Rows[n]["Sex"].ToString() != "")
                    {
                        model.Sex = dt.Rows[n]["Sex"].ToString();
                    }
                    if (dt.Rows[n]["Phone"] != null && dt.Rows[n]["Phone"].ToString() != "")
                    {
                        model.Phone = dt.Rows[n]["Phone"].ToString();
                    }
                    if (dt.Rows[n]["Email"] != null && dt.Rows[n]["Email"].ToString() != "")
                    {
                        model.Email = dt.Rows[n]["Email"].ToString();
                    }
                    if (dt.Rows[n]["EmployeeID"] != null && dt.Rows[n]["EmployeeID"].ToString() != "")
                    {
                        model.EmployeeID = int.Parse(dt.Rows[n]["EmployeeID"].ToString());
                    }
                    if (dt.Rows[n]["DepartmentID"] != null && dt.Rows[n]["DepartmentID"].ToString() != "")
                    {
                        model.DepartmentID = dt.Rows[n]["DepartmentID"].ToString();
                    }
                    if (dt.Rows[n]["Activity"] != null && dt.Rows[n]["Activity"].ToString() != "")
                    {
                        if ((dt.Rows[n]["Activity"].ToString() == "1") || (dt.Rows[n]["Activity"].ToString().ToLower() == "true"))
                        {
                            model.Activity = true;
                        }
                        else
                        {
                            model.Activity = false;
                        }
                    }
                    if (dt.Rows[n]["UserType"] != null && dt.Rows[n]["UserType"].ToString() != "")
                    {
                        model.UserType = dt.Rows[n]["UserType"].ToString();
                    }
                    if (dt.Rows[n]["Style"] != null && dt.Rows[n]["Style"].ToString() != "")
                    {
                        model.Style = int.Parse(dt.Rows[n]["Style"].ToString());
                    }
                    if (dt.Rows[n]["User_iCreator"] != null && dt.Rows[n]["User_iCreator"].ToString() != "")
                    {
                        model.User_iCreator = int.Parse(dt.Rows[n]["User_iCreator"].ToString());
                    }
                    if (dt.Rows[n]["User_dateCreate"] != null && dt.Rows[n]["User_dateCreate"].ToString() != "")
                    {
                        model.User_dateCreate = DateTime.Parse(dt.Rows[n]["User_dateCreate"].ToString());
                    }
                    if (dt.Rows[n]["User_dateValid"] != null && dt.Rows[n]["User_dateValid"].ToString() != "")
                    {
                        model.User_dateValid = DateTime.Parse(dt.Rows[n]["User_dateValid"].ToString());
                    }
                    if (dt.Rows[n]["User_dateExpire"] != null && dt.Rows[n]["User_dateExpire"].ToString() != "")
                    {
                        model.User_dateExpire = DateTime.Parse(dt.Rows[n]["User_dateExpire"].ToString());
                    }
                    if (dt.Rows[n]["User_iApprover"] != null && dt.Rows[n]["User_iApprover"].ToString() != "")
                    {
                        model.User_iApprover = int.Parse(dt.Rows[n]["User_iApprover"].ToString());
                    }
                    if (dt.Rows[n]["User_dateApprove"] != null && dt.Rows[n]["User_dateApprove"].ToString() != "")
                    {
                        model.User_dateApprove = DateTime.Parse(dt.Rows[n]["User_dateApprove"].ToString());
                    }
                    if (dt.Rows[n]["User_iApproveState"] != null && dt.Rows[n]["User_iApproveState"].ToString() != "")
                    {
                        model.User_iApproveState = int.Parse(dt.Rows[n]["User_iApproveState"].ToString());
                    }
                    if (dt.Rows[n]["User_cLang"] != null && dt.Rows[n]["User_cLang"].ToString() != "")
                    {
                        model.User_cLang = dt.Rows[n]["User_cLang"].ToString();
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
        /// 根据DepartmentID删除一条数据
        /// </summary>
        public bool DeleteByDepartmentID(int DepartmentID)
        {
            return dal.DeleteByDepartmentID(DepartmentID);
        }
        /// <summary>
        /// 根据DepartmentID批量删除数据
        /// </summary>
        public bool DeleteListByDepartmentID(string DepartmentIDlist)
        {
            return dal.DeleteListByDepartmentID(DepartmentIDlist);
        }

        public bool ExistByPhone(string Phone)
        {
            return dal.ExistByPhone(Phone);
        }

        /// <summary>
        /// 根据邮箱判断是否存在该记录
        /// </summary>
        public bool ExistsByEmail(string UserEmail)
        {
            return dal.ExistsByEmail(UserEmail);
        }

        /// <summary>
        ///根据用户输入的昵称是否存在
        /// </summary>
        public bool ExistsNickName(string nickname)
        {
            return dal.ExistsNickName(nickname);
        }

          /// <summary>
        ///根据用户ID判断昵称是否已被其他用户使用
        /// </summary>
        public bool ExistsNickName(int userid,string nickname)
        {
            return dal.ExistsNickName(userid,nickname);
        }
        #endregion

        public DataSet GetList(string type, string keyWord)
        {
            return dal.GetList(type, keyWord);
        }


        /// <summary>
        /// 根据DepartmentID批量删除数据
        /// </summary>
        public bool DeleteEx(int  userId)
        {
            return dal.DeleteEx(userId);
        }

        /// <summary>
        /// 获取用户信息和用户附件信息（普通用户）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public DataSet GetListEX(string keyWord)
        {
            return dal.GetListEX(keyWord);
        }
        public DataSet GetListEXByType(string type, string keyWord = "")
        {
            return dal.GetListEXByType(type, keyWord);
        }
        public DataSet GetSearchList(string type, string StrWhere = "")
        {
            return dal.GetSearchList(type, StrWhere);
        }
        public bool UpdateFansAndFellowCount()
        {
            return dal.UpdateFansAndFellowCount();
        }

        public int GetUserIdByNickName(string NickName)
        {
            return dal.GetUserIdByNickName(NickName);
        }

        public int GetUserIdByUserName(string UserName)
        {
            return dal.GetUserIdByUserName(UserName);
        }

        public int GetUserIdByUserEmail(string userName)
        {
            return dal.GetUserIdByUserEmail(userName);
        }

        public string GetUserName(int userId)
        {
            return dal.GetUserName(userId);
        }
        public Maticsoft.Model.Members.Users GetUserIdByDepartmentID(string DepartmentID)
        {
            return dal.GetUserIdByDepartmentID(DepartmentID);
        }

        public bool UpdateActiveStatus(string Ids, int ActiveType)
        {
            return dal.UpdateActiveStatus(Ids, ActiveType);
        }

        public int GetDefaultUserId()
        {
            return dal.GetDefaultUserId();
        }
        public string GetNickName(int userId)
        {
            return dal.GetNickName(userId);
        }


        public Maticsoft.Model.Members.Users GetModel(string userName)
        {
            return dal.GetModel(userName);
        }

        public DataSet GetUserCount(StatisticMode mode, DateTime startDate, DateTime endDate)
        {
            return dal.GetUserCount(mode, startDate, endDate);
        }
        public Maticsoft.Model.Members.Users GetUsersInfo(int userid)
        {
            return dal.GetModel(userid);
        }

        public string GetPhoneByID(int? UserId)
        {
            return dal.GetUserByID(UserId);
        }

        /// <summary>
        /// 得到推荐人
        /// </summary>
        /// <returns></returns>
        public Maticsoft.Model.Members.Users RecommendUserName(int RecommendUserID)
        {
            return dal.RecommendUserName(RecommendUserID);
        }
    }
}

