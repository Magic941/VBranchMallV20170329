/**
* EntryForm.cs
*
* 功 能： 
* 类 名： EntryForm
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
namespace Maticsoft.IDAL.Ms
{
    /// <summary>
    /// 接口层报名表
    /// </summary>
    public interface IEntryForm
    {
        #region  成员方法
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int Id);
        /// <summary>
        /// 是否存在该用户记录
        /// </summary>
        bool Exists(string UserName);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(Maticsoft.Model.Ms.EntryForm model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Ms.EntryForm model);

        /// 更新一条数据
        /// </summary>
        bool Updates(Maticsoft.Model.Ms.EntryForm model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int Id);
        bool DeleteList(string Idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Ms.EntryForm GetModel(int Id);
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
        #endregion  成员方法

        #region 扩展的成员方法
        /// <summary>
        /// 批量处理
        /// </summary>
        /// <returns></returns>
        bool UpdateList(string IDlist, string strWhere);

        /// <summary>
        /// 根据用户名查询出当前用户是否申请好邻体验店
        /// </summary>
        /// <returns></returns>
        Maticsoft.Model.Ms.EntryForm GetByUserNameModel(string username);
        #endregion
    }
}
