using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Controls;
using Maticsoft.Payment.BLL;
using Maticsoft.Payment.Configuration;

namespace Maticsoft.Web.Admin.TaoPayment
{
    public partial class PaymentModes : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 64; } } //系统管理_是否显示支付方式管理页面

        protected new int Act_DelData = 67;    //系统管理_支付方式管理_删除支付方式
        protected new int Act_UpdateData = 66;    //系统管理_支付方式管理_编辑支付方式
        protected new int Act_AddData = 65;    //系统管理_支付方式管理_添加支付方式

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.lkbDelectCheck.Click += new EventHandler(this.lkbDelectCheck_Click);
            this.grdPaymentMode.RowDataBound += new GridViewRowEventHandler(this.grdPaymentMode_RowDataBound);
            this.grdPaymentMode.RowDeleting += new GridViewDeleteEventHandler(this.grdPaymentMode_RowDeleting);
            this.grdPaymentMode.RowCommand += new GridViewCommandEventHandler(this.grdPaymentMode_RowCommand);
            if (!this.Page.IsPostBack)
            {
                //是否有添加支付方式的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }

                //是否有删除支付方式的权限
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    //liDele.Visible = false;
                }
                this.BindData();
                CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
            }
        }

        protected void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                grdPaymentMode.Columns[5].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                grdPaymentMode.Columns[6].Visible = false;
            }
            this.grdPaymentMode.DataSource = PaymentModeManage.GetPaymentModes(Maticsoft.Payment.Model.DriveEnum.ALL);
            this.grdPaymentMode.DataBind();
            CheckBoxColumn.RegisterClientCheckEvents(this.Page, this.Page.Form.ClientID);
        }

        protected void grdPaymentMode_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Sort")
            {
                int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
                string commandName = e.CommandName;
                if (commandName != null)
                {
                    if (!(commandName == "Fall"))
                    {
                        if (!(commandName == "Rise"))
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (rowIndex != this.grdPaymentMode.Rows.Count)
                        {
                            PaymentModeManage.DescPaymentMode((int)this.grdPaymentMode.DataKeys[rowIndex].Value);
                            this.BindData();
                        }
                        return;
                    }
                    if (rowIndex != 0)
                    {
                        PaymentModeManage.AscPaymentMode((int)this.grdPaymentMode.DataKeys[rowIndex].Value);
                        this.BindData();
                    }
                }
            }
        }

        protected void grdPaymentMode_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor='#EAFED7';this.style.cursor='pointer';");//#F4F4F4

                //当鼠标移走时还原该行的背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=currentcolor");
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                Label label = (Label)e.Row.FindControl("lblGatawayType");
                if (label != null)
                {
                    GatewayProvider provider = PayConfiguration.GetConfig().Providers[label.Text.ToLower()] as GatewayProvider;
                    if (provider != null)
                    {
                        label.Text = provider.DisplayName;
                    }
                }
            }
        }

        protected void grdPaymentMode_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (PaymentModeManage.DeletePaymentMode((int)this.grdPaymentMode.DataKeys[e.RowIndex].Value))
            {
                this.BindData();
                this.ShowMsg((string)HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_Message_Delete_Success"), true);
            }
            else
            {
                this.ShowMsg((string)HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_ErrorMessage_UnKownError"), false);
            }
        }

        protected void lkbDelectCheck_Click(object sender, EventArgs e)
        {
            int num = 0;
            foreach (GridViewRow row in this.grdPaymentMode.Rows)
            {
                CheckBox box = (CheckBox)row.FindControl("checkboxCol");
                if (box.Checked && PaymentModeManage.DeletePaymentMode(Convert.ToInt32(this.grdPaymentMode.DataKeys[row.RowIndex].Value, CultureInfo.InvariantCulture)))
                {
                    num++;
                }
            }
            if (num == 0)
            {
                this.ShowMsg((string)HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_ErrorMessage_No_Check"), false, true);
            }
            else
            {
                this.BindData();
                this.ShowMsg(string.Format(CultureInfo.InvariantCulture, (string)HttpContext.GetGlobalResourceObject("PaymentModes", "IDS_Message_Delete_Number"), new object[] { num }), true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPaymentMode.PageIndex = e.NewPageIndex;
            grdPaymentMode.OnBind();
        }


        protected virtual void ShowMsg(string msg, bool success)
        {
            this.ShowMsg(msg, success, false);
        }

        protected void ShowMsg(string msg, bool success, bool isWarning)
        {
            this.statusMessage.Success = success;
            this.statusMessage.IsWarning = isWarning;
            this.statusMessage.Text = msg;
            this.statusMessage.Visible = true;
        }
    }
}