using System;
namespace Maticsoft.Model.Members
{
	/// <summary>
	/// UserBind:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class UserBind
	{
		public UserBind()
		{}
        #region Model
        private int _bindid;
        private int _userid;
        private string _tokenaccess;
        private DateTime? _tokenexpiretime;
        private string _tokenrefresh;
        private string _mediauserid;
        private string _medianickname;
        private int _mediaid;
        private bool _ihome;
        private bool _comment;
        private bool _grouptopic;
        private int? _status = 1;
        /// <summary>
        /// 用户绑定ID
        /// </summary>
        public int BindId
        {
            set { _bindid = value; }
            get { return _bindid; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TokenAccess
        {
            set { _tokenaccess = value; }
            get { return _tokenaccess; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? TokenExpireTime
        {
            set { _tokenexpiretime = value; }
            get { return _tokenexpiretime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TokenRefresh
        {
            set { _tokenrefresh = value; }
            get { return _tokenrefresh; }
        }
        /// <summary>
        /// denglu.cc平台账号ID  灯鹭服务器返回值
        /// </summary>
        public string MediaUserID
        {
            set { _mediauserid = value; }
            get { return _mediauserid; }
        }
        /// <summary>
        /// 绑定的微博昵称
        /// </summary>
        public string MediaNickName
        {
            set { _medianickname = value; }
            get { return _medianickname; }
        }
        /// <summary>
        /// 对应媒体ID
        /// </summary>
        public int MediaID
        {
            set { _mediaid = value; }
            get { return _mediaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool iHome
        {
            set { _ihome = value; }
            get { return _ihome; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool Comment
        {
            set { _comment = value; }
            get { return _comment; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool GroupTopic
        {
            set { _grouptopic = value; }
            get { return _grouptopic; }
        }
        /// <summary>
        /// 状态  是否可用 0：不可用，1：可用
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

	    private string _weiboName;
        public string WeiboName
        {
            set { _weiboName = value; }
            get { return _weiboName; }
        }

        private string _weiboLogo;
        public string WeiboLogo
        {
            set { _weiboLogo = value; }
            get { return _weiboLogo; }
        }

	}
}

