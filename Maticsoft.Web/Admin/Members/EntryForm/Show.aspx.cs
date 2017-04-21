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
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 278; } } //客服管理_报名用户管理_详细页
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        public int Id
        {
            get
            {
                int id = 0;
                strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.BLL.Ms.EntryForm bll = new Maticsoft.BLL.Ms.EntryForm();
            Maticsoft.Model.Ms.EntryForm model = bll.GetModel(Id);
            if (null != model)
            {
                this.lblId.Text = model.Id.ToString();
                this.lblUserName.Text = model.UserName;
                if (model.Age.HasValue)
                {
                    this.lblAge.Text = model.Age.ToString();
                }
                this.lblEmail.Text = model.Email;
                this.lblTelPhone.Text = model.TelPhone;
                this.lblPhone.Text = model.Phone;
                this.lblQQ.Text = model.QQ;
                this.lblMSN.Text = model.MSN;
                this.lblHouseAddress.Text = model.HouseAddress;
                this.lblCompanyAddress.Text = model.CompanyAddress;
                if (model.RegionId.HasValue)
                {
                    this.lblRegionId.Text = GetRegionNameByRID((int)model.RegionId);
                }
                this.lblSex.Text = GetSex(model.Sex);
                this.lblDescription.Text = model.Description;
                this.lblremark.Text = model.Remark;
                if (model.State.HasValue)
                {
                    this.lblState.Text = GetState(model.State);
                }

                this.lblApplyType.Text = GetApplyType(model.ApplyType);
                
            }
        }

        #region 获取处理状态
        /// <summary>
        /// 获取处理状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetState(object target)
        {
            //0.未处理；1：已处理
            string state = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        state =Resources.Site.Untreated;
                        break;
                    case "1":
                        state = Resources.Site.Processed;
                        break;
                    default:
                        break;
                }
            }
            return state;
        }
        #endregion

        #region 获取审核状态
        /// <summary>
        /// 获取审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetSex(object target)
        {
            //0.男；1：女
            string state = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString().Trim())
                {
                    case "0":
                        state =Resources.Site.SexMale;
                        break;
                    case "1":
                        state =Resources.Site.SexWoman;
                        break;
                    default:
                        break;
                }
            }
            return state;
        }
        #endregion
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

        public string GetRegionNameByRID(int RID)
        {
            Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
            return bll.GetRegionNameByRID(RID);
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.Redirect("Modify.aspx?id=" + Id);
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}