using System;
namespace Maticsoft.Model.Settings
{
    /// <summary>
    /// FLinks:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class FriendlyLink
    {
        public FriendlyLink()
        {}
        #region Model
        private int _id;
        private string _name;
        private string _imgurl;
        private string _linkurl;
        private string _linkdesc;
        private int _state;
        private int _orderid;
        private string _contactperson;
        private string _email;
        private string _telphone;
        private int _typeid;
        private bool _isdisplay = true;
        private int? _imgwidth;
        private int? _imgheight;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImgUrl
        {
            set { _imgurl = value; }
            get { return _imgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkUrl
        {
            set { _linkurl = value; }
            get { return _linkurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LinkDesc
        {
            set { _linkdesc = value; }
            get { return _linkdesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int OrderID
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContactPerson
        {
            set { _contactperson = value; }
            get { return _contactperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Email
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TelPhone
        {
            set { _telphone = value; }
            get { return _telphone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsDisplay
        {
            set { _isdisplay = value; }
            get { return _isdisplay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ImgWidth
        {
            set { _imgwidth = value; }
            get { return _imgwidth; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ImgHeight
        {
            set { _imgheight = value; }
            get { return _imgheight; }
        }
        #endregion Model
    }
}

