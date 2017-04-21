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
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.StarRank
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 619; } } //SNS_达人排行管理_列表页
        protected new int Act_DelData = 620;    //SNS_达人排行管理_删除数据
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.SNS.StarRank bll = new Maticsoft.BLL.SNS.StarRank();
        Maticsoft.BLL.SNS.StarType starType = new BLL.SNS.StarType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                DataSet ds = starType.GetList(" Status=1");
                 this.ddStarType.DataSource = ds;
                 this.ddStarType.DataTextField = "TypeName";
                 this.ddStarType.DataValueField = "TypeID";
                 this.ddStarType.DataBind();

                 if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

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
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        //批量审核
        protected void btnUpdateState_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!bll.UpdateStateList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量审核达人排行（id=" + idlist + "）失败", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, "批量设置失败");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量审核达人排行（id=" + idlist + "）成功", this);
            }
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            #region

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[7].Visible = false;
            }


            #endregion gridView

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            int TypeId =Common.Globals.SafeInt(this.ddStarType.SelectedValue,-1);
            strWhere.AppendFormat("TypeId =" + TypeId);
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" and NickName like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = bll.GetList(strWhere.ToString());

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
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
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

        protected string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int Status = Common.Globals.SafeInt(target.ToString(), 0);
                switch (Status)
                {
                    case 0:
                        return "未审核";
                    case 1:
                        return "已审核";
                    case 2:
                        return "审核未通过";
                    default:
                        return "未审核";
                }
            }
            return str;
        }

        protected string GetStarType(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                Maticsoft.BLL.SNS.StarType cateBll = new BLL.SNS.StarType();
                int SNSCateId = Common.Globals.SafeInt(target.ToString(), 0);
                Maticsoft.Model.SNS.StarType model = cateBll.GetModel(SNSCateId);
                if (model != null)
                {
                    str = model.TypeName;
                }
                else
                {
                    str = "";
                }
            }
            return str;
        }
    }
}