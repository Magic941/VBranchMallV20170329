using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Coupon;
using Maticsoft.Common;
using Maticsoft.Json;

namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class ClassList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 409; } } //Shop_优惠券分类管理_列表页
        protected new int Act_AddData = 412;    //Shop_优惠券分类管理_添加数据
        protected new int Act_UpdateData = 413;    //Shop_优惠券分类管理_编辑数据
        protected new int Act_DelData = 414;    //Shop_优惠券分类管理_删除数据
        Maticsoft.BLL.Shop.Coupon.CouponClass classBll=new CouponClass();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());
            }
        }

        #region gridView

        public void BindData()
        {
            
            StringBuilder whereStr=new StringBuilder();
            string keyword = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                whereStr.AppendFormat(" Name Like '%{0}%'", keyword);
            }
            DataSet ds = classBll.GetList(-1, whereStr.ToString(), " Sequence");
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

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
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("btnModify");
                    updatebtn.Visible = false;
                }


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
            int  classId = (int) gridView.DataKeys[e.RowIndex].Value;
            Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll=new CouponRule();
            int count=ruleBll.GetRecordCount(" ClassId="+classId);
            if (count > 0)
            {
                MessageBox.ShowFailTip(this,"该分类下有优惠券数据！");
                return;
            }
            classBll.Delete(classId);
            gridView.OnBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        public string GetStatus(object  target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                int status = Common.Globals.SafeInt(target, 0);
                switch (status)
                {
                    case 0:
                        str = "不启用";
                        break;
                    case 1:
                        str = "启用";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }

        protected void btnSaveEq_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                int classId = Common.Globals.SafeInt(gridView.DataKeys[i].Values[0].ToString(),0);
                int seq = Common.Globals.SafeInt(((TextBox)(gridView.Rows[i].Cells[0].Controls[1])).Text, 0);
                classBll.UpdateSeqByCid(classId, seq);
            }
            gridView.OnBind();
        }
        
        #endregion


        #region Ajax方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateSeqNum":

                    writeText = UpdateSeqNum();
                    break;
                case "UpdateStatus":

                    writeText = UpdateStatus();
                    break;
                default:
                    writeText = UpdateSeqNum();
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string UpdateSeqNum()
        {
            JsonObject json = new JsonObject();
            int ClassId = Common.Globals.SafeInt(this.Request.Form["ClassId"], 0);
            int UpdateValue = Common.Globals.SafeInt(this.Request.Form["UpdateValue"], 0);
            if (ClassId == 0 || UpdateValue == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                if (classBll.UpdateSeqByCid(ClassId,UpdateValue))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }


        private string UpdateStatus()
        {
            JsonObject json = new JsonObject();
            int ClassId = Common.Globals.SafeInt(this.Request.Form["ClassId"], 0);
            int Status = Common.Globals.SafeInt(this.Request.Form["Status"], 0);
          
                if (classBll.UpdateStatus(ClassId, Status))
                {
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            return json.ToString();
        }
        #endregion
    }
}