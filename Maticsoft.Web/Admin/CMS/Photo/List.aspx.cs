using System;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.CMS.Photo
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
		BLL.CMS.Photo bll = new BLL.CMS.Photo();
        protected string strThumbImageWidth = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageWidth");
        protected string strThumbImageHeight = BLL.SysManage.ConfigSystem.GetValueByCache("ThumbImageHeight");

        protected override int Act_PageLoad { get { return 235; } } //CMS_图片管理_列表页
        protected new int Act_AddData = 239;    //CMS_图片管理_添加数据
        protected new int Act_UpdateData = 240;    //CMS_图片管理_编辑数据
        protected new int Act_DelData = 241;    //CMS_图片管理_删除数据
 

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
                BLL.CMS.PhotoAlbum albumBll = new BLL.CMS.PhotoAlbum();
                this.dorpPhotoAlbum.DataSource = albumBll.GetList("");
                this.dorpPhotoAlbum.DataTextField = "AlbumName";
                this.dorpPhotoAlbum.DataValueField = "AlbumID";
                this.dorpPhotoAlbum.DataBind();
                this.dorpPhotoAlbum.Items.Insert(0, new ListItem("全部", "0"));

                this.dorpPhotoAlbum.SelectedValue = this.AlbumId.ToString();

                BindData(this.AlbumId.ToString());
            }
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
        public void BindData(string strAlbumId)
        {            
            StringBuilder strWhere = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(strAlbumId) && strAlbumId != "0")
            {
                strWhere.Append("T.AlbumId=" + strAlbumId);
            }
            if (!string.IsNullOrWhiteSpace(this.ddlPhotoClass.SelectedValue))
            {
                if (!string.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" AND ");
                }
                strWhere.Append("T.ClassID = " + ddlPhotoClass.SelectedValue);
            }
            if (!string.IsNullOrWhiteSpace(this.txtKeyWord.Text.Trim()))
            {
                if (!string.IsNullOrWhiteSpace(strWhere.ToString()))
                {
                    strWhere.Append(" AND ");
                }
                strWhere.Append("T.PhotoName like '%" + this.txtKeyWord.Text.Trim() + "%' OR T.Description like '%" + this.txtKeyWord.Text.Trim() + "%' OR T.Tags like '%" + this.txtKeyWord.Text.Trim() + "%'");
            }
            this.AspNetPager1.RecordCount = bll.GetRecordCount(strWhere.ToString());
            DataListPhoto.DataSource = bll.GetListByPage(strWhere.ToString(),"",this.AspNetPager1.StartRecordIndex,this.AspNetPager1.EndRecordIndex);
            DataListPhoto.DataBind();
        }

        public int AlbumId
        {
            get
            {
                int iAlbumId = 0;
                if (Request.Params["AlbumID"] != null && PageValidate.IsNumber(Request.Params["AlbumID"]))
                {
                    iAlbumId = int.Parse(Request.Params["AlbumID"]);
                }
                return iAlbumId;
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData(dorpPhotoAlbum.SelectedValue);
        }

        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < DataListPhoto.Items.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)DataListPhoto.Items[i].FindControl("ckPhoto");
                HiddenField hfPhotoId = (HiddenField) DataListPhoto.Items[i].FindControl("hfPhotoId");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (hfPhotoId.Value != null)
                    {
                        idlist += hfPhotoId.Value + ",";
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
            DataSet ds;
            if (bll.DeleteList(idlist,out  ds))
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0) PhysicalFileInfo(ds.Tables[0]);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
            }
            else
            {
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelError);
            }
            BindData(dorpPhotoAlbum.SelectedValue);
        }


        #region 物理删除文件

        private void PhysicalFileInfo(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "" )
                    {
                        DeletePhysicalFile(dt.Rows[n]["ImageUrl"].ToString());
                    }
                    if (dt.Rows[n]["ThumbImageUrl"] != null && dt.Rows[n]["ThumbImageUrl"].ToString() != "" )
                    {
                        DeletePhysicalFile(dt.Rows[n]["ThumbImageUrl"].ToString());
                    }
                    if (dt.Rows[n]["NormalImageUrl"] != null && dt.Rows[n]["NormalImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["NormalImageUrl"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        private void DeletePhysicalFile(string path)
        {
            try
            {
                Maticsoft.Web.Components.FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.CMS, path);
            }
            catch (Exception)
            {
                LogHelp.AddUserLog(CurrentUser.UserName,CurrentUser.UserType,string.Format("删除文件:{0}失败！",path),this);
            }
        }

        #endregion 物理删除文件

        protected void DataListPhoto_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (e.CommandArgument != null)
                {
                    int id = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    DataSet ds;
                    if (bll.DeleteList(id.ToString(),out ds))
                    {
                        if (ds != null && ds.Tables[0].Rows.Count > 0) PhysicalFileInfo(ds.Tables[0]);
                        MessageBox.ShowSuccessTip(this,Resources.Site.TooltipDelOK);
                    }
                    else
                    {
                        MessageBox.ShowFailTip(this,Resources.Site.TooltipDelError);
                        return;
                    }
                }
                BindData(dorpPhotoAlbum.SelectedValue);
            }
        }

        public string GetPhotoCover(object objCoverPhoto, object objPhoto)
        {
            string str =Resources.CMSPhoto.lblSetToCover;
            if (objPhoto != null && objCoverPhoto != null)
            {
                string strPhoto = objPhoto.ToString();
                string strCoverPhoto = objCoverPhoto.ToString();
                if (strPhoto == strCoverPhoto)
                {
                    str =Resources.CMSPhoto.lblFrontCover;
                }
            }
            return str;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(dorpPhotoAlbum.SelectedValue);
        }
    }
}
