using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.SNS
{
     [Serializable]
     public class ZuiInPhoto
    {
        private int _photoid;
        private string _nickname;
        private int _albumscount;
        private int _fanscount;
        private string _photourl;
        private int _userid;

        public string StaticUrl = "";

       
        /// <summary>
        /// 图片的id
        /// </summary>
        public int PhotoId
        {
            set { _photoid = value; }
            get { return _photoid; }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string NickName
        {
            set { _nickname = value; }
            get { return _nickname; }
        }
        /// <summary>
        /// 晒货
        /// </summary>
        public int AlbumsCount
        {
            set { _albumscount = value; }
            get { return _albumscount; }
        }
        public string PhotoUrl
        {
            set { _photourl = value; }
            get { return _photourl; }
        
        }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int FansCount
        {
            set { _fanscount = value; }
            get { return _fanscount; }
        }
        public int UserId
        {
            set { _userid = value; }
            get { return _userid; }
        }
    }
}
