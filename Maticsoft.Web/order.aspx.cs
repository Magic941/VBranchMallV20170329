using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Maticsoft.Model.Shop.Shipping;
using Maticsoft.Model.SysManage;
using Maticsoft.BLL.Shop.Shipping;
using System.Text;

namespace Maticsoft.Web
{
    public partial class order : System.Web.UI.Page
    {
        public String output;
        private const ApplicationKeyType applicationKeyType = ApplicationKeyType.OpenAPI;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();

            string etype = Request["etype"].ToString();
            string ename = Request["ename"].ToString();
            string number = Request["number"].ToString();
            string ordercode = Request["ordercode"].ToString();


            string key = BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_Express_ApiKey", applicationKeyType);

            string curl = HttpContext.Current.Request.Url.ToString();
            int ft = curl.IndexOf('/');
            int lt = curl.Substring(curl.IndexOf('/') + 2).IndexOf('/');
            string url = curl.Substring(0, ft + 2 + lt);

            String param = "";
            param += "{";
            param += "\"company\":\"" + etype + "\",";
            param += "\"number\":\"" + number + " \",";
            param += "\"from\":\"\",";
            param += "\"to\":\"\",";
            param += "\"key\":\"" + key + "\",";
            param += "\"parameters\":{";
            param += "\"callbackurl\":\"" + url + "/callback.aspx\"";
            param += "}";
            param += "}";
            PostVars.Add("schema", "json");
            PostVars.Add("param", param);

            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues("http://www.kuaidi100.com/poll", "POST", PostVars);
                output = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
                //注意返回的信息，只有result=true的才是成功

                #region 此处用以处理订阅成功操作
                SubscriptionResult model = new SubscriptionResult();
                model = comm.JsonToObject<SubscriptionResult>(output);
                if (model.result != "true")
                {
                    StringBuilder result = new StringBuilder();
                    result.Append("订单编号：" + ordercode + "   ");
                    result.Append("快递单号：" + number + "   ");
                    result.Append("快递公司：" + ename + "   ");
                    result.Append("发生时间：" + DateTime.Now.ToString() + "   \r\n");
                    result.Append("订阅失败");
                    result.Append("\r\n======================================================================================================\r\n");
                    comm.Write(result.ToString());
                    comm.Write(output);
                }

                #endregion
            }
            catch(Exception ex)
            {
                StringBuilder result = new StringBuilder();
                result.Append("订单编号：" + ordercode + "   ");
                result.Append("快递单号：" + number + "   ");
                result.Append("快递公司：" + ename + "   ");
                result.Append("发生时间：" + DateTime.Now.ToString() + "   \r\n");
                result.Append("订阅失败");
                result.Append("\r\n======================================================================================================\r\n");
                comm.Write(result.ToString());
                comm.Write(ex.Message);
            }
        }
    }
}