/**
* List.cs
*
* 功 能： 网站菜单列表
* 类 名： List.cs
*
* Ver    变更日期                             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年5月23日 16:38:16  孙鹏       初版
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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Settings.MainMenus
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 386; } } //设置_导航菜单管理_列表页
        protected new int Act_AddData = 389;    //设置_导航菜单管理_添加数据
        protected new int Act_UpdateData = 390;    //设置_导航菜单管理_编辑数据
        protected new int Act_DelData = 391;    //设置_导航菜单管理_删除数据
        //int Act_ShowInvalid = -1; //查看失效数据行为
     
        private Maticsoft.BLL.Settings.MainMenus bll = new Maticsoft.BLL.Settings.MainMenus();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                { 
                    btnDelete.Visible = false;
                    btnDeletetwo.Visible = false;
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    butAdd.Visible = false;
                    btnAddtwo.Visible = false;               
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

                #region 记录上次条件和页索引
                if (!string.IsNullOrWhiteSpace(Request.Params["all"]))
                {
                    Session["strWhereMainMenuslist"] = "";
                    Session["MainMenuslistPageIndex"] = 0;
                }

                if (Session["MainMenuslistPageIndex"] != null && Session["MainMenuslistPageIndex"].ToString() != "")
                {
                    gridView.PageIndex = (int)Session["MainMenuslistPageIndex"];
                }
                #endregion
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            #region 记录搜索条件
            Session["strWhereMainMenuslist"] = "";
            gridView.PageIndex = 0;
            StringBuilder strWhere = new StringBuilder();
            int type = Maticsoft.Common.Globals.SafeInt(this.ddlType.AreaInt, -1);
            if (type != -1)
            {
                strWhere.AppendFormat("NavArea ={0}", type);
            }

            if (txtKeyword.Text.Trim() != "")
            {
                if (!String.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append("And  ");
                }
                strWhere.AppendFormat("  MenuName like '%{0}%'", txtKeyword.Text.Trim());
            }
            Session["strWhereMainMenuslist"] = strWhere.ToString();
            #endregion

            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
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

            #endregion gridView

            #region 使用记忆搜索条件
            string strWhere = "";
            Session["MainMenuslistPageIndex"] = 0;
            if (Session["strWhereMainMenuslist"] != null && Session["strWhereMainMenuslist"].ToString() != "")
            {
                strWhere = Session["strWhereMainMenuslist"].ToString();
            }
            #endregion

            DataSet ds = new DataSet();
            ds = bll.GetList(-1, strWhere, " Sequence");
            gridView.DataSetSource = ds;
            Session["MainMenuslistPageIndex"] = gridView.PageIndex;
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
            e.Row.Attributes.Add("style", "background:#FFF");//CDC9C
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
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("spanmodify");
                HiddenField HiddenField1 = (HiddenField)e.Row.FindControl("HiddenField1");
                HiddenField1.Value = DataBinder.Eval(e.Row.DataItem, "MenuID").ToString();
                CheckBox ChkBxItem = (CheckBox)e.Row.FindControl(gridView.CheckBoxID);
                object obj1 = DataBinder.Eval(e.Row.DataItem, "MenuType");
                if (obj1 != null && obj1.ToString() == "0")
                {
                    linkbtnDel.Visible = false;
                    ChkBxItem.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    linkbtnDel.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    updatebtn.Visible = false;
                }

            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            HiddenField HiddenField1 = (HiddenField)gridView.Rows[e.RowIndex].FindControl("HiddenField1");
            int ID = int.Parse(HiddenField1.Value);
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

        protected string GetAreaName( object target)
        {
            int type = Common.Globals.SafeInt(target.ToString(), -1);
            string str = "未知区域";
            switch (type)
            {
                case 0:
                    str = "CMS";
                    break;
                case 1:
                    str = "SNS";
                    break;
                case 2:
                    str = "Shop";
                    break;
               default:
                    str = "未知区域";
                    break;
            }
            return str;
        }

        protected string GetURLType(object target)
        {
            int type = Common.Globals.SafeInt(target.ToString(), -1);
            string str = "未知类型";
            switch (type)
            {
                case 0:
                    str = "自定义";
                    break;
                case 1:
                    str = "CMS文章栏目";
                    break;
                case 2:
                    str = "社区商品分类";
                    break;
                case 3:
                    str = "社区图片分类";
                    break;
                case 4:
                    str = "商城商品分类";
                    break;
                default:
                    str = "自定义";
                    break;
            }
            return str;
        }
    }
}