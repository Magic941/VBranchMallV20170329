using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Ms.AgentInfo
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 532; } } //Shop_代理商管理_列表页
        protected new int Act_AddData = 533;    //Shop_代理商管理_添加数据
        protected new int Act_UpdateData = 534;    //Shop_代理商管理_编辑数据
        protected new int Act_DelData = 535;    //Shop_代理商管理_删除数据
        Maticsoft.BLL.Ms.Agent.Agents bll = new BLL.Ms.Agent.Agents();
        Maticsoft.BLL.Members.Users bllUsers = new Maticsoft.BLL.Members.Users();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
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
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            bll.DeleteList(idlist);

            //删除代理商同时删除和代理商相关的用户
            bllUsers.DeleteListByDepartmentID(idlist);

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
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat("Name like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = bll.GetList(strWhere.ToString());
            gridView.DataSource = ds;
            gridView.DataBind();
        }

    

        protected void GridViewEx1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindData();
        }
        protected void GridViewEx1_OnRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //e.Row.Cells[0].Text = "<input id='Checkbox2' type='checkbox' onclick='CheckAll()'/><label></label>";
            }
        }
        protected void GridViewEx1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
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

        protected void GridViewEx1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);

            BLL.Members.Users users = new BLL.Members.Users();
            Maticsoft.Model.Members.Users model = users.GetUserIdByDepartmentID(ID.ToString());
             if (null != model)
             {
                 //删除代理商同时删除和代理商相关的用户（删除用户基本表信息）
                 bllUsers.DeleteByDepartmentID(ID);
                 //删除扩展表用户信息
                 BLL.Members.UsersExp usersEx = new BLL.Members.UsersExp();
                 usersEx.DeleteUsersExp(model.UserID);
             }
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
                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
                    if (gridView.DataKeys[i].Value != null)
                    {
                        idlist += gridView.DataKeys[i].Value + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #region 批量审核
        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApproveList_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "Status=" + 1;
            bll.UpdateList(idlist, strWhere);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            gridView.OnBind();
        }
        #endregion

        #region 批量拒绝
        /// <summary>
        /// 批量拒绝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "Status=" + 0;
            bll.UpdateList(idlist, strWhere);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            gridView.OnBind();
        }
        #endregion

        #region 批量冻结
        /// <summary>
        /// 批量冻结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateState_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "Status=" + 2;
            bll.UpdateList(idlist, strWhere);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            gridView.OnBind();
        }
        #endregion

        #region 获取代理商性质
        /// <summary>
        /// 获取代理商性质
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetCompanyType(object target)
        {
            //0:个体工商; 1:私营独资代理商; 2:国营代理商。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "个体工商";
                        break;
                    case "2":
                        str = "私营独资代理商";
                        break;
                    case "3":
                        str = "国营代理商";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取代理商分类名称
        /// <summary>
        /// 获取代理商分类名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetEnteClassName(object target)
        {
            //合资、独资、国有、私营、全民所有制、集体所有制、股份制、有限责任制
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "合资";
                        break;
                    case "2":
                        str = "独资";
                        break;
                    case "3":
                        str = "国有";
                        break;
                    case "4":
                        str = "私营";
                        break;
                    case "5":
                        str = "全民所有制";
                        break;
                    case "6":
                        str = "集体所有制";
                        break;
                    case "7":
                        str = "股份制";
                        break;
                    case "8":
                        str = "有限责任制";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取代理商审核状态
        /// <summary>
        /// 获取代理商审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetStatus(object target)
        {
            //0:未审核; 1:正常;2:冻结;3:删除
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "未审核";
                        break;
                    case "1":
                        str = "正常";
                        break;
                    case "2":
                        str = "冻结";
                        break;
                    case "3":
                        str = "删除";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        public string DateTimeFormat(object target, string format, bool isFormat)
        {
            return TimeParser.DateTimeFormat(target, format, isFormat);
        }
        #endregion
    }
}