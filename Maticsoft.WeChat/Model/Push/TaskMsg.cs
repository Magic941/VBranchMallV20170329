/**  版本信息模板在安装目录下，可自行修改。
* TaskMsg.cs
*
* 功 能： N/A
* 类 名： TaskMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/7 17:58:09   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
namespace Maticsoft.WeChat.Model.Push
{
	/// <summary>
	/// TaskMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class TaskMsg
	{
		public TaskMsg()
		{}
        #region Model
        private int _taskid;
        private string _openid;
        private int _groupid = 0;
        private string _username = "“”";
        private string _msgtype;
        private DateTime _createddate;
        private string _title;
        private string _description;
        private string _mediaid;
        private string _voiceurl;
        private string _musicurl;
        private string _hqmusicurl;
        private int _articlecount;
        private DateTime _publishdate;
        /// <summary>
        /// 任务ID
        /// </summary>
        public int TaskId
        {
            set { _taskid = value; }
            get { return _taskid; }
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
        /// 用户分组
        /// </summary>
        public int GroupId
        {
            set { _groupid = value; }
            get { return _groupid; }
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
        /// 
        /// </summary>
        public string MsgType
        {
            set { _msgtype = value; }
            get { return _msgtype; }
        }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
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
        /// 媒体事件
        /// </summary>
        public string MediaId
        {
            set { _mediaid = value; }
            get { return _mediaid; }
        }
        /// <summary>
        /// 语音文件路径
        /// </summary>
        public string VoiceUrl
        {
            set { _voiceurl = value; }
            get { return _voiceurl; }
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
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime PublishDate
        {
            set { _publishdate = value; }
            get { return _publishdate; }
        }
        #endregion Model
        #region 扩展属性
        public List<Maticsoft.WeChat.Model.Core.MsgItem> MsgItems = new List<Maticsoft.WeChat.Model.Core.MsgItem>();
        #endregion 
    }
}

