/**
* PollActionHelper.cs
*
* 功 能： [N/A]
* 类 名： PollActionHelper
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/10/22 15:31:20  Rock    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Collections.Generic;
namespace Maticsoft.Model.Poll
{
    public class PollActionHelper
    {
        private Model.Poll.Forms _formsHelper;

        public Model.Poll.Forms FormsHelper
        {
            get { return _formsHelper; }
            set { _formsHelper = value; }
        }

        private Model.Poll.Options _optionsHelper;

        public Model.Poll.Options OptionsHelper
        {
            get { return _optionsHelper; }
            set { _optionsHelper = value; }
        }

        private List< Model.Poll.Topics> _topicsHelper;

        public List< Model.Poll.Topics> TopicsHelper
        {
            get { return _topicsHelper; }
            set { _topicsHelper = value; }
        }
    }
}