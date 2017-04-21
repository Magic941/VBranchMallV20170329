using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

using System.Web.Routing;
using ProductLuceneTool.App_Start;
using Maticsoft.BLL.Products.Lucene;
using HL.ProductLuceneTool;

namespace ProductLuceneTool
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            ProductIndexManager.productIndex.StartNewThread();

            GlobalConfiguration.Configuration.Filters.Clear();
            GlobalConfiguration.Configuration.Filters.Add(new ApiFilter());
            GlobalConfiguration.Configuration.Filters.Add(new ModelValidationFilterAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new MyExceptionFilter());

           
        }
    }
}