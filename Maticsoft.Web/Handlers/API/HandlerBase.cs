/**
* HandlerBase.cs
*
* 功 能： Json.RPC API 基类
* 类 名： HandlerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/12/17 17:04:23  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Collections;
using System.Web;
using System.Web.Caching;
using System.Web.SessionState;
using Maticsoft.Json.RPC;
using Maticsoft.Json.RPC.Web;

namespace Maticsoft.Web.Handlers.API
{

    public class HandlerBase : JsonRpcHandler, IRequiresSessionState
    {
        #region 常量成员
        protected const string REQUEST_HEADER_METHOD = "X-JSON-RPC";

        protected const string ERROR_CODE_UNAUTHORIZED = "41";
        protected const string ERROR_MSG_UNAUTHORIZED = "您的帐号未授权，无法使用接口系统！";

        protected const string ERROR_CODE_ARGUMENT = "102";
        protected const string ERROR_MSG_ARGUMENT = "参数错误!";

        protected const string ERROR_CODE_NODATA = "104";
        protected const string ERROR_MSG_NODATA = "数据不存在！";

        protected const string ERROR_MSG_LOG = "API: [{0}] 方法发生错误! {1}";

        /// <summary>
        /// 缩略信息_最大数 (TOP)
        /// </summary>
        /// <remarks>GetUserData使用</remarks>
        protected int MAXNUM_SHORTINFO = 10;

        /// <summary>
        /// 上传目录 格式化字符串
        /// <remarks>
        /// {0} 模块文件夹 如 简报 或 考勤
        /// {1} 用户ID文件夹
        /// </remarks>
        /// </summary>
        protected static readonly string PATH_FORMAT_FOLDER =
            string.Format("/{0}/API/{1}/", MvcApplication.UploadFolder, "{0}/{1}");

        /// <summary>
        /// 上传文件名 格式化字符串
        /// <remarks> 
        /// {0} 模块主键 如 简报ID 或 考勤ID
        /// {1} 文件名称 接口端上传
        /// </remarks>
        /// </summary>
        protected const string PATH_FORMAT_FILENAME = "{0}_{1}";

        /// <summary>
        /// 日期时间格式
        /// </summary>
        protected const string DATE_FORMAT_DATETIME = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 日期格式
        /// </summary>
        protected const string DATE_FORMAT_DATE = "yyyy-MM-dd";
        #endregion

        public override void ProcessRequest()
        {
            try
            {
                base.ProcessRequest();
            }
            catch (JsonRpcException ex)
            {
                if (HttpContext.Current.IsDebuggingEnabled)
                    throw;

                LogHelp.AddInvadeLog(ex.Message, System.Web.HttpContext.Current.Request);
                Response.Clear();
                Response.StatusCode = 404;
                Response.Status = "404 Not Found";
                Response.End();
            }
        }

        protected override IDictionary GetFeatures()
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                return base.GetFeatures();
            }

            string key = typeof(JsonRpcService).FullName;
            IDictionary config = (IDictionary)HttpRuntime.Cache.Get(key);

            if (config == null)
            {
                config = new Hashtable(6);

                config.Add("rpc", typeof(JsonRpcExecutive).AssemblyQualifiedName);
                config.Add("getrpc", typeof(JsonRpcGetProtocol).AssemblyQualifiedName);

                HttpRuntime.Cache.Add(key, config, null, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            }
            return config;
        }


    }
}