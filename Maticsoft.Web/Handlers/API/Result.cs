/**
* Result.cs
*
* 功 能： API返回值
* 类 名： Result
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

using System;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers.API
{
    /// <summary>
    /// API返回值
    /// </summary>
    public class Result : JsonObject
    {
        public const string KEY_RESULT = "result";
        public const string KEY_STATUS = "status";
        public const string KEY_ERROR_CODE = "error_code";
        public const string KEY_ERROR_MESSAGE = "message";

        public const string KEY_TOALCOUNT = "toalcount";
        public const string KEY_DATA = "data";
        public const string KEY_SERVERTIME = "servertime";

        public const string STATUS_SUCCESS = "success";
        public const string STATUS_FAILED = "failed";
        public const string STATUS_ERROR = "error";

        public Result(bool value)
        {
            this.Put(KEY_STATUS, value ?
                Handlers.API.ResultStatus.Success.ToString().ToLower() :
                Handlers.API.ResultStatus.Failed.ToString().ToLower());
            this.Put(KEY_RESULT, value);
        }

        public Result(Handlers.API.ResultStatus resultStatus, object value, JsonObject appendResult = null)
        {
            this.Put(KEY_STATUS, resultStatus.ToString().ToLower());
            if (resultStatus == Handlers.API.ResultStatus.Error && value is Exception)
            {
                this.Put(KEY_RESULT, FormatException(value as Exception));
                return;
            }
            this.Put(KEY_RESULT, value);
            if (appendResult != null)
            {
                foreach (JsonMember jsonMember in appendResult)
                    this.Put(jsonMember.Name, jsonMember.Value);
            }
        }

        public static Result HasResult(bool value)
        {
            return new Result(Handlers.API.ResultStatus.Success, value);
        }

        public static JsonObject FormatFailed(string code, string message)
        {
            if (string.IsNullOrWhiteSpace(code)) return null;
            JsonObject exJson = new JsonObject();
            exJson.Put(KEY_ERROR_CODE, code);
            exJson.Put(KEY_ERROR_MESSAGE, message);
            return exJson;
        }
        public static JsonObject FormatException(Exception ex)
        {
            if (ex == null) return null;
            JsonObject exJson = new JsonObject();
            exJson.Put(KEY_ERROR_CODE, "500");
            exJson.Put(KEY_ERROR_MESSAGE, "内部执行错误!");
            return exJson;
        }
    }
}