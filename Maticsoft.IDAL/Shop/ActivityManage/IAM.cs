using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Maticsoft.IDAL.Shop.ActivityManage
{
    public interface IAM
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(Maticsoft.Model.Shop.ActivityManage.AMModel model);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.ActivityManage.AMModel GetModel(int AMId);
        Maticsoft.Model.Shop.ActivityManage.AMModel DataRowToModel(DataRow row);
        Maticsoft.Model.Shop.ActivityManage.AMModel GetAllActivity(int type);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int AMId);
        bool DeleteList(string AMIdlist);

        bool DeleteEx(int AMId);

        bool DeleteListEx(string AMIdlist);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(Maticsoft.Model.Shop.ActivityManage.AMModel model);

        bool UpdateStatus(int AMId, int AMStatus);
    }
}
