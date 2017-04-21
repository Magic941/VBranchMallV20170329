/**
* EntryForm.cs
*
* 功 能： [N/A]
* 类 名： EntryForm
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.UI;
using Maticsoft.Common;
namespace Maticsoft.Web.Ms.EntryForm
{
    public partial class Modify : PageBaseAdmin
    {
        Maticsoft.BLL.Ms.EntryForm bll = new Maticsoft.BLL.Ms.EntryForm();
        Maticsoft.BLL.Members.UsersExp userexpbll = new BLL.Members.UsersExp();
        Maticsoft.BLL.Members.Users userbll = new BLL.Members.Users();

        protected override int Act_PageLoad { get { return 277; } } //客服管理_报名用户管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(id))
                {
                    ShowInfo(int.Parse(id));
                }
            }
        }

        protected void BindData()
        {
            for (int i = 0; i <= 119; i++)
            {
                this.dropAge.Items.Add(i.ToString());
            }
        }


        private void ShowInfo(int Id)
        {

            Maticsoft.Model.Ms.EntryForm model = bll.GetModel(Id);
            if (null != model)
            {
                this.lblId.Text = model.Id.ToString();
                this.txtUserName.Text = model.UserName;
                BindData();
                if (model.Age.HasValue)
                {
                    this.dropAge.SelectedValue = model.Age.ToString();
                }
                this.txtEmail.Text = model.Email;
                this.txtTelPhone.Text = model.TelPhone;
                this.txtPhone.Text = model.Phone;
                this.txtQQ.Text = model.QQ;
                this.txtApplyType.Text = model.ApplyType.ToString()=="0"?"分销店":"服务店" ;
                this.txtMSN.Text = model.MSN;
                this.txtHouseAddress.Text = model.HouseAddress;
                this.txtCompanyAddress.Text = model.CompanyAddress;
                if (model.RegionId.HasValue)
                {
                    this.dropProvince.Area_iID = (int)model.RegionId;
                }
                this.dropSex.SelectedValue = model.Sex.Trim();
                this.txtDescription.Text = model.Description;
                this.txtRemark.Text = model.Remark;
                this.radAppStatus.SelectedValue="0";
                if(model.AppStatus.HasValue)
                {
                    this.radAppStatus.SelectedValue = model.AppStatus.Value.ToString();
                }

                if (model.State.HasValue)
                {
                    this.dropState.SelectedValue = model.State.ToString();
                }
            }

        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            string username = this.txtUserName.Text.Trim();
            string remark = this.txtRemark.Text.Trim();
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.ShowFailTip(this, Resources.MsEntryForm.ErrorNameNotNull);
                return;
            }
            if (remark.Length > 300)
            {
                MessageBox.ShowFailTip(this, Resources.MsEntryForm.ErrorRemarkoverlength);
                return;
            }
            int Id = int.Parse(this.lblId.Text);

            Maticsoft.Model.Ms.EntryForm model = bll.GetModel(Id);
            model.UserName = username;
            model.Age = int.Parse(this.dropAge.SelectedValue);
            model.Email = this.txtEmail.Text;
            model.TelPhone = this.txtTelPhone.Text;
            model.Phone = this.txtPhone.Text;
            model.QQ = this.txtQQ.Text;
            model.MSN = this.txtMSN.Text;
            model.HouseAddress = this.txtHouseAddress.Text;
            model.CompanyAddress = this.txtCompanyAddress.Text;
            model.RegionId = Convert.ToInt32(this.dropProvince.Area_iID);
            model.Sex = this.dropSex.SelectedValue;
            model.Description = this.txtDescription.Text;
            model.Remark = remark;
            model.State = int.Parse(this.dropState.SelectedValue);

            model.AppStatus = int.Parse(this.radAppStatus.SelectedValue, 0);
            
            Maticsoft.Model.Members.Users user = new Model.Members.Users();
            int UserId = userbll.GetUserIdByUserName(model.UserName);
            int UserOldType=0;
            
                if (model.ApplyType == 0)
                {
                    UserOldType = 3;
                }
                else
                {
                    UserOldType = 4;
                }

            if (bll.Update(model) && model.AppStatus ==1)
            {
                userexpbll.UpdateOldType(UserOldType, UserId);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "list.aspx");
            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
        #region 获取申请店铺类型
        /// <summary>
        /// 获取审核状态 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetApplyType(object target)
        {
            //
            string state = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString().Trim())
                {
                    case "0":
                        state = "分销店";
                        break;
                    case "1":
                        state = "服务店";
                        break;
                    default:
                        break;
                }
            }
            return state;
        }
        #endregion
    }
}
