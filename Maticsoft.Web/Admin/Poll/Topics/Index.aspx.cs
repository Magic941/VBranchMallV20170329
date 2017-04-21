using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.Topics
{
    public partial class Index : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 357; } } //客服管理_题目管理_列表页
        protected new int Act_AddData = 358;    //客服管理_题目管理_添加数据
        protected new int Act_DelData = 359;    //客服管理_题目管理_删除数据
        Maticsoft.BLL.Poll.Topics bll = new BLL.Poll.Topics();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    btnAdd.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                string fid = Request.Params["fid"];
                if (!string.IsNullOrWhiteSpace(fid) && PageValidate.IsNumber(fid))
                {
                    Maticsoft.BLL.Poll.Forms bll = new Maticsoft.BLL.Poll.Forms();
                    Maticsoft.Model.Poll.Forms model = bll.GetModel(Convert.ToInt32(fid));
                    if (null != model)
                    {
                        lblFormID.Text = model.FormID.ToString();
                        Session["strWhereTopics"] = " FormID= " + model.FormID;
                        lblFormName.Text = model.Name;
                    }
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

        #region BindData
        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[4].Visible = false;
            }
            #endregion

            string strWhere = "";
            if (Session["strWhereTopics"] != null && Session["strWhereTopics"].ToString() != "")
            {
                strWhere += Session["strWhereTopics"].ToString();
            }
            DataSet ds = new DataSet();

            ds = bll.GetList(strWhere.ToString());
            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSetSource = ds;
        }
        #endregion

        #region btn_Search
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = this.txtKey.Text.Trim();
            string strsql = "";

            if (radbtnTrueName.Checked)
            {
                if (keyword != "")
                {
                    strsql += " and (Title like'%" + keyword + "%')";
                }
            }
            else
            {
                if (keyword != "")
                {
                    strsql += " and (ID =" + keyword + ")";
                }
            }
            if (strsql != "")
            {
                Session["strWhereTopics"] = " FormID= " + lblFormID.Text + strsql;
            }
            else
            {
                Session["strWhereTopics"] = " FormID= " + lblFormID.Text;
            }
            //BindData();
            gridView.OnBind();
        }
        #endregion

        #region gridView事件

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
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string ID = gridView.DataKeys[e.RowIndex].Value.ToString();
            Maticsoft.BLL.Poll.Topics bll = new Maticsoft.BLL.Poll.Topics();
            bll.Delete(Convert.ToInt32(ID));
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            gridView.OnBind();
        }

        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string Title = this.txtTitle.Text;
            int Type = int.Parse(radbtnType.SelectedValue);
            int FormID = int.Parse(this.lblFormID.Text);

            Maticsoft.Model.Poll.Topics model = new Maticsoft.Model.Poll.Topics();
            model.Title = Title;
            model.Type = Type;
            model.FormID = FormID;

            Maticsoft.BLL.Poll.Topics bll = new Maticsoft.BLL.Poll.Topics();
            if (bll.Exists(FormID, Title))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Poll.ErrorTopicExists);
                return;
            }
            if (bll.Add(model) > 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }

            Session["strWhereTopics"] = " FormID= " + lblFormID.Text;
            gridView.OnBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("../forms/Index.aspx");
        }

        protected void gridView_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.DeleteList(idlist);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
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
    }
}
