/**
* Photo.cs
*
* 功 能： [N/A]
* 类 名： Photo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/24 16:23:03  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Model.SNS;

namespace Maticsoft.ViewModel.SNS
{
    /// <summary>
    /// 单个专辑/照片列表
    /// </summary>
    public class PhotoList
    {
        public Maticsoft.Model.SNS.UserAlbums AlbumModel { get; set; }
        public Model.Members.UsersExpModel UserModel { get; set; }
        public List<PostContent> PhotoListWaterfall { get; set; }
        public List<Maticsoft.Model.SNS.ZuiInPhoto> ZuiInList { get; set; }
        public List<Maticsoft.Model.SNS.Categories> PhotoCategory { get; set; }
        public int CommentCount { get; set; }
        public int CommentPageSize { get; set; }
        public List<Maticsoft.Model.SNS.UserAlbums> AlbumsList;
        public Maticsoft.Model.SNS.Categories CategoryInfo { get; set; }
        public Webdiyer.WebControls.Mvc.PagedList<Model.SNS.PostContent> PhotoPagedList { get; set; }
        #region 属性<PhotoList4ThreeCol>和<PhotoList4FourCol>等待合并 BEN ADD 2012-10-08
        private List<PostContent>[] _photoList4ThreeCol;
        public List<PostContent>[] PhotoList4ThreeCol
        {
            get
            {
                if (_photoList4ThreeCol != null) return _photoList4ThreeCol;
                List<PostContent>[] list = new[] { new List<PostContent>(), new List<PostContent>(), new List<PostContent>() };
                if (PhotoPagedList == null) return list;
                int index = 0;
                PhotoPagedList.ForEach(image =>
                {
                    //reset
                    if (index == 3) index = 0;
                    list[index++].Add(image);
                });
                return list;
            }
            set { _photoList4ThreeCol = value; }
        }
        private List<PostContent>[] _photoList4FourCol;
        public List<PostContent>[] PhotoList4FourCol
        {
            get
            {
                if (_photoList4FourCol != null) return _photoList4FourCol;
                List<PostContent>[] list = new[] { new List<PostContent>(), new List<PostContent>(), new List<PostContent>(), new List<PostContent>() };
                if (PhotoPagedList == null) return list;
                int index = 0;
                PhotoPagedList.ForEach(image =>
                {
                    //reset
                    if (index == 4) index = 0;
                    list[index++].Add(image);
                });
                return list;
            }
            set { _photoList4FourCol = value; }
        }

        #endregion
    }

    public class ImageMessage
    {
        public string ImageUrl { get; set; }
        public string ImageAlt { get; set; }

    }

    public class CollectImages
    {
        public List<Maticsoft.Model.SNS.UserAlbums> AlbumList = new List<Maticsoft.Model.SNS.UserAlbums>();
        public List<ImageMessage> ImageList = new List<ImageMessage>();
    }

    public class PhotoAlbum
    {
        public  List<Maticsoft.Model.SNS.UserAlbums> UserAlbums=new List<UserAlbums>();
        public List<Maticsoft.Model.SNS.Categories> PhotoCateList = new List<Maticsoft.Model.SNS.Categories>();
    }
}
