using System.Collections.Generic;

namespace Maticsoft.ViewModel.Shop
{
    public class ProductSKUModel
    {
        public List<Model.Shop.Products.SKUInfo> ListSKUInfos = new List<Model.Shop.Products.SKUInfo>();

        public List<Model.Shop.Products.SKUItem> ListSKUItems = new List<Model.Shop.Products.SKUItem>();

        public SortedListAttribute ListAttrSKUItems = new SortedListAttribute();
    }


    public class SortedListAttribute :
        SortedList<Model.Shop.Products.AttributeInfo, SortedSet<Model.Shop.Products.SKUItem>>
    {
        public SortedListAttribute()
            : base(Common.ComparerFactroy<Model.Shop.Products.AttributeInfo>.Create(x => x.DisplaySequence))
        {
        }

        /// <summary>
        /// 添加对象, 如Key不存在会自动创建
        /// </summary>
        /// <param name="key">AttributeInfo</param>
        /// <param name="value">SKUItem</param>
        public void Add(Model.Shop.Products.AttributeInfo key, Model.Shop.Products.SKUItem value)
        {
            if (ContainsKey(key.AttributeId))
            {
                this[key.AttributeId].Add(value);
            }
            else
            {
                Add(key, new SortedSet<Model.Shop.Products.SKUItem>(
                    Common.ComparerFactroy<Model.Shop.Products.SKUItem>.Create(
                        x => x.AB_DisplaySequence, x => x.AV_DisplaySequence)
                    ) { value });
            }
        }

        public bool ContainsKey(long attributeId)
        {
            return System.Linq.Enumerable.Any(Keys, attributeInfo => attributeInfo.AttributeId == attributeId);
        }

        public SortedSet<Model.Shop.Products.SKUItem> this[long attributeId]
        {
            get
            {
                foreach (KeyValuePair<Model.Shop.Products.AttributeInfo,
                    SortedSet<Model.Shop.Products.SKUItem>> keyValuePair in this)
                {
                    if (keyValuePair.Key.AttributeId == attributeId)
                    {
                        if (keyValuePair.Value == null)
                            return new SortedSet<Model.Shop.Products.SKUItem>();
                        return keyValuePair.Value;
                    }
                }
                return null;
            }
            set
            {
                Model.Shop.Products.AttributeInfo model = null;
                foreach (KeyValuePair<Model.Shop.Products.AttributeInfo,
                    SortedSet<Model.Shop.Products.SKUItem>> keyValuePair in this)
                {
                    if (keyValuePair.Key.AttributeId == attributeId)
                    {
                        model = keyValuePair.Key;
                        break;
                    }
                }
                if (model != null)
                {
                    this[model] = value;
                }
            }
        }
    }
}
