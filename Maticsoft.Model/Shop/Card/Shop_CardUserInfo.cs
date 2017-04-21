/**  版本信息模板在安装目录下，可自行修改。
* Shop_CardUserInfo.cs
*
* 功 能： N/A
* 类 名： Shop_CardUserInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/8/14 15:06:32   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Maticsoft.Model
{
    /// <summary>
    /// Shop_CardUserInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Shop_CardUserInfo
    {
        public Shop_CardUserInfo()
        { }

        public virtual int Id { get; set; }

        /// <summary>
        /// 用户表中系统
        /// </summary>
        public virtual int UserId { get; set; }

        /// <summary>
        /// 用户表中的登录编号
        /// </summary>
        public string UserName { get; set; }     

        /// <summary>
        /// 会员卡号
        /// </summary>
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public virtual string TrueName { get; set; }

        /// <summary>
        /// 卡类型编号
        /// </summary>
        public virtual string CardTypeNo { get; set; }

        /// <summary>
        /// 卡类型名称
        /// </summary>
        public virtual string CardTypeName { get; set; }

        /// <summary>
        /// 会员姓名CardTypeNo
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 加密后的密码
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// 会员原始密码
        /// </summary>
        public virtual string PasswordOrigin { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public virtual int Sex { get; set; }

        public virtual string SexTxt { get {

            if (this.Sex == 1)
            {
                return "男";
            }
            else
            {
                return "女";
            }
        
        } }

        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string CardId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Moble { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        public virtual string Tel { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public virtual string CodeNo { get; set; }


        private DateTime _birthDay = Convert.ToDateTime("1900-01-01 00:00:00.000");

        /// <summary>
        /// 生日
        /// </summary>
        public virtual DateTime BirthDay
        {
            get
            {
                return _birthDay;

            }
            set
            {

                _birthDay = value;

            }
        }

        /// <summary>
        /// 是否结婚
        /// </summary>
        public virtual bool IsMarry { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public virtual string Job { get; set; }

        /// <summary>
        /// 告知书号
        /// </summary>
        public virtual string BookNo { get; set; }

        /// <summary>
        /// 紧急联系人
        /// </summary>
        public virtual string BackPerson { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public virtual int UserInfoStatus { get; set; }

        private DateTime _CREATEDATE = Convert.ToDateTime("1900-01-01 00:00:00.000");

        /// <summary>
        /// 创建日期
        /// </summary>
        public virtual DateTime CREATEDATE
        {
            get
            {
                return _CREATEDATE;

            }
            set
            {

                _CREATEDATE = value;

            }
        }

        /// <summary>
        /// 创建者
        /// </summary>
        public virtual string CREATER { get; set; }

        private DateTime _MODIFYDATE = Convert.ToDateTime("1900-01-01 00:00:00.000");

        /// <summary>
        /// 更新日期
        /// </summary>
        public virtual DateTime MODIFYDATE
        {
            get
            {
                return _MODIFYDATE;

            }
            set
            {

                _MODIFYDATE = value;

            }
        }

        /// <summary>
        /// 更新者
        /// </summary>
        public virtual string MODIFYER { get; set; }

        /// <summary>
        /// 投保告知编号
        /// </summary>
        public virtual string InsureNo { get; set; }

        private DateTime _InsureActiveDate = Convert.ToDateTime("1900-01-01 00:00:00.000");

        /// <summary>
        /// 投保生效日期
        /// </summary>
        public virtual DateTime InsureActiveDate
        {
            get
            {
                return _InsureActiveDate;

            }
            set
            {

                _InsureActiveDate = value;

            }
        }

        /// <summary>
        /// 导出号
        /// </summary>
        public virtual string UserInfoOutNo { get; set; }

        private DateTime _OutDate = Convert.ToDateTime("1900-01-01 00:00:00.000");

        /// <summary>
        /// 导出日期
        /// </summary>
        public virtual DateTime OutDate
        {
            get
            {
                return _OutDate;

            }
            set
            {

                _OutDate = value;

            }
        }

        /// <summary>
        /// 紧急手机号
        /// </summary>
        public virtual string BakPersonMoble { get; set; }

        /// <summary>
        /// 导出日期
        /// </summary>
        public virtual string BakPersonNo { get; set; }

        /// <summary>
        /// 投保录入人
        /// </summary>
        public virtual string InsurePerson { get; set; }

        private DateTime _InsureDate = Convert.ToDateTime("1900-01-01 00:00:00.000");

        /// <summary>
        /// 投保录入日期
        /// </summary>
        public virtual DateTime InsureDate
        {
            get
            {
                return _InsureDate;

            }
            set
            {

                _InsureDate = value;

            }
        }

        private DateTime _ActiveDate = Convert.ToDateTime("1900-01-01 00:00:00.000");

        /// <summary>
        /// 激活日期
        /// </summary>
        public virtual DateTime ActiveDate
        {
            get
            {
                return _ActiveDate;

            }
            set
            {

                _ActiveDate = value;

            }
        }

        /// <summary>
        /// 是否默认卡
        /// </summary>
        public virtual string IsMainCard { get; set; }

        /// <summary>
        /// 卡的系统编号
        /// </summary>
        public virtual int CardSysId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string NameOne { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string NameOneCardId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RelationshipOne { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string NameTwo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string NameTwoCardId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual string RelationshipTwo { get; set; }



        public List<Insurants> CardInsurantList { get; set; }

        public string CardInsurants { get; set; }


        /// <summary>
        /// 保单文件地址
        /// </summary>
        public string InsureOrderFileUrl { get; set; }

        /// <summary>
        /// 保单文件名
        /// </summary>
        public string InsureOrderName { get; set; }

        /// <summary>
        /// 服务凭证内容 
        /// </summary>
        public string ServiceVoucherContent { get; set; }

        /// <summary>
        /// 服务凭证文件 
        /// </summary>
        public string ServiceVoucherFileUrl { get; set; }


        public string CardTypeAgreement { get; set; }

        /// <summary>
        /// 保单生效开始日期
        /// </summary>
        public DateTime InsureOrderStart { get; set; }

        /// <summary>
        /// 保单生效结束日期
        /// </summary>
        public DateTime InsureOrderEnd { get; set; }


    }
    /// <summary>
    /// 被保人信息
    /// </summary>
    [Serializable]
    public partial class Insurants
    {

        /// <summary>
        /// 会员卡号
        /// </summary>
        // [Required(ErrorMessage = "会员卡号不能为空")]
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 会员姓名CardTypeNo
        /// </summary>
        [Required(ErrorMessage = "被保人名字不能为空")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>        
        public virtual int Sex { get; set; }

        /// <summary>
        /// 证件号，身份证，出生证或出生日期
        /// </summary>
         [Required(ErrorMessage = "被保人证件号不能为空")]
        public virtual string CardId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [RegularExpression(@"^^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+", ErrorMessage = "邮件格式不正确!")]
        public virtual string Email { get; set; }

        /// <summary>
        /// 证件号类型 1=身份证 2=出生证 3=军人证
        /// </summary>
        public virtual int CertificateType { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required(ErrorMessage = "被保人生日不能为空")]
        public virtual DateTime BirthDay { get; set; }


        /// <summary>
        /// 关系 1=父亲 2=母亲 3.儿子 4.姑娘
        /// </summary>       
        public virtual int RelationShip { get; set; }

        public virtual string CertificateTypeTxt
        {

            get
            {
                var r = "";
                switch (this.CertificateType)
                {
                    case 1:
                        r = "身份证";
                        break;
                    case 2:
                        r = "出生证";
                        break;
                    case 3:
                        r = "军人证";
                        break;


                }
                return r;
            }

        }

        public string RelationShipTxt
        {
            get
            {
                var r = "";
                switch (this.RelationShip)
                {
                    case 1:
                        r = "父亲";
                        break;
                    case 2:
                        r = "母亲";
                        break;
                    case 3:
                        r = "儿子";
                        break;
                    case 4:
                        r = "姑娘";
                        break;
                    case 5:
                        r = "本人";
                        break;
                    case 6:
                        r = "配偶";
                        break;
                }
                return r;
            }
        }
        public string SexTxt
        {
            get
            {
                return this.Sex == 1 ? "男" : "女";

            }
        }

        /// <summary>
        /// 职业类型
        /// </summary>
        public string JobType
        { get; set; }

    }

   /// <summary>
   /// 激活的数据接收模型加验证，最后全转到正式传递模型上
   /// </summary>
    [Serializable]
    public class ShopCardUserInfo2
    {
        public string Name { get;set;}
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 第一投保人身份证号
        /// </summary>       
        [CardIdIsValid(18, 15, ErrorMessage = "投保人身份证格式不正确")]
        //[RegularExpression(@"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", ErrorMessage = "被保人身份证格式不正确!")]
        public string CardId { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [RegularExpression(@"^^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+", ErrorMessage = "投保人邮件格式不正确!")]
        public string Email { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [RegularExpression(@"^1[3|4|5|8|9|7][0-9]{9}$", ErrorMessage = "投保人手机格式不正确!")]
        [Required(ErrorMessage = "投保人手机号不能为空")]
        public string Moble { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [Required(ErrorMessage = "投保人地址不为空")]
        public string Address { get; set; }


        [Required(ErrorMessage = "投保人生日不能为空")]
        public DateTime BirthDay
        {
            get;
            set;
        }

        public List<Insurants> CardInsurants { get; set; }

        [Required(ErrorMessage = "最少有一位被保人")]
        public string CardInsurantsTxt { get; set; }
    }


    /// <summary>
    /// 激活卡基本信息, 从前端一直到卡服务端传递
    /// </summary>
   [Serializable]
    public class CardInfo {
        public string CardNo { get; set; }
        public string Password { get; set; }
    }

    /// <summary>
    /// 驾乘卡激活信息
    /// </summary>
     [Serializable]
    public partial class DriveCardInfoModel
    {     

        /// <summary>
        /// json格式的数据
        /// </summary>
        public virtual string CardsTxt { get; set; }

        /// <summary>
        /// 会员卡号
        /// </summary>
        public virtual List<CardInfo> Cards { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string EnterpriseName { get; set; }

        /// <summary>
        /// 企业证号
        /// </summary>
        public virtual string EnterpriseCode { get; set; }

        /// <summary>
        /// 1=个人申请 2=单位申请
        /// 不同类型
        /// </summary>
        public virtual int ApplicantType { get; set; }

        /// <summary>
        /// 激活用户名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string CardID { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public virtual string LPNumber { get; set; }

        /// <summary>
        ///车驾号
        /// </summary>
        public virtual string VINumber { get; set; }

        /// <summary>
        /// 车辆类型 可以用户激活手工填写，用以参考，大车，小车，轿车
        /// </summary>
        public virtual string CarType { get; set; }

        /// <summary>
        /// 座位号 
        /// </summary>
        public virtual int SeatsNumber { get; set; }

        /// <summary>
        ///销售员系统编号
        /// </summary>
        public virtual int SalesSysId { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public virtual DateTime CREATEDATE { get; set; }


        /// <summary>
        /// 激活日期
        /// </summary>
        public virtual DateTime ActivateDate { get; set; }

        /// <summary>
        /// 销售人员名称
        /// </summary>
        public virtual string SalesName { get; set; }

        /// <summary>
        /// 卡类型编号
        /// </summary>
        public virtual string CardTypeNo { get; set; }


        /// <summary>
        /// 卡批次号，方便查询
        /// </summary>
        public virtual string Batch { get; set; }

        /// <summary>
        /// 激活用户编号
        /// </summary>
        public virtual int ActivateUserID { get; set; }

        /// <summary>
        /// 激活用户名，就是商城方面的唯一编号
        /// </summary>
        public virtual string ActivateUserName { get; set; }
    }


    public class CardIdIsValidAttribute : ValidationAttribute
    {
        private readonly int MaxLength;
        private readonly int MinLength;

        public CardIdIsValidAttribute(int maxLength, int minLength)
            : base("{0}是无效的格式!")
        {
            MaxLength = maxLength;
            MinLength = minLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            /*长度较验*/
            string content = value.ToString();
            if (content.Length > MaxLength)
            {
                string errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            if (content.Length < MinLength)
            {
                string errorMessage = FormatErrorMessage(validationContext.DisplayName);
                return new ValidationResult(errorMessage);
            }

            string[] aCity = new string[] { null, null, null, null, null, null, null, null, null, null, null, "北京", "天津", "河北", "山西", "内蒙古", null, null, null, null, null, "辽宁", "吉林", "黑龙江", null, null, null, null, null, null, null, "上海", "江苏", "浙江", "安微", "福建", "江西", "山东", null, null, null, "河南", "湖北", "湖南", "广东", "广西", "海南", null, null, null, "重庆", "四川", "贵州", "云南", "西藏", null, null, null, null, null, null, "陕西", "甘肃", "青海", "宁夏", "新疆", null, null, null, null, null, "台湾", null, null, null, null, null, null, null, null, null, "香港", "澳门", null, null, null, null, null, null, null, null, "国外" };
            double iSum = 0;
            if (aCity[int.Parse(content.Substring(0, 2))] == null)
            {
                return new ValidationResult("身份证指示的地区不正确");
            }
            try
            {
                DateTime.Parse(content.Substring(6, 4) + "-" + content.Substring(10, 2) + "-" + content.Substring(12, 2));
            }
            catch
            {
                return new ValidationResult("身份证指示生日不正确");
            }
         

            return ValidationResult.Success;
           
        }
    }

    /// <summary>
    /// 驾乘卡视图
    /// </summary>
    public partial class V_DriveCardInfo
    {

        /// <summary>
        /// 会员卡号
        /// </summary>
        public virtual string CardNo { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string EnterpriseName { get; set; }

        /// <summary>
        /// 企业证号
        /// </summary>
        public virtual string EnterpriseCode { get; set; }

        /// <summary>
        /// 1=个人申请 2=单位申请
        /// </summary>
        public virtual int ApplicantType { get; set; }

        /// <summary>
        /// 激活用户名
        /// </summary>
        public virtual string Name { get; set; }


        public virtual string Password { get; set; }


        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string CardID { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public virtual string Mobile { get; set; }

        /// <summary>
        /// 车牌号
        /// </summary>
        public virtual string LPNumber { get; set; }

        /// <summary>
        ///车驾号
        /// </summary>
        public virtual string VINumber { get; set; }

        /// <summary>
        /// 车辆类型 可以用户激活手工填写，用以参考，大车，小车，轿车
        /// </summary>
        public virtual string CarType { get; set; }

        /// <summary>
        /// 座位号 
        /// </summary>
        public virtual int SeatsNumber { get; set; }

        /// <summary>
        ///销售员系统编号
        /// </summary>
        public virtual int SalesSysId { get; set; }

        /// <summary>
        ///创建时间
        /// </summary>
        public virtual DateTime CREATEDATE { get; set; }

        /// <summary>
        /// 保单号
        /// </summary>
        public virtual string InsureNo { get; set; }

        /// <summary>
        /// 导出信息编号
        /// </summary>
        public virtual string UserInfoOutNo { get; set; }

        /// <summary>
        /// 保单文件地址
        /// </summary>
        public virtual string InsureOrderFileUrl { get; set; }

        /// <summary>
        /// 保单文件名
        /// </summary>
        public virtual string InsureOrderName { get; set; }


        /// <summary>
        /// 激活日期
        /// </summary>
        public virtual DateTime ActivateDate { get; set; }

        /// <summary>
        /// 销售人员名称
        /// </summary>
        public virtual string SalesName { get; set; }

        /// <summary>
        /// 导出日期
        /// </summary>
        public virtual DateTime OutDate { get; set; }


        /// <summary>
        /// 卡类型编号
        /// </summary>
        public virtual string CardTypeNo { get; set; }

        /// <summary>
        /// 卡类型编号 1=意外卡单  2=驾乘卡
        /// </summary>
        public virtual int CardTypeNum { get; set; }

        /// <summary>
        /// 卡批次号，方便查询
        /// </summary>
        public virtual string Batch { get; set; }

        /// <summary>
        /// 服务凭证号内容
        /// </summary>
        public virtual string ServiceVoucherContent { get; set; }


        public virtual string CardTypeName { get; set; }

        public string CardTypeAgreement { get; set; }

        /// <summary>
        /// 保单生效开始日期
        /// </summary>
        public DateTime InsureOrderStart { get; set; }

        /// <summary>
        /// 保单生效结束日期
        /// </summary>
        public DateTime InsureOrderEnd { get; set; }
    }

}

