/**
* PhotoClass.cs
*
* 功 能： 图片分类下拉列表
* 类 名： PhotoClass
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/25 12:08:25  伍伟    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.BLL.Shop.Products;

namespace Maticsoft.Web.Controls
{
    /// <summary>
    /// 图片分类下拉列表 基于DropDownList
    /// </summary>
    public class GoodTypeDropDownList : DropDownList
    {
        public GoodTypeDropDownList()
        {
            NullToDisplay = false;
            ParentId = null;
        }

        public override void DataBind()
        {
            this.Items.Clear();
            if (this.NullToDisplay)
            {
                this.Items.Add(new ListItem("", "0"));
            }

            GoodsType goodBll = new GoodsType();
            DataSet dsParent = goodBll.GetList(ParentId.HasValue ? "PID=" + this.ParentId : "0");

            if (dsParent != null && dsParent.Tables[0].Rows.Count > 0)
            {
                Common.TreeBind.SetMultiLevelDropDownList(this, "PID", dsParent.Tables[0]);
            }
            base.DataBind();
        }

        public bool NullToDisplay { get; set; }

        public int? ParentId { get; set; }
    }
}
