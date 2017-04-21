/**
* List.cs
*
* 功 能： [N/A]
* 类 名： SingleList.cs
*
* Ver            变更日期                     负责人     变更内容
* ───────────────────────────────────
* V0.01  2012年6月1日 11:34:28  孙鹏             创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.Advertisement
{
    public partial class SingleList : PageBaseAdmin
    {

        protected new int Act_DelData = 173;    //网站管理_广告内容管理_删除广告内容
        protected new int Act_UpdateData = 172;    //网站管理_广告内容管理_编辑广告内容
        protected new int Act_AddData = 171;    //网站管理_广告内容管理_添加广告内容
        protected new int Act_DeleteList = 174;    //网站管理_广告内容管理_批量删除广告内容

        Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
        #region
        private string AdWidth = "0";
        private string AdHeight = "0";
        public string ADPositionId = "0";
        public int AdPositionID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        private void AltInfo()
        {
            Maticsoft.BLL.Settings.AdvertisePosition bllPosition = new BLL.Settings.AdvertisePosition();
            Maticsoft.Model.Settings.AdvertisePosition modelPosition = bllPosition.GetModel(AdPositionID);
            if (modelPosition != null)
            {
                this.litTitle.Text = modelPosition.AdvPositionName;
                AdWidth = modelPosition.Width.HasValue ? modelPosition.Width.Value.ToString() : "auto" + "px";
                AdHeight = modelPosition.Height.HasValue ? modelPosition.Height.Value.ToString() : "auto" + "px";
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
                {
                    this.Controls.Clear();
                    this.DoCallback();
                }
                ADPositionId = AdPositionID.ToString();
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                AltInfo();
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
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "删除成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "删除失败！");
            }

            string url = string.Format("SingleList.aspx?id={0}", AdPositionID);
            Response.Redirect(url);
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[9].Visible = false;
            }
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" AdvPositionId={0}", AdPositionID);
            ds = bll.GetList(-1, strWhere.ToString(), " Sequence");
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
                Literal litContent = (Literal)e.Row.FindControl("litContent");
                Literal litState = (Literal)e.Row.FindControl("litState");
                if (litState != null)
                {
                    switch (DataBinder.Eval(e.Row.DataItem, "State").ToString())
                    {
                        case "0":
                            litState.Text = "无效";
                            break;
                        case "1":
                            litState.Text = "有效";
                            break;
                        case "-1":
                            litState.Text = "欠费停止";
                            break;
                        default:
                            litState.Text = "";
                            break;
                    }
                }
                if (litContent != null)
                {
                    HiddenField hfShowType = (HiddenField)e.Row.FindControl("hfShowType");
                    switch (hfShowType.Value)
                    {
                        case "0":
                            litContent.Text = "<div style=\"height:70;width:110;\" >" + DataBinder.Eval(e.Row.DataItem, "AlternateText").ToString() + "</div>";
                            break;
                        case "1":
                            litContent.Text = "<div style=\"height:70;width:110;\" >" + "<img src=\"" + Maticsoft.Web.Components.FileHelper.GetImageUrl(DataBinder.Eval(e.Row.DataItem, "FileUrl").ToString()) + "\" height=\"70\" width=\"110\" />" + "</div>";
                            break;
                        case "2":
                            litContent.Text = "<div style=\"height:70;width:110;\" >" + "<a href='" + Maticsoft.Web.Components.FileHelper.GetImageUrl(DataBinder.Eval(e.Row.DataItem, "FileUrl").ToString()) + "'><img src=\"/admin/images/logo.gif\" height=\"70\" width=\"110\" /></a>" + "</div>";
                            break;
                        case "3":
                            if (DataBinder.Eval(e.Row.DataItem, "AdvHtml").ToString().Contains("baidu"))
                            {
                                litContent.Text = "百度广告脚本";
                            }
                            else if (DataBinder.Eval(e.Row.DataItem, "AdvHtml").ToString().Contains("google"))
                            {
                                litContent.Text = "谷歌广告脚本";
                            }
                            else
                            {
                                litContent.Text = "自定义广告脚本";
                            }
                            // litContent.Text = "<div style=\"height:70;width:110;\" >" + DataBinder.Eval(e.Row.DataItem, "AdvHtml").ToString() + "</div>";
                            break;
                        default:
                            litContent.Text = "";
                            break;
                    }
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //#warning 代码生成警告：请检查确认真实主键的名称和类型是否正确
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //bll.Delete(ID);
            //gridView.OnBind();
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

        protected string ConvertContentType(object obj)
        {
            if (obj != null)
            {
                switch (obj.ToString())
                {
                    case "0":
                        return "文字";
                    case "1":
                        return "图片";
                    case "2":
                        return "Flash";
                    case "3":
                        return "Html代码";
                    default:
                        return "未定义的显示类型";
                }
            }
            else
            {
                return "未定义的显示类型";
            }
        }

        public string GetEnName(object obj)
        {
            if (obj != null)
            {
                int id = Globals.SafeInt(obj.ToString(), 0);
                Model.Ms.Enterprise model = new BLL.Ms.Enterprise().GetModel(id);
                if (model != null)
                {
                    return model.Name;
                }
                else
                {
                    return "广告主不存在";
                }
            }
            else
            {
                return "广告主不存在";
            }
        }
        #endregion

        #region Ajax方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateSeqNum":

                    writeText = UpdateSeqNum();
                    break;
                default:
                    writeText = UpdateSeqNum();
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string UpdateSeqNum()
        {
            JsonObject json = new JsonObject();
            int AdvId = Common.Globals.SafeInt(this.Request.Form["AdvId"], 0);
            int UpdateValue = Common.Globals.SafeInt(this.Request.Form["UpdateValue"], 0);
            if (AdvId == 0 || UpdateValue == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {

                if (bll.UpdateSeq(UpdateValue, AdvId))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }
        #endregion
    }
}
