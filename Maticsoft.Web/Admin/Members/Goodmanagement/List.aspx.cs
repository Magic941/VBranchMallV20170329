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
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace Maticsoft.Web.Admin.Members.Goodmanagement
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 182; } } //用户管理_是否显示会员信息管理页面

        protected int Act_BatActive = 183;
        protected int Act_BatUnActive = 184;
        private Maticsoft.BLL.Members.UsersExp userEXP = new BLL.Members.UsersExp();
        private Maticsoft.BLL.Members.Users user = new BLL.Members.Users();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }


        #region gridView

        /// <summary>
        /// 获取数据源
        /// </summary>
        public void BindData()
        {
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" NickName like '%" + txtKeyword.Text + "%' ");
            }

            if (txtPhone.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" Phone like '%" + txtPhone.Text + "%' ");
            }

            if (txtEmail.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" Email like '%" + txtEmail.Text + "%' ");
            }

            if (txtHaolinCard.Text.Trim() != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                Maticsoft.BLL.Shop_CardUserInfo cardUserInfo = new BLL.Shop_CardUserInfo();
                Model.Shop_CardUserInfo userCard = cardUserInfo.GetModelByCard(txtHaolinCard.Text.Trim());
                if (userCard != null)
                    strWhere.Append(" UserName like '%" + userCard.UserId + "%' ");
            }
            if (Common.Globals.SafeInt(DropUserAppType.SelectedValue, -1) > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  UserAppType=" + DropUserAppType.SelectedValue + " ");
            }
            if (Common.Globals.SafeInt(DropUserOldType.SelectedValue, -1) > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  UserOldType=" + DropUserOldType.SelectedValue + " ");
            }
            if (Common.Globals.SafeInt(DropProbation.SelectedValue, -1) > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  Probation=" + DropProbation.SelectedValue + " ");
            }
            if (Common.Globals.SafeInt(DropUserStatus.SelectedValue, -1) > -1)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  UserStatus=" + DropUserStatus.SelectedValue + " ");
            }


            ds = userEXP.Select_UsersEXP("UU", strWhere.ToString());

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

                HtmlInputText hidProbationstatus = e.Row.FindControl("hidProbationstatus") as HtmlInputText;

                HtmlInputRadioButton radlist2 = e.Row.FindControl("RadProbation2") as HtmlInputRadioButton;   //正常试用
                HtmlInputRadioButton radlist3 = e.Row.FindControl("RadProbation3") as HtmlInputRadioButton;   //开始试用
                HtmlInputRadioButton radlist4 = e.Row.FindControl("RadProbation4") as HtmlInputRadioButton;   //拒绝试用

                switch (hidProbationstatus.Value)
                {
                    case "0":
                        radlist2.Checked = true;
                        break;
                    case "1":
                        radlist3.Checked = true;
                        break;
                    case "2":
                        radlist4.Checked = true;
                        break;
                }
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
            //int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            //if (rankBll.Delete(ID))
            //{
            //    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "删除成功！");
            //}
            gridView.OnBind();
        }
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Status")
            {
                if (e.CommandArgument != null)
                {
                    int Id = 0;
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    Id = Common.Globals.SafeInt(Args[0], 0);
                    AccountsPrincipal user = new AccountsPrincipal(Id);
                    User currentUser = new Maticsoft.Accounts.Bus.User(user);
                    bool Status = Common.Globals.SafeBool(Args[1], false);
                    currentUser.Activity = Status ? false : true;
                    currentUser.Update();
                    gridView.OnBind();
                }
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

        /// <summary>
        /// 得到图片路径
        /// </summary>
        /// <returns></returns>
        protected string GetGravatar(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string savePath = Maticsoft.BLL.SysManage.ConfigSystem.GetValue("SNSGravatarPath");
                if (string.IsNullOrEmpty(savePath))
                {
                    savePath = "/Upload/User/Gravatar/";
                }
                int UserId = Common.Globals.SafeInt(target.ToString(), 0);
                return savePath + UserId + ".jpg";
            }
            return str;
        }
        /// <summary>
        /// 得到申请状态
        /// </summary>
        /// <returns></returns>
        protected string GetUserAppType(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    int UserID = Common.Globals.SafeInt(obj.ToString(), 0);

                    Maticsoft.BLL.Members.UsersExp userExpBll = new BLL.Members.UsersExp();

                    if (userExpBll.GetUserAppType(UserID) == 0)
                    {
                        return "<p style='color:#fb0d64'>会员申请</p>";
                    }
                    else if (userExpBll.GetUserAppType(UserID) == 1)
                    {
                        return "<p style='color:#074ef6'>好粉申请</p>";
                    }
                    else if (userExpBll.GetUserAppType(UserID) == 2)
                    {
                        return "<p style='color:#16ccf5'>个人微店申请</p>";
                    }
                    else if (userExpBll.GetUserAppType(UserID) == 3)
                    {
                        return "<p style='color:#09C22B'>分销店申请</p>";
                    }
                    else if (userExpBll.GetUserAppType(UserID) == 4)
                    {
                        return "<p style='color:#C25609'>服务店申请</p>";
                    }
                    else
                    {
                        return "<p style='color:#fb0d64'>会员申请</p>";
                    }
                }
                else
                {
                    return "<p style='color:#fb0d64'>会员申请</p>";
                }
            }
            else
            {
                return "<p style='color:#fb0d64'>会员申请</p>";
            }
        }

        /// <summary>
        ///获取审核的组合状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string GetUserStatus(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    int UserID = Common.Globals.SafeInt(obj.ToString(), 0);
                    Maticsoft.BLL.Members.UsersExp userExpBll = new BLL.Members.UsersExp();

                    if (userExpBll.GetUserStatus("UU", UserID) == 0)
                    {
                        return "<p style='color:#f00'>处理中</p>";
                    }
                    else if (userExpBll.GetUserStatus("UU", UserID) == 1)
                    {
                        return "<p style='color:#f6780e'>通过审核</p>";
                    }
                    else if (userExpBll.GetUserStatus("UU", UserID) == 2)
                    {
                        return "<p style='color:#dd0ef5'>未通过审核</p>";
                    }
                    else
                    {
                        return "<p style='color:#f00'>处理中</p>";
                    }
                }
                else
                {
                    return "<p style='color:#f00'>处理中</p>";
                }
            }
            else
            {
                return "<p style='color:#f00'>处理中</p>";
            }
        }

        /// <summary>
        /// 得到推荐人
        /// </summary>
        /// <returns></returns>
        protected string GetRecommendUserName(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    int UserID = Common.Globals.SafeInt(obj.ToString(), 0);
                    Maticsoft.Model.Members.Users userModel = user.RecommendUserName(UserID);
                    if (userModel != null)
                    {
                        return userModel.TrueName+" "+userModel.Phone;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        #endregion


    }
}