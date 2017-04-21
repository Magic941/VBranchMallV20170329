/**
* AttributeHelper.cs
*
* 功 能： [N/A]
* 类 名： AttributeHelper
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/8/22 9:54:32  Rock    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.Shop.Products
{
    public class AttributeHelper
    {

        #region Model
        private long _attributeid;
        private string _attributename;
        private int _typeid;
        private int _usagemode;
        private bool _useattributeimage;

        /// <summary>
        /// 
        /// </summary>
        public long AttributeId
        {
            set { _attributeid = value; }
            get { return _attributeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string AttributeName
        {
            set { _attributename = value; }
            get { return _attributename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TypeId
        {
            set { _typeid = value; }
            get { return _typeid; }
        }
        /// <summary>
        /// 0:单选 1:多选 2:自定义填写 3:规格
        /// </summary>
        public int UsageMode
        {
            set { _usagemode = value; }
            get { return _usagemode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UseAttributeImage
        {
            set { _useattributeimage = value; }
            get { return _useattributeimage; }
        }
        #endregion Model

        private int _valueId;

        public int ValueId
        {
            get { return _valueId; }
            set { _valueId = value; }
        }

        private string _valueStr;

        public string ValueStr
        {
            get { return _valueStr; }
            set { _valueStr = value; }
        }
    }
}
