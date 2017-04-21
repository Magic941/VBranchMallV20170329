using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.CMS.ContentClass
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        Maticsoft.BLL.CMS.ContentClass bll = new Maticsoft.BLL.CMS.ContentClass();
        public string tag = "_self";

        protected override int Act_PageLoad { get { return 220; } } //CMS_栏目管理_列表页
        protected new int Act_AddData = 224;    //CMS_栏目管理_添加数据224
        protected new int Act_UpdateData = 225;    //CMS_栏目管理_编辑数据225
        protected new int Act_DelData = 226;    //CMS_栏目管理_删除数据226
 

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
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

                BindData();

                string strT = Request.Params["t"];
                if (!string.IsNullOrWhiteSpace(strT))
                {
                    if (strT.ToLower() == "list")
                    {
                        tag = "_parent";
                    }
                }
            }
        }

        public int ClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["ClassID"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            BindData();
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "State=" + 0;
            if (bll.UpdateList(idlist, strWhere))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
            BindData();
        }

        /// <summary>
        /// 批量草稿
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateState_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "State=" + 1;
            if (bll.UpdateList(idlist, strWhere))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
            BindData();
        }

        /// <summary>
        /// 批量拒绝
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnInverseApprove_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            string strWhere = "State=" + 2;
            if (bll.UpdateList(idlist, strWhere))
            {

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
            }
            BindData();
        }

        #region gridView

        public void BindData()
        {
            #region
            
          
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[15].Visible = false;
             
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[16].Visible = false;
            } 
          

            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            string state = ddlState.SelectedValue;

            if (!string.IsNullOrWhiteSpace(ddlState.SelectedValue))
            {
                strWhere.AppendFormat(" State={0} ", ddlState.SelectedValue);
            }

            if (!string.IsNullOrWhiteSpace(ddlType.SelectedValue))
            {
                string keywords = InjectionFilter.QuoteFilter(txtKeyword.Text.Trim());
                if (!string.IsNullOrWhiteSpace(keywords))
                {
                    switch (ddlType.SelectedValue)
                    {
                        case "1":
                            if (strWhere.ToString().Trim().Length > 0)
                            {
                                strWhere.AppendFormat(" and ClassName like '%{0}%' ", keywords);
                            }
                            else
                            {
                                strWhere.AppendFormat(" ClassName like '%{0}%' ", keywords);
                            }
                            break;
                        case "2":
                            if (strWhere.ToString().Trim().Length > 0)
                            {
                                strWhere.AppendFormat(" and Description like '%{0}%' ", keywords);
                            }
                            else
                            {
                                strWhere.AppendFormat(" Description like '%{0}%' ", keywords);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            if (ClassID > 0)
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and ");
                }
                strWhere.AppendFormat(" CMSCC.ClassID={0} ", ClassID);
            }

            ds = bll.GetListByView(0,strWhere.ToString(),"Sequence ASC");
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
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
                //if (e.Row.RowIndex % 2 == 0)
                //{
                //    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                //}
                //else
                //{
                //    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                //}

                int num = (int)DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "ClassName").ToString();
                e.Row.Cells[1].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl control = e.Row.FindControl("spShowImage") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    control.Visible = false;
                }
                Label label = e.Row.FindControl("lblContentClassName") as Label;
                if (null != label)
                {
                    label.Text = str;
                }
            }
        }


        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
            int ContentClassID = (int)this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                bll.SwapCategorySequence(ContentClassID, Maticsoft.Common.Video.SwapSequenceIndex.Down);
            }
            if (e.CommandName == "Rise")
            {
                bll.SwapCategorySequence(ContentClassID, Maticsoft.Common.Video.SwapSequenceIndex.Up);
            }
            BindData();
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.DeleteCategory(ID);
            BindData();
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl("ckSelect");
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

        #region 获取栏目审核状态
        /// <summary>
        /// 获取栏目审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetState(object target)
        {
            //0:审核通过、1:作为草稿、2:等待审核。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = Resources.Site.Approved;
                        break;
                    case "1":
                        str = Resources.Site.Draft;
                        break;
                    case "2":
                        str = Resources.Site.PendingReview;
                        break;
                    default:
                        break;
                }
            }
            return str;
        } 
        #endregion

        #region 获取栏目内容模式
        /// <summary>
        /// 获取栏目内容模式
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetContentModel(object target)
        {
            //2:单文章、3:文章列表。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "2":
                        str = Resources.CMS.CCSingleArticle;
                        break;
                    case "3":
                        str = Resources.CMS.CCArticleList;
                        break;
                    default:
                        break;
                }
            }
            return str;
        } 
        #endregion
    }
}
