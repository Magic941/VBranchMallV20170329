using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Maticsoft.IDAL.Shop.ActivityManage
{
    public interface IAMDetail
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int Add(Maticsoft.Model.Shop.ActivityManage.AMDetailModel model);
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ItemId);
        
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.ActivityManage.AMDetailModel model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int AMDId);
        bool DeleteList(string AMIdlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.ActivityManage.AMDetailModel GetModel(int AMDId);
        Maticsoft.Model.Shop.ActivityManage.AMDetailModel DataRowToModel(DataRow row);
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


        bool DeleteByAMId(int AMId);
    }
}
