<%@ WebHandler Language="C#" Class="imageDel" %>

using System;
using System.Web;
using System.IO;

public class imageDel : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";

        string action = context.Server.HtmlEncode(context.Request["action"]);
        string str = string.Empty;
        string imageUrl = context.Server.HtmlEncode(context.Request["fileName"]);
        try
        {
            if (!string.IsNullOrWhiteSpace((imageUrl)) && action=="del")
            {
                DeleteFile(context.Server.MapPath(imageUrl));
                str = "success";
            }
            else
            {
                str = "failed";
            }
        }
        catch (Exception ex)
        {
            str = "failed";
        }
        context.Response.Write(str);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>删除结果，成功或失败</returns>
    private bool DeleteFile(string path)
    {
        try
        {
            File.Delete(path);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}