using System;
namespace Maticsoft.Model.Members
{
    /// <summary>
    /// Guestbook:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Guestbook
    {
        public Guestbook()
        { }
        #region Model
        private int _id;
        private int? _createuserid;
        private string _createnickname;
        private int? _touserid;
        private string _tonickname;
        private string _creatoruserip;
        private string _title;
        private string _description;
        private DateTime _createddate = DateTime.Now;
        private string _creatoremail;
        private string _creatorregion;
        private string _creatorcompany;
        private string _creatorpagesite;
        private string _creatorphone;
        private string _creatorqq;
        private string _creatormsn;
        private bool _creatorsex;
        private string _handlernickname;
        private int? _handleruserid;
        private DateTime? _handlerdate;
        private int? _privacy;
        private int _replycount = 0;
        private string _replydescription;
        private int? _parentid = 0;
        private int? _status;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 留言者ID(未登录则为-1)
        /// </summary>
        public int? CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        /// <summary>
        /// 留言者昵称
        /// </summary>
        public string CreateNickName
        {
            set { _createnickname = value; }
            get { return _createnickname; }
        }
        /// <summary>
        /// 留言接受者(管理员则为-1)
        /// </summary>
        public int? ToUserID
        {
            set { _touserid = value; }
            get { return _touserid; }
        }
        /// <summary>
        /// 留言接受者昵称（管理员为admin）
        /// </summary>
        public string ToNickName
        {
            set { _tonickname = value; }
            get { return _tonickname; }
        }
        /// <summary>
        /// 留言者IP
        /// </summary>
        public string CreatorUserIP
        {
            set { _creatoruserip = value; }
            get { return _creatoruserip; }
        }
        /// <summary>
        /// 留言标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 留言内容
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 留言时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 留言者邮箱
        /// </summary>
        public string CreatorEmail
        {
            set { _creatoremail = value; }
            get { return _creatoremail; }
        }
        /// <summary>
        /// 留言者地区
        /// </summary>
        public string CreatorRegion
        {
            set { _creatorregion = value; }
            get { return _creatorregion; }
        }
        /// <summary>
        /// 留言者所在公司
        /// </summary>
        public string CreatorCompany
        {
            set { _creatorcompany = value; }
            get { return _creatorcompany; }
        }
        /// <summary>
        /// 留言者主页
        /// </summary>
        public string CreatorPageSite
        {
            set { _creatorpagesite = value; }
            get { return _creatorpagesite; }
        }
        /// <summary>
        /// 留言者电话
        /// </summary>
        public string CreatorPhone
        {
            set { _creatorphone = value; }
            get { return _creatorphone; }
        }
        /// <summary>
        /// 留言者QQ
        /// </summary>
        public string CreatorQQ
        {
            set { _creatorqq = value; }
            get { return _creatorqq; }
        }
        /// <summary>
        /// 留言者Msn
        /// </summary>
        public string CreatorMsn
        {
            set { _creatormsn = value; }
            get { return _creatormsn; }
        }
        /// <summary>
        /// 留言者性别(null为未知,0为男,1为女)
        /// </summary>
        public bool CreatorSex
        {
            set { _creatorsex = value; }
            get { return _creatorsex; }
        }
        /// <summary>
        /// 处理人
        /// </summary>
        public string HandlerNickName
        {
            set { _handlernickname = value; }
            get { return _handlernickname; }
        }
        /// <summary>
        /// 处理人
        /// </summary>
        public int? HandlerUserID
        {
            set { _handleruserid = value; }
            get { return _handleruserid; }
        }
        /// <summary>
        /// 处理时间
        /// </summary>
        public DateTime? HandlerDate
        {
            set { _handlerdate = value; }
            get { return _handlerdate; }
        }
        /// <summary>
        /// 隐私(0 如果登录状态不显示当前登录者信息.1公开)
        /// </summary>
        public int? Privacy
        {
            set { _privacy = value; }
            get { return _privacy; }
        }
        /// <summary>
        /// 回复的数量
        /// </summary>
        public int ReplyCount
        {
            set { _replycount = value; }
            get { return _replycount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReplyDescription
        {
            set { _replydescription = value; }
            get { return _replydescription; }
        }
        /// <summary>
        /// 父评论
        /// </summary>
        public int? ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        ///  0：留言未处理 1：留言已处理
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}

