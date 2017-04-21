using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Maticsoft.ViewModel
{
    /// <summary>
    /// viewmodel基类
    /// </summary>
    public  class ViewModelBase
    {
    
        /// <summary>
        /// 根据页数和每页的数据得到开始的索引
        /// </summary>
        /// <param name="PageSize">每页的数据</param>
        /// <param name="PageIndex">第几页</param>
        /// <returns></returns>
        public static int GetStartPageIndex(int PageSize, int PageIndex)
        {
            return PageSize * (PageIndex - 1) + 1;
        }
        /// <summary>
        /// 根据页数和每页的数据得到结束的索引
        /// </summary>
        /// <param name="PageSize">每页的数据</param>
        /// <param name="PageIndex">第几页</param>
        /// <returns></returns>
        public static int GetEndPageIndex(int PageSize, int PageIndex)
        {
            return PageSize * PageIndex;
        }
        /// <summary>
        /// 正则匹配@功能换成相应的链接
        /// </summary>
        /// <returns></returns>
        public static string RegexNickName(string  Des)
        {
            if (Des != null)
            {

                return Regex.Replace(Des, @"@([^,，：:\s@]+)", "<a style=\"color:#ff62ac\" class='UserTip' NickName=$1 href=\"/User/Posts?nickname=$1\">@$1</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            return null;
        }
        /// <summary>
        /// 正则匹配话题
        /// </summary>
        /// <returns></returns>
        public static string RegexTopic(string Des)
        {
            if (Des != null)
            {

                return Regex.Replace(Des, @"#(.*?)#", "<a style=\"color:#ff62ac\"  href=\"/Friend/Index?fid=$1\">$1</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            return null;
        }
        public static TimeSpan DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            try
            {
                TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                return ts;
            }
            catch
            {

            }
            return TimeSpan.Zero;
        }
        /// <summary>
        /// 正则匹配商品id
        /// </summary>
        /// <returns></returns>
        public static string RegexProductId(string ProductLink)
        {
            if (ProductLink != null)
            {
                Match match = Regex.Match(ProductLink, @"id=(\d*?)[&|\s]");
                if (match.Success)
                { 
                 return  match.Groups[1].Value;
                }
               
            }
            return "No";
        }
        public static string ReturnThumUrl(string ImageUrl)
        {
            if (!string.IsNullOrEmpty(ImageUrl)&&!ImageUrl.Contains("http"))
            {
                ImageUrl = ImageUrl.Replace("{0}", "T400x1280_");
            }
            return ImageUrl;

        }
        public static string Substring(string sub, int EndIndex)
        {
            if (!string.IsNullOrEmpty(sub))
            {
                return sub.Length > EndIndex ? sub.Substring(0, EndIndex - 1)+".." : sub;
            }
            return "";
        }

        public static string  DateDiff(DateTime dt)
        {
            //TimeSpan ts1 = new TimeSpan(dt.Ticks);
            //TimeSpan ts2 = new TimeSpan(DateTime.Now.Ticks);
            //TimeSpan ts = ts1.Subtract(ts2).Duration();
            TimeSpan ts = DateTime.Now - dt;

            if (dt.Year==DateTime.Now.Year&& dt.Day == DateTime.Now.Day && dt.Month == DateTime.Now.Month && dt.Hour == DateTime.Now.Hour && ts.Seconds < 60 && ts.Hours == 0 && ts.Minutes == 0)
            {
                return "刚刚";
            }
            if (dt.Year == DateTime.Now.Year && dt.Day == DateTime.Now.Day && dt.Month == DateTime.Now.Month && ts.Minutes < 60 && ts.Hours == 0)
            {
                return ts.Minutes + "分钟前";
            }
            if (dt.Year == DateTime.Now.Year && dt.Day == DateTime.Now.Day && dt.Month == DateTime.Now.Month && ts.Hours <= 2)
            {
                return ts.Hours + "小时前";
            }
            if (dt.AddDays(1).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                return "昨天" + dt.ToString("HH:mm");
            }
            if (dt.Year == DateTime.Now.Year && dt.Day == DateTime.Now.Day && dt.Month == DateTime.Now.Month && ts.Hours > 2)
            {
                return "今天" + dt.ToString("HH:mm");
            }
            //if (ts.Days > 1)
            //{
            //    return "今天" + dt.ToString("HH:mm");
            //}
            else
            {
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        #region 微博表情
        /// <summary>
        /// 微博表情
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReplaceFace(string content)
        {
            StringBuilder str = new StringBuilder();
            if (!string.IsNullOrEmpty(content))
            {
                str.Append(content.Replace("[微笑]", "<img src=\"/Images/face/微笑.gif\" title=\"微笑\" border=\"0\" alt=\"\">")
                           .Replace("[撇嘴]", "<img src=\"/Images/face/撇嘴.gif\" title=\"撇嘴\" border=\"0\" alt=\"\">")
                           .Replace("[色]", "<img src=\"/Images/face/色.gif\" title=\"色\" border=\"0\" alt=\"\">")
                           .Replace("[发呆]", "<img src=\"/Images/face/发呆.gif\" title=\"发呆\" border=\"0\" alt=\"\">")
                           .Replace("[得意]", "<img src=\"/Images/face/得意.gif\" title=\"得意\" border=\"0\" alt=\"\">")

                           .Replace("[流泪]", "<img src=\"/Images/face/流泪.gif\" title=\"流泪\" border=\"0\" alt=\"\">")
                           .Replace("[害羞]", "<img src=\"/Images/face/害羞.gif\" title=\"害羞\" border=\"0\" alt=\"\">")
                           .Replace("[闭嘴]", "<img src=\"/Images/face/闭嘴.gif\" title=\"闭嘴\" border=\"0\" alt=\"\">")
                           .Replace("[睡]", "<img src=\"/Images/face/睡.gif\" title=\"睡\" border=\"0\" alt=\"\">")
                           .Replace("[大哭]", "<img src=\"/Images/face/大哭.gif\" title=\"大哭\" border=\"0\" alt=\"\">")

                           .Replace("[尴尬]", "<img src=\"/Images/face/尴尬.gif\" title=\"尴尬\" border=\"0\" alt=\"\">")
                           .Replace("[调皮]", "<img src=\"/Images/face/调皮.gif\" title=\"调皮\" border=\"0\" alt=\"\">")
                           .Replace("[呲牙]", "<img src=\"/Images/face/呲牙.gif\" title=\"呲牙\" border=\"0\" alt=\"\">")
                           .Replace("[惊讶]", "<img src=\"/Images/face/惊讶.gif\" title=\"惊讶\" border=\"0\" alt=\"\">")
                           .Replace("[难过]", "<img src=\"/Images/face/难过.gif\" title=\"难过\" border=\"0\" alt=\"\">")

                           .Replace("[酷]", "<img src=\"/Images/face/酷.gif\" title=\"酷\" border=\"0\" alt=\"\">")
                           .Replace("[冷汗]", "<img src=\"/Images/face/冷汗.gif\" title=\"冷汗\" border=\"0\" alt=\"\">")
                           .Replace("[抓狂]", "<img src=\"/Images/face/抓狂.gif\" title=\"抓狂\" border=\"0\" alt=\"\">")
                           .Replace("[吐]", "<img src=\"/Images/face/吐.gif\" title=\"吐\" border=\"0\" alt=\"\">")
                           .Replace("[偷笑]", "<img src=\"/Images/face/偷笑.gif\" title=\"偷笑\" border=\"0\" alt=\"\">")

                           .Replace("[可爱]", "<img src=\"/Images/face/可爱.gif\" title=\"可爱\" border=\"0\" alt=\"\">")
                           .Replace("[白眼]", "<img src=\"/Images/face/白眼.gif\" title=\"白眼\" border=\"0\" alt=\"\">")
                           .Replace("[傲慢]", "<img src=\"/Images/face/傲慢.gif\" title=\"傲慢\" border=\"0\" alt=\"\">")
                           .Replace("[饥饿]", "<img src=\"/Images/face/饥饿.gif\" title=\"饥饿\" border=\"0\" alt=\"\">")
                           .Replace("[困]", "<img src=\"/Images/face/困.gif\" title=\"困\" border=\"0\" alt=\"\">")

                           .Replace("[惊恐]", "<img src=\"/Images/face/惊恐.gif\" title=\"惊恐\" border=\"0\" alt=\"\">")
                           .Replace("[流汗]", "<img src=\"/Images/face/流汗.gif\" title=\"流汗\" border=\"0\" alt=\"\">")
                           .Replace("[憨笑]", "<img src=\"/Images/face/憨笑.gif\" title=\"憨笑\" border=\"0\" alt=\"\">")
                           .Replace("[大兵]", "<img src=\"/Images/face/大兵.gif\" title=\"大兵\" border=\"0\" alt=\"\">")
                           .Replace("[奋斗]", "<img src=\"/Images/face/奋斗.gif\" title=\"奋斗\" border=\"0\" alt=\"\">")

                           .Replace("[咒骂]", "<img src=\"/Images/face/咒骂.gif\" title=\"咒骂\" border=\"0\" alt=\"\">")
                           .Replace("[疑问]", "<img src=\"/Images/face/疑问.gif\" title=\"疑问\" border=\"0\" alt=\"\">")
                           .Replace("[嘘]", "<img src=\"/Images/face/嘘.gif\" title=\"嘘\" border=\"0\" alt=\"\">")
                           .Replace("[晕]", "<img src=\"/Images/face/晕.gif\" title=\"晕\" border=\"0\" alt=\"\">")
                           .Replace("[折磨]", "<img src=\"/Images/face/折磨.gif\" title=\"折磨\" border=\"0\" alt=\"\">")

                           .Replace("[衰]", "<img src=\"/Images/face/衰.gif\" title=\"衰\" border=\"0\" alt=\"\">")
                           .Replace("[骷髅]", "<img src=\"/Images/face/骷髅.gif\" title=\"骷髅\" border=\"0\" alt=\"\">")
                           .Replace("[再见]", "<img src=\"/Images/face/再见.gif\" title=\"再见\" border=\"0\" alt=\"\">")
                           .Replace("[擦汗]", "<img src=\"/Images/face/擦汗.gif\" title=\"擦汗\" border=\"0\" alt=\"\">")
                           .Replace("[抠鼻]", "<img src=\"/Images/face/抠鼻.gif\" title=\"抠鼻\" border=\"0\" alt=\"\">")

                           .Replace("[鼓掌]", "<img src=\"/Images/face/鼓掌.gif\" title=\"鼓掌\" border=\"0\" alt=\"\">")
                           .Replace("[糗大了]", "<img src=\"/Images/face/糗大了.gif\" title=\"糗大了\" border=\"0\" alt=\"\">")
                           .Replace("[坏笑]", "<img src=\"/Images/face/坏笑.gif\" title=\"坏笑\" border=\"0\" alt=\"\">")
                           .Replace("[左哼哼]", "<img src=\"/Images/face/左哼哼.gif\" title=\"左哼哼\" border=\"0\" alt=\"\">")
                           .Replace("[右哼哼]", "<img src=\"/Images/face/右哼哼.gif\" title=\"\" border=\"0\" alt=\"\">")

                           .Replace("[哈欠]", "<img src=\"/Images/face/哈欠.gif\" title=\"哈欠\" border=\"0\" alt=\"\">")
                           .Replace("[鄙视]", "<img src=\"/Images/face/鄙视.gif\" title=\"鄙视\" border=\"0\" alt=\"\">")
                           .Replace("[委屈]", "<img src=\"/Images/face/委屈.gif\" title=\"委屈\" border=\"0\" alt=\"\">")
                           .Replace("[快哭了]", "<img src=\"/Images/face/快哭了.gif\" title=\"快哭了\" border=\"0\" alt=\"\">")
                           .Replace("[阴险]", "<img src=\"/Images/face/阴险.gif\" title=\"阴险\" border=\"0\" alt=\"\">")

                           .Replace("[亲亲]", "<img src=\"/Images/face/亲亲.gif\" title=\"亲亲\" border=\"0\" alt=\"\">")
                           .Replace("[吓]", "<img src=\"/Images/face/吓.gif\" title=\"吓\" border=\"0\" alt=\"\">")
                           .Replace("[可怜]", "<img src=\"/Images/face/可怜.gif\" title=\"可怜\" border=\"0\" alt=\"\">")
                           .Replace("[菜刀]", "<img src=\"/Images/face/菜刀.gif\" title=\"菜刀\" border=\"0\" alt=\"\">")
                           .Replace("[西瓜]", "<img src=\"/Images/face/西瓜.gif\" title=\"西瓜\" border=\"0\" alt=\"\">")

                           .Replace("[啤酒]", "<img src=\"/Images/face/啤酒.gif\" title=\"啤酒\" border=\"0\" alt=\"\">")
                           .Replace("[篮球]", "<img src=\"/Images/face/篮球.gif\" title=\"篮球\" border=\"0\" alt=\"\">")
                           .Replace("[乒乓]", "<img src=\"/Images/face/乒乓.gif\" title=\"乒乓\" border=\"0\" alt=\"\">")
                           .Replace("[咖啡]", "<img src=\"/Images/face/咖啡.gif\" title=\"咖啡\" border=\"0\" alt=\"\">")
                           .Replace("[饭]", "<img src=\"/Images/face/饭.gif\" title=\"饭\" border=\"0\" alt=\"\">")

                           .Replace("[猪头]", "<img src=\"/Images/face/猪头.gif\" title=\"猪头\" border=\"0\" alt=\"\">")
                           .Replace("[玫瑰]", "<img src=\"/Images/face/玫瑰.gif\" title=\"玫瑰\" border=\"0\" alt=\"\">")
                           .Replace("[凋谢]", "<img src=\"/Images/face/凋谢.gif\" title=\"凋谢\" border=\"0\" alt=\"\">")
                           .Replace("[示爱]", "<img src=\"/Images/face/示爱.gif\" title=\"示爱\" border=\"0\" alt=\"\">")
                           .Replace("[爱心]", "<img src=\"/Images/face/爱心.gif\" title=\"爱心\" border=\"0\" alt=\"\">")

                           .Replace("[心碎]", "<img src=\"/Images/face/心碎.gif\" title=\"心碎\" border=\"0\" alt=\"\">")
                           .Replace("[蛋糕]", "<img src=\"/Images/face/蛋糕.gif\" title=\"蛋糕\" border=\"0\" alt=\"\">")
                           .Replace("[闪电]", "<img src=\"/Images/face/闪电.gif\" title=\"闪电\" border=\"0\" alt=\"\">")
                           .Replace("[炸弹]", "<img src=\"/Images/face/炸弹.gif\" title=\"炸弹\" border=\"0\" alt=\"\">")
                           .Replace("[刀]", "<img src=\"/Images/face/刀.gif\" title=\"刀\" border=\"0\" alt=\"\">")

                           .Replace("[足球]", "<img src=\"/Images/face/足球.gif\" title=\"足球\" border=\"0\" alt=\"\">")
                           .Replace("[瓢虫]", "<img src=\"/Images/face/瓢虫.gif\" title=\"瓢虫\" border=\"0\" alt=\"\">")
                           .Replace("[便便]", "<img src=\"/Images/face/便便.gif\" title=\"便便\" border=\"0\" alt=\"\">")
                           .Replace("[月亮]", "<img src=\"/Images/face/月亮.gif\" title=\"月亮\" border=\"0\" alt=\"\">")
                           .Replace("[太阳]", "<img src=\"/Images/face/太阳.gif\" title=\"太阳\" border=\"0\" alt=\"\">")

                           .Replace("[礼物]", "<img src=\"/Images/face/礼物.gif\" title=\"礼物\" border=\"0\" alt=\"\">")
                           .Replace("[拥抱]", "<img src=\"/Images/face/拥抱.gif\" title=\"拥抱\" border=\"0\" alt=\"\">")
                           .Replace("[强]", "<img src=\"/Images/face/强.gif\" title=\"强\" border=\"0\" alt=\"\">")
                           .Replace("[弱]", "<img src=\"/Images/face/弱.gif\" title=\"弱\" border=\"0\" alt=\"\">")
                           .Replace("[握手]", "<img src=\"/Images/face/握手.gif\" title=\"握手\" border=\"0\" alt=\"\">")

                           .Replace("[胜利]", "<img src=\"/Images/face/胜利.gif\" title=\"胜利\" border=\"0\" alt=\"\">")
                           .Replace("[抱拳]", "<img src=\"/Images/face/抱拳.gif\" title=\"抱拳\" border=\"0\" alt=\"\">")
                           .Replace("[勾引]", "<img src=\"/Images/face/勾引.gif\" title=\"勾引\" border=\"0\" alt=\"\">")
                           .Replace("[拳头]", "<img src=\"/Images/face/拳头.gif\" title=\"拳头\" border=\"0\" alt=\"\">")
                           .Replace("[差劲]", "<img src=\"/Images/face/差劲.gif\" title=\"差劲\" border=\"0\" alt=\"\">")

                           .Replace("[爱你]", "<img src=\"/Images/face/爱你.gif\" title=\"爱你\" border=\"0\" alt=\"\">")
                           .Replace("[NO]", "<img src=\"/Images/face/NO.gif\" title=\"NO\" border=\"0\" alt=\"\">")
                           .Replace("[OK]", "<img src=\"/Images/face/OK.gif\" title=\"OK\" border=\"0\" alt=\"\">")

                           );
            }
            return str.ToString();
        }
        #endregion

      
    }
}
