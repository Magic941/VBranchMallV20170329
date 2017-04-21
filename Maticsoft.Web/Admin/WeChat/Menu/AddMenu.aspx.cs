using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.WeChat.Menu
{
    public partial class AddMenu : PageBaseAdmin
    {
      private  Maticsoft.WeChat.BLL.Core.Menu menuBll = new Maticsoft.WeChat.BLL.Core.Menu();
      private  Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
      private Maticsoft.BLL.CMS.ContentClass classBll = new BLL.CMS.ContentClass();
      private Maticsoft.BLL.CMS.Content contentBll = new BLL.CMS.Content();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!IsPostBack)
            {
                string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
                if (String.IsNullOrWhiteSpace(openId))
                {
                    MessageBox.ShowFailTipScript(this, "您还未填写微信原始ID，请在公众号配置中填写！", "window.parent.location.href='/admin/WeChat/Setting/Config.aspx';");
                }
                List<Maticsoft.WeChat.Model.Core.Action> actionList = Maticsoft.WeChat.BLL.Core.Action.GetAllAction();
                this.ddMenuAction.DataSource = actionList;
                this.ddMenuAction.DataTextField = "Name";
                this.ddMenuAction.DataValueField = "ActionId";
                this.ddMenuAction.DataBind();
                this.ddMenuAction.Items.Insert(0, new ListItem("自定义网址", "0"));
                this.ddMenuAction.Items.Insert(1, new ListItem("微信商城", "-1"));
                this.ddMenuAction.Items.Insert(2, new ListItem("微官网", "-2"));
                this.ddMenuAction.Items.Insert(3, new ListItem("我的账户", "-3"));
                this.ddMenuAction.Items.Insert(4, new ListItem("商品分类", "-4"));
                this.ddMenuAction.Items.Insert(5, new ListItem("我的订单", "-5"));
                this.ddMenuAction.Items.Insert(6, new ListItem("我的会员卡", "-6"));
                this.ddMenuAction.Items.Insert(7, new ListItem("会员签到", "-7"));
                this.ddMenuAction.Items.Insert(8, new ListItem("我要报名", "-8"));
                this.ddMenuAction.Items.Insert(9, new ListItem("单篇文章", "-9"));
                this.ddMenuAction.Items.Insert(10, new ListItem("优惠券兑换", "-10"));


                //商品一级分类
                this.ddCategory.DataSource = cateBll.GetCategorysByDepth(1);
                this.ddCategory.DataTextField = "Name";
                this.ddCategory.DataValueField = "CategoryId";
                this.ddCategory.DataBind();
                this.ddCategory.Items.Insert(0, new ListItem("请选择", "0"));

                //栏目分类
                List<Maticsoft.Model.CMS.ContentClass> AllClass=  Maticsoft.BLL.CMS.ContentClass.GetAllClass();
                this.ddCMSClass.DataSource = AllClass.Where(c => c.Depth == 1);
                this.ddCMSClass.DataTextField = "ClassName";
                this.ddCMSClass.DataValueField = "ClassID";
                this.ddCMSClass.DataBind();
                this.ddCMSClass.Items.Insert(0, new ListItem("请选择", "0"));

                ddlParent.DataSource = menuBll.GetList(-1, "ParentId=0", "Sequence");
                this.ddlParent.DataTextField = "Name";
                this.ddlParent.DataValueField = "MenuId";
                this.ddlParent.DataBind();
                this.ddlParent.Items.Insert(0, new ListItem("主菜单", "0"));
                this.txtSequence.Text = (menuBll.GetSequence(openId) + 1).ToString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.Model.Core.Menu menuModel = new Maticsoft.WeChat.Model.Core.Menu();
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            int  parentId= Common.Globals.SafeInt(ddlParent.SelectedValue, 0);
          



            int actionId = Common.Globals.SafeInt(ddMenuAction.SelectedValue, 0);
            string name = this.tName.Text;
            int categoryId = Common.Globals.SafeInt(ddCategory.SelectedValue, 0);
            string remark = this.txtRemark.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入菜单名称");
                return;
            }
            //判断是否超出了限制
            int count = menuBll.GetRecordCount("ParentId=" + parentId);
            if (parentId == 0)
            {
                if (count >= 3)
                {
                    MessageBox.ShowFailTip(this, "一级菜单请不要超过三个");
                    return;
                }
                if (name.Length > 4)
                {
                    MessageBox.ShowFailTip(this, "一级菜单名称请不要超过4个汉字");
                    return;
                }
            }
            else
            {
                if (count >= 5)
                {
                    MessageBox.ShowFailTip(this, "二级菜单请不要超过5个");
                    return;
                }
                if (name.Length >7)
                {
                    MessageBox.ShowFailTip(this, "二级菜单名称请不要超过7个汉字");
                    return;
                }

            }


            menuModel.MenuKey = "";
            menuModel.MenuUrl = "";
            menuModel.Type = "view";
            if (actionId <= 0)
            {
                switch (actionId)
                {
                    case -10:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.COM) + "Admin/CouponEx";
                        break;
                    case -9:
                        int articleId = Globals.SafeInt(this.hfArticleId.Value, 0);
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.COM) + "Article/Detail/" + articleId;
                        break;
                    case -8:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.COM) + "WeChat/Apply";
                        break;
                    case -7:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.COM) + "UserCenter/signpoint";
                        break;
                    case -6:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.COM) + "WeChat/usercard";
                        break;
                    case -5:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u/Orders";
                        break;
                    case -4:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "p/" + categoryId;
                        break;
                    case -3:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) + "u";
                        break;
                    case -2:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MPage);
                        break;
                    case -1:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                        break;
                    case 0:
                        menuModel.MenuUrl = String.IsNullOrWhiteSpace(remark) ? Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop) : remark;
                        break;
                    default:
                        menuModel.MenuUrl = Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.MShop);
                        break;
                }
            }
            // 走Action里面的逻辑
            else
            {
                menuModel.Type = "click";
                menuModel.MenuKey = "Action_" + actionId;
                menuModel.Remark = remark;
                if (actionId == 1)
                {
                    menuModel.Remark = ddCMSClass.SelectedValue;
                    menuModel.MenuKey = "Action_" + actionId + "_" + ddCMSClass.SelectedValue;
                }
            }

            menuModel.ParentId = parentId;
            menuModel.Name = name;
            menuModel.Status = chkStatus.Checked ? 1 : 0;
            menuModel.CreateDate = DateTime.Now;
            menuModel.HasChildren = false;
            menuModel.Sequence = Common.Globals.SafeInt(this.txtSequence.Text, 0);
            menuModel.OpenId = openId;
            if (menuBll.AddEx(menuModel))
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功", " window.parent.location.reload();");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败！");
            }
        }


        #region Ajax 方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "GetArticles":
                    writeText = GetArticles();
                    break;
                default:
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string GetArticles()
        {
            JsonObject json = new JsonObject();
            int classId = Common.Globals.SafeInt(this.Request.Form["ClassId"], 0);
             List<Maticsoft.Model.CMS.Content> ContentList= contentBll.GetModelList(classId);
             JsonArray newsArry = new JsonArray();
             JsonObject itemObj = null;
             foreach (var item in ContentList)
             {
                 itemObj = new JsonObject();
                 itemObj.Accumulate("title", item.Title);
                 itemObj.Accumulate("articleId", item.ContentID);
                 newsArry.Add(itemObj);
             }
             json.Accumulate("Data", newsArry.ToString());
            return json.ToString();
        }

        #endregion
    }
}