/**
* Show.cs
*
* 功 能： N/A
* 类 名： Show
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01						   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.UI;
namespace Maticsoft.Web.Admin.SNS.Categories
{
    public partial class Show : PageBaseAdmin
    { //区分是否为商品分类还是图片分类
        public int Type
        {
            get
            {
                int type = 0;
                if (Session["CategoryType"] != null)
                {
                    type = Common.Globals.SafeInt(Session["CategoryType"].ToString(), 0);
                }
                return type;
            }
        }
        protected override int Act_PageLoad //SNS_图片分类管理_详细页
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return 566; //SNS_商品分享分类管理_详细页
                    case 1:
                        return 563;//SNS_图片分享分类管理_详细页
                    default:
                        return 566;
                }
            }
        }
        		public string strid=""; 
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
				{
					strid = Request.Params["id"];
					int CategoryId=(Convert.ToInt32(strid));
					ShowInfo(CategoryId);
				}
			}
		}
		
	private void ShowInfo(int CategoryId)
	{
		Maticsoft.BLL.SNS.Categories bll=new Maticsoft.BLL.SNS.Categories();
		Maticsoft.Model.SNS.Categories model=bll.GetModel(CategoryId);
		this.lblCategoryId.Text=model.CategoryId.ToString();
		this.lblName.Text=model.Name;
		this.lblDescription.Text=model.Description;
		this.lblParentID.Text=model.ParentID.ToString();
		this.lblPath.Text=model.Path;
		this.lblDepth.Text=model.Depth.ToString();
		this.lblSequence.Text=model.Sequence.ToString();
		this.lblHasChildren.Text=model.HasChildren?"是":"否";
		this.lblIsMenu.Text=model.IsMenu?"是":"否";
		this.lblType.Text=model.Type.ToString();
		this.lblMenuIsShow.Text=model.MenuIsShow?"是":"否";
		this.lblMenuSequence.Text=model.MenuSequence.ToString();
		this.lblCreatedUserID.Text=model.CreatedUserID.ToString();
		this.lblCreatedDate.Text=model.CreatedDate.ToString();
		this.lblStatus.Text=model.Status.ToString();

	}



        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }

}
