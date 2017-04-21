using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Ms.WeiBo
{
    public partial class WeiBoTaskList : PageBaseAdmin
    {
        private Maticsoft.BLL.Ms.WeiBoTaskMsg taskMsgBll = new Maticsoft.BLL.Ms.WeiBoTaskMsg();

        protected override int Act_PageLoad { get { return 345; } } //微博管理_微博任务信息_列表页
        protected new int Act_DelData = 347;    //微博管理_微博任务信息_删除数据
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
           
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[3].Visible = false;
            }

            DataSet ds = new DataSet();
            string strWhere = "";
            string keyWord = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyWord))
            {
                strWhere = " WeiboMsg  like '%" + keyWord + "%' ";
            }
            ds = taskMsgBll.GetList(-1, strWhere, " CreateDate  desc");
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.OnBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
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
            int id = (int)gridView.DataKeys[e.RowIndex].Value;
            //删除任务微博
            taskMsgBll.Delete(id);
            //移除定时器
            //Maticsoft.TimerTask.Task
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
            int msgId = (int)this.gridView.DataKeys[rowIndex].Value;

            BindData();
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (gridView.DataKeys[i].Value != null)
                    {
                        //idlist += gridView.Rows[i].Cells[1].Text + ",";
                        idlist += gridView.DataKeys[i].Value.ToString() + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }


        public void btnDeleteWeibo_Click()
        {

        }

    }
}