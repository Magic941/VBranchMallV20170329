using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.JLT.ToDoInfo
{
    public partial class MyToDoInfo : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 675; } } //移动办公_待办管理_列表页
        
        User users = new User();
        UserType usersType = new UserType();
        Maticsoft.BLL.JLT.ToDoInfo bll = new BLL.JLT.ToDoInfo();
        Maticsoft.Model.JLT.ToDoInfo model = new Model.JLT.ToDoInfo();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                gridView.OnBind();
                btnBatch.Attributes.Add("onclick", "return confirm(\"" + Resources.CMS.ContentTooltipBatch + "\")");
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

        public int TypeID
        {
            get
            {
                int type = -1;
                string strtype = Request.Params["type"];
                if (!string.IsNullOrWhiteSpace(strtype))
                {
                    type = Globals.SafeInt(strtype, 0);
                }
                return type;
            }
        }

        public void BindData()
         {
            DataSet ds = new DataSet();
            string keyword = "";
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                keyword = txtKeyword.Text.Trim();
            }
            strWhere.AppendFormat(" UserID={0}", CurrentUser.UserID);
            if (strWhere.Length > 0)
            {
                strWhere.Append(" and");
            }
            strWhere.AppendFormat(" Status={0}", Status);
            if (!String.IsNullOrWhiteSpace(txtCreatedUserName.Text))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" CreatedUserID in( Select UserID from Accounts_Users where UserName like '%" + txtCreatedUserName.Text + "%') ");
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
            if (Status >= 2)
            {
                appTr.Visible = false;
            }
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
            int status=int.Parse(drop_Status.SelectedValue);
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


        #endregion
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
                    strToType = "所有人";
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
    }


}
