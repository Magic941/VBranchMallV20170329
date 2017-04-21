/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify.cs
*
* Ver    变更日期                             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年5月31日 14:38:42   孙鹏   创建
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
namespace Maticsoft.Web.Admin.AdvertisePosition
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 367; } } //网站管理_广告管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    int AdvPositionId = (Convert.ToInt32(Request.Params["id"]));
                    ShowInfo(AdvPositionId);
                }
            }
        }

        private void ShowInfo(int AdvPositionId)
        {
            Maticsoft.BLL.Settings.AdvertisePosition bll = new Maticsoft.BLL.Settings.AdvertisePosition();
            Maticsoft.Model.Settings.AdvertisePosition model = bll.GetModel(AdvPositionId);
            this.lblAdvPositionId.Text = model.AdvPositionId.ToString();
            this.txtAdvPositionName.Text = model.AdvPositionName;
            this.ddlShowType.SelectedValue = model.ShowType.Value.ToString();
            this.txtRepeatColumns.Text = model.RepeatColumns.ToString();
            this.txtWidth.Text = model.Width.ToString();
            this.txtHeight.Text = model.Height.ToString();
            this.txtAdvHtml.Text = model.AdvHtml;
            this.chkIsOne.Checked = model.IsOne;
            this.txtTimeInterval.Text = model.TimeInterval.ToString();
        }

        public void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Settings.AdvertisePosition model = new Maticsoft.Model.Settings.AdvertisePosition();
            int AdvPositionId = int.Parse(this.lblAdvPositionId.Text);

            //始终清空自定义广告位
            model.AdvHtml = string.Empty;

            if (this.txtAdvPositionName.Text.Trim().Length == 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "广告位名称不能为空！");
                return;
            }
            string strShowType = this.ddlShowType.SelectedValue;
            if (strShowType == "1")
            {
                if (!PageValidate.IsNumber(txtRepeatColumns.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请数如正确的横向平铺时行显示个数！");
                    return;
                }
                else
                {
                    model.RepeatColumns = Globals.SafeInt(this.txtRepeatColumns.Text, 0);
                }
            }
            if (strShowType == "4")
            {
                if (string.IsNullOrWhiteSpace(this.txtAdvHtml.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请数广告位内容！");
                    return;
                }
                else
                {
                    model.AdvHtml = this.txtAdvHtml.Text.Trim();
                }
            }
            if (!PageValidate.IsNumber(this.txtWidth.Text) || !PageValidate.IsNumber(this.txtHeight.Text))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请设置此广告位里面广告内容的宽、高，单位为像素（px）！");
                return;
            }
            else
            {
                model.Width = Globals.SafeInt(this.txtWidth.Text, 0);
                model.Height = Globals.SafeInt(this.txtHeight.Text, 0);
            }
            if (this.chkIsOne.Checked)
            {
                if (!PageValidate.IsNumber(txtTimeInterval.Text))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请输入正确的循环广告时间间隔！");
                    return;
                }
                else
                {
                    model.IsOne = true;
                    model.TimeInterval = Globals.SafeInt(this.txtTimeInterval.Text, 0);
                }
            }
            else
            {
                model.IsOne = false;
            }
            model.AdvPositionId = AdvPositionId;
            model.ShowType = Globals.SafeInt(strShowType, -1);
            model.AdvPositionName = this.txtAdvPositionName.Text.Trim();
            model.CreatedDate = DateTime.Now;
            model.CreatedUserID = CurrentUser.UserID;

            Maticsoft.BLL.Settings.AdvertisePosition bll = new Maticsoft.BLL.Settings.AdvertisePosition();
            if (bll.Update(model))
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "网络异常，请稍后再试！");
                return;
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
