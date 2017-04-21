using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class UserAdmin : PageBaseAdmin
    {
        private UserType userTypeManage = new UserType();
        protected override int Act_PageLoad { get { return 28; } } //系统管理_是否显示用户管理
        protected new int Act_AddData = 16;    //用户管理_添加数据 
        protected new int Act_UpdateData = 29;    //系统管理_用户管理_编辑用户
        protected new int Act_DelData = 30;    //系统管理_用户管理_删除用户
        protected int Act_SetPerData = 27;//系统管理_用户管理_设置角色

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
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
                DropUserType.Items.Insert(0, new ListItem(Resources.SysManage.ListItemAll, ""));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_SetPerData)) && GetPermidByActID(Act_SetPerData) != -1)
            {
                gridView.Columns[1].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[7].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[8].Visible = false;
            }

            DataSet ds = new DataSet();
            string usertype = DropUserType.SelectedValue;
            string keyword = "";
            if (txtKeyword.Text.Trim() != "")
            {
                keyword = txtKeyword.Text.Trim();
            }
            //Maticsoft.BLL.Members.Users user = new BLL.Members.Users();
            //ds = user.GetListEXByType(usertype, keyword);
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
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                object obj2 = DataBinder.Eval(e.Row.DataItem, "UserID");
                if (obj2 != null)
                {
                    List<string> UserIDlist = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("AdminUserID"), ',', true);
                    if (UserIDlist.Contains(obj2.ToString()))
                    {
                        LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                        linkbtnDel.Visible = false;
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
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.ErrorCannotDeleteID);
                return;
            }
            try
            {
                User User2 = new User(int.Parse(ID));
                Maticsoft.BLL.Members.Users userBll=new BLL.Members.Users();
                userBll.DeleteEx(int.Parse(ID));

                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType,string.Format( "删除用户：【{0}】" , User2.UserName), this);
                Common.MessageBox.ShowSuccessTip(this, "删除成功！");
                gridView.OnBind();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                if (ex.Number == 547)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.ErrorCannotDeleteUser);
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

        public string GetTypeName(object userType)
        {
            if (userType != null && !String.IsNullOrEmpty(userType.ToString()))
            {
                return userTypeManage.GetDescriptionByCache(userType.ToString());
            }
            else
            {
                return "";
            }
 
        }
        #endregion gridView
    }
}