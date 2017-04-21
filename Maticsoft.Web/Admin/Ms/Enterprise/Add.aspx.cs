/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01   
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using Maticsoft.Common;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Ms.Enterprise
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 317; } } //企业管理_添加页
        Maticsoft.BLL.Ms.Enterprise bll = new Maticsoft.BLL.Ms.Enterprise();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string UserName = this.txtUserName.Text.Trim();
            string Password = this.txtPassword.Text.Trim();
            if (UserName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "用户名不能为空！");
                return;
            }
            User newUser = new User();
            if (newUser.HasUserByUserName(UserName))
            {
                MessageBox.ShowServerBusyTip(this, Resources.Site.TooltipUserExist);
                return;
            }
            if (Password.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "密码不能为空！");
                return;
            }
            if (Password.Length >16)
            {
                MessageBox.ShowServerBusyTip(this, "密码不能超过16个字符！");
                return;
            }
            string Name = this.txtName.Text.Trim();
            if (Name.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "企业名称不能为空！");
                return;
            }
            if (Name.Length >100)
            {
                MessageBox.ShowServerBusyTip(this, "企业名称请控制在1~100字符！");
                return;
            }
            if (bll.Exists(Name))
            {
                MessageBox.ShowServerBusyTip(this, "该企业名称已经被注册，请更换企业名称再操作！");
                return;
            }
            int EnterpriseID = 0;
            //后期考虑事物
            try
            {
                #region 企业特信息
                Maticsoft.Model.Ms.Enterprise model = new Maticsoft.Model.Ms.Enterprise();
                model.Name = Name;
                model.Introduction = this.txtIntroduction.Text;
                model.RegisteredCapital = Globals.SafeInt(this.txtRegisteredCapital.Text, 0);
                model.TelPhone = this.txtTelPhone.Text;
                model.CellPhone = this.txtCellPhone.Text;
                model.ContactMail = this.txtContactMail.Text;
                model.RegionID = this.RegionID.Region_iID;
                model.Address = this.txtAddress.Text; ;
                model.Remark = this.txtRemark.Text; ;
                model.Contact = this.txtContact.Text;
                model.UserName = this.txtUserName.Text;
                string EstablishedDate = this.txtEstablishedDate.Text;
                if (PageValidate.IsDateTime(EstablishedDate))
                {
                    model.EstablishedDate = Globals.SafeDateTime(EstablishedDate, DateTime.Now);
                }
                else
                {
                    model.EstablishedDate = null;
                }
                model.EstablishedCity = this.RegionEstablishedCity.Region_iID;
                model.LOGO = this.txtLOGO.Text;
                model.Fax = this.txtFax.Text;
                model.PostCode = this.txtPostCode.Text;
                model.HomePage = this.txtHomePage.Text;
                model.ArtiPerson = this.txtArtiPerson.Text;
                model.EnteRank = Globals.SafeInt(this.dropEnteRank.SelectedValue, 0);
                model.EnteClassID = Globals.SafeInt(this.dropEnteClassID.SelectedValue, 0);
                model.CompanyType = Globals.SafeInt(this.dropCompanyType.SelectedValue, 0);
                model.BusinessLicense = this.txtBusinessLicense.Text;
                model.TaxNumber = this.txtTaxNumber.Text;
                model.AccountBank = this.txtAccountBank.Text;
                model.AccountInfo = this.txtAccountInfo.Text;
                model.ServicePhone = this.txtServicePhone.Text;
                model.QQ = this.txtQQ.Text;
                model.MSN = this.txtMSN.Text;
                model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
                model.CreatedDate = DateTime.Now;
                model.CreatedUserID = CurrentUser.UserID;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedUserID = CurrentUser.UserID;
                model.Balance = Globals.SafeDecimal(this.txtBalance.Text, 0);
                model.AgentID = Globals.SafeInt(this.txtAgentID.Text, 0); 
                #endregion

                EnterpriseID = bll.Add(model);
                if (EnterpriseID > 0)
                {
                    #region 用户基本信息
                    newUser.UserName = UserName;
                    newUser.NickName = this.txtName.Text;
                    newUser.Password = AccountsPrincipal.EncryptPassword(Password);
                    newUser.TrueName = "";
                    newUser.Sex = "1";
                    newUser.Phone = this.txtCellPhone.Text;
                    newUser.Email = this.txtContactMail.Text;
                    newUser.EmployeeID = 0;
                    newUser.DepartmentID = EnterpriseID.ToString();
                    newUser.Activity = true;
                    newUser.UserType = "EE";
                    newUser.Style = 1;
                    newUser.User_dateCreate = DateTime.Now;
                    newUser.User_iCreator = CurrentUser.UserID;
                    newUser.User_dateValid = DateTime.Now;
                    newUser.User_cLang = "zh-CN"; 
                    #endregion

                    int userid = newUser.Create();
                    if (userid == -100)
                    {
                        //若创建用户失败则删除创建的企业编号。
                        bll.Delete(EnterpriseID);
                        MessageBox.ShowServerBusyTip(this, Resources.Site.TooltipUserExist);
                    }
                    else
                    {

                        #region 用户扩展信息
                        BLL.Members.UsersExp usersEx = new BLL.Members.UsersExp();
                        usersEx.UserID = userid;
                        usersEx.BirthdayVisible = 0;
                        usersEx.BirthdayIndexVisible = false;
                        usersEx.ConstellationVisible = 0;
                        usersEx.ConstellationIndexVisible = false;
                        usersEx.NativePlaceVisible = 0;
                        usersEx.NativePlaceIndexVisible = false;
                        usersEx.RegionId = 0;
                        usersEx.AddressVisible = 0;
                        usersEx.AddressIndexVisible = false;
                        usersEx.BodilyFormVisible = 0;
                        usersEx.BodilyFormIndexVisible = false;
                        usersEx.BloodTypeVisible = 0;
                        usersEx.BloodTypeIndexVisible = false;
                        usersEx.MarriagedVisible = 0;
                        usersEx.MarriagedIndexVisible = false;
                        usersEx.PersonalStatusVisible = 0;
                        usersEx.PersonalStatusIndexVisible = false;
                        usersEx.LastAccessIP = "";
                        usersEx.LastAccessTime = DateTime.Now;
                        usersEx.LastLoginTime = DateTime.Now;
                        usersEx.LastPostTime = DateTime.Now; 
                        #endregion

                        if (usersEx.AddUsersExp(usersEx))
                        {
                            MessageBox.ShowSuccessTip(this, "添加成功！", "List.aspx");
                        }
                        else
                        {
                            //若创建用户失败则删除用户基本表信息
                            newUser.UserID = userid;
                            newUser.Delete();

                            //若创建用户失败则删除用户扩展表信息
                            usersEx.DeleteUsersExp(userid);

                            //若创建用户失败则删除创建的企业编号。
                            bll.Delete(EnterpriseID);
                            MessageBox.ShowFailTip(this, "添加失败！");
                        }
                    }
                }
                else
                {
                    //若创建用户失败则删除创建的企业编号。
                    bll.Delete(EnterpriseID);
                    MessageBox.ShowFailTip(this, "添加失败！");
                }
            }
            catch (Exception ex)
            {
                //若创建用户失败则删除创建的企业编号。
                bll.Delete(EnterpriseID);
                throw ex;
            }
            finally
            {

            }
        }


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
