/**
* Add.cs
*
* 功 能： N/A
* 类 名： Add
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01							N/A    初版
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

namespace Maticsoft.Web.Admin.SNS.TagType
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 597; } } //SNS_商品标签分类管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        protected void btnSave_Click(object sender, EventArgs e)
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

			Maticsoft.Model.SNS.TagType model=new Maticsoft.Model.SNS.TagType();
			model.TypeName=TypeName;
			model.Remark=Remark;
            model.Status = Globals.SafeInt(this.radlStatus.SelectedValue, 0);
            model.Cid = Globals.SafeInt(this.dropCid.SelectedValue, 0);

			Maticsoft.BLL.SNS.TagType bll=new Maticsoft.BLL.SNS.TagType();
            if (bll.Add(model) > 0)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加标签类型成功", this);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "TagTypelist.aspx");
            }
            else
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "添加标签类型失败", this);
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }
		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("TagTypelist.aspx");
        }
    }
}
