using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maticsoft.BLL.Ms;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Accounts.Bus;

namespace Maticsoft.Web.Admin.JLT.UserAttendance
{
    public partial class UserInfo : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.Users bll = new Maticsoft.BLL.Members.Users();

        protected override int Act_PageLoad { get { return 89; } } //客服管理_是否显示发件箱页面
        protected new int Act_DeleteList = 90;    //客服管理_发件箱_批量删除已发送信息

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        public int  EmployeeID
        {
            get
            {
                int id = -1;
                string strid = Request.Params["eid"];
                if (!String.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }

        }

        public string GetCompany(object id)
        {
            if (id != null)
            {
                Maticsoft.BLL.Ms.Enterprise bll = new Maticsoft.BLL.Ms.Enterprise();
                 var model=  bll.GetModel(Common.Globals.SafeInt(id.ToString(), 0));
                if (model != null)
                {
                    return model.Name;
                }
            }
            return "暂无单位";
        }

        public int DepartmentID
        {
            get
            {
                int id = -1;
                string strid = Request.Params["did"];
                if (!String.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }

        }
       
        #region gridView

        public void BindData()
        {
            #region

            StringBuilder strWhere = new StringBuilder();

            if (txtKeyword.Text.Length > 0)
            {

                strWhere.Append(" UserName like '%"+txtKeyword.Text.Trim()+"%'");
            }

            if (EmployeeID>0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
         
                strWhere.Append(" EmployeeID="+EmployeeID+"");
            }
            if (DepartmentID > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" DepartmentID=" + DepartmentID + "");
            }
            if (CurrentUser.UserType == "EE")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and");
                }
                strWhere.Append(" DepartmentID=" + CurrentUser.DepartmentID + "");
            }
          
            #endregion gridView
            gridView.DataSource = bll.GetList(strWhere.ToString());
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindData();
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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");

                if (linkbtnDel != null)
                {
                    linkbtnDel.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
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