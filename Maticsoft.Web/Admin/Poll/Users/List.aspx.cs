using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Poll.Users
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 363; } } //客服管理_投票用户管理_列表页
        protected new int Act_DelData = 364; //客服管理_投票用户管理_删除数据
        Maticsoft.BLL.Poll.PollUsers bll = new Maticsoft.BLL.Poll.PollUsers();
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

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        #region BindData
        public void BindData()
        {
            #region 权限检查
            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    return;
            //}
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);

            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            #endregion

            string strWhere = "";
            if (Session["strWherePollUser"] != null && Session["strWherePollUser"].ToString() != "")
            {
                strWhere += Session["strWherePollUser"].ToString();
            }
            DataSet ds = new DataSet();
            ds = bll.GetList(strWhere);
            gridView.DataSetSource = ds;
        }
        #endregion
        
        #region btn_Search
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtKey.Text.Trim();
            string strsql = "";
            if (radbtnTrueName.Checked)
            {
                if (keyword != "")
                {
                    strsql += " and (TrueName like'%" + keyword + "%')";
                }
            }
            else
            {
                if (keyword != "")
                {
                    strsql += " and (UserID =" + keyword + ")";
                }
            }
            if (strsql != "")
            {
                Session["strWherePollUser"] = " (1=1) " + strsql;
            }
            else
            {
                Session["strWherePollUser"] = "";
            }            
            gridView.OnBind();
        }
        #endregion

        #region gridView事件

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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {          
            string userid = gridView.DataKeys[e.RowIndex].Value.ToString();
            bll.Delete(Convert.ToInt32(userid));
            BindData();
        }

        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
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
    }
}