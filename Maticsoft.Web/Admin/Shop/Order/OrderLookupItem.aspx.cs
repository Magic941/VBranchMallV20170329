using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Order
{
    public partial class OrderLookupItem : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 439; } } //Shop_订单可选项内容管理_列表页
        protected new int Act_AddData = 440;    //Shop_订单可选项内容管理_添加数据
        protected new int Act_UpdateData = 441; //Shop_订单可选项内容管理_编辑数据
        protected new int Act_DelData = 442;    //Shop_订单可选项内容管理_删除数据
        Maticsoft.BLL.Shop.Order.OrderLookupItems bll = new BLL.Shop.Order.OrderLookupItems();
        Maticsoft.BLL.Shop.Order.OrderLookupList listBll=new BLL.Shop.Order.OrderLookupList();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    btnSave.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnSave.Visible = false;
                }

                Maticsoft.Model.Shop.Order.OrderLookupList listModel = listBll.GetModel(ListId);
                if (listModel != null)
                {
                    this.txtTitle.Text = "订单可选项【" + listModel.Name + "】的内容管理";
                    this.txtDesc.Text = "您可以对网站订单可选项【" + listModel.Name + "】的内容进行添加，编辑，删除等操作";
                }
            }
        }


        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindData()
        {
            DataSet ds = new DataSet();
            ds = bll.GetList(" LookupListId=" + ListId);
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
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
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton linkDel = (LinkButton)e.Row.FindControl("linkDel");
                    linkDel.Visible = false;  
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    LinkButton linkModify = (LinkButton)e.Row.FindControl("linkModify");
                    linkModify.Visible = false;
                }
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
            int ID = Common.Globals.SafeInt(gridView.DataKeys[e.RowIndex].Value.ToString(), 0);
            bll.Delete(ID);
            gridView.OnBind();
        }

        #region 编号
        /// <summary>
        /// 可选项Id
        /// </summary>
        public int ListId
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid))
                {
                    id = Globals.SafeInt(strid, 0);
                }
                return id;
            }
        }
        #endregion

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OnUpdate")
            {
                int lookupItemId = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Maticsoft.Model.Shop.Order.OrderLookupItems model = bll.GetModel(lookupItemId);
                if (model != null)
                {
                    this.tDesc.Text = model.Remark;
                    this.txtLookupItemId.Value = model.LookupItemId.ToString();
                    this.tName.Text = model.Name;
                    this.ddlCalculateMode.SelectedValue = model.CalculateMode.ToString();
                    if (model.IsInputRequired)
                    {
                        this.Required_Y.Checked = true;
                        this.Required_N.Checked = false;
                    }
                    else
                    {
                        this.Required_Y.Checked = false;
                    }
                    this.tTitle.Text = model.InputTitle;
                    this.tAppendMoney.Text = model.AppendMoney.ToString();
                }
            }
        }

        protected string GetMoneyInfo(object target,object mode)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int type = Common.Globals.SafeInt(mode.ToString(), 1);
                if (type == 1)
                {
                    str = "￥" + Common.Globals.SafeDecimal(target.ToString(), 0);
                }
                else
                {
                   str=  Common.Globals.SafeDecimal(target.ToString(), 0) + "%";
                }
            }
            return str;
        }


        public void btnSave_Click(object sender, System.EventArgs e)
        {
            int lookupItemId = Common.Globals.SafeInt(this.txtLookupItemId.Value, 0);

            Maticsoft.Model.Shop.Order.OrderLookupItems model = new Model.Shop.Order.OrderLookupItems();

            model.IsInputRequired = this.Required_Y.Checked;
            model.InputTitle = this.Required_Y.Checked? this.tTitle.Text:"";
            model.AppendMoney = Common.Globals.SafeDecimal(this.tAppendMoney.Text,0);
            model.CalculateMode = Common.Globals.SafeInt(ddlCalculateMode.SelectedValue, 0); 
            model.Name = this.tName.Text.Trim();
            model.Remark = this.tDesc.Text.Trim();
            model.LookupListId = ListId;
            if (lookupItemId > 0)
            {
                model.LookupItemId = lookupItemId;
                if (bll.Update(model))
                {
                    this.tName.Text = "";
                    this.tDesc.Text = "";
                    this.txtLookupItemId.Value = "";
                    this.Required_N.Checked = true;
                    this.ddlCalculateMode.SelectedValue = "1";
                    this.tTitle.Text = "";
                    this.tAppendMoney.Text = "0";
                    gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "编辑失败！请重试。");
                }
            }
            else
            {
                if (bll.Add(model) > 0)
                {
                    gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "添加失败！请重试。");
                }
            }
        }

        public void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.tName.Text = "";
            this.tDesc.Text = "";
            this.txtLookupItemId.Value = "";
            this.Required_N.Checked = true;
            this.ddlCalculateMode.SelectedValue = "1";
            this.tTitle.Text = "";
            this.tAppendMoney.Text = "0";
        }
    }
}