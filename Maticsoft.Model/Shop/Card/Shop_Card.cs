/**  版本信息模板在安装目录下，可自行修改。
* Shop_Card.cs
*
* 功 能： N/A
* 类 名： Shop_Card
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/6/28 16:15:20   lcy    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海真好邻电子商务有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// Shop_Card:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Shop_Card
    {
        public Shop_Card()
        { }
        #region Model
        private int _id;
        private string _batch;
        private string _cardno;
        private string _password;
        private int _salesid;
        private int _cardtypeid;
        private int? _number;
        private DateTime? _activatedate;
        private string _activateaccid;
        private string _activateaccno;
        private string _activateaccname;
        private DateTime? _unlockdate;
        private bool _isactivate;
        private bool _islock;
        private decimal? _value;
        private decimal? _invalue;
        private int? _outstatus;
        private int? _cardstatus;
        private string _cardoutno;
        private DateTime? _createdate;
        private string _creater;
        private DateTime? _modifydate;
        private string _modifyer;
        private string _cardtypeno;

        /// <summary>
        /// 关联销售人
        /// </summary>
        public string SalesName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Batch
        {
            set { _batch = value; }
            get { return _batch; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardNo
        {
            set { _cardno = value; }
            get { return _cardno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 未加密的密码，用户输入
        /// </summary>
        public string PasswordOrigin
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public int SalesId
        {
            set { _salesid = value; }
            get { return _salesid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int CardTypeId
        {
            set { _cardtypeid = value; }
            get { return _cardtypeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? Number
        {
            set { _number = value; }
            get { return _number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? ActivateDate
        {
            set { _activatedate = value; }
            get { return _activatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ActivateAccID
        {
            set { _activateaccid = value; }
            get { return _activateaccid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ActivateAccNo
        {
            set { _activateaccno = value; }
            get { return _activateaccno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ActivateAccName
        {
            set { _activateaccname = value; }
            get { return _activateaccname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? UnLockDate
        {
            set { _unlockdate = value; }
            get { return _unlockdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsActivate
        {
            set { _isactivate = value; }
            get { return _isactivate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsLock
        {
            set { _islock = value; }
            get { return _islock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Value
        {
            set { _value = value; }
            get { return _value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? InValue
        {
            set { _invalue = value; }
            get { return _invalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OutStatus
        {
            set { _outstatus = value; }
            get { return _outstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CardStatus
        {
            set { _cardstatus = value; }
            get { return _cardstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardOutNo
        {
            set { _cardoutno = value; }
            get { return _cardoutno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CREATEDATE
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATER
        {
            set { _creater = value; }
            get { return _creater; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? MODIFYDATE
        {
            set { _modifydate = value; }
            get { return _modifydate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MODIFYER
        {
            set { _modifyer = value; }
            get { return _modifyer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CardTypeNo
        {
            set { _cardtypeno = value; }
            get { return _cardtypeno; }
        }
        #endregion Model

        /// <summary>
        /// 卡的类型，随卡一起传递
        /// </summary>
        public Shop_CardType CardSelfType { get; set; }

    }
}

