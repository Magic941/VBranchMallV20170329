/**  版本信息模板在安装目录下，可自行修改。
* GoodsType.cs
*
* 功 能： N/A
* 类 名： GoodsType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/11/25 10:14:27   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Products
{
	/// <summary>
	/// GoodsType:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class GoodsType
	{
		public GoodsType()
		{}
		#region Model
		private int _goodtypeid;
		private string _goodtypename;
		private int? _sort=0;
		private int? _pid=0;
        private int _IshasClass;
        private string _Path;
        private string _entryPicPath;
        private string _bannerPicPath;
        private string _bgColor;


        public int IshasClass {
            get {return _IshasClass;}
            set { _IshasClass = value; }
        }
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

		/// <summary>
		/// 
		/// </summary>
		public int GoodTypeID
		{
			set{ _goodtypeid=value;}
			get{return _goodtypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string GoodTypeName
		{
			set{ _goodtypename=value;}
			get{return _goodtypename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}

        public string EntryPicPath
        {
            set { _entryPicPath = value; }
            get { return _entryPicPath; }
        }

        public string BannerPicPath
        {
            set { _bannerPicPath = value; }
            get { return _bannerPicPath; }
        }

        public string BgColor
        {
            set { _bgColor = value; }
            get { return _bgColor; }
        }

		#endregion Model

	}
}

