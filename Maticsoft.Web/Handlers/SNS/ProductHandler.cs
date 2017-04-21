using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Common;
using Maticsoft.Json;
using Maticsoft.TaoBao;
using Maticsoft.TaoBao.Request;
using Maticsoft.TaoBao.Response;

namespace Maticsoft.Web.Handlers.SNS
{
    public class ProductHandler : HandlerBase
    {

        //定义一个全局变量(方便构造树形节点使用)
        Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
        #region IHttpHandler 成员

        public override bool IsReusable
        {
            get { return false; }
        }

        public override void ProcessRequest(HttpContext context)
        {
            //安全起见, 所有产品相关Ajax请求为POST模式
            string action = context.Request.Form["Action"];

            context.Response.Clear();
            context.Response.ContentType = "application/json";

            try
            {
                switch (action)
                {
                    #region 社区商品

                    case "EditRecomend":
                        EditRecomend(context);
                        break;

                    case "EditStatus":
                        EditStatus(context);
                        break;


                    #endregion 社区商品分类

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                JsonObject json = new JsonObject();
                json.Put(KEY_STATUS, STATUS_ERROR);
                json.Put(KEY_DATA, ex);
                context.Response.Write(json.ToString());
            }
        }

        #endregion IHttpHandler 成员
        #region 实现方法


        private void EditRecomend(HttpContext context)
        {
            int ProductId = Globals.SafeInt(context.Request.Params["ProductId"], 0);
            int Recomend = Globals.SafeInt(context.Request.Params["Recomend"], 0);
            JsonObject json = new JsonObject();
            if (productBll.UpdateRecomend(ProductId, Recomend))
            {
                json.Accumulate("STATUS", "OK");
            }
            else
            {
                json.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(json.ToString());
        }

        private void EditStatus(HttpContext context)
        {
            int ProductId = Globals.SafeInt(context.Request.Params["ProductId"], 0);
            int Recomend = Globals.SafeInt(context.Request.Params["Status"], 0);
            JsonObject json = new JsonObject();
            if (productBll.UpdateStatus(ProductId, Recomend))
            {
                json.Accumulate("STATUS", "OK");
            }
            else
            {
                json.Accumulate("STATUS", "NODATA");
            }
            context.Response.Write(json.ToString());
        }

        ///// <summary>
        ///// 对商品的类别进行全部更新或初始化
        ///// </summary>
        //public void ResetCategory()
        //{
        //    CategoryLoop(0L, "", 0);
        //}

        /////<summary>
        /////获取淘宝的全部分类，采用递归调用的方式,(初始化或更新某一个类别),管理员操作
        ////</summary>
        ////<param name="CategoryId">类别的名称 如果是初始化类别，则是0L</param>
        ////<param name="Path">路径，如果是初始化则为""</param>
        ////<param name="Depth">深度，如果是初始化则为0</param>
        //public void CategoryLoop(long CategoryId, string Path, int Depth)
        //{
        //    string TaoBaoAppkey =BLL.SysManage.ConfigSystem.GetValue("OpenAPI_TaoBaoAppkey");
        //    string TaobaoAppsecret =BLL.SysManage.ConfigSystem.GetValue("OpenAPI_TaobaoAppsecret");
        //    string TaobaoApiUrl =BLL.SysManage.ConfigSystem.GetValue("OpenAPI_TaobaoApiUrl");
        //    Maticsoft.Model.Tao.Category CateModel = new Model.Tao.Category();
        //    ITopClient client = new DefaultTopClient(TaobaoApiUrl, TaoBaoAppkey, TaobaoAppsecret);
        //    ItemcatsGetRequest req = new ItemcatsGetRequest();
        //    req.Fields = "cid,parent_cid,name,is_parent";
        //    req.ParentCid = CategoryId;
        //    ItemcatsGetResponse response = client.Execute(req);
        //    if (response.ItemCats.Count > 0)
        //    {
        //        foreach (var item in response.ItemCats)
        //        {
        //            CateModel.CategoryId = Common.Globals.SafeInt(item.Cid.ToString(), 0);

        //            // 存在则删除
        //            if (Exists(item.Name))
        //            {
        //                Delete(CateModel.CategoryId);
        //            }
        //            else
        //            {
        //                CateModel.ParentID = Common.Globals.SafeInt(item.ParentCid.ToString(), 0);
        //                if (string.IsNullOrEmpty(Path))
        //                {
        //                    CateModel.Path = item.Cid.ToString();
        //                }
        //                else
        //                {
        //                    CateModel.Path = Path + "|" + item.Cid.ToString();
        //                }
        //                CateModel.Depth = Depth + 1;
        //                CateModel.CreatedDate = DateTime.Now;
        //                CateModel.CreatedUserID = 1;
        //                CateModel.Description = "暂无描述";
        //                CateModel.HasChildren = item.IsParent;
        //                CateModel.IsMenu = false;
        //                CateModel.MenuIsShow = false;
        //                CateModel.MenuSequence = 0;
        //                CateModel.Name = item.Name;
        //                CateModel.Status = 1;
        //                CateModel.Type = 0;
        //                Add(CateModel);

        //                ///测试阶段，淘宝的限制是没分钟400次访问
        //                Thread primaryThread = Thread.CurrentThread;
        //                Thread.Sleep(500);

        //                //下面是递归调用和相应的出口（没有子集的情况下，直接返回）
        //                if (item.IsParent)
        //                {
        //                    CategoryLoop(item.Cid, CateModel.Path, CateModel.Depth);
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion
    }
}
