using System;
using System.Drawing;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.PhotoAlbum
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 243; } } //CMS_相册管理_列表页
        protected new int Act_AddData = 246;    //CMS_相册管理_添加数据
        protected new int Act_UpdateData = 247;    //CMS_相册管理_编辑数据
        protected new int Act_DelData = 248;    //CMS_相册管理_删除数据
 
        //int Act_ShowInvalid = -1; //查看失效数据行为
		BLL.CMS.PhotoAlbum bll = new BLL.CMS.PhotoAlbum();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    btnDelete.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                if (Session["Style"] != null)
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null)
                    {
                        RepeaterPhotoAlbum.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        RepeaterPhotoAlbum.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

                BindData(this.txtKeyword.Text.Trim());
            }
        }

        private void BindData(string strWhere)
        {
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                strWhere = "AlbumName LIKE '%" + strWhere + "%'";
            }
            BLL.CMS.PhotoAlbum albumBLL = new BLL.CMS.PhotoAlbum();
            this.AspNetPager1.RecordCount = albumBLL.GetRecordCount(strWhere);
            RepeaterPhotoAlbum.DataSource = albumBLL.GetListByPage(strWhere, "", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            RepeaterPhotoAlbum.DataBind();
        }
        protected void DataListPhoto_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                LinkButton delbtn = (LinkButton)e.Item.FindControl("lbtnDel");
                delbtn.Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                HtmlGenericControl updatebtn = (HtmlGenericControl)e.Item.FindControl("lbtnModify");
                updatebtn.Visible = false;
            }
        }
        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < RepeaterPhotoAlbum.Items.Count; i++)
            {
                CheckBox ckAlbum = (CheckBox)RepeaterPhotoAlbum.Items[i].FindControl("ckAlbum");
                HiddenField hfAlbumID = (HiddenField)RepeaterPhotoAlbum.Items[i].FindControl("hfAlbumID");
                if (ckAlbum != null && ckAlbum.Checked)
                {
                    BxsChkd = true;
                    if (hfAlbumID.Value != null)
                    {
                        idlist += hfAlbumID.Value + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            if (bll.DeleteList(idlist))
            {
                MessageBox.ShowSuccessTip(this,Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowFailTip(this,Resources.Site.TooltipDelError);
            }
            BindData(this.txtKeyword.Text.Trim());
        }

        protected void RepeaterPhotoAlbum_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                BLL.CMS.PhotoAlbum AlbumBll = new BLL.CMS.PhotoAlbum();
                if (e.CommandArgument != null)
                {
                    int id = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    if (AlbumBll.Delete(id))
                    {
                        BLL.CMS.Photo PhotoBll = new BLL.CMS.Photo();
                        PhotoBll.UpdatePhotoAlbum(id, 0);
                        MessageBox.ShowSuccessTip(this,Resources.Site.TooltipDelOK);
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this,Resources.Site.TooltipDelError);
                        return;
                    }
                }
                BindData(this.txtKeyword.Text.Trim());
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData(this.txtKeyword.Text.Trim());
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(this.txtKeyword.Text.Trim());
        }
    }
}
