using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.SysManage
{
    public static class EnumHelper
    {
        /// <summary>
        /// 任务类型   0：表示文章  1: 表示清空商品   2：表示SNS图片生成
        /// </summary>
        public enum TaskQueueType
        {
            None = -1,
            /// <summary>
            /// 文章静态化任务
            /// </summary>
            Article = 0,
            /// <summary>
            /// 清空产品回收站任务
            /// </summary>
            RecyleProduct = 1,
               /// <summary>
            /// SNS图片生成任务
            /// </summary>
            SNSImageReGen=2,
                  /// <summary>
            /// CMS图片生成任务
            /// </summary>
            CMSPhotoReGen= 3,
            /// <summary>
            /// SNS 产品详细页静态化
            /// </summary>
            SNSProduct=4,
            /// <summary>
            /// SNS 图片详细页静态化
            /// </summary>
            SNSPhoto=5,
            /// <summary>
            /// 商城图片 生成任务
            /// </summary>
            ShopImageReGen=6,
            /// <summary>
            /// 商品详细页静态化
            /// </summary>
            ShopProduct = 7,
            /// <summary>
            /// 商品二维码
            /// </summary>
            ShopProductCode = 8
        }
    }
}
