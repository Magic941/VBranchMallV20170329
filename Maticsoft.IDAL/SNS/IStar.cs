/**
* Star.cs
*
* 功 能： N/A
* 类 名： Star
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:55   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Data;

namespace Maticsoft.IDAL.SNS
{
    /// <summary>
    /// 接口层达人表
    /// </summary>
    public interface IStar
    {
        #region 成员方法

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int UserID, int TypeID);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.SNS.Star model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SNS.Star model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int ID);

        bool DeleteList(string IDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SNS.Star GetModel(int ID);

        Maticsoft.Model.SNS.Star DataRowToModel(DataRow row);

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

        #region MethodEx

        bool UpdateStateList(string IDlist, int status);

        /// <summary>
        /// 是否是达人
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        bool IsStar(int userId);

        /// <summary>
        /// 查询用户申请的达人信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        DataSet StarName(int userId);

        //删除数据，同时返回删除的数据，用于删除头像的物理文件
        bool DeleteList(string IDlist,out DataSet ds);

        bool IsExists(int userId, int typeId);

        #endregion MethodEx
    }
}