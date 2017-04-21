using System;

namespace Maticsoft.Web
{
    #region IPageBaseMessageTip
    public class PageBaseMessageTip : IPageBaseMessageTip
    {
        #region IPageBaseMessageTip 成员

        public string TooltipDelConfirm
        {
            get { return Resources.Site.TooltipDelConfirm; }
        }

        public string TooltipForceLogin
        {
            get { return Resources.Site.TooltipForceLogin; }
        }

        public string TooltipNoAuthenticated
        {
            get { return Resources.Site.TooltipNoAuthenticated; }
        }

        public string TooltipNoPermission
        {
            get { return Resources.Site.TooltipNoPermission; }
        }

        public string lblFalse
        {
            get { return Resources.Site.lblFalse; }
        }

        public string lblTrue
        {
            get { return Resources.Site.lblTrue; }
        }

        #endregion
    }
    #endregion

    #region IPageBaseOption
    public class PageBaseOption : IPageBaseOption
    {
        #region IPageBaseOption 成员

        public string DefaultLogin
        {
            get { return BLL.SysManage.ConfigSystem.GetValueByCache("DefaultLogin"); }
        }

        public string DefaultLoginAdmin
        {
            get { return BLL.SysManage.ConfigSystem.GetValueByCache("DefaultLoginAdmin"); }
        }

        public string DefaultLoginEnterprise
        {
            get { return BLL.SysManage.ConfigSystem.GetValueByCache("DefaultLoginEnterprise"); }
        }
        #endregion
    }
    #endregion

    /// <summary>
    /// 页面层(表示层)基类,所有前台页面继承，无权限验证
    /// </summary>
    public class PageBase : PageBaseAbs
    {
        public PageBase() : base(new PageBaseOption(), new PageBaseMessageTip()) { }

        #region 错误处理

        private string GetSqlExceptionMessage(int number)
        {
            //set default value which is the generic exception message
            string error = Resources.Site.ErrorMessageSQL;
            switch (number)
            {
                case 17:
                    // 	SQL Server does not exist or access denied.
                    error = Resources.Site.ErrorMessageSQL17;
                    break;
                case 547:
                    // ForeignKey Violation
                    error = Resources.Site.ErrorMessageSQL547;
                    break;
                case 4060:
                    // Invalid Database
                    error = Resources.Site.ErrorMessageSQL4060;
                    break;
                case 18456:
                    // Login Failed
                    error = Resources.Site.ErrorMessageSQL18456;
                    break;
                case 1205:
                    // DeadLock Victim     
                    error = Resources.Site.ErrorMessageSQL1205;
                    break;
                case 2627:
                    error = Resources.Site.ErrorMessageSQL2627;
                    break;
                case 2601:
                    // Unique Index/Constriant Violation
                    error = Resources.Site.ErrorMessageSQL2601;
                    break;
                default:
                    // throw a general DAL Exception
                    error = Resources.Site.ErrorMessageSQL;
                    break;
            }

            return error;
        }

        protected override void PageError(object sender, EventArgs e)
        {
            string errMsg = "";
            Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
            Exception currentError = Server.GetLastError();
            if (currentError is System.Data.SqlClient.SqlException)
            {
                System.Data.SqlClient.SqlException sqlerr = (System.Data.SqlClient.SqlException)currentError;
                if (sqlerr != null)
                {
                    string sqlMsg = GetSqlExceptionMessage(sqlerr.Number);
                    if (sqlerr.Number == 547)
                    {
                        errMsg += "<h1 class=\"SystemTip\">" + Resources.Site.ErrorSystemTip + "</h1><br/> " +
                        "<font class=\"ErrorPageText\">" + sqlMsg + "</font>";
                    }
                    else
                    {
                        errMsg += "<h1 class=\"ErrorMessage\">" + Resources.Site.ErrorSystemTip + "</h1><hr/> " +
                        "该信息已被系统记录，请稍后重试或与管理员联系。<br/>" +
                        "错误信息： <font class=\"ErrorPageText\">" + sqlMsg + "</font>";
                        model.Loginfo = sqlMsg;
                        model.StackTrace = currentError.ToString();
                        model.Url = Request.Url.AbsoluteUri;
                    }
                }
            }
            else
            {
                errMsg += "<h1 class=\"ErrorMessage\">" + Resources.Site.ErrorSystemTip + "</h1><hr/> " +
                    "该信息已被系统记录，请稍后重试或与管理员联系。<br/>" +
                    "错误信息： <font class=\"ErrorPageText\">" + currentError.Message.ToString() + "<hr/>" +
                    "<b>Stack Trace:</b><br/>" + currentError.ToString() + "</font>";

                model.Loginfo = currentError.Message;
                model.StackTrace = currentError.ToString();
                model.Url = Request.Url.AbsoluteUri;

            }
            Maticsoft.BLL.SysManage.ErrorLog.Add(model);
            
            //Session["ErrorMsg"] = errMsg;
            //Server.Transfer("~/ErrorPage.aspx", true);

            //考虑不Response当前页面，直接弹出信息提示。根据不同信息做不同的样式处理：系统提示，系统错误
            Response.Write(errMsg);
            Server.ClearError();
        }
        #endregion
    }
}