/**
* SelectCategory.cs
*
* 功 能： 选择商品分类
* 类 名： SelectCategory
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/12 11:12:07   Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class SelectCategory : PageBaseAdmin
    {
        private BLL.Shop.Products.CategoryInfo manage = new BLL.Shop.Products.CategoryInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) &&
                (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
        }

        #region AjaxCallback
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "GetInfo":
                    writeText = GetCategoryInfo();
                    break;
                case "GetList":
                    writeText = GetCategoriesList();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string GetCategoryInfo()
        {
            JsonObject json = new JsonObject();
            int categoryId = Globals.SafeInt(this.Request.Form["CategoryId"], -1);
            if (categoryId < 1)
            {
                json.Put("ERROR", "NOCATEGORYID");
                return json.ToString();
            }
            CategoryInfo categoryInfo = manage.GetModel(categoryId);
            if (categoryInfo == null)
            {
                json.Put("STATUS", "NODATA");
                return json.ToString();
            }
            json.Put("STATUS", "OK");
            json.Put("DATA", categoryInfo);
            return json.ToString();
        }

        private string GetCategoriesList()
        {
            JsonObject json = new JsonObject();
            int parentCategoryId = Globals.SafeInt(this.Request.Form["ParentCategoryId"], -1);
            if (parentCategoryId < 0)
            {
                json.Put("ERROR", "NOPARENTCATEGORYID");
                return json.ToString();
            }
            List<CategoryInfo> list = manage.GetCategorysByParentId(parentCategoryId);
            if (list == null || list.Count < 1)
            {
                json.Put("STATUS", "NODATA");
                return json.ToString();
            }
            list.Sort((x, y) => x.DisplaySequence.CompareTo(y.DisplaySequence));
            json.Put("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "CategoryId", "HasChildren", "CategoryName","Path" },
                    new object[]
                        {
                            info.CategoryId.ToString(CultureInfo.InvariantCulture),
                            info.HasChildren, info.Name,info.Path
                        }
                    )));
            json.Put("DATA", data);
            return json.ToString();
        }
        #endregion

    }
}