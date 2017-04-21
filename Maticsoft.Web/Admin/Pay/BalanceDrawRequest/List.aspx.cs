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
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using Maticsoft.Common;
using System.Drawing;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Pay.BalanceDrawRequest
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 682; } } //Shop_提现管理_列表页
        protected new int Act_UpdateData = 683;    //Shop_提现管理_编辑数据
        protected new int Act_DelData = 684;    //Shop_提现管理_删除数据

        Maticsoft.BLL.Pay.BalanceDrawRequest bll = new Maticsoft.BLL.Pay.BalanceDrawRequest();
        #region 加载
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                    liDel.Visible = false;
                }
                btnDelete.Attributes.Add("onclick", "return confirm(\"" + Resources.Site.TooltipDelConfirm + "\")");
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)))
                {
                    btnDelete.Visible = false;
                }

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

                gridView.OnBind();
            }
        }
        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Status")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int JournalNumber = Globals.SafeInt(Args[0], -1);
                    int Status = Globals.SafeInt(Args[1], -1);
                    if (JournalNumber <= 0 || Status <= 0)
                    {
                        return;
                    }
                    Model.Pay.BalanceDrawRequest model = bll.GetModelByCache(JournalNumber);
                    if (bll.UpdateStatus(model, Status))
                    {
                        DataCache.DeleteCache("BalanceDrawRequestModel-" + JournalNumber);
                        MessageBox.ShowSuccessTip(this, "修改成功");
                        gridView.OnBind();
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this, "修改失败");
                    }
                }
            }
        }

        #region gridView

        public void BindData()
        {

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (dropStatusSearch.SelectedValue != "0")
            {
                strWhere.AppendFormat(" RequestStatus= '{0}'", dropStatusSearch.SelectedValue);
            }
            if (txtKeyword.Text.Trim() != "")
            {
                if (strWhere.Length > 1) strWhere.Append(" and ");
                strWhere.AppendFormat(" UserName= '{0}'", txtKeyword.Text.Trim());
            }

            ds = bll.GetListUser(strWhere.ToString(), " JournalNumber desc");
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
                LinkButton linkbtnFail = (LinkButton)e.Row.FindControl("lbtnStatusFail");
                LinkButton linkbtnSuccess = (LinkButton)e.Row.FindControl("lbtnStatusSuccess");
                object obj1 = DataBinder.Eval(e.Row.DataItem, "RequestStatus");
                if ((obj1 != null) && ((obj1.ToString() != "")))
                {
                    if (obj1.ToString() == "1")
                    {
                        linkbtnFail.Visible = true;
                        linkbtnSuccess.Visible = true;
                    }
                }

                #region
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    LinkButton linkbtnedit = (LinkButton)e.Row.FindControl("btnEdit");
                    linkbtnedit.Visible = false;
                }
                #endregion
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
                idlist = idlist.TrimEnd(',');
            }
            return idlist;
        }

        #endregion

        #region 获取提现的审核状态
        /// <summary>
        /// 获取提现的审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetDrawState(object target)
        {
            //1:未审核、2:审核失败 3：审核通过。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "未审核";
                        break;
                    case "2":
                        str = "审核失败";
                        break;
                    case "3":
                        str = "审核通过";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        protected string GetCardType(int typeID)
        {
            switch (typeID)
            {
                case 1:
                    return "银行卡号";
                case 2:
                    return "支付宝帐号";
                default:
                    return "";
            }
        }
        #region 批量操作
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                gridView.OnBind();
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }

        }

        #endregion


    }
}
