/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;
using Maticsoft.Model.Settings;

namespace Maticsoft.Web.Admin.Settings.FilterWord
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 374; } } //设置_敏感词管理_列表页
        protected new int Act_AddData = 376;    //设置_敏感词管理_添加数据
        protected new int Act_UpdateData = 377;    //设置_敏感词管理_编辑数据
        protected new int Act_DelData = 378;    //设置_敏感词管理_删除数据
 
        private Maticsoft.BLL.Settings.FilterWords bll = new Maticsoft.BLL.Settings.FilterWords();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    btnSave.Visible = false;
                    liAdd.Visible = false;
                }
                if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int  typeId = Common.Globals.SafeInt(this.ddSelect.SelectedValue,0);
            string replace = this.txtReplace.Text;
            if (bll.UpdateActionType(idlist, typeId,replace))
            {
                MessageBox.ShowSuccessTip(this, "批量设置成功!");
            }
            gridView.OnBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Settings.FilterWords model=new FilterWords();
            model.WordPattern = this.tName.Text;
            model.ActionType = Common.Globals.SafeInt(this.ddlSelectType.SelectedValue, 0);
            model.RepalceWord = this.txtAddReplace.Text;
            if (bll.Exists(model.WordPattern))
            {
                MessageBox.ShowFailTip(this, "该关键字已存在，请不要重复添加！");
                return;
            }
            if (bll.Add(model)>0)
            {
                gridView.OnBind();
            }
            else
            {
                MessageBox.ShowFailTip(this, "添加失败，请稍候再试！");
            }
          
        }

       
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.DeleteList(idlist);
            MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }
        

        #region gridView

        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat("WordPattern like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = bll.GetList(-1, strWhere.ToString(), " FilterId desc");
            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
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
               
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl modifybtn = (HtmlGenericControl)e.Row.FindControl("btnModify");
                    modifybtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
            bll.ClearCache();
            gridView.OnBind();
        }

        public void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridView.EditIndex = e.NewEditIndex;
            String action =  gridView.Rows[e.NewEditIndex].Cells[1].Text;
            // For this example, cancel the edit operation if the user attempts
            // to edit the record of a customer from the Unites States. 

            
            gridView.OnBind();
        }

        public void gridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridView.EditIndex = -1;
            gridView.OnBind();
        }

        public void gridView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = (int)gridView.DataKeys[e.RowIndex].Value;
            string WordPattern = (gridView.Rows[e.RowIndex].Cells[0].Controls[0] as TextBox).Text;
           
            string RepalceWord = (gridView.Rows[e.RowIndex].Cells[3].Controls[0] as TextBox).Text;
            if (string.IsNullOrWhiteSpace(WordPattern))
            {
                MessageBox.ShowFailTip(this, "请输入敏感词！");
                return;
            }

            Model.Settings.FilterWords model = new Model.Settings.FilterWords();
            model.WordPattern = WordPattern;
            model.FilterId = id;
            //model.IsForbid = IsForbid;
            //model.IsMod = IsMod;
            model.RepalceWord = RepalceWord;
            bll.Update(model);
            bll.ClearCache();
            gridView.EditIndex = -1;
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

        #region 辅助方法

        public string GetActionText(object target)
        {
            string str = "";
            int type = Common.Globals.SafeInt(target.ToString(), 0);
            switch (type)
            {
                case 0:
                    str = "禁止关键词";
                    break;
                case 1:
                    str = "审核关键词";
                    break;
                case 2:
                    str = "替换关键词";
                    break;
                default:
                     str = "禁止关键词";
                    break;
            }
            return str;

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
                case "Update":

                    writeText = Update();
                    break;
                default:
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }




        private string Update()
        {
            JsonObject json = new JsonObject();
            int filterId = Common.Globals.SafeInt(this.Request.Form["FilterId"], 0);
            int actionType = Common.Globals.SafeInt(this.Request.Form["ActionType"], 0);
            string word = Request.Params["Word"];
            string replaceWord = Request.Params["ReplaceWord"];
            Maticsoft.Model.Settings.FilterWords model = bll.GetModel(filterId);
            if (model == null)
            {
                  json.Put("STATUS", "FAILED");
            }
            model.ActionType = actionType;
            model.RepalceWord = replaceWord;
            model.WordPattern = word;

            if (bll.Update(model))
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