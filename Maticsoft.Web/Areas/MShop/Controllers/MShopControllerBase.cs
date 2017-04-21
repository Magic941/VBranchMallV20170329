using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Web.Areas.Shop.Controllers;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    /// <summary>
    /// Mobile网站前台基类
    /// </summary>
    [MShopError]
    public class MShopControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
        //TODO: 性能损耗警告,每次访问页面都加载了以下数据 BEN ADD 2013-03-12
        public int FallDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallDataSize", ApplicationKeyType.SNS), 20);
        public int PostDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS), 15);
        public int CommentDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS), 5);
        public int FallInitDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS), 5);
        //
        // GET: /Mobile/MobileControllerBase/
      
        #region UserName
        public string UserOpen
        {
            get
            {
                if (Session["WeChat_UserName"] != null)
                {
                    return Session["WeChat_UserName"].ToString();
                }
                return String.Empty;
            }
        }
        #endregion

       


        #region 获取最后推荐人 UserNameID 推荐人ID
        public string RecommendUserNameID
        {
            get
            {

                return Maticsoft.Common.Cookies.getCookie("Recommend_UserNameID", "Value");
            }

        }
        #endregion
        #region 获取用户ID
        public string UserNameID
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Maticsoft.Common.Cookies.getCookie("UserNameID", "Value")))
                {
                    Maticsoft.Common.Cookies.setCookie("UserNameID", "", 60 * 24 * 30);
                }
                if (CurrentUser != null)
                {
                    int userid = CurrentUser.UserID;
                    Maticsoft.Common.Cookies.updateCookies("UserNameID", userid.ToString(), 60 * 24 *30);
                }

                return Maticsoft.Common.Cookies.getCookie("UserNameID", "Value");
            }

        }
        #endregion

      


        #region  OpenId
        public string OpenId
        {
            get
            {
                if (Session["WeChat_OpenId"] != null)
                {
                    return Session["WeChat_OpenId"].ToString();
                }
                return String.Empty;
            }
        }
        #endregion

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

    }
}
