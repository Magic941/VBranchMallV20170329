using System;
using System.Data;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.CMS.ContentClass
{
    public partial class Add : PageBaseAdmin
    {
        Maticsoft.BLL.CMS.ContentClass bll = new Maticsoft.BLL.CMS.ContentClass();
        protected override int Act_PageLoad { get { return 221; } }//CMS_栏目管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                BindClassTypeID();
                BindTree();
            }
        }

        #region 绑定菜单树

        public void BindClassTypeID()
        {
            Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
            DataSet ds = bll.GetAllList();
            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.dropClassTypeID.DataSource = ds;
                this.dropClassTypeID.DataTextField = "ClassTypeName";
                this.dropClassTypeID.DataValueField = "ClassTypeID";
                this.dropClassTypeID.DataBind();
            }
        }

        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem("无父级", "0"));

            DataSet ds=bll.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                DataTable dt = ds.Tables[0];

                //加载树
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    DataRow[] drs = dt.Select("ParentID= " + 0);
                    foreach (DataRow r in drs)
                    {
                        string nodeid = r["ClassID"].ToString();
                        string text = r["ClassName"].ToString();
                        string parentid = r["ParentID"].ToString();
                        //string permissionid = r["PermissionID"].ToString();
                        text = "╋" + text;
                        this.dropParentID.Items.Add(new ListItem(text, nodeid));
                        int sonparentid = int.Parse(nodeid);
                        string blank = "├";

                        BindNode(sonparentid, dt, blank);
                    }
                }
            }
            this.dropParentID.DataBind();

        }
        private void BindNode(int parentid, DataTable dt, string blank)
        {
            DataRow[] drs = dt.Select("ParentID= " + parentid);

            foreach (DataRow r in drs)
            {
                string nodeid = r["ClassID"].ToString();
                string text = r["ClassName"].ToString();
                text = blank + "『" + text + "』";
                this.dropParentID.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";
                BindNode(sonparentid, dt, blank2);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ParentId = 0;
            if (dropParentID.SelectedIndex > 0)
            {
                ParentId = Globals.SafeInt(dropParentID.SelectedValue, 0);
                Maticsoft.Model.CMS.ContentClass modelContentClass = bll.GetModel(ParentId);
                if (modelContentClass != null && !modelContentClass.AllowSubclass)
                {
                    MessageBox.ShowFailTip(this, Resources.CMS.CCErrorAddClass);
                    return;
                }
            }
            Maticsoft.Model.CMS.ContentClass model = new Maticsoft.Model.CMS.ContentClass();
            model.ClassName = Globals.HtmlEncode(txtClassName.Text);
            model.ClassIndex = txtClassIndex.Text;
            model.ParentId = ParentId;
            model.State = Globals.SafeInt(radlState.SelectedValue, 0);
            model.AllowSubclass = chkAllowSubclass.Checked;
            model.AllowAddContent = chkAllowAddContent.Checked;
            model.ImageUrl = this.HiddenField_ICOPath.Value;
            model.Description = Globals.HtmlEncode(txtDescription.Text);
            model.Keywords = Globals.HtmlEncode(txtKeywords.Text);
            model.ClassTypeID = Globals.SafeInt(dropClassTypeID.SelectedValue, 0);
            model.ClassModel = Globals.SafeInt(radlClassModel.SelectedValue, 0);
            model.PageModelName = Globals.HtmlEncode(txtPageModelName.Text);

            model.IndexChar = this.txtIndexChar.Text;

            model.CreatedUserID = CurrentUser.UserID;
            model.Remark = Globals.HtmlEncode(txtRemark.Text);
            if (bll.AddExt(model))
            {
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx");
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }

        } 
        #endregion

        #region 取消操作，返回列表页面
        /// <summary>
        /// 取消操作，返回列表页面
        /// </summary>
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx");
        } 
        #endregion
    }
}
