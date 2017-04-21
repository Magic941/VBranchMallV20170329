using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.WeChat.Scene
{
    public partial class DetailCount : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.SceneDetail detailBll = new Maticsoft.WeChat.BLL.Core.SceneDetail();
        private Maticsoft.WeChat.BLL.Core.Scene sceneBll = new Maticsoft.WeChat.BLL.Core.Scene();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!IsPostBack)
            {
                //加载渠道推广场景
                ddlScene.DataSource = sceneBll.GetAllList();
                ddlScene.DataTextField = "Name";
                ddlScene.DataValueField = "SceneId";
                ddlScene.DataBind();
                ddlScene.Items.Insert(0, new ListItem("全部", "0"));
            }
        }
        #region Ajax方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "GetCount":
                    writeText = GetCount();
                    break;
                default:
                    writeText = GetCount();
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string GetCount()
        {
            JsonObject json = new JsonObject();
            string txtFrom = Request.Form["txtFrom"];
            string txtTo = Request.Form["txtTo"];
            int SceneId = Common.Globals.SafeInt(Request.Form["SceneId"],0);
            JsonArray newsArry = new JsonArray();
            JsonObject itemObj = null;
            if (SceneId > 0)
            {
                if (String.IsNullOrWhiteSpace(txtFrom) || String.IsNullOrWhiteSpace(txtFrom))
                {
                    json.Accumulate("STATUS", "NoDate");
                    return json.ToString();
                }
                //查询每一天的数据
                var Fdate = Common.Globals.SafeDateTime(txtFrom, DateTime.Now);
                var Tdate = Common.Globals.SafeDateTime(txtTo, DateTime.Now);
                int days =Convert.ToInt32((Tdate - Fdate).TotalDays);
                for (int i = 0; i <= days; i++)
                {
                    itemObj = new JsonObject();
                    string name = Fdate.AddDays(i).ToString("yyyy-MM-dd");
                    int count = detailBll.GetDayCount(SceneId, Fdate.AddDays(i).ToString(), Fdate.AddDays(i + 1).ToString());
                    itemObj.Accumulate("name", name);
                    itemObj.Accumulate("y", count);
                    newsArry.Add(itemObj);
                }
                json.Accumulate("STATUS", "Success");
                json.Accumulate("Data", newsArry.ToString());
            }
            else
            {
                //获取推广渠道
                List<Maticsoft.WeChat.Model.Core.Scene> sceneList = sceneBll.GetModelList("");
                foreach (var item in sceneList)
                {
                    itemObj = new JsonObject();
                    int count = detailBll.GetDayCount(item.SceneId, txtFrom, txtTo);
                    itemObj.Accumulate("name", item.Name);
                    itemObj.Accumulate("y", count);
                    newsArry.Add(itemObj);
                }
                json.Accumulate("STATUS", "Success");
                json.Accumulate("Data", newsArry.ToString());
             }
            return json.ToString();
        }
        #endregion
    }
}