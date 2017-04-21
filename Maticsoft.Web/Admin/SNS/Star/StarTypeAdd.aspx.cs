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
namespace Maticsoft.Web.Admin.SNS.StarType
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 614; } } //SNS_达人类型管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
                       
        }

        		protected void btnSave_Click(object sender, EventArgs e)
		{
			
			string strErr="";
			if(this.txtTypeName.Text.Trim().Length==0)
			{
				strErr+="达人类型（如新晋达人不能为空！\\n";	
			}

			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}
			string TypeName=this.txtTypeName.Text;
			string CheckRule=this.txtCheckRule.Text;
			string Remark=this.txtRemark.Text;
            int Status = Common.Globals.SafeInt(this.radlStatus.SelectedValue, 0);

			Maticsoft.Model.SNS.StarType model=new Maticsoft.Model.SNS.StarType();
			model.TypeName=TypeName;
			model.CheckRule=CheckRule;
			model.Remark=Remark;
			model.Status=Status;

			Maticsoft.BLL.SNS.StarType bll=new Maticsoft.BLL.SNS.StarType();
            int ResultID = 0;
            if ((ResultID=bll.Add(model))>0)
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "增加达人类型（ID=" + ResultID + "）成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "StarTypeList.aspx");
            }

		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("StarTypeList.aspx");
        }
    }
}
