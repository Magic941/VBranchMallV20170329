using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.Model.SysManage;

namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 参数配置类
    /// </summary>
    public class ConfigSystem : Maticsoft.BLL.SysManage.ConfigSystem
    {
        public static Maticsoft.Model.SNS.PostsSet GetPostSetByCache()
        {
            string CacheKey = "ConfigSystemPostSetting";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.Model.SNS.PostsSet model = new Model.SNS.PostsSet();
                    model._Narmal_Pricture = PostSetHelper("SNS_Narmal_Pricture_IsShow");
                    model._Narmal_Audio = PostSetHelper("SNS_Narmal_Audio_IsShow");
                    model._Narmal_Video = PostSetHelper("SNS_Narmal_Video_IsShow");
                    model._Picture = PostSetHelper("SNS_Picture_IsShow");
                    model._Product = PostSetHelper("SNS_Product_IsShow");
                    model._PostType_All = PostSetHelper("SNS_PostType_All_IsShow");
                    model._PostType_EachOther = PostSetHelper("_PostType_EachOther_IsShow");
                    model._PostType_Fellow = PostSetHelper("SNS_PostType_Fellow_IsShow");
                    model._PostType_ReferMe = PostSetHelper("SNS_PostType_ReferMe_IsShow");
                    model._PostType_User = PostSetHelper("SNS_PostType_User_IsShow");
                    model.CustomProduct = PostSetHelper("SNS_CustomProduct_IsShow");
                    objModel = model;
                    if (objModel != null)
                    {
                        int CacheTime = Common.Globals.SafeInt(GetValue("CacheTime"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.PostsSet)objModel;
        }

        public static bool PostSetHelper(string name)
        {
            string Temp = GetValue(name);
            return (Temp == null || (Temp != null && Common.Globals.SafeInt(Temp, -1) == 1)) ? true : false;
        }

        public static void UpdatePostSet(Maticsoft.Model.SNS.PostsSet model)
        {

            if (Exists("SNS_Narmal_Pricture_IsShow"))
            {
                Update("SNS_Narmal_Pricture_IsShow", (model._Narmal_Pricture == true ? "1" : "0"), "微博上传图片是否显示");
            }
            else
            {
                Add("SNS_Narmal_Pricture_IsShow", (model._Narmal_Pricture == true ? "1" : "0"), "微博上传图片是否显示");
            }
            if (Exists("SNS_Narmal_Audio_IsShow"))
            {
                Update("SNS_Narmal_Audio_IsShow", (model._Narmal_Audio == true ? "1" : "0"), "微博上传音乐是否显示");
            }
            else
            {
                Add("SNS_Narmal_Audio_IsShow", (model._Narmal_Audio == true ? "1" : "0"), "微博上传音乐是否显示");
            }
            if (Exists("SNS_Narmal_Video_IsShow"))
            {
                Update("SNS_Narmal_Video_IsShow", (model._Narmal_Video == true ? "1" : "0"), "微博上传视频是否显示");
            }
            else
            {
                Add("SNS_Narmal_Video_IsShow", (model._Narmal_Video == true ? "1" : "0"), "微博上传视频是否显示");
            }
            if (Exists("SNS_Picture_IsShow"))
            {
                Update("SNS_Picture_IsShow", (model._Picture == true ? "1" : "0"), "上传图片模块是否显示");
            }
            else
            {
                Add("SNS_Picture_IsShow", (model._Picture == true ? "1" : "0"), "上传图片模块是否显示");
            }

            if (Exists("SNS_Blog_IsShow"))
            {
                Update("SNS_Blog_IsShow", (model._Blog == true ? "True" : "False"), "发表文章");
            }
            else
            {
                Add("SNS_Blog_IsShow", (model._Blog == true ? "True" : "False"), "发表文章");
            }


            if (Exists("SNS_Product_IsShow"))
            {
                Update("SNS_Product_IsShow", (model._Product == true ? "1" : "0"), "上传商品模块是否显示");
            }
            else
            {
                Add("SNS_Product_IsShow", (model._Product == true ? "1" : "0"), "上传商品模块是否显示");
            }
            if (Exists("SNS_PostType_All_IsShow"))
            {
                Update("SNS_PostType_All_IsShow", (model._PostType_All == true ? "1" : "0"), "全部微博是否显示");
            }
            else
            {
                Add("SNS_PostType_All_IsShow", (model._PostType_All == true ? "1" : "0"), "全部微博是否显示");
            }
            if (Exists("SNS_PostType_EachOther_IsShow"))
            {
                Update("SNS_PostType_EachOther_IsShow", (model._PostType_EachOther == true ? "1" : "0"), "互相关注的微博是否显示");
            }
            else
            {
                Add("SNS_PostType_EachOther_IsShow", (model._PostType_EachOther == true ? "1" : "0"), "互相关注的微博是否显示");
            }
            if (Exists("SNS_PostType_Fellow_IsShow"))
            {
                Update("SNS_PostType_Fellow_IsShow", (model._PostType_Fellow == true ? "1" : "0"), "我关注的微博是否显示");

            }
            else
            {
                Add("SNS_PostType_Fellow_IsShow", (model._PostType_Fellow == true ? "1" : "0"), "我关注的微博是否显示");
            }
            if (Exists("SNS_PostType_ReferMe_IsShow"))
            {
                Update("SNS_PostType_ReferMe_IsShow", (model._PostType_ReferMe == true ? "1" : "0"), "提到我的微博是否显示");
            }
            else
            {
                Add("SNS_PostType_ReferMe_IsShow", (model._PostType_ReferMe == true ? "1" : "0"), "提到我的微博是否显示");
            }
            if (Exists("SNS_PostType_User_IsShow"))
            {
                Update("SNS_PostType_User_IsShow", (model._PostType_User == true ? "1" : "0"), "我发表的");
            }
            else
            {
                Add("SNS_PostType_User_IsShow", (model._PostType_User == true ? "1" : "0"), "我发表的");
            }

            Modify("SNS_CustomProduct_IsShow", (model.CustomProduct == true ? "1" : "0"), "是否启用用户分享自定义商品", ApplicationKeyType.SNS);

            Modify("SNS_TaoProduct_IsShow", (model.TaoProduct == true ? "1" : "0"), "是否启用用户分享淘宝商品", ApplicationKeyType.SNS);
   

            string CacheKey = "ConfigSystemPostSetting";
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel != null)
            {
                Maticsoft.Common.DataCache.DeleteCache("ConfigSystemPostSetting");
            }
            objModel = model;
            int CacheTime = Common.Globals.SafeInt(GetValue("CacheTime"), 30);
            Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(CacheTime), TimeSpan.Zero);

        }


    }
}
