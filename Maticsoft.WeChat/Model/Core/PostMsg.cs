/**
* PostMsg.cs
*
* 功 能： N/A
* 类 名： PostMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:06:16   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;

namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// PostMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PostMsg
	{
		public PostMsg()
		{}
        #region Model
        private long _postmsgid;
        private int _ruleid = -1;
        private string _msgtype;
        private DateTime _createtime=DateTime.Now;
        private string _title;
        private string _description;
        private string _musicurl;
        private string _hqmusicurl;
        private int _articlecount;
        /// <summary>
        /// 
        /// </summary>
        public long PostMsgId
        {
            set { _postmsgid = value; }
            get { return _postmsgid; }
        }
        /// <summary>
        /// 对应自动规则Id
        /// </summary>
        public int RuleId
        {
            set { _ruleid = value; }
            get { return _ruleid; }
        }
        /// <summary>
        /// 消息类型  （文本消息:text,音乐:music，图文：news）
        /// </summary>
        public string MsgType
        {
            set { _msgtype = value; }
            get { return _msgtype; }
        }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 消息标题（链接消息才会使用）
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 音乐链接（消息类型为 music 时使用）
        /// </summary>
        public string MusicUrl
        {
            set { _musicurl = value; }
            get { return _musicurl; }
        }
        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐（消息类型为 music 时使用）
        /// </summary>
        public string HQMusicUrl
        {
            set { _hqmusicurl = value; }
            get { return _hqmusicurl; }
        }
        /// <summary>
        /// 图文消息个数，限制为10条以内 （消息类型为news使用）
        /// </summary>
        public int ArticleCount
        {
            set { _articlecount = value; }
            get { return _articlecount; }
        }
        #endregion Model

        #region 扩展属性

        public List<Model.Core.MsgItem> MsgItems = new List<MsgItem>();

        private string _userName;
        public string UserName
        {
            set { _userName = value; }
            get { return _userName; }
        }

        private string _openId;
        public string OpenId
        {
            set { _openId = value; }
            get { return _openId; }
        }

        private string _mediaId;
        public string MediaId
        {
            set { _mediaId = value; }
            get { return _mediaId; }
        }
	    #endregion

	}
}

