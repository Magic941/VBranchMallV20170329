using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.JLT.ToDoInfo
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 675; } } //移动办公_待办管理_列表页
        protected new int Act_AddData = 676;    //移动办公_待办管理_添加数据
        protected new int Act_UpdateData = 677;    //移动办公_待办管理_编辑数据
        protected new int Act_DelData = 678;   //移动办公_待办管理_删除数据
        private User users = new User();
        private UserType usersType = new UserType();
        private Maticsoft.BLL.JLT.ToDoInfo bll = new BLL.JLT.ToDoInfo();
        private Maticsoft.Model.JLT.ToDoInfo model = new Model.JLT.ToDoInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
               
                gridView.OnBind();
                btnBatch.Attributes.Add("onclick", "return confirm(\"" + Resources.CMS.ContentTooltipBatch + "\")");
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
            }
        }

        public int Status
        {
            get
            {
                int status = 0;
                string strstatus = Request.Params["status"];
                if (!string.IsNullOrWhiteSpace(strstatus))
                {
                    status = Globals.SafeInt(strstatus, 0);
                }
                return status;
            }
        }

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[9].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[10].Visible = false;
            }
            DataSet ds = new DataSet();
            string keyword = "";
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                keyword = txtKeyword.Text.Trim();
            }
            if (CurrentUser.UserType == "UU")
            {
                strWhere.Append(" (UserID in( SELECT UserID FROM Accounts_Users  WHERE EmployeeID=" + CurrentUser.UserID + ") OR UserID=" + CurrentUser.UserID + ")  AND CreatedUserId=" + CurrentUser.UserID + "");
            }
            if (CurrentUser.UserType == "EE")
            {
                strWhere.Append(" UserID in( SELECT UserID FROM Accounts_Users  WHERE DepartmentID='" + CurrentUser.DepartmentID + "')");
            }
            if (strWhere.Length > 0)
            {
                strWhere.Append(" and");
            }
            strWhere.Append(" Status=" + Status);
            if (!String.IsNullOrWhiteSpace(txtUserName.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" UserName like '%" + txtUserName.Text + "%'");
            }
            if (!String.IsNullOrWhiteSpace(txtDate.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" CreatedDate >= '" + txtDate.Text + "' and CreatedDate < dateadd(dd,1,'" + txtDate.Text + "') ");
            }
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" Title like '%" + keyword + "%'");
            }

            ds = bll.GetList(0, Globals.SafeString(strWhere, ""), " ID DESC");

            gridView.DataSetSource = ds;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView

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
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            model = bll.GetModel(int.Parse(ID));
            string todoinfo = model.Title;
            bll.Delete(int.Parse(ID));
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除待办：【{0}】", todoinfo), this);
            Common.MessageBox.ShowSuccessTip(this, "删除成功！");
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

        #region 批量操作

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量删除待办：【{0}】", idlist), this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            StringBuilder strWhere = new StringBuilder();
            string idlist = GetSelIDlist();
            int status = int.Parse(drop_Status.SelectedValue);
            string reviewedcontent = txtReviewedContent.Text.Trim();
            if (idlist.Trim().Length == 0)
            {
                return;
            }
            if (status >= 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" , ");
                }
                strWhere.Append(" Status=" + status);
            }
            if (!String.IsNullOrWhiteSpace(reviewedcontent))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" , ");
                }
                strWhere.Append(" ReviewedContent='" + reviewedcontent + "'");
            }
            if (strWhere.Length > 0)
            {
                strWhere.Append(" , ");
            }
            strWhere.Append(" ReviewedUserID=" + CurrentUser.UserID);
            if (strWhere.Length > 0)
            {
                if (bll.UpdateList(idlist, strWhere.ToString()))
                {
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量批复待办：【{0}】", idlist), this);
                    MessageBox.ShowSuccessTip(this, "批处理成功！");
                    gridView.OnBind();
                    drop_Status.SelectedValue = "-1";
                }
            }
        }

        #endregion 批量操作

        public string GetInfoToType(object taget)
        {
            int ToType = Convert.ToInt32(taget);
            string strToType = "";
            switch (ToType)
            {
                case 0:
                    strToType = "本人";
                    break;

                case 1:
                    strToType = "下属";
                    break;

                case 2:
                    strToType = "所有下属";
                    break;

                case 3:
                    strToType = "指定用户";
                    break;
            }
            return strToType;
        }

        public string GetStatus(object taget)
        {
            int Status = Convert.ToInt32(taget);
            string strStatus = "";
            switch (Status)
            {
                case 0:
                    strStatus = "未办";
                    break;

                case 1:
                    strStatus = "已办";
                    break;

                case 2:
                    strStatus = "未通过";
                    break;

                case 3:
                    strStatus = "已通过";
                    break;
            }
            return strStatus;
        }

        public string GetUserName(object taget)
        {
            int userid = Globals.SafeInt(taget.ToString(), -1);
            if (userid < 0) return "";

            Maticsoft.Accounts.Bus.User user = new User(userid);
            if (user == null) return "";
            return user.UserName;
        }

        public string FileSource(object fileName, object filePath)
        {
            string hrefLink = "<a href='{0}' target='_blank' title='{1}'>查看/下载</a>";
            if (fileName == null || filePath == null)
            {
                return "无";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(fileName.ToString()) || string.IsNullOrWhiteSpace(filePath.ToString()))
                    return "无";
                StringBuilder strFilePth = new StringBuilder();
                string[] names = fileName.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < names.Length; i++)
                {
                    string path = Maticsoft.Common.Globals.HostPath(Request.Url) +
                                  string.Format(filePath.ToString(), names[i]);
                    strFilePth.AppendFormat(hrefLink, path, names[i]);
                    strFilePth.Append("<br/>");
                }
                return strFilePth.ToString();
            }
        }
    }
}