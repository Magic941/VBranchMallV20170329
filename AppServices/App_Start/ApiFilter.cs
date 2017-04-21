using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

using System.IO;
using System.Web.Http.Controllers;

using System.Web.Http.ModelBinding;
using System.Net;



using System.Net.Http;


using System.Text;
using Maticsoft.BLL.SysManage;
using Maticsoft.Model.Shop.Products.Lucene;

namespace AppServices
{
    public class ApiFilter : ActionFilterAttribute
    {

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            try
            {
                MemoryStream mystream = new MemoryStream();
                actionExecutedContext.ActionContext.Response.Content.CopyToAsync(mystream);
                byte[] a = mystream.ToArray();
                string s = System.Text.Encoding.UTF8.GetString(a);

                ErrorLogTxt.GetInstance("商品搜索进入[" + actionExecutedContext.ActionContext.ActionDescriptor.ActionName + "]").Write("执行后：" + s);

                base.OnActionExecuted(actionExecutedContext);
            }
            catch (Exception e) {
                ErrorLogTxt.GetInstance("商品搜索进入[" + actionExecutedContext.ActionContext.ActionDescriptor.ActionName + "]").Write("执行后：" + e.Message);
            }
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var searchParams = actionContext.ActionDescriptor.GetParameters();
            if (searchParams != null && searchParams.Count() > 0)
            {

                if (actionContext.ActionDescriptor.ActionName == "SearchByKeyWord")
                {
                    var paramsvalue = searchParams.FirstOrDefault().DefaultValue;
                    //记录到数据库中，最后分析大家用的关键词是什么
                    ErrorLogTxt.GetInstance("商品搜索日志").Write("搜索关键字为：" + (paramsvalue !=null?paramsvalue.ToString():""));
                }
            }         
            ErrorLogTxt.GetInstance("商品搜索进入[" + actionContext.ActionDescriptor.ActionName + "]").Write("进入前：" + actionContext.Request.RequestUri.AbsoluteUri.ToString());
            base.OnActionExecuting(actionContext);
        }


    }

    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(System.Web.Http.Filters.HttpActionExecutedContext actionExecutedContext)
        {
            ErrorLogTxt.GetInstance("商品搜索进入[" + actionExecutedContext.ActionContext.ActionDescriptor.ActionName + "]").Write("异常发生：" + actionExecutedContext.Exception.Message);

            base.OnException(actionExecutedContext);

            ProductIndexAPIBaseModel result = new ProductIndexAPIBaseModel();
            result.ErrCode = -3;
            result.ErrMsg = actionExecutedContext.Exception.Message;

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.Accepted, result);
        }

    }

    /// <summary>
    /// 模型较验，首先参数要符合要求
    /// </summary>
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var errors = new Dictionary<string, List<string>>();
            if (actionContext.ModelState.IsValid == false)
            {
                ErrorLogTxt.GetInstance("商品搜索进入").Write(actionContext.ActionDescriptor.ActionName + "参数验证未通过");
                StringBuilder errormessage = new StringBuilder();

                foreach (KeyValuePair<string, ModelState> keyValue in actionContext.ModelState)
                {
                    if (keyValue.Value.Errors.Count() > 0)
                    {
                        errors[keyValue.Key] = keyValue.Value.Errors.Select(e => e.ErrorMessage).ToList();
                        keyValue.Value.Errors.ToList().ForEach(a => errormessage.Append(a.ErrorMessage).Append(";"));
                    }
                }
                //标准的返回错误对象
                ProductIndexAPIBaseModel result = new ProductIndexAPIBaseModel();
                result.ErrCode = -1;
                result.ErrMsg = errormessage.ToString();

                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Accepted, result);
            }
            else
                ErrorLogTxt.GetInstance("商品搜索进入").Write(actionContext.ActionDescriptor.ActionName + "参数验证通过");
        }
    }
}