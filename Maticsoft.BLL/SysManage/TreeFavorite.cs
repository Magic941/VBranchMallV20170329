using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SysManage;
namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 快捷菜单收藏管理
    /// </summary>
    public class TreeFavorite
    {
        private readonly ITreeFavorite dal = DASysManage.CreateTreeFavorite();


        #region  Method

        /// <summary>
        /// Whether there is Exists
        /// </summary>
        public bool Exists(int UserID, int NodeID)
        {
            return dal.Exists(UserID, NodeID);
        }


        /// <summary>
        /// Add a record
        /// </summary>
        public int Add(int UserID, int NodeID)
        {
            return dal.Add(UserID, NodeID);
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int ID)
        {

            dal.Delete(ID);
        }
        public void DeleteByUser(int UserID)
        {
            dal.DeleteByUser(UserID);
        }
        /// <summary>
        /// Delete a record
        /// </summary>
        public void Delete(int UserID, int NodeID)
        {
            dal.Delete(UserID, NodeID);
        }

        /// <summary>
        /// Query data list
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        public void UpDate(int OrderID, int UserID, int NodeID) {
            dal.UpDate(OrderID, UserID, NodeID);
        }
        public List<int> GetNodeIDsByUser(int UserID)
        {
            List<int> nodeids = new List<int>();
            DataSet ds= dal.GetNodeIDsByUser(UserID);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int nodeid = Convert.ToInt32(dr["NodeID"]);
                    nodeids.Add(nodeid);
                }
            }
            return nodeids;
        }
               
        /// <summary>
        /// Get menu list by Userid
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public DataSet GetMenuListByUser(int UserID)
        {
            return dal.GetMenuListByUser(UserID);
        }

               

        #endregion  Method

    }
}
