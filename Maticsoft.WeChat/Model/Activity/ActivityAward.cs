/**  版本信息模板在安装目录下，可自行修改。
* ActivityAward.cs
*
* 功 能： N/A
* 类 名： ActivityAward
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/25 19:04:14   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Activity
{
	/// <summary>
	/// ActivityAward:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ActivityAward
	{
		public ActivityAward()
		{}
        #region Model
        private int _awardid;
        private int _activityid;
        private string _awardname;
        private string _giftname;
        private string _imageurl;
        private string _thumbimage;
        private int _count;
        private string _awarddesc;
        private string _remark;
        private int _targetid = 0;
        /// <summary>
        /// 奖品ID
        /// </summary>
        public int AwardId
        {
            set { _awardid = value; }
            get { return _awardid; }
        }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int ActivityId
        {
            set { _activityid = value; }
            get { return _activityid; }
        }
        /// <summary>
        /// 奖品名称 比如：一等奖
        /// </summary>
        public string AwardName
        {
            set { _awardname = value; }
            get { return _awardname; }
        }
        /// <summary>
        /// 礼品名称
        /// </summary>
        public string GiftName
        {
            set { _giftname = value; }
            get { return _giftname; }
        }
        /// <summary>
        /// 礼品图片
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 礼品缩略图
        /// </summary>
        public string ThumbImage
        {
            set { _thumbimage = value; }
            get { return _thumbimage; }
        }
        /// <summary>
        /// 奖品数量
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 奖品描述
        /// </summary>
        public string AwardDesc
        {
            set { _awarddesc = value; }
            get { return _awarddesc; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }


        public int TargetId
        {
            get
            {
                return this._targetid;
            }
            set
            {
                this._targetid = value;
            }
        }
        #endregion Model

	}
}

