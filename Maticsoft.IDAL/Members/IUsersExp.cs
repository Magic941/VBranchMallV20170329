using System;
using System.Data;

namespace Maticsoft.IDAL.Members
{
    /// <summary>
    /// 接口层UsersExp
    /// </summary>
    public interface IUsersExp
    {
        #region  成员方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Maticsoft.Model.Members.UsersExpModel model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Members.UsersExpModel model);

        /// <summary>
        /// 更新一条数据 申请好代
        /// </summary>
        bool UpdateApplyAgent(Maticsoft.Model.Members.UsersExpModel model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int UserID);
        bool DeleteList(string UserIDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Members.UsersExpModel GetModel(int UserID);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetUserList(int Top, string strWhere, string filedOrder);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        int GetRecordCount(string strWhere);


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        #endregion  成员方法
        #region 扩展方法
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(int userId);

        bool UpdateFavouritesCount();

        bool UpdateProductCount();

        bool UpdateShareCount();

        bool UpdateAblumsCount();

       
        /// <summary>
        /// 用户中心好代管理(李永琴)
        /// </summary>
        /// <returns></returns>
        DataSet Select_UsersEXP(string type, string StrWhere);

        /// <summary>
        /// 得到申请状态
        /// </summary>
        /// <returns></returns>
        long GetUserAppType(int UserID);

        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <returns></returns>
        long GetUserStatus(string type, int UserID);

        /// <summary>
        /// 审核修改状态
        /// </summary>
        /// <returns></returns>
        bool UpdateGoodUserStatus(Maticsoft.Model.Members.UsersExpModel model);

        /// <summary>
        /// 修改试用状态
        /// </summary>
        /// <returns></returns>
        bool UpdateGoodUsersProbation(Maticsoft.Model.Members.UsersExpModel model);

        /// <summary>
        /// 修改UserOldType 分销店=3 服务店=4
        /// </summary>
        /// <returns></returns>
        bool UpdateOldType(int UserOldType,int UserID);
        #endregion

        /// <summary>
        /// 搜索个数
        /// </summary>
        int GetUserCountByKeyWord(string NickName);
        /// <summary>
        /// 搜索分页
        /// </summary>
        /// <param name="NickName"></param>
        DataSet GetUserListByKeyWord(string NickName, string orderby, int startIndex, int endIndex);



        /// <summary>
        /// 是否通过实名认证
        /// </summary>
        bool UpdateIsDPI(string userIds, int status);

        bool UpdatePhoneAndPay(int userid, string account, string phone);

        int GetUserRankId(int UserId);
        decimal GetUserBalance(int UserId);

        DataSet GetAllEmpByUserId(int userId);

        /// <summary>
        /// 增加一条数据 (用户表和邀请表)事物执行
        /// </summary>
        /// <param name="model"></param>
        /// <param name="inviteuid">邀请者UserID</param>
        /// <param name="inviteNick">邀请者昵称</param>
        /// <param name="pointScore">影响积分</param>
        /// <returns></returns>
        bool AddEx(Model.Members.UsersExpModel model, int inviteuid, string inviteNick, int pointScore);

    }

}
