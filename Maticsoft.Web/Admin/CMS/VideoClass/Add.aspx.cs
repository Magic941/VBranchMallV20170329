/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:47:17  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;
namespace Maticsoft.Web.CMS.VideoClass
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 263; } }  
        Maticsoft.BLL.CMS.VideoClass bll=new Maticsoft.BLL.CMS.VideoClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                //BindData();
                string id = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(id) && PageValidate.IsNumber(id))
                {
                    //this.dropParentID.Enabled = false;
                    //this.dropParentID.SelectedValue = id;
                    //this.VideoClassDropList1.Enabled = false;
                    this.VideoClassDropList1.SelectedValue = id;
                }
            }
        }

        //protected void BindData()
        //{
        //    this.dropParentID.DataBind();
        //}

        protected void btnSave_Click(object sender, EventArgs e)
		{
            string videoclassname = this.txtVideoClassName.Text.Trim();

			string strErr="";

			if(this.txtVideoClassName.Text.Trim().Length==0)
			{
                strErr += Resources.CMSVideo.ErrorClassNameNull+"\\n";	
			}
			if(strErr!="")
			{
				MessageBox.Show(this,strErr);
				return;
			}

			Maticsoft.Model.CMS.VideoClass model=new Maticsoft.Model.CMS.VideoClass();

            model.VideoClassName = videoclassname;
            //model.ParentID = Globals.SafeInt(this.dropParentID.SelectedValue, 0);
            model.ParentID = Globals.SafeInt(this.VideoClassDropList1.SelectedValue, 0);

            if (bll.AddEx(model) > 0)
            {
                if (chkAddContinue.Checked)
                {
                    //BindData();
                    this.txtVideoClassName.Text = "";
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                }
                else
                {
                    MessageBox.ResponseScript(this, "parent.location.href='List.aspx'");
                    //MessageBox.ShowAndRedirects(this, Resources.Site.TooltipSaveOK, "list.aspx");
                }
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
                
            }
		}
    }
}
