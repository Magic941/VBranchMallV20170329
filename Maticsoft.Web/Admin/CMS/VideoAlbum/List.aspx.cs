/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 16:06:13  蒋海滨    初版
* V0.02  2012年6月8日 18:22:02  孙鹏    提示信息修改、using引用移除
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.CMS.VideoAlbum
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为

        Maticsoft.BLL.CMS.VideoAlbum bll = new Maticsoft.BLL.CMS.VideoAlbum();
        protected override int Act_PageLoad { get { return 255; } } //CMS_视频专辑管理_列表页    
        protected new int Act_AddData = 259;    //CMS_视频专辑管理_添加数据
        protected new int Act_UpdateData = 260;    //CMS_视频专辑管理_编辑数据
        protected new int Act_DelData = 261;    //CMS_视频专辑管理_删除数据
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    lbtnDelete.Visible = false;
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }

                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {
            #region
           
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {

                gridView.Columns[7].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {

                gridView.Columns[6].Visible = false;
            }
            #endregion

            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (txtKeyword.Text.Trim() != "")
            {
                strWhere.AppendFormat(" AlbumName like '%{0}%' ", txtKeyword.Text.Trim());
            }
            if (this.dropState.SelectedValue != "")
            {
                if (strWhere.Length > 0)
                {
                    strWhere.AppendFormat(" AND State={0} ", this.dropState.SelectedValue);
                }
                else
                {
                    strWhere.AppendFormat(" State={0} ", this.dropState.SelectedValue);
                }
            }
            ds = bll.GetListEx(strWhere.ToString(), "");
            gridView.DataSetSource = ds;
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
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
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

        #endregion

        #region 获取专辑状态
        /// <summary>
        /// 获取专辑状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoAlbumState(object target)
        {
            //0未审核; 1待审核; 2正常。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "未审核";
                        break;
                    case "1":
                        str = "待审核";
                        break;
                    case "2":
                        str = "正常";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取专辑权限
        /// <summary>
        /// 获取专辑权限
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetVideoAlbumPrivacy(object target)
        {
            //0.对所有人公开；1：仅自己可见；2：仅好友观看。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = Resources.CMSVideo.Open;
                        break;
                    case "1":
                        str = Resources.CMSVideo.Private;
                        break;
                    case "2":
                        str = Resources.CMSVideo.SemiOpen;
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        protected void btnBatch_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (!string.IsNullOrWhiteSpace(this.dropType.SelectedValue) && PageValidate.IsNumber(this.dropType.SelectedValue))
            {
                if (bll.UpdateList(idlist, " State=" + this.dropType.SelectedValue))
                {
                    gridView.OnBind();
                }
            }
        }

        #region 截取字符串
        public string SubString(object target, string sign, int subLength)
        {
            return StringPlus.SubString(target, subLength, sign);
        }
        #endregion
    }
}
