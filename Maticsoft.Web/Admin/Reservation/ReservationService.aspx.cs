using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Reservation
{
    public partial class ReservationService : System.Web.UI.Page
    {
        BLL.Appt.Services bll = new BLL.Appt.Services();
        Model.Appt.Services serviceModel = new Model.Appt.Services();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindData()
        {
            DataSet ds = new DataSet();
            ds = bll.GetAllList();
            gridView.DataSetSource = ds;
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }

       

        /// <summary>
        /// 获取服务类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetServiceType(object target)
        {
            string str = string.Empty;
            string typeName = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int serviceID = Common.Globals.SafeInt(target, 0);
                serviceModel = bll.GetModel(serviceID);
                if (serviceModel != null)
                {
                    typeName = serviceModel.Name;
                }
            }
            return typeName;
        }

        /// <summary>
        /// 获取规则类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetRuleType(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    //case 0:
                    //    str = "时间限定";
                    //    break;
                    case 1:
                        str = "每日量限定";
                        break;
                    case 2:
                        str = "总量限定";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        public string GetPay(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                string status = Globals.SafeString(target, "false").ToLower();
                switch (status)
                {
                    case "true":
                        str = "是";
                        break;
                    case "false":
                        str = "否";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            if (bll.Delete(ID))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "删除成功！");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "删除失败！");
            }
            gridView.OnBind();
        }
    }
}