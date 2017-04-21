
using System.Web.Mvc;
using Maticsoft.Model.SysManage;
using System;

namespace Maticsoft.Web.Areas.Shop.Controllers
{
    /// <summary>
    /// Shop网站前台基类
    /// </summary>
    [ShopError]
    public class ShopControllerBaseUser : Maticsoft.Web.Controllers.ControllerBaseUser
    {
        //TODO: 性能损耗警告,每次访问页面都加载了以下数据 BEN ADD 2013-03-12
        public int FallDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallDataSize", ApplicationKeyType.SNS), 20);
        public int PostDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS), 15);
        public int CommentDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS), 5);
        public int FallInitDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS), 5);

        #region 覆盖父类的  ViewResult View 方法 用于ViewName动态判空
        protected new ViewResult View(string viewName, object model)
        {
            return !string.IsNullOrWhiteSpace(viewName) ? base.View(viewName, model) : View(model);
        }

        protected new ViewResult View(string viewName)
        {
            return !string.IsNullOrWhiteSpace(viewName) ? base.View(viewName) : View();
        }
        #endregion


        private readonly Func<ActionExecutingContext, ActionResult> _toLoginFunc;
        public ShopControllerBaseUser(Func<ActionExecutingContext, ActionResult> func = null)
        {
            if (func == null)
            {
                //Shop 区域默认登录
                func = context =>
                {
                    string rawurl = Request.RawUrl;
                    return RedirectToAction("Login", "Account",
                        new {area = "Shop", returnUrl = Server.UrlEncode(rawurl)});
                };
            }
            _toLoginFunc = func;
        }

        public override ActionResult RedirectToLogin(ActionExecutingContext filterContext)
        {
            return _toLoginFunc(filterContext);
        }
    }
}
