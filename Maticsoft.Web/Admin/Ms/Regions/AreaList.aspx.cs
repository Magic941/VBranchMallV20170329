using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.Ms;

namespace Maticsoft.Web.Admin.Ms.Regions
{
    public partial class AreaList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 330; } } //地区分组_列表页
        protected new int Act_UpdateData =331;    //地区分组_编辑数据
        protected new int Act_DelData = 332;    //地区分组_删除数据
        Maticsoft.BLL.Ms.RegionAreas bll = new BLL.Ms.RegionAreas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
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
                    LinkButton updatebtn = (LinkButton)e.Row.FindControl("linkModify");
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OnUpdate")
            {
                int areaId = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
               Maticsoft.Model.Ms.RegionAreas model = bll.GetModel(areaId);
                if (model != null)
                {
                    this.txtAreaId.Value = model.AreaId.ToString();
                    this.tName.Text = model.Name;
                }
            }
        }



        public void btnSave_Click(object sender, System.EventArgs e)
        {
            int areaId = Common.Globals.SafeInt(this.txtAreaId.Value, 0);

            Maticsoft.Model.Ms.RegionAreas model =new RegionAreas();

            model.Name = Common.Globals.HtmlEncode(this.tName.Text.Trim());
            if (areaId > 0)
            {
                model.AreaId = areaId;
                if (bll.Update(model))
                {
                    this.tName.Text = "";
                    this.txtAreaId.Value = "";
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
            this.txtAreaId.Value = "";
        }
    }
}