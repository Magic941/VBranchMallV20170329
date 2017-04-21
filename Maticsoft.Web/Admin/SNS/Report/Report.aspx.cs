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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using EnumHelper = Maticsoft.Model.Ms.EnumHelper;

namespace Maticsoft.Web.Admin.SNS.Report
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 155; } } //运营管理_是否显示举报管理页面

        protected new int Act_DelData = 156;    //运营管理_举报管理_删除举报信息
        protected new int Act_DeleteList = 157;    //运营管理_举报管理_批量删除举报信息
        protected new int Act_ApproveList = 158;    //运营管理_举报管理_批量审核举报信息

        private Maticsoft.BLL.SNS.Report bll = new Maticsoft.BLL.SNS.Report();
        private Maticsoft.BLL.SNS.Posts Postbll = new Maticsoft.BLL.SNS.Posts();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList) != -1)
                {
                    btnDelete.Visible = false;
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_ApproveList)) && GetPermidByActID(Act_ApproveList) != -1)
                {
                    btnAlreadyDone.Visible = false;
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
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[9].Visible = false;
            }
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if ((Common.Globals.SafeInt(rdostatus.SelectedValue, -1) > -1))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("  SNSR.Status={0}", rdostatus.SelectedValue);
            }
            if ((Common.Globals.SafeInt(rdoType.SelectedValue, -1) > -1))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("  TargetType={0}", rdoType.SelectedValue);
            }
            if (!string.IsNullOrWhiteSpace(strWhere.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    strWhere.AppendFormat(" AND CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(this.txtName.Text))
                {
                    strWhere.AppendFormat(" CreatedNickName  like '%{0}%'", this.txtName.Text);
                }
            }
            if (txtKeyWord.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }

                strWhere.AppendFormat("Description like '%{0}%'", txtKeyWord.Text.Trim());
            }

            ds = bll.GetSearchList(strWhere.ToString());
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
                LinkButton lbtnDel = (LinkButton)e.Row.FindControl("lbtnDel");
                LinkButton lbtnDelPost = (LinkButton)e.Row.FindControl("lbtnDelPost");

                Literal litShow = (Literal)e.Row.FindControl("litShow");
                object obj1 = DataBinder.Eval(e.Row.DataItem, "TargetType");
                object pid = DataBinder.Eval(e.Row.DataItem, "ID");
                object objID = DataBinder.Eval(e.Row.DataItem, "TargetID");
                if (obj1 != null)
                {
                   
                }

                //<span style="color: rgb(0, 100, 0); ">已处理</span>
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

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "Status")
            //{
            //    if (e.CommandArgument != null)
            //    {
            //        int Id = 0;
            //        string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            //        Id = Common.Globals.SafeInt(Args[0], 0);
            //        int Status = Common.Globals.SafeInt(Args[1], 0);

            //        if (Status == 0)
            //        {
            //            bll.UpdateReportStatus(1, Id);
            //            SendMsg();
            //            gridView.OnBind();
            //        }
            //    }
            //}
            //if (e.CommandName == "DeleteByType")
            //{
            //    if (e.CommandArgument != null)
            //    {
            //        Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            //        Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
            //        int result1 = 0, result2 = 0;
            //        string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
            //        int Id = Globals.SafeInt(Args[2], 0);
            //        if (Args[0] == "0")
            //        {
            //            Postbll.DeleteListByNormalPost(Args[0]);
            //        }
            //        else if (Args[0] == "1")
            //        {
            //            DataSet ds = photoBll.DeleteListEx(Args[1], out result1);
            //            if (ds != null)
            //            {
            //                PhysicalFileInfo(ds.Tables[0]);
            //            }
            //        }
            //        else
            //        {
            //            DataSet ds = productBll.DeleteListEx(Args[1], out result2);
            //            if (ds != null)
            //            {
            //                PhysicalFileInfo(ds.Tables[0]);
            //            }
            //        }
            //        bll.UpdateReportStatus(1, Id);
            //        gridView.OnBind();
            //    }
            //}
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
            Maticsoft.Web.Components.FileHelper.DeleteFile(EnumHelper.AreaType.SNS, path);
        }

        #endregion 物理删除文件

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

        #endregion gridView

        public string GetTargetType(object target)
        {
            //举报内容的类型(0:动态或者1:图片或者2:商品)
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "动态";
                        break;

                    case 1:
                        str = "图片";
                        break;

                    case 2:
                        str = "商品";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        public string GetStatus(object target)
        {
            //状态 1:举报属实已处理 ：2虚假举报，已忽略处理，3：举报信息审核中
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (Globals.SafeInt(target.ToString(), -1))
                {
                    case 0:
                        str = "未处理";
                        break;

                    case 1:
                        str = "举报属实已删除";
                        break;

                    case 2:
                        str = "虚假举报已忽略";
                        break;

                    case 3:
                        str = "举报内容核实中";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        /// <summary>
        ///批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量删除举报（id=" + idlist + "）成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
        }

        //批量已处理
        protected void btnAlreadyDone_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateReportStatus(1, idlist))
            {
                SendMsg();
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "批量处理举报（id=" + idlist + "）成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量处理成功！");
                gridView.OnBind();
            }
        }

        private void SendMsg()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    HiddenField HiddenField_UserId = (HiddenField)gridView.Rows[i].FindControl("HiddenField_UserId");
                    if (HiddenField_UserId != null)
                    {
                        idlist += HiddenField_UserId.Value + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }

            Maticsoft.Model.Members.SiteMessage SitemMsgModel = new Model.Members.SiteMessage();
            Maticsoft.BLL.Members.SiteMessage SiteMsgBll = new BLL.Members.SiteMessage();

            SitemMsgModel.Title = "系统管理员通知";
            SitemMsgModel.Content = "您好，您的举报信息我们已经收到并处理，非常感谢您对我们工作的支持。";
            SitemMsgModel.SenderID = CurrentUser.UserID;
            SitemMsgModel.SendTime = DateTime.Now;
            SitemMsgModel.ReaderIsDel = false;
            SitemMsgModel.ReceiverIsRead = false;
            SitemMsgModel.SenderIsDel = false;
            string[] users = idlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string user in users)
            {
                SitemMsgModel.ReceiverID = int.Parse(user);
                SiteMsgBll.Add(SitemMsgModel);
            }
        }

        private string GetSelPostIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    HiddenField HiddenField_PostId = (HiddenField)gridView.Rows[i].FindControl("HiddenField_PostId");
                    HiddenField HiddenField_TagTypeId = (HiddenField)gridView.Rows[i].FindControl("HiddenField_TagTypeId");
                    if (HiddenField_PostId != null)
                    {
                        idlist += HiddenField_TagTypeId.Value + "_" + HiddenField_PostId.Value + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }

            return idlist;
        }

        private void BatchActionManage(string idlist)
        {
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
            int result1 = 0, result2 = 0;
            string[] Ids = idlist.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in Ids)
            {
                string[] tagInfo = item.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                if (tagInfo[0] == "0")
                {
                    Postbll.DeleteListByNormalPost(tagInfo[1],true,CurrentUser.UserID);
                }
                else if (tagInfo[0] == "1")
                {
                    DataSet ds = photoBll.DeleteListEx(tagInfo[1], out result1,true,CurrentUser.UserID);
                    if (ds != null)
                    {
                        PhysicalFileInfo(ds.Tables[0]);
                    }
                }
                else
                {
                    DataSet ds = productBll.DeleteListEx(tagInfo[1], out result2);
                    if (ds != null)
                    {
                        PhysicalFileInfo(ds.Tables[0]);
                    }
                }
            }

            //Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            //Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
            //int result1 = 0, result2 = 0;
            //if (rdoType.SelectedValue == "0")
            //{
            //    Postbll.DeleteListByNormalPost(idlist);
            //}
            //else if (rdoType.SelectedValue == "1")
            //{
            //    DataSet ds = photoBll.DeleteListEx(idlist, out result1);
            //    if (ds != null)
            //    {
            //        PhysicalFileInfo(ds.Tables[0]);
            //    }
            //}
            //else
            //{
            //    DataSet ds = productBll.DeleteListEx(idlist, out result2);
            //    if (ds != null)
            //    {
            //        PhysicalFileInfo(ds.Tables[0]);
            //    }
            //}
        }

        protected void btnReportTrue_Click(object sender, EventArgs e)
        {
            //if (rdoType.SelectedValue == "-1")
            //{
            //    MessageBox.ShowFailTip(this, "请选择检举内容类型！");
            //    return;
            //}
            string idlist = GetSelPostIDlist();
            if (idlist.Trim().Length == 0) return;

            string postIdList = GetSelIDlist();
            if (postIdList.Trim().Length == 0) return;

            BatchActionManage(idlist);
            bll.UpdateReportStatus(1, postIdList);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量处理成功！");
            gridView.OnBind();
        }

        protected void btnReportFalse_Click(object sender, EventArgs e)
        {
            //if (rdoType.SelectedValue == "-1")
            //{
            //    MessageBox.ShowFailTip(this, "请选择检举内容类型！");
            //    return;
            //}
            string idlist = GetSelPostIDlist();
            if (idlist.Trim().Length == 0) return;

            string postIdList = GetSelIDlist();
            if (postIdList.Trim().Length == 0) return;
            BatchActionManage(idlist);
            bll.UpdateReportStatus(2, postIdList);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量处理成功！");
            gridView.OnBind();
        }

        protected void btnReportUnKnow_Click(object sender, EventArgs e)
        {
            //if (rdoType.SelectedValue == "-1")
            //{
            //    MessageBox.ShowFailTip(this, "请选择检举内容类型！");
            //    return;
            //}
            string postIdList = GetSelIDlist();
            if (postIdList.Trim().Length == 0) return;

            string idlist = GetSelPostIDlist();
            if (idlist.Trim().Length == 0) return;
            BatchActionManage(idlist);
            bll.UpdateReportStatus(3, postIdList);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量处理成功！");
            gridView.OnBind();
        }
    }
}