using System;

namespace Maticsoft.Accounts
{

    /// <summary>
    /// web.config������
    /// Copyright (C) ����׿Խ    
    /// </summary>
    internal sealed class ConfigHelper
    {
        /// <summary>
        /// Ĭ�ϻ���ʱ��
        /// </summary>
        public const int DEFAULT_CACHETIME = 180;

        /// <summary>
        /// �õ�AppSettings�е������ַ�����Ϣ
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
                    //DONE: ���������ļ���ȡ����BUG BEN MODIFY 2013-03-05 20:07
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
