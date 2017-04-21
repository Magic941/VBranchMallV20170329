using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class SetProductList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 551; } } //Shop_活动商品管理_列表页
        protected new int Act_DelData = 552;    //Shop_活动商品管理_删除数据
        private Maticsoft.BLL.Shop.ActivityManage.AMPBLL ampBll = new BLL.Shop.ActivityManage.AMPBLL();
        private Maticsoft.BLL.Shop.ActivityManage.AMBLL amBll = new BLL.Shop.ActivityManage.AMBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }

                //加载活动规则
                ddlRule.DataSource = amBll.GetAllList();
                ddlRule.DataTextField = "AMName";
                ddlRule.DataValueField = "AMId";
                ddlRule.DataBind();
                this.ddlRule.Items.Insert(0, new ListItem("请选择", "0"));
                gridView.OnBind();
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
            //ampBll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[3].Visible = false;
                btnDelete.Visible = false;
            }
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            int AMId = Common.Globals.SafeInt(ddlRule.SelectedValue, 0);
            if (AMId > 0)
            {
                strWhere.AppendFormat(" AMId={0}", AMId);
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("a.ProductName like '%{0}%'", txtKeyword.Text.Trim());
            }
            if (txtCode.Text.Trim() != "")
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("a.ProductId like '%{0}%'", txtCode.Text.Trim());
            }
            //GetList
            ds = ampBll.GetListAndPrice(-1, strWhere.ToString(), "ProductId ");
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
            int AMId = (int)gridView.DataKeys[e.RowIndex][0];
            int ProductId = Common.Globals.SafeInt(gridView.DataKeys[e.RowIndex][1].ToString(), 0);
            //ampBll.Delete(AMId, SupplierId);
            gridView.OnBind();
            Common.MessageBox.ShowSuccessTip(this, "删除成功");
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
                        int AMId = (int)gridView.DataKeys[i][0];
                        int ProductId = Common.Globals.SafeInt(gridView.DataKeys[i][1].ToString(), 0);
                        idlist += (AMId + "|" + ProductId) + ",";
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

        public string GetAMName(object obj)
        {
            if (obj == null)
            return string.Empty;
            int AMId = Common.Globals.SafeInt(obj.ToString(), 0);
            Maticsoft.Model.Shop.ActivityManage.AMModel amModel = amBll.GetModel(AMId);
            if (amModel != null)
            {
                return amModel.AMName;
            }
            return "未知活动名称";
        }

        #region 获取优惠规则名称 

        public string GetAMType(object obj)
        {
            if (obj == null)
                return string.Empty;
            int AMId = Common.Globals.SafeInt(obj.ToString(), 0);
            Maticsoft.Model.Shop.ActivityManage.AMModel amModel = amBll.GetModel(AMId);
            if (amModel.AMType == 0)
            {
                return "折扣";
            }
            else 
            {
                return "减价";
            }
            
        }

        #endregion

        
    }
}