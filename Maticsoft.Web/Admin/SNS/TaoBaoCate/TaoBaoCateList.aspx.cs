using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.SNS.TaoBaoCate
{
    public partial class TaoBaoCateList : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.CategorySource bll = new BLL.SNS.CategorySource();
        protected override int Act_PageLoad { get { return 120; } } //运营管理_是否显示淘宝分类页面


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrWhiteSpace(this.Request.Form["Callback"]) &&
            (this.Request.Form["Callback"] == "true"))
                {
                    this.Controls.Clear();
                    this.DoCallback();
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

        protected void btnTaoBaoShow_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnUpdateTaoBaoCate_Click(object sender, EventArgs e)
        {
            int addCount = 0;
            int updateCount = 0;
            bll.ResetCategory(out addCount, out updateCount);
            Maticsoft.Web.LogHelp.AddUserLog(CurrentUser.UserName, "更新淘宝分类",
                                           "更新淘宝分类成功，新增了【" + addCount + "】条数据，更新了【" + updateCount + "】条数据", this);
            MessageBox.ShowSuccessTip(this, "更新淘宝分类成功，新增了【" + addCount + "】条数据，更新了【" + updateCount + "】条数据", "TaoBaoCateList.aspx");
        }
      

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            int TaoBaoCategoryId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            strWhere.AppendFormat(" ParentID=" + TaoBaoCategoryId);
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" and Name like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = bll.GetCategoryList(strWhere.ToString());
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.DataBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
            int categoryId = (int)this.gridView.DataKeys[rowIndex].Value;

            //if (e.CommandName == "Fall")
            //{
            //    bll.SwapSequence(categoryId, Model.Shop.Products.SwapSequenceIndex.Up);
            //}
            //if (e.CommandName == "Rise")
            //{
            //    bll.SwapSequence(categoryId, Model.Shop.Products.SwapSequenceIndex.Down);
            //}
            BindData();
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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
             
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            if (bll.DeleteCategory(ID))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除淘宝分类(id=" + ID + ")成功", this);
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除淘宝分类(id=" + ID + ")失败", this);
            }
            BindData();
        }

        #region 批量处理

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int SNSCategoryId = Globals.SafeInt(this.SNSCategoryDropList.SelectedValue, 0);
            bool IsLoop = Globals.SafeBool(radlState.SelectedValue, false);
            if (bll.UpdateSNSCateList(idlist, SNSCategoryId, IsLoop))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量对应淘宝分类(id=" + SNSCategoryId + ")成功成功", this);
                MessageBox.ShowSuccessTip(this, "分类批量对应成功");
                Response.Redirect("TaoBaoCateList.aspx");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量对应淘宝分类(id=" + SNSCategoryId + ")失败成功", this);
                MessageBox.ShowFailTip(this, "分类批量对应失败！");
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

        #endregion 批量处理

        #endregion gridView

        #region 获取SNS分类的名称

        /// <summary>
        /// 获取SNS分类的名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetSNSCateName(object target)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                Maticsoft.BLL.SNS.Categories cateBll = new BLL.SNS.Categories();
                int SNSCateId = Common.Globals.SafeInt(target.ToString(), 0);
                Maticsoft.Model.SNS.Categories model = cateBll.GetModel(SNSCateId);
                if (model != null)
                {
                    str = model.Name;
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }

        #endregion 获取SNS分类的名称

        #region AjaxCallback
        private void DoCallback()
        {
            //DONE: 登录Check 及跳转
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "GetTaoBaoCateList":
                    GetTaoBaoCateList();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        protected void GetTaoBaoCateList()
        {
            JsonObject json = new JsonObject();
            int addCount = 0;
            int updateCount = 0;
            bll.ResetCategory(out addCount, out updateCount);
            json.Put("DATA", addCount + "|" + updateCount);
            json.Put("STATUS", "SUCCESS");
            Response.Write(json.ToString());
        }

        #endregion
    }
}