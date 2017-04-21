using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.CMS;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Command
{
    public partial class AddCommand : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 632; } } //移动营销_微信导航菜单管理_添加页
        Maticsoft.WeChat.BLL.Core.Command commandBll = new Maticsoft.WeChat.BLL.Core.Command();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
                if (String.IsNullOrWhiteSpace(openId))
                {
                    MessageBox.ShowFailTip(this, "您还未填写微信原始ID，请在公众号配置中填写！", "/admin/WeChat/Setting/Config.aspx");
                }
                List<Maticsoft.WeChat.Model.Core.Action> actionList = Maticsoft.WeChat.BLL.Core.Action.GetAllAction();
                this.dropAction.DataSource = actionList;
                this.dropAction.DataTextField = "Name";
                this.dropAction.DataValueField = "ActionId";
                this.dropAction.DataBind();
                this.dropAction.Items.Insert(0, new ListItem("请选择", "0"));
                this.txtSequence.Text = (commandBll.GetSequence(openId) + 1).ToString();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            Maticsoft.WeChat.Model.Core.Command commandModel = new Maticsoft.WeChat.Model.Core.Command();
            int actionId = Common.Globals.SafeInt(dropAction.SelectedValue, 0);
            string name = this.tName.Text;
            if (actionId == 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择指定操作");
                return;
            }
            if (String.IsNullOrWhiteSpace(name))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入指令名称");
                return;
            }
            commandModel.Name = name;
            commandModel.ActionId = Common.Globals.SafeInt(dropAction.SelectedValue, 0);
            commandModel.ParseType = Common.Globals.SafeInt(ddParseType.SelectedValue, 0);
            commandModel.Status = chkStatus.Checked ? 1 : 0;
            int targetId = Common.Globals.SafeInt(this.ddTarget.Text, 0);
            commandModel.TargetId = targetId;
            if (commandModel.ParseType == 0)
            {
                commandModel.ParseLength = Common.Globals.SafeInt(this.txtParseType.Text, 0);
            }
            else
            {
                commandModel.ParseChar = this.txtParseType.Text.Trim();
            }

            commandModel.Remark = this.txtDesc.Text;
            commandModel.Sequence = Common.Globals.SafeInt(this.txtSequence.Text, 0);
            commandModel.OpenId = openId;
            if (commandBll.Add(commandModel) > 0)
            {
                MessageBox.ShowSuccessTip(this, "操作成功", "CommandList.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "操作失败！");
            }
        }

        #region dropDown IndexChange  事件

        protected void dropAction_IndexChange(object sender, EventArgs e)
        {
            int actionId = Common.Globals.SafeInt(this.dropAction.SelectedValue, 0);
            this.ddTarget.Visible = true;
            switch (actionId)
            {
                //文章栏目
                case 1:
                    Maticsoft.BLL.CMS.ContentClass classBll = new ContentClass();
                    List<Maticsoft.Model.CMS.ContentClass> classList =
                        classBll.GetModelList(" Depth=1 and State=0");
                    this.ddTarget.DataSource = classList;
                    ddTarget.DataTextField = "ClassName";
                    ddTarget.DataValueField = "ClassID";
                    ddTarget.DataBind();
                    ddTarget.Items.Insert(0, new ListItem("请选择", "0"));
                    break;
                //商品分类
                case 2:
                    Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new CategoryInfo();
                    List<Maticsoft.Model.Shop.Products.CategoryInfo> CategoryInfos =
                        cateBll.GetModelList(" Depth=1");
                    this.ddTarget.DataSource = CategoryInfos;
                    ddTarget.DataTextField = "Name";
                    ddTarget.DataValueField = "CategoryId";
                    ddTarget.DataBind();
                    ddTarget.Items.Insert(0, new ListItem("请选择", "0"));
                    break;
                default:
                    this.ddTarget.Visible = false;
                    break;
            }
            //Maticsoft.BLL.Shop.Products.   ddCateImage
        }

        #endregion



    }
}