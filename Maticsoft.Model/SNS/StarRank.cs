/**
* StarRank.cs
*
* 功 能： N/A
* 类 名： StarRank
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:56   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 达人排行
	/// </summary>
	[Serializable]
	public partial class StarRank
	{
		public StarRank()
		{}
        #region Model
        private int _id;
        private int _userid;
        private string _usergravatar;
        private int _typeid;
        private string _nickname;
        private bool _isrecommend;
        private int _sequence;
        private int _timeunit;
        private DateTime _startdate;
        private DateTime _enddate;
        private DateTime _createddate;
        private DateTime _rankdate;
        private int _status;
        private int _ranktype;
        /// <summary>
        /// 流水id
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 达人id
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 达人头像
        /// </summary>
        public string UserGravatar
        {
            set { _usergravatar = value; }
            get { return _usergravatar; }
        }
        /// <summary>
        /// 对应达人的类型
        /// </summary>
        public int TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 达人姓名
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 是否被管理员推荐到首页
        /// </summary>
        public bool IsRecommend
        {
            set { _isrecommend = value; }
            get { return _isrecommend; }
        }
        /// <summary>
        /// 显示的顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 0 :天 1:周 2:月 3:年
        /// </summary>
        public int TimeUnit
        {
            set { _timeunit = value; }
            get { return _timeunit; }
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
        /// 申请的日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 达人排行的日期
        /// </summary>
        public DateTime RankDate
        {
            set { _rankdate = value; }
            get { return _rankdate; }
        }
        /// <summary>
        ///  状态 0:不可用 1：可用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 0:总的排行 1：新晋排行
        /// </summary>
        public int RankType
        {
            set { _ranktype = value; }
            get { return _ranktype; }
        }
        #endregion Model

    }
}


