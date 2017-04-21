/**
* ResultStatus.cs
*
* 功 能： API接口状态
* 类 名： ResultStatus
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

namespace Maticsoft.Web.Handlers.API
{
    /// <summary>
    /// API接口状态
    /// </summary>
    public enum ResultStatus
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 失败
        /// </summary>
        Failed,
        /// <summary>
        /// 错误
        /// </summary>
        Error
    }

}