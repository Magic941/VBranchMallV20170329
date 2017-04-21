/**
* Modify.cs
*
* 功 能： [N/A]
* 类 名： Modify.cs
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
namespace Maticsoft.Web.SNS.TagType
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 598; } } //SNS_商品标签分类管理_编辑页
        Maticsoft.BLL.SNS.TagType bll = new Maticsoft.BLL.SNS.TagType();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowInfo();
            }
        }

        protected int Id
        {
            get
            {
                int id = 0;
                string strId = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strId))
                {
                    id = Globals.SafeInt(strId, 0);
                }
                return id;
            }
        }

        private void ShowInfo()
        {
            Maticsoft.Model.SNS.TagType model = bll.GetModel(Id);
            if (null != model)
            {
                this.lblID.Text = model.ID.ToString();
                this.txtTypeName.Text = model.TypeName;
                this.txtRemark.Text = model.Remark;
                if (model.Cid.HasValue)
                {
                    this.dropCid.SelectedValue = model.Cid.ToString();
                }
                if (model.Status.HasValue)
                {
                    this.radlStatus.SelectedValue = model.Status.ToString();
                }
            }
        }

		public void btnSave_Click(object sender, EventArgs e)
		{
            string TypeName = this.txtTypeName.Text.Trim();
            if (TypeName.Length == 0)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能为空！");
                return;
            }
            if (TypeName.Length > 50)
            {
                MessageBox.ShowServerBusyTip(this, "类型名称不能大于50个字符！");
                return;
            }
            string Remark = this.txtRemark.Text.Trim();
            if (Remark.Length > 100)
            {
                MessageBox.ShowServerBusyTip(this, "备注不能大于100个字符！");
                return;
            }

            Maticsoft.Model.SNS.TagType model = bll.GetModel(Id);
             if (null != model)
             {
                 model.TypeName = TypeName;
                 model.Remark = Remark;
                 model.Cid = Globals.SafeInt(this.dropCid.SelectedValue, 0);
                 model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);

                 if (bll.Update(model))
                 {
                     MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "TagTypelist.aspx");
                 }
                 else
                 {
                     MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                 }
             }
		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TagTypelist.aspx");
        }
    }
}
