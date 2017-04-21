/**
* GroupUsers.cs
*
* 功 能： N/A
* 类 名： GroupUsers
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:44   N/A    初版
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
    /// 小组人员
    /// </summary>
    [Serializable]
    public partial class GroupUsers
    {
        public GroupUsers()
        { }
        #region Model
        private int _groupid;
        private int _userid;
        private string _nickname;
        private DateTime _jointime;
        private int _role;
        private string _applyreason;
        private bool _isrecommend;
        private int _sequence;
        private int _status;

        /// <summary>
        /// 用户信息 需采用Cache加载
        /// </summary>
        public Model.Members.UsersExpModel User;

        /// <summary>
        /// 小组的id
        /// </summary>
        public int GroupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 加入的时间
        /// </summary>
        public DateTime JoinTime
        {
            set { _jointime = value; }
            get { return _jointime; }
        }
        /// <summary>
        /// 0 为普通的用户, 1为管理员, 2.超级管理员(组长), 3.提交申请..
        /// </summary>
        public int Role
        {
            set { _role = value; }
            get { return _role; }
        }
        /// <summary>
        /// 提交申请的理由(后加的)
        /// </summary>
        public string ApplyReason
        {
            set { _applyreason = value; }
            get { return _applyreason; }
        }
        /// <summary>
        /// 当role是小组长的时候,是否被管理员推荐的首页
        /// </summary>
        public bool IsRecommend
        {
            set { _isrecommend = value; }
            get { return _isrecommend; }
        }
        /// <summary>
        /// 推荐到首页后显示的顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 状态（0,未审核 1，已审核，3禁言）
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}

