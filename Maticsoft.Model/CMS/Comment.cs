/**
* Comment.cs
*
* 功 能： N/A
* 类 名： Comment
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/1/30 18:33:35   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.CMS
{
	/// <summary>
	/// Comment:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Comment
	{
		public Comment()
		{}
        #region Model
        private int _id;
        private int? _contentid;
        private string _description;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private int _replycount;
        private int _parentid;
        private int _typeid;
        private bool _state;
        private bool _isread;
        private string _creatednickname;
        /// <summary>
        /// 编号
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 主题
        /// </summary>
        public int? ContentId
        {
            set { _contentid = value; }
            get { return _contentid; }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 发布人
        /// </summary>
        public int CreatedUserID
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
        /// <summary>
        /// 回复数
        /// </summary>
        public int ReplyCount
        {
            set { _replycount = value; }
            get { return _replycount; }
        }
        /// <summary>
        /// 父评论
        /// </summary>
        public int ParentID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 类型：1：图片评论；2：视频评论；3：内容评论；4：帖子评论
        /// </summary>
        public int TypeID
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 审核状态
        /// </summary>
        public bool State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 是否已读
        /// </summary>
        public bool IsRead
        {
            set { _isread = value; }
            get { return _isread; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CreatedNickName
        {
            set { _creatednickname = value; }
            get { return _creatednickname; }
        }
        #endregion Model

        private string _title;
        /// <summary>
        /// 文章标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

   
	}
}

