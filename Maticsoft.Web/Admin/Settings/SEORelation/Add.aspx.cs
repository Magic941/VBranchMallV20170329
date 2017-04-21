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
using System.Text.RegularExpressions;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Settings.SEORelation
{
    public partial class Add : PageBaseAdmin
    {

        protected override int Act_PageLoad { get { return 393; } } //设置_SEO关联链接管理_添加页
        private  Maticsoft.BLL.Settings.SEORelation bll = new Maticsoft.BLL.Settings.SEORelation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
            {
                MessageBox.ShowAndBack(this, "您没有权限");
                return;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrWhiteSpace(this.txtKeyName.Text))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "链接文字不能为空！");
                return;
            }
            if (string.IsNullOrWhiteSpace(this.txtLinkURL.Text))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "链接地址不能为空！");
                return;
            }
               string KeyName = this.txtKeyName.Text;
            //判重
            if (bll.Exists(KeyName))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "链接文字已经存在！");
                return;
            }
         
            string LinkURL = this.txtLinkURL.Text;
            bool IsCMS = this.chkIsCMS.Checked;
            bool IsShop = this.chkIsShop.Checked;
            bool IsSNS = this.chkIsSNS.Checked;
            bool IsComment = this.chkIsComment.Checked;
            bool IsActive = this.chkIsActive.Checked;

            Maticsoft.Model.Settings.SEORelation model = new Maticsoft.Model.Settings.SEORelation();
            model.KeyName = KeyName;
            model.LinkURL = LinkURL;
            model.IsCMS = IsCMS;
            model.IsShop = IsShop;
            model.IsSNS = IsSNS;
            model.IsComment = IsComment;
            model.CreatedDate = DateTime.Now;
            model.IsActive = IsActive;

           
            bll.Add(model);
            if (chkIsAdd.Checked)
            {
                MessageBox.ShowSuccessTip(this, "添加成功", "add.aspx");
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
            }
            else
            {
                MessageBox.ShowSuccessTip(this, "添加成功,正在跳转...", "list.aspx");
            }
        }

        private bool IsUrl(string s)
        {
            string pattern = @"^(http|https|ftp|rtsp|mms):(\/\/|\\\\)[A-Za-z0-9%\-_@]+\.[A-Za-z0-9%\-_@]+[A-Za-z0-9\.\/=\?%\-&_~`@:\+!;]*$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}