
namespace Maticsoft.DALFactory
{
    public sealed class DAShopProducts : DataAccessBase
    {
        /// <summary>
        /// 创建AccessoriesValues数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IAccessoriesValue CreateAccessoriesValue()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.AccessoriesValue";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IAccessoriesValue)objType;
        }
        
        /// <summary>
        /// 创建Attributes数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IAttributeInfo CreateAttributeInfo()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.AttributeInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IAttributeInfo)objType;
        }

        /// <summary>
        /// 创建AttributeValues数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IAttributeValue CreateAttributeValue()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.AttributeValue";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IAttributeValue)objType;
        }

        /// <summary>
        /// 创建Brands数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IBrandInfo CreateBrandInfo()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.BrandInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IBrandInfo)objType;
        }

        /// <summary>
        /// 创建Categories数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.ICategoryInfo CreateCategoryInfo()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.CategoryInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.ICategoryInfo)objType;
        }

        /// <summary>
        /// 创建Distributors数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IDistributor CreateDistributor()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.Distributor";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IDistributor)objType;
        }

        /// <summary>
        /// 创建LineDistributors数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.ILineDistributor CreateLineDistributor()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.LineDistributor";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.ILineDistributor)objType;
        }

        /// <summary>
        /// 创建ProductAccessories数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductAccessorie CreateProductAccessorie()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductAccessorie";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductAccessorie)objType;
        }

        /// <summary>
        /// 创建ProductAttributes数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductAttribute CreateProductAttribute()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductAttribute";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductAttribute)objType;
        }

        /// <summary>
        /// 创建ProductImages数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductImage CreateProductImage()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductImage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductImage)objType;
        }

        /// <summary>
        /// 创建ProductLines数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductLine CreateProductLine()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductLine";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductLine)objType;
        }

        /// <summary>
        /// 创建Products数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductInfo CreateProductInfo()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductInfo)objType;
        }

        /// <summary>
        /// 创建ProductStationModes数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductStationMode CreateProductStationMode()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductStationMode";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductStationMode)objType;
        }

        /// <summary>
        /// 创建ProductTypeBrands数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductTypeBrand CreateProductTypeBrand()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductTypeBrand";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductTypeBrand)objType;
        }

        /// <summary>
        /// 创建ProductTypes数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductType CreateProductType()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductType)objType;
        }

        /// <summary>
        /// 创建RelatedProducts数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IRelatedProduct CreateRelatedProduct()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.RelatedProduct";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IRelatedProduct)objType;
        }

        /// <summary>
        /// 创建SKUItems数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.ISKUItem CreateSKUItem()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.SKUItem";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.ISKUItem)objType;
        }

        /// <summary>
        /// 创建SKUMemberPrice数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.ISKUMemberPrice CreateSKUMemberPrice()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.SKUMemberPrice";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.ISKUMemberPrice)objType;
        }

        /// <summary>
        /// 创建SKUs数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.ISKUInfo CreateSKUInfo()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.SKUInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.ISKUInfo)objType;
        }

        /// <summary>
        /// 创建HotKeyword数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IHotKeyword CreateHotKeyword()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.HotKeyword";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IHotKeyword)objType;
        }

        /// <summary>
        /// 创建ProductService数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductService CreateProductService()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductService";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductService)objType;
        }

        /// <summary>
        /// 创建ProductConsultationsType数据层接口。商品咨询类别表
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductConsultsType CreateProductConsultsType()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductConsultsType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductConsultsType)objType;
        }

        /// <summary>
        /// 创建ProductConsultations数据层接口。商品咨询类别表
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductConsults CreateProductConsults()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductConsults";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductConsults)objType;
        }
        /// <summary>
        /// 创建ProductReviews数据层接口。商品评论表
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductReviews CreateProductReviews()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductReviews";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductReviews)objType;
        }

        /// <summary>
        /// 创建ScoreDetails数据层接口。评分记录表
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IScoreDetails CreateScoreDetails()
        {
            string ClassNamespace = AssemblyPath + ".Admin.Shop.Products.ScoreDetails";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IScoreDetails)objType;
        }
        /// <summary>
        /// 创建ScoreDetails数据层接口。评分记录表
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductQA CreateProductQA()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductQA";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductQA)objType;
        }

        /// <summary>
        /// 创建TagCategories数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Tags.ITagCategories CreateTagCategories()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Tags.TagCategories";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Tags.ITagCategories)objType;
        }

        /// <summary>
        /// 创建Tags数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Tags.ITags CreateTags()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Tags.Tags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Tags.ITags)objType;
        }

        /// <summary>
        /// 创建ProductCategories数据层接口。产品类别关联表
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IProductCategories CreateProductCategories()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ProductCategories";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IProductCategories)objType;
        }

        /// <summary>
        /// 创建ShoppingCarts数据层接口。产品类别关联表
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IShoppingCarts CreateShoppingCarts()
        {
            string ClassNamespace = AssemblyPath + ".Shop.Products.ShoppingCarts";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IShoppingCarts)objType;
        }

        /// <summary>
        /// 创建GoodsType数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.Products.IGoodsType CreateGoodsType()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Products.GoodsType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.Products.IGoodsType)objType;
        }



    }
}