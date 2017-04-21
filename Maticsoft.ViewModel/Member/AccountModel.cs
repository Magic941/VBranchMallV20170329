using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Maticsoft.ViewModel.Member
{

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
        }
}
