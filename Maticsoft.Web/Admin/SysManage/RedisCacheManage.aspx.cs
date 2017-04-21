using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using ServiceStack.RedisCache.Products;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class RedisCacheManage : System.Web.UI.Page
    {
        ServiceStack.RedisCache.Products.ProductInfo ProductCache = new ProductInfo();
        ServiceStack.RedisCache.Products.ProductImage productImageCache = new ProductImage();
        ServiceStack.RedisCache.Products.ProductCategories productCatCache = new ProductCategories();
        ServiceStack.RedisCache.Products.GroupBuy groupBuyCache = new GroupBuy();
        ServiceStack.RedisCache.Products.CategoryInfo categoryCache = new CategoryInfo();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void DeleteAllCache_Click(object sender, EventArgs e)
        {
            ProductCache.DeleteAllProduct();
            productImageCache.DeleteAllProductImage();
            productCatCache.DeleteAllCategories();
            groupBuyCache.DeleteAllGroupBuy();
            categoryCache.DeleteAllCategory();
            ShowText.Text = "所有缓存清理成功";
        }

        protected void LoadAllCache_Click(object sender, EventArgs e)
        {
            ProductCache.SaveAllProduct();
            productImageCache.SaveAllProductImage();
            productCatCache.SaveAllCategories();
            groupBuyCache.SaveAllGroupBuy();
            categoryCache.SaveAllCategory();
            ShowText.Text = "所有缓存加载成功";
        }

        protected void DeleteAllProduct_Click(object sender, EventArgs e)
        {
            ProductCache.DeleteAllProduct();
            ShowText.Text = "所有商品缓存删除成功";
        }

        protected void LoadAllProduct_Click(object sender, EventArgs e)
        {
            ProductCache.SaveAllProduct();
            ShowText.Text = "所有缓存商品加载成功";
        }

        protected void DeleteAllGroupBuy_Click(object sender, EventArgs e)
        {
            groupBuyCache.DeleteAllGroupBuy();
            ShowText.Text = "所有缓存团购信息删除成功";
        }

        protected void LoadAllGroupBuy_Click(object sender, EventArgs e)
        {
            groupBuyCache.SaveAllGroupBuy();
            ShowText.Text = "所有缓存团购信息加载成功";
        }

        protected void DeleteAllCategory_Click(object sender, EventArgs e)
        {
            productCatCache.DeleteAllCategories();
            ShowText.Text = "所有缓存分类关联信息删除成功";
        }

        protected void LoadAllCategory_Click(object sender, EventArgs e)
        {
            productCatCache.SaveAllCategories();
            ShowText.Text = "所有缓存分类关联信息加载成功";
        }

        protected void DeleteAllProductImage_Click(object sender, EventArgs e)
        {
            productImageCache.DeleteAllProductImage();
            ShowText.Text = "所有缓存商品图片信息删除成功";
        }

        protected void LoadAllProductImage_Click(object sender, EventArgs e)
        {
            productImageCache.SaveAllProductImage();
            ShowText.Text = "所有缓存商品图片信息加载成功";
        }

        protected void DeleteAllProductCategories_Click(object sender, EventArgs e)
        {            
            categoryCache.DeleteAllCategory();
            ShowText.Text = "所有缓存商品分类删除成功";
        }

        protected void LoadAllProductCategories_Click(object sender, EventArgs e)
        {
            categoryCache.SaveAllCategory();
            ShowText.Text = "所有缓存商品分类加载成功";
        }
    }
}