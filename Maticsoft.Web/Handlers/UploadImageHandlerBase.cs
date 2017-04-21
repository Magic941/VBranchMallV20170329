/**
* UploadImageHandlerBase.cs
*
* 功 能： 上传图片Handler基类
* 类 名： UploadImageHandlerBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/4 17:06:00    Ben     初版
* V0.02  2012/10/16 18:45:00  Ben     上传图片基类再次抽离出上传文件基类
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System.Drawing;
using System.Web;
using Maticsoft.Common;
using System.Collections.Generic;
using Maticsoft.Model.Ms;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Components;
using Maticsoft.BLL.SysManage;
using System.IO;

namespace Maticsoft.Web.Handlers
{
    public abstract class UploadImageHandlerBase : UploadHandlerBase
    {
        protected readonly MakeThumbnailMode ThumbnailMode;

        protected override string[] AllowFileExt
        {
            get
            {
                return ".jpg|.jpeg|.gif|.png|.bmp".Split('|');
            }
        }

        public UploadImageHandlerBase(MakeThumbnailMode mode = MakeThumbnailMode.None, bool isLocalSave = true,ApplicationKeyType applicationKeyType = ApplicationKeyType.None)
            : base(isLocalSave,applicationKeyType)
        {
            ThumbnailMode = mode == MakeThumbnailMode.None ? MakeThumbnailMode.W : mode;
        }

        #region 子类实现
        /// <summary>
        /// 获取常规缩略图尺寸
        /// </summary>
        protected virtual List<Maticsoft.Model.Ms.ThumbnailSize> GetThumSizeList()
        {
            return new List<ThumbnailSize>();
        }

        /// <summary>
        /// 临时保存原文件并生成缩略图
        /// </summary>
        protected override void SaveAs(string uploadPath, string fileName, HttpPostedFile file)
        {
            //保存临时原图
            file.SaveAs(uploadPath + fileName);
            //string message = string.Empty;
            //var  mystream =file.InputStream;var mstream = new MemoryStream();
            //System.Drawing.Image original = System.Drawing.Image.FromStream(mystream);
            //original.Save(mstream,System.Drawing.Imaging.ImageFormat.Jpeg);

            //byte[] data = mstream.GetBuffer();

            //ftpHelper.UploadFile(uploadPath , fileName, data,out  message);

            //生成临时缩略图
            MakeThumbnailList(uploadPath, fileName, GetThumSizeList());
        }

        #endregion

        #region 生成缩略图

        /// <summary>
        /// 生成缩略图
        /// </summary>
        protected virtual void MakeThumbnailList(string uploadPath, string fileName, List<ThumbnailSize> thumSizeList)
        {
        
            bool isAddWater = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_ThumbImage_AddWater");
            //原图水印保存地址
            string imagePath = uploadPath;
            if (isAddWater)
            {
                imagePath = uploadPath + "W_";
                //生成临时原图水印图
                FileHelper.MakeWater(uploadPath + fileName, imagePath + fileName);
            }
            if(thumSizeList!=null&&thumSizeList.Count>0)
            {
                foreach (var thumbnailSize in thumSizeList)
                {
                    ImageTools.MakeThumbnail(imagePath + fileName, uploadPath + thumbnailSize.ThumName + fileName, thumbnailSize.ThumWidth, thumbnailSize.ThumHeight, GetThumMode(thumbnailSize.ThumMode));
                }
            }
        }
        protected virtual void MakeThumbnail(string uploadPath, string fileName, string thumName, int thumWidth, int thumHeight, MakeThumbnailMode mode)
        {
            ImageTools.MakeThumbnail(uploadPath + fileName, uploadPath + thumName + fileName, thumWidth, thumHeight, mode);
        }
        /// <summary>
        /// 获取裁剪模式
        /// </summary>
        /// <param name="ThumMode"></param>
        /// <returns></returns>
        protected MakeThumbnailMode GetThumMode(int ThumMode)
        {
            MakeThumbnailMode mode = MakeThumbnailMode.None;
            switch (ThumMode)
            {
                case 0:
                    mode = MakeThumbnailMode.Auto;
                    break;
                case 1:
                    mode = MakeThumbnailMode.Cut;
                    break;
                case 2:
                    mode = MakeThumbnailMode.H;
                    break;
                case 3:
                    mode = MakeThumbnailMode.HW;
                    break;
                case 4:
                    mode = MakeThumbnailMode.W;
                    break;
                default:
                    mode = MakeThumbnailMode.Auto;
                    break;
            }
            return mode;
        }

        #endregion
    }
}