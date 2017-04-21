using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;
using Maticsoft.Json.Conversion;

namespace Maticsoft.Web.Admin.WeChat.Menu
{
    public partial class MenuList : PageBaseAdmin
    {

        private  Maticsoft.WeChat.BLL.Core.Menu menuBll=new Maticsoft.WeChat.BLL.Core.Menu();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
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
            
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 批量修改分类顺序值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateSeq_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                int menuId = Common.Globals.SafeInt(gridView.DataKeys[i].Value.ToString(), 0);
                var item = (TextBox)this.gridView.Rows[i].FindControl("TextBox1");
                int seq = Common.Globals.SafeInt(item.Text, 0);
                if (menuId > 0 && seq > 0)
                {
                    menuBll.UpdateSeq(seq, menuId);
                }
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            //先授权 
            string AppId =Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            bool IsAuto=  Common.Globals.SafeBool(Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AutoLogin", -1, CurrentUser.UserType), false);
            string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
            if (String.IsNullOrWhiteSpace(token))
            {
                MessageBox.ShowFailTip(this, "获取微信授权失败！请检查您的微信API设置和对应的权限");
                return;
            }
            bool IsSuccess = Maticsoft.Web.Components.PostMsgHelper.CreateMenu(token, openId, IsAuto);
            if (IsSuccess)
            {
                MessageBox.ShowSuccessTip(this,"创建菜单成功！");
            }
            else
            {
                MessageBox.ShowFailTip(this,"服务器繁忙请重新再试！");
            }
        }
        


        #region gridView

        public void BindData()
        {
            #region

         
            #endregion gridView
              string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
              List<Maticsoft.WeChat.Model.Core.Menu> menuList = menuBll.GetMenuList(openId);
                

            //对商品数据进行排序
            List<Maticsoft.WeChat.Model.Core.Menu> orderList = new List<Maticsoft.WeChat.Model.Core.Menu>();
            var RootList = menuList.Where(c => c.ParentId == 0).OrderBy(c => c.Sequence).ToList();

            foreach (var item in RootList)
            {
                orderList = MenuOrder(item, menuList, orderList);
            }
            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSource = orderList;
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.DataBind();
        }
        private List<Maticsoft.WeChat.Model.Core.Menu> MenuOrder(Maticsoft.WeChat.Model.Core.Menu model, List<Maticsoft.WeChat.Model.Core.Menu> menuList, List<Maticsoft.WeChat.Model.Core.Menu> orderList)
        {
            orderList.Add(model);
            if (model.HasChildren)
            {
                var list = menuList.Where(c => c.ParentId == model.MenuId).OrderBy(c => c.Sequence);
                foreach (var item in list)
                {
                    MenuOrder(item, menuList, orderList);
                }
            }
            else
            {
                return orderList;
            }
            return orderList;
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
                }

                int num = (int)DataBinder.Eval(e.Row.DataItem, "ParentId")==0?0:1;
                string str = DataBinder.Eval(e.Row.DataItem, "Name").ToString(); 
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl control = e.Row.FindControl("spShowImage") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    control.Visible = false;
                }
                Label label = e.Row.FindControl("lblName") as Label;
                label.Text = str;

                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

            menuBll.DeleteEx((int)gridView.DataKeys[e.RowIndex].Value);
            Common.MessageBox.ShowSuccessTip(this, "删除成功！");
            BindData();
        }




        private bool RegURL(string path)
        {
            Regex regex = new Regex("^[a-zA-z]+://(//w+(-//w+)*)(//.(//w+(-//w+)*))*(//?//S*)?$");
            Match match = regex.Match(path);
            return match.Success;
        }

        #endregion

        #region Ajax方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateSeqNum":

                    writeText = UpdateSeqNum();
                    break;
                default:
                    writeText = UpdateSeqNum();
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string UpdateSeqNum()
        {
            JsonObject json = new JsonObject();
            int MenuId = Common.Globals.SafeInt(this.Request.Form["MenuId"], 0);
            int UpdateValue = Common.Globals.SafeInt(this.Request.Form["UpdateValue"], 0);
            if (MenuId == 0 || UpdateValue == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                if (menuBll.UpdateSeq(UpdateValue, MenuId))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
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

        public string GetTypeName(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string  type = target.ToString();
                switch (type)
                {
                    case "view":
                        str = "页面跳转";
                        break;
                    case "click":
                        str = "发送消息";
                        break;

                    default:
                        break;
                }
            }
            return str;
        }

    }
}