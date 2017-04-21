using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Maticsoft.IDAL.Shop.ActivityManage
{
    public interface IAMP
    {
        /// <summary>
        /// 得到最大ID
        /// </summary>
        int GetMaxId();

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int AMId, long SupplierId);

        bool ExistsSup(int SupplierId);
        bool ExistsSups(int SupplierId,int AMId);
        bool ExistsPro(int ProductId);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(Maticsoft.Model.Shop.ActivityManage.AMPModel model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.ActivityManage.AMPModel model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(int SupplierId);

        bool Delete(int AMId, long SupplierId);
        bool DeleteByProId(int ProductId);
        
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        DataSet GetLists(string strWhere);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Maticsoft.Model.Shop.ActivityManage.AMPModel GetModel(int AMPId, long SupplierId);
        Maticsoft.Model.Shop.ActivityManage.AMPModel GetModelBySupplierId(int SupplierId);
        Maticsoft.Model.Shop.ActivityManage.AMPModel DataRowToModel(DataRow row);

        Maticsoft.Model.Shop.ActivityManage.AMPModel DataRow2ToModel(DataRow row);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetList();
        DataSet GetLists();
        DataSet GetLists(int AMId);
        DataSet GetListAndPrice(int Top, string strWhere, string filedOrder);
        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        bool DeleteByAMId(int AMId);
        DataSet GetAMProducts(int AMId, string strWhere);

        DataTable GetActiveRuleByAMId(int AMId, decimal Price);
        
    }
}
