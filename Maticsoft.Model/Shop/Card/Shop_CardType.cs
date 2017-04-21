/**  版本信息模板在安装目录下，可自行修改。
* Shop_CardType.cs
*
* 功 能： N/A
* 类 名： Shop_CardType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/6/28 16:15:19   lcy    初版
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
    /// Shop_CardType:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Shop_CardType
    {
        
        #region Model
        private int _id;
        private string _typeno;
        private string _typename;
        private decimal? _value;
        private int? _activenumber;
        private int? _endyear;
        private int? _ageup;
        private int? _agedown;
        private bool _isonline;
        private bool _ispay;
        private decimal? _outvalue;
        private string _product;
        private string _describe;
        private int? _typestatus;
        private DateTime? _createdate;
        private string _creater;
        private DateTime? _modifydate;
        private string _modifyer;
        private bool _registertype;
        public int PersonNum { get; set; }
        public bool IsMorePerson { get; set; }
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
        public string TypeNo
        {
            set { _typeno = value; }
            get { return _typeno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
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
        public int? ActiveNumber
        {
            set { _activenumber = value; }
            get { return _activenumber; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? EndYear
        {
            set { _endyear = value; }
            get { return _endyear; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AgeUp
        {
            set { _ageup = value; }
            get { return _ageup; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AgeDown
        {
            set { _agedown = value; }
            get { return _agedown; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsOnline
        {
            set { _isonline = value; }
            get { return _isonline; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsPay
        {
            set { _ispay = value; }
            get { return _ispay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OutValue
        {
            set { _outvalue = value; }
            get { return _outvalue; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Product
        {
            set { _product = value; }
            get { return _product; }
        }
        /// <summary>
        /// 
        /// </summary>
        
        public string Agreement { get; set; }
        
        public string ActivatePrompt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Describe
        {
            set { _describe = value; }
            get { return _describe; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TypeStatus
        {
            set { _typestatus = value; }
            get { return _typestatus; }
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
        public bool RegisterType
        {
            set { _registertype = value; }
            get { return _registertype; }
        }
        #endregion Model

        /// <summary>
        /// 1=普通卡激活 2=驾乘卡激活 
        /// </summary>
        public int CardTypeNum
        {
            get;
            set;
        }


        public Shop_CardType()
        {
            this.Agreement = string.Empty;
            this.ActivatePrompt = string.Empty;
        }

        public string JobTypeInsuranceCompanyCode { set; get; }

        /*保险条款文档地址，直接以http存储*/
        public virtual string InsurantClauseFileName1 { get; set; }
        public virtual string InsurantClauseFileName2 { get; set; }
        public virtual string InsurantClauseFileName3 { get; set; }
        public virtual string InsurantClauseFileName4 { get; set; }

    }


}

