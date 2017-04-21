using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.Ms.RegionRec
{
    public partial class RecList : PageBaseAdmin
    {
        Maticsoft.BLL.Ms.RegionRec recBll=new BLL.Ms.RegionRec();
        Maticsoft.BLL.Ms.Regions regionBll = new BLL.Ms.Regions();
        protected override int Act_PageLoad { get { return 324; } } //设置_热门地区推荐管理页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }
                    ddRegion.DataSource = regionBll.GetPrivoces();
                    this.ddRegion.DataTextField = "RegionName";
                    this.ddRegion.DataValueField = "RegionId";
                    this.ddRegion.DataBind();
                    ddRegion.Items.Insert(0, new ListItem("请选择", ""));
               
                gridView.OnBind();
            }
        }

        

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region 批量操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (recBll.DeleteList(idlist))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
        }


        #endregion

        

        #region gridView

        public void BindData()
        {
            int parentId = Common.Globals.SafeInt(this.ddRegion.SelectedValue, 0);
            if(parentId>0)
            {
                gridView.DataSource = regionBll.GetList(" ParentId=" + parentId);
            }

            gridView.DataBind();
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
            if (recBll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
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


           protected void  ddRegion_Change(object sender, EventArgs e)
        {
            gridView.OnBind();
            }


           protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
           {
               if (e.CommandName == "RegionRec")
               {
                   int regionId = Common.Globals.SafeInt(e.CommandArgument.ToString(), 0);
                   int recId = recBll.AddEx(regionId, 0);
                   if (recId<=0)
                   {
                       Maticsoft.Common.MessageBox.ShowSuccessTip(this, "推荐失败！");
                   }
               }
           }
        #endregion

           #region AjaxCallback

           private void DoCallback()
           {
               string action = this.Request.Form["Action"];
               this.Response.Clear();
               this.Response.ContentType = "application/json";
               string writeText = string.Empty;

               switch (action)
               {
                   case "GetRecList":

                       writeText = GetRecList();
                       break;
                   case "Delete":
                       writeText = Delete();
                       break;
               }
               this.Response.Write(writeText);
               this.Response.End();
           }

           private string GetRecList()
           {
               JsonObject json = new JsonObject();
             List<Maticsoft.Model.Ms.RegionRec> recList=recBll.GetModelList(" type=0");
             string data = "";
             if (recList != null && recList.Count > 0)
             {
                 int i=0;
                 foreach (var item in recList)
                 {
                     if (i == 0)
                     {
                         data = item.ID + "," + item.RegionName;
                     }
                     else
                     {
                         data = data + "|" + item.ID + "," + item.RegionName;
                     }
                     i++;
                 }
             }
             json.Put("STATUS", "SUCCESS");
             json.Put("DATA", data);
               return json.ToString();
           }

           private string Delete()
           {
               JsonObject json = new JsonObject();
               int  RecId = Common.Globals.SafeInt(this.Request.Form["RecId"], 0);
               if (recBll.Delete(RecId))
                   {
                       json.Put("STATUS", "SUCCESS");
                   }
                   else
                   {
                       json.Put("STATUS", "FAILED");
                   }
               return json.ToString();
           }

           #endregion AjaxCallback

    }
}