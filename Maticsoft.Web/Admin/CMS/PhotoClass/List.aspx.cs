using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.PhotoClass
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 249; } } //CMS_图片分类管理_列表页
        protected new int Act_AddData = 252;    //CMS_图片分类管理_添加数据
        protected new int Act_UpdateData = 253;    //CMS_图片分类管理_编辑数据
        protected new int Act_DelData = 254;    //CMS_图片分类管理_删除数据
 
        readonly BLL.CMS.PhotoClass bll = new BLL.CMS.PhotoClass();

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

            }
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }
        
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if(idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }
        
        #region gridView
                        
        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[4].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[3].Visible = false;
            }
            //    gridView.Columns[6].Visible = true;
            
            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {      
                strWhere.AppendFormat("ClassName like '%{0}%'", txtKeyword.Text.Trim());
            }            
            ds = bll.GetList(strWhere.ToString());
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
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
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

        public string GetParentName(object obj)
        {
            string str = string.Empty;
            BLL.CMS.PhotoClass classBll = new BLL.CMS.PhotoClass();
            if (obj != null && obj.ToString() != "0" && PageValidate.IsNumber(obj.ToString()))
            {
                str = obj.ToString();
                Model.CMS.PhotoClass classModel = classBll.GetModel(int.Parse(str));
                if (classModel != null)
                {
                    str = classModel.ClassName;
                }
            }
            return str;
        }
    }
}
