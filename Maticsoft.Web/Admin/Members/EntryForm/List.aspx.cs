/**
* EntryForm.cs
*
* 功 能： [N/A]
* 类 名： EntryForm
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
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

namespace Maticsoft.Web.Ms.EntryForm
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 86; } } //客服管理_是否显示在线报名页面
        protected new int Act_DeleteList = 87;    //客服管理_在线报名_批量删除在线报名信息
        protected new int Act_UpdateData = 88;    //客服管理_在线报名_编辑在线报名信息

        private Maticsoft.BLL.Ms.EntryForm bll = new Maticsoft.BLL.Ms.EntryForm();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                //if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                //{
                //    liAdd.Visible = false;
                //}
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

        #region gridView

        public void BindData()
        {
            #region

            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    return;
            //}
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            //if (user.HasPermissionID(PermId_Modify))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[13].Visible = false;
            }
            #endregion gridView

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" UserName like '%{0}%'", txtKeyword.Text.Trim());
            }
            if (this.dropState.SelectedValue != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.AppendFormat(" AND State={0} ", this.dropState.SelectedValue);
                }
                else
                {
                    strWhere.AppendFormat(" State={0} ", this.dropState.SelectedValue);
                }
            }
            ds = bll.GetList(0, strWhere.ToString(), " Id desc  ");

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

        #region 获取处理状态

        /// <summary>
        /// 获取处理状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetState(object target)
        {
            //0.未处理；1：已处理
            string state = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        state = Resources.Site.Untreated;
                        break;

                    case "1":
                        state = Resources.Site.Processed;
                        break;
                    default:
                        break;
                }
            }
            return state;
        }
        /// <summary>
        /// 获取申请健康店类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetApplyType(object target)
        {
            //0.分销店；1：服务店
            string state = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        state = "分销店";
                        break;

                    case "1":
                        state = "服务店";
                        break;
                    default:
                        break;
                }
            }
            return state;
        }
        #endregion

        #region 获取性别信息

        /// <summary>
        /// 获取性别信息
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetSex(object target)
        {
            //0.男；1：女
            string state = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString().Trim())
                {
                    case "0":
                        state = Resources.Site.SexMale;
                        break;

                    case "1":
                        state = Resources.Site.SexWoman;
                        break;
                    default:
                        break;
                }
            }
            return state;
        }

        #endregion

        #region
        public string GetAppStatus(object target)
        {
           //0:处理中；1：通过   2:未通过
            string AppStatus = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        AppStatus = "处理中";
                        break;

                    case "1":
                        AppStatus = "通过";
                        break;
                    case "2":
                        AppStatus = "未通过";
                        break;
                    default:
                        AppStatus = "未知状态";
                        break;
                }
            }
           return AppStatus;
        }
        #endregion

        public string GetRegionNameByRID(object target)
        {
            Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
            string regionName = "";
            if (null != target && "" != target.ToString())
            {
                string rid = target.ToString();
                if (PageValidate.IsNumber(rid))
                {
                    regionName = bll.GetRegionNameByRID(int.Parse(rid));
                }
            }
            return regionName;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        protected void btnBatchManage_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;

            if (bll.UpdateList(idlist, " State= 1"))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量处理成功！");
                gridView.OnBind();
            }
        }
    }
}