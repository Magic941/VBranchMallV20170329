using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.Content
{
    public partial class ListSimple : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        Maticsoft.BLL.CMS.Content bll = new BLL.CMS.Content();
        protected override int Act_PageLoad { get { return 227; } } //CMS_内容管理_列表页
        protected new int Act_AddData = 231;    //CMS_内容管理_添加数据
        protected new int Act_UpdateData = 232;    //CMS_内容管理_编辑数据
        protected new int Act_DelData = 233;    //CMS_内容管理_删除数据
 
        public string strClassID = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                    //btnBatch.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                BindTree();

                if (ClassID > 0)
                {
                    strClassID = "?classid=" + ClassID;
                    dropParentID.SelectedValue = ClassID.ToString();
                    dropParentID.Enabled = false;
                }

                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

            }
        }

        #region 栏目编号
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int ClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["classid"];
                if (!string.IsNullOrWhiteSpace(strid))
                {
                    id = Globals.SafeInt(strid, 0);
                }
                return id;
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region 批量操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                //清理文章缓存
                string[] arrcontid = idlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in arrcontid)
                {
                    DataCache.DeleteCache("ContentModelEx-" + item);
                    DataCache.DeleteCache("ContentModel-" + item);
                }
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        protected void Type_Changed(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            switch (this.dropType.SelectedValue)
            {
                case "1":
                    if (bll.UpdateList(idlist, 0))
                    {
                        strErr = Resources.Site.TooltipBatchUpdateOK;
                    }
                    break;
                case "2":
                    if (bll.UpdateList(idlist, 2))
                    {
                        strErr = Resources.Site.TooltipBatchUpdateOK;
                    }
                    break;
                case "3":
                    if (bll.UpdateList(idlist, 1))
                    {
                        strErr = Resources.Site.TooltipBatchUpdateOK;
                    }
                    break;
                case "4"://推荐文章
                    if (bll.SetRecList(idlist))
                    {
                        strErr = "批量设置推荐文章成功";
                    }
                    break;
                case "5"://热门文章
                    if (bll.SetHotList(idlist))
                    {
                        strErr = "批量设置热门文章成功";
                    }
                    break;
                case "6"://醒目文章
                    if (bll.SetColorList(idlist))
                    {
                        strErr = "批量设置醒目文章成功";
                    }
                    break;
                case "7"://置顶文章
                    if (bll.SetTopList(idlist))
                    {
                        strErr = "批量设置置顶文章成功";
                    }
                    break;
                default:
                    break;
            }
            if (strErr != "")
            {
                //清理文章缓存
                string[] arrcontid = idlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in arrcontid)
                {
                    DataCache.DeleteCache("ContentModelEx-" + item);
                    DataCache.DeleteCache("ContentModel-" + item);
                }
                MessageBox.ShowSuccessTip(this, strErr);
                gridView.OnBind();
            }
        }


        #endregion

        #region 绑定菜单树
        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem(Resources.Site.All, ""));
            Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();
            DataSet ds = bllContentClass.GetTreeList(" ClassModel=3 ");//频道列表
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
                //string permissionid = r["PermissionID"].ToString();
                text = blank + "『" + text + "』";
                this.dropParentID.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";
                BindNode(sonparentid, dt, blank2);
            }
        }
        #endregion

        #region gridView


        private string ContentStatus
        {
            get
            {
                string str = string.Empty;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    str = Request.Params["type"];
                }
                return str;
            }
        }

        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[11].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[12].Visible = false;
            }

            #endregion
            StringBuilder strWhere = new StringBuilder();
            //string state = this.DropState.SelectedValue;
            string classID = this.dropParentID.SelectedValue;
            //if (!string.IsNullOrWhiteSpace(this.DropState.SelectedValue) )  
            //{
            //    strWhere.AppendFormat(" state={0}", state);
            //}
            strWhere.Append(" ClassModel=3 " );
            if (!string.IsNullOrWhiteSpace(ContentStatus) && PageValidate.IsNumber(ContentStatus))
            {
                strWhere.AppendFormat(" and state={0}", ContentStatus);
            }

            if (!string.IsNullOrWhiteSpace(this.txtKeyword.Text.Trim()))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("Title like '%{0}%'", InjectionFilter.QuoteFilter(txtKeyword.Text.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(this.dropParentID.SelectedValue))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("ClassID = {0} ", classID);
            }

            if (ClassID > 0)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" ClassID={0} ", ClassID);
            }
            gridView.DataSetSource = bll.GetListByView(0, strWhere.ToString(), "ContentID desc");
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
            if (bll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
        }
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SetRec")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int contentId = Common.Globals.SafeInt(Args[0], 0);
                    bool Rec = Common.Globals.SafeBool(Args[1], false);
                    if (bll.SetRec(contentId, !Rec))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
            if (e.CommandName == "SetHot")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int contentId = Common.Globals.SafeInt(Args[0], 0);
                    bool Rec = Common.Globals.SafeBool(Args[1], false);
                    if (bll.SetHot(contentId, !Rec))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
            if (e.CommandName == "SetColor")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int contentId = Common.Globals.SafeInt(Args[0], 0);
                    bool Rec = Common.Globals.SafeBool(Args[1], false);
                    if (bll.SetColor(contentId, !Rec))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
                }
            }
            if (e.CommandName == "SetTop")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int contentId = Common.Globals.SafeInt(Args[0], 0);
                    bool Rec = Common.Globals.SafeBool(Args[1], false);
                    if (bll.SetTop(contentId, !Rec))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
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

        #region 获取内容的审核状态
        /// <summary>
        /// 获取内容的审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetContentState(object target)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = Resources.Site.btnApproveText;
                        break;
                    case "1":
                        str = Resources.Site.Unaudited;
                        break;
                    case "2":
                        str = Resources.CMS.ContentdrpDraft;
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion



    }
}