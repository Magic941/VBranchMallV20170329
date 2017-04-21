using System.Data;
using Maticsoft.Model.Shop.Order;
using System;

namespace Maticsoft.IDAL.Members
{
    /// <summary>
    /// 接口层Users
    /// </summary>
    public interface IUsers
    {
        #region 成员方法

        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int UserID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Members.Users model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Members.Users model);
        /// <summary>
        /// 更新一条数据 申请好代
        /// </summary>
        bool UpdateApplyAgentHao(Maticsoft.Model.Members.Users model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int UserID);

        bool DeleteList(string UserIDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Members.Users GetModel(int UserID);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        int GetRecordCount(string strWhere);

        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 根据分页获得数据列表
        /// </summary>
        //DataSet GetList(int PageSize,int PageIndex,string strWhere);

        #endregion 成员方法

        #region 扩展的成员方法

        /// <summary>
        /// 根据DepartmentID删除一条数据
        /// </summary>
        bool DeleteByDepartmentID(int DepartmentID);

        /// <summary>
        /// 根据DepartmentID批量删除数据
        /// </summary>
        bool DeleteListByDepartmentID(string DepartmentIDlist);

        bool ExistByPhone(string Phone);

        bool ExistsByEmail(string Email);

        /// <summary>
        ///根据用户ID判断昵称是否已被其他用户使用
        /// </summary>
        bool ExistsNickName(string nickname);

        /// <summary>
        ///根据用户ID判断昵称是否已被其他用户使用
        /// </summary>
        bool ExistsNickName(int userid, string nickname);

        #endregion 扩展的成员方法

        DataSet GetList(string type, string keyWord);

        DataSet GetListEX(string keyWord);

        DataSet GetListEXByType(string type, string keyWord = "");
        /// <summary>
        /// 搜索用户
        /// </summary>
        DataSet GetSearchList(string type, string StrWhere = "");
        /// <summary>
        /// 一键更新用户的粉丝数和关注数
        /// </summary>
        bool UpdateFansAndFellowCount();

        /// <summary>
        /// 通过昵称获得用户的userid @某人的时候用到
        /// </summary>
        /// <param name="NickName">昵称</param>
        /// <returns></returns>
        int GetUserIdByNickName(string NickName);

        /// <summary>
        /// 通过用户名获得用户的userid @某人的时候用到
        /// </summary>
        /// <param name="UserName">昵称</param>
        /// <returns></returns>
        int GetUserIdByUserName(string UserName);

        string GetUserName(int userId);

        Maticsoft.Model.Members.Users GetUserIdByDepartmentID(string DepartmentID);
 
        /// <summary>
        /// 批量冻结或解冻
        /// </summary>
        bool UpdateActiveStatus(string Ids, int ActiveType);

        int GetUserIdByUserEmail(string userName);


        int GetDefaultUserId();
        string GetNickName(int userId);

       bool DeleteEx(int userId);

        Maticsoft.Model.Members.Users GetModel(string userName);

        DataSet GetUserCount(StatisticMode mode, DateTime startDate, DateTime endDate);

        string GetUserByID(int? UserId);

        /// <summary>
        /// 得到推荐人
        /// </summary>
        /// <returns></returns>
        Maticsoft.Model.Members.Users RecommendUserName(int RecommendUserID);
    }
}