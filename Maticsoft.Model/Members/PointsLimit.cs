using System;
namespace Maticsoft.Model.Members
{
    /// <summary>
    /// PointsLimit:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PointsLimit
    {
        public PointsLimit()
        { }

        #region Model
        private int _limitid;
        private string _name;
        private int _cycle;
        private string _cycleunit;
        private int _maxtimes;
        private int _targetid;
        private int _targettype;
        /// <summary>
        /// 
        /// </summary>
        public int LimitID
        {
            set { _limitid = value; }
            get { return _limitid; }
        }
        /// <summary>
        /// 条件限制名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 周期
        /// </summary>
        public int Cycle
        {
            set { _cycle = value; }
            get { return _cycle; }
        }
        /// <summary>
        /// 周期单位 如：day,month,year
        /// </summary>
        public string CycleUnit
        {
            set { _cycleunit = value; }
            get { return _cycleunit; }
        }
        /// <summary>
        /// 限制次数
        /// </summary>
        public int MaxTimes
        {
            set { _maxtimes = value; }
            get { return _maxtimes; }
        }
        /// <summary>
        /// 目标ID
        /// </summary>
        public int TargetId
        {
            set { _targetid = value; }
            get { return _targetid; }
        }
        /// <summary>
        /// 目标类型 0：表示商家，1：表示代理商
        /// </summary>
        public int TargetType
        {
            set { _targettype = value; }
            get { return _targettype; }
        }
        #endregion Model

    }
}

