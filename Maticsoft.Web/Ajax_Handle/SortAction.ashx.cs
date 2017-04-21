using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maticsoft.Web.Ajax_Handle
{
    /// <summary>
    /// SortAction 的摘要说明
    /// </summary>
    public class SortAction : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string result = string.Empty;
            Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
            HttpRequest Request = context.Request;

            if (string.IsNullOrWhiteSpace(Request.Params["value"])) return;
            string value = Request.Params["value"];

            if (string.IsNullOrWhiteSpace(Request.Params["sid"])) return;
            string sid = Request.Params["sid"];

            try
            {
                if (string.IsNullOrWhiteSpace(sid.Trim()))
                    result = "false";
                Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModel(Convert.ToInt32(sid.Trim()));
                model.Sequence = Convert.ToInt32(string.IsNullOrWhiteSpace(value.Trim()) ? "0" : value.Trim());
                bool bl_result = bll.Update(model);
                result = bl_result.ToString();
            }
            catch
            {
                result = "false";
            }

            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
            context.Response.Write(result);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}