/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。 
//
// 文件名：ProductTypesCheckBoxList.cs
// 文件功能描述：  Rock   商品类型列表
// 
// 创建标识：
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/

using System.Web.UI.WebControls;

namespace Maticsoft.Web.Controls
{
    /// <summary>
    /// 商品类型列表 基于CheckBoxList
    /// </summary>
    public class ProductTypesCheckBoxList : CheckBoxList
    {
        private int repeatColumns = 7;
        private System.Web.UI.WebControls.RepeatDirection repeatDirection;

        public override void DataBind()
        {
            this.Items.Clear();
            BLL.Shop.Products.ProductType productTypes = new BLL.Shop.Products.ProductType();
            foreach (Model.Shop.Products.ProductType model in productTypes.GetProductTypes())
            {
                base.Items.Add(new ListItem(model.TypeName, model.TypeId.ToString()));
            }
        }

        public override int RepeatColumns
        {
            get
            {
                return this.repeatColumns;
            }
            set
            {
                this.repeatColumns = value;
            }
        }

        public override System.Web.UI.WebControls.RepeatDirection RepeatDirection
        {
            get
            {
                return this.repeatDirection;
            }
            set
            {
                this.repeatDirection = value;
            }
        }
    }
}
