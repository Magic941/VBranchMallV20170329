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
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.SNS;

namespace Maticsoft.Web.SNS.PhotoTags
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.SNS.PhotoTags bll = new Maticsoft.BLL.SNS.PhotoTags();
        protected override int Act_PageLoad { get { return 125; } } //SNS_是否显示图片标签页面

        protected new int Act_DelData = 128;    //SNS_图片标签_删除图片标签
        protected new int Act_UpdateData = 127;    //SNS_图片标签_编辑图片标签
        protected new int Act_AddData = 126;    //SNS_图片标签_添加图片标签
        protected new int Act_DeleteList = 129;    //SNS_图片标签_批量删除图片标签

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList)!=-1)
                {
                    liDel.Visible = false;
                    lbtnDelete.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    tableAdd.Visible = false;
                }
                ShowInfo();
            }
        }

        public int TagID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.BLL.SNS.PhotoTags bll = new Maticsoft.BLL.SNS.PhotoTags();
            Maticsoft.Model.SNS.PhotoTags model = bll.GetModel(TagID);
            if (null != model)
            {
                this.txtTagName.Text = model.TagName;
                this.radlIsRecommand.SelectedValue = model.IsRecommand.ToString();
                this.radlStatus.SelectedValue = model.Status.ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除图片标签(id=" + idlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
        }

        #region gridView

        public void BindData()
        {
            #region
 
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[6].Visible = false;
            }
            #endregion gridView

            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSetSource = bll.GetList("TagName like '%" + txtKeyword.Text.TrimEnd() + "%'");
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "RecommandIndex")
            //{
            //    if (e.CommandArgument != null)
            //    {
            //        string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
            //        Model.SNS.EnumHelper.RecommendType isRecommend =
            //            Common.Globals.SafeEnum<Model.SNS.EnumHelper.RecommendType>(args[1], 0);
            //        if (isRecommend != Model.SNS.EnumHelper.RecommendType.Home)
            //        {
            //            bll.UpdateIsRecommand((int)Model.SNS.EnumHelper.RecommendType.Home, args[0]);
            //            gridView.OnBind();
            //        }
            //        else
            //        {
            //            bll.UpdateIsRecommand((int)Model.SNS.EnumHelper.RecommendType.None, args[0]);
            //            gridView.OnBind();
            //        }
            //    }
            //}
            if (e.CommandName == "Status")
            {
                if (e.CommandArgument != null)
                {
                    string[] args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Model.SNS.EnumHelper.Status status =
                       Common.Globals.SafeEnum<Model.SNS.EnumHelper.Status>(args[1], 0);
                    if (status != EnumHelper.Status.Enabled)
                    {
                        bll.UpdateStatus((int)Model.SNS.EnumHelper.Status.Enabled, args[0]);
                        gridView.OnBind();
                    }
                    else
                    {
                        bll.UpdateStatus((int)Model.SNS.EnumHelper.Status.Disabled, args[0]);
                        gridView.OnBind();
                    }
                }
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
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");

                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //#warning 代码生成警告：请检查确认真实主键的名称和类型是否正确
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            if (bll.Delete(ID))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除图片标签(id=" + ID + ")成功", this);
            }

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

                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
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

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string TagName = this.txtTagName.Text.Trim();
            if (TagName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "名称不能为空！");
                return;
            }
            Maticsoft.BLL.SNS.PhotoTags bll = new Maticsoft.BLL.SNS.PhotoTags();
            Maticsoft.Model.SNS.PhotoTags model = null;
            if (TagID > 0)
            {
                model = bll.GetModel(TagID);
                if (null != model)
                {
                    if (bll.Exists(model.TagID, TagName))
                    {
                        MessageBox.ShowServerBusyTip(this, "标签已存在！");
                        return;
                    }
                    model.TagName = TagName;
                    model.IsRecommand = Globals.SafeInt(this.radlIsRecommand.SelectedValue, 0);
                    model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                    model.CreatedDate = DateTime.Now;
                    if (bll.Update(model))
                    {
                        this.txtTagName.Text = "";
                        gridView.OnBind();
                        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "PhotoTags.aspx");
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                    }
                }
            }
            else
            {
                if (bll.Exists(TagName))
                {
                    MessageBox.ShowServerBusyTip(this, "标签已存在！");
                    return;
                }
                model = new Model.SNS.PhotoTags();
                model.TagName = TagName;
                model.IsRecommand = Globals.SafeInt(this.radlIsRecommand.SelectedValue, 0);
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                model.CreatedDate = DateTime.Now;
                if (bll.Add(model) > 0)
                {
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "增加图片标签成功", this);
                    this.txtTagName.Text = "";
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                    gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
                }
            }
        }

        public string GetStatus(object target)
        {
            //状态 0:不可用;1:可用
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "不可用";
                        break;

                    case 1:
                        str = "可用";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public string GetIsRecommand(object target)
        {
            //推荐到分类标签页 0:推荐;1:不推荐
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "推荐";
                        break;

                    case 1:
                        str = "不推荐";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        protected void btnRecommand_Click(object sender, EventArgs e)
        {
            //string idlist = GetSelIDlist();
            //if (idlist.Trim().Length == 0) return;
            //if (this.dropIsRecommand.SelectedValue != "-1")
            //{
            //    if (bll.UpdateIsRecommand(Globals.SafeInt(this.dropIsRecommand.SelectedValue, 0), idlist))
            //    {
            //        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipBatchUpdateOK);
            //        gridView.OnBind();
            //    }
            //}
        }

        protected void dropStatus_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (this.dropStatus.SelectedValue != "-1")
            {
                if (bll.UpdateStatus(Globals.SafeInt(this.dropStatus.SelectedValue, 0), idlist))
                {
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量更新图片标签(TagsID=" + idlist + ")成功", this);
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipBatchUpdateOK);
                    gridView.OnBind();
                }
            }
        }
    }
}