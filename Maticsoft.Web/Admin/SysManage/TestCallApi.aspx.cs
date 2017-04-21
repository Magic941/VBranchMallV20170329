using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.BLL;

using ServiceStack.RedisCache.Products;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class TestCallApi : System.Web.UI.Page
    {
        ServiceStack.RedisCache.Products.ProductInfo ProductCache = new ProductInfo();
        ServiceStack.RedisCache.Products.ProductImage productImageCache = new ProductImage();
        ServiceStack.RedisCache.Products.ProductCategories productCatCache = new ProductCategories();
        ServiceStack.RedisCache.Products.GroupBuy groupBuyCache = new GroupBuy();
        ServiceStack.RedisCache.Products.CategoryInfo categoryCache = new CategoryInfo();

        CallApi callapiBll = new CallApi();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string msg = "";
            callapiBll.AutoActive(ref msg,"13355301363", "18916725566", "abcde12332", "abcde12332");
            Label1.Text = msg;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
            int sumPage = 0;
            GridView1.DataSource = callapiBll.getMyMembers("13355301363", ref sumPage, Convert.ToInt32(TextBox1.Text),20);
            GridView1.DataBind();
            TextBox2.Text = sumPage.ToString();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            TextBox2.Text = "";
            int sumPage = 0;
            GridView1.DataSource = callapiBll.getMyInCome("13355301363", ref sumPage, Convert.ToInt32(TextBox1.Text), 20);
            GridView1.DataBind();
            TextBox2.Text = sumPage.ToString();
        }

        protected void Button5_Click1(object sender, EventArgs e)
        {
            int sumPage = 0;
            GridView1.DataSource = callapiBll.getMyMembers("13355301363", ref sumPage);
            GridView1.DataBind();
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int sumPage = 0;
            GridView1.DataSource = callapiBll.getMyInCome("13355301363", ref sumPage);
            GridView1.DataBind();
        }
    }
}