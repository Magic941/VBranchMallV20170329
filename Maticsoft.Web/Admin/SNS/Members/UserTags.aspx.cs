using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Members
{
    public partial class UserTags : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 578; } } //SNS_用户标签管理_列表页
        protected new int Act_AddData = 579;    //SNS_用户标签管理添加数据
        protected new int Act_DelData = 580;    //SNS_用户标签管理_删除数据
        Maticsoft.BLL.SNS.Tags  bTags=new BLL.SNS.Tags();
        Maticsoft.BLL.SNS.TagType bTagType=new BLL.SNS.TagType();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Id > 0)
            {
                bTags.Delete(Id);

            }
            if (!IsPostBack)
            {
                  if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData)!=-1)
                {
                    btnAdd.Visible = false;
                }
                 
                rptTags.DataSource = bTags.GetList("TypeId="+bTagType.GetTagsTypeId("用户标签")+"");
                rptTags.DataBind();

            }
        }
        protected void rptTags_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                HtmlGenericControl delbtn = (HtmlGenericControl)e.Item.FindControl("lbtnDel");
                if (delbtn != null)
                {
                    delbtn.Visible = false;
                }
            }
        }
        public int Id
        {
            get
            {
                int _id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    _id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return _id;
            }
        }

      
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
        
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTags.Text))
            {
                Maticsoft.Model.SNS.Tags mTags=new Model.SNS.Tags();
                mTags.TypeId = bTagType.GetTagsTypeId("用户标签");
                mTags.TagName = txtTags.Text;
                bTags.Add(mTags);
                Response.Redirect("UserTags.aspx");
            }
        }
    }


}