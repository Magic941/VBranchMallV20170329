using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Maticsoft.Model.Shop.Shipping;
using Maticsoft.BLL.Shop.Shipping;
using System.Data;
using Newtonsoft.Json;

namespace Maticsoft.Web
{
    public partial class callback : System.Web.UI.Page
    {
        public String output;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                String param = Request.Form["param"].ToString();
                ExpressTrace model = new ExpressTrace();
                model = comm.JsonToObject<ExpressTrace>(param);

                Express bll = new Express();
                DataSet ds = bll.GetList("ExpressCode='" + model.lastResult.nu + "'", "UpdateTime");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string expresscontent = JsonConvert.SerializeObject(model.lastResult.data);
                    Shop_Express expressmodel = new Shop_Express();
                    expressmodel = bll.GetListModel("ExpressCode='" + model.lastResult.nu + "'", "UpdateTime",1)[0];
                    expressmodel.ExpressContent = expresscontent;
                    expressmodel.UpdateTime = DateTime.Now;
                    expressmodel.State = model.lastResult.message == "3天查询无记录" ? "2" : model.lastResult.message == "60天无变化时" ? "3" : "1";
                    expressmodel.IsCheck = model.lastResult.ischeck;
                    bll.Update(expressmodel);
                    output = "{\"result\":\"true\",\"returnCode\":\"200\",\"message\":\"成功\"}";
                }
            }
            catch(Exception ex)
            {
                output = "{\"result\":\"false\",\"returnCode\":\"500\",\"message\":\"服务器错误\"}"; //如果快递信息保存失败，这里返回失败信息，过30分钟会重推
                comm.Write(ex.Message);
            }
        }
    }
}