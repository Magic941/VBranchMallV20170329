using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Team
{
    /// <summary>
    /// Accounts:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Accounts
    {
        public Accounts()
        { }
        #region Model
        private long _id;
        private string _title;
        private string _salespersonmobile;
        private int _salessysid;
        private string _salesname;
        private decimal _totalcount = 0M;
        private decimal _deduct = 0M;
        private DateTime? _starttime;
        private DateTime? _endtime;
        private int? _accountsstate = 0;
        private int? _auditor;
        private DateTime _createtime = DateTime.Now;
        private string _remark;
        /// <summary>
        /// 结算编号
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 结算名称
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 营销员电话
        /// </summary>
        public string SalesPersonMobile
        {
            set { _salespersonmobile = value; }
            get { return _salespersonmobile; }
        }
        /// <summary>
        /// 营销员系统编号
        /// </summary>
        public int SalesSysID
        {
            set { _salessysid = value; }
            get { return _salessysid; }
        }
        /// <summary>
        /// 营销员系统名称
        /// </summary>
        public string SalesName
        {
            set { _salesname = value; }
            get { return _salesname; }
        }
        /// <summary>
        /// 营销员应得合计
        /// </summary>
        public decimal TotalCount
        {
            set { _totalcount = value; }
            get { return _totalcount; }
        }
        /// <summary>
        /// 营销员扣除合计
        /// </summary>
        public decimal Deduct
        {
            set { _deduct = value; }
            get { return _deduct; }
        }
        /// <summary>
        /// 结算开始日期
        /// </summary>
        public DateTime? StartTime
        {
            set { _starttime = value; }
            get { return _starttime; }
        }
        /// <summary>
        /// 结算截止日期
        /// </summary>
        public DateTime? EndTime
        {
            set { _endtime = value; }
            get { return _endtime; }
        }
        /// <summary>
        /// 结算状态：0：未审核，1：已审核，2：已结算（已完成）,4：废单
        /// </summary>
        public int? AccountsState
        {
            set { _accountsstate = value; }
            get { return _accountsstate; }
        }
        /// <summary>
        /// 审核人ID
        /// </summary>
        public int? Auditor
        {
            set { _auditor = value; }
            get { return _auditor; }
        }
        /// <summary>
        /// 结算单创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model
    }
}
