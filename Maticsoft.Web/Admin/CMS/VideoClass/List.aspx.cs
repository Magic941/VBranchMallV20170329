/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
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
using Maticsoft.Common;
using Maticsoft.Common.Video;

namespace Maticsoft.Web.CMS.VideoClass
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 262; } }//CMS_视频分类管理_列表页
        protected new int Act_AddData = 266;    //CMS_视频分类管理_添加数据 
        protected new int Act_UpdateData = 267;    //CMS_视频分类管理_编辑用户
        protected new int Act_DelData = 268;    //CMS_视频分类管理_删除用户
 
        Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }


                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                LoadVideoClassData();
                BindData();
            }
        }

        protected void LoadVideoClassData()
        {

            DataSet ds = bll.GetListEx(" ParentID=0 ","");

            //if (!DataSetTools.DataSetIsNull(ds))
            //{
            //    this.dropVideoClassID.DataSource = ds;
            //    this.dropVideoClassID.DataTextField = "VideoClassName";
            //    this.dropVideoClassID.DataValueField = "VideoClassID";
            //    this.dropVideoClassID.DataBind();
            //}
            //this.dropVideoClassID.Items.Insert(0, new ListItem(Resources.Site.PleaseSelect, "0"));
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        #region gridView

        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[3].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[4].Visible = false;  
            }
           
            #endregion
            StringBuilder strWhere = new StringBuilder();
            string id = this.VideoClassDropList1.SelectedValue;
            if (id != "" && id != "0" && PageValidate.IsNumber(id))
            {
                string path = "";
                Maticsoft.Model.CMS.VideoClass model = bll.GetModel(int.Parse(id));
                if (model != null)
                {
                    path = model.Path;
                }
                strWhere.Append(" VideoClassID=" + this.VideoClassDropList1.SelectedValue + " OR Path LIKE '" + path + "|%'");
            }
            DataSet ds = bll.GetListEx(strWhere.ToString(), " Sequence ASC ");
            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int num = (int)DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "VideoClassName").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl control = e.Row.FindControl("spShowImage") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    control.Visible = false;
                }
                Label label = e.Row.FindControl("lblVideoClassName") as Label;
                label.Text = str;
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.DeleteEx(ID);
            LoadVideoClassData();
            BindData();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
            int videoClassID = (int)this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                bll.SwapCategorySequence(videoClassID, SwapSequenceIndex.Down);
            }
            if (e.CommandName == "Rise")
            {
                bll.SwapCategorySequence(videoClassID, SwapSequenceIndex.Up);
            }
            BindData();
        }

        #endregion

        #region 根据ParentID得到一个对象实体
        /// <summary>
        /// 根据ParentID得到一个对象实体
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoClassNameByParentID(object target)
        {
            Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
            string videoClassName = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string str = target.ToString();
                if (PageValidate.IsNumber(str))
                {
                    Maticsoft.Model.CMS.VideoClass model = bll.GetModelByParentID(int.Parse(str));
                    if (null != model)
                    {
                        videoClassName = model.VideoClassName;
                    }
                }
            }
            return videoClassName;
        }
        #endregion


    }
}
