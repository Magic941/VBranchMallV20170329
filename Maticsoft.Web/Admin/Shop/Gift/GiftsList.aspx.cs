/**
* GiftsList.cs
*
* 功 能： [N/A]
* 类 名： GiftsList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/23 12:24:33  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.Shop.Gift
{
    public partial class GiftsList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 422; } } //Shop_礼品管理_列表页
        protected new int Act_AddData = 423;    //Shop_礼品管理_添加数据
        protected new int Act_UpdateData = 424;    //Shop_礼品管理_编辑数据
        protected new int Act_DelData = 425;    //Shop_礼品管理_删除数据

        Maticsoft.BLL.Shop.Gift.Gifts GiftBll = new BLL.Shop.Gift.Gifts();
        private const string ImageUrl = "/images/pics/none.gif";//可以在配置文件配置或者后台参数表配置
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
            {
                liAdd.Visible = false;
            }
            

            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    liAdd.Visible = false;
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
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

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            ds = GiftBll.GetAllList();
            if (ds != null)
            {
                gridView.DataSetSource = ds;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
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
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
        public string GetImageUrl(string ThumbnailsUrl)
        {
            return String.IsNullOrWhiteSpace(ThumbnailsUrl) ? ImageUrl : ThumbnailsUrl;
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            GiftBll.Delete(ID);
            gridView.OnBind();
        }

        protected void SetStock(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SetStock")
            {
                int ID = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
                int index = Convert.ToInt32(e.CommandArgument);
                // GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent;
                GridViewRow drv = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;

                int name = Convert.ToInt32(gridView.Rows[drv.RowIndex].Cells[2].Text);
                // GridViewRow row = gridView.Rows[];
                //if (!String.IsNullOrWhiteSpace(row.Cells[2].Text) && Common.PageValidate.IsNumber(row.Cells[2].Text))
                //{
                //    if (GiftBll.UpdateStock(ID, Common.Globals.SafeInt(row.Cells[2].Text, 0)))
                //    {
                //        gridView.OnBind();
                //    }
                //}

            }
        }


        #region AjaxCallback
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "SetStock":
                    writeText = SetStock();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string SetStock()
        {
            if (!String.IsNullOrWhiteSpace(Request.Form["GiftId"]) && !String.IsNullOrWhiteSpace(Request.Form["Stock"]))
            {
                int giftId = Common.Globals.SafeInt(Request.Form["GiftId"], 0);
                int stock = Common.Globals.SafeInt(Request.Form["Stock"], 0);

                JsonObject json = new JsonObject();
                if (GiftBll.UpdateStock(giftId, stock))
                {
                    json.Put("STATUS", "OK");
                }
                else
                {
                    json.Put("STATUS", "NODATA");
                }
                return json.ToString();
            }
            return "Fail";

        }
        #endregion

    }
}