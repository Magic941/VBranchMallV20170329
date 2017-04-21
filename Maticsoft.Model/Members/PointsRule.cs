using System;
namespace Maticsoft.Model.Members
{
    /// <summary>
    /// PointsRule:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PointsRule
    {
        public PointsRule()
        { }
        #region Model
        private int _ruleid;
        private int _actionid;
        private int _limitid;
        private string _name;
        private int _score;
        private string _description;
        private int _targetid;
        private int _targettype;
        /// <summary>
        /// 规则ID
        /// </summary>
        public int RuleId
        {
            set { _ruleid = value; }
            get { return _ruleid; }
        }
        /// <summary>
        /// 对应Action
        /// </summary>
        public int ActionId
        {
            set { _actionid = value; }
            get { return _actionid; }
        }
        /// <summary>
        /// 限制条件ID
        /// </summary>
        public int LimitID
        {
            set { _limitid = value; }
            get { return _limitid; }
        }
        /// <summary>
        /// 规则名称 如：登录
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 影响分数
        /// </summary>
        public int Score
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 目标ID 商家或者代理商ID
        /// </summary>
        public int TargetId
        {
            set { _targetid = value; }
            get { return _targetid; }
        }
        /// <summary>
        /// 目标类型0：管理员 1：表示商家，2：表示代理商
        /// </summary>
        public int TargetType
        {
            set { _targettype = value; }
            get { return _targettype; }
        }
        #endregion Model

    }
}

