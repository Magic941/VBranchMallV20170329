using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Shipping
{
    public partial class ShippingType :PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 517; } } //Shop_配送方式管理_列表页
        protected new int Act_AddData = 518;    //Shop_配送方式管理_添加数据
        protected new int Act_UpdateData = 519;    //Shop_配送方式管理_编辑数据
        protected new int Act_DelData = 520;    //Shop_配送方式管理_删除数据

        Maticsoft.BLL.Shop.Shipping.ShippingType bll = new BLL.Shop.Shipping.ShippingType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
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
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
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



        protected string GetModeName(object target)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "下拉列表";
                        break;
                    case "2":
                        str = "单选按钮";
                        break;
                    default:
                        str = "下拉列表";
                        break;
                }
            }
            return str;
        }

    }
}