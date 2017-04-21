using System.Web;
using Maticsoft.Json;

namespace Maticsoft.Web.Handlers
{
    public class UploadWeChatFileHandler : UploadFileHandlerBase
    {
        protected override string[] AllowFileExt
        {
            get
            {
                return ".mp3|.wma|.wav|.amr|.rar|.zip|.doc|.docx|.xls|.swf|.xlsx|.jpg|.jpeg|.gif|.png|.bmp".Split('|');
            }
        }

        public new const string KEY_DATA = "data";
        public const string KEY_SUCCESS = "success";
        public const string KEY_NAME = "name";
        protected override void ProcessSub(HttpContext context, string uploadPath, string fileName)
        {
            JsonObject json = new JsonObject();
            json.Put(KEY_DATA, uploadPath + fileName);
            json.Put(KEY_NAME, fileName);
            json.Put(KEY_SUCCESS, true);
            context.Response.Write(json.ToString());
        }
        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            //保存临时原图
            file.SaveAs(uploadPath + fileName);
        }
    }
}