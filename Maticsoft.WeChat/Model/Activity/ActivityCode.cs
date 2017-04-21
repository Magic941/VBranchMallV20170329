/**  版本信息模板在安装目录下，可自行修改。
* ActivityCode.cs
*
* 功 能： N/A
* 类 名： ActivityCode
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:15   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Activity
{
	/// <summary>
	/// ActivityCode:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ActivityCode
	{
		public ActivityCode()
		{}
        #region Model
        private string _codename;
        private int _activityid;
        private int _awardid;
        private string _awardname;
        private string _activityname;
        private string _activitypwd;
        private string _userid;
        private string _username;
        private string _phone;
        private int _status;
        private bool _ispwd;
        private DateTime _startdate;
        private DateTime _enddate;
        private DateTime _generatedate;
        private DateTime? _useddate;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public string CodeName
        {
            set { _codename = value; }
            get { return _codename; }
        }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActivityId
        {
            set { _activityid = value; }
            get { return _activityid; }
        }
        /// <summary>
        /// 礼品ID
        /// </summary>
        public int AwardId
        {
            set { _awardid = value; }
            get { return _awardid; }
        }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string AwardName
        {
            set { _awardname = value; }
            get { return _awardname; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName
        {
            set { _activityname = value; }
            get { return _activityname; }
        }
        /// <summary>
        /// 活动密码
        /// </summary>
        public string ActivityPwd
        {
            set { _activitypwd = value; }
            get { return _activitypwd; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 微信用户号
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// 状态 0：表示未使用 1：表示已使用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 是否需要密码 0：不需要  1：需要
        /// </summary>
        public bool IsPwd
        {
            set { _ispwd = value; }
            get { return _ispwd; }
        }
        /// <summary>
        /// 开始时间  
        /// </summary>
        public DateTime StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 结束时间 
        /// </summary>
        public DateTime EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime GenerateDate
        {
            set { _generatedate = value; }
            get { return _generatedate; }
        }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? UsedDate
        {
            set { _useddate = value; }
            get { return _useddate; }
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

