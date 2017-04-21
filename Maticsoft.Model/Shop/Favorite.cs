/**
* Favorite.cs
*
* 功 能： N/A
* 类 名： Favorite
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/6/23 14:47:11   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop
{
    /// <summary>
    /// Favorite:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Favorite
    {
        public Favorite()
        { }
        #region Model
        private int _favoriteid;
        private int _type = 0;
        private long _targetid;
        private int _userid;
        private string _tags;
        private string _remark;
        private DateTime _createddate = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        public int FavoriteId
        {
            set { _favoriteid = value; }
            get { return _favoriteid; }
        }
        /// <summary>
        /// 收藏类型:   1:商品  
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 产品ID
        /// </summary>
        public long TargetId
        {
            set { _targetid = value; }
            get { return _targetid; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 收藏标签
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        /// <summary>
        /// 收藏备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 收藏时间  默认值为当前日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        #endregion Model

    }
}

