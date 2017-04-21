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
    public partial class ReservationLists : System.Web.UI.Page
    {
        private Maticsoft.BLL.Appt.Reservation bll = new BLL.Appt.Reservation();
        private Maticsoft.BLL.Appt.Services serviceBll = new BLL.Appt.Services();
        private Maticsoft.Model.Appt.Services serviceModel = new Model.Appt.Services();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // BindData();
            }
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

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex % 2 == 0)
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#F4F4F4");
                }
                else
                {
                    e.Row.Style.Add(HtmlTextWriterStyle.BackgroundColor, "#FFFFFF");
                }
            }
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

        /// <summary>
        /// 获取状态 0未预约 1已预约 2已过期
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case -2:
                        str = "系统锁定";
                        break;
                    case -1:
                        str = "用户锁定";
                        break;
                    case 0:
                        str = "未处理";
                        break;
                    case 1:
                        str = "取消";
                        break;
                    case 2:
                        str = "活动";
                        break;
                    case 3:
                        str = "已处理";
                        break;
                    case 4:
                        str = "已过期";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取预约类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetReservaType(object target)
        {
            string str = string.Empty;
            string typeName = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int ServiceId = Common.Globals.SafeInt(target, 0);
                serviceModel = serviceBll.GetModel(ServiceId);
                if (serviceModel != null)
                {
                    typeName = serviceModel.Name;
                }
            }
            return typeName;
        }
    }
}