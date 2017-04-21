using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using System.Data;
using Maticsoft.Accounts.Bus;
using System.Drawing;
using Maticsoft.Model.Shop;
namespace Maticsoft.Web.Admin.Shop
{
    public partial class FavoriteList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 560; } } //Shop_收藏管理页
        private User currentUser;
        Maticsoft.BLL.Shop.Favorite favorBll = new BLL.Shop.Favorite();
        Maticsoft.BLL.Shop.Products.ProductInfo productBll = new BLL.Shop.Products.ProductInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!Page.IsPostBack)
            {
                if (UserId != 0)
                {
                    currentUser = new User(UserId);
                    if (currentUser == null)
                    {
                        Response.Write("<script language=javascript>window.alert('" + Resources.Site.TooltipUserExist + "\\');history.back();</script>");
                        return;
                    }
                    this.txtTitle.Text = currentUser.UserName + "的礼品兑换明细";
                }
                else
                {
                    this.txtTitle.Text = "会员收藏列表";
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

        #region gridView

        public void BindData()
        {
            string keyword = this.txtKeyword.Text;
            DataSet ds = favorBll.GetListEX(UserId, keyword);
            if (ds != null)
            {
                gridView.DataSetSource = ds;
            }
        }
        public int UserId
        {
            get
            {
                int userid = 0;
                if (Request.Params["userid"] != null && PageValidate.IsNumber(Request.Params["userid"]))
                {
                    userid = int.Parse(Request.Params["userid"]);
                }
                return userid;
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

        //返回处理
        public void btnReturn_Click(object sender, System.EventArgs e)
        {
            Response.Redirect("/Admin/Accounts/Admin/UserAdmin.aspx");
        }

        /// <summary>
        /// 返回商品名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetTargetName(long ProductId,Int16 typeid)
        {
            switch (typeid)
            {
                case (Int16)FavoriteEnums.Product:
                    return productBll.GetProductName(ProductId);
                default :
                    return "";
            }  
        }
        /// <summary>
        /// 返回用户名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetUserName(int userid)
        {
            Maticsoft.Accounts.Bus.User user=new User(userid);
            if(user!=null)
            {
                return user.UserName;
            }
            return "--";
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.OnBind();
        }

    }
}