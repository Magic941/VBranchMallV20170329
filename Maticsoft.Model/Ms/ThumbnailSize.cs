using System;
namespace Maticsoft.Model.Ms
{
	/// <summary>
	/// ThumbnailSize:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class ThumbnailSize
	{
		public ThumbnailSize()
		{}
        #region Model
        private string _thumname;
        private int _thumwidth;
        private int _thumheight;
        private int _type;
        private string _remark;
        private string _cloudsizename;
        private int _cloudtype = 0;
        private int _thummode = 0;
        private bool _iswatermark = false;
        private string _theme;
        /// <summary>
        /// 缩略图名称（主键）
        /// </summary>
        public string ThumName
        {
            set { _thumname = value; }
            get { return _thumname; }
        }
        /// <summary>
        /// 缩略图宽度
        /// </summary>
        public int ThumWidth
        {
            set { _thumwidth = value; }
            get { return _thumwidth; }
        }
        /// <summary>
        /// 缩略图高度
        /// </summary>
        public int ThumHeight
        {
            set { _thumheight = value; }
            get { return _thumheight; }
        }
        /// <summary>
        /// 缩略图所属区域类型,0:表示CMS,1:表示SNS,2:表示Shop,3:表示Tao,4:表示COM
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 标记描述
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 对应云存储尺寸名称
        /// </summary>
        public string CloudSizeName
        {
            set { _cloudsizename = value; }
            get { return _cloudsizename; }
        }
        /// <summary>
        /// 云存储服务提供商类型 0：表示又拍云
        /// </summary>
        public int CloudType
        {
            set { _cloudtype = value; }
            get { return _cloudtype; }
        }
        /// <summary>
        /// 0:表示 Auto 自动缩放 1：Cut  指定高宽裁减（不变形） 2：H 指定高，宽按比例 3：HW 指定高宽缩放（可能变形） 4：W 指定宽，高按比例
        /// </summary>
        public int ThumMode
        {
            set { _thummode = value; }
            get { return _thummode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsWatermark
        {
            set { _iswatermark = value; }
            get { return _iswatermark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Theme
        {
            set { _theme = value; }
            get { return _theme; }
        }
        #endregion Model

	}
}

