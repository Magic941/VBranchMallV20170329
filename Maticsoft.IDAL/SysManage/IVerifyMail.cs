using System;
using System.Data;
namespace Maticsoft.IDAL.SysManage
{
    /// <summary>
    /// 接口层VerifyMail
    /// </summary>
    public interface IVerifyMail
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string KeyValue);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Maticsoft.Model.SysManage.VerifyMail model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.SysManage.VerifyMail model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string KeyValue);
        bool DeleteList(string KeyValuelist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.SysManage.VerifyMail GetModel(string KeyValue);
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
        
        #endregion  成员方法
    }
}
