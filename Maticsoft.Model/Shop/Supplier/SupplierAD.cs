/**  版本信息模板在安装目录下，可自行修改。
* SupplierAD.cs
*
* 功 能： N/A
* 类 名： SupplierAD
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/10/30 10:48:43   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Supplier
{
	/// <summary>
	/// SupplierAD:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SupplierAD
	{
		public SupplierAD()
		{}
		#region Model
		private int _advertisementid;
		private string _name;
		private int _positionid;
		private int _contenttype;
		private string _fileurl;
		private string _alternatetext;
		private string _navigateurl;
		private string _advhtml;
		private DateTime _createddate= DateTime.Now;
		private int _createduserid;
		private int _status;
		private int _sequence;
		private int _supplierid;
		/// <summary>
		/// 广告编号
		/// </summary>
		public int AdvertisementId
		{
			set{ _advertisementid=value;}
			get{return _advertisementid;}
		}
		/// <summary>
		/// 广告名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 广告位(模版中定义)
		/// </summary>
		public int PositionId
		{
			set{ _positionid=value;}
			get{return _positionid;}
		}
		/// <summary>
		/// 内容类型：0:文字 1:图片  2flash   3代码
		/// </summary>
		public int ContentType
		{
			set{ _contenttype=value;}
			get{return _contenttype;}
		}
		/// <summary>
		/// 图片地址: 0:图片类型   1:图片地址  2:flash类型  3:flash类型
		/// </summary>
		public string FileUrl
		{
			set{ _fileurl=value;}
			get{return _fileurl;}
		}
		/// <summary>
		/// 广告语(文字信息，图片类型的时候，是图片alt信息，文字类型的时候，就是广告的文字)
		/// </summary>
		public string AlternateText
		{
			set{ _alternatetext=value;}
			get{return _alternatetext;}
		}
		/// <summary>
		/// 链接:图片和文字的链接
		/// </summary>
		public string NavigateUrl
		{
			set{ _navigateurl=value;}
			get{return _navigateurl;}
		}
		/// <summary>
		/// 自定义代码
		/// </summary>
		public string AdvHtml
		{
			set{ _advhtml=value;}
			get{return _advhtml;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 创建用户Id
		/// </summary>
		public int CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 状态: 1 有效，0 无效  
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 顺序
		/// </summary>
		public int Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 供应商Id
		/// </summary>
		public int SupplierId
		{
			set{ _supplierid=value;}
			get{return _supplierid;}
		}
		#endregion Model

	}
}

