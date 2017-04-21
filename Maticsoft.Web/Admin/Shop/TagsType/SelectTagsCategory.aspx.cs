using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Json;
using System.Globalization;
using Maticsoft.Common;
using Maticsoft.Model.Shop.Tags;

namespace Maticsoft.Web.Admin.Shop.TagsType
{
    public partial class SelectTagsCategory : System.Web.UI.Page
    {
        private BLL.Shop.Tags.TagCategories manage = new BLL.Shop.Tags.TagCategories();

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
            TagCategories categoryInfo = manage.GetModel(categoryId);
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
            List<TagCategories> list = manage.GetCategorysByParentId(parentCategoryId);
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
                    new string[] { "CategoryId", "HasChildren", "CategoryName" },
                    new object[]
                        {
                            info.ID.ToString(CultureInfo.InvariantCulture),
                            info.HasChildren, info.CategoryName
                        }
                    )));
            json.Put("DATA", data);
            return json.ToString();
        }
        #endregion
    }
}
