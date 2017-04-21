using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Maticsoft.Model.Shop.Shipping;

namespace Maticsoft.IDAL.Shop.Shipping
{
    public interface IExpress
    {
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(string strWhere, string filedOrder);

         /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(Maticsoft.Model.Shop.Shipping.Shop_Express model);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        bool Add(Maticsoft.Model.Shop.Shipping.Shop_Express model);

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="filedOrder">排序</param>
        /// <param name="type">转换类型，0：用于前台显示，1：用于后台数据处理</param>
        /// <returns></returns>
        List<Maticsoft.Model.Shop.Shipping.Shop_Express> GetListModel(string strWhere, string filedOrder,int type);

        /// <summary>
        /// Table转化为对象列表
        /// </summary>
        /// <param name="table">表对象</param>
        /// <param name="type">转换类型，0：用于前台显示，1：用于后台数据处理</param>
        /// <returns></returns>
        List<Maticsoft.Model.Shop.Shipping.Shop_Express> DataTableToList(DataTable table, int type);
    }
}
