/**  版本信息模板在安装目录下，可自行修改。
* Location.cs
*
* 功 能： N/A
* 类 名： Location
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/3/24 14:00:02   N/A    初版
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
	/// Location:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Location
	{
		public Location()
		{}
		#region Model
		private long _locationid;
		private string _openid;
		private string _username;
		private decimal _latitude;
		private decimal _longitude;
		private decimal _precision;
		private DateTime _createtime;
		/// <summary>
		/// 位置ID
		/// </summary>
		public long LocationId
		{
			set{ _locationid=value;}
			get{return _locationid;}
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
		/// 用户名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 纬度
		/// </summary>
		public decimal Latitude
		{
			set{ _latitude=value;}
			get{return _latitude;}
		}
		/// <summary>
		/// 经度
		/// </summary>
		public decimal Longitude
		{
			set{ _longitude=value;}
			get{return _longitude;}
		}
		/// <summary>
		/// 精度
		/// </summary>
		public decimal Precision
		{
			set{ _precision=value;}
			get{return _precision;}
		}
		/// <summary>
		/// 消息时间
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}
		#endregion Model

	}
}

