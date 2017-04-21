using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.SysManage
{
    public partial class Treelist : PageBaseAdmin
    {
        Maticsoft.BLL.SysManage.SysTree bll = new Maticsoft.BLL.SysManage.SysTree();
        //int PermId_Modify = 81;
        //int PermId_Delete = 82;

        protected override int Act_PageLoad { get { return 52; } } //系统管理_是否显示后台菜单管理
        protected new int Act_AddData = 53;    //系统管理_后台菜单管理_增加菜单
        protected new int Act_UpdateData = 54;    //系统管理_后台菜单管理_编辑菜单
        protected new int Act_DelData = 55;    //系统管理_后台菜单管理_删除菜单
        protected int Act_SetPerData = 56;//系统管理_后台菜单管理_移动菜单
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Safe
                if (this.TreeType < 0)
                    return;
                if (TreeType == 0)
                {
                    //Act_PageLoad = 52;
                    Act_AddData = 53;
                    Act_UpdateData = 54;
                    Act_DelData = 55;
                    Act_SetPerData = 56;
                }
                else
                {
                    //Act_PageLoad = 68;
                    Act_AddData = 69;
                    Act_UpdateData = 70;
                    Act_DelData = 71;
                    Act_SetPerData = 72;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList)!=-1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData)!=-1)
                {
                    liAdd.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_SetPerData)) && GetPermidByActID(Act_SetPerData) != -1)
                {
                    listTarget2.Visible = false;
                    listTarget2.Visible = false;
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

                BiudTree(this.TreeType);

                if (!string.IsNullOrWhiteSpace(Request.Params["all"]))
                {
                    Session["strWhereTreelist"] = "";
                    Session["TreelistPageIndex"] = 0;
                }

                if (Session["TreelistPageIndex"] != null && Session["TreelistPageIndex"].ToString()!="")
                {
                    gridView.PageIndex = (int)Session["TreelistPageIndex"];
                }
                
            }
        }

        public int TreeType
        {
            get { return Globals.SafeInt(Request.Params["TreeType"], -1); }
        }

        

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            #region 记住搜索条件
            Session["strWhereTreelist"] = "";
            StringBuilder strSql = new StringBuilder();
            string connector = "";
            int ParentID = -1;
            if ((listTarget.SelectedItem != null) && (listTarget.SelectedIndex > 0))
            {
                ParentID = Convert.ToInt32(listTarget.SelectedValue);
                strSql.AppendFormat("parentid={0}", ParentID);
                connector = "and";
            }
            if (!String.IsNullOrWhiteSpace(ddlStatus.SelectedValue))
            {
                strSql.AppendFormat(" {0} Enabled='{1}'",connector, ddlStatus.SelectedValue);
                connector = "and";
            }
            if (txtKeyword.Text.Trim() != "")
            {
                strSql.AppendFormat(" {0} TreeText like '%{1}%'", connector, txtKeyword.Text.Trim());
            }
            if (strSql.Length > 0)
            {
                Session["strWhereTreelist"] = strSql.ToString();
            }
            //else
            //{
            //    Session.Remove("strWhereTreelist");
            //}
            #endregion

            gridView.OnBind();
        }


        #region 绑定菜单树
        private void BiudTree(int treeType)
        {

            Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
            DataTable dt;
            if (treeType > -1)
            {
                dt = sm.GetTreeList("TreeType = " + treeType).Tables[0];
            }
            else
            {
                dt = sm.GetTreeList("").Tables[0];
            }

            this.listTarget.Items.Clear();
            this.listTarget2.Items.Clear();
            //加载树
            this.listTarget.Items.Add(new ListItem(Resources.SysManage.listTargetOne, "-1"));
            this.listTarget.Items.Add(new ListItem(Resources.SysManage.listTargetTwo, "0"));
            this.listTarget2.Items.Add(new ListItem(Resources.SysManage.listTargetTwo, "0"));
            DataRow[] drs = dt.Select("ParentID= " + 0);


            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                string parentid = r["ParentID"].ToString();
                string permissionid = r["PermissionID"].ToString();
                text = "╋" + text;
                this.listTarget.Items.Add(new ListItem(text, nodeid));
                this.listTarget2.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank = "├";

                BindNode(sonparentid, dt, blank);

            }
            this.listTarget.DataBind();
            this.listTarget2.DataBind();
            this.listTarget2.Items.Insert(0,new ListItem("移动到...",""));

        }
        private void BindNode(int parentid, DataTable dt, string blank)
        {
            DataRow[] drs = dt.Select("ParentID= " + parentid);

            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["TreeText"].ToString();
                string permissionid = r["PermissionID"].ToString();
                text = blank + "『" + text + "』";

                DataRow[] tempdrs = dt.Select("ParentID= " + nodeid);
                if (tempdrs.Length > 0)
                {
                    this.listTarget.Items.Add(new ListItem(text, nodeid));
                }
                this.listTarget2.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";


                BindNode(sonparentid, dt, blank2);
            }
        }
        #endregion

        #region gridView

        public void BindData()
        {

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[6].Visible = false;
            }

            #region 使用记忆搜索条件
            string strWhere = "";
            Session["TreelistPageIndex"] = 0;
            if (Session["strWhereTreelist"] != null && Session["strWhereTreelist"].ToString() != "")
            {
                strWhere = " AND " + Session["strWhereTreelist"];
            }
            #endregion

            DataSet ds = new DataSet();            
            ds = bll.GetTreeList("TreeType=" + this.TreeType + strWhere);
            gridView.DataSetSource = ds;
            Session["TreelistPageIndex"] = gridView.PageIndex;
            
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
            }
        }


        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.DelTreeNode(ID);
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType,"删除菜单", this);
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Enabled")
            {
                if (e.CommandArgument != null)
                {
                    int nodeid = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    bll.UpdateEnabled(nodeid);
                        gridView.OnBind();
                    
                }
            }
           
        }
        #endregion


        #region btn

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
                    idlist += gridView.Rows[i].Cells[1].Text + ",";
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist(); if (idlist.Trim().Length == 0) return;
            bll.DelTreeNodes(idlist);

            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除菜单", this);
            gridView.OnBind();
        }

        protected void listTarget2_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist(); if (idlist.Trim().Length == 0) return;
            int ParentID = -1;
            if (!String.IsNullOrWhiteSpace(listTarget2.SelectedValue))
            {
                ParentID = Common.Globals.SafeInt(listTarget2.SelectedValue,0);
                bll.MoveNodes(idlist, ParentID);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量移动菜单", this);
            }

            gridView.OnBind();
        }

        #endregion


    }
}
