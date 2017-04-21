using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.Components.Filters;
using Maticsoft.Web.Components.Setting.CMS;
using Webdiyer.WebControls.Mvc;
using Maticsoft.BLL.Shop.Card;
using Maticsoft.BLL;
using System.Text;
using Maticsoft.Model;
using Newtonsoft.Json;
using Maticsoft.Web.Models;
using Maticsoft.Services;


namespace Maticsoft.Web.Areas.CMS.Controllers
{
    /// <summary>
    /// 卡激活
    /// </summary>
    public class AssuranceMarketController : CMSControllerBase
    {

        /// <summary>
        /// 激活起始页页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {          
            return View();
        }
    }
}