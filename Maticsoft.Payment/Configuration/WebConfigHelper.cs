using System;
using Maticsoft.Payment.Core;

namespace Maticsoft.Payment.Configuration
{
    /// <summary>
    /// web.config������
    /// Copyright (C) ����׿Խ
    /// </summary>
    public sealed class WebConfigHelper
    {
        /// <summary>
        /// �õ�AppSettings�е������ַ�����Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string cacheKey = "AppSettings-" + key;
            object objModel = DataCache.Get(cacheKey);
            if (objModel == null)
            {
                try
                {
                    System.Configuration.Configuration rootWebConfig =
                    System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                    if (0 < rootWebConfig.AppSettings.Settings.Count &&
                        rootWebConfig.AppSettings.Settings[key] != null)
                    {
                        objModel = rootWebConfig.AppSettings.Settings[key].Value;
                        DataCache.Insert(cacheKey, objModel, 180);
                    }
                }
                catch { }
            }
            return objModel != null ? objModel.ToString() : null;
        }

        /// <summary>
        /// �õ�AppSettings�е�����Bool��Ϣ
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
        /// �õ�AppSettings�е�����Decimal��Ϣ
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
        /// �õ�AppSettings�е�����int��Ϣ
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
