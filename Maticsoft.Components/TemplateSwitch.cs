/**
* TemplateSwitch.cs
*
* 功 能： MVC模版切换
* 类 名： TemplateSwitch
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/12/11 17:27:24  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using Maticsoft.Common;
using Maticsoft.ZipLib;
using Maticsoft.ZipLib.Zip;
using System.IO;

namespace Maticsoft.Web.Components
{
    public static class TemplateSwitch
    {
        private const string DEFAULT_ZIP = "default.zip";
        private static readonly string ApplicationMapPath;

        static TemplateSwitch()
        {
            #region Test
            //Thread.GetDomain().SetData(".appPath", "E:\\Projects\\MaticsoftFK\\Code\\Maticsoft.Web");
            //Thread.GetDomain().SetData(".appVPath", "/");
            //TextWriter tw = new StringWriter();
            //String address = "localhost:8050";
            //HttpWorkerRequest wr = new MyWorkerRequest
            //("/", "friendId=1300000000", tw, address);
            //HttpContext.Current = new HttpContext(wr);
            #endregion

            ApplicationMapPath = HttpContext.Current != null ?
                HttpContext.Current.Server.MapPath(Globals.ApplicationPath) : string.Empty;
        }

        public static bool Switch(string filePath)
        {
            if (string.IsNullOrEmpty(ApplicationMapPath)) return false;

            if (!File.Exists(ApplicationMapPath + DEFAULT_ZIP)) SaveDefault();

            FastZip fastZip = new FastZip();
            fastZip.CreateEmptyDirectories = true;

            fastZip.ExtractZip(filePath, HttpContext.Current.Server.MapPath("/"), FastZip.Overwrite.Always, null, null, "[Content]|[Views]", true);
            //fastZip.ExtractZip(filePath, "/Views/", FastZip.Overwrite.Always, null, null, null, true);

            //(new FastZip()).ExtractEntry(@"E:\001a.zip", "a.txt");  //解压单个文件
            //(new FastZip()).ExtractEntry(@"E:\001a.zip", "test/a.dat"); //解压单个文件
            //(new FastZip()).ExtractEntry(@"E:\001a.zip", "images/");    //解压文件夹

            //if (ZipHelper.ExistsZipFileDirectory(new[] { "Content/", "Views/" }, filePath)) return false;


            return true;
        }


        public static void SaveDefault()
        {
            if (string.IsNullOrEmpty(ApplicationMapPath)) return;

        }
    }

    #region Test
    public class MyWorkerRequest : SimpleWorkerRequest
    {
        private string localAdd = string.Empty;

        public MyWorkerRequest(string page, string query, TextWriter output, string address)
            : base(page, query, output)
        {
            this.localAdd = address;
        }

        public override string GetLocalAddress()
        {
            return this.localAdd;
        }
    } 
    #endregion
}