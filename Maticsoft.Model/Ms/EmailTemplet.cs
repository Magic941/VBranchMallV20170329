using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// EmailTemplet:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class EmailTemplet
	{
		public EmailTemplet()
		{}
        #region Model
        private int _templetid;
        private string _emailtype;
        private int _emailpriority;
        private string _tagdescription;
        private string _emaildescription;
        private string _emailsubject;
        private string _emailbody;
        /// <summary>
        /// 
        /// </summary>
        public int TempletId
        {
            set { _templetid = value; }
            get { return _templetid; }
        }
        /// <summary>
        /// 模板类型
        /// </summary>
        public string EmailType
        {
            set { _emailtype = value; }
            get { return _emailtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int EmailPriority
        {
            set { _emailpriority = value; }
            get { return _emailpriority; }
        }
        /// <summary>
        /// 模板标签
        /// </summary>
        public string TagDescription
        {
            set { _tagdescription = value; }
            get { return _tagdescription; }
        }
        /// <summary>
        /// 模板描述
        /// </summary>
        public string EmailDescription
        {
            set { _emaildescription = value; }
            get { return _emaildescription; }
        }
        /// <summary>
        /// 模板主题
        /// </summary>
        public string EmailSubject
        {
            set { _emailsubject = value; }
            get { return _emailsubject; }
        }
        /// <summary>
        /// 模板内容
        /// </summary>
        public string EmailBody
        {
            set { _emailbody = value; }
            get { return _emailbody; }
        }
        #endregion Model

	}
}

