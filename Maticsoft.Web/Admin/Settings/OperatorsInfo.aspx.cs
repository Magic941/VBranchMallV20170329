using System;
using Maticsoft.Model.SysManage;
using Maticsoft.Common;
using System.Collections;

namespace Maticsoft.Web.Admin.Settings
{
    public partial class OperatorsInfo : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 160; } } //网站管理_是否显示网站设置页面
        protected new int Act_UpdateData = 161;    //网站管理_网站设置_编辑网站信息
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    btnSave.Visible = false;
                }
                BoundData();
            }
        }
        private void BoundData()
        {
            this.txtName.Text =GetValueByCache("Opertors_Name");  
            this.txtAddress.Text =GetValueByCache("Opertors_Address");
            this.txtTelephone.Text = GetValueByCache("Opertors_Telephone");
            this.txtBusinessHoursStart.Text = GetValueByCache("Opertors_BusinessHoursStart"); 
            this.txtBusinessHoursEnd.Text = GetValueByCache("Opertors_BusinessHoursEnd"); 
            this.txtDeliveryArea.Text = GetValueByCache("Opertors_DeliveryArea"); 
            this.txtServiceRadius.Text =GetValueByCache("Opertors_ServiceRadius"); 
            this.txtSentPrices.Text = GetValueByCache("Opertors_SentPrices"); 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdateKey("Opertors_Name", txtName.Text.Trim(), "运营名称(微预定)");
                UpdateKey("Opertors_Address", txtAddress.Text.Trim(), "运营地址(微预定)");
                UpdateKey("Opertors_Telephone", txtTelephone.Text.Trim(), "运营联系方式");
                UpdateKey("Opertors_BusinessHoursStart", txtBusinessHoursStart.Text.Trim(), "营业开始时间");
                UpdateKey("Opertors_BusinessHoursEnd", txtBusinessHoursEnd.Text.Trim(), "营业结束时间");
                UpdateKey("Opertors_DeliveryArea", txtDeliveryArea.Text.Trim(), "配送区域");
                UpdateKey("Opertors_ServiceRadius", txtServiceRadius.Text.Trim(), "服务半径");
                UpdateKey("Opertors_SentPrices", txtSentPrices.Text.Trim(), "起送价格");
                Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.Shop);//清除网站设置的缓存文件
                this.btnReset.Enabled = false;
                this.btnSave.Enabled = false;
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "OperatorsInfo.aspx");
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipTryAgainLater, "OperatorsInfo.aspx");
            }
        }
        public bool UpdateKey(string keyName, string value, string desc)
        {
            return BLL.SysManage.ConfigSystem.Modify(keyName, value, desc, ApplicationKeyType.Shop);
        }
        public string GetValueByCache(string keyName)
        {
            return BLL.SysManage.ConfigSystem.GetValueByCache(keyName);
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }
    }
}