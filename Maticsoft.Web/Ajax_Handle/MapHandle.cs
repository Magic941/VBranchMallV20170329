/**
* MapHandle.cs
*
* 功 能： 地图Handler
* 类 名： MapHandle
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/31 14:11:06   Ben    初版
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
using System.Linq;
using System.Web;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.Map.Model;

namespace Maticsoft.Web.AjaxHandle
{
    public class MapHandle : Maticsoft.Map.Handler.MapHandlerBase
    {

        protected override bool CheckUser(HttpContext context)
        {
            return (context.User.Identity.IsAuthenticated && context.Session[Globals.SESSIONKEY_ENTERPRISE] != null);
        }

        protected override void ProcessAction(string actionName, HttpContext context)
        {
            switch (actionName)
            {
                case "SetDepartmentMap":
                    SetDepartmentMap(context);
                    return;
                default:
                    return;
            }
        }

        /// <summary>
        /// 设置企业地图标注点
        /// </summary>
        protected void SetDepartmentMap(HttpContext context)
        {
            JsonObject json = new JsonObject();

            //当前操作用户
            Maticsoft.Accounts.Bus.User currentUser = context.Session[Globals.SESSIONKEY_ENTERPRISE] as Maticsoft.Accounts.Bus.User;
            if (currentUser == null) return;

            //获取Ajax参数
            int departmentId = Maticsoft.Common.Globals.SafeInt(
                context.Request.Params["DepartmentId"], 0);                         //设置企业ID
            string markersLongitude = context.Request.Params["MarkersLongitude"];   //标注点-经度
            string markersDimension = context.Request.Params["MarkersDimension"];   //标注点-纬度
            string pointerTitle = context.Request.Params["PointerTitle"];           //标注点-标题
            string pointerContent = context.Request.Params["PointerContent"];       //标注点-描述内容
            string pointImg = context.Request.Params["PointImg"];                   //标注点-图片URL
            int mapId = Maticsoft.Common.Globals.SafeInt(
                context.Request.Params["MapId"], 0);                                //是否更新标注点

            if (departmentId < 1)
            {
                json.Accumulate("ERROR", "NOENTERPRISEID");
                context.Response.Write(json.ToString());
                return;
            }

            //保存数据
            Map.Model.MapInfo mapInfo = new MapInfo();
            mapInfo.UserId = currentUser.UserID;
            mapInfo.DepartmentId = departmentId;
            mapInfo.MarkersLongitude = markersLongitude;
            mapInfo.MarkersDimension = markersDimension;
            mapInfo.PointerTitle =
                Maticsoft.Common.Globals.HtmlEncode(pointerTitle);                  //标题编码保存
            mapInfo.PointerContent =
                Maticsoft.Common.Globals.HtmlEncodeForSpaceWrap(pointerContent);    //将换行符编码保存
            if (!string.IsNullOrWhiteSpace(pointImg))
            {
                //图片URL 可选项
                mapInfo.PointImg = pointImg;
            }
            if (mapId < 1)  //更新判断
            {
                mapInfo.MapId = mapInfoManage.Add(mapInfo); //ADD
            }
            else
            {
                mapInfo.MapId = mapId;
                mapInfoManage.Update(mapInfo);  //UPDATE
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate("DATA", mapInfo);
            context.Response.Write(json.ToString());
            return;
        }
    }
}