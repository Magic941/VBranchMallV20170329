using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Members;
using Maticsoft.BLL.Ms;
using Maticsoft.Common;
using Maticsoft.Model.Ms;

namespace Maticsoft.Web.Admin.Ms.WeiBo
{
    public partial class WeiBoList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 342; } } //微博管理_微博群发_列表页
        protected new int Act_AddData = 343;    // 微博管理_微博群发_添加数据
        protected new int Act_DelData = 344;    // 微博管理_微博群发_删除数据
        private Maticsoft.BLL.Ms.WeiBoMsg msgBll = new Maticsoft.BLL.Ms.WeiBoMsg();
        private Maticsoft.BLL.Ms.WeiBoTaskMsg taskMsgBll = new Maticsoft.BLL.Ms.WeiBoTaskMsg();
        private Maticsoft.BLL.Members.UserBind bindBll = new UserBind();
        private const string ImagePath = "/Upload/Weibo/{0}/";
        private const string UploadPath = "/Upload/Temp/{0}/";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    btnSave.Visible = false;
                }

                List<Maticsoft.Model.Members.UserBind> userBinds = bindBll.GetWeiBoList(-1);
                //填充时间
                this.txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                for (int i = 0; i < 24; i++)
                {
                    this.ddlHour.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                this.ddlHour.SelectedValue = DateTime.Now.Hour.ToString();
                for (int i = 0; i < 60; i++)
                {
                    this.ddlMins.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                this.ddlMins.SelectedValue = DateTime.Now.Minute.ToString();
                //获取当前用户的微博
                hfWeiboCount.Value = userBinds.Count.ToString();
                if (userBinds != null && userBinds.Count > 0)
                {
                    this.ChkWeibo.DataSource = userBinds;
                    ChkWeibo.DataTextField = "WeiboLogo";
                    ChkWeibo.DataValueField = "BindId";
                    ChkWeibo.DataBind();
                    this.ChkWeibo2.DataSource = userBinds;
                    ChkWeibo2.DataTextField = "WeiboLogo";
                    ChkWeibo2.DataValueField = "BindId";
                    ChkWeibo2.DataBind();

                    for (int i = 0; i < ChkWeibo.Items.Count; i++)
                    {
                        ChkWeibo.Items[i].Selected = true;
                    }

                    for (int i = 0; i < ChkWeibo2.Items.Count; i++)
                    {
                        ChkWeibo2.Items[i].Selected = true;
                    }

                }

            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindData()
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[3].Visible= false;
            }
            DataSet ds = new DataSet();
            string strWhere = "";
            string keyWord = this.txtKeyword.Text;
            if (!String.IsNullOrWhiteSpace(keyWord))
            {
                strWhere = " WeiboMsg  like '%" + keyWord + "%' ";
            }
            ds = msgBll.GetList(-1, strWhere, " CreateDate  desc");
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
            msgBll.Delete(id);
            gridView.OnBind();
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            BindData();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            //添加微博信息
            string desc = this.txtDesc.Value;
            if (String.IsNullOrWhiteSpace(desc))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "微博消息不能为空！");
                return;
            }
            Maticsoft.Model.Ms.WeiBoMsg msg = new Maticsoft.Model.Ms.WeiBoMsg();
            msg.CreateDate = DateTime.Now;
            msg.WeiboMsg = desc;
            string imageUrl = this.hfImage.Value;
            
            if (!String.IsNullOrWhiteSpace(imageUrl))
            {
                //移动图片文件
                string uploadPath = String.Format(UploadPath, DateTime.Now.ToString("yyyyMMdd"));
                string targetPath = String.Format(ImagePath, DateTime.Now.ToString("yyyyMMdd"));
                if (!Directory.Exists(HttpContext.Current.Server.MapPath(targetPath)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath(targetPath));
                }
                string TargetUrl = imageUrl.Replace(uploadPath, targetPath);
                File.Move(HttpContext.Current.Server.MapPath(imageUrl), HttpContext.Current.Server.MapPath(TargetUrl));
                msg.ImageUrl = TargetUrl;
            }

            #region  同步微博

            //获取用户绑定的所有微博帐号
            int count = Common.Globals.SafeInt(hfWeiboCount.Value, 0);
            if (count == 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "该帐号没有绑定任何微博，请先绑定微博！");
                return;
            }
            string BindIds = "";
            bool BxsChkd = false;
            for (int i = 0; i < ChkWeibo.Items.Count; i++)
            {
                if (ChkWeibo.Items[i].Selected)
                {
                    BxsChkd = true;
                    BindIds += ChkWeibo.Items[i].Value.ToString() + ",";

                }
            }
            if (BxsChkd)
            {
                BindIds = BindIds.Substring(0, BindIds.LastIndexOf(","));
            }
            if (String.IsNullOrWhiteSpace(BindIds))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择同步微博帐号！");
                return;
            }

            string url = "http://" + Common.Globals.DomainFullName;

            //定时发送
            if (chkSetTime.Checked)
            {
                //时间格式 如：2013-8-21 19:42
                string dateStr = this.txtDate.Text + " " + this.ddlHour.SelectedValue + ":" + this.ddlMins.SelectedValue;
                msg.PublishDate = Common.Globals.SafeDateTime(dateStr, DateTime.Now) > DateTime.Now ? Common.Globals.SafeDateTime(dateStr, DateTime.Now) : DateTime.Now;

                //添加微博任务数据
                int taskId=taskMsgBll.AddEx(msg);
                string[] str_arr = new string[] { taskId.ToString(), BindIds, desc, url, msg.ImageUrl };
                Maticsoft.TimerTask.Task.Add(Common.Globals.SafeDateTime(dateStr, DateTime.MinValue), WeiBoCallBack, str_arr);
            }
            else
            {
                msg.PublishDate = DateTime.Now;
                msgBll.Add(msg);
                bindBll.SendWeiBo(BindIds, desc, url, msg.ImageUrl);
            }
            #endregion
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "发布成功！");
            this.hfImage.Value = "";
            this.txtDesc.Value = "";
            gridView.OnBind();
        }
        /// <summary>
        /// 定时发送微博CallBack
        /// </summary>
        /// <param name="str_arr"></param>
        private void WeiBoCallBack(string[] str_arr)
        {
            int taskId = Common.Globals.SafeInt(str_arr[0], 0);
            try
            {
                //任务完成
                taskMsgBll.RunTask(taskId);
            }
            catch (Exception)
            {
                throw;
            }
            bindBll.SendWeiBo(str_arr[1], str_arr[2], str_arr[3], str_arr[4]);
        }

        protected void btnSendWeibo_Click(object sender, EventArgs e)
        {
            string ids = GetSelIDlist();
            if (String.IsNullOrWhiteSpace(ids))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择要发布的微博信息！");
                return;
            }
            List<Maticsoft.Model.Ms.WeiBoMsg> msgList = msgBll.GetModelList(" WeiBoId in (" + ids + ") ");

            string BindIds = "";
            bool BxsChkd = false;
            for (int i = 0; i < ChkWeibo2.Items.Count; i++)
            {
                if (ChkWeibo2.Items[i].Selected)
                {
                    BxsChkd = true;
                    BindIds += ChkWeibo2.Items[i].Value.ToString() + ",";

                }
            }
            if (BxsChkd)
            {
                BindIds = BindIds.Substring(0, BindIds.LastIndexOf(","));
            }
            if (String.IsNullOrWhiteSpace(BindIds))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择同步微博帐号！");
                return;
            }

            if (msgList != null && msgList.Count > 0)
            {
                string url = "http://" + Common.Globals.DomainFullName;
                foreach (var msg in msgList)
                {
                    bindBll.SendWeiBo(BindIds, msg.WeiboMsg, url, msg.ImageUrl);
                }
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "发布成功！");
            gridView.OnBind();
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

    }
}