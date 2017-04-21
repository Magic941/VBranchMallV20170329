using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.UserCard
{
    public partial class CardList : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.UserCard cardBll = new BLL.Members.UserCard();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    btnDelete.Visible = false;
                }
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

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
            cardBll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            #region


            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[4].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            #endregion gridView

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat("CardCode like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = cardBll.GetList(-1, strWhere.ToString(), " CreatedDate desc");

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
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            if (cardBll.DeleteEx(ID))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "删除成功！");
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

        #endregion

        public string GetUserName(object target)
        {
            int userId = Common.Globals.SafeInt(target, 0);
            Maticsoft.Accounts.Bus.User user = new Maticsoft.Accounts.Bus.User(userId);
            return user.TrueName;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "冻结";
                        break;
                    case 1:
                        str = "已激活";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetType(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int type = Common.Globals.SafeInt(target, 0);
                switch (type)
                {
                    case 0:
                        str = "金额卡";
                        break;
                    case 1:
                        str = "计次卡";
                        break;
                    case 2:
                        str = "折扣卡";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public string GetCardValue(object target1,object target2)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target2))
            {
                int type = Common.Globals.SafeInt(target2, 0);
                switch (type)
                {
                    case 0:
                        str = target1.ToString();
                        break;
                    case 1:
                        str = Common.Globals.SafeInt(target1.ToString(),0).ToString();
                        break;
                    case 2:
                        str = target1.ToString();
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        

    }
}