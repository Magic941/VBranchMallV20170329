using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Maticsoft.Common;
using System.Data;
using System.Drawing;
using Maticsoft.Accounts.Bus;
namespace Maticsoft.Web.Admin.Accounts
{
    public partial class MailConfiglist : PageBaseAdmin
    {
        Maticsoft.BLL.MailConfig bll = new Maticsoft.BLL.MailConfig();
        protected override int Act_PageLoad { get { return 205; } } 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
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

        #region BindData

        public void BindData()
        {
            DataSet ds = new DataSet();
            if (this.CurrentUser != null)
            {
                ds = bll.GetListByUser(this.CurrentUser.UserID);
                gridView.DataSetSource = ds;
            }

        }

        #endregion



        #region gridView

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
                
                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
                //object obj2 = DataBinder.Eval(e.Row.DataItem, "Userid");
                //if ((obj2 != null) && ((obj2.ToString().Trim() != "")))
                //{
                //    int uid = Convert.ToInt32(obj2);
                //    if (uid != UserID)
                //    {
                //        e.Row.Cells[6].Text = "Modify";
                //        e.Row.Cells[7].Text = "Delete";
                //    }
                //}
                //object obj3 = DataBinder.Eval(e.Row.DataItem, "userid");
                //if ((obj3 != null) && ((obj3.ToString() != "")))
                //{
                //    int userid = Convert.ToInt32(obj3);
                //    Maticsoft.Accounts.Bus.User user = new User(userid);
                //    e.Row.Cells[5].Text = user.TrueName;
                //}
            }
        }


        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            bll.Delete(ID);
            gridView.OnBind();
        }
        
        //private string GetSelIDlist()
        //{
        //    string idlist = "";
        //    bool BxsChkd = false;
        //    for (int i = 0; i < gridView.Rows.Count; i++)
        //    {
        //        CheckBox ChkBxItem = (CheckBox)gridView.Rows[i].FindControl(gridView.CheckBoxID);
        //        if (ChkBxItem != null && ChkBxItem.Checked)
        //        {
        //            BxsChkd = true;
        //            idlist += gridView.Rows[i].Cells[1].Text + ",";
        //        }
        //    }
        //    if (BxsChkd)
        //    {
        //        idlist = idlist.Substring(0, idlist.LastIndexOf(","));
        //    }
        //    return idlist;
        //}

        #endregion
    }
}