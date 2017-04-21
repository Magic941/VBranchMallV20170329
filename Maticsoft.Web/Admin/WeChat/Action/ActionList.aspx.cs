using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Action
{
    //功能行为
    
    public partial class ActionList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 623; } } //移动营销_指令功能管理_列表页
        protected new int Act_AddData = 624;    //移动营销_指令功能管理_添加数据
        protected new int Act_UpdateData = 625;    //移动营销_指令功能管理_编辑数据
        protected new int Act_DelData = 626;    //移动营销_指令功能管理_删除数据

        Maticsoft.WeChat.BLL.Core.Action actionBll=new Maticsoft.WeChat.BLL.Core.Action();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    btnSave.Visible = false;
                }
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
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
        //添加系统指令
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.Model.Core.Action actionModel = new Maticsoft.WeChat.Model.Core.Action();
            actionModel.Name = this.tName.Text;
            actionModel.Remark = this.tDesc.Text;
            if (actionBll.Add(actionModel) > 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "添加成功！");
                gridView.OnBind();
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "添加失败！");
            }

        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (actionBll.DeleteListEx(idlist))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
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
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[3].Visible = false;
            }

            StringBuilder strWhere = new StringBuilder();
            //string state = this.DropState.SelectedValue;
            //if (!string.IsNullOrWhiteSpace(this.DropState.SelectedValue) )  
            //{
            //    strWhere.AppendFormat(" state={0}", state);
            //}
            gridView.DataSetSource = actionBll.GetAllList();
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
            if (actionBll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
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

                    //#warning 代码生成警告：请检查确认Cells的列索引是否正确
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




    }
}