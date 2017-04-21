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
    public partial class OrderLookupList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 435; } } //Shop_订单可选项管理_列表页
        protected new int Act_AddData = 436;    //Shop_订单可选项管理_添加数据
        protected new int Act_UpdateData = 437;    //Shop_订单可选项管理_编辑数据
        protected new int Act_DelData = 438;    //Shop_订单可选项管理_删除数据
        Maticsoft.BLL.Shop.Order.OrderLookupList bll=new BLL.Shop.Order.OrderLookupList();
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
            }
        }


           /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindData()
        {
            DataSet ds = new DataSet();
            ds = bll.GetAllList();
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OnUpdate")
            {
                int lookupListId = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
                Maticsoft.Model.Shop.Order.OrderLookupList model = bll.GetModel(lookupListId);
                if (model != null)
                {
                    this.tDesc.Text = model.Description;
                    this.txtLookupListId.Value = model.LookupListId.ToString();
                    this.tName.Text = model.Name;
                    this.ddlType.SelectedValue = model.SelectMode.ToString();
                }
            }
        }

        protected string GetModeName(object target)
        {
            //1:下拉列表、2:单选按钮、3:复选框
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str ="下拉列表";
                        break;
                    case "2":
                        str = "单选按钮";
                        break;
                    case "3":
                        str = "复选框";
                        break;
                    default:
                        str = "未知";
                        break;
                }
            }
            return str;
        }


        public void btnSave_Click(object sender, System.EventArgs e)
        {
            int lookupListId = Common.Globals.SafeInt(this.txtLookupListId.Value, 0);
            
            Maticsoft.Model.Shop.Order.OrderLookupList model=new Model.Shop.Order.OrderLookupList();
         
            model.SelectMode = Common.Globals.SafeInt(this.ddlType.SelectedValue,1);
            model.Name = this.tName.Text.Trim();
            model.Description = this.tDesc.Text.Trim();
            if (lookupListId > 0)
            {
                model.LookupListId = lookupListId;
                if (bll.Update(model) )
                {
                    this.tName.Text = "";
                    this.tDesc.Text = "";
                    this.txtLookupListId.Value = "";
                    this.ddlType.SelectedValue = "1";
                    gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "编辑失败！请重试。");
                }
            }
            else
            {
                if (bll.Add(model)>0)
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
            this.txtLookupListId.Value = "";
        }
    }
}