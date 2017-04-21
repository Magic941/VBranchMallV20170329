using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.SysManage
{
    public partial class ErrorLog : PageBaseAdmin
    {
        Maticsoft.BLL.SysManage.ErrorLog bll = new Maticsoft.BLL.SysManage.ErrorLog();
        protected override int Act_PageLoad { get { return 76; } } //系统管理_是否显示错误日志页面

        protected new int Act_DeleteList = 77;    //系统管理_ 错误日志_批量删除错误日志
        protected new int Act_DelData = 78;    //系统管理_ 错误日志_删除指定日期之前错误日志

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    Button1.Visible = false;
                    txtDate.Visible = false;
                }
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
                this.txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {            
            Maticsoft.BLL.SysManage.ErrorLog.DeleteByDate(Globals.SafeDateTime((txtDate.Text),DateTime.Now));
            gridView.OnBind();
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量删除【{0}】之前的错误日志", txtDate.Text), this);
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist(); 
            if(idlist.Trim().Length == 0) return;
            Maticsoft.BLL.SysManage.ErrorLog.Delete(idlist);

            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除错误日志", this);
            gridView.OnBind();
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
        }


        #region gridView

        public void BindData()
        {
            #region
            if (!Context.User.Identity.IsAuthenticated)
            {
                return;
            }
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            //if (user.HasPermissionID(PermId_Modify))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}
            #endregion

            DataSet ds = new DataSet();
            string keyword = "";
            if (txtKeyword.Text.Trim() != "")
            {
                keyword = txtKeyword.Text.Trim();
            }

            ds = Maticsoft.BLL.SysManage.ErrorLog.GetList(-1, string.Format("Loginfo like '%{0}%'",InjectionFilter.SqlFilter(keyword)), "id desc");
            gridView.DataSetSource = ds;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
				
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
                //object obj2 = DataBinder.Eval(e.Row.DataItem, "Userid");
                //if ((obj2 != null) && ((obj2.ToString().Trim() != "")))
                //{
                //    int uid = Convert.ToInt32(obj2);
                //    if (uid != UserID)
                //    {
                //        e.Row.Cells[6].Text = "Modify";
                //        e.Row.Cells[7].Text = "Delete";
                //    }
                //}
                //object obj3 = DataBinder.Eval(e.Row.DataItem, "userid");
                //if ((obj3 != null) && ((obj3.ToString() != "")))
                //{
                //    int userid = Convert.ToInt32(obj3);
                //    Maticsoft.Accounts.Bus.User user = new User(userid);
                //    e.Row.Cells[5].Text = user.TrueName;
                //}
            }
        }


        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            Maticsoft.BLL.SysManage.ErrorLog.Delete(ID);
            gridView.OnBind();
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #endregion

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            Maticsoft.BLL.SysManage.ErrorLog.Delete(idlist);
            gridView.OnBind();
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
        }
    }
}
