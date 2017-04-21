/**
* AlbumType.cs
*
* 功 能： N/A
* 类 名： AlbumType
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:39   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.SNS
{
    /// <summary>
    /// 专辑类型
    /// </summary>
    [Serializable]
    public partial class AlbumType
    {
        public AlbumType()
        { }
        #region Model
        private int _id;
        private string _typename;
        private bool _ismenu;
        private bool _menuisshow;
        private int _menusequence;
        private int? _albumscount = 0;
        private int _status;
        private string _remark;
        /// <summary>
        /// id
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 专辑类型的名称
        /// </summary>
        public string TypeName
        {
            set { _typename = value; }
            get { return _typename; }
        }
        /// <summary>
        /// 是不是菜单
        /// </summary>
        public bool IsMenu
        {
            set { _ismenu = value; }
            get { return _ismenu; }
        }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool MenuIsShow
        {
            set { _menuisshow = value; }
            get { return _menuisshow; }
        }
        /// <summary>
        /// 菜单显示的顺序
        /// </summary>
        public int MenuSequence
        {
            set { _menusequence = value; }
            get { return _menusequence; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AlbumsCount
        {
            set { _albumscount = value; }
            get { return _albumscount; }
        }
        /// <summary>
        /// 状态 0:不可用 1：可用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 专辑类型的说明
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

    }
}


