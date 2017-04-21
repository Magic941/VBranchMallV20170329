using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class EntryFormController : MPageControllerBase
    {
        private readonly BLL.Ms.EntryForm bll = new BLL.Ms.EntryForm();
        //
        // GET: /MPage/EntryForm/

        public ActionResult Index(string viewName="Index")
        {
            return View(viewName);
        }
        [HttpPost]
        public ActionResult SubmitEntryForm(FormCollection fm)
        {
            #region  微信用户名
            string cookieValue = "entryform";
            if (Session["WeChat_UserName"] != null)
            {
                cookieValue  +="_"+ Session["WeChat_UserName"].ToString();
            }    
            #endregion

            if (Request.Cookies["entry"] != null)
            {
                if (cookieValue == Request.Cookies["entry"].Values["entry"])
               {
                   return Content("isnotnull");//ERROR  "您已经报过名，请不要重复报名！ 
               }  
            }
           
            string username = fm["UserName"];
            if (String.IsNullOrWhiteSpace(username))  
            {
                return Content("UserNameISNULL");
            }
            int  age =Common.Globals.SafeInt(fm["Age"],-1) ;
            string email = Common.InjectionFilter.SqlFilter(fm["Email"]);
            string telPhone =Common.InjectionFilter.SqlFilter(fm["TelPhone"]) ;
            string phone =Common.InjectionFilter.SqlFilter(fm["Phone"]) ;
            int  region  = Common.Globals.SafeInt(fm["Region"],-1);
            string qq  = Common.InjectionFilter.SqlFilter(fm["QQ"]);
            string houseaddress =Common.InjectionFilter.SqlFilter( fm["Houseaddress"]);
            string CompanyAddress = Common.InjectionFilter.SqlFilter(fm["CompanyAddress"]);
            string Sex =Common.InjectionFilter.SqlFilter(fm["Sex"]) ;
            string Description =Common.InjectionFilter.SqlFilter(fm["Description"]) ;
            string Remark = Common.InjectionFilter.SqlFilter(fm["Remark"]);
            Model.Ms.EntryForm model = new Model.Ms.EntryForm();
            if (age > 0)
            {
                model.Age = age;
            }
            if (region > 0)
            {
                model.RegionId = region;
            }
            model.UserName = username;
            model.Email = email;
            model.TelPhone = telPhone;
            model.Phone = phone;
            model.QQ = qq;
            model.HouseAddress = houseaddress;
            model.CompanyAddress = CompanyAddress;
            model.Sex = Sex;
            model.Description = Description;
            model.State = 0;
            model.Remark = Remark;
            if (bll.Add(model) > 0)
            {
                HttpCookie httpCookie = new HttpCookie("entry");
                httpCookie.Values.Add("entry", cookieValue);
                httpCookie.Expires = DateTime.Now.AddHours(240);
                Response.Cookies.Add(httpCookie);
                return Content("true");
            }
            return Content("false");
        }
    }
}
