using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Scene
{
    public partial class AddScene : PageBaseAdmin
    {
        Maticsoft.WeChat.BLL.Core.Scene sceneBll = new Maticsoft.WeChat.BLL.Core.Scene();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }
        

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.WeChat.Model.Core.Scene model = new Maticsoft.WeChat.Model.Core.Scene();
            string name = this.tName.Text;
            if (String.IsNullOrWhiteSpace(name))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入您的推广场景名称");
                return;
            }
            string openId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_OpenId", -1, CurrentUser.UserType);
            model.OpenId = openId;
            model.Name = this.tName.Text;
            model.Remark = this.tDesc.Text;
            model.CreateTime = DateTime.Now;
            string AppId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", -1, CurrentUser.UserType);
            string AppSecret = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", -1, CurrentUser.UserType);
            string domain = Common.Globals.DomainFullName;
            if (domain.Contains("192.168.1") || domain.Contains("localhost"))
            {
                MessageBox.ShowFailTip(this, "此功能需要请求远程服务器，请在正式服务器使用");
                return;
            }
            try
            {
                string token = Maticsoft.Web.Components.PostMsgHelper.GetToken(AppId, AppSecret);
                if (String.IsNullOrWhiteSpace(token))
                {
                    MessageBox.ShowFailTip(this, "获取微信授权失败！请检查您的微信API设置和对应的权限");
                    return;
                }
                int sceneId = sceneBll.Add(model);
                if (sceneId > 0)
                {
                    //获取推广图片
                    model.ImageUrl= Maticsoft.Web.Components.PostMsgHelper.GetQRImage(token, sceneId);
                    model.SceneId = sceneId;
                    sceneBll.Update(model);
                    MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败！");
                }
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, "生成场景二维码失败，请检查您的公众号配置或服务器环境");
                return;
            }
        }
    }
}