using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Json.RPC;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.SNS.API
{
    public partial class SNSHandler
    {

        #region  社区专辑
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserId">用户Id，不传表示 无限制</param>
        /// <param name="Type"> 专辑类型：0表示全部</param>
        /// <param name="Top"></param>
        /// <returns></returns>
        [JsonRpcMethod("GetAlbumList", Idempotent = false)]
        [JsonRpcHelp("社区专辑")]
        public JsonObject GetAlbumList(int UserId=0, int Type = 0,int pageIndex = 1, int pageSize = 10 )
        {
            JsonObject result = new JsonObject();
            Maticsoft.BLL.SNS.UserAlbums albumBll = new BLL.SNS.UserAlbums();
            //重置页面索引
            pageIndex = pageIndex > 1 ? pageIndex : 1;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * pageSize;
            List<ViewModel.SNS.AlbumIndex> albumList = albumBll.GetListForPage(
                Type, "", startIndex, endIndex, -1);
            if (albumList == null)
            {
                result.Put("status", "fail");
                result.Put("result", "productModel");
                return result;
            }
            Json.JsonArray jsonArray = new JsonArray();
            JsonObject json;
            foreach (var item in albumList)
            {
                json = new JsonObject();
                json.Put("albumid", item.AlbumID);
                json.Put("name", item.AlbumName);
                json.Put("coverurl", item.CoverPhotoUrl);
                json.Put("desc", item.Description);
                json.Put("photocount", item.PhotoCount);
                json.Put("userid", item.CreatedUserID);
                json.Put("nickname", item.CreatedNickName);
                json.Put("favcount", item.FavouriteCount);
                json.Put("pvcount", item.PVCount);
                json.Put("status", item.Status);
                json.Put("comcount", item.CommentsCount);
                jsonArray.Add(json);
            }
            result.Put("status", "success");
            return result;
        }
      
        #endregion
        
    }
}