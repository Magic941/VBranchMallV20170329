/**
* SysMsg.cs
*
* 功 能： N/A
* 类 名： SysMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/2 14:55:58   N/A    初版
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
	/// SysMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysMsg
	{
		public SysMsg()
		{}
        #region Model
        private int _sysmsgid;
        private string _openid;
        private string _msgtype;
        private int _systype;
        private DateTime _createtime;
        private string _title;
        private string _picurl;
        private string _url;
        private string _description;
        private string _musicurl;
        private string _hqmusicurl;
        /// <summary>
        /// 
        /// </summary>
        public int SysMsgId
        {
            set { _sysmsgid = value; }
            get { return _sysmsgid; }
        }
        /// <summary>
        /// 开发者ID
        /// </summary>
        public string OpenId
        {
            set { _openid = value; }
            get { return _openid; }
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
        /// 系统消息类型  1：表是自动关注的消息  2：表示自动回复消息
        /// </summary>
        public int SysType
        {
            set { _systype = value; }
            get { return _systype; }
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
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。
        /// </summary>
        public string PicUrl
        {
            set { _picurl = value; }
            get { return _picurl; }
        }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
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
        #endregion Model

        #region 扩展属性
        public List<Model.Core.MsgItem> MsgItems = new List<MsgItem>();
    
        #endregion
	}
}

