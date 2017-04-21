using System;
using System.Collections.Generic;
using System.Data;
using Maticsoft.Common;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
using Maticsoft.Model.Members;

namespace Maticsoft.BLL.Members
{
    /// <summary>
    /// 用户扩展类，继承了USER类相关属性和方法
    /// </summary>
    public class UsersExp : Maticsoft.Model.Members.UsersExpModel
    {
        private readonly IUsersExp dal = DAMembers.CreateUsersExp();

        #region Method

        /// <summary>
        /// 增加用户扩展数据
        /// </summary>
        public bool AddUsersExp(UsersExpModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新用户扩展数据
        /// </summary>
        public bool UpdateUsersExp(Maticsoft.Model.Members.UsersExpModel model)
        {
            return dal.Update(model);
        }
        /// <summary>
        /// 更新用户扩展数据
        /// </summary>
        public bool UpdateApplyAgent(Maticsoft.Model.Members.UsersExpModel model)
        {
            return dal.UpdateApplyAgent(model);
        }
        /// <summary>
        /// 删除用户扩展数据
        /// </summary>
        public bool DeleteUsersExp(int UserID)
        {
            return dal.Delete(UserID);
        }

        /// <summary>
        /// 删除用户扩展数据
        /// </summary>
        public bool DeleteListUsersExp(string UserIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(UserIDlist, 0));
        }

        /// <summary>
        /// 只获取用户扩展部分实体
        /// </summary>
        public Maticsoft.Model.Members.UsersExpModel GetUsersExpModel(int UserID)
        {
            return dal.GetModel(UserID);
        }

        /// <summary>
        /// 获取用户全部属性信息实体
        /// </summary>
        public Maticsoft.Model.Members.UsersExpModel GetUsersModel(int UserID)
        {
            //Users
            Maticsoft.Model.Members.UsersExpModel model = dal.GetModel(UserID);

            Maticsoft.BLL.Shop.Account.User u = new Shop.Account.User();
            if (model == null)
            {
                model = new UsersExpModel();
            }
            Maticsoft.Accounts.Bus.User user = new Accounts.Bus.User(UserID);

            model.Activity = user.Activity;
            model.DepartmentID = user.DepartmentID;
            model.Email = user.Email;
            model.EmployeeID = user.EmployeeID;
            model.Phone = user.Phone;
            if (user.Sex != null)
            {
                model.Sex = user.Sex.Trim();
            }
            model.Style = user.Style;
            model.TrueName = user.TrueName;
            model.NickName = user.NickName;
            model.User_cLang = user.User_cLang;
            model.User_dateApprove = user.User_dateApprove;
            model.User_dateCreate = user.User_dateCreate;
            model.User_dateExpire = user.User_dateExpire;
            model.User_dateValid = user.User_dateValid;
            model.User_iApprover = user.User_iApprover;
            model.User_iApproveState = user.User_iApproveState;
            model.User_iCreator = user.User_iCreator;
            model.UserID = user.UserID;
            model.UserName = user.UserName;
            model.UserType = user.UserType;
      
          
           
            model.IsPhoneVerify = u.GetPhoneMarkByID(UserID);

            return model;
        }


        /// <summary>
        /// 获取用户全部属性信息实体
        /// </summary>
        public Maticsoft.Model.Members.UsersExpModel GetUsersModelphone(int UserID)
        {
            //Users
            Maticsoft.Model.Members.UsersExpModel model = dal.GetModel(UserID);

            Maticsoft.BLL.Shop.Account.User u = new Shop.Account.User();
            if (model == null)
            {
                model = new UsersExpModel();
            }
            Maticsoft.Accounts.Bus.User user = new Accounts.Bus.User(UserID);

            model.Activity = user.Activity;
            model.DepartmentID = user.DepartmentID;
            model.Email = user.Email;
            model.EmployeeID = user.EmployeeID;
            model.Phone = user.Phone;
            if (user.Sex != null)
            {
                model.Sex = user.Sex.Trim();
            }
            model.Style = user.Style;
            model.TrueName = user.TrueName;
            model.NickName = user.NickName;
            model.User_cLang = user.User_cLang;
            model.User_dateApprove = user.User_dateApprove;
            model.User_dateCreate = user.User_dateCreate;
            model.User_dateExpire = user.User_dateExpire;
            model.User_dateValid = user.User_dateValid;
            model.User_iApprover = user.User_iApprover;
            model.User_iApproveState = user.User_iApproveState;
            model.User_iCreator = user.User_iCreator;
            model.UserID = user.UserID;
            model.UserName = user.UserName;
            model.UserType = user.UserType;
            

            model.IsPhoneVerify = u.GetPhoneMarkByID(UserID);

            return model;
        }


        /// <summary>
        /// 获取用户全部属性信息实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Members.UsersExpModel GetUsersExpModelByCache(int UserID)
        {
            string CacheKey = "UsersExpModel-" + UserID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetUsersModel(UserID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Members.UsersExpModel)objModel;
        }

        /// <summary>
        /// 获得用户扩展数据列表
        /// </summary>
        public DataSet GetUsersExpList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得用户扩展数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.UsersExpModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 获得用户扩展数据列表
        /// </summary>
        public List<Maticsoft.Model.Members.UsersExpModel> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Members.UsersExpModel> modelList = new List<Maticsoft.Model.Members.UsersExpModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Members.UsersExpModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Maticsoft.Model.Members.UsersExpModel();
                    if (dt.Rows[n]["UserID"] != null && dt.Rows[n]["UserID"].ToString() != "")
                    {
                        model.UserID = int.Parse(dt.Rows[n]["UserID"].ToString());
                    }
                    if (dt.Rows[n]["Gravatar"] != null && dt.Rows[n]["Gravatar"].ToString() != "")
                    {
                        model.Gravatar = dt.Rows[n]["Gravatar"].ToString();
                    }
                    if (dt.Rows[n]["Singature"] != null && dt.Rows[n]["Singature"].ToString() != "")
                    {
                        model.Singature = dt.Rows[n]["Singature"].ToString();
                    }
                    if (dt.Rows[n]["TelPhone"] != null && dt.Rows[n]["TelPhone"].ToString() != "")
                    {
                        model.TelPhone = dt.Rows[n]["TelPhone"].ToString();
                    }
                    if (dt.Rows[n]["QQ"] != null && dt.Rows[n]["QQ"].ToString() != "")
                    {
                        model.QQ = dt.Rows[n]["QQ"].ToString();
                    }
                    if (dt.Rows[n]["MSN"] != null && dt.Rows[n]["MSN"].ToString() != "")
                    {
                        model.MSN = dt.Rows[n]["MSN"].ToString();
                    }
                    if (dt.Rows[n]["HomePage"] != null && dt.Rows[n]["HomePage"].ToString() != "")
                    {
                        model.HomePage = dt.Rows[n]["HomePage"].ToString();
                    }
                    if (dt.Rows[n]["Birthday"] != null && dt.Rows[n]["Birthday"].ToString() != "")
                    {
                        model.Birthday = DateTime.Parse(dt.Rows[n]["Birthday"].ToString());
                    }
                    if (dt.Rows[n]["BirthdayVisible"] != null && dt.Rows[n]["BirthdayVisible"].ToString() != "")
                    {
                        model.BirthdayVisible = int.Parse(dt.Rows[n]["BirthdayVisible"].ToString());
                    }
                    if (dt.Rows[n]["BirthdayIndexVisible"] != null && dt.Rows[n]["BirthdayIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["BirthdayIndexVisible"].ToString() == "1") || (dt.Rows[n]["BirthdayIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.BirthdayIndexVisible = true;
                        }
                        else
                        {
                            model.BirthdayIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["Constellation"] != null && dt.Rows[n]["Constellation"].ToString() != "")
                    {
                        model.Constellation = dt.Rows[n]["Constellation"].ToString();
                    }
                    if (dt.Rows[n]["ConstellationVisible"] != null && dt.Rows[n]["ConstellationVisible"].ToString() != "")
                    {
                        model.ConstellationVisible = int.Parse(dt.Rows[n]["ConstellationVisible"].ToString());
                    }
                    if (dt.Rows[n]["ConstellationIndexVisible"] != null && dt.Rows[n]["ConstellationIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["ConstellationIndexVisible"].ToString() == "1") || (dt.Rows[n]["ConstellationIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.ConstellationIndexVisible = true;
                        }
                        else
                        {
                            model.ConstellationIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["NativePlace"] != null && dt.Rows[n]["NativePlace"].ToString() != "")
                    {
                        model.NativePlace = dt.Rows[n]["NativePlace"].ToString();
                    }
                    if (dt.Rows[n]["NativePlaceVisible"] != null && dt.Rows[n]["NativePlaceVisible"].ToString() != "")
                    {
                        model.NativePlaceVisible = int.Parse(dt.Rows[n]["NativePlaceVisible"].ToString());
                    }
                    if (dt.Rows[n]["NativePlaceIndexVisible"] != null && dt.Rows[n]["NativePlaceIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["NativePlaceIndexVisible"].ToString() == "1") || (dt.Rows[n]["NativePlaceIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.NativePlaceIndexVisible = true;
                        }
                        else
                        {
                            model.NativePlaceIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["RegionId"] != null && dt.Rows[n]["RegionId"].ToString() != "")
                    {
                        model.RegionId = int.Parse(dt.Rows[n]["RegionId"].ToString());
                    }
                    if (dt.Rows[n]["Address"] != null && dt.Rows[n]["Address"].ToString() != "")
                    {
                        model.Address = dt.Rows[n]["Address"].ToString();
                    }
                    if (dt.Rows[n]["AddressVisible"] != null && dt.Rows[n]["AddressVisible"].ToString() != "")
                    {
                        model.AddressVisible = int.Parse(dt.Rows[n]["AddressVisible"].ToString());
                    }
                    if (dt.Rows[n]["AddressIndexVisible"] != null && dt.Rows[n]["AddressIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["AddressIndexVisible"].ToString() == "1") || (dt.Rows[n]["AddressIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.AddressIndexVisible = true;
                        }
                        else
                        {
                            model.AddressIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["BodilyForm"] != null && dt.Rows[n]["BodilyForm"].ToString() != "")
                    {
                        model.BodilyForm = dt.Rows[n]["BodilyForm"].ToString();
                    }
                    if (dt.Rows[n]["BodilyFormVisible"] != null && dt.Rows[n]["BodilyFormVisible"].ToString() != "")
                    {
                        model.BodilyFormVisible = int.Parse(dt.Rows[n]["BodilyFormVisible"].ToString());
                    }
                    if (dt.Rows[n]["BodilyFormIndexVisible"] != null && dt.Rows[n]["BodilyFormIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["BodilyFormIndexVisible"].ToString() == "1") || (dt.Rows[n]["BodilyFormIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.BodilyFormIndexVisible = true;
                        }
                        else
                        {
                            model.BodilyFormIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["BloodType"] != null && dt.Rows[n]["BloodType"].ToString() != "")
                    {
                        model.BloodType = dt.Rows[n]["BloodType"].ToString();
                    }
                    if (dt.Rows[n]["BloodTypeVisible"] != null && dt.Rows[n]["BloodTypeVisible"].ToString() != "")
                    {
                        model.BloodTypeVisible = int.Parse(dt.Rows[n]["BloodTypeVisible"].ToString());
                    }
                    if (dt.Rows[n]["BloodTypeIndexVisible"] != null && dt.Rows[n]["BloodTypeIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["BloodTypeIndexVisible"].ToString() == "1") || (dt.Rows[n]["BloodTypeIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.BloodTypeIndexVisible = true;
                        }
                        else
                        {
                            model.BloodTypeIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["Marriaged"] != null && dt.Rows[n]["Marriaged"].ToString() != "")
                    {
                        model.Marriaged = dt.Rows[n]["Marriaged"].ToString();
                    }
                    if (dt.Rows[n]["MarriagedVisible"] != null && dt.Rows[n]["MarriagedVisible"].ToString() != "")
                    {
                        model.MarriagedVisible = int.Parse(dt.Rows[n]["MarriagedVisible"].ToString());
                    }
                    if (dt.Rows[n]["MarriagedIndexVisible"] != null && dt.Rows[n]["MarriagedIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["MarriagedIndexVisible"].ToString() == "1") || (dt.Rows[n]["MarriagedIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.MarriagedIndexVisible = true;
                        }
                        else
                        {
                            model.MarriagedIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["PersonalStatus"] != null && dt.Rows[n]["PersonalStatus"].ToString() != "")
                    {
                        model.PersonalStatus = dt.Rows[n]["PersonalStatus"].ToString();
                    }
                    if (dt.Rows[n]["PersonalStatusVisible"] != null && dt.Rows[n]["PersonalStatusVisible"].ToString() != "")
                    {
                        model.PersonalStatusVisible = int.Parse(dt.Rows[n]["PersonalStatusVisible"].ToString());
                    }
                    if (dt.Rows[n]["PersonalStatusIndexVisible"] != null && dt.Rows[n]["PersonalStatusIndexVisible"].ToString() != "")
                    {
                        if ((dt.Rows[n]["PersonalStatusIndexVisible"].ToString() == "1") || (dt.Rows[n]["PersonalStatusIndexVisible"].ToString().ToLower() == "true"))
                        {
                            model.PersonalStatusIndexVisible = true;
                        }
                        else
                        {
                            model.PersonalStatusIndexVisible = false;
                        }
                    }
                    if (dt.Rows[n]["Grade"] != null && dt.Rows[n]["Grade"].ToString() != "")
                    {
                        model.Grade = int.Parse(dt.Rows[n]["Grade"].ToString());
                    }
                    if (dt.Rows[n]["Balance"] != null && dt.Rows[n]["Balance"].ToString() != "")
                    {
                        model.Balance = decimal.Parse(dt.Rows[n]["Balance"].ToString());
                    }
                    if (dt.Rows[n]["Points"] != null && dt.Rows[n]["Points"].ToString() != "")
                    {
                        model.Points = int.Parse(dt.Rows[n]["Points"].ToString());
                    }
                    if (dt.Rows[n]["PvCount"] != null && dt.Rows[n]["PvCount"].ToString() != "")
                    {
                        model.PvCount = int.Parse(dt.Rows[n]["PvCount"].ToString());
                    }
                    if (dt.Rows[n]["LastAccessTime"] != null && dt.Rows[n]["LastAccessTime"].ToString() != "")
                    {
                        model.LastAccessTime = DateTime.Parse(dt.Rows[n]["LastAccessTime"].ToString());
                    }
                    if (dt.Rows[n]["LastAccessIP"] != null && dt.Rows[n]["LastAccessIP"].ToString() != "")
                    {
                        model.LastAccessIP = dt.Rows[n]["LastAccessIP"].ToString();
                    }
                    if (dt.Rows[n]["LastPostTime"] != null && dt.Rows[n]["LastPostTime"].ToString() != "")
                    {
                        model.LastPostTime = DateTime.Parse(dt.Rows[n]["LastPostTime"].ToString());
                    }
                    if (dt.Rows[n]["LastLoginTime"] != null && dt.Rows[n]["LastLoginTime"].ToString() != "")
                    {
                        model.LastLoginTime = DateTime.Parse(dt.Rows[n]["LastLoginTime"].ToString());
                    }
                    if (dt.Rows[n]["Remark"] != null && dt.Rows[n]["Remark"].ToString() != "")
                    {
                        model.Remark = dt.Rows[n]["Remark"].ToString();
                    }
                    if (dt.Rows[n]["NickName"] != null && dt.Rows[n]["NickName"].ToString() != "")
                    {
                        model.NickName = dt.Rows[n]["NickName"].ToString();
                    }
                    if (dt.Rows[n]["FellowCount"] != null && dt.Rows[n]["FellowCount"].ToString() != "")
                    {
                        model.FellowCount = int.Parse(dt.Rows[n]["FellowCount"].ToString());
                    }
                    if (dt.Rows[n]["FansCount"] != null && dt.Rows[n]["FansCount"].ToString() != "")
                    {
                        model.FansCount = int.Parse(dt.Rows[n]["FansCount"].ToString());
                    }
                    if (dt.Rows[n]["AblumsCount"] != null && dt.Rows[n]["AblumsCount"].ToString() != "")
                    {
                        model.AblumsCount = int.Parse(dt.Rows[n]["AblumsCount"].ToString());
                    }
                    if (dt.Rows[n]["ShareCount"] != null && dt.Rows[n]["ShareCount"].ToString() != "")
                    {
                        model.ShareCount = int.Parse(dt.Rows[n]["ShareCount"].ToString());
                    }

                    if (dt.Rows[n]["UserAppType"] != null && dt.Rows[n]["UserAppType"].ToString() != "")
                    {
                    }
                    if (dt.Rows[n]["UserStatus"] != null && dt.Rows[n]["UserStatus"].ToString() != "")
                    {
                        model.UserStatus = int.Parse(dt.Rows[n]["UserStatus"].ToString());
                    }
                    if (dt.Rows[n]["UserOldType"] != null && dt.Rows[n]["UserOldType"].ToString() != "")
                    {
                        model.UserOldType = int.Parse(dt.Rows[n]["UserOldType"].ToString());
                    }
                    if (dt.Rows[n]["IsUserDPI"] != null && dt.Rows[n]["IsUserDPI"].ToString() != "")
                    {
                        if ((dt.Rows[n]["IsUserDPI"].ToString() == "1") || (dt.Rows[n]["IsUserDPI"].ToString().ToLower() == "true"))
                        {
                            model.IsUserDPI = true;
                        }
                        else
                        {
                            model.IsUserDPI = false;
                        }
                    }
                    if (dt.Rows[n]["PayAccount"] != null)
                    {
                        model.PayAccount = dt.Rows[n]["PayAccount"].ToString();
                    }

                    if (dt.Rows[n]["Probation"] != null && dt.Rows[n]["Probation"].ToString() != "")
                    {
                        model.Probation = int.Parse(dt.Rows[n]["Probation"].ToString());
                    }

                    modelList.Add(model);
                }
            }
            return modelList;
        }

        #endregion Method

        #region 扩展方法

        public bool Add(int userId)
        {
            return dal.Add(userId);
        }

        public DataSet GetUserName(string strUName, int iCount)
        {
            string strWhere = "UserName like '" + strUName + "%' AND Activity=1";
            return dal.GetUserList(iCount, strWhere, "UserName");
        }

        //更新用户的分享数据
        public bool UpdateShareCount()
        {
            return dal.UpdateShareCount();
        }

        //更新用户的商品数据
        public bool UpdateProductCount()
        {
            return dal.UpdateProductCount();
        }

        //更新用户的喜欢数据
        public bool UpdateFavouritesCount()
        {
            return dal.UpdateFavouritesCount();
        }

        //更新用户的专辑数据
        public bool UpdateAblumsCount()
        {
            return dal.UpdateAblumsCount();
        }

        /// <summary>
        /// 用户中心好代管理(李永琴)
        /// </summary>
        /// <returns></returns>
        public DataSet Select_UsersEXP(string type, string StrWhere)
        {
            return dal.Select_UsersEXP(type, StrWhere);
        }

        /// <summary>
        /// 得到申请状态
        /// </summary>
        /// <param name="UserAppType"></param>
        /// <returns></returns>
        public long GetUserAppType(int UserID)
        {
            return dal.GetUserAppType(UserID);
        }

        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public long GetUserStatus(string type, int UserID)
        {
            return dal.GetUserStatus(type, UserID);
        }

        /// <summary>
        /// 审核修改状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateGoodUserStatus(Maticsoft.Model.Members.UsersExpModel model)
        {
            return dal.UpdateGoodUserStatus(model);
        }

        /// <summary>
        /// 修改试用状态
        /// </summary>
        /// <returns></returns>
        public bool UpdateGoodUsersProbation(Maticsoft.Model.Members.UsersExpModel model)
        {
            return dal.UpdateGoodUsersProbation(model);
        }
        /// <summary>
        /// 修改UserOldType 分销店=3 服务店=4
        /// </summary>
        /// <returns></returns>
        public bool UpdateOldType(int UserOldType,int UserID)
        {
            return dal.UpdateOldType(UserOldType, UserID);
        }

        /// <summary>
        /// 批量审核数据
        /// </summary>
        /// <returns></returns>
        //public bool UpdateGoodUserType(string Ids, int ActiveType)
        //{
        //    return dal.UpdateGoodUserType(Ids, ActiveType);
        //}

        #endregion 扩展方法

        /// <summary>
        /// 搜索个数
        /// </summary>
        public int GetUserCountByKeyWord(string NickName)
        {
            return dal.GetUserCountByKeyWord(NickName);
        }

        /// <summary>
        /// 搜索分页
        /// </summary>
        /// <param name="NickName"></param>
        public List<Maticsoft.Model.Members.UsersExpModel> GetUserListByKeyWord(string NickName, string orderby, int startIndex, int endIndex)
        {
            return DataTableToList(dal.GetUserListByKeyWord(NickName, orderby, startIndex, endIndex).Tables[0]);
        }

        /// <summary>
        /// 是否通过实名认证
        /// </summary>
        public bool UpdateIsDPI(string userIds, int status)
        {
            return dal.UpdateIsDPI(userIds, status);
        }

        public bool UpdatePhoneAndPay(int userid, string account, string phone)
        {
            return dal.UpdatePhoneAndPay(userid, account, phone);
        }

        public int GetUserRankId(int UserId)
        {
            return dal.GetUserRankId(UserId);
        }
        /// <summary>
        /// 获得指定用户ID的全部下属用户
        /// </summary>
        public DataSet GetAllEmpByUserId(int userId)
        {
            return dal.GetAllEmpByUserId(userId);
        }
        /// <summary>
        /// 获取用户余额
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public decimal GetUserBalance(int UserId)
        {
            return dal.GetUserBalance(UserId);
        }

        /// 增加一条数据 (用户表和邀请表)事物执行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="inviteID">邀请者UserID</param>
        /// <param name="inviteNick">邀请者昵称</param>
        /// <param name="pointScore">影响积分</param>
        /// <returns></returns>
        public bool AddEx(UsersExpModel model, int inviteID, string inviteNick, int pointScore)
        {
            return dal.AddEx(model, inviteID, inviteNick, pointScore);
        }
        /// <summary>
        /// 增加用户扩展数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="inviteuid">邀请用户UserID</param>
        /// <returns></returns>
        public bool AddExp(UsersExpModel model, int inviteuid)
        {
            //如果是被邀请用户则执行 AddEx()方法
            if (inviteuid > 0)
            {
                string InviteNick = new Users().GetNickName(inviteuid);
                if (!String.IsNullOrWhiteSpace(InviteNick))
                {
                    //邀请加积分
                    PointsDetail pointBll = new PointsDetail();
                    int pointScore = pointBll.AddPoints(6, inviteuid, "邀请用户");//影响分数
                    return dal.AddEx(model, inviteuid, InviteNick, pointScore);
                }
            }
            return dal.Add(model);
        }

        public Maticsoft.Model.Members.UsersExpModel GetUsersExpInfo(int userid)
        {
            return dal.GetModel(userid);
        }
    }
}