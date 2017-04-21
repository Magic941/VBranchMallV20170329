using System;
namespace Maticsoft.Model.Shop.Package
{
    /// <summary>
    /// Package:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Package
    {
        public Package()
        { }
        #region Model
        private int _packageid;
        private int _categoryid;
        private string _name;
        private string _description;
        private string _photourl;
        private string _normalphotourl;
        private string _thumbphotourl;
        private DateTime? _createddate = DateTime.Now;
        private int? _status = 0;
        private string _remark;
        /// <summary>
        /// 包装的ID
        /// </summary>
        public int PackageId
        {
            set { _packageid = value; }
            get { return _packageid; }
        }
        /// <summary>
        /// 所属类别的id
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 包装的名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 包装的描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 对应的包装图片
        /// </summary>
        public string PhotoUrl
        {
            set { _photourl = value; }
            get { return _photourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NormalPhotoUrl
        {
            set { _normalphotourl = value; }
            get { return _normalphotourl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbPhotoUrl
        {
            set { _thumbphotourl = value; }
            get { return _thumbphotourl; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}

