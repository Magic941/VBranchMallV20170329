using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// Regions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Regions
	{
		public Regions()
        { }
        #region Model
        private int? _areaid;
        private int _regionid;
        private int? _parentid;
        private string _regionname;
        private string _spell;
        private string _spellshort;
        private int _displaysequence;
        private string _path;
        private int _depth;
        /// <summary>
        /// 
        /// </summary>
        public int? AreaId
        {
            set { _areaid = value; }
            get { return _areaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int RegionId
        {
            set { _regionid = value; }
            get { return _regionid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RegionName
        {
            set { _regionname = value; }
            get { return _regionname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Spell
        {
            set { _spell = value; }
            get { return _spell; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SpellShort
        {
            set { _spellshort = value; }
            get { return _spellshort; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DisplaySequence
        {
            set { _displaysequence = value; }
            get { return _displaysequence; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Path
        {
            set { _path = value; }
            get { return _path; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Depth
        {
            set { _depth = value; }
            get { return _depth; }
        }
        #endregion Model

	}
}

