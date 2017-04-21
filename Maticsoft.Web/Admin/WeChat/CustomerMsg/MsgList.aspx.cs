using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Text;

namespace Maticsoft.Web.Admin.WeChat.CustomerMsg
{
    public partial class MsgList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.CustomerMsg msgBll = new Maticsoft.WeChat.BLL.Core.CustomerMsg();
        Maticsoft.WeChat.BLL.Core.Group groupBll = new Maticsoft.WeChat.BLL.Core.Group();
        Maticsoft.WeChat.BLL.Core.User userBll = new Maticsoft.WeChat.BLL.Core.User();
        protected void Page_Load(object sender, EventArgs e)
        {
            //获取所有的公众号
            if (!IsPostBack)
            {
                List<Maticsoft.WeChat.Model.Core.Config> openIds = Maticsoft.WeChat.BLL.Core.Config.GetOpenIds();
                this.ddOpenId.DataSource = openIds;
                this.ddOpenId.DataTextField = "Value";
                this.ddOpenId.DataValueField = "Value";
                this.ddOpenId.DataBind();
                this.ddOpenId.Items.Insert(0, new ListItem("全部", ""));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        #region gridView


        public void BindData()
        {
            StringBuilder strWhere = new StringBuilder();
            string openId = ddOpenId.SelectedValue;
            if (!String.IsNullOrWhiteSpace(openId))
            {
                strWhere.AppendFormat(" OpenId='{0}'", openId);
            }
            if (!String.IsNullOrWhiteSpace(this.txtFrom.Text) && Common.PageValidate.IsDateTime(this.txtFrom.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime >='" + this.txtFrom.Text + "' ");
            }
            //时间段
            if (!String.IsNullOrWhiteSpace(this.txtTo.Text) && Common.PageValidate.IsDateTime(this.txtTo.Text))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("  CreateTime< dateadd(day,1,'{0}')", txtTo.Text.Trim());
            }
            gridView.DataSetSource = msgBll.GetList(-1, strWhere.ToString(), " CreateTime desc ");
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
            if (msgBll.Delete((int)gridView.DataKeys[e.RowIndex].Value))
            {
                gridView.OnBind();
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
        }
        #endregion

        /// <summary>
        ///消息类型
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        protected string GetMsgType(object target)
        {
            //0:取消关注、1:关注、
            string str = "";
            if (!StringPlus.IsNullOrEmpty(target))
            {
                //  地理位置:location,文本消息:text,消息类型:image，链接信息：link，事件信息：event
                string msgType = target.ToString();
                switch (msgType)
                {
                    case "text":
                        str = "文本消息";
                        break;
                    case "location":
                        str = "地理位置";
                        break;
                    case "image":
                        str = "图片消息";
                        break;
                    case "link":
                        str = "链接信息";
                        break;
                    case "event":
                        str = "事件信息";
                        break;
                    case "voice":
                        str = "语音信息";
                        break;
                    case "news":
                        str = "图文消息";
                        break;
                    default:
                        str = "文本消息";
                        break;
                }
            }
            return str;
        }

        /// <summary>
        /// 获取消息ID 的集合
        /// </summary>
        /// <returns></returns>
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

        protected void btnDeleteMsg_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
                return;
            if (msgBll.DeleteListEx(idlist))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK, "MsgList.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }


    }
}