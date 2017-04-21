using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Posts
{
    public partial class UserBlog : PageBaseAdmin
    {
        private Maticsoft.BLL.SNS.UserBlog bll = new Maticsoft.BLL.SNS.UserBlog();
        protected new int Act_DelData = 25;

        protected override int Act_PageLoad { get { return 91; } } //社区管理_是否显示全部动态页面

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
             
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
       

            #endregion gridView
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(txtPoster.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat("UserName like '%{0}%' ", txtPoster.Text.Trim());

            }
            if (!string.IsNullOrEmpty(txtBeginTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append("  CreatedDate>='" + txtBeginTime.Text.Trim()+"'");
            }
            if (!string.IsNullOrEmpty(txtEndTime.Text.Trim()))
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" CreatedDate<='" + txtEndTime.Text.Trim() + "'");
            }
         
            if (txtKeyword.Text.Length > 0)
            {
                if (strWhere.Length > 0)
                {
                    strWhere.Append(" and ");
                }
                strWhere.Append(" Title like '%" + txtKeyword.Text + "%' ");
            }
            DataSet ds = bll.GetList(-1, strWhere.ToString(), " CreatedDate desc");

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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("linkbtnDel");
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandArgument.Equals("Delete"))
            {
                if (e.CommandName == null || e.CommandName.ToString() == "")
                {
                    return;
                }
                int blogId = Globals.SafeInt(e.CommandName, -1);
                if (bll.DeleteEx(blogId))
                {
                    MessageBox.ShowSuccessTip(this, "删除成功！");
                    gridView.OnBind();
                }
                else
                {
                    MessageBox.ShowFailTip(this, "删除失败，请稍候再试！");
                }

            }
            if (e.CommandName == "RecommendIndex")
            {
                if (e.CommandArgument != null)
                {
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    int blogId = Common.Globals.SafeInt(Args[0], 0);
                    int Rec = Common.Globals.SafeInt(Args[1], 0)==0?1:0;
                    if (bll.UpdateRec(blogId, Rec))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                        gridView.OnBind();
                    }
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

        private string GetSelIDTypelist(string type)
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Values.Count > 1)
                    {
                        if (gridView.DataKeys[i].Values[1].ToString() == type && type == "0")
                        {
                            idlist += gridView.DataKeys[i].Values[0] + ",";
                        }
                        else if (gridView.DataKeys[i].Values[1].ToString() == type)
                        {
                            idlist += gridView.DataKeys[i].Values[2] + ",";
                        }
                    }
                }
            }
            if (BxsChkd)
            {
                if (idlist.Length > 0)
                {
                    idlist = idlist.Substring(0, idlist.LastIndexOf(","));
                }
            }
            return idlist;
        }

        #endregion

        #region 公共方法，列信息解析
   

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
                        return "未通过";
                }
            }
            return str;
        }

        #endregion

        #region 按钮操作
        protected void btnIndexRec_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateRecList(idlist, 1))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
            gridView.OnBind();
        }
        

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheck_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.UpdateStatusList(idlist,1))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                MessageBox.ShowFailTip(this, "操作失败！");
            }
            gridView.OnBind();
        }


        #endregion


    }
}