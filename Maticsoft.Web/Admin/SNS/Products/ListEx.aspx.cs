using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Members;
using Maticsoft.BLL.SNS;
using Maticsoft.Common;
using System.Text.RegularExpressions;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.SNS.Products
{
    public partial class ListEx : PageBaseAdmin
    {
        #region 公共属性和方法

        public int ProType
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
                        return "审核通过";
                    case 1:
                        return "取消审核";
                    case 2:
                        return "已下架";
                    default:
                        return "审核通过";
                }
            }
            return str;
        }

        protected string GetIsRecomend(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int Status = Common.Globals.SafeInt(target.ToString(), 0);
                switch (Status)
                {
                    case 0:
                        return "推荐到首页";
                    case 1:
                        return "取消推荐";

                    default:
                        return "推荐到首页";
                }
            }
            return str;
        }

        #endregion

        protected override int Act_PageLoad
        {
            get
            {
                switch (ProType)
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
        new protected int Act_DelData = 0;

        private Maticsoft.BLL.SNS.Products bll = new Maticsoft.BLL.SNS.Products();
        private Maticsoft.BLL.Members.SiteMessage siteBll = new SiteMessage();

        public string BasePath = "/SNS/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string area = ConfigSystem.GetValueByCache("MainArea");
                if (area == "SNS")
                {
                    BasePath = "/";
                }
                if (ProType == 1)
                {
                    this.txtProduct.Text = "未分类商品管理";
                   // this.txtCateParent.Visible = false;
                    this.SNSCate.Visible = false;
                    this.noneCategory.Visible = true;
                    Act_DelData = 20;
                }
                else
                {
                    Act_DelData = 19;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                    Button1.Visible = false;
                }
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
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
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}

            #endregion gridView

            StringBuilder strWhere = new StringBuilder();
            int SNSCateId = Globals.SafeInt(this.SNSCate.SelectedValue, 0);
            if (ProType == 0)//已分类商品
            {
                if (SNSCateId == 0)
                {
                    strWhere.AppendFormat("  CategoryID is not null and CategoryID>0");
                }
            }
            else//未分类商品
            {
                strWhere.AppendFormat("  (CategoryID is null or CategoryID=0)");
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

            this.AspNetPager1.RecordCount = bll.GetRecordCountEx(strWhere.ToString(), SNSCateId);
            DataListProduct.DataSource = bll.GetListByPageEx(strWhere.ToString(), SNSCateId, "CreatedDate", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            DataListProduct.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void DataListProduct_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (e.CommandArgument != null)
                {
                    int id = Globals.SafeInt(e.CommandArgument.ToString(), 0);
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

                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除商品(ProductId=" + id + ")成功", this);
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除商品(ProductId=" + id + ")失败", this);
                        return;
                    }
                }
                BindData();
            }
        }

        #region 物理删除文件

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
            Maticsoft.Web.Components.FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS, path);
        }

        #endregion 物理删除文件


        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < DataListProduct.Items.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)DataListProduct.Items[i].FindControl("ckProduct");
                HiddenField hfPhotoId = (HiddenField)DataListProduct.Items[i].FindControl("hfProduct");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (hfPhotoId.Value != null)
                    {
                        idlist += hfPhotoId.Value + ",";
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


        #region 按钮处理

        protected void radlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx?type=" + ProType);
        }

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
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);

                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除商品(ProductIds=" + idlist + ")成功", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除商品(ProductIds=" + idlist + ")失败", this);
                return;
            }
            BindData();
        }

        protected void btnDeleteAll_Click(object sender, EventArgs e)
        {
            string SNS_AllIds = Session["SNS_AllIds"] == null ? "" : Session["SNS_AllIds"].ToString();
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
            BindData();
        }

        protected void btnMove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int SNSCateId2 = Globals.SafeInt(this.SNSCate2.SelectedValue, 0);
            if (!bll.UpdateCateList(idlist, SNSCateId2))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "批量转移分类失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量转移商品(ProductIds=" + idlist + ")失败", this);
            }
            BindData();
        }
        #endregion

        protected void DataListProduct_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LinkButton linkBtn = (LinkButton)e.Item.FindControl("lbtnDel");
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    linkBtn.Visible = false;
                }
            }
        }

    }
}