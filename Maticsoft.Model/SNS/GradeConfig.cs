/*----------------------------------------------------------------

// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：GradeConfig.cs
// 文件功能描述：
//
// 创建标识： [Name]  2012/11/12 14:54:12
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;

namespace Maticsoft.Model.SNS
{
    /// <summary>
    /// 会员等级
    /// </summary>
    [Serializable]
    public partial class GradeConfig
    {
        public GradeConfig()
        { }

        #region Model

        private int _gradeid;
        private string _gradename;
        private int? _minrange;
        private int? _maxrange;
        private string _remark;

        /// <summary>
        ///
        /// </summary>
        public int GradeID
        {
            set { _gradeid = value; }
            get { return _gradeid; }
        }

        /// <summary>
        /// 等级
        /// </summary>
        public string GradeName
        {
            set { _gradename = value; }
            get { return _gradename; }
        }

        /// <summary>
        /// 要求等级最小分数
        /// </summary>
        public int? MinRange
        {
            set { _minrange = value; }
            get { return _minrange; }
        }

        /// <summary>
        /// 要求等级最大分数
        /// </summary>
        public int? MaxRange
        {
            set { _maxrange = value; }
            get { return _maxrange; }
        }

        /// <summary>
        ///
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

        #endregion Model
    }
}