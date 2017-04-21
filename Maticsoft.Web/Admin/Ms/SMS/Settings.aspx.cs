using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.Ms.SMS
{
    public partial class Settings : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 112; } } //运营管理_是否显示API接口设置页面
        protected new int Act_UpdateData = -1;    //运营管理_保存短信接口设置

        protected void Page_Load(object sender, EventArgs e)
        {
            //是否有编辑信息的权限
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                btnSave.Visible = false;
            }
            if (!IsPostBack)
            {
                BoundData();
            }
        }



        private void BoundData()
        {
            this.txtSerialNo.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_SerialNo");
            this.txtKey.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Key");
            this.txtPassword.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Pwd");
            this.chkOpen.Checked = BLL.SysManage.ConfigSystem.GetBoolValueByCache("Emay_SMS_IsOpen");
            this.txtSMSContent.Text = BLL.SysManage.ConfigSystem.GetValueByCache("Emay_SMS_Content");

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string SerialNo = this.txtSerialNo.Text;
                string Key = this.txtKey.Text;
                string content = this.txtSMSContent.Text;
                if (String.IsNullOrWhiteSpace(SerialNo))
                {
                    Common.MessageBox.ShowFailTip(this,"请填写软件序列号");
                    return;
                }
                if (String.IsNullOrWhiteSpace(Key))
                {
                    Common.MessageBox.ShowFailTip(this, "请填写自定义关键字");
                    return;
                }
                if (String.IsNullOrWhiteSpace(content))
                {
                    Common.MessageBox.ShowFailTip(this, "请填写短信发送内容");
                    return;
                }
                BLL.SysManage.ConfigSystem.Modify("Emay_SMS_SerialNo", SerialNo, "亿美短信接口 软件序列号",ApplicationKeyType.OpenAPI);
                BLL.SysManage.ConfigSystem.Modify("Emay_SMS_Key", Key, "亿美短信接口 自定义关键字", ApplicationKeyType.OpenAPI);
                if (!String.IsNullOrWhiteSpace(this.txtPassword.Text))
                {
                    BLL.SysManage.ConfigSystem.Modify("Emay_SMS_Pwd", this.txtPassword.Text, "亿美短信接口序列号密码", ApplicationKeyType.OpenAPI);
                }
                bool IsOpen = this.chkOpen.Checked;

                BLL.SysManage.ConfigSystem.Modify("Emay_SMS_IsOpen", IsOpen.ToString(), "是否启用短信机制", ApplicationKeyType.OpenAPI);
                BLL.SysManage.ConfigSystem.Modify("Emay_SMS_Content", content, "短信发送内容", ApplicationKeyType.OpenAPI);

                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.OpenAPI);//清除网站设置的缓存文件
                Cache.Remove("ConfigSystemHashList");
                if (IsOpen)
                {
                    //注册序列号
                    Maticsoft.Web.Components.SMSHelper.RegistEx();
                }
                else
                {
                    //注销序列号
                    Maticsoft.Web.Components.SMSHelper.Logout();
                }

                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "Settings.aspx");

            }
            catch (Exception )
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "操作失败", "Settings.aspx");
            }
        }
      
    }
}