/**
* Photos.cs
*
* 功 能： N/A
* 类 名： Photos
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:46   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
namespace Maticsoft.Model.SNS
{
	/// <summary>
	/// 照片
	/// </summary>
	[Serializable]
	public partial class Photos
	{
		public Photos()
		{}
		#region Model
        private int _photoid;
        private string _photoname;
        private string _photourl;
        private int _type=0;
        private string _description;
        private int _status = 1;
        private int _createduserid;
        private string _creatednickname;
        private DateTime _createddate = DateTime.Now;
        private int _categoryid=-1;
        private int _pvcount = 0;
        private string _thumbimageurl;
        private string _normalimageurl;
        private int _sequence = 0;
        private int _isrecomend = 0;
        private string _topcommentsid;
        private int _forwardedcount = 0;
        private int _commentcount = 0;
        private int _favouritecount = 0;
        private int? _ownerphotoid;
        private string _tags;
        private string _staticurl;
        private string _maplng;
        private string _maplat;
        private string _photoaddress;

        #region 扩展属性
        public List<Maticsoft.Model.SNS.Comments> commmentList = new List<Comments>();
        #endregion 
        /// <summary>
        /// 图片id
        /// </summary>
        public int PhotoID
        {
            set { _photoid = value; }
            get { return _photoid; }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        public string PhotoName
        {
            set { _photoname = value; }
            get { return _photoname; }
        }
        /// <summary>
        /// 正常的图片地址
        /// </summary>
        public string PhotoUrl
        {
            set { _photourl = value; }
            get { return _photourl; }
        }
        /// <summary>
        /// 0:晒货 1:搭配 2:网购实拍
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 0:未审核 1：已审核  2：审核未通过 3：分类未明确 4：分类已明确
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 用户id
        /// </summary>
        public int CreatedUserID
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
        /// <summary>
        /// 用户名称
        /// </summary>
        public string CreatedNickName
        {
            set { _creatednickname = value; }
            get { return _creatednickname; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// 点击量
        /// </summary>
        public int PVCount
        {
            set { _pvcount = value; }
            get { return _pvcount; }
        }
        /// <summary>
        /// 缩略小图
        /// </summary>
        public string ThumbImageUrl
        {
            set { _thumbimageurl = value; }
            get { return _thumbimageurl; }
        }
        /// <summary>
        /// 缩略正常图
        /// </summary>
        public string NormalImageUrl
        {
            set { _normalimageurl = value; }
            get { return _normalimageurl; }
        }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 是否推荐 1：推荐到首页 2：推荐到晒货频道的首页 3：推荐到搭配频道的首页
        /// </summary>
        public int IsRecomend
        {
            set { _isrecomend = value; }
            get { return _isrecomend; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TopCommentsId
        {
            set { _topcommentsid = value; }
            get { return _topcommentsid; }
        }
        /// <summary>
        /// 转发的数量
        /// </summary>
        public int ForwardedCount
        {
            set { _forwardedcount = value; }
            get { return _forwardedcount; }
        }
        /// <summary>
        /// 评论的个数
        /// </summary>
        public int CommentCount
        {
            set { _commentcount = value; }
            get { return _commentcount; }
        }
        /// <summary>
        /// 喜欢的次数
        /// </summary>
        public int FavouriteCount
        {
            set { _favouritecount = value; }
            get { return _favouritecount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OwnerPhotoId
        {
            set { _ownerphotoid = value; }
            get { return _ownerphotoid; }
        }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StaticUrl
        {
            set { _staticurl = value; }
            get { return _staticurl; }
        }
        /// <summary>
        /// 地图坐标 经度
        /// </summary>
        public string MapLng
        {
            set { _maplng = value; }
            get { return _maplng; }
        }
        /// <summary>
        /// 地图坐标 纬度
        /// </summary>
        public string MapLat
        {
            set { _maplat = value; }
            get { return _maplat; }
        }
        /// <summary>
        /// 图片拍摄地址
        /// </summary>
        public string PhotoAddress
        {
            set { _photoaddress = value; }
            get { return _photoaddress; }
        }
		#endregion Model

	}
}

