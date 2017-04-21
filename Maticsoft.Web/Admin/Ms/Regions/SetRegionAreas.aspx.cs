using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Ms.Regions
{
	public partial class SetRegionAreas : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 333; } } //地区分组_设置地区页

        BLL.Ms.Regions regBll = new BLL.Ms.Regions();
		protected void Page_Load(object sender, EventArgs e)
		{
		    if (!IsPostBack)
		    {
                if (Areaid>0)
		        {
                    BindData();
		            ShowLoad();
		        }
		       
            } 
		}
        private void BindData()
        {
            List<Model.Ms.Regions> regList= regBll.GetProvinceList();
            dropRegion.DataSource = regList;
            dropRegion.DataTextField = "RegionName";
            dropRegion.DataValueField = "RegionId";
            this.dropRegion.DataBind();
        }
        private void ShowLoad()
        {
          hidRegionIDsLoad.Value=regBll.GetRegionIDsByAreaId(Areaid);
        }
        protected int Areaid
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString ["areaid"]))
                {
                    id = Globals.SafeInt(Request.QueryString["areaid"], 0);
                }
                return id;
            }
        }
        protected void SaveBut_Click(object sender, EventArgs e)
        {
            string selectitems = hidRegionValue.Value;
            string db = hidRegionIDsLoad.Value;
            if (db != selectitems) //判读值是否有改变
            {
                //DB
                string[] dbvalue = db.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //Page
                string[] pagevalue = selectitems.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string regionNew = "";//新增的
                string regionDel = "";//删除的

                //识别旧数据
                for (int i = 0; i < dbvalue.Length; i++)
                {
                    if (Array.Exists(pagevalue, xx => xx == dbvalue[i]))
                        continue; //忽略	
                    //不存在 更新到数据库中  //没有的需要更新数据库   修改AreasID 为0
                    regionDel += dbvalue[i] + ",";
                }
                //识别新数据
                for (int i = 0; i < pagevalue.Length; i++)
                {
                    if (Array.Exists(dbvalue, xx => xx == pagevalue[i])) continue;
                    regionNew += pagevalue[i] + ",";  //新数据
                }
                //更新被删除的regionid  修改AreasID 为0
                if (!string.IsNullOrWhiteSpace(regionDel))
                {
                    regionDel = regionDel.TrimEnd(',');
                    if (regBll.UpdateAreaID(regionDel, 0))
                    {
                        hidColse.Value = "1";
                    }
                }
                else
                {
                    hidColse.Value = "1";
                }
                //更新新增的regionid   修改AreasID 为Areaid
                if (!string.IsNullOrWhiteSpace(regionNew))
                {
                    regionNew = regionNew.TrimEnd(',');
                    if (regBll.UpdateAreaID(regionNew, Areaid))
                    {
                        MessageBox.ShowSuccessTip(this, "修改成功");
                        hidColse.Value = hidColse.Value+"2";
                    }
                }
                else
                {
                    hidColse.Value = hidColse.Value + "2";
                }
            }
            else
            {
                hidColse.Value = "12";  //根据这个值来关闭层
            }
            


            //string selectitems = hidRegionValue.Value;
            //string db = hidRegionIDsLoad.Value;
            //if ( db!= selectitems) //判读值是否有改变
            //{
            //    if (!string.IsNullOrWhiteSpace(selectitems))
            //    {
            //        if (regBll.UpdateAreaID(selectitems, Areaid))
            //        {
            //            MessageBox.ShowSuccessTip(this, "修改成功");
            //            hidColse.Value = "Success";
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.ShowFailTip(this, "请先选择");
            //    }
            //}
            //else
            //{
            //    hidColse.Value = "Success"; 
            //}
        }
	}
   
}