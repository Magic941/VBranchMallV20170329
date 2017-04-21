/**
* SupplierScoreDetails.cs
*
* 功 能： N/A
* 类 名： SupplierScoreDetails
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/8/26 17:31:51   Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Supplier
{
	/// <summary>
	/// 供应商(店铺)评分明细
	/// </summary>
	[Serializable]
	public partial class SupplierScoreDetails
	{
		public SupplierScoreDetails()
		{}
        #region Model
        private int _id;
        private decimal _score;
        private int _scoretype;
        private DateTime _createddate = DateTime.Now;
        private int _createduserid;
        private int _status = 0;
        private string _remark;
        private int _supplierid;
        /// <summary>
        /// 流水ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 评分值
        /// </summary>
        public decimal Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 评分类型: 1 描述相符评分 2:服务态度评分 3:发货速度评分
        /// </summary>
        public int ScoreType
        {
            set { _scoretype = value; }
            get { return _scoretype; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 创建用户
        /// </summary>
        public int CreatedUserId
        {
            set { _createduserid = value; }
            get { return _createduserid; }
        }
        /// <summary>
        /// 是否生效 0:未生效 1:生效
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
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
        /// 
        /// </summary>
        public int SupplierId
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        #endregion Model

	}
}

