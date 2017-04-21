/**
* CMSContentHandle.cs
*
* 功 能： [N/A]
* 类 名： CMSContentHandle
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/27 13:12:07  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Maticsoft.Json;
using System.Data;
using Maticsoft.Common;
using System.Linq;

namespace Maticsoft.Web.Ajax_Handle
{
    public class CMSContentHandle : IHttpHandler
    {
        private static readonly string[] AllowFileExt = ".rar|.zip|.doc|.docx|.xls|.swf|.xlsx|.jpg|.jpeg|.png|.gif|.bmp".Split('|');

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            HttpRequest Request = context.Request;
            HttpResponse Response = context.Response;
            string Action = Request.Params["action"];
            switch (Action)
            {
                case "Add":
                    ContentTypeAdd(Request, Response);
                    break;
                case "uploadico":
                    string strFileUrl = BLL.SysManage.ConfigSystem.GetValueByCache("UploadImagePath");
                    UploadPic(Request, Response, strFileUrl);
                    break;
                case "uploadAttachment":
                    ContentAttachmentUpload(Request, Response);
                    break;
                case "DeleteAttachment":
                    DeleteAttachment(Request, Response);
                    break;
                case "uploadSwf":
                    VideoAction(Request, Response);
                    break;
                case "BrandsLogo":
                    string strLogoFileUrl = BLL.SysManage.ConfigSystem.GetValueByCache("BrandsLogo");
                    UploadPic(Request, Response, strLogoFileUrl);
                    break;
                case "GetChildNode":
                    GetChildNode(context);
                    break;
                case "GetDepthNode":
                    GetDepthNode(context);
                    break;
                case "GetParentNode":
                    GetParentNode(context);
                    break;
                default:
                    break;
            }
        }



        private void VideoAction(HttpRequest Request, HttpResponse Response)
        {
            HttpPostedFile file = Request.Files["Filedata"];
            Response.Charset = "utf-8";
            Common.Video.ConvertVideo cv = new Common.Video.ConvertVideo();
            Common.Video.VideoModel model = new Common.Video.VideoModel();
            string strFileUrl = BLL.SysManage.ConfigSystem.GetValueByCache("UploadVideoUrl");

            JsonObject json = new JsonObject();
            string ext = Path.GetExtension(file.FileName).ToLower();
            if (!AllowFileExt.Contains(ext))
            {
                LogHelp.AddInvadeLog("Ajax_Handle-CMSContentHandle-VideoAction", Request);
                Response.Clear();
                return;
            }
            if (cv.UploadVideo(file, false, strFileUrl, null, false, false, out model, ".swf"))
            {
                json.Accumulate("Status", "OK");
                json.Accumulate("SavePath", model.SavePath);
                Response.Write(("1|" + json.ToString()));
            }
            else
            {
                json.Accumulate("Status", "Failed");
                json.Accumulate("ErrorMessage", cv.errorMessage);
                Response.Write("0|" + json.ToString());
            }
        }

        private void DeleteAttachment(HttpRequest Request, HttpResponse Response)
        {
            Response.Charset = "utf-8";
            if (!string.IsNullOrWhiteSpace(Request.Params["ContentID"]))
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["ContentID"]))
                {
                    id = Maticsoft.Common.Globals.SafeInt(Request.Params["ContentID"], 0);
                }
                Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
                Maticsoft.Model.CMS.Content model = bll.GetModel(id);
                if (model != null)
                {
                    model.Attachment = null;
                    if (bll.Update(model))
                    {
                        Response.Write("SUCCESS");
                    }
                    else
                    {
                        Response.Write("FAILED");
                    }
                }
                else
                {
                    Response.Write("FAILED");
                }
            }
        }

        private void ContentAttachmentUpload(HttpRequest Request, HttpResponse Response)
        {
            HttpPostedFile file = Request.Files["Filedata"];
            Response.Charset = "utf-8";
            string strFileUrl = BLL.SysManage.ConfigSystem.GetValueByCache("UploadAttachmentPath");
            if (file != null)
            {
                //文件夹是否存在
                string pathStr = HttpContext.Current.Server.MapPath("/" + strFileUrl);
                if (!Directory.Exists(pathStr))
                {
                    //不存在则自动创建文件夹
                    Directory.CreateDirectory(pathStr);
                }
                string ext = Path.GetExtension(file.FileName).ToLower();
                if (!AllowFileExt.Contains(ext))
                {
                    LogHelp.AddInvadeLog("Ajax_Handle-CMSContentHandle-ContentAttachmentUpload", Request);
                    Response.Clear();
                    return;
                }
                string fileName = Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.InvariantCulture) + ext;
                string savepath = pathStr + fileName;
                JsonObject json = new JsonObject();
                try
                {
                    file.SaveAs(savepath);
                    json.Accumulate("Status", "OK");
                    json.Accumulate("SavePath", strFileUrl + fileName);
                    Response.Write(("1|" + json.ToString()));
                }
                catch (Exception)
                {
                    json.Accumulate("Status", "Failed");
                    json.Accumulate("ErrorMessage", "ERROR501，请联系管理员！");
                    Response.Write("0|" + json.ToString());
                }
            }
            else
            {
                JsonObject json = new JsonObject();
                json.Accumulate("Status", "Failed");
                json.Accumulate("ErrorMessage", "ERROR502，请联系管理员！");
                Response.Write("0|" + json.ToString());
            }
        }

        private void UploadPic(HttpRequest Request, HttpResponse Response, string strFileUrl)
        {
            HttpPostedFile file = Request.Files["Filedata"];
            if (file != null)
            {
                //文件夹是否存在
                string pathStr = HttpContext.Current.Server.MapPath("/" + strFileUrl);
                if (!Directory.Exists(pathStr))
                {
                    //不存在则自动创建文件夹
                    Directory.CreateDirectory(pathStr);
                }
                string ext = Path.GetExtension(file.FileName).ToLower();
                if (!AllowFileExt.Contains(ext))
                {
                    LogHelp.AddInvadeLog("Ajax_Handle-CMSContentHandle-UploadPic", Request);
                    Response.Clear();
                    return;
                }
                string fileName = Guid.NewGuid().ToString("N", System.Globalization.CultureInfo.InvariantCulture) + ext;
                string savepath = pathStr + fileName;
                JsonObject json = new JsonObject();
                try
                {
                    file.SaveAs(savepath);
                    json.Accumulate("Status", "OK");
                    json.Accumulate("SavePath", strFileUrl + fileName);
                    Response.Write(("1|" + json.ToString()));
                }
                catch (Exception)
                {
                    json.Accumulate("Status", "Failed");
                    json.Accumulate("ErrorMessage", "ERROR401，请联系管理员！");
                    Response.Write("0|" + json.ToString());
                }
            }
            else
            {
                JsonObject json = new JsonObject();
                json.Accumulate("Status", "Failed");
                json.Accumulate("ErrorMessage", "ERROR402，请联系管理员！");
                Response.Write("0|" + json.ToString());
            }
        }

        private void ContentTypeAdd(HttpRequest Request, HttpResponse Response)
        {
            Maticsoft.BLL.CMS.ClassType bll = new Maticsoft.BLL.CMS.ClassType();
            Maticsoft.Model.CMS.ClassType model = null;
            if (!string.IsNullOrWhiteSpace(Request.Params["ClassTypeName"]) && !string.IsNullOrWhiteSpace(Request.Params["ClassTypeID"]))
            {
                string str = Request.Params["ClassTypeID"];
                model = new Maticsoft.Model.CMS.ClassType();
                model.ClassTypeID = int.Parse(str);
                model.ClassTypeName = Request.Params["ClassTypeName"];
                if (bll.Update(model))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("ADDFAILED");
                }
            }
            else if (!string.IsNullOrWhiteSpace(Request.Params["ClassTypeName"]))
            {
                model = new Maticsoft.Model.CMS.ClassType();
                model.ClassTypeName = Request.Params["ClassTypeName"];
                if (bll.Add(model))
                {
                    Response.Write("SUCCESS");
                }
                else
                {
                    Response.Write("EDITFAILED");
                }
            }
            else
            {
                Response.Write("FAILED");
            }
        }


        BLL.CMS.VideoClass videocate = new BLL.CMS.VideoClass();
        private void GetChildNode(HttpContext context)
        {
            string parentIdStr = context.Request.Params["ParentId"];
            JsonObject json = new JsonObject();
            int parentId = Globals.SafeInt(parentIdStr, 0);
            DataSet ds = videocate.GetCategorysByParentIdDs(parentId);
            if (ds.Tables[0].Rows.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            json.Accumulate("DATA", ds.Tables[0]);
            context.Response.Write(json.ToString());
        }

        private void GetDepthNode(HttpContext context)
        {
            int nodeId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            List<Maticsoft.Model.CMS.VideoClass> list;
            if (nodeId > 0)
            {
                Model.CMS.VideoClass model = videocate.GetModel(nodeId);
                list = videocate.GetCategorysByDepth(model.Depth);
            }
            else
            {
                list = videocate.GetCategorysByDepth(1);
            }
            if (list.Count < 1)
            {
                json.Accumulate("STATUS", "NODATA");
                context.Response.Write(json.ToString());
                return;
            }
            json.Accumulate("STATUS", "OK");
            JsonArray data = new JsonArray();
            list.ForEach(info => data.Add(
                new JsonObject(
                    new string[] { "ClassID", "ClassName" },
                    new object[] { info.VideoClassID, info.VideoClassName }
                    )));
            json.Accumulate("DATA", data);
            context.Response.Write(json.ToString());
        }

        private void GetParentNode(HttpContext context)
        {
            int ParentId = Globals.SafeInt(context.Request.Params["NodeId"], 0);
            JsonObject json = new JsonObject();
            DataSet ds = videocate.GetList("");
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                Model.CMS.VideoClass model = videocate.GetModel(ParentId);
                if (model != null)
                {

                    string[] strList = model.Path.TrimEnd('|').Split('|');
                    string strClassID = string.Empty;
                    if (strList.Length > 0)
                    {
                        List<DataRow[]> list = new List<DataRow[]>();
                        for (int i = 0; i < strList.Length; i++)
                        {
                            DataRow[] dsParent = null;
                            if (i == 0)
                            {
                                dsParent = dt.Select("ParentID=0");
                            }
                            else
                            {
                                dsParent = dt.Select("ParentID=" + strList[i]);
                            }
                            if (dsParent.Length > 0)
                            {
                                list.Add(dsParent);
                            }
                        }
                        json.Accumulate("STATUS", "OK");
                        json.Accumulate("DATA", list);
                        json.Accumulate("PARENT", strList);
                    }
                    else
                    {
                        json.Accumulate("STATUS", "NODATA");
                        context.Response.Write(json.ToString());
                        return;
                    }
                }
            }

            context.Response.Write(json.ToString());
        }
    }
}