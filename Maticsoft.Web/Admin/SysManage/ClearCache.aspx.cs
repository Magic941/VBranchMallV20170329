using System;
using System.Collections;
using System.Text;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class ClearCache : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 62; } } //系统管理_是否显示清空缓存

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnClear_Click(object sender, System.EventArgs e)
        {
            IDictionaryEnumerator de = Cache.GetEnumerator();
            ArrayList list = new ArrayList();
            StringBuilder str = new StringBuilder();

            while (de.MoveNext())
            {
                list.Add(de.Key.ToString());
            }
            foreach (string key in list)
            {
                Cache.Remove(key);
                str.Append("<li>" + key + "......OK! <br>");
            }
            Label1.Text = string.Format("<br>{0}<br>{1}", str.ToString(), Resources.SysManage.lblClearSucceed);
        }
    }
}