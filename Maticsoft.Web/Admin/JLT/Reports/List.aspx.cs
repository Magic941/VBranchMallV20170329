using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.JLT.Reports
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 662; } } //移动办公_日报管理_列表页
        protected new int Act_DelData = 663;   //移动办公_日报管理_删除数据

        private User users = new User();
        private Maticsoft.BLL.JLT.Reports bll = new BLL.JLT.Reports();
        private Maticsoft.Model.JLT.Reports model = new Model.JLT.Reports();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
 

                gridView.OnBind();
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
            }
        }
        public int UserId
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!String.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }
        public void BindData()
        {
            DataSet ds = new DataSet();
            string keyword = txtKeyword.Text;
            StringBuilder strWhere = new StringBuilder();
            if (CurrentUser.UserType == "UU")
            {
                int userId = UserId == 0 ? CurrentUser.UserID : UserId;
                if (Request.QueryString["Type"]=="1")
                {
                    strWhere.Append(" AU.UserID in( SELECT UserID FROM Accounts_Users WHERE EmployeeID=" + userId + " )");
                }
                else
                {
                    strWhere.Append(" AU.UserID=" + userId + " ");
                }
            }
            if (CurrentUser.UserType == "EE")
            {
                strWhere.Append(" AU.UserID in( SELECT UserID FROM Accounts_Users WHERE DepartmentID='" + CurrentUser.DepartmentID + "')");
            }
            if (!String.IsNullOrWhiteSpace(txtUserName.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" AU.UserName like '%" + txtUserName.Text + "%'");
            }
            if (!String.IsNullOrWhiteSpace(txtDate.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" CreatedDate >= '" + txtDate.Text + "' and CreatedDate< dateadd(dd,1,'" + txtDate.Text + "') ");
            }
            //简报类型
            if (!String.IsNullOrWhiteSpace(this.ddType.SelectedValue))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.AppendFormat(" Type={0}", this.ddType.SelectedValue);
            }
            if (!String.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" Content like '%" + keyword + "%'");
            }
            if (strWhere.Length > 0)
            {
                strWhere.Append(" and");
            }
            strWhere.Append(" 1=1 Order by ID DESC");
            ds = bll.GetListEx(Globals.SafeString(strWhere, ""));

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
                object obj2 = DataBinder.Eval(e.Row.DataItem, "UserID");
                if (obj2 != null)
                {
                    List<string> UserIDlist = Maticsoft.Common.StringPlus.GetStrArray(BLL.SysManage.ConfigSystem.GetValueByCache("AdminUserID"), ',', true);
                    if (UserIDlist.Contains(obj2.ToString()))
                    {
                        LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                        linkbtnDel.Visible = false;
                    }
                    else
                    {
                        LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                        linkbtnDel.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
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
                model = bll.GetModel(int.Parse(ID));
                string todoinfo = model.ID.ToString();
                bll.Delete(int.Parse(ID));
                if (!string.IsNullOrWhiteSpace(model.FileDataPath))
                {
                    string oldpath = Server.MapPath(model.FileDataPath);
                    File.Delete(oldpath);
                }
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("删除简报：【{0}】", todoinfo), this);
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

        #endregion gridView

        #region 批量操作

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, string.Format("批量删除简报：【{0}】", idlist), this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        #endregion 批量操作

        public string GetType(object taget)
        {
            int Type = Convert.ToInt32(taget);
            string strType = "";
            switch (Type)
            {
                case 0:
                    strType = "文字";
                    break;

                case 1:
                    strType = "图片";
                    break;

                case 2:
                    strType = "声音";
                    break;
            }
            return strType;
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

        public string ImageSource(object fileName, object filePath)
        {

            string hrefLink = " <a class='imageinfo' href='{0}' title='{1}'><img src='{0}' target='_blank' title='{1}' width='80' height='80' /></a>";
            if (fileName == null || filePath == null)
            {
                return "";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(fileName.ToString()) || string.IsNullOrWhiteSpace(filePath.ToString()))
                {
                    return "";
                }
             
                StringBuilder strFilePth = new StringBuilder();
                string[] names = fileName.ToString().Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < names.Length; i++)
                {
                    string path = Maticsoft.Common.Globals.HostPath(Request.Url) +
                                  string.Format(filePath.ToString(), names[i]);
                    strFilePth.AppendFormat(hrefLink, path, names[i]);
                }
                return strFilePth.ToString();
            }
        }
    }
}