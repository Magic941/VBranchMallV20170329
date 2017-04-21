/**
* Modify.cs
*
* 功 能： N/A
* 类 名： Modify
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
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 615; } } //SNS_达人类型管理_编辑页
        Maticsoft.BLL.SNS.StarType bll = new Maticsoft.BLL.SNS.StarType();
      protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
			
					ShowInfo(TypeID);
			}
		}
      protected int TypeID
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
	private void ShowInfo(int TypeID)
	{
	
		Maticsoft.Model.SNS.StarType model=bll.GetModel(TypeID);
		this.txtTypeName.Text=model.TypeName;
		this.txtCheckRule.Text=model.CheckRule;
		this.txtRemark.Text=model.Remark;
		this.radlStatus.SelectedValue=model.Status.ToString();

	}

		public void btnSave_Click(object sender, EventArgs e)
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
            Maticsoft.Model.SNS.StarType model = bll.GetModel(TypeID);
			string TypeName=this.txtTypeName.Text;
			string CheckRule=this.txtCheckRule.Text;
			string Remark=this.txtRemark.Text;
            int Status = Common.Globals.SafeInt(this.radlStatus.SelectedValue, 0);
			model.TypeName=TypeName;
			model.CheckRule=CheckRule;
			model.Remark=Remark;
			model.Status=Status;
            if (bll.Update(model))
            {
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改达人类型(id=" + model.TypeID + ")成功", this);
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "StarTypeList.aspx");
            
            }


            LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "修改达人类型(id=" + model.TypeID + ")失败", this);
		}


        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("StarTypeList.aspx");
        }
    }
}
