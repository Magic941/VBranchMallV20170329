using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using System.Threading.Tasks;

namespace ServiceStack.RedisCache
{
    public partial class RedisSettingsManager
    {
        protected const char separator = ':';
        protected const string filename = "Settings.txt";

        /// <summary>
        /// Maps a virtual path to a physical disk path.
        /// </summary>
        /// <param name="path">The path to map. E.g. "~/bin"</param>
        /// <returns>The physical path. E.g. "c:\inetpub\wwwroot\bin"</returns>
        protected virtual string MapPath(string path)
        {
            if (HostingEnvironment.IsHosted)
            {
                //hosted
                return HostingEnvironment.MapPath(path);
            }
            else
            {
                //not hosted. For example, run in unit tests
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
                return Path.Combine(baseDirectory, path);
            }
        }

        protected virtual RedisSettings ParseSettings(string text)
        {
            var shellSettings = new RedisSettings();
            if (String.IsNullOrEmpty(text))
                return shellSettings;

            //Old way of file reading. This leads to unexpected behavior when a user's FTP program transfers these files as ASCII (\r\n becomes \n).
            //var settings = text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var settings = new List<string>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                    settings.Add(str);
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(separator);
                if (separatorIndex == -1)
                {
                    continue;
                }
                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                switch (key)
                {
                    case "WriteServerList":
                        shellSettings.WriteServerList = value;
                        break;
                    case "ReadServerList":
                        shellSettings.ReadServerList = value;
                        break;
                    case "MaxWritePoolSize":
                        shellSettings.MaxWritePoolSize = Convert.ToInt32(value);
                        break;
                    case "MaxReadPoolSize":
                        shellSettings.MaxReadPoolSize = Convert.ToInt32(value);
                        break;
                    case "AutoStart":
                        shellSettings.AutoStart = Convert.ToBoolean(value);
                        break;
                    default:
                        shellSettings.MaxWritePoolSize = Convert.ToInt32(value);
                        break;
                }
            }

            return shellSettings;
        }

        protected virtual string ComposeSettings(RedisSettings settings)
        {
            if (settings == null)
                return "";

            return string.Format("RedisSettings: {1}{2}{3}{4}{5}",
                                 settings.WriteServerList,
                                 settings.ReadServerList,
                                 settings.MaxReadPoolSize,
                                 settings.MaxWritePoolSize,
                                 settings.AutoStart,
                                 Environment.NewLine
                );
        }

        /// <summary>
        /// Load settings
        /// </summary>
        /// <param name="filePath">File path; pass null to use default settings file path</param>
        /// <returns></returns>
        public virtual RedisSettings LoadSettings(string filePath = null)
        {
            if (String.IsNullOrEmpty(filePath))
            {
                //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
                filePath = Path.Combine(MapPath("~/App_Data/"), filename);
            }
            if (File.Exists(filePath))
            {
                string text = File.ReadAllText(filePath);
                return ParseSettings(text);
            }
            else
                return new RedisSettings();
        }

        public virtual void SaveSettings(RedisSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            //use webHelper.MapPath instead of HostingEnvironment.MapPath which is not available in unit tests
            string filePath = Path.Combine(MapPath("~/"), filename);
            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {
                    //we use 'using' to close the file after it's created
                }
            }

            var text = ComposeSettings(settings);
            File.WriteAllText(filePath, text);
        }
    }
}
