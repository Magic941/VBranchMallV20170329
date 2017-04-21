/**
* EditInfo.cs
*
* 功 能： [N/A]
* 类 名： EditInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/27 13:17:11  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.ClassType
{
    public partial class EditInfo :PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 216; } }   //CMS_类型管理_编辑页
        Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ClassTypeID >= 0)
                {
                    this.modify.Visible = true;
                    ShowInfo();
                }
                else
                {
                    this.modify.Visible = false;
                }
            }
        }

        public int ClassTypeID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.ClassType model = bll.GetModelByCache(ClassTypeID);
            if (null != model)
            {
                this.lblClassTypeID.Text = model.ClassTypeID.ToString();
                HiddenField_ID.Value = model.ClassTypeID.ToString();
                this.txtClassTypeName.Text = model.ClassTypeName;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnCancle.Enabled = false;
            this.btnSave.Enabled = false;
            if (string.IsNullOrWhiteSpace(txtClassTypeName.Text.Trim()))
            {
                MessageBox.ShowFailTip(this,"请输入类型名称！");
                return;
            }
            Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
            Maticsoft.Model.CMS.ClassType model = null;
            if (!string.IsNullOrWhiteSpace(txtClassTypeName.Text.Trim()) && !string.IsNullOrWhiteSpace(this.HiddenField_ID.Value))
            {
                string str = this.HiddenField_ID.Value;
                model = new Maticsoft.Model.CMS.ClassType();
                model.ClassTypeID = int.Parse(str);
                model.ClassTypeName = txtClassTypeName.Text.Trim();
                if (bll.Update(model))
                {
                    MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                    MessageBox.ShowSuccessTip(this, "保存成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败！");
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtClassTypeName.Text.Trim()))
            {
                model = new Maticsoft.Model.CMS.ClassType();
                model.ClassTypeName = txtClassTypeName.Text.Trim();
                if (bll.Add(model))
                {
                    MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                    MessageBox.ShowSuccessTip(this, "保存成功！");
                }
                else
                {
                    MessageBox.ShowFailTip(this, "保存失败！");
                }
            }
        }
    }
}