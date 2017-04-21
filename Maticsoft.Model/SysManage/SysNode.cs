using System;
namespace Maticsoft.Model.SysManage
{

    /// <summary>
    /// 树节点
    /// </summary>
    [Serializable]
    public class SysNode
    {
        #region Model
        private int _nodeid;
        private string _treetext;
        private int _parentid;
        private string _parentpath;
        private string _location;
        private int? _orderid;
        private string _comment;
        private string _url;
        private int _permissionid;
        private string _imageurl;
        private int? _moduleid;
        private int? _keshidm;
        private string _keshipublic;
        private int _treetype = 0;
        private bool _enabled = true;
        /// <summary>
        /// 
        /// </summary>
        public int NodeID
        {
            set { _nodeid = value; }
            get { return _nodeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TreeText
        {
            set { _treetext = value; }
            get { return _treetext; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ParentPath
        {
            set { _parentpath = value; }
            get { return _parentpath; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Location
        {
            set { _location = value; }
            get { return _location; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int PermissionID
        {
            set { _permissionid = value; }
            get { return _permissionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ModuleID
        {
            set { _moduleid = value; }
            get { return _moduleid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? KeShiDM
        {
            set { _keshidm = value; }
            get { return _keshidm; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string KeshiPublic
        {
            set { _keshipublic = value; }
            get { return _keshipublic; }
        }
        /// <summary>
        /// 0:admin后台 1:企业后台  2:代理商后台 3:用户后台
        /// </summary>
        public int TreeType
        {
            set { _treetype = value; }
            get { return _treetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Enabled
        {
            set { _enabled = value; }
            get { return _enabled; }
        }
        #endregion Model

        private bool _hasChildren=true;

        public bool hasChildren
        {
            set { _hasChildren = value; }
            get { return _hasChildren; }
        }
    }
}
