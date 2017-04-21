using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using ServiceStack.Text;
using ServiceStack.Redis.Support;

namespace ServiceStack.RedisCache
{
    public class RedisManager
    {
        /// <summary>  
        /// redis配置文件信息  
        /// </summary>  
        //private static RedisConfigInfo redisConfigInfo = RedisConfigInfo.GetConfig();  

        private static PooledRedisClientManager prcm;

        /// <summary>  
        /// 静态构造方法，初始化链接池管理对象  
        /// </summary>  
        static RedisManager()
        {
            CreateManager();
        }


        /// <summary>  
        /// 创建链接池管理对象  
        /// </summary>  
        private static void CreateManager()
        {
            RedisSettingsManager redisset = new RedisSettingsManager();
            RedisSettings rediss = redisset.LoadSettings();
            string[] writeServerList = SplitString(rediss.WriteServerList, ",");
            string[] readServerList = SplitString(rediss.ReadServerList, ",");

            prcm = new PooledRedisClientManager(readServerList, writeServerList,
                             new RedisClientManagerConfig
                             {
                                 MaxWritePoolSize = rediss.MaxWritePoolSize,
                                 MaxReadPoolSize = rediss.MaxReadPoolSize,
                                 AutoStart = rediss.AutoStart
                             });
        }

        private static string[] SplitString(string strSource, string split)
        {
            return strSource.Split(split.ToArray());
        }

        /// <summary>  
        /// 客户端缓存操作对象  
        /// </summary>  
        public static IRedisClient GetClient()
        {
            if (prcm == null)
                CreateManager();

            return prcm.GetClient();
        }

    }
}
