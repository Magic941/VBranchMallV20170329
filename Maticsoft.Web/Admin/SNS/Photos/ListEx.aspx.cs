using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.SNS.Photos
{
    public partial class ListEx : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 590; } } //SNS_图片分享管理_列表页
        protected new int Act_UpdateData = 591;    //SNS_图片分享管理_编辑数据
        protected new int Act_DelData = 21;    //社区_分享_晒货管理_删除数据

        private Maticsoft.BLL.SNS.Photos bll = new Maticsoft.BLL.SNS.Photos();
        private Maticsoft.BLL.Members.SiteMessage siteBll = new SiteMessage();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                    Button1.Visible = false;
                }
                if (CateType == 0)//表示是未分类图片
                {
                    //this.txtCateParent.Visible = false;
                    this.PhotoCate.Visible = false;
                    this.txtPhotoCate.Visible = true;
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

            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("  (Type <>3 or Type is Null)");
            //int type = Common.Globals.SafeInt(ddType.SelectedValue, -1);
            //if (type != -1)
            //{
            //    strWhere.AppendFormat(" Type =" + type);
            //}
            //else
            //{
            //   
            //}
            int PhotoCateId = Globals.SafeInt(this.PhotoCate.SelectedValue, 0);
            if (CateType == 0)//表示是未分类图片
            {
                PhotoCateId = -1;
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  (PhotoName like '%{0}%' or CreatedNickName like '%{0}%')", txtKeyword.Text.Trim());
            }
            if (this.chkRecomend.Checked)
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                if (this.chkRecomend2.Checked)
                {
                  
                    strWhere.AppendFormat("  IsRecomend<>0 ");
                }
                else
                {
                    strWhere.AppendFormat("  IsRecomend=1 ");
                }
            }
            else if (chkRecomend2.Checked)
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  IsRecomend=2 ");
            }
            if (this.dropState.SelectedIndex > 0)
            {
                int state = Common.Globals.SafeInt(this.dropState.SelectedValue, 0);
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  Status =" + state);
            }
            if (!String.IsNullOrWhiteSpace(this.txtFrom.Value) && Common.PageValidate.IsDateTime(this.txtFrom.Value))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("   CreatedDate >'" + this.txtFrom.Value + "' ");
            }

            if (!String.IsNullOrWhiteSpace(this.txtTo.Value) && Common.PageValidate.IsDateTime(this.txtTo.Value))
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.AppendFormat(" and");
                }
                strWhere.AppendFormat("  CreatedDate <'" + this.txtTo.Value + "' ");
            }

            this.AspNetPager1.RecordCount = bll.GetRecordCountEx(strWhere.ToString(), PhotoCateId);
            DataListPhoto.DataSource = bll.GetListByPageEx(strWhere.ToString(), PhotoCateId, "", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            DataListPhoto.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
      
        protected void DataListPhoto_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
                if (e.CommandArgument != null)
                {
                    int id = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    int result = 0;
                    DataSet ds = bll.DeleteListEx(id.ToString(), out result,true,CurrentUser.UserID);
                    if (result > 0)
                    {
                        if (ds != null)
                        {
                            //物理删除文件
                            PhysicalFileInfo(ds.Tables[0]);
                        }
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "删除图片(PhotoID=" + id + ")成功", this);
                     
                        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                        return;
                    }
                }
                BindData();
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < DataListPhoto.Items.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)DataListPhoto.Items[i].FindControl("ckPhoto");
                HiddenField hfPhotoId = (HiddenField)DataListPhoto.Items[i].FindControl("hfPhotoId");
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

        #region 公共属性和方法

        public int CateType
        {
            get
            {
                int _type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    _type = Globals.SafeInt(Request.Params["type"], 1);
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
                        return "通过审核";
                    case 1:
                        return "撤消审核";
                    default:
                        return "通过审核";
                }
            }
            return str;
        }

        protected string GetRecomendIndex(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int Status = Common.Globals.SafeInt(target.ToString(), 0);
                switch (Status)
                {
                    case 0:
                        return "推荐首页";
                    case 1:
                        return "取消首页";
                    default:
                        return "推荐首页";
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
                        return "推荐频道首页";
                    case 2:
                        return "取消频道首页";
                    default:
                        return "推荐频道首页";
                }
            }
            return str;
        }

        #endregion

        #region 按钮事件

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int result = 0;
            DataSet ds = bll.DeleteListEx(idlist, out result,true,CurrentUser.UserID);
            if (result > 0)
            {
                if (ds != null)
                {
                    //物理删除文件
                    PhysicalFileInfo(ds.Tables[0]);
                }
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除图片(PhotoIDs=" + idlist + ")成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
                return;
            }
            BindData();
        }

        protected void radlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx?type=" + CateType);
        }

        //批量转移
        protected void btnMove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            int SNSPhotoCateId = Globals.SafeInt(this.PhotoCategory.SelectedValue, 0);
            if (SNSPhotoCateId == 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "请选择类别");
            }
            if (!bll.UpdateCateList(idlist, SNSPhotoCateId))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量归类失败");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量归类成功");
            }
            BindData();
        }

        protected void btnRecomend_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!bll.UpdateRecomendList(idlist, 1))
            {

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量推荐到首页失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + idlist + ")到首页失败", this);
            }
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + idlist + ")到首页成功", this);
            BindData();
        }

        protected void btnRecomend2_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!bll.UpdateRecomendList(idlist, 2))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "批量推荐到频道首页失败");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + idlist + ")到首页失败", this);
            }
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量推荐图片(PhotoIDs=" + idlist + ")到首页成功", this);
            BindData();
        }

        protected void btnNoRecomend_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!bll.UpdateRecomendList(idlist, 0))
            {

                Maticsoft.Common.MessageBox.ShowFailTip(this, "批量取消推荐失败");
            }
            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量取消推荐图片(PhotoIDs=" + idlist + ")到首页成功", this);
            BindData();
        }

        //批量转移
        //protected void btnMove_Click(object sender, EventArgs e)
        //{
        //    string idlist = GetSelIDlist();
        //    if (idlist.Trim().Length == 0) return;
        //    int SNSPhotoCateId = Globals.SafeInt(this.PhotoCategory.SelectedValue, 0);
        //    if (!bll.UpdateCateList(idlist, SNSPhotoCateId))
        //    {
        //        Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量转移分类失败");
        //    }
        //    gridView.OnBind();
        //}

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

        #endregion

        protected void DataListPhoto_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    HtmlGenericControl linkBtn = (HtmlGenericControl)e.Item.FindControl("spanbtnDel");
                    linkBtn.Visible = false;
                }
            }
        }

        #endregion
    }
}