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

namespace Maticsoft.Web.Admin.Shop.ProductQA
{
    public partial class ReplyList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 464; } } //Shop_商品疑问回复管理_列表页
        protected new int Act_DelData = 465;    //Shop_商品疑问回复管理_删除数据
        private Maticsoft.BLL.Shop.Products.ProductQA QAbll = new BLL.Shop.Products.ProductQA();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                    liDel.Visible = false;
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
            QAbll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[6].Visible = false;
            }
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            int status = Common.Globals.SafeInt(this.ddlStatus.SelectedValue, -1);
            ds = QAbll.GetReplyList(QAId, status);
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }
        public int QAId
        {
            get
            {
                int qaid = 0;
                if (Request.Params["qaid"] != null && PageValidate.IsNumber(Request.Params["qaid"]))
                {
                    qaid = int.Parse(Request.Params["qaid"]);
                }
                else
                {
                    Response.Redirect("ProductQAList.aspx");
                }
                return qaid;
            }
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
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            QAbll.Delete(ID);
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

        #endregion

        public string GetStatus(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    switch (obj.ToString())
                    {
                        case "0":
                            return "未审核";
                        case "1":
                            return "已审核";
                        case "2":
                            return "审核失败";
                        default:
                            return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        protected void btnAction_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.ddlAction.SelectedValue))
            {
                string idlist = GetSelIDlist();
                if (idlist.Trim().Length == 0) return;
                if (QAbll.SetStatus(idlist, Common.Globals.SafeInt(this.ddlAction.SelectedValue, 0)))
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量操作成功！");
                    gridView.OnBind();
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "批量操作失败！");
                    gridView.OnBind();
                }
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择一个操作！");
                return;
            }
        }
        public string SubString(object target, string sign, int subLength)
        {
            return StringPlus.SubString(target, subLength, sign);
        }
    }
}