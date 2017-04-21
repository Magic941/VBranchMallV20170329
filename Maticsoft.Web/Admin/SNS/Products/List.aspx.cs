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
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.SysManage;
using Maticsoft.Common;
using Maticsoft.BLL.Members;
using System.Linq;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.SNS.Products
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        public int type
        {
            get
            {
                int _type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    _type = Globals.SafeInt(Request.Params["type"], 0);
                }
                return _type;
            }
        }
        protected override int Act_PageLoad
        {
            get
            {
                switch (type)
                {
                    case 1:
                        return 594;    //SNS_未分类商品分享管理_列表页
                    case 2:
                        return 595;  //SNS_已分类商品分享管理_列表页
                    default:
                        return 595;
                }
            }
        }
        private Maticsoft.BLL.SNS.Products bll = new Maticsoft.BLL.SNS.Products();
        private Maticsoft.BLL.Members.SiteMessage siteBll = new SiteMessage();
        public string TypeStr = "已分类商品管理";
        new protected int Act_DelData = 0;
        public string BasePath = "/SNS/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.tType.Value = type.ToString();
                if (type > 0)
                {
                    this.txtProduct.Text = "未分类商品管理";
                    this.txtCateParent.Visible = false;
                    this.SNSCate.Visible = false;
                    Act_DelData = 20;
                }
                else
                {
                    Act_DelData = 19;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    btnDelete.Visible = false;
                }
                string area = ConfigSystem.GetValueByCache("MainArea");
                if (area == "SNS")
                {
                    BasePath = "/";
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
            int result = 0;
            DataSet ds = bll.DeleteListEx(idlist, out result, true, CurrentUser.UserID);
            if (result > 0)
            {
                if (ds != null)
                {
                    //物理删除文件
                    PhysicalFileInfo(ds.Tables[0]);
                }
                MessageBox.ShowSuccessTip(this, "批量删除成功");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除商品(id=" + idlist + ")成功", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "批量删除失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除商品(id=" + idlist + ")失败", this);
                return;
            }
            gridView.OnBind();
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
        string  SNS_AllIds=Session["SNS_AllIds"]==null?"": Session["SNS_AllIds"].ToString();
            if (String.IsNullOrWhiteSpace(SNS_AllIds))
            {
                return;
            }
            int result = 0;
            DataSet ds = bll.DeleteListEx(SNS_AllIds, out result, true, CurrentUser.UserID);
            if (result > 0)
            {
                if (ds != null)
                {
                    //物理删除文件
                    PhysicalFileInfo(ds.Tables[0]);
                }
                MessageBox.ShowSuccessTip(this, "一键删除成功");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "一键删除商品(id=" + SNS_AllIds + ")成功", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "一键删除失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "一键删除商品(id=" + SNS_AllIds + ")失败", this);
                return;
            }
            gridView.OnBind();
        }


        protected void btnMove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int SNSCateId2 = Globals.SafeInt(this.SNSCate2.SelectedValue, 0);
            if (!bll.UpdateCateList(idlist, SNSCateId2))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量转移商品(id=" + idlist + ")分类成功", this);
                Maticsoft.Common.MessageBox.ShowFailTip(this, "批量转移分类失败");
            }
            gridView.OnBind();
        }

        //批量推荐到首页
        protected void ddAction_Changed(object sender, EventArgs e)
        {
            string strErr = string.Empty;
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            switch (this.ddAction.SelectedValue)
            {
                case "1":
                    if (!bll.UpdateRecomendList(idlist, 1))
                    {
                        Maticsoft.Common.MessageBox.ShowFailTip(this, "批量推荐到首页失败");
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐商品(ProductIds=" + idlist + ")到首页失败", this);
                    }
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量取消推荐成功");
                    break;
                case "2":
                    if (!bll.UpdateRecomendList(idlist, 0))
                    {
                        Maticsoft.Common.MessageBox.ShowFailTip(this, "批量取消推荐失败");
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "取消推荐商品(ProductIds=" + idlist + ")到首页失败", this);
                    }
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量取消推荐成功");
                    break;
                case "3":
                    if (!bll.UpdateStatusList(idlist, 2))
                    {
                        Maticsoft.Common.MessageBox.ShowFailTip(this, "批量下架失败");
                    }
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量下架成功");
                    break;
                default:
                    break;
            }
            BindData();
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
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[4].Visible = false;
            }

            #endregion gridView

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            int SNSCateId = Globals.SafeInt(this.SNSCate.SelectedValue, 0);
            if (type > 0)
            {
                strWhere.AppendFormat("  (CategoryID is null or CategoryID=0)");
            }
            else
            {
                if (SNSCateId == 0)
                {
                    strWhere.AppendFormat("  CategoryID is not null and CategoryID>0");
                }
            }

            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" (ProductName like '%{0}%' or CreatedNickName like '%{0}%')", txtKeyword.Text.Trim());
            }

            if (this.chkRecomend.Checked)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" IsRecomend=1");
            }
            if (this.dropState.SelectedIndex > 0)
            {
                int state = Common.Globals.SafeInt(this.dropState.SelectedValue, 0);
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" Status =" + state);
            }
            if (!String.IsNullOrWhiteSpace(this.txtFrom.Value) && Common.PageValidate.IsDateTime(this.txtFrom.Value))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreatedDate >'" + this.txtFrom.Value + "' ");
            }

            if (!String.IsNullOrWhiteSpace(this.txtTo.Value) && Common.PageValidate.IsDateTime(this.txtTo.Value))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat(" CreatedDate <'" + this.txtTo.Value + "' ");
            }

            ds = bll.GetListEx(strWhere.ToString(), SNSCateId);

            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSetSource = ds;
            List<string> AllIds_List = new List<string>();
            if (ds.Tables[0] != null)
            {
                int rowsCount = ds.Tables[0].Rows.Count;
                if (rowsCount > 0)
                {
                    for (int n = 0; n < rowsCount; n++)
                    {
                        DataRow row = ds.Tables[0].Rows[n];
                        if (row != null)
                        {
                            if (row["ProductID"] != null && row["ProductID"].ToString() != "")
                            {
                                AllIds_List.Add(row["ProductID"].ToString());
                            }
                        }
                    }
                }
               string AllIds = String.Join(",", AllIds_List);
                Session["SNS_AllIds"] = AllIds;
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
            int id = (int)gridView.DataKeys[e.RowIndex].Value;
            int result = 0;
            DataSet ds = bll.DeleteListEx(id.ToString(), out result, true, CurrentUser.UserID);
            if (result > 0)
            {
                if (ds != null)
                {
                    //物理删除文件
                    PhysicalFileInfo(ds.Tables[0]);
                }

                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                return;
            }
            gridView.OnBind();
        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["TargetImageURL"] != null && dt.Rows[n]["TargetImageURL"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["TargetImageURL"].ToString());
                    }
                    if (dt.Rows[n]["ThumbImageUrl"] != null && dt.Rows[n]["ThumbImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ThumbImageUrl"].ToString());
                    }
                    if (dt.Rows[n]["NormalImageUrl"] != null && dt.Rows[n]["NormalImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["NormalImageUrl"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        private void DeletePhysicalFile(string path)
        {
            if (!path.StartsWith("http://"))
            {
                Common.FileManage.DeleteFile(Server.MapPath(path));
            }
            else
            {
                Maticsoft.BLL.SysManage.UpYunManager.DeleteImage(path, ApplicationKeyType.SNS);
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

        protected string GetCategoryName(object target)
        {
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
                        return "已下架";
                    case 3:
                        return "分类未明确";
                    case 4:
                        return "分类已明确";
                }
            }
            return str;
        }

        protected void radlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("listEx.aspx?type=" + type);
        }
    }
}