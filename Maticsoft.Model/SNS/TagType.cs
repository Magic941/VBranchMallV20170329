/**
* TagType.cs
*
* 功 能： N/A
* 类 名： TagType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:59   N/A    初版
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
    /// 标签的类型 如 材质,热门风格,品牌
    /// </summary>
    [Serializable]
    public partial class TagType
    {
        public TagType()
        { }
        #region Model
        private int _id;
        private string _typename;
        private string _remark;
        private int? _cid;
        private int? _status;
        /// <summary>
        /// id
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 此标签所属的商品的那一分类
        /// </summary>
        public int? Cid
        {
            set { _cid = value; }
            get { return _cid; }
        }
        /// <summary>
        /// 状态 0:不可用 1：可用
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        #endregion Model

    }
}


