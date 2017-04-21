using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.ActivityManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.ActivityManage;

namespace Maticsoft.BLL.Shop.ActivityManage
{
    public partial class AMDetailBLL
    {
        private readonly IAMDetail dal = DAShopAMDetail.CreateAMD();
        public AMDetailBLL()
		{}
        #region
        public int Add(Maticsoft.Model.Shop.ActivityManage.AMDetailModel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.ActivityManage.AMDetailModel> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.Shop.ActivityManage.AMDetailModel> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.Shop.ActivityManage.AMDetailModel> modelList = new List<AMDetailModel>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.Shop.ActivityManage.AMDetailModel model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        //public DataSet GetAllList()
        //{
        //    return GetList("");
        //}

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByAMId(int AMId)
        {
            return dal.DeleteByAMId(AMId);
        }
        #endregion
    }
}
