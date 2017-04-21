/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：Advertisement.cs
// 文件功能描述：
// 
// 创建标识： [孙鹏]  2012/05/31 14:04:16
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Settings
{
	/// <summary>
	/// Advertisement:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Advertisement
	{
		public Advertisement()
		{}
		#region Model
		private int _advertisementid;
		private string _advertisementname;
		private int? _advpositionid;
		private int? _contenttype;
		private string _fileurl;
		private string _alternatetext;
		private string _navigateurl;
		private string _advhtml;
		private int? _impressions;
		private DateTime? _createddate;
		private int? _createduserid;
		private int? _state;
		private DateTime? _startdate;
		private DateTime? _enddate;
		private int? _daymaxpv=0;
		private int? _daymaxip=0;
		private decimal? _cpmprice=0M;
		private int? _autostop=-1;
		private int? _sequence;
		private int? _enterpriseid;
        private int _row;

        public int Row
        {
            get { return _row; }
            set { _row = value; }
        }
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
		public string AdvertisementName
		{
			set{ _advertisementname=value;}
			get{return _advertisementname;}
		}
		/// <summary>
		/// 广告位(外键)
		/// </summary>
		public int? AdvPositionId
		{
			set{ _advpositionid=value;}
			get{return _advpositionid;}
		}
		/// <summary>
		/// 内容类型：0:文字 1:图片  2flash   3代码
		/// </summary>
		public int? ContentType
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
		/// 显示频率(广告在当前广告位中的显示频率，数值越大显示的频率越高)
		/// </summary>
		public int? Impressions
		{
			set{ _impressions=value;}
			get{return _impressions;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? CreatedUserID
		{
			set{ _createduserid=value;}
			get{return _createduserid;}
		}
		/// <summary>
		/// 状态: 1 有效，0 无效 ，-1 欠费停止
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 起始时间(默认空，无限制)
		/// </summary>
		public DateTime? StartDate
		{
			set{ _startdate=value;}
			get{return _startdate;}
		}
		/// <summary>
		/// 结束时间(默认空，无限制)
		/// </summary>
		public DateTime? EndDate
		{
			set{ _enddate=value;}
			get{return _enddate;}
		}
		/// <summary>
		/// 最大PV(默认0 无限制)
		/// </summary>
		public int? DayMaxPV
		{
			set{ _daymaxpv=value;}
			get{return _daymaxpv;}
		}
		/// <summary>
		/// 最大IP(默认0   无限制)
		/// </summary>
		public int? DayMaxIP
		{
			set{ _daymaxip=value;}
			get{return _daymaxip;}
		}
		/// <summary>
		/// 每千次展示价格，默认0
		/// </summary>
		public decimal? CPMPrice
		{
			set{ _cpmprice=value;}
			get{return _cpmprice;}
		}
		/// <summary>
		/// 到最大数自动停止（费用到期自动停止）:1 , 0 , -1(无费用计算)
		/// </summary>
		public int? AutoStop
		{
			set{ _autostop=value;}
			get{return _autostop;}
		}
		/// <summary>
		/// 顺序
		/// </summary>
		public int? Sequence
		{
			set{ _sequence=value;}
			get{return _sequence;}
		}
		/// <summary>
		/// 广告主
		/// </summary>
		public int? EnterpriseID
		{
			set{ _enterpriseid=value;}
			get{return _enterpriseid;}
		}
		#endregion Model

	}
}

