using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Guestbook
{
    public partial class GuestBookList : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.Guestbook bll = new BLL.Members.Guestbook();

        protected override int Act_PageLoad { get { return 84; } } //客服管理_是否显示客户反馈页面

        protected new int Act_DeleteList = 85;    //客服管理_客户反馈_批量删除客户反馈

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
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
            }
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        public int Type
        {
            get
            {
                int _type = -1;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["type"]))
                {
                    _type = Globals.SafeInt(Request.QueryString["type"], -1);
                }
                return _type;
            }
        }
        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
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

                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
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

        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(txtRegion.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("CreatorRegion like '%{0}%' ", txtRegion.Text.Trim());
            }
            if (Type>-1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("Status={0} ", Type);
            }
            if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("CreatorEmail like '%{0}%' ", txtEmail.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  convert(date,CreatedDate)>='" + txtBeginTime.Text.Trim() + "' ");
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  convert(date,CreatedDate)<='" + txtEndTime.Text.Trim() + "' ");
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("Description like '%{0}%' ", txtKeyword.Text.Trim());
            }

            if (strWhere.Length > 0)
            {
                strWhere.Append(" and");
            }

            strWhere.Append("  1=1 order by CreatedDate desc");
            ds = bll.GetList(strWhere.ToString());
            gridView.DataSetSource = ds;
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
                object obj1 = DataBinder.Eval(e.Row.DataItem, "Description");
                if ((obj1 != null) && ((obj1.ToString() != "")))
                {
                    if (obj1.ToString().Length > 20)
                    {
                        e.Row.Cells[7].Text = obj1.ToString().Substring(0, 20);
                    }
                }
                object obj2 = DataBinder.Eval(e.Row.DataItem, "Title");
                if ((obj2 != null) && ((obj2.ToString() != "")))
                {
                    if (obj2.ToString().Length > 20)
                    {
                        e.Row.Cells[11].Text = obj2.ToString().Substring(0, 20);
                    }
                }
        
            }
        }


        /// <summary>
        /// 获取类型名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetStatus(object target)
        {
            //0:未处理 1：已处理。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target) && Common.PageValidate.IsNumber(target.ToString()))
            {
                int statusId = Common.Globals.SafeInt(target.ToString(), 0);
                if (statusId == 0)
                {
                    return "未处理";
                }
                else
                {
                    return "已处理";
                }

            }
            return str;
        }
     
    }
}