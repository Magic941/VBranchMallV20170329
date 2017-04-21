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
    /// 品牌类型多选CheckBox 基于CheckBoxList
    /// </summary>
    public class BrandsCheckBoxList : CheckBoxList
    {
        private int repeatColumns = 7;
        private System.Web.UI.WebControls.RepeatDirection repeatDirection;

        public override void DataBind()
        {
            this.Items.Clear();
            BLL.Shop.Products.BrandInfo bll = new BLL.Shop.Products.BrandInfo();
            foreach (Model.Shop.Products.BrandInfo model in bll.GetBrands())
            {
                base.Items.Add(new ListItem(model.BrandName, model.BrandId.ToString()));
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
