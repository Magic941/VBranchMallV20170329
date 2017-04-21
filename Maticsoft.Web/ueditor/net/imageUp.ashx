<%@ WebHandler Language="C#" Class="imageUp" %>

using System;
using System.Web;
using System.IO;

public class imageUp : IHttpHandler{
    public void ProcessRequest(HttpContext context){
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        var picServerUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PicServerUrl");
        //上传配置
        String pathbase ="/Upload/RTF/"+DateTime.Now.ToString("yyyyMMdd")+"/";                           //保存路径
        string[] filetype = { ".gif", ".png", ".jpg", ".jpeg", ".bmp" };          //文件允许格式
        int size = 10240;                                                          //文件大小限制，单位KB

        //文件上传状态,初始默认成功，可选参数{"SUCCESS","ERROR","SIZE","TYPE"}
        String state = "SUCCESS";

        String title = String.Empty;
        String oriName = String.Empty;
        String filename = String.Empty;
        String url = String.Empty;
        String currentType = String.Empty;
        String uploadpath = String.Empty;

        uploadpath = pathbase;//context.Server.MapPath(pathbase);

        var ftpURI = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("FtpURI");
        var ftpName = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("FtpName");
        var ftpPWD = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("FtpPWD");

        var ftphelper = Microsoft.Practices.Unity.InterceptionExtension.Intercept.NewInstance<Maticsoft.BLL.SysManage.FTPHelper>(new Microsoft.Practices.Unity.InterceptionExtension.VirtualMethodInterceptor(), new[] { new Maticsoft.BLL.SysManage.ErrorLogBehavior() }, new[] { ftpURI, ftpName, ftpPWD });

        try{
            HttpPostedFile uploadFile = context.Request.Files[0];
            title = uploadFile.FileName;

            if (!ftphelper.Exists(uploadpath))
                ftphelper.MakeDir(uploadpath);
            //目录验证
            //if (!Directory.Exists(uploadpath)){
            //    Directory.CreateDirectory(uploadpath);
            //}
            
            pathbase = pathbase.Remove(0,13);
            //格式验证
            string[] temp=uploadFile.FileName.Split('.');
            currentType = "."+ temp[temp.Length - 1].ToLower();
            if (Array.IndexOf(filetype, currentType)==-1){
                state = "TYPE";
            }

            //大小验证
            if( uploadFile.ContentLength/1024>size){
                state="SIZE";
            }

            //保存图片  保存到远程，wusg 20140712
            if (state=="SUCCESS"){
                filename = DateTime.Now.ToString("yyyy-MM-dd-ss") + System.Guid.NewGuid() + currentType;
                var mstream = new MemoryStream();
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int sz = uploadFile.InputStream.Read(buffer, 0, 1024);
                    if (sz == 0) break;
                    mstream.Write(buffer, 0, sz);
                } 
                //uploadFile.SaveAs(uploadpath + filename);
                ftphelper.Upload(mstream, uploadpath + filename);
                url = uploadpath + filename;
            }
        }catch (Exception){
            state = "ERROR";
        }

        //获取图片描述
        if (context.Request.Form["pictitle"] != null){
            if (!string.IsNullOrWhiteSpace(context.Request.Form["pictitle"])){
                title = context.Request.Form["pictitle"];
            }
        }
        //获取原始文件名
        if (context.Request.Form["fileName"] != null)
        {
            if (!string.IsNullOrWhiteSpace(context.Request.Form["fileName"]))
            {
                oriName = context.Request.Form["fileName"].Split(',')[1];
            }
        }

        //向浏览器返回数据json数据
        HttpContext.Current.Response.Write("{'url':'" + picServerUrl +"/" + url + "','title':'" + title + "','original':'"+oriName+"','state':'" + state + "'}");
    }

    public bool IsReusable{
        get{
            return false;
        }
    }

}