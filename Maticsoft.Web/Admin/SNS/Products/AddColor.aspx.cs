using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Products
{
    public partial class AddColor : PageBaseAdmin
    {
         protected  string strColorValue;
         protected StringBuilder strSelectValue = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindColor();
            }
            
        }
        private void BindColor()
        {
                int id = Maticsoft.Common.Globals.SafeInt(Id, 0);
                Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
                Maticsoft.Model.SNS.Products productModel = new Model.SNS.Products();
                productModel = productBll.GetModel(id);
                if (productModel != null)
                {
                  //  txtColorSelect.Text = productModel.Color;
                    this.hidValue.Value = productModel.Color;
                }
        }


        public string Id
        {
            get
            {
                string id = Request.QueryString["Id"];
                return id;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            int id = Maticsoft.Common.Globals.SafeInt(Id, 0);
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            Maticsoft.Model.SNS.Products productModel = new Model.SNS.Products();
            productModel = productBll.GetModel(id);
            if (productModel != null)
            {
                productModel.Color = this.hidValue.Value;
                productBll.Update(productModel);
                lblTip.Visible = true;
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "更新商品（ProductId="+id+"）的颜色成功", this);
            }

            BindColor();
        }


    }
}