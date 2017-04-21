using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Forms
{
    public partial class Index : PageBaseAdmin
    {
        Maticsoft.BLL.Poll.Forms bll = new BLL.Poll.Forms();

        protected override int Act_PageLoad { get { return 350; } } //客服管理_问卷管理_列表页
        protected new int Act_AddData = 351;    //客服管理_问卷管理_添加数据
        protected new int Act_UpdateData = 352;    //客服管理_问卷管理_编辑数据
        protected new int Act_DelData = 353;    //客服管理_问卷管理_删除数据
        
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
                    btnAdd.Visible = false;
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
                int formid= BLL.SysManage.ConfigSystem.GetIntValueByCache("System_Poll_FormID");
                if (formid > 0)
                {
                    this.txtFormID.Text = formid.ToString();
                }
               
            }
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
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            #region
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[5].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[7].Visible = false;
            }
 
            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtName.Text))
            {
                MessageBox.ShowFailTip(this, Resources.Poll.ErrorFormsNameNull);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtDescription.Text))
            {
                MessageBox.ShowFailTip(this, Resources.Poll.ErrorFormsExplainNull);
                return;
            }

            string Name = this.txtName.Text;
            string Description = this.txtDescription.Text;

            Maticsoft.Model.Poll.Forms model = new Maticsoft.Model.Poll.Forms();
            model.Name = Name;
            model.Description = Description;

            model.IsActive = chkIsActive.Checked;

            Maticsoft.BLL.Poll.Forms bll = new Maticsoft.BLL.Poll.Forms();
            int id = bll.Add(model);
            if (id > 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipAddSuccess);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }
            gridView.OnBind();
        }
        #region 要启用的问卷 
        protected void btnSaveFormID_Click(object sender, EventArgs e)
        {
            int formId = Common.Globals.SafeInt(this.txtFormID.Text, -1);
            if (formId<= 0)
            {
                MessageBox.ShowFailTip(this, Resources.Poll.lblWriteFormsID);
                return;
            }

            if (BLL.SysManage.ConfigSystem.Modify("System_Poll_FormID", formId.ToString(), "要启用的问卷的ID",
                                                  ApplicationKeyType.CMS))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                BLL.SysManage.ConfigSystem.ClearCacheByKey("System_Poll_FormID");//清除缓存
                return;
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
                return;
            }
        }
        #endregion

        public string IsActive(object obj)
        {
            if (obj != null)
            {
                if (!string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    if (Convert.ToBoolean(obj))
                    {
                        return "是";
                    }
                    else
                    {
                        return "否";
                    }
                }
                else
                {
                    return "否";
                }
            }
            else
            {
                return "否";
            }
        }
    }
}
