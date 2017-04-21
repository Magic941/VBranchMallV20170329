using System;
using System.Web;
using System.Configuration;
namespace Maticsoft.Accounts
{
    public class PubConstant
    {
        private const string SQLSERVERDAL = "Maticsoft.SQLServerDAL";
        public static bool IsSQLServer = (ConfigHelper.GetConfigString("DAL") == SQLSERVERDAL);

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get
            {
                string _connectionString = ConfigHelper.GetConfigString("ConnectionString");
                string ConStringEncrypt = ConfigHelper.GetConfigString("ConStringEncrypt");
                if (ConStringEncrypt == "true")
                {
                    _connectionString = DESEncrypt.Decrypt(_connectionString);
                }
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    throw new ArgumentNullException("Illegal [ConnectionString] in the configuration file.");
                }
                return _connectionString;
            }
        }
    }
}
