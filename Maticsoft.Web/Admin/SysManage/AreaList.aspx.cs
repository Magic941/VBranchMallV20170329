using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class AreaList : PageBaseAdmin
    {

        Maticsoft.BLL.SysManage.ConfigArea areaBll = new Maticsoft.BLL.SysManage.ConfigArea();
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
            Maticsoft.Model.SysManage.ConfigArea areaModel = new Maticsoft.Model.SysManage.ConfigArea();
            if (String.IsNullOrWhiteSpace(tName.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入区域名称！");
                return;
            }
            if (areaBll.Exists(tName.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "该区域名称已存在！");
                return;
            }
            areaModel.AreaName = this.tName.Text;
            areaModel.Status = chkStatus.Checked ? 1 : 0;
            if (areaBll.Add(areaModel))
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
            if (areaBll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, "操作成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();

        }

        protected void ddlAction_Changed(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            switch (ddlAction.SelectedValue)
            {
                case "1":
                    if (areaBll.UpdateList(idlist, 1))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败");
                    }
                    break;
                case "2":
                    if (areaBll.UpdateList(idlist, 0))
                    {
                        MessageBox.ShowSuccessTip(this, "操作成功！");
                    }
                    else
                    {
                        Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败");
                    }
                    break;
                default:
                    break;
            }

            gridView.OnBind();

        }
        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Status")
            {
                if (e.CommandArgument != null)
                {
                    Maticsoft.Model.SysManage.ConfigArea areaModel = new Model.SysManage.ConfigArea();
                    string[] Args = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string area = Args[0];
                    int status = Common.Globals.SafeInt(Args[1], 0);
                    status = status == 1 ? 0 : 1;
                    areaModel.AreaName = area;
                    areaModel.Status = status;
                    areaBll.Update(areaModel);
                    gridView.OnBind();
                }
            }
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
            gridView.DataSetSource = areaBll.GetAllList();
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
                        idlist += "'"+gridView.DataKeys[i].Value.ToString() + "',";
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

        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "不启用";
                        break;
                    case 1:
                        str = "启用";
                        break;

                    default:
                        break;
                }
            }
            return str;
        }
    }
}