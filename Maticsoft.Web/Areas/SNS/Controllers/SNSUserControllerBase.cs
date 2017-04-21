using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    /// <summary>
    ///  SNS用户中心基类（需要权限验证和用户登录才能访问）
    /// </summary>
    [SNSError]
    public class SNSUserControllerBase : Maticsoft.Web.Controllers.ControllerBaseUser
    {
        /// <summary>
        /// 重写父类的登录跳转, 指向SNS登录
        /// </summary>
        public override ActionResult RedirectToLogin(ActionExecutingContext filterContext)
        {
            if (MvcApplication.MainAreaRoute == AreaRoute.SNS)
            {
                //SNS 主域
                return Redirect("/Account/Login");
            }
            return Redirect("/SNS/Account/Login");
        }

      

    }
}
