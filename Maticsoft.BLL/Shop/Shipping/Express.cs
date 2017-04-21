using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maticsoft.Model.Shop.Shipping;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.Shipping;
using System.Data;

namespace Maticsoft.BLL.Shop.Shipping
{
    public class Express
    {
        private readonly IExpress dal = DAShopShipping.CreateShop_Express();

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(string strWhere, string filedOrder)
        {
            return dal.GetList(strWhere,filedOrder);
        }

         /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.Shipping.Shop_Express model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Maticsoft.Model.Shop.Shipping.Shop_Express model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 获取对象列表
        /// </summary>
        /// <param name="strWhere">查询条件</param>
        /// <param name="filedOrder">排序</param>
        /// <param name="type">转换类型，0：用于前台显示，1：用于后台数据处理</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Shipping.Shop_Express> GetListModel(string strWhere, string filedOrder, int type)
        {
            return dal.GetListModel(strWhere, filedOrder, type);
        }

        /// <summary>
        /// Table转化为对象列表
        /// </summary>
        /// <param name="table">表对象</param>
        /// <param name="type">转换类型，0：用于前台显示，1：用于后台数据处理</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Shop.Shipping.Shop_Express> DataTableToList(DataTable table, int type)
        {
            return dal.DataTableToList(table, type);
        }
    }
}
