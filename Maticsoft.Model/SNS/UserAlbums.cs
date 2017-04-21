/**
* UserAlbums.cs
*
* 功 能： N/A
* 类 名： UserAlbums
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:15:01   N/A    初版
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
    /// 用户专辑
    /// </summary>
    [Serializable]
    public partial class UserAlbums
    {
        public UserAlbums()
        { }

        #region Model

        private int _albumid;
        private string _albumname;
        private string _description;
        private int? _covertargetid;
        private string _coverphotourl;
        private int? _covertargettype;
        private int _status = 1;
        private int _createduserid;
        private string _creatednickname;
        private int _photocount = 0;
        private int _pvcount = 0;
        private int _favouritecount = 0;
        private DateTime _createddate = DateTime.Now;
        private int? _commentscount = 0;
        private int _isrecommend = 0;
        private int _channelsequence = 0;
        private int _privacy = 0;
        private int _sequence = 0;
        private DateTime? _lastupdateddate = DateTime.Now;
        private string _tags;

        /// <summary>
        /// 主键
        /// </summary>
        public int AlbumID
        {
            set { _albumid = value; }
            get { return _albumid; }
        }

        /// <summary>
        /// 相册的名称
        /// </summary>
        public string AlbumName
        {
            set { _albumname = value; }
            get { return _albumname; }
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
        /// 封皮的图片id
        /// </summary>
        public int? CoverTargetID
        {
            set { _covertargetid = value; }
            get { return _covertargetid; }
        }

        /// <summary>
        /// 封面的url
        /// </summary>
        public string CoverPhotoUrl
        {
            set { _coverphotourl = value; }
            get { return _coverphotourl; }
        }

        /// <summary>
        /// 封面的类型(0:照片 1:商品)
        /// </summary>
        public int? CoverTargetType
        {
            set { _covertargettype = value; }
            get { return _covertargettype; }
        }

        /// <summary>
        ///  状态 0:不可用 1：可用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }

        /// <summary>
        /// 创建者的用户ID
        /// </summary>
        public int CreatedUserID
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }

        /// <summary>
        /// 创建者用户的姓名
        /// </summary>
        public string CreatedNickName
        {
            set { _creatednickname = value; }
            get { return _creatednickname; }
        }

        /// <summary>
        ///
        /// </summary>
        public int PhotoCount
        {
            set { _photocount = value; }
            get { return _photocount; }
        }

        /// <summary>
        /// 访问量
        /// </summary>
        public int PVCount
        {
            set { _pvcount = value; }
            get { return _pvcount; }
        }

        /// <summary>
        /// 喜欢的人数
        /// </summary>
        public int FavouriteCount
        {
            set { _favouritecount = value; }
            get { return _favouritecount; }
        }

        /// <summary>
        /// 创建的日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }

        /// <summary>
        ///
        /// </summary>
        public int? CommentsCount
        {
            set { _commentscount = value; }
            get { return _commentscount; }
        }

        /// <summary>
        /// 是否被管理员推荐
        /// </summary>
        public int IsRecommend
        {
            set { _isrecommend = value; }
            get { return _isrecommend; }
        }

        /// <summary>
        /// 在首页推荐的显示顺序(后加的)
        /// </summary>
        public int ChannelSequence
        {
            set { _channelsequence = value; }
            get { return _channelsequence; }
        }

        /// <summary>
        /// 隐私性 0:公开 1:对好友可见 2:自己可以见
        /// </summary>
        public int Privacy
        {
            set { _privacy = value; }
            get { return _privacy; }
        }

        /// <summary>
        /// 显示的顺序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }

        /// <summary>
        /// 最后的更新时间
        /// </summary>
        public DateTime? LastUpdatedDate
        {
            set { _lastupdateddate = value; }
            get { return _lastupdateddate; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }

        #endregion Model

        #region 扩展属性

        public int TypeId
        {
            set; get;
        }

        #endregion
    }
}