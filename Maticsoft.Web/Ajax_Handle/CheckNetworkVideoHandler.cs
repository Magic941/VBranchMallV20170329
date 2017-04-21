/**
* CheckNetworkVideo.cs
*
* 功 能： 检查网络视频地址是否正确
* 类 名： CheckNetworkVideo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/1 15:02:06  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web;

using Maticsoft.Common.Video;
using System.Text.RegularExpressions;

namespace Maticsoft.Web.Ajax_Handle
{
    public class CheckNetworkVideoHandler : IHttpHandler
    {
        /// <summary>
        /// 您将需要在您网站的 web.config 文件中配置此处理程序，
        /// 并向 IIS 注册此处理程序，然后才能进行使用。有关详细信息，
        /// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // 如果无法为其他请求重用托管处理程序，则返回 false。
            // 如果按请求保留某些状态信息，则通常这将为 false。
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            //在此写入您的处理程序实现。
            context.Response.ContentType = "text/plain";
            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            string videoUrl = Request.Params["videoUrl"];
            if (Check(videoUrl))
            {
                Response.Write("true");
            }
            else
            {
                Response.Write("false");
            }
        }

        #endregion

        public bool Check(string url)
        {
            if (!IsUrl(url))
            {
                return false;
            }
            else
            {
                if (VideoHelper.IsYouKuVideoUrl(url) || VideoHelper.IsKu6VideoUrl(url))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        Regex regUrl = new Regex(@"(http:\/\/([\w.]+\/?)\S*)");

        /// <summary>
        /// 是否是链接
        /// </summary>
        /// <returns></returns>
        public bool IsUrl(string content)
        {
            Match m = regUrl.Match(content);
            return m.Success;
        }
    }
}
