using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Maticsoft.ViewModel.CMS
{
    public class AccountModels
    {
    }
    public class LogOnModel
    {
        [Required(ErrorMessage = "请输入邮箱")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮件地址")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$",
            ErrorMessage = "请输入正确的邮箱格式")]
        public string Email { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住登录状态")]
        public bool RememberMe { get; set; }
    }
    public class RegisterModel
    {
        [Required(ErrorMessage = "昵称不能为空")]
        [Display(Name = "昵称")]
        [Remote("IsExistNickName", "Account", "CMS", ErrorMessage = "昵称已经被Ta人抢注, 换个试试")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "邮箱地址不能为空")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "电子邮件地址")]
        [RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$",
            ErrorMessage = "请输入正确的邮箱格式")]
        [Remote("IsExistEmail", "Account", "CMS", ErrorMessage = "该邮箱地址已经被注册过")]
        public string Email { get; set; }

        [Required(ErrorMessage = "密码不能为空")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配")]
        public string ConfirmPassword { get; set; }
    }
}