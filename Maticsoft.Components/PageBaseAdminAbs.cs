using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Security;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.Components;

namespace Maticsoft.Web
{
    /// <summary>
    /// 页面基类,所有Admin页面继承该页面
    /// </summary>
    public abstract class PageBaseAdminAbs : System.Web.UI.Page
    {
        protected static IPageBaseOption PageBaseOption;
        protected static IPageBaseMessageTip PageBaseMessageTip;
        public PageBaseAdminAbs(IPageBaseOption option, IPageBaseMessageTip message)
        {
            PageBaseOption = option;
            PageBaseMessageTip = message;

            DefaultLoginAdmin = PageBaseOption.DefaultLoginAdmin;
            ToolTipDelete = PageBaseMessageTip.TooltipDelConfirm;
        }

        #region Method

        protected readonly string DefaultLoginAdmin;

        private static Hashtable ActHashtab;

        public string ToolTipDelete;

        #region 权限控制

        //子类通过 new 重写该值
        protected int Act_DeleteList = 1;   //批量删除按钮

        protected int Act_ShowInvalid = 2;  //查看失效数据
        protected int Act_CloseList = 3;    //批量关闭按钮
        protected int Act_OpenList = 8;     //批量打开按钮
        protected int Act_ApproveList = 4;  //批量审核按钮
        protected int Act_SetInvalid = 5;   //批量设为无效
        protected int Act_SetValid = 6;     //批量设为有效

        protected virtual int Act_PageLoad { get { return -1; } }//显示页面

        protected int Act_AddData = 15;       //添加数据
        protected int Act_UpdateData = -1;    //修改数据-单个
        protected int Act_DelData = -1;       //删除数据-单个

        //private int permissionid = -1;

        ///// <summary>
        ///// 页面访问需要的权限。可以在不同页面继承里来控制不同页面的权限。默认-1为无限制。
        ///// </summary>
        //public int PermissionID
        //{
        //    set
        //    {
        //        permissionid = value;
        //    }
        //    get
        //    {
        //        return permissionid;
        //    }
        //}

        private AccountsPrincipal userPrincipal;

        /// <summary>
        ///  权限角色验证对象
        /// </summary>
        public AccountsPrincipal UserPrincipal
        {
            get
            {
                return userPrincipal;
            }
        }

        private Maticsoft.Accounts.Bus.User currentUser;

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public Maticsoft.Accounts.Bus.User CurrentUser
        {
            get
            {
                return currentUser;
            }
        }

        #endregion 权限控制

        #region 初始化

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
            this.Error += new System.EventHandler(PageBase_Error);

            SingleLogin slogin = new SingleLogin();
            if (slogin.ValidateForceLogin())
            {
                Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipForceLogin + "');parent.location='" + DefaultLoginAdmin + "';</script>");
                Response.End();
            }

            Actions bllAction = new Actions();
            ActHashtab = bllAction.GetHashListByCache();
        }

        private void InitializeComponent()
        {
            //if (!Page.IsPostBack)
            {
                if (string.IsNullOrWhiteSpace(DefaultLoginAdmin))   //BEN ADD 2012-10-25 客户反馈, 登录跳转死循环BUG修复
                {
                    throw new ArgumentNullException("SA_Config_System - KEY [DefaultLoginAdmin] IS NULL!");
                }
                //DONE: 20121219单点登录功能恢复 BEN ADD
                if (!Context.User.Identity.IsAuthenticated) //|| Session[Globals.SESSIONKEY_ADMIN] == null)
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    Session.Abandon();
                    Response.Clear();
                    //Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipNoAuthenticated + "');parent.location='" + DefaultLoginAdmin + "';</script>");
                    Response.Write("<script defer>parent.location='" + DefaultLoginAdmin + "';</script>");
                    Response.End();
                    return;
                }

#if true       //DONE: 启用从Context.User.Identity.Name获取用户名进行自动登录 20121219单点登录功能恢复 BEN ADD
                userPrincipal = new AccountsPrincipal(Context.User.Identity.Name);

                if ((GetPermidByActID(Act_PageLoad) != -1) && (!userPrincipal.HasPermissionID(GetPermidByActID(Act_PageLoad))))
                {
                    Response.Clear();
                    Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipNoPermission + "');history.back();</script>");
                    Response.End();
                }

                if (Session[Globals.SESSIONKEY_ADMIN] == null)
                {
                    currentUser = new Maticsoft.Accounts.Bus.User(userPrincipal);
                    Session[Globals.SESSIONKEY_ADMIN] = currentUser;
                    Session["Style"] = currentUser.Style;

                    ////Response.Write("<script defer>location.reload();</script>");
                    ////跳转到 session 超时页面，提示用户重新登录

                    //Response.Clear();
                    //Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipSessionExpired + "');parent.location='" + defaullogin + "';</script>");
                    //Response.End();
                }
                else
                {
                    currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_ADMIN];
                    Session["Style"] = currentUser.Style;
                    if (currentUser.UserType != "AA")
                    {
                        FormsAuthentication.SignOut();
                        Session.Clear();
                        Session.Abandon();
                        Response.Clear();
                        Response.Write("<script defer>parent.location='" + DefaultLoginAdmin + "';</script>");
                        Response.End();
                        return;
                    }
                }
#else
                currentUser = (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_ADMIN];
                if (currentUser.UserType != "AA")
                {
                    FormsAuthentication.SignOut();
                    Session.Clear();
                    Session.Abandon();
                    Response.Clear();
                    Response.Write("<script defer>parent.location='" + DefaultLoginAdmin + "';</script>");
                    Response.End();
                    return;
                }
                Session["Style"] = currentUser.Style;

                userPrincipal = new AccountsPrincipal(currentUser.UserName);

                //if ((PermissionID != -1) && (!userPrincipal.HasPermissionID(PermissionID)))
                //{
                //    Response.Clear();
                //    Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipNoPermission + "');history.back();</script>");
                //    Response.End();
                //}

                if ((GetPermidByActID(Act_PageLoad) != -1) && (!userPrincipal.HasPermissionID(GetPermidByActID(Act_PageLoad))))
                {
                    Response.Clear();
                    Response.Write("<script defer>window.alert('" + PageBaseMessageTip.TooltipNoPermission + "');history.back();</script>");
                    Response.End();
                }
#endif
            }
        }

        #endregion 初始化

        #region 子类实现
        protected abstract void PageError(object sender, System.EventArgs e);
        #endregion

        #region 错误处理
        protected void PageBase_Error(object sender, System.EventArgs e)
        {
            PageError(sender, e);
        }
        #endregion 错误处理

        #region 公共方法

        /// <summary>
        /// 根据功能行为编号得到所属权限编号
        /// </summary>
        /// <returns></returns>
        public int GetPermidByActID(int ActionID)
        {
            if (ActHashtab == null)
            {
                return -1;
            }

            object obj = ActHashtab[ActionID.ToString()];
            if (obj != null && obj.ToString().Length > 0)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// 转换Bool类型的文本多语言描述
        /// </summary>
        /// <param name="bValid"></param>
        /// <returns></returns>
        public string GetboolText(object boolValue)
        {
            StringBuilder str = new StringBuilder();
            if (null != boolValue)
            {
                str.Append(boolValue.ToString().Trim().ToLower() == "true" ? "<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>" : "<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>");
            }
            return str.ToString();
        }

        #endregion 公共方法

        #region 字段数据转换

        /// <summary>
        /// 百分率转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string TranslateToPercent(string str)
        {
            decimal weight = Convert.ToDecimal(str);
            return weight.ToString("#0.##%", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// 审批状态
        /// </summary>
        /// <param name="Value">审核人</param>
        /// <returns></returns>
        public string GetApprovedText(object Value)
        {
            bool isApproved = false;
            if (Value != null && Value.ToString().Length > 0)
            {
                if (Convert.ToInt32(Value) > 0)
                {
                    isApproved = true;
                }
            }
            return isApproved ? "<span style=\"color: #006600;\">" + PageBaseMessageTip.lblTrue + "</span>" : "<span style=\"color: #800000;\">" + PageBaseMessageTip.lblFalse + "</span>";
        }

        #endregion 字段数据转换

        #region 导出excel

        /// <summary>
        /// HTML Table表格数据(html)导出EXCEL
        /// </summary>
        /// <param name="tabHead"></param>
        /// <param name="tabData"></param>
        protected void ExportToExcel(string tabHead, string tabData)
        {
            string sheetName = "sheetName";
            string fileName = "fileName";
            if (tabData != null)
            {
                StringWriter sw = new StringWriter();

                #region

                sw.WriteLine("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                sw.WriteLine("<head>");
                sw.WriteLine("<!--[if gte mso 9]>");
                sw.WriteLine("<xml>");
                sw.WriteLine(" <x:ExcelWorkbook>");
                sw.WriteLine("  <x:ExcelWorksheets>");
                sw.WriteLine("   <x:ExcelWorksheet>");
                sw.WriteLine("    <x:Name>" + sheetName + "</x:Name>");
                sw.WriteLine("    <x:WorksheetOptions>");
                sw.WriteLine("      <x:Print>");
                sw.WriteLine("       <x:ValidPrinterInfo />");
                sw.WriteLine("      </x:Print>");
                sw.WriteLine("    </x:WorksheetOptions>");
                sw.WriteLine("   </x:ExcelWorksheet>");
                sw.WriteLine("  </x:ExcelWorksheets>");
                sw.WriteLine("</x:ExcelWorkbook>");
                sw.WriteLine("</xml>");
                sw.WriteLine("<![endif]-->");
                sw.WriteLine("</head>");
                sw.WriteLine("<body>");

                #endregion 导出excel

                sw.WriteLine("<table>");
                sw.WriteLine("<tr style=\"background-color: #e4ecf7; text-align: center; font-weight: bold\">");
                sw.WriteLine(tabHead);
                sw.WriteLine("</tr>");
                sw.WriteLine(tabData);
                sw.WriteLine("</table>");
                sw.WriteLine("</body>");
                sw.WriteLine("</html>");
                sw.Close();
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "UTF-8";
                //Response.Charset = "GB2312";
                this.EnableViewState = false;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".xls");
                Response.ContentType = "application/ms-excel";
                Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                Response.Write(sw);
                Response.End();
            }
        }

        /// <summary>
        /// 将控件内容，导出EXCEL
        /// </summary>
        /// <param name="ctr"></param>
        public void ExportToExcel(System.Web.UI.Control ctr)
        {
            string style = @"<style> .text { mso-number-format:\@; } </script> ";
            Response.ClearContent();
            Response.Write("<meta   http-equiv=Content-Type   content=text/html;charset=utf-8>");
            Response.AddHeader("content-disposition", "attachment; filename=filename.xls");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/excel";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            ctr.RenderControl(htw);

            // Style is added dynamically
            Response.Write(style);
            Response.Write(sw.ToString());
            Response.End();
        }

        #endregion Method

        #endregion
    }
}