/**
* List.cs
*
* 功 能： N/A
* 类 名： List
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01						   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Tags
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.SNS.Tags bll = new Maticsoft.BLL.SNS.Tags();
        protected override int Act_PageLoad { get { return 139; } } //运营管理_是否显示商品标签管理页面

        protected new int Act_DelData = 142;    //运营管理_商品标签_删除商品标签
        protected new int Act_UpdateData = 141;    //运营管理_商品标签_编辑商品标签
        protected new int Act_AddData = 140;    //运营管理_商品标签_添加商品标签
        protected new int Act_DeleteList = 143;    //运营管理_商品标签_批量删除商品标签

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

                //if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                //{
                //    LiAdd.Visible = false;
                //}

                TagTypeBindData();
                ShowInfo();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string TagName = this.txtTagName.Text.Trim();
            if (TagName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "标签的名称不能为空！");
                return;
            }
            int TypeID = Globals.SafeInt(this.dropTypeId.SelectedValue, 0);
            if (TypeID == 0)
            {
                MessageBox.ShowServerBusyTip(this, "请选择类别！");
                return;
            }

            Maticsoft.Model.SNS.Tags model = null;
            if (TagID > 0)
            {
                model = bll.GetModel(TagID);
                if (null != model)
                {
                    model.TagName = TagName;
                    model.TypeId = TypeID;
                    model.IsRecommand = int.Parse(this.radlIsRecommand.SelectedValue);
                    model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                    if (bll.Update(model))
                    {
                        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "List.aspx");
                    }
                    else
                    {
                        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateError);
                    }
                }
            }
            else
            {
                model = new Model.SNS.Tags();
                model.TagName = TagName;
                model.TypeId = TypeID;
                model.IsRecommand = int.Parse(this.radlIsRecommand.SelectedValue);
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                if (bll.Add(model) > 0)
                {
                    LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "增加商品标签成功", this);
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
                }
            }
        }

        protected int TagID
        {
            get
            {
                int id = 0;
                string strId = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strId))
                {
                    id = Globals.SafeInt(strId, 0);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.SNS.Tags model = bll.GetModel(TagID);
            if (null != model)
            {
                this.txtTagName.Text = model.TagName;
                this.dropTypeId.SelectedValue = model.TypeId.ToString();
                this.radlIsRecommand.SelectedValue = model.IsRecommand.ToString();
                this.radlStatus.SelectedValue = model.Status.ToString();
            }
        }

        public void TagTypeBindData()
        {
            Maticsoft.BLL.SNS.TagType tagTypeBll = new BLL.SNS.TagType();
            this.dropTypeId.DataSource = tagTypeBll.GetList("Cid>=0");
            this.dropTypeId.DataTextField = "TypeName";
            this.dropTypeId.DataValueField = "ID";
            this.dropTypeId.DataBind();
            this.dropTypeId.Items.Insert(0, new ListItem("--请选择--", "0"));
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
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除商品标签(id="+idlist+")成功", this);
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
                gridView.Columns[6].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[7].Visible = false;
            }
            #endregion gridView
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" TagName like '%{0}%'", txtKeyword.Text.Trim());
            }

            if (strWhere.Length > 1)
            {
                strWhere.Append(" and  ");
            }
            strWhere.AppendFormat(" TypeId in (select ID from SNS_TagType where Cid>=0)");

            gridView.DataSetSource = bll.GetListEx(strWhere.ToString());
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
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
           

                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
            MessageBox.ShowSuccessTip(this, "删除成功！");
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

        public string GetStatus(object target)
        {
            //状态 1:不可用 ：2可用
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

        public string IsRecommand(object target)
        {
            //状态 0:推荐 ：1不推荐
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

        /// <summary>
        /// 推荐到分类标签页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropIsRecommand_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (this.dropIsRecommand.SelectedValue != "-1")
            {
                if (bll.UpdateIsRecommand(Globals.SafeInt(this.dropIsRecommand.SelectedValue, 0), idlist))
                {  
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipBatchUpdateOK);
                    gridView.OnBind();
                }
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dropStatus_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (this.dropStatus.SelectedValue != "-1")
            {
                if (bll.UpdateStatus(Globals.SafeInt(this.dropStatus.SelectedValue, 0), idlist))
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipBatchUpdateOK);
                    gridView.OnBind();
                }
            }
        }
    }
}