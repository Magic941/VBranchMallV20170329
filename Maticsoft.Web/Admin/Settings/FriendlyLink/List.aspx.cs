using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.FriendlyLink.FLinks
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 379; } } //设置_友情链接管理_列表页
        protected new int Act_AddData = 383;    //设置_友情链接管理_添加数据
        protected new int Act_UpdateData = 384;    //设置_友情链接管理_编辑数据
        protected new int Act_DelData = 385;    //设置_友情链接管理_删除数据
        //int Act_ShowInvalid = -1; //查看失效数据行为
        Maticsoft.BLL.Settings.FriendlyLink bll = new BLL.Settings.FriendlyLink();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
               MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
               MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveError);
            }
            gridView.OnBind();
        }

        public string fsState(object data)
        {
            string State = "";
            if (data != null)
            {
                int value = Convert.ToInt32(data);
                switch (value)
                {
                    case 0:
                        State = Resources.Site.Unaudited;
                        break;
                    case 1:
                        State = Resources.Site.btnApproveText;
                        break;
                    default:
                        State = Resources.Site.Unknown;
                        break;
                }
            }
            return State;
        }

        public string fsType(object data)
        {
            string Type = "";
            if (data != null)
            {
                int value = Convert.ToInt32(data);
                switch (value)
                {
                    case 0:
                        Type = Resources.SiteSetting.lblImgLink;
                        break;
                    case 1:
                        Type = Resources.SiteSetting.lblTextLink;
                        break;
                    default:
                        Type = Resources.Site.Unknown;
                        break;
                }
            }
            return Type;
        }

        #region gridView

        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[12].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[13].Visible = false;
            }
            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            string state = DropState.SelectedValue;

            if ("" != DropState.SelectedValue)
            {
                strWhere.AppendFormat("Name like '%{0}%' and state=" + state + "", txtKeyword.Text.Trim());
            }
            else
            {
                if (txtKeyword.Text.Trim() != "")
                {
                    strWhere.AppendFormat("Name like '%{0}%'", txtKeyword.Text.Trim());
                }
            }
            if (strWhere.Length > 0)
            {
                strWhere.Append(" and ");
            }
            strWhere.Append(" 1=1 order by OrderID ");
            ds = bll.GetList(strWhere.ToString());
            gridView.DataSetSource = ds;
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
            bll.Delete(ID);
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

        #endregion

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApproveList_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "State=" + 1;
            if (bll.UpdateList(idlist, strWhere))
            {
               MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            }
            else
            {
               MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveError);
            }
            gridView.OnBind();
        }

        /// <summary>
        /// 批量反审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "State=" + 0;
            if (bll.UpdateList(idlist, strWhere))
            {
               MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            }
            else
            {
               MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveError);
            }
            gridView.OnBind();
        }
    }
}
