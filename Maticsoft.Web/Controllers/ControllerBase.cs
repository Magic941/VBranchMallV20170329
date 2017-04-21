#define isFirstProgram
/**
* ControllerBase.cs
*
* 功 能： 页面层(表示层)基类,所有前台页面继承，无权限验证
* 类 名： ControllerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/23 14:36:01   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Controllers
{
    /// <summary>
    /// 页面层(表示层)基类,所有前台页面继承，无权限验证
    /// </summary>
    public abstract class ControllerBase : ControllerBaseAbs
    {
        #region 错误处理

        protected ControllerBase() : base(new PageBaseOption()) { }

        /// <summary>
        /// 错误处理
        /// </summary>
        protected override void ControllerException(ExceptionContext filterContext)
        {
            //前方已处理
            if (filterContext.ExceptionHandled)
            {
                
                 return;
            }

            Maticsoft.Services.ErrorLogTxt.GetInstance("激活卡查询基类异常处理").Write(filterContext.Exception.Message); 

            //// 标记异常已处理
            //filterContext.ExceptionHandled = true;

            //string errMsg = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">";
            //errMsg += string.Format("<title>系统发生错误 MaticsoftFK {0}</title>", MvcApplication.Version);
            //errMsg += "<style>body{	font-family: 'Microsoft Yahei', Verdana, arial, sans-serif;	font-size:14px;}a{text-decoration:none;color:#174B73;}a:hover{ text-decoration:none;color:#FF6600;}h2{	border-bottom:1px solid #DDD;	padding:8px 0;    font-size:25px;}.title{	margin:4px 0;	color:#F60;	font-weight:bold;}.message,#trace{	padding:1em;	border:solid 1px #000;	margin:10px 0;	background:#FFD;	line-height:150%;}.message{	background:#FFD;	color:#2E2E2E;		border:1px solid #E0E0E0;}#trace{	background:#E7F7FF;	border:1px solid #E0E0E0;	color:#535353;	word-wrap: break-word;}.notice{    padding:10px;	margin:5px;	color:#666;	background:#FCFCFC;	border:1px solid #E0E0E0;}.red{	color:red;	font-weight:bold;}</style></head>";

            //errMsg += "<body><div class=\"notice\"><h2>系统发生错误1 </h2>";
            //errMsg += "<div>您可以选择1 [ <a href=\"javascript:location.reload();\" >重试</a> ] [ <a href=\"javascript:history.back()\">返回</a> ] 或者 [ <a target=\"_blank\" href=\"http://bbs.maticsoft.com/\">去官方论坛找找答案</a> ]</div>";

            //Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
            //Exception currentError = filterContext.Exception;
            //HttpContextBase context = filterContext.HttpContext;
            //context.Response.Clear();

            //System.Diagnostics.StackTrace st = new System.Diagnostics.StackTrace(currentError, true);
            //int fileLineNumber = st.GetFrame(0).GetFileLineNumber();
            //int fileColumnNumber = st.GetFrame(0).GetFileColumnNumber();
            //string fileName = st.GetFrame(0).GetFileName();

            //errMsg += "<p><strong>错误位置:</strong>　File: <span class=\"red\">" + fileName + "</span>　Line: <span class=\"red\">" + fileLineNumber + "</span> Column: <span class=\"red\">" + fileColumnNumber + "</span></p>";
            //errMsg += "<p class=\"title\">[ 错误信息 ]</p>";
            ////
            //if (currentError is System.Data.SqlClient.SqlException)
            //{
            //    System.Data.SqlClient.SqlException sqlerr = (System.Data.SqlClient.SqlException)currentError;
            //    if (sqlerr != null)
            //    {
            //        string sqlMsg = GetSqlExceptionMessage(sqlerr.Number);
            //        if (sqlerr.Number == 547)
            //        {
            //            //errMsg += "<h1 class=\"SystemTip\">" + Resources.Site.ErrorSystemTip + "</h1><br/> " +
            //            //"<font class=\"ErrorPageText\">" + sqlMsg + "</font>";

            //            errMsg += "<p class=\"message\">" + sqlMsg + "</p>";
            //        }
            //        else
            //        {
            //            //errMsg += "<h1 class=\"ErrorMessage\">" + Resources.Site.ErrorSystemTip + "</h1><hr/> " +
            //            //"该信息已被系统记录，请稍后重试或与管理员联系。<br/>" +
            //            //"错误信息： <font class=\"ErrorPageText\">" + sqlMsg + "</font>";
            //            errMsg += "<p class=\"message\">" + sqlMsg + "</p>";
            //            model.Loginfo = sqlMsg;
            //            model.StackTrace = currentError.ToString();
            //            model.Url = context.Request.Url.AbsoluteUri;
            //        }
            //    }
            //}
            //else
            //{
            //    //errMsg += "<h1 class=\"ErrorMessage\">" + Resources.Site.ErrorSystemTip + "</h1><hr/> " +
            //    //    "该信息已被系统记录，请稍后重试或与管理员联系。<br/>" +
            //    //    "错误信息： <font class=\"ErrorPageText\">" + currentError.Message.ToString() + "<hr/>" +
            //    //    "<b>Stack Trace:</b><br/>" + currentError.ToString() + "</font>";
            //    errMsg += "<p class=\"message\">" + currentError.Message + "</p>";
            //    errMsg += "<p class=\"title\">[ StackTrace ]</p><p id=\"trace\">" + currentError.StackTrace + "</p></div>";
            //    errMsg +=
            //        string.Format(
            //            "<div align=\"center\" style=\"color:#FF3300;margin:5pt;font-family:Verdana\"> MaticsoftFK <sup style=\"color:gray;font-size:9pt\">{0}</sup>",
            //            MvcApplication.Version);
            //    errMsg += "<span style=\"color:silver\"> { Building &amp; OOP MVC Maticsoft Framework } -- [ WE CAN DO IT JUST HAPPY WORKING ]</span></div>";
            //    errMsg += "</body><style type=\"text/css\"></style></html>";

            //    model.Loginfo = currentError.Message;
            //    model.StackTrace = currentError.ToString();
            //    model.Url = context.Request.Url.AbsoluteUri;

            //}
            ////记录错误日志
            //Maticsoft.BLL.SysManage.ErrorLog.Add(model);

            ////拦截潜在危险值错误, 转换为404输出
            //if (!HttpContext.IsDebuggingEnabled &&
            //    currentError is HttpRequestValidationException)
            //{
            //    filterContext.Result = new HttpNotFoundResult();
            //    context.Server.ClearError();
            //    return;
            //}

            ////拦截MVC的FindView未找到错误, 转换为404输出
            //if (!HttpContext.IsDebuggingEnabled &&
            //    currentError.TargetSite.ToString().StartsWith("System.Web.Mvc.ViewEngineResult FindView"))
            //{
            //    filterContext.Result = new HttpNotFoundResult();
            //    context.Server.ClearError();
            //    return;
            //}
            //filterContext.Result = Content(errMsg);   //阻止之后的View错误输出, 只输出一次.
            //context.Server.ClearError();
        }

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

        #endregion
    }
}
