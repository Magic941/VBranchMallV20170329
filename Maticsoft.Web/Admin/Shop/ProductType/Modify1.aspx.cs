/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
*
* Ver        变更日期                           负责人          变更内容
* ───────────────────────────────────
* V0.01   2012年6月13日 20:46:27    Rock            创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace Maticsoft.Web.Admin.Shop.ProductType
{
    public partial class Modify1 : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 494; } } //Shop_商品类型管理_编辑页
        Maticsoft.BLL.Shop.Products.ProductType bll = new Maticsoft.BLL.Shop.Products.ProductType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ProductTypeId == 0)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                    return;
                }
            }
        }


        private int ProductTypeId
        {
            get
            {
                int producrTypeId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["tid"]))
                {
                    producrTypeId = Maticsoft.Common.Globals.SafeInt(Request.Params["tid"], 0);
                }
                return producrTypeId;
            }
        }
    }
}
