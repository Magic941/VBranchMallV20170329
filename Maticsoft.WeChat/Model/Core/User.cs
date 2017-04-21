/**
* User.cs
*
* 功 能： N/A
* 类 名： User
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:06:19   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class User
	{
		public User()
		{}
        #region Model
        private int _id;
        private int _userid = -1;
        private int _groupid = 0;
        private string _openid;
        private string _username;
        private string _nickname;
        private int _sex;
        private string _city;
        private string _province;
        private string _country;
        private string _language;
        private string _headimgurl;
        private DateTime _createtime;
        private int _status;
        private DateTime? _canceltime;
        private string _remark;
        private DateTime? _lastmsgtime;
        /// <summary>
        /// 流水ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 对应站内用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 分组名称 默认是未分组
        /// </summary>
        public int GroupId
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 开发者帐号 （微信那边获取的）
        /// </summary>
        public string OpenId
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 微信用户  OpenID
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 微信昵称
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 0: 未知 1：男 2：女
        /// </summary>
        public int Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string City
        {
            set { _city = value; }
            get { return _city; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Province
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country
        {
            set { _country = value; }
            get { return _country; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Language
        {
            set { _language = value; }
            get { return _language; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Headimgurl
        {
            set { _headimgurl = value; }
            get { return _headimgurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 状态 0：取消关注  1：关注
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CancelTime
        {
            set { _canceltime = value; }
            get { return _canceltime; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastMsgTime
        {
            set { _lastmsgtime = value; }
            get { return _lastmsgtime; }
        }
        #endregion Model

	}
}

