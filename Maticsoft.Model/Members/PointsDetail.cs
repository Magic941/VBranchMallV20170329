using System;
namespace Maticsoft.Model.Members
{
    /// <summary>
    /// PointsDetail:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class PointsDetail
    {
        public PointsDetail()
        { }

        #region Model
        private int _detailid;
        private int _ruleid;
        private int _userid;
        private int _score;
        private string _extdata;
        private int _currentpoints;
        private string _description;
        private DateTime _createddate;
        private int _type;
        /// <summary>
        /// 流水帐号 ID
        /// </summary>
        public int DetailID
        {
            set { _detailid = value; }
            get { return _detailid; }
        }
        /// <summary>
        /// 积分操作ID   特殊值处理 -1：表示购买商品所得积分
        /// </summary>
        public int RuleId
        {
            set { _ruleid = value; }
            get { return _ruleid; }
        }
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
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
        /// 附加数据 如：用户上传了附件的地址
        /// </summary>
        public string ExtData
        {
            set { _extdata = value; }
            get { return _extdata; }
        }
        /// <summary>
        /// 当前积分
        /// </summary>
        public int CurrentPoints
        {
            set { _currentpoints = value; }
            get { return _currentpoints; }
        }
        /// <summary>
        /// 积分详细
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate
        {
            set { _createddate = value; }
            get { return _createddate; }
        }
        /// <summary>
        /// 0:表示积分的获得，1：表示积分的消费
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion Model

        public  string RuleName;

    }
}

