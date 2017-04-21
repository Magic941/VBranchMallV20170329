using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json.RPC;
using Maticsoft.Model.Settings;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.SNS.API
{
    public partial class SNSHandler
    {
        #region 获取广告位
        [JsonRpcMethod("AdDetail", Idempotent = true)]
        [JsonRpcHelp("根据广告位Id获取广告位数据")]
        public JsonArray AdDetail(int Aid,int Top=0)
        {
            Maticsoft.BLL.Settings.Advertisement bll = new Maticsoft.BLL.Settings.Advertisement();
            List<Advertisement> list = bll.GetListByAidCache(Aid, Top);
            Json.JsonArray array = new JsonArray();
            JsonObject json;
            JsonObject result = new JsonObject();
            if (list == null)
            {
                return null;
            }
            foreach (Advertisement item in list)
            {
                json = new JsonObject();
                json.Put("id", item.AdvertisementId);
                json.Put("title", item.AlternateText);
                json.Put("pic", item.FileUrl);
                json.Put("url", item.NavigateUrl);
                array.Add(json);
            }
            return array;
        }
        #endregion
    }
}