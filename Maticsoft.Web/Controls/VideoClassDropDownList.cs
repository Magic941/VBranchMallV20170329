/**
* VideoClassDropDownList.cs
*
* 功 能： [N/A]
* 类 名： VideoClassDropDownList
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/27 17:17:35  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Data;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Controls
{
    /// <summary>
    /// 视频分类下拉控件 基于DropDownList
    /// </summary>
    public class VideoClassDropDownList : DropDownList
    {
        public VideoClassDropDownList()
        {
            NullToDisplay = false;
            ParentId = null;
        }

        public override void DataBind()
        {
            this.Items.Clear();
            if (this.NullToDisplay)
            {
                this.Items.Add(new ListItem("--  请选择 --", "0"));
            }

            Maticsoft.BLL.CMS.VideoClass bll = new Maticsoft.BLL.CMS.VideoClass();
            DataSet ds = bll.GetList("");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                SetMultiLevelDropDownList("ParentId", ds.Tables[0]);
            }
            base.DataBind();
        }

        public bool NullToDisplay { get; set; }

        public int? ParentId { get; set; }


        public void SetMultiLevelDropDownList(string parentColName, DataTable dataTable)
        {
            string nodeid;
            //加载树
            DataRow[] drs = dataTable.Select(parentColName + "= 0");
            foreach (DataRow r in drs)
            {
                nodeid = r[0].ToString();
                this.Items.Add(new ListItem("╋" + Globals.HtmlDecode(r[1].ToString()), nodeid));
                BindNode(parentColName, nodeid, dataTable, "├");
            }
        }

        private void BindNode(string parentColName, string parentid, DataTable dt, string blank)
        {
            DataRow[] drs = dt.Select(parentColName + "= " + parentid);
            string nodeid;
            foreach (DataRow r in drs)
            {
                nodeid = r[0].ToString();
                this.Items.Add(new ListItem(blank + "『" + Globals.HtmlDecode(r[1].ToString()) + "』", nodeid));
                BindNode(parentColName, nodeid, dt, blank + "─");
            }
        }
    }
}
