/**
* UserInvite.cs
*
* 功 能： N/A
* 类 名： UserInvite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/9 13:49:11   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Members
{
    /// <summary>
    /// UserInvite:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserInvite
    {
        public UserInvite()
        { }
        #region Model
        private int _inviteid;
        private int _userid;
        private string _usernick;
        private int _inviteuserid;
        private string _invitenick;
        private bool _isrebate;
        private bool _isnew;
        private DateTime _createddate;
        private string _remark;
        private string _rebatedesc;
        /// <summary>
        /// 邀请ID
        /// </summary>
        public int InviteId
        {
            set { _inviteid = value; }
            get { return _inviteid; }
        }
        /// <summary>
        /// 用户ID（被邀请用户）
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 用户昵称（被邀请用户）
        /// </summary>
        public string UserNick
        {
            set { _usernick = value; }
            get { return _usernick; }
        }
        /// <summary>
        /// 邀请用户ID
        /// </summary>
        public int InviteUserId
        {
            set { _inviteuserid = value; }
            get { return _inviteuserid; }
        }
        /// <summary>
        /// 邀请用户昵称
        /// </summary>
        public string InviteNick
        {
            set { _invitenick = value; }
            get { return _invitenick; }
        }
        /// <summary>
        /// 是否已返利
        /// </summary>
        public bool IsRebate
        {
            set { _isrebate = value; }
            get { return _isrebate; }
        }
        /// <summary>
        /// 是否是新用户
        /// </summary>
        public bool IsNew
        {
            set { _isnew = value; }
            get { return _isnew; }
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
        /// 备注：奖励情况
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 返利情况
        /// </summary>
        public string RebateDesc
        {
            set { _rebatedesc = value; }
            get { return _rebatedesc; }
        }
        #endregion Model

    }
}

