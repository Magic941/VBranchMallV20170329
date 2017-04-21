using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Newtonsoft.Json;

namespace Maticsoft.Web.Ajax_Handle
{
    /// <summary>
    /// ProductBrands 的摘要说明
    /// </summary>
    public class ProductBrands : IHttpHandler
    {

        Maticsoft.BLL.Shop.Products.BrandInfo bll = new BLL.Shop.Products.BrandInfo();
        Maticsoft.BLL.Shop.Products.ProductType bllType = new BLL.Shop.Products.ProductType();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            HttpRequest Request = context.Request;
            var str_result = "";
            if (Request.Params["Action"] != null && !string.IsNullOrWhiteSpace(Request.Params["Action"].ToString()))
            {
                string str_Action = Request.Params["Action"].ToString().Trim();
                switch (str_Action)
                {
                    case "GetProductTypeList":
                        {
                            string str_where = " 1=1 ";
                            if (Request.Params["page"] != null && !string.IsNullOrWhiteSpace(Request.Params["page"].ToString()) && Request.Params["pagesize"] != null && !string.IsNullOrWhiteSpace(Request.Params["pagesize"].ToString()))
                            {
                                if(Request.Params["Value1"] != null)
                                {
                                    str_where = " BrandName like '%" + Request.Params["Value1"] .ToString()+ "%' ";
                                }
                                GetProductTypeList(ref str_result, str_where, (Convert.ToInt32(Request.Params["page"].ToString().Trim()) - 1) * Convert.ToInt32(Request.Params["pagesize"].ToString().Trim()) + 1, Convert.ToInt32(Request.Params["page"].ToString().Trim()) * Convert.ToInt32(Request.Params["pagesize"].ToString().Trim()));
                            }
                        }
                        break;
                    case "AddProductType":
                        {
                            if (Request.Params["Value1"] != null && !string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()) && Request.Params["Value3"] != null && !string.IsNullOrWhiteSpace(Request.Params["Value3"].ToString()))
                            {
                                string str_Remark = "";
                                if (Request.Params["Value2"] != null)
                                {
                                    str_Remark = Request.Params["Value2"].ToString();
                                }
                                AddProductType(ref str_result, Request.Params["Value1"].ToString(), str_Remark, Request.Params["Value3"].ToString());
                            }
                        }
                        break;
                    case "GetBrandList":
                        {
                            if (Request.Params["Value1"] != null && !string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()))
                            {
                                GetBrandList(ref str_result, Convert.ToInt32(Request.Params["Value1"].ToString()));
                            }
                        }
                        break;
                    case "GetTypeInfo":
                        {
                            if (Request.Params["Value1"] != null && !string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()))
                            {
                                GetTypeInfo(ref str_result, Convert.ToInt32(Request.Params["Value1"].ToString()));
                            }
                        }
                        break;
                    case "UpdateProductType":
                        {
                            if (Request.Params["Value1"] != null && !string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()) && Request.Params["Value3"] != null && !string.IsNullOrWhiteSpace(Request.Params["Value3"].ToString()) && Request.Params["Value4"] != null && !string.IsNullOrWhiteSpace(Request.Params["Value4"].ToString()))
                            {
                                string str_Remark = "";
                                if (Request.Params["Value2"] != null)
                                {
                                    str_Remark = Request.Params["Value2"].ToString();
                                }
                                UpdateProductType(ref str_result, Request.Params["Value1"].ToString(), str_Remark, Request.Params["Value3"].ToString(), Convert.ToInt32(Request.Params["Value4"].ToString()));
                            }
                        }
                        break;
                    default:
                        str_result = "操作不正确！";
                        break;
                }
            }
            //返回结果
            context.Response.Write(str_result);
        }

        private void GetProductTypeList(ref string Str_result, string Where, int StartIndex, int EndIndex)
        {
            DataTable dt_List = new DataTable();
            dt_List = bll.GetListByPage(Where, "BrandId", StartIndex, EndIndex).Tables[0];
            int total = bll.GetRecordCount(Where);
            Str_result = "{\"Rows\":" + JsonConvert.SerializeObject(dt_List) + ",\"Total\":" + total + "}";
        }

        private void AddProductType(ref string Str_result,string TypeName,string Remark,string BrandList)
        {
            IList<int> list = new List<int>();
            foreach (var item in BrandList.Split(','))
            {
                list.Add(int.Parse(item));
            }
            Maticsoft.Model.Shop.Products.ProductType model = new Maticsoft.Model.Shop.Products.ProductType();
            model.TypeName = TypeName;
            model.Remark = Remark;
            model.BrandsTypes = list;
            int typeid = 0;
            if (bllType.ProductTypeManage(model, Model.Shop.Products.DataProviderAction.Create, out typeid))
            {
                Str_result = "Step2.aspx?tid=" + typeid;
            }
        }

        private void GetBrandList(ref string Str_result, int ProductTypeId)
        {
            Model.Shop.Products.BrandInfo model = new Model.Shop.Products.BrandInfo();
            model = bll.GetRelatedProduct(null, ProductTypeId);

            DataSet ds = bll.GetList(-1, "", "BrandId");

            string str_Json = "[";
            foreach(DataRow item in ds.Tables[0].Rows)
            {
                if (model.ProductTypeIdOrBrandsId.Contains(Convert.ToInt32(item["BrandId"])))
                {
                    str_Json += "{\"BrandId\":" + item["BrandId"] + ",\"BrandName\":\"" + item["BrandName"] + "\"},";
                }
            }
            str_Json = str_Json.Substring(0,str_Json.Length-1)+"]";
            Str_result = "{\"Rows\":" + str_Json + ",\"Total\":" + model.ProductTypeIdOrBrandsId.Count + "}";
        }

        private void GetTypeInfo(ref string Str_result,int ProductTypeId)
        {
            DataTable dt = bllType.GetList(" TypeId = "+ProductTypeId).Tables[0];
            if (dt.Rows.Count>0)
            {
                Str_result = "{\"TypeId\":" + dt.Rows[0]["TypeId"] + ",\"TypeName\":\"" + dt.Rows[0]["TypeName"] + "\",\"Remark\":\"" + dt.Rows[0]["Remark"] + "\"}";    
            }
        }

        private void UpdateProductType(ref string Str_result, string TypeName, string Remark, string BrandList,int ProductTypeId)
        {
            IList<int> list = new List<int>();
            foreach (var item in BrandList.Split(','))
            {
                list.Add(int.Parse(item));
            }
            Maticsoft.Model.Shop.Products.ProductType model = new Maticsoft.Model.Shop.Products.ProductType();
            model.TypeName = TypeName;
            model.Remark = Remark;
            model.BrandsTypes = list;
            model.TypeId = ProductTypeId;
            int typeid = 0;
            if (bllType.ProductTypeManage(model, Model.Shop.Products.DataProviderAction.Update, out typeid))
            {
                Str_result = "1";
            }
            else
            {
                Str_result = "0";
            }
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