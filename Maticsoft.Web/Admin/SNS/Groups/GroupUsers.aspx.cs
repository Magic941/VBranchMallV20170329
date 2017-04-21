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
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.SNS.GroupUsers
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 570; } } //SNS_小组成员_列表页
        protected new int Act_DelData = 571;    //SNS_小组成员_删除数据
		Maticsoft.BLL.SNS.GroupUsers bll = new Maticsoft.BLL.SNS.GroupUsers();
        protected int groupid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
                
            }
        }

        public int GroupId
        {
            get
            {
                int groupid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["GroupID"]))
                {
                    groupid = Globals.SafeInt(Request.Params["GroupID"], 0);
                }
                return groupid;
            }
          
        
        }
        protected void DataListUser_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                LinkButton delbtn = (LinkButton)e.Item.FindControl("lbtnDel");
                delbtn.Visible = false;
            }
             
        }
        protected void DataListUser_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLL.SNS.GroupUsers GroupUserBll = new BLL.SNS.GroupUsers();
                if (e.CommandArgument != null)
                {
                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int UserId = Globals.SafeInt(commandArgs[0], 0);
                    int GroupId = Globals.SafeInt(commandArgs[1], 0);
                    if (GroupUserBll.DeleteEx(GroupId,UserId))
                    {
                        LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "移除群组（id="+GroupId+"）用户（userid="+UserId+"）成功", this);
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
        


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            int GroupID = GroupId;
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteEx(GroupID,idlist))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "移除群组（id=" + GroupId + "）用户（userid=" + idlist + "）成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "移除群组（id=" + GroupId + "）用户（userid=" + idlist + "）失败", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelError);
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
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}
            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (GroupId > 0)
            {
                strWhere.Append("GroupID="+GroupId+"");
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append("and");
                }
                strWhere.AppendFormat("NickName like '%{0}%'", txtKeyword.Text.Trim());
            }
            this.AspNetPager1.RecordCount = bll.GetRecordCount(strWhere.ToString());
            DataListUser.DataSource = bll.GetListByPage(strWhere.ToString(), "", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            DataListUser.DataBind();
        }

        //protected string GetIsRecomend(object target)
        //{
        //    string str = string.Empty;
        //    if (!StringPlus.IsNullOrEmpty(target))
        //    {
        //        int Status = Common.Globals.SafeInt(target.ToString(), 0);
        //        switch (Status)
        //        {
        //            case 0:
        //                return "推荐频道首页";
        //            case 2:
        //                return "取消频道首页";
        //            default:
        //                return "推荐频道首页";
        //        }
        //    }
        //    return str;
        //}

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        //protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gridView.PageIndex = e.NewPageIndex;
        //    gridView.OnBind();
        //}

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
            //#warning 代码生成警告：请检查确认真实主键的名称和类型是否正确
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //bll.Delete(ID);
            //gridView.OnBind();
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < DataListUser.Items.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)DataListUser.Items[i].FindControl("ckUser");
                HiddenField hfPhotoId = (HiddenField)DataListUser.Items[i].FindControl("hfUserId");
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





    }
}
