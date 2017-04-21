using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Drawing;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class HotKeyword : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 471; } } //Shop_热门关键词管理_列表页
        protected new int Act_AddData = 472;    //Shop_热门关键词管理_添加数据
        protected new int Act_UpdateData = 473;    //Shop_热门关键词管理_编辑数据
        protected new int Act_DelData = 474;    //Shop_热门关键词管理_删除数据


        private Maticsoft.BLL.Shop.Products.HotKeyword hotKeywordBll = new BLL.Shop.Products.HotKeyword();
        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryInfoBll = new BLL.Shop.Products.CategoryInfo();

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
                    btnAdd.Visible = false;
                }
                //一级分类
                Maticsoft.BLL.Shop.Products.CategoryInfo bllc = new BLL.Shop.Products.CategoryInfo();
                DataSet dsc = bllc.GetList(" Depth = 1");

                if (!DataSetTools.DataSetIsNull(dsc))
                {
                    this.dropCategories.DataSource = dsc;
                    this.dropCategories.DataTextField = "Name";
                    this.dropCategories.DataValueField = "CategoryId";
                    this.dropCategories.DataBind();
                    dropCategories.Items.Insert(0, new ListItem("", ""));
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
            gridView.OnBind();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            hotKeywordBll.DeleteList(idlist);
            MessageBox.ShowSuccessTip(this, "删除成功！");
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[2].Visible = false;
            }
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat("keywords like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = hotKeywordBll.GetListLeftjoinCategories(strWhere.ToString());
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

        #endregion

        protected void LinkDelete_Click(object sender, EventArgs e)
        {
            btnDelete_Click(e, e);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int cateId = Common.Globals.SafeInt(dropCategories.SelectedValue, -1);
            if (cateId == -1)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择商品分类");
                return;
            }
            Maticsoft.Model.Shop.Products.HotKeyword model = new Model.Shop.Products.HotKeyword();
            model.Keywords = tbKeyWord.Text.Trim();
            model.CategoryId = cateId;

            if (hotKeywordBll.Add(model) > 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "添加成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "保存失败！");
            }
            gridView.OnBind();
        }
    }
}