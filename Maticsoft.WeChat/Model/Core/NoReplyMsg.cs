/**  版本信息模板在安装目录下，可自行修改。
* NoReplyMsg.cs
*
* 功 能： N/A
* 类 名： NoReplyMsg
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/23 17:18:18   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// NoReplyMsg:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class NoReplyMsg
	{
		public NoReplyMsg()
		{}
		#region Model
		private long _msgid;
		private string _openid;
		private string _username;
		private string _msgtype;
		private DateTime _createtime;
		private string _description;
		private string _location_x;
		private string _location_y;
		private int _scale;
		private string _picurl;
		private string _title;
		private string _url;
		private string _event;
		private string _eventkey;
		private int _status;
		/// <summary>
		/// 
		/// </summary>
		public long MsgId
		{
			set{ _msgid=value;}
			get{return _msgid;}
		}
		/// <summary>
		/// 开发者帐号 （微信那边获取的）
		/// </summary>
		public string OpenId
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 微信用户  OpenID
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 消息类型  （地理位置:location,文本消息:text,消息类型:image，链接信息：link，事件信息：event）
		/// </summary>
		public string MsgType
		{
			set{ _msgtype=value;}
			get{return _msgtype;}
		}
		/// <summary>
		/// 消息时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		/// <summary>
		/// 消息内容
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 地理位置纬度
		/// </summary>
		public string Location_X
		{
			set{ _location_x=value;}
			get{return _location_x;}
		}
		/// <summary>
		/// 地理位置经度
		/// </summary>
		public string Location_Y
		{
			set{ _location_y=value;}
			get{return _location_y;}
		}
		/// <summary>
		/// 地图缩放大小
		/// </summary>
		public int Scale
		{
			set{ _scale=value;}
			get{return _scale;}
		}
		/// <summary>
		/// 图片链接地址（图片消息才会使用）
		/// </summary>
		public string PicUrl
		{
			set{ _picurl=value;}
			get{return _picurl;}
		}
		/// <summary>
		/// 消息标题（链接消息才会使用）
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 消息链接 （链接消息才会使用）
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件)
		/// </summary>
		public string Event
		{
			set{ _event=value;}
			get{return _event;}
		}
		/// <summary>
		/// 事件KEY值，与自定义菜单接口中KEY值对应
		/// </summary>
		public string EventKey
		{
			set{ _eventkey=value;}
			get{return _eventkey;}
		}
		/// <summary>
		/// 处理状态 0：未处理  1：已处理
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

