/**  版本信息模板在安装目录下，可自行修改。
* ActivityInfo.cs
*
* 功 能： N/A
* 类 名： ActivityInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:16   N/A    初版
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
	/// ActivityInfo:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ActivityInfo
	{
		public ActivityInfo()
		{}
        #region Model
        private int _activityid;
        private string _openid;
        private string _prename;
        private string _name;
        private string _imageurl;
        private DateTime _startdate;
        private DateTime _enddate;
        private DateTime _createddate;
        private string _summary;
        private string _activitydesc;
        private int _createduserid;
        private int _type = 0;
        private int _status;
        private decimal _probability;
        private int _limittype;
        private int _usertotal;
        private int _eachcount;
        private int _daytotal;
        private bool _ispwd;
        private int _pwdlength;
        private string _remark;
        private int _awardtype = 0;
        /// <summary>
        /// 
        /// </summary>
        public int ActivityId
        {
            set { _activityid = value; }
            get { return _activityid; }
        }
        /// <summary>
        /// 公众号
        /// </summary>
        public string OpenId
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 生成SN码前缀
        /// </summary>
        public string PreName
        {
            set { _prename = value; }
            get { return _prename; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
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
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary
        {
            set { _summary = value; }
            get { return _summary; }
        }
        /// <summary>
        /// 活动说明
        /// </summary>
        public string ActivityDesc
        {
            set { _activitydesc = value; }
            get { return _activitydesc; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CreatedUserId
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
        /// <summary>
        /// 优惠券类型 0：刮刮卡  1：大转盘 2：砸金蛋
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
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
        /// 获奖几率
        /// </summary>
        public decimal Probability
        {
            set { _probability = value; }
            get { return _probability; }
        }
        /// <summary>
        /// 0 按活动限制 1：按天限制
        /// </summary>
        public int LimitType
        {
            set { _limittype = value; }
            get { return _limittype; }
        }
        /// <summary>
        /// 每个人 总共参与次数
        /// </summary>
        public int UserTotal
        {
            set { _usertotal = value; }
            get { return _usertotal; }
        }
        /// <summary>
        /// 每个人抽奖次数
        /// </summary>
        public int EachCount
        {
            set { _eachcount = value; }
            get { return _eachcount; }
        }
        /// <summary>
        /// 每天抽奖总数
        /// </summary>
        public int DayTotal
        {
            set { _daytotal = value; }
            get { return _daytotal; }
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
        /// 密码长度
        /// </summary>
        public int PwdLength
        {
            set { _pwdlength = value; }
            get { return _pwdlength; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        public int AwardType
        {
            get
            {
                return this._awardtype;
            }
            set
            {
                this._awardtype = value;
            }
        }
        #endregion Model

	}
}

