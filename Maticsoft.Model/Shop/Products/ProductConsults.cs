/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductConsultations.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/08/24 17:49:44
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// 商品咨询类别表
	/// </summary>
	[Serializable]
	public partial class ProductConsults
	{
		public ProductConsults()
		{}
        #region Model
        private int _consultationid;
        private int _typeid = -1;
        private int _userid;
        private long _productid;
        private string _username;
        private string _useremail;
        private string _consultationtext;
        private DateTime _createddate;
        private DateTime? _replydate;
        private bool _isreply = false;
        private string _replytext;
        private int _replyuserid;
        private string _replyusername;
        private int _status;
        private int _recomend = 0;
        /// <summary>
        /// 咨询ID
        /// </summary>
        public int ConsultationId
        {
            set { _consultationid = value; }
            get { return _consultationid; }
        }
        /// <summary>
        /// 咨询类型
        /// </summary>
        public int TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 咨询人
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 咨询产品
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 咨询人姓名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 咨询人邮箱
        /// </summary>
        public string UserEmail
        {
            set { _useremail = value; }
            get { return _useremail; }
        }
        /// <summary>
        /// 咨询内容
        /// </summary>
        public string ConsultationText
        {
            set { _consultationtext = value; }
            get { return _consultationtext; }
        }
        /// <summary>
        /// 咨询时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 回复时间
        /// </summary>
        public DateTime? ReplyDate
        {
            set { _replydate = value; }
            get { return _replydate; }
        }
        /// <summary>
        /// 是否回复
        /// </summary>
        public bool IsReply
        {
            set { _isreply = value; }
            get { return _isreply; }
        }
        /// <summary>
        /// 回复内容
        /// </summary>
        public string ReplyText
        {
            set { _replytext = value; }
            get { return _replytext; }
        }
        /// <summary>
        /// 回复人
        /// </summary>
        public int ReplyUserId
        {
            set { _replyuserid = value; }
            get { return _replyuserid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ReplyUserName
        {
            set { _replyusername = value; }
            get { return _replyusername; }
        }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 推荐类型 0： 不推荐，1：推荐
        /// </summary>
        public int Recomend
        {
            set { _recomend = value; }
            get { return _recomend; }
        }
        #endregion Model

	}
}

