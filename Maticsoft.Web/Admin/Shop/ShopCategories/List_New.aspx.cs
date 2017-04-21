/**
* List_New.cs
*
* 功 能： [N/A]
* 类 名： List_New
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/2/1 11:33:15  Rock    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    public partial class List_New :PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 524; } } //shop_商品分类管理_列表页
        protected new int Act_AddData = 525;    //shop_商品分类管理_添加数据
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
            {
                liAdd.Visible = false;
            }

        }
    }
}