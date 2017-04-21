using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.WeChat.Model.Core;

namespace Maticsoft.Web.Admin.WeChat.Group
{
    public partial class GroupList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 634; } } //移动营销_用户小组管理_列表页
        protected new int Act_AddData = 635;    //移动营销_用户小组管理_添加数据
        protected new int Act_UpdateData = 636;    //移动营销_用户小组管理_编辑数据
        Maticsoft.WeChat.BLL.Core.Group groupBll = new Maticsoft.WeChat.BLL.Core.Group();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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
        //添加分组
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            if (String.IsNullOrWhiteSpace(openId))
            {
                MessageBox.ShowFailTip(this, "您还未填写微信原始ID，请在公众号配置中填写！", "/admin/WeChat/Setting/Config.aspx");
            }
            if (String.IsNullOrWhiteSpace(this.tName.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请填写分组名称！");
                return;
            }
            Maticsoft.WeChat.Model.Core.Group groupModel = new Maticsoft.WeChat.Model.Core.Group();
            groupModel.GroupName = this.tName.Text;

            groupModel.Remark = this.tDesc.Text;
            groupModel.OpenId = openId;
            if (groupBll.Add(groupModel) > 0)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this,"添加成功！");
                   gridView.OnBind();
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "添加失败！");
            }
           
        }
        

        #region gridView


        public void BindData()
        {

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[2].Visible = false;
            }
            StringBuilder strWhere = new StringBuilder();
            //string state = this.DropState.SelectedValue;
            //if (!string.IsNullOrWhiteSpace(this.DropState.SelectedValue) )  
            //{
            //    strWhere.AppendFormat(" state={0}", state);
            //}
            gridView.DataSetSource =  groupBll.GetAllList();
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
            if (groupBll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
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


        protected void btnGetGroup_Click(object sender, EventArgs e)
        {
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            if (String.IsNullOrWhiteSpace(openId))
            {
                MessageBox.ShowFailTipScript(this, "您还未填写微信原始ID，请在公众号配置中填写！", "window.parent.location.href='/admin/WeChat/Setting/Config.aspx';");
            }
            //先授权 
            string AppId =Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
            if (String.IsNullOrWhiteSpace(token))
            {
                MessageBox.ShowFailTip(this, "获取微信授权失败！请检查您的微信API设置和对应的权限");
                return;
            }
            bool isCover = this.chkIsCover.Checked;
            bool IsSuccess = Maticsoft.Web.Components.PostMsgHelper.GetGroups(token, openId, isCover);
            if (IsSuccess)
            {
                MessageBox.ShowSuccessTip(this, "获取微信用户小组成功！");
            }
            else
            {
                MessageBox.ShowFailTip(this, "服务器繁忙请重新再试！");
            }
        }


        
        #endregion

    }
}