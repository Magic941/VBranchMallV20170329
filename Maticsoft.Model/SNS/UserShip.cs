/**
* UserShip.cs
*
* 功 能： N/A
* 类 名： UserShip
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:05   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 用户的关注
	/// </summary>
	[Serializable]
	public partial class UserShip
	{
		public UserShip()
		{}
		#region Model
		private int _activeuserid;
		private int _passiveuserid;
		private int _state;
		private int _type=0;
		private int _categoryid;
		private DateTime _createddate= DateTime.Now;
		private bool _isread;
        private bool _isfellew=false;
        
        /// <summary>
		/// 主动建立关系者ID
		/// </summary>
		public bool  IsFellow
		{
            set { _isfellew = value; }
            get { return _isfellew; }
		}
		/// <
		/// <summary>
		/// 主动建立关系者ID
		/// </summary>
		public int ActiveUserID
		{
			set{ _activeuserid=value;}
			get{return _activeuserid;}
		}
		/// <summary>
		/// 被动建立关系者ID
		/// </summary>
		public int PassiveUserID
		{
			set{ _passiveuserid=value;}
			get{return _passiveuserid;}
		}
		/// <summary>
		///  ActiveUser 是否同意PassiveUser的请求状态  0 是未处置 1 是同意 2 是拒绝
		/// </summary>
		public int State
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 关系的类型（0为一般，1为互相关注）
		/// </summary>
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 所对应的类别
		/// </summary>
		public int CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 建立的时间
		/// </summary>
		public DateTime CreatedDate
		{
			set{ _createddate=value;}
			get{return _createddate;}
		}
		/// <summary>
		/// 新增的粉丝是不是已经看到了
		/// </summary>
		public bool IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		#endregion Model

        private string nickname;
        private int fanscount = 0;
        private string singature;
        private bool isBothway;

        public bool IsBothway
        {
            get { return isBothway; }
            set { isBothway = value; }
        }

        public string NickName
        {
            get { return nickname; }
            set { nickname = value; }
        }
        public int FansCount
        {
            get { return fanscount; }
            set { fanscount = value; }
        }
        public string Singature
        {
            get { return singature; }
            set { singature = value; }
        }
	}
}

