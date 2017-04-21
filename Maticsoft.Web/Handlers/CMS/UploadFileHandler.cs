/**
* UploadFileHandler.cs
*
* 功 能： [CMS文件上传]
* 类 名： UploadFileHandler
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/19 11:01:09  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Web;

namespace Maticsoft.Web.Handlers.CMS
{
    public class UploadFileHandler : UploadFileHandlerBase
    {
        /// <summary>
        /// 保存文件后的操作(Response输出状态) - 子类实现
        /// </summary>
        /// <param name="uploadPath">上传路径</param>
        /// <param name="fileName">文件名称</param>
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            context.Response.Write("1|" + uploadPath + "{0}" + fileName);
        }
    }
}