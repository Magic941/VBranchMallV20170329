/**  版本信息模板在安装目录下，可自行修改。
* OPLog.cs
*
* 功 能： N/A
* 类 名： OPLog
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/1/6 18:34:24   N/A    初版
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
	/// OPLog:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class OPLog
	{
		public OPLog()
		{}
		#region Model
		private long _id;
		private string _username;
		private string _openid;
		private string _address;
		private decimal? _longitude;
		private decimal? _latitude;
		private string _url;
		private int? _actionid;
		private int _optype;
		private DateTime _optime;
		private string _remark;
		/// <summary>
		/// 
		/// </summary>
		public long ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户微信号
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 公众号
		/// </summary>
		public string OpenId
		{
			set{ _openid=value;}
			get{return _openid;}
		}
		/// <summary>
		/// 用户地址
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 经度
		/// </summary>
		public decimal? Longitude
		{
			set{ _longitude=value;}
			get{return _longitude;}
		}
		/// <summary>
		/// 纬度
		/// </summary>
		public decimal? Latitude
		{
			set{ _latitude=value;}
			get{return _latitude;}
		}
		/// <summary>
		/// 用户浏览网页地址
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 功能指令ID
		/// </summary>
		public int? ActionId
		{
			set{ _actionid=value;}
			get{return _actionid;}
		}
		/// <summary>
		/// 0:浏览页面 1：点击菜单
		/// </summary>
		public int OPType
		{
			set{ _optype=value;}
			get{return _optype;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime OPTime
		{
			set{ _optime=value;}
			get{return _optime;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

