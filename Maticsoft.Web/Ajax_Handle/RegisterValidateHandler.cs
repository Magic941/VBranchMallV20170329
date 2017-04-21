using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maticsoft.Common;
using System.Data;
using System.Text;
using System.IO;
using Maticsoft.Json;
namespace Maticsoft.Web.AjaxHandle
{
    public class RegisterValidateHandler : IHttpHandler
    {
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
                case "CheckUser":
                    CheckUser(Request, Response);
                    break;
                case "CheckPhone":
                    CheckPhone(Request, Response);
                    break;
                case "CheckEmil":
                    CheckEmail(Request, Response);
                    break;
                case "ValidateEmil":
                    ValidateEmil(Request, Response);
                    break;
                case "VideoAction":
                    VideoAction(Request, Response);
                    break;
                case "getCitys":
                    getCitys(Request, Response);
                    break;
                case "getAreas":
                    getAreas(Request, Response);
                    break;
                default:
                    break;
            }
        }

        #region 检查用户名唯一性
        /// <summary>
        /// 检查用户名唯一性
        /// </summary>
        private void CheckUser(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["UserName"]))
            {
                string UserName = Request.Params["UserName"];
                if (new Maticsoft.Accounts.Data.User().HasUserByUserName(UserName))
                {
                    Response.Write("COUNTREG");//已存在该用户名，不允许注册
                }
                else
                {
                    Response.Write("CANREG");//不存在该用户名，可以注册
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }
        #endregion

        #region 检查邮箱唯一性
        /// <summary>
        /// 检查邮箱唯一性
        /// </summary>
        private void CheckEmail(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Email"]) && Maticsoft.Common.PageValidate.IsEmail(Request.Params["Email"]))
            {
                string Email = Request.Params["Email"];
                if (new BLL.Members.Users().ExistsByEmail(Email))
                {
                    Response.Write("COUNTREG");
                }
                else
                {
                    Response.Write("CANREG");
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }
        #endregion

        #region 检查电话号码唯一性
        /// <summary>
        /// 检查电话号码
        /// </summary>
        private void CheckPhone(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["PhoneNumber"]) && Maticsoft.Common.PageValidate.IsPhone(Request.Params["PhoneNumber"]))
            {
                string PhoneNumber = Request.Params["PhoneNumber"];
                if (new BLL.Members.Users().ExistByPhone(PhoneNumber))
                {
                    Response.Write("COUNTREG");
                }
                else
                {
                    Response.Write("CANREG");
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }
        #endregion

        #region 邮箱认证   -----暂无用到
        /// <summary>
        /// 邮箱认证
        /// </summary>
        private void ValidateEmil(HttpRequest Request, HttpResponse Response)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["Email"]) && Maticsoft.Common.PageValidate.IsEmail(Request.Params["Email"]))
            {
                BLL.SysManage.VerifyMail vmbll = new BLL.SysManage.VerifyMail();
                Model.SysManage.VerifyMail vmmodel = new Model.SysManage.VerifyMail();
                string emailStr = Request.Params["Email"];
                string webTitle = Request.Params["WebTitle"];
                string keyvalue = Guid.NewGuid().ToString().Replace("-", "");
                string uName = Request.Params["userName"];
                vmmodel.UserName = uName;
                vmmodel.KeyValue = keyvalue;
                vmmodel.CreatedDate = DateTime.Now;
                vmmodel.Status = 0;
                vmbll.Add(vmmodel);
                string connstr = BLL.SysManage.ConfigSystem.GetValueByCache("EmailValidity");//配置路径
                string str = connstr + "?uid=" + uName + "&code=" + keyvalue; //校验参数
                string content = "亲爱的【" + webTitle + "】用户，请您在七天内点击（或复制到浏览器地址栏）以下连接完成邮箱验证: <a href=" + str + ">" + str + "</a>";//发送内容
                try
                {
                    Maticsoft.Common.MailSender.Send(emailStr, webTitle + "用户邮箱验证", content);
                    Response.Write("SENDSUCCESS");
                }
                catch (Exception)
                {
                    Response.Write("SENDERROR");
                }
            }
            else
            {
                Response.Write("ERRORPARA");
            }
        }

        #endregion

        #region 上传视频
        /// <summary>
        /// 上传视频
        /// </summary>
        private void VideoAction(HttpRequest Request, HttpResponse Response)
        {
            HttpPostedFile file = Request.Files["Filedata"];
            Response.Charset = "utf-8";
            Maticsoft.Common.Video.ConvertVideo cv = new Maticsoft.Common.Video.ConvertVideo();
            Maticsoft.Common.Video.VideoModel model = new Maticsoft.Common.Video.VideoModel();
            string strFileUrl = BLL.SysManage.ConfigSystem.GetValueByCache("EnteServiceItemVideoUrl");

            JsonObject json = new JsonObject();

            if (cv.UploadVideo(file, true, strFileUrl, null, true, true, out model, ".flv"))
            {
                json.Accumulate("Status", "OK");
                json.Accumulate("SavePath", model.SavePath);
                json.Accumulate("ImgPath", model.ImgPath);
                json.Accumulate("VideoSpan", model.VideoSpan);

                Response.Write(("1|" + json.ToString()));
            }
            else
            {
                json.Accumulate("Status", "Failed");
                json.Accumulate("ErrorMessage", cv.errorMessage);
                Response.Write("0|" + json.ToString());
            }
        }
        #endregion

        #region 省市选择
        Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
        private void getCitys(HttpRequest Request, HttpResponse Response)
        {
            int pid = Globals.SafeInt(Request.Params["ProvinceID"], 0);
            DataSet ds = bll.GetDisByParentId(pid);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string str = ToJson(ds);
                    Response.Write(str);
                }
                else
                {
                    Response.Write("");
                }
            }
            else
            {
                Response.Write("");
            }
        }

        private void getAreas(HttpRequest Request, HttpResponse Response)
        {
            int cid = Globals.SafeInt(Request.Params["CityID"], 0);
            DataSet ds = bll.GetDisByParentId(cid);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string str = ToJson(ds);
                    Response.Write(str);
                }
                else
                {
                    Response.Write("");
                }
            }
            else
            {
                Response.Write("");
            }
        }
        #endregion

        #region DataSet转换为Json
        /// <summary> 
        /// DataSet转换为Json 
        /// </summary> 
        /// <param name="dataSet">DataSet对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataSet dataSet)
        {
            string jsonString = "{";
            foreach (DataTable table in dataSet.Tables)
            {
                jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
            }
            jsonString = jsonString.TrimEnd(',');
            return jsonString + "}";
        }
        #endregion

        #region Datatable转换为Json
        /// <summary> 
        /// Datatable转换为Json 
        /// </summary> 
        /// <param name="table">Datatable对象</param> 
        /// <returns>Json字符串</returns> 
        public static string ToJson(DataTable dt)
        {
            StringBuilder jsonString = new StringBuilder();
            jsonString.Append("[");
            DataRowCollection drc = dt.Rows;
            for (int i = 0; i < drc.Count; i++)
            {
                jsonString.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string strKey = dt.Columns[j].ColumnName;
                    string strValue = drc[i][j].ToString();
                    Type type = dt.Columns[j].DataType;
                    jsonString.Append("\"" + strKey + "\":");
                    strValue = String.Format(strValue, type);
                    if (j < dt.Columns.Count - 1)
                    {
                        jsonString.Append("'" + strValue + "',");
                    }
                    else
                    {
                        jsonString.Append("'" + strValue + "'");
                    }
                }
                jsonString.Append("},");
            }
            jsonString.Remove(jsonString.Length - 1, 1);
            jsonString.Append("]");
            return jsonString.ToString();
        }
        #endregion
    }
}