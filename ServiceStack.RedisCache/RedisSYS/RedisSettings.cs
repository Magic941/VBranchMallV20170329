using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStack.RedisCache
{
    public partial class RedisSettings
    {
        public RedisSettings()
        {
        }
        public string WriteServerList { get; set; }
        public string ReadServerList { get; set; }
        public int MaxWritePoolSize { get; set; }
        public int MaxReadPoolSize { get; set; }
        public bool AutoStart { get; set; }

        public bool IsValid()
        {
            return !String.IsNullOrEmpty(this.WriteServerList) && !String.IsNullOrEmpty(this.ReadServerList) && this.MaxWritePoolSize > 0 && this.MaxReadPoolSize>0;
        }
    }
}
