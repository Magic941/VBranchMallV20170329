using System;
using System.Data;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.CMS.ContentClass
{
    public partial class Modify : PageBaseAdmin
    {
        Maticsoft.BLL.CMS.ContentClass bll = new Maticsoft.BLL.CMS.ContentClass();
        protected override int Act_PageLoad { get { return 222; } }//CMS_栏目管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                BindTree();
                BindClassTypeID();
                if (ClassID > 0)
                {
                    ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent, "List.aspx");
                }
            }
        }

        public int ClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.ContentClass model = bll.GetModel(ClassID);
            if (null != model)
            {
                this.lblClassID.Text = model.ClassID.ToString();
                this.txtClassName.Text = Globals.HtmlDecode(model.ClassName);
                this.txtClassIndex.Text = model.ClassIndex;
                this.txtOrders.Text = model.Sequence.ToString();
                if (model.ParentId.HasValue)
                {
                    this.dropParentID.SelectedValue = model.ParentId.ToString();
                }
                this.radlState.SelectedValue = model.State.ToString();
                this.chkAllowSubclass.Checked = model.AllowSubclass;
                this.chkAllowAddContent.Checked = model.AllowAddContent;
                this.HiddenField_ICOPath.Value = model.ImageUrl;
                this.imgUrl.ImageUrl = model.ImageUrl;
                this.txtDescription.Text = Globals.HtmlDecode(model.Description);
                this.txtKeywords.Text = Globals.HtmlDecode(model.Keywords);
                this.dropClassTypeID.SelectedValue = model.ClassTypeID.ToString();
                this.radlClassModel.SelectedValue = model.ClassModel.ToString();
                this.txtPageModelName.Text = Globals.HtmlDecode(model.PageModelName);
                this.txtRemark.Text = Globals.HtmlDecode(model.Remark);
                this.txtIndexChar.Text = Globals.HtmlDecode(model.IndexChar);
                this.txtSequence.Text = model.Sequence.ToString();
            }
        }

        #region 绑定菜单树

        public void BindClassTypeID()
        {
            Maticsoft.BLL.CMS.ClassType bllClassType = new Maticsoft.BLL.CMS.ClassType();
            DataSet ds = bllClassType.GetAllList();
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
            this.dropParentID.Items.Add(new ListItem(Resources.CMS.CCTooltipNoParent, "0"));

            DataSet ds = bll.GetTreeList("");
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

                        if (nodeid == Request.Params["id"].ToLower())
                        {
                            continue;
                        }

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

                if (nodeid == Request.Params["id"])
                {
                    continue;
                }

                this.dropParentID.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";
                BindNode(sonparentid, dt, blank2);
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ParentId = 0;

            if (string.IsNullOrWhiteSpace(this.txtClassName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, "请输入栏目名称!");
                return;
            }

            if (dropParentID.SelectedIndex > 0)
            {
                ParentId = int.Parse(dropParentID.SelectedValue);
                Maticsoft.Model.CMS.ContentClass modelContentClass = bll.GetModel(ParentId);
                if (modelContentClass != null && !modelContentClass.AllowSubclass)
                {
                    MessageBox.ShowFailTip(this, Resources.CMS.CCErrorAddClass);
                    return;
                }
            }
            if (String.IsNullOrWhiteSpace(txtSequence.Text))
            {
                MessageBox.ShowFailTip(this, "请填写顺序值！");
                return;
            }
            int sequence = Common.Globals.SafeInt(txtSequence.Text,-1);// txtSequence.Text
            if (sequence <= 0)
            {
                MessageBox.ShowFailTip(this, "请正确填写顺序值！");
                return;
            }
            Maticsoft.Model.CMS.ContentClass model = bll.GetModelByCache(ClassID);
            if (null != model)
            {
                model.ClassName =Globals.HtmlEncode(txtClassName.Text);
                model.ClassIndex = txtClassIndex.Text;
                model.Sequence = Globals.SafeInt(txtOrders.Text,0);
                model.ParentId = ParentId;
                model.State = int.Parse(radlState.SelectedValue);
                model.AllowSubclass = chkAllowSubclass.Checked;
                model.AllowAddContent = chkAllowAddContent.Checked;

                if (!string.IsNullOrWhiteSpace(HiddenField_ICOPath.Value))
                {
                    model.ImageUrl = HiddenField_ICOPath.Value;
                }
                else
                {
                    model.ImageUrl = this.HiddenField_ICOPath.Value;
                }

                model.Description = Globals.HtmlEncode(txtDescription.Text);
                model.Keywords = Globals.HtmlEncode(txtKeywords.Text);
                model.ClassTypeID = Globals.SafeInt(dropClassTypeID.SelectedValue,0);
                model.ClassModel = Globals.SafeInt(radlClassModel.SelectedValue,0);
                model.PageModelName = Globals.HtmlEncode(txtPageModelName.Text);
                model.CreatedDate = DateTime.Now;
                model.CreatedUserID = CurrentUser.UserID;
                model.IndexChar = Globals.HtmlEncode(this.txtIndexChar.Text);
                model.Remark = Globals.HtmlEncode(txtRemark.Text);
                model.Sequence = sequence;
                if (bll.Update(model))
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
