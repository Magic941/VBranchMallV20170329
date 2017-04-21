using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class UserLog : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 73; } } //系统管理_是否显示参数管理

        protected new int Act_DeleteList = 74;    //系统管理_ 用户日志_批量删除用户日志
        protected new int Act_DelData = 75;    //系统管理_ 用户日志_删除指定日期之前用户日志

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
                txtDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist(); if (idlist.Trim().Length == 0) return;
            Maticsoft.BLL.SysManage.UserLog.Delete(idlist);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除用户日志", this);
            gridView.OnBind();
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            Maticsoft.BLL.SysManage.UserLog.Delete(Convert.ToDateTime(txtDate.Text));
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量删除【{0}】之前用户日志", txtDate.Text), this);
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();

            string strWhere = "";
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere = " OPInfo like '%" + txtKeyword.Text.Trim() + "%'";
            }
            ds = Maticsoft.BLL.SysManage.UserLog.GetList(strWhere);
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
            Maticsoft.BLL.SysManage.UserLog.Delete(ID);
            gridView.OnBind();
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

        #endregion gridView
    }
}