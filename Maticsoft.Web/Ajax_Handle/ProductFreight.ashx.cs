using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

using System.Web.Script.Serialization;
using System.Web.Security;
using Newtonsoft.Json;

namespace Maticsoft.Web.Ajax_Handle
{
    /// <summary>
    /// ProductFreight 的摘要说明
    /// </summary>
    public class ProductFreight : IHttpHandler
    {
        BLL.Shop.Products.ProductInfo bll = new BLL.Shop.Products.ProductInfo();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            HttpRequest Request = context.Request;
            var str_result = "";
            if (Request.Params["Action"] != null && !string.IsNullOrWhiteSpace(Request.Params["Action"].ToString()))
            {
                string name = context.User.Identity.Name;
                string str_Action = Request.Params["Action"].ToString().Trim();
                switch (str_Action)
                {
                    //查询分页商品运费
                    case "GetFreightList":
                        if ( Request.Params["page"] != null && !string.IsNullOrWhiteSpace(Request.Params["page"].ToString()) && Request.Params["pagesize"] != null && !string.IsNullOrWhiteSpace(Request.Params["pagesize"].ToString()))
                        {
                            string where = string.Empty;
                            if (!string.IsNullOrWhiteSpace(Request.Params["Value1"]))
                            {
                                where += " and y.ProductCode='" + Request.Params["Value1"].ToString().Trim() + "'  ";
                            }
                            if (!string.IsNullOrWhiteSpace(Request.Params["Value2"]))
                            {
                                where += " and y.ProductName like '%" + Request.Params["Value2"].ToString().Trim() + "%'  ";
                            }
                            GetFreightList(ref str_result, where, (Convert.ToInt64(Request.Params["page"].ToString().Trim()) - 1) * Convert.ToInt64(Request.Params["pagesize"].ToString().Trim()) + 1, Convert.ToInt64(Request.Params["page"].ToString().Trim()) * Convert.ToInt64(Request.Params["pagesize"].ToString().Trim()));
                        }
                        break;
                    case "IsExist":
                        if (!string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()))
                        {
                            GetFreightList(ref str_result,Request.Params["Value1"].ToString());
                        }
                        break;
                    case "AddFreight":
                        if (!string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()) && Request.Params["Value2"] != null && Request.Params["Value3"] != null && Request.Params["Value4"] != null)
                        {
                            AddFreight(ref str_result, Request.Params["Value1"].ToString(), Request.Params["Value2"].ToString(), Convert.ToDecimal(Request.Params["Value3"].ToString().Trim()), Convert.ToInt32(Request.Params["Value4"].ToString().Trim()), name);
                        }
                        break;
                    case "DeleteFreight":
                        if (!string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()))
                        {
                            DeleteFreight(ref str_result, Request.Params["Value1"].ToString());
                        }
                        break;
                    case "ProductList":
                        if (Request.Params["page"] != null && !string.IsNullOrWhiteSpace(Request.Params["page"].ToString()) && Request.Params["pagesize"] != null && !string.IsNullOrWhiteSpace(Request.Params["pagesize"].ToString()))
                        {
                            string where = " and SaleStatus=1 ";
                            if(!string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()))
                            {
                               where += " and ProductCode='" + Request.Params["Value1"].ToString().Trim() + "'  ";
                            }
                            if(!string.IsNullOrWhiteSpace(Request.Params["Value2"].ToString()))
                            {
                                where += " and ProductName like '%" + Request.Params["Value2"].ToString().Trim() + "%'  ";
                            }
                            GetProductListByPage(ref str_result,where,(Convert.ToInt64(Request.Params["page"].ToString().Trim()) - 1) * Convert.ToInt64(Request.Params["pagesize"].ToString().Trim()) + 1, Convert.ToInt64(Request.Params["page"].ToString().Trim()) * Convert.ToInt64(Request.Params["pagesize"].ToString().Trim()));
                        }
                        break;
                    case "UpdateFreight":
                        if (!string.IsNullOrWhiteSpace(Request.Params["Value1"].ToString()) && Request.Params["Value2"] != null)
                        {
                            UpdateFreight(ref str_result, Request.Params["Value1"].ToString(), Convert.ToInt32(Request.Params["Value2"].ToString().Trim()), name);
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

        /// <summary>
        /// 获取分页JSON数据
        /// </summary>
        /// <param name="Result">分页JSON数据</param>
        /// <param name="Where">查询条件</param>
        /// <param name="StartIndex">开始行</param>
        /// <param name="EndIndex">结束行</param>
        private void GetFreightList(ref string Result,string Where,Int64 StartIndex,Int64 EndIndex)
        {
            DataTable dt_FreightList = new DataTable();
            dt_FreightList = bll.GetFreightListByPage(Where, StartIndex, EndIndex).Tables[0];
            int total = bll.GetFreightListByPage(Where, null, null).Tables[0].Rows.Count;
            Result = "{\"Rows\":" + JsonConvert.SerializeObject(dt_FreightList) + ",\"Total\":" + total + "}";
        }

        private void GetFreightList(ref string Result,string ProductCode)
        {
            DataTable dt_FreightList = new DataTable();
            dt_FreightList = bll.GetFreightList(ProductCode, "").Tables[0];
            if (dt_FreightList.Rows.Count > 0)
                Result = "1";
            else
                Result = "0";
        }

        private void AddFreight(ref string Result,string ProductCode,string SKU,decimal Freight,int ModeId,string Editor)
        {
            bool bl_result = bll.AddFreight(ProductCode, SKU, Freight, ModeId,Editor);
            if (bl_result)
                Result = "1";
            else
                Result = "0";
        }

        private void DeleteFreight(ref string Result, string ProductCode)
        {
            bool bl_result = bll.DeleteFreight(ProductCode, "");
            if (bl_result)
                Result = "1";
            else
                Result = "0";
        }

        private void GetProductListByPage(ref string Result,string Where,Int64? StartIndex,Int64? EndIndex)
        { 
            DataTable dt_FreightList = new DataTable();
            dt_FreightList = bll.GetProductListByPage(Where,StartIndex,EndIndex).Tables[0];
            int total = bll.GetProductListByPage(Where, null, null).Tables[0].Rows.Count;
            Result = "{\"Rows\":" + JsonConvert.SerializeObject(dt_FreightList) + ",\"Total\":" + total + "}";
        }

        private void UpdateFreight(ref string Result, string ProductCode, decimal Freight,string Editor)
        {
            bool bl_result = bll.UpdateFreight(ProductCode,"", Freight,0,Editor);
            if (bl_result)
                Result = "1";
            else
                Result = "0";
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