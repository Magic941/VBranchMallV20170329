using System;
using System.Data;
using Maticsoft.Model.SysManage;
using Maticsoft.DALFactory;
using Maticsoft.Common;
using System.Collections.Generic;
using Maticsoft.IDAL.SysManage;
using System.Linq;
namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 系统菜单管理
    /// </summary>
    public class SysTree
    {
        private readonly ISysTree dal = DASysManage.CreateSysTree();


        public int GetPermissionCatalogID(int permissionID)
        {
            return dal.GetPermissionCatalogID(permissionID);
        }
        public SysTree()
        {
        }

        public int AddTreeNode(SysNode node)
        {
            return dal.AddTreeNode(node);
        }
        public void UpdateNode(SysNode node)
        {
            dal.UpdateNode(node);
        }
        public void DelTreeNode(int nodeid)
        {
            dal.DelTreeNode(nodeid);
        }
        public void DelTreeNodes(string nodeidlist)
        {
            dal.DelTreeNodes(nodeidlist);
        }
        public void MoveNodes(string nodeidlist, int ParentID)
        {
            dal.MoveNodes(nodeidlist, ParentID);
        }

        public DataSet GetTreeList(string strWhere)
        {
            return dal.GetTreeList(strWhere);
        }

        /// <summary>
        /// 获取全部菜单数据
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllTree()
        {
            return dal.GetTreeList("");
        }

        /// <summary>
        /// 根据菜单类型获取对应菜单数据
        /// </summary>
        /// <param name="treeType">菜单类型 0:admin后台 1:企业后台  2:代理商后台 3:用户后台</param>
        /// <returns></returns>
        public DataSet GetAllTreeByType(int treeType)
        {
            return dal.GetTreeList("TreeType=" + treeType);
        }

        /// <summary>
        /// 根据菜单类型获取启用的菜单数据
        /// </summary>
        /// <param name="treeType">菜单类型 0:admin后台 1:企业后台  2:代理商后台 3:用户后台</param>
        /// <returns></returns>
        public DataSet GetAllEnabledTreeByType(int treeType)
        {
            return GetAllEnabledTreeByType(treeType, true);
        }

        /// <summary>
        /// 根据菜单类型获取对应菜单数据
        /// </summary>
        /// <param name="treeType">菜单类型 0:admin后台 1:企业后台  2:代理商后台 3:用户后台</param>
        /// <param name="Enabled">是否启用</param>
        /// <returns></returns>
        public DataSet GetAllEnabledTreeByType(int treeType, bool Enabled)
        {
            return dal.GetTreeList("TreeType=" + treeType + " AND Enabled = " + (Enabled ? "1" : "0"));
        }

        /// <summary>
        /// 根据菜单类型获取对应菜单数据
        /// </summary>
        /// <param name="parentID">父ID</param>
        /// <param name="treeType">菜单类型 0:admin后台 1:企业后台  2:代理商后台 3:用户后台</param>
        /// <param name="Enabled">是否启用</param>
        /// <returns></returns>
        public DataSet GetEnabledTreeByParentId(int parentID, int treeType, bool Enabled)
        {
            return dal.GetTreeList("ParentID=" + parentID + " AND TreeType=" + treeType + " AND Enabled = " + (Enabled ? "1" : "0"));
        }

        /// <summary>
        /// Get an object list，From the cache
        /// <param name="treeType">菜单类型 0:admin后台 1:企业后台  2:代理商后台 3:用户后台</param>
        /// </summary>
        public DataSet GetAllEnabledTreeByType4Cache(int treeType)
        {
            string CacheKey = "GetAllEnabledTreeByType4Cache" + treeType;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetAllEnabledTreeByType(treeType);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (DataSet)objModel;
        }


        public DataSet GetTreeSonList(int NodeID, int treeType, List<int> UserPermissions)
        {
            string strWhere = " Enabled=1 and TreeType=" + treeType;
            if (NodeID > -1)
            {
                strWhere += " and parentid=" + NodeID;
            }
            if (UserPermissions.Count > 0)
            {
                strWhere += " and (PermissionID=-1 or PermissionID in (" + StringPlus.GetArrayStr(UserPermissions) + "))";
            }
            return dal.GetTreeList(strWhere);
        }

        public SysNode GetNode(int NodeID)
        {
            return dal.GetNode(NodeID);
        }

        /// <summary>
        /// Get an object entity，From the cache
        /// </summary>
        public SysNode GetModelByCache(int NodeID)
        {

            string CacheKey = "SysManageModel-" + NodeID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetNode(NodeID);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SysNode)objModel;
        }


        /// <summary>
        /// 修改启用状态
        /// </summary>
        /// <param name="nodeid"></param>
        public void UpdateEnabled(int nodeid)
        {
           dal.UpdateEnabled(nodeid);
        }


        public List<Maticsoft.Model.SysManage.SysNode> GetTreeListByType(int treeType,bool Enabled)
        {
            DataSet ds = GetAllEnabledTreeByType(treeType, Enabled);
            List<Maticsoft.Model.SysManage.SysNode> NodeList= DataTableToList(ds.Tables[0]);
            foreach (var sysNode in NodeList)
            {
                int count = NodeList.Where(c => c.ParentID == sysNode.NodeID).Count();
                if (count == 0)
                    sysNode.hasChildren = false;
            }
            return NodeList;
        }


        public List<Maticsoft.Model.SysManage.SysNode> GetTreeListByTypeCache(int treeType, bool Enabled)
        {
            string CacheKey = "GetTreeListByTypeCache-" + treeType + Enabled;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetTreeListByType(treeType, Enabled);
                    if (objModel != null)
                    {
                        int CacheTime = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SysManage.SysNode>)objModel;
        }
      
	
		/// <summary>
		/// 获得数据列表
		/// </summary>
        public List<Maticsoft.Model.SysManage.SysNode> DataTableToList(DataTable dt)
		{
            List<Maticsoft.Model.SysManage.SysNode> modelList = new List<Maticsoft.Model.SysManage.SysNode>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
                Maticsoft.Model.SysManage.SysNode model;
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

        #region 日志管理
        public void AddLog(string time, string loginfo, string Particular)
        {
            dal.AddLog(time, loginfo, Particular);
        }
        public void DelOverdueLog(int days)
        {
            dal.DelOverdueLog(days);
        }
        public void DeleteLog(string Idlist)
        {
            string str = "";
            if (Idlist.Trim() != "")
            {
                str = " ID in (" + Idlist + ")";
            }
            dal.DeleteLog(str);
        }
        public void DeleteLog(string timestart, string timeend)
        {
            string str = " datetime>'" + timestart + "' and datetime<'" + timeend + "'";
            dal.DeleteLog(str);
        }
        public DataSet GetLogs(string strWhere)
        {
            return dal.GetLogs(strWhere);
        }
        public DataRow GetLog(string ID)
        {
            return dal.GetLog(ID);
        }

        #endregion


    }
}
