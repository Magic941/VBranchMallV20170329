using System;

namespace Maticsoft.Accounts
{

    /// <summary>
    /// web.config操作类
    /// Copyright (C) 动软卓越    
    /// </summary>
    internal sealed class ConfigHelper
    {
        /// <summary>
        /// 默认缓存时间
        /// </summary>
        public const int DEFAULT_CACHETIME = 180;

        /// <summary>
        /// 得到AppSettings中的配置字符串信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string cacheKey = "AppSettings-" + key;
            object objModel = Maticsoft.Accounts.DataCache.GetCache(cacheKey);
            if (objModel == null)
            {
                try
                {
                    //DONE: 更正配置文件读取缓存BUG BEN MODIFY 2013-03-05 20:07
                    System.Configuration.Configuration rootWebConfig =
                    System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                    if (0 < rootWebConfig.AppSettings.Settings.Count)
                    {
                        objModel = rootWebConfig.AppSettings.Settings[key].Value;
                        Maticsoft.Accounts.DataCache.SetCache(cacheKey, objModel, DateTime.Now.AddMinutes(DEFAULT_CACHETIME), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return objModel != null ? objModel.ToString() : null;
        }

        /// <summary>
        /// 得到AppSettings中的配置Bool信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(cfgVal))
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置Decimal信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(cfgVal))
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        /// <summary>
        /// 得到AppSettings中的配置int信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (!string.IsNullOrWhiteSpace(cfgVal))
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
    }
}
