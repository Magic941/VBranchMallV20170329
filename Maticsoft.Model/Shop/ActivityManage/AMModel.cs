using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.ActivityManage
{
    [Serializable]
    public partial class AMModel
    {
        public AMModel() { }
        #region Model
        private int _amid;
        private int _amtype;
        private string _amname;
        private string _amlabel;
        private DateTime _amstartdate;
        private DateTime _amenddate;
        private int _amunit;
        private int _amfreeshipment;
        private int _amapplystyles;
        private int _amstatus;
        private string _amuserid;
        private DateTime _amcreatedate;

        /// <summary>
        /// 活动Id
        /// </summary>
        public int AMId
        {
            set { _amid = value; }
            get { return _amid; }
        }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int AMType
        {
            set { _amtype = value; }
            get { return _amtype; }
        }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string AMName 
        {
            set { _amname = value; }
            get { return _amname; }
        }
        /// <summary>
        /// 活动标签
        /// </summary>
        public string AMLabel
        {
            set { _amlabel = value; }
            get { return _amlabel; }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime AMStartDate
        {
            set { _amstartdate = value; }
            get { return _amstartdate; }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime AMEndDate
        {
            set { _amenddate = value; }
            get { return _amenddate; }
        }
        /// <summary>
        /// 规则单位 0为件 ，1为元
        /// </summary>
        public int AMUnit
        {
            set { _amunit = value; }
            get { return _amunit; }
        }
        /// <summary>
        /// 是否包邮 0是 1 否
        /// </summary>
        public int AMFreeShipment
        {
            set { _amfreeshipment = value; }
            get { return _amfreeshipment; }
        }
        /// <summary>
        /// 应用方式 0 单个商品，1 全场商品 ，2 单个商家
        /// </summary>
        public int AMApplyStyles
        {
            set { _amapplystyles = value; }
            get { return _amapplystyles; }
        }
        /// <summary>
        /// 是否启用 0 是 1 否
        /// </summary>
        public int AMStatus
        {
            set { _amstatus = value; }
            get { return _amstatus; }
        }
        /// <summary>
        /// 用户ID
        /// </summary>
        public string AMUserId
        {
            set { _amuserid = value; }
            get { return _amuserid; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime AMCreateDate
        {
            set { _amcreatedate = value; }
            get { return _amcreatedate; }
        }
        #endregion

    }
}
