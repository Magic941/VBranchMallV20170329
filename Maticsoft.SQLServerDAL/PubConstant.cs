using System;
using System.Configuration;
namespace Maticsoft.SQLServerDAL
{
    public class PubConstant
    {
        /// <summary>
        /// 得到web.config里配置项的数据库连接字符串。
        /// </summary>
        /// <param name="configName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string configName)
        {
            string connectionString = Maticsoft.Common.ConfigHelper.GetConfigString(configName);
            string ConStringEncrypt = Maticsoft.Common.ConfigHelper.GetConfigString("ConStringEncrypt");
            if (ConStringEncrypt == "true")
            {
                connectionString = Maticsoft.Common.DEncrypt.DESEncrypt.Decrypt(connectionString);
            }
            return connectionString;
        }

    }
}
