using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Team
{
    /// <summary>
    /// SalesPersonIncome:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>

    public class SalesPersonIncome
    {
        public SalesPersonIncome()
        { }
        #region Model
        private Int64 _id;
        private long _orderid;
        private string _ordercode;
        private string _mysalespersonmobile;
        private int _mysalessysid;
        private string _mysalesname;
        private int _incomestatus;
        private DateTime _ordercreatedate;
        private DateTime _createdate;
        private string _createperson;
        private DateTime? _auditdate;
        private DateTime? _closeincomedate;
        private int _incometype;
        private string _remark;
        private decimal _incomeratio;
        private string _auditperson;
        private string _closeincomeperson;
        private decimal _income;
        /// <summary>
        /// 
        /// </summary>
        public Int64 Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 订单ID
        /// </summary>
        public long OrderId
        {
            set { _orderid = value; }
            get { return _orderid; }
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode
        {
            set { _ordercode = value; }
            get { return _ordercode; }
        }
        /// <summary>
        /// 收入人的 营销员电话，做为结算依据
        /// </summary>
        public string MySalesPersonMobile
        {
            set { _mysalespersonmobile = value; }
            get { return _mysalespersonmobile; }
        }
        /// <summary>
        /// 收入营销员的系统编号
        /// </summary>
        public int MySalesSysID
        {
            set { _mysalessysid = value; }
            get { return _mysalessysid; }
        }
        /// <summary>
        /// 收入营销员的系统名称
        /// </summary>
        public string MySalesName
        {
            set { _mysalesname = value; }
            get { return _mysalesname; }
        }
        /// <summary>
        /// 收入的状态  0=未结算  1=结算审核中  2.结算打款处理中  3=结算完成 -2=结算作废,不给结算（可以让财务选择）
        /// </summary>
        public int InComeStatus
        {
            set { _incomestatus = value; }
            get { return _incomestatus; }
        }
        /// <summary>
        /// 订单创建时间，什么时间创建的就按什么时间来算。
        /// </summary>
        public DateTime OrderCreateDate
        {
            set { _ordercreatedate = value; }
            get { return _ordercreatedate; }
        }
        /// <summary>
        /// 结算单创建时间 系统自动创建
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 如果是拓展费用则会有指定的人
        /// </summary>
        public string CreatePerson
        {
            set { _createperson = value; }
            get { return _createperson; }
        }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? AuditDate
        {
            set { _auditdate = value; }
            get { return _auditdate; }
        }
        /// <summary>
        /// 结算完成时间
        /// </summary>
        public DateTime? CloseIncomeDate
        {
            set { _closeincomedate = value; }
            get { return _closeincomedate; }
        }
        /// <summary>
        /// 收入类型 0=粉丝收入 1=一级分销店收入 2=二级分销店收入 3=拓展费用 -1=退货
        /// </summary>
        public int IncomeType
        {
            set { _incometype = value; }
            get { return _incometype; }
        }
        /// <summary>
        /// 结算备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 结算费率
        /// </summary>
        public decimal IncomeRatio
        {
            set { _incomeratio = value; }
            get { return _incomeratio; }
        }
        /// <summary>
        /// 结算审核人，获取当前用户名，系统编号和人在一起
        /// </summary>
        public string AuditPerson
        {
            set { _auditperson = value; }
            get { return _auditperson; }
        }
        /// <summary>
        /// 结算完成人
        /// </summary>
        public string CloseIncomePerson
        {
            set { _closeincomeperson = value; }
            get { return _closeincomeperson; }
        }
        /// <summary>
        /// 收入额
        /// </summary>
        public decimal Income
        {
            set { _income = value; }
            get { return _income; }
        }
        #endregion Model

    }
}
