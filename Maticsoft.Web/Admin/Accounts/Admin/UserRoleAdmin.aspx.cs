using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class UserRoleAdmin : PageBaseAdmin
    {
        private UserType userTypeManage = new UserType();
        protected override int Act_PageLoad { get { return 202; } } //系统管理_用户角色分配管理_列表页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    liAdd.Visible = false;
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


                DropUserType.DataSource = userTypeManage.GetAllList();
                DropUserType.DataTextField = "Description";
                DropUserType.DataValueField = "UserType";
                DropUserType.DataBind();
                //DropUserType.Items.Insert(0, new ListItem("Resources.SysManage.ListItemAll", ""));

                //排除用户类别
                DropUserType.Items.Remove(DropUserType.Items.FindByValue("UU"));
                DropUserType.Items.Remove(DropUserType.Items.FindByValue("AA"));
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            string usertype = DropUserType.SelectedValue;
            string keyword = "";
            if (txtKeyword.Text.Trim() != "")
            {
                keyword = txtKeyword.Text.Trim();
            }
            User userAdmin = new User();
            if (usertype != "")
            {
                ds = userAdmin.GetUsersByType(usertype, keyword);
            }
            else
            {
                ds = userAdmin.GetUserList(keyword);
            }
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
                object obj1 = DataBinder.Eval(e.Row.DataItem, "EmployeeID");
                
                if ((obj1 != null) && ((obj1.ToString() != "")))
                {
                    string EmployeeID = obj1.ToString().Trim();
                    if (EmployeeID == "-1")
                    {
                        e.Row.Cells[4].Text = "";
                    }
                    else
                    {
                        e.Row.Cells[4].Text = EmployeeID;
                        //if (Maticsoft.Common.PageValidate.IsNumber(EmployeeID))
                        //{
                        //    e.Row.Cells[4].Text = GetEmpCode(int.Parse(EmployeeID));
                        //}
                    }
                }

            }
        }


        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            List<string> UserIDlist = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("AdminUserID"), ',', true);
            if (UserIDlist.Contains(ID))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.ErrorCannotDeleteID);
                return;
            }
            try
            {
                User User2 = new User(int.Parse(ID));
                User2.Delete();
                new BLL.Members.UsersExp().DeleteUsersExp(User2.UserID);
                gridView.OnBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.ErrorCannotDeleteUser);
                }
            }
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

    }
}
