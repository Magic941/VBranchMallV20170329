/**
* EntryForm.cs
*
* 功 能： 
* 类 名： EntryForm
*
* Ver    变更日期             负责人：  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// 报名表
	/// </summary>
	[Serializable]
	public partial class EntryForm
	{
		public EntryForm()
		{}
		#region Model
		private int _id;
		private string _username;
		private int? _age;
		private string _email;
		private string _telphone;
		private string _phone;
		private string _qq;
		private string _msn;
		private string _houseaddress;
		private string _companyaddress;
		private int? _regionid;
		private string _sex;
		private string _description;
		private string _remark;
		private int? _state;
        private int _applytype;

        private string _AppName;
        private int? _AppStatus;

        public int? AppStatus
        {
          get { return _AppStatus; }
          set { _AppStatus = value; }
        }

        public string AppName
        {
            get { return _AppName; }
            set { _AppName = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户姓名
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 年龄
		/// </summary>
		public int? Age
		{
			set{ _age=value;}
			get{return _age;}
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 固定电话
		/// </summary>
		public string TelPhone
		{
			set{ _telphone=value;}
			get{return _telphone;}
		}
		/// <summary>
		/// 手机号码
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// QQ
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// MSN
		/// </summary>
		public string MSN
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		/// <summary>
		/// 家庭地址
		/// </summary>
		public string HouseAddress
		{
			set{ _houseaddress=value;}
			get{return _houseaddress;}
		}
		/// <summary>
		/// 公司地址
		/// </summary>
		public string CompanyAddress
		{
			set{ _companyaddress=value;}
			get{return _companyaddress;}
		}
		/// <summary>
		/// 所在省份
		/// </summary>
		public int? RegionId
		{
			set{ _regionid=value;}
			get{return _regionid;}
		}
		/// <summary>
		/// 性别
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 状态 
		/// </summary>
		public int? State
		{
			set{ _state=value;}
			get{return _state;}
		}
        /// <summary>
        ///申请好邻店类型 现用作区分  0分销店 1服务店
        /// </summary>
        public int ApplyType
        {
            set { _applytype = value; }
            get { return _applytype; }
        }

      
    
		#endregion Model

	}
}

