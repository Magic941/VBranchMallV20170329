using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Shop.ActivityManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Shop.ActivityManage;

namespace Maticsoft.BLL.Shop.ActivityManage
{
    public partial class AMBLL
    {
        private readonly IAM dal =  DAShopAM.CreateAM();
        public AMBLL()
		{}
        #region
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(Maticsoft.Model.Shop.ActivityManage.AMModel model)
        {
            return dal.Add(model);
            
        }


        public Maticsoft.Model.Shop.ActivityManage.AMModel GetAllActivity(int type)
        {
            return dal.GetAllActivity(type);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.Shop.ActivityManage.AMModel model)
        {
            return dal.Update(model);
        }

        public bool UpdateStatus(int AMId, int AMStatus)
        {
            return dal.UpdateStatus(AMId, AMStatus);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.Shop.ActivityManage.AMModel GetModel(int AMId)
        {

            return dal.GetModel(AMId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.Shop.ActivityManage.AMModel GetModelByCache(int AMId)
        {

            string CacheKey = "AMModel-" + AMId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AMId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.Shop.ActivityManage.AMModel)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }


        #region 删除方法
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteEx(int AMId)
        {
            return dal.DeleteEx(AMId);
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteListEx(string AMIdlist)
        {
            return dal.DeleteListEx(AMIdlist);
        }
        #endregion
        #endregion
    }
}
