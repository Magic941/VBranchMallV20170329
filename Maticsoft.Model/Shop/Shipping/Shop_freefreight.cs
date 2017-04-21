/**  版本信息模板在安装目录下，可自行修改。
* Shop_freefreight.cs
*
* 功 能： N/A
* 类 名： Shop_freefreight
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/8/13 11:01:41   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.Shop.Shipping
{
    /// <summary>
    /// Shop_freefreight:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Shop_freefreight
    {
        public Shop_freefreight()
        { }
        #region Model
        private int _id;
        private int _regionid;
        private DateTime? _createdate;
        private int? _createrid;
        private decimal? _totalmoney;
        private DateTime? _startdate;
        private DateTime? _enddate;
        private long? _productid;
        private int? _quantity;
        private int? _freetype;
        private decimal _unitvalue; //单位数值
        private int _unit; //单位
        private int _fstatus;  //是否启用
        private string _allregion; //存储多个id
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? createdate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? createrid
        {
            set { _createrid = value; }
            get { return _createrid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? totalmoney
        {
            set { _totalmoney = value; }
            get { return _totalmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? StartDate
        {
            set { _startdate = value; }
            get { return _startdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate
        {
            set { _enddate = value; }
            get { return _enddate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long? ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Quantity
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? FreeType
        {
            set { _freetype = value; }
            get { return _freetype; }
        }

        /// <summary>
        /// 单位数值   
        /// </summary>
        public decimal UnitValue
        {
            set { _unitvalue = value; }
            get { return _unitvalue; }
        }
        /// <summary>
        /// 单位   元/件
        /// </summary>
        public int Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 开启状态，0 是  1 否
        /// </summary>
        public int FStatus
        {
            set { _fstatus = value; }
            get { return _fstatus; }
        }

        public string AllRegion
        {
            set { _allregion = value; }
            get { return _allregion; }
        }
        #endregion Model

        public string RegionName { get; set; }
        public string username { get; set; }
    }
}

