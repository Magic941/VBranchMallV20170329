/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Settings.FilterWord
{
    public partial class Add : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 375; } } //设置_敏感词管理_添加页

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.txtWords.Text))
            {
                MessageBox.ShowFailTip(this, "请输入敏感词！");
                return;
            }

            Maticsoft.Model.Settings.FilterWords model = new Maticsoft.Model.Settings.FilterWords();
            BLL.Settings.FilterWords manage = new BLL.Settings.FilterWords();

            //把文本逐行分割
            string[] lines = txtWords.Text.TrimEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                if (line.IndexOf("=")<0)
                {
                    MessageBox.ShowFailTip(this, "输入的字符格式不正确！");
                    return;
                }
                string[] fields = line.Split('=');
                string wordPattern = fields[0];
                string replaceWord = fields[1];

                model.WordPattern = wordPattern;
                if (replaceWord == "{BANNED}")//禁用词
                {
                    model.ActionType = 0;
                    model.RepalceWord = "";
                }
                 if (replaceWord == "{MOD}")//需审核词
                {
                    //model.IsForbid = false;
                    //model.IsMod = true;
                    model.ActionType = 1;
                    model.RepalceWord = "";
                }
                if (replaceWord == "{REPLACE}")
                {
                    model.ActionType = 2;
                    model.RepalceWord = String.IsNullOrWhiteSpace(replaceWord) ? "**" : replaceWord;
                }

                Maticsoft.Model.Settings.FilterWords oldWord = manage.GetByWordPattern(wordPattern);
                if (oldWord != null)
                {
                    manage.Delete(oldWord.FilterId);
                }
                manage.Add(model);
            }
            manage.ClearCache();
            MessageBox.ShowSuccessTip(this, "保存成功！", "list.aspx");
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}