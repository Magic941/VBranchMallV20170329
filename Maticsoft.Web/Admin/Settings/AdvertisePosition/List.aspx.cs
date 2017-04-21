/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List.cs
*
* Ver    变更日期                             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年5月31日 14:38:42   孙鹏   创建
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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace Maticsoft.Web.Admin.AdvertisePosition
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 166; } } //网站管理_是否显示广告管理页面

        protected new int Act_DelData = 169;    //网站管理_广告管理_删除广告位
        protected new int Act_UpdateData = 168;    //网站管理_广告管理_编辑广告位
        protected new int Act_AddData = 167;    //网站管理_广告管理_添加广告位
        protected new int Act_DeleteList = 170;    //网站管理_广告管理_批量删除广告位
        Maticsoft.BLL.Settings.AdvertisePosition bll = new Maticsoft.BLL.Settings.AdvertisePosition();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DeleteList)) && GetPermidByActID(Act_DeleteList)!=-1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
               if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData)!=-1)
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

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, Resources.Site.TooltipDelError);
            }
            gridView.OnBind();
        }

        #region gridView

        public void BindData()
        {

            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                gridView.Columns[8].Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                gridView.Columns[9].Visible = false;
            }
            DataSet ds = new DataSet();
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(txtKeyword.Text))
            {
                strWhere.AppendFormat("AdvPositionName like '%{0}%'", txtKeyword.Text.Trim());
            }
            ds = bll.GetList(strWhere.ToString());
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
                Literal litAdCotent = (Literal)e.Row.FindControl("SetAdContent");
                object adType = DataBinder.Eval(e.Row.DataItem, "ShowType");
                object adPostionId = DataBinder.Eval(e.Row.DataItem, "AdvPositionId");
                if (adType != null && Common.Globals.SafeInt(adType.ToString(), -1) != 4)
                {
                    litAdCotent.Text = string.Format("<a href=\"../Advertisement/SingleList.aspx?id={0}\">设置广告内容</a>&nbsp;&nbsp;", adPostionId);
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

        protected string ConvertShowType(object obj)
        {
            if (obj != null)
            {
                switch (obj.ToString())
                {
                    case"0":
                        return "纵向平铺";
                    case "1":
                        return "横向平铺";
                    case "2":
                        return "层叠显示";
                    case "3":
                        return "交替显示";
                    case "4":
                        return "自定义广告位";
                    default:
                        return "未定义的显示类型";
                }
            }
            else
            {
                return "未定义的显示类型";
            }
        }

        public string AdPositionTag(object objPID,object objW, object objH)
        {
            if (objPID != null)
            {
                if (string.IsNullOrWhiteSpace(objPID.ToString())) return "";
                if (objW != null && objH != null)
                {
                    return HttpUtility.HtmlEncode("<script src=\"js/showads.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = 'c=" + objPID.ToString() + "&a=0'; </script>  ");
                }
                else
                {
                    return HttpUtility.HtmlEncode("<script src=\"js/showads.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = 'c=" + objPID.ToString() + "a=0'; </script>  ");
                }
            }
                return "";
        }
    }
}
