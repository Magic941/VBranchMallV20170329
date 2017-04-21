/**
* Groups.cs
*
* 功 能： N/A
* 类 名： Groups
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:41   N/A    初版
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
    /// 小组
    /// </summary>
    [Serializable]
    public partial class Groups
    {
        public Groups()
        { }
        #region Model
        private int _groupid;
        private string _groupname;
        private string _groupdescription;
        private int _groupusercount;
        private int _createduserid;
        private string _creatednickname;
        private DateTime _createddate = DateTime.Now;
        private string _grouplogo;
        private string _grouplogothumb;
        private string _groupbackground;
        private string _applygroupreason;
        private int _isrecommand;
        private int _topiccount;
        private int _topicreplycount;
        private int _status = 0;
        private int _sequence;
        private int _privacy;
        private string _tags;
        /// <summary>
        /// 小组的id
        /// </summary>
        public int GroupID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 小组的名称
        /// </summary>
        public string GroupName
        {
            set { _groupname = value; }
            get { return _groupname; }
        }
        /// <summary>
        /// 小组的简介
        /// </summary>
        public string GroupDescription
        {
            set { _groupdescription = value; }
            get { return _groupdescription; }
        }
        /// <summary>
        /// 小组成员的人数
        /// </summary>
        public int GroupUserCount
        {
            set { _groupusercount = value; }
            get { return _groupusercount; }
        }
        /// <summary>
        /// 创建者ID
        /// </summary>
        public int CreatedUserId
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedNickName
        {
            set { _creatednickname = value; }
            get { return _creatednickname; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 小组的logo
        /// </summary>
        public string GroupLogo
        {
            set { _grouplogo = value; }
            get { return _grouplogo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GroupLogoThumb
        {
            set { _grouplogothumb = value; }
            get { return _grouplogothumb; }
        }
        /// <summary>
        /// 小组的背景图片
        /// </summary>
        public string GroupBackground
        {
            set { _groupbackground = value; }
            get { return _groupbackground; }
        }
        /// <summary>
        /// 申请创建小组的理由
        /// </summary>
        public string ApplyGroupReason
        {
            set { _applygroupreason = value; }
            get { return _applygroupreason; }
        }
        /// <summary>
        /// 0 不推荐 1推荐到小组首页  2推荐为精选小组
        /// </summary>
        public int IsRecommand
        {
            set { _isrecommand = value; }
            get { return _isrecommand; }
        }
        /// <summary>
        /// 小组话题的个数()
        /// </summary>
        public int TopicCount
        {
            set { _topiccount = value; }
            get { return _topiccount; }
        }
        /// <summary>
        /// 小组一共有多少个回复(后加)
        /// </summary>
        public int TopicReplyCount
        {
            set { _topicreplycount = value; }
            get { return _topicreplycount; }
        }
        /// <summary>
        /// 状态(0：未审核 1：已经审核 2：审核未通过)
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 邀请链接才可加入
        /// </summary>
        /// <remarks>预留字段</remarks>
        public int Privacy
        {
            set { _privacy = value; }
            get { return _privacy; }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        #endregion Model

    }
}

