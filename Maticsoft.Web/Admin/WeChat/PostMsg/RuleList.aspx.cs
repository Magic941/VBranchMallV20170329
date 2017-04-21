using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.WeChat.BLL.Core;

namespace Maticsoft.Web.Admin.WeChat.PostMsg
{
    public partial class RuleList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 638; } } //移动营销_智能客服设置管理_列表页
        protected new int Act_AddData = 639;    //移动营销_智能客服设置管理_添加数据
        protected new int Act_UpdateData = 640;    //移动营销_智能客服设置管理_编辑数据
        protected new int Act_DelData = 641;    //移动营销_智能客服设置管理_删除数据
        private   Maticsoft.WeChat.BLL.Core.KeyRule ruleBll=new KeyRule();
        private  Maticsoft.WeChat.BLL.Core.KeyValue valueBll=new KeyValue();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    btnDelete.Visible = false;
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

        //删除规则
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (ruleBll.DeleteListEx(idlist))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }
        #region gridView


        public void BindData()
        {

            StringBuilder strWhere = new StringBuilder();
            //string state = this.DropState.SelectedValue;
            //if (!string.IsNullOrWhiteSpace(this.DropState.SelectedValue) )  
            //{
            //    strWhere.AppendFormat(" state={0}", state);
            //}
            gridView.DataSetSource =ruleBll.GetAllList();
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
            if (ruleBll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
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


        #region 获取关键字集合

        protected string GetValues(object target)
        {
            string values = "";
              if (!StringPlus.IsNullOrEmpty(target))
              {
                 int ruleId = Common.Globals.SafeInt(target.ToString(), 0);
               List<Maticsoft.WeChat.Model.Core.KeyValue> ruleList=  valueBll.GetModelList(" ruleId=" + ruleId);
                  if (ruleList != null && ruleList.Count > 0)
                  {
                      values = String.Join(",", ruleList.Select(c => c.Value));
                  }
              }
            return values;
        }

        #endregion 

    }
}