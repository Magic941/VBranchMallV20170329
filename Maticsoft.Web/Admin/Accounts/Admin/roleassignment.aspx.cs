using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts.Admin
{
    public partial class roleassignment : PageBaseAdmin
    {
        protected int Act_ShowReservedRole = 13; //查看系统保留角色
        List<string> ReservedRoleIDs = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("ReservedRoleIDs"),',',true);
        protected override int Act_PageLoad { get { return 198; } } //系统管理_用户管理_角色分配页面
        public void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {
                if ((Request.Params["UserID"] != null) && (Request.Params["UserID"]!=""))
                {                  
                    string userID = Request.Params["UserID"];
                    lblUserID.Text = userID;
                    User currentUser = new User(Convert.ToInt32(userID));                    
                    DataSet dsRole = AccountsTool.GetRoleList();
                    CheckBoxList1.DataSource = dsRole.Tables[0].DefaultView;
                    CheckBoxList1.DataTextField = "Description";
                    CheckBoxList1.DataValueField = "RoleID";
                    CheckBoxList1.DataBind();

                    AccountsPrincipal newUser = new AccountsPrincipal(currentUser.UserName);
                    if (newUser.Roles.Count > 0)
                    {
                        ArrayList roles = newUser.Roles;
                        for (int i = 0; i < roles.Count; i++)
                        {                            
                            foreach (ListItem item in CheckBoxList1.Items)
                            {
                                if (item.Text == roles[i].ToString())
                                {
                                    item.Selected = true;
                                }                                
                            }                            
                        }
                    }

                    
                    //移除保留角色
                    if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ShowReservedRole)))
                    {
                        for (int n = 0; n < CheckBoxList1.Items.Count; n++)
                        {
                            if (ReservedRoleIDs.Contains(CheckBoxList1.Items[n].Value))
                            {
                                CheckBoxList1.Items.Remove(CheckBoxList1.Items[n]);
                            }
                        }
                    }
                    
                                        
                }
               
            }
        }


        public void btnSave_Click(object sender, EventArgs e)
        {
            User currentUser = new User(Convert.ToInt32(lblUserID.Text));
            foreach (ListItem item in CheckBoxList1.Items)
            {
                if (item.Selected == true)
                {
                    currentUser.AddToRole(Convert.ToInt32(item.Value));
                }
                else
                {
                    currentUser.RemoveRole(Convert.ToInt32(item.Value));
                }
            }

            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("编辑用户【{0}】的权限", currentUser.UserName), this);
            Response.Redirect("UserAdmin.aspx?PageIndex=" + Request.Params["PageIndex"]);

        }

        public void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserAdmin.aspx?PageIndex=" + Request.Params["PageIndex"]);
        }
    }
}
