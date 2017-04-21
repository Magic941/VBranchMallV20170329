using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Maticsoft.ViewModel.Shop
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "用户名")]
        //[RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$",
        //    ErrorMessage = "请输入正确的邮箱格式")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住登录状态")]
        public bool RememberMe { get; set; }

       /// <summary>
       /// 来源 为空是是网站登录，1.卡激活时登录
       /// </summary>
        public string Source { get; set; }
    }

    [Serializable]
    public class RegisterModel
    {
        [Required(ErrorMessage = "昵称不能为空")]
        [Display(Name = "昵称")]
        [Remote("IsExistNickName", "Account", "Shop", ErrorMessage = "昵称已经被Ta人抢注, 换个试试")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "电子邮件地址")]
        //[RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$",
        //    ErrorMessage = "请输入正确的邮箱格式")]
        [Remote("IsExistUserName", "Account", "Shop", ErrorMessage = "该用户名已经被注册过")]
        public string UserName { get; set; }

        //[Required(ErrorMessage = "密码不能为空")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "手机号码")]
        [RegularExpression(@"^(1(([35][0-9])|(47)|[8][0126789]))\d{8}$",
        ErrorMessage = "请输入正确的手机号码")]
        public string Phone { get; set; }

            [Display(Name = "短信效验码")]
        public string SMSCode { get; set; }

        public string UserAgreement { get; set; }
    }
}
