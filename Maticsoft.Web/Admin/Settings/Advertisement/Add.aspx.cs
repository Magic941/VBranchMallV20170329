/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
*
* Ver    变更日期                             负责人      变更内容
* ───────────────────────────────────
* V0.01  2012年5月31日 14:33:10  孙鹏             创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using Maticsoft.Common;
using System.Collections;
using Maticsoft.BLL.SysManage;

namespace Maticsoft.Web.Admin.Settings.Advertisement
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 371; } } //网站管理_广告内容管理_添加页
        private Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
        private Maticsoft.BLL.Settings.AdvertisePosition bllPosition = new BLL.Settings.AdvertisePosition();

        public int AdPositionID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                    return;
                }
                AltInfo();
            }
        }

        private void AltInfo()
        {
            Maticsoft.BLL.Settings.AdvertisePosition bllPosition = new BLL.Settings.AdvertisePosition();
            Maticsoft.Model.Settings.AdvertisePosition modelPosition = bllPosition.GetModel(AdPositionID);
            if (modelPosition != null)
            {
                this.Literal2.Text = modelPosition.AdvPositionName + "】广告位新增广告内容";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Settings.Advertisement model = new Maticsoft.Model.Settings.Advertisement();
            model.AdvPositionId = AdPositionID;// int.Parse(this.ddlAdPosition.SelectedValue);
            if (string.IsNullOrWhiteSpace(this.txtAdvertisementName.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "广告名称不能为空！");
                return;
            }
            model.AdvertisementName = this.txtAdvertisementName.Text;
            string strAdType = string.Empty;
            if (this.rbTextContent.Checked)
            {
                strAdType = "0";
            }
            else if (this.rbImgContent.Checked)
            {
                strAdType = "1";
            }
            else if (rbFlashContent.Checked)
            {
                strAdType = "2";
            }
            else
            {
                strAdType = "3";
            }
            //if (strAdType.Equals("0"))
            //{
            //    if (string.IsNullOrWhiteSpace(this.txtAlternateText.Text))
            //    {
            //        Maticsoft.Common.MessageBox.ShowFailTip(this, "广告语不能为空！");
            //        return;
            //    }
            //}
            //待上传的图片名称
            string tempFile = string.Format("/Upload/Temp/{0}", DateTime.Now.ToString("yyyyMMdd"));
            string ImageFile = string.Format("/Upload/AD/{0}", AdPositionID);
            ArrayList imageList = new ArrayList();
            if (strAdType.Equals("1"))
            {
                if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择要上传的图片！");
                    return;
                }

                string imageUrl = string.Format(this.hfFileUrl.Value, "");

                imageList.Add(imageUrl.Replace(tempFile, ""));

                model.FileUrl = imageUrl.Replace(tempFile, ImageFile);
            }

            //if (strAdType.Equals("0")||strAdType.Equals("1"))
            //{
            //    if (!string.IsNullOrWhiteSpace(this.txtNavigateUrl.Text))
            //    {
            //        Maticsoft.Common.MessageBox.ResponseScript(this, "clickautohide(5, \"连接地址不能为空！\", 2000);");
            //        return;
            //    }
            //}

            if (strAdType.Equals("2"))
            {
                //上传Flash

                model.FileUrl = this.hfSwfUrl.Value;
            }
            if (strAdType.Equals("3"))
            {
                if (string.IsNullOrWhiteSpace(this.txtAdvHtml.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "广告HTML代码不能为空！");
                    return;
                }
                else
                {
                    model.AdvHtml = this.txtAdvHtml.Text;
                }
            }
            model.ContentType = int.Parse(strAdType);
            model.AlternateText = this.txtAlternateText.Text;
            model.NavigateUrl = this.txtNavigateUrl.Text;

            if (!PageValidate.IsNumber(this.txtImpressions.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "显示频率格式不正确！");
                return;
            }
            model.Impressions = int.Parse(this.txtImpressions.Text);
            model.CreatedDate = DateTime.Now;
            model.CreatedUserID = CurrentUser.UserID;
            if (this.chkIsValid.Checked)
            {
                model.State = 1;
            }
            else
            {
                model.State = 0;
            }
            if (!string.IsNullOrWhiteSpace(this.txtStartDate.Text))
            {
                if (!PageValidate.IsDateTime(this.txtStartDate.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入正确的开始时间！");
                    return;
                }
                else
                {
                    model.StartDate = DateTime.Parse(this.txtStartDate.Text);
                }
            }
            if (!string.IsNullOrWhiteSpace(this.txtEndDate.Text))
            {
                if (!PageValidate.IsDateTime(this.txtEndDate.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入正确的结束时间！");
                    return;
                }
                else
                {
                    model.EndDate = DateTime.Parse(this.txtEndDate.Text);
                }
            }
            if (!PageValidate.IsNumber(txtDayMaxPV.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "最大PV格式不正确！");
                return;
            }
            model.DayMaxPV = int.Parse(this.txtDayMaxPV.Text);
            if (!PageValidate.IsNumber(txtDayMaxIP.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "最大IP格式不正确！");
                return;
            }
            model.DayMaxIP = int.Parse(this.txtDayMaxIP.Text);
            if (string.IsNullOrWhiteSpace(this.txtCPMPrice.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入正确的价格！");
                return;
            }
            decimal CPMPrice = 0M;
            if (!decimal.TryParse(this.txtCPMPrice.Text, out CPMPrice))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "价格格式不正确！");
                return;
            }
            model.CPMPrice = CPMPrice;
            if (this.rbAutoStop.Checked)
            {
                model.AutoStop = 1;
            }
            else if (this.rbNoStup.Checked)
            {
                model.AutoStop = 0;
            }
            else
            {
                model.AutoStop = -1;
            }
            model.Sequence = bll.GetMaxSequence();

            //if (string.IsNullOrWhiteSpace(this.txtEnterpriseID.Text))
            //{
            //    Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入广告主的名称！");
            //    return;
            //}

            string enterpriseName = this.txtEnterpriseID.Text;
            BLL.Ms.Enterprise enteBLL = new BLL.Ms.Enterprise();
            if (!string.IsNullOrWhiteSpace(enterpriseName))
            {
                List<Model.Ms.Enterprise> list = enteBLL.GetModelByEnterpriseName(enterpriseName);
                if (list.Count > 0)
                { model.EnterpriseID = list[0].EnterpriseID; }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "没有找到相应商户，请重新输入！");
                    return;
                }
            }
            else
            {
                model.EnterpriseID = -1;
            }

            if (bll.Add(model))
            {
                string url = string.Format("SingleList.aspx?id={0}", AdPositionID);
                this.btnCancle.Enabled = false;
                this.btnSave.Enabled = false;
                if (!string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
                {
                    //将图片从临时文件夹移动到正式的文件夹下
                   // Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);

                    FileManager.MoveImageForFTP(string.Format(this.hfFileUrl.Value, ""), ImageFile);
                }
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功", url);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "网络异常，请稍后再试！");
                return;
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            //Response.Redirect("list.aspx");
        }
    }
}