/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List.cs
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
using System.Text;

namespace Maticsoft.Web.Admin.Shop.Brands
{
    public partial class AList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 397; } } //Shop_品牌管理_列表页
        protected new int Act_AddData = 401;    //Shop_品牌管理_添加数据
        protected new int Act_UpdateData = 402;    //Shop_品牌管理_编辑数据
        protected new int Act_DelData = 403;    //Shop_品牌管理_删除数据
        //int Act_ShowInvalid = -1; //查看失效数据行为

        public string strLiList = "";
        public string strHiddenScript = "";
        Maticsoft.BLL.Shop.Products.BrandInfo bll = new Maticsoft.BLL.Shop.Products.BrandInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!Page.IsPostBack)
            {
              
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    hidDelbtn.Value = "hidden";
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                   hidModifybtn.Value = "hidden";
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                CreateTabs();
               
            }
        }

        private void CreateTabs()
        {
            BLL.Shop.Products.ProductType produceType = new BLL.Shop.Products.ProductType();
            StringBuilder strLi = new StringBuilder();
            int num = 1;
            foreach (Model.Shop.Products.ProductType item in produceType.GetProductTypes())
            {
                strLi.AppendFormat("<li class=\"normal\" onclick=\"nTabs(this,{0},{1});\"><a href=\"#\">{2}</a></li>", num, item.TypeId, item.TypeName);
                num++;
            }
            strLiList = strLi.ToString();
           
        }
    }
}
