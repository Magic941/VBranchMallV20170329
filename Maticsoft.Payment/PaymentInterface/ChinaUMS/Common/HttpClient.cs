using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace GL.Payment.Common
{
    /// <summary>
    /// HttpClient操作类
    /// 创建用户：shiyuankao
    /// 创建时间：2014-04-12
    /// 引用来源：http://blog.csdn.net/dieindark/article/details/3930595
    /// </summary>
    public class HttpClient
    {
        #region fields

        private bool _keepContext;

        private string _defaultLanguage = "zh-CN";

        private Encoding _defaultEncoding = Encoding.UTF8;

        private string _accept = "*/*";

        private string _userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        private HttpVerb _verb = HttpVerb.GET;

        private HttpClientContext _context;

        private readonly List<HttpUploadingFile> _files = new List<HttpUploadingFile>();

        private Dictionary<string, string> _postingData = new Dictionary<string, string>();

        private string _url;

        /// <summary>
        /// 
        /// </summary>
        private WebHeaderCollection _requestHeaders;

        private WebHeaderCollection _responseHeaders;

        private int _startPoint;

        private int _endPoint;

        #endregion

        #region events

        public event EventHandler<StatusUpdateEventArgs> StatusUpdate;

        private void OnStatusUpdate(StatusUpdateEventArgs e)
        {
            EventHandler<StatusUpdateEventArgs> temp = StatusUpdate;

            if (temp != null)
                temp(this, e);
        }

        #endregion

        #region properties

        /// <summary>
        /// 是否自动在不同的请求间保留Cookie, Referer
        /// </summary>
        public bool KeepContext
        {
            get { return _keepContext; }
            set { _keepContext = value; }
        }

        /// <summary>
        /// 期望的回应的语言
        /// </summary>
        public string DefaultLanguage
        {
            get { return _defaultLanguage; }
            set { _defaultLanguage = value; }
        }

        /// <summary>
        /// GetString()如果不能从HTTP头或Meta标签中获取编码信息,则使用此编码来获取字符串
        /// </summary>
        public Encoding DefaultEncoding
        {
            get { return _defaultEncoding; }
            set { _defaultEncoding = value; }
        }

        /// <summary>
        /// 指示发出Get请求还是Post请求
        /// </summary>
        public HttpVerb Verb
        {
            get { return _verb; }
            set { _verb = value; }
        }

        /// <summary>
        /// 要上传的文件.如果不为空则自动转为Post请求
        /// </summary>
        public List<HttpUploadingFile> Files
        {
            get { return _files; }
        }

        /// <summary>
        /// 要发送的Form表单信息
        /// </summary>
        public Dictionary<string, string> PostingData
        {
            get { return _postingData; }
            set { _postingData = value; }
        }

        /// <summary>
        /// 获取或设置请求资源的地址
        /// </summary>
        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        /// <summary>
        /// HTTP头
        /// </summary>
        public WebHeaderCollection RequestHeaders
        {
            get { return _requestHeaders ?? (_requestHeaders = new WebHeaderCollection()); }
        }

        /// <summary>
        /// 用于在获取回应后,暂时记录回应的HTTP头
        /// </summary>
        public WebHeaderCollection ResponseHeaders
        {
            get { return _responseHeaders; }
        }

        /// <summary>
        /// 获取或设置期望的资源类型
        /// </summary>
        public string Accept
        {
            get { return _accept; }
            set { _accept = value; }
        }

        /// <summary>
        /// 获取或设置请求中的Http头User-Agent的值
        /// </summary>
        public string UserAgent
        {
            get { return _userAgent; }
            set { _userAgent = value; }
        }

        /// <summary>
        /// 获取或设置Cookie及Referer
        /// </summary>
        public HttpClientContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        /// <summary>
        /// 获取或设置获取内容的起始点,用于断点续传,多线程下载等
        /// </summary>
        public int StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; }
        }

        /// <summary>
        /// 获取或设置获取内容的结束点,用于断点续传,多下程下载等.
        /// 如果为0,表示获取资源从StartPoint开始的剩余内容
        /// </summary>
        public int EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; }
        }

        #endregion

        #region constructors

        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        public HttpClient()
            : this(null)
        {
        }

        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        /// <param name="url">要获取的资源的地址</param>
        public HttpClient(string url)
            : this(url, null)
        {
        }

        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        /// <param name="url">要获取的资源的地址</param>
        /// <param name="context">Cookie及Referer</param>
        public HttpClient(string url, HttpClientContext context)
            : this(url, context, false)
        {
        }

        /// <summary>
        /// 构造新的HttpClient实例
        /// </summary>
        /// <param name="url">要获取的资源的地址</param>
        /// <param name="context">Cookie及Referer</param>
        /// <param name="keepContext">是否自动在不同的请求间保留Cookie, Referer</param>
        public HttpClient(string url, HttpClientContext context, bool keepContext)
        {
            this._url = url;
            this._context = context;
            this._keepContext = keepContext;
            if (this._context == null)
                this._context = new HttpClientContext();
        }

        #endregion

        #region AttachFile

        /// <summary>
        /// 在请求中添加要上传的文件
        /// </summary>
        /// <param name="fileName">要上传的文件路径</param>
        /// <param name="fieldName">文件字段的名称(相当于&lt;input type=file name=fieldName&gt;)里的fieldName)</param>
        public void AttachFile(string fileName, string fieldName)
        {
            var file = new HttpUploadingFile(fileName, fieldName);
            _files.Add(file);
        }

        /// <summary>
        /// 在请求中添加要上传的文件
        /// </summary>
        /// <param name="data">要上传的文件内容</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fieldName">文件字段的名称(相当于&lt;input type=file name=fieldName&gt;)里的fieldName)</param>
        public void AttachFile(byte[] data, string fileName, string fieldName)
        {
            var file = new HttpUploadingFile(data, fileName, fieldName);
            _files.Add(file);
        }

        #endregion

        /// <summary>
        /// 清空PostingData, Files, StartPoint, EndPoint, ResponseHeaders, 并把Verb设置为Get.
        /// 在发出一个包含上述信息的请求后,必须调用此方法或手工设置相应属性以使下一次请求不会受到影响.
        /// </summary>
        public void Reset()
        {
            _verb = HttpVerb.GET;
            _files.Clear();
            _postingData.Clear();
            _responseHeaders = null;
            _startPoint = 0;
            _endPoint = 0;
        }

        private HttpWebRequest CreateRequest()
        {
            //ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            var req = (HttpWebRequest)WebRequest.Create(_url);
            req.AllowAutoRedirect = false;
            req.CookieContainer = new CookieContainer();
            req.Headers.Add("Accept-Language", _defaultLanguage);
            foreach (var header in RequestHeaders.AllKeys)
            {
                req.Headers.Add(header, RequestHeaders[header]);
            }
            req.Accept = _accept;
            req.UserAgent = _userAgent;
            req.KeepAlive = false;

            if (_context.Cookies != null)
                req.CookieContainer.Add(_context.Cookies);
            if (!string.IsNullOrEmpty(_context.Referer))
                req.Referer = _context.Referer;

            if (_verb == HttpVerb.HEAD)
            {
                req.Method = "HEAD";
                return req;
            }
            if (_verb == HttpVerb.DELETE)
            {
                req.Method = "DELETE";
                return req;
            }

            if ((_postingData.Count > 0 || _files.Count > 0) && _verb != HttpVerb.PUT)
                _verb = HttpVerb.POST;

            if (_verb == HttpVerb.POST)
            {
                req.Method = "POST";

                var memoryStream = new MemoryStream();
                var writer = new StreamWriter(memoryStream);

                if (_files.Count > 0)
                {
                    const string newLine = "\r\n";
                    var boundary = Guid.NewGuid().ToString().Replace("-", "");
                    req.ContentType = "multipart/form-data; boundary=" + boundary;

                    foreach (var key in _postingData.Keys)
                    {
                        writer.Write("--" + boundary + newLine);
                        writer.Write("Content-Disposition: form-data; name=\"{0}\"{1}{1}", key, newLine);
                        writer.Write(_postingData[key] + newLine);
                    }

                    foreach (var file in _files)
                    {
                        writer.Write("--" + boundary + newLine);
                        writer.Write("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.FieldName, file.FileName, newLine);
                        writer.Write("Content-Type: application/octet-stream" + newLine + newLine);
                        writer.Flush();
                        memoryStream.Write(file.Data, 0, file.Data.Length);
                        writer.Write(newLine);
                        writer.Write("--" + boundary + newLine);
                    }
                }
                else
                {
                    req.ContentType = "application/x-www-form-urlencoded";
                    var sb = new StringBuilder();
                    foreach (var key in _postingData.Keys)
                    {
                        var value = _postingData[key];
                        sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value == null ? string.Empty : value.ToString()));
                    }
                    if (sb.Length > 0)
                        sb.Length--;
                    writer.Write(sb.ToString());
                }

                writer.Flush();

                using (var stream = req.GetRequestStream())
                {
                    memoryStream.WriteTo(stream);
                }
            }
            if (_verb == HttpVerb.PUT)
            {
                req.Method = "PUT";

                var memoryStream = new MemoryStream();
                var writer = new StreamWriter(memoryStream);
                req.ContentType = "application/x-www-form-urlencoded";
                var sb = new StringBuilder();
                foreach (var key in _postingData.Keys)
                {
                    sb.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(_postingData[key].ToString()));
                }
                if (sb.Length > 0)
                    sb.Length--;
                writer.Write(sb.ToString());
                writer.Flush();

                using (var stream = req.GetRequestStream())
                {
                    memoryStream.WriteTo(stream);
                }
            }

            if (_startPoint != 0 && _endPoint != 0)
                req.AddRange(_startPoint, _endPoint);
            else if (_startPoint != 0 && _endPoint == 0)
                req.AddRange(_startPoint);

            return req;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        /// <summary>
        /// 发出一次新的请求,并返回获得的回应
        /// 调用此方法永远不会触发StatusUpdate事件.
        /// </summary>
        /// <returns>相应的HttpWebResponse</returns>
        public HttpWebResponse GetResponse()
        {
            var req = CreateRequest();
            var res = (HttpWebResponse)req.GetResponse();
            _responseHeaders = res.Headers;
            if (_keepContext)
            {
                _context.Cookies = res.Cookies;
                _context.Referer = _url;
            }
            return res;
        }

        /// <summary>
        /// 发出一次新的请求,并返回回应内容的流
        /// 调用此方法永远不会触发StatusUpdate事件.
        /// </summary>
        /// <returns>包含回应主体内容的流</returns>
        public Stream GetStream()
        {
            return GetResponse().GetResponseStream();
        }

        /// <summary>
        /// 发出一次新的请求,并以字节数组形式返回回应的内容
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <returns>包含回应主体内容的字节数组</returns>
        public byte[] GetBytes()
        {
            var res = GetResponse();
            var length = (int)res.ContentLength;

            var memoryStream = new MemoryStream();
            var buffer = new byte[0x100];
            var rs = res.GetResponseStream();
            for (var i = rs.Read(buffer, 0, buffer.Length); i > 0; i = rs.Read(buffer, 0, buffer.Length))
            {
                memoryStream.Write(buffer, 0, i);
                OnStatusUpdate(new StatusUpdateEventArgs((int)memoryStream.Length, length));
            }
            rs.Close();

            return memoryStream.ToArray();
        }

        /// <summary>
        /// 发出一次新的请求,以Http头,或Html Meta标签,或DefaultEncoding指示的编码信息对回应主体解码
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <returns>解码后的字符串</returns>
        public string GetString()
        {
            var data = GetBytes();
            var encodingName = GetEncodingFromHeaders() ?? GetEncodingFromBody(data);

            Encoding encoding;
            if (encodingName == null)
                encoding = _defaultEncoding;
            else
            {
                try
                {
                    encoding = Encoding.GetEncoding(encodingName);
                }
                catch (ArgumentException)
                {
                    encoding = _defaultEncoding;
                }
            }
            return encoding.GetString(data);
        }

        /// <summary>
        /// 发出一次新的请求,对回应的主体内容以指定的编码进行解码
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <param name="encoding">指定的编码</param>
        /// <returns>解码后的字符串</returns>
        public string GetString(Encoding encoding)
        {
            var data = GetBytes();
            return encoding.GetString(data);
        }

        /// <summary>
        /// 获取返回结果，并将结果反序列化为对象
        /// 创建用户：shiyuankao
        /// 创建时间：2014-04-12
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public T GetObject<T>() where T : class
        {
            var data = GetString();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetEncodingFromHeaders()
        {
            string encoding = null;
            var contentType = _responseHeaders["Content-Type"];
            if (contentType != null)
            {
                var i = contentType.IndexOf("charset=", System.StringComparison.Ordinal);
                if (i != -1)
                {
                    encoding = contentType.Substring(i + 8);
                }
            }
            return encoding;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetEncodingFromBody(byte[] data)
        {
            string encodingName = null;
            var dataAsAscii = Encoding.ASCII.GetString(data);
            var i = dataAsAscii.IndexOf("charset=", System.StringComparison.Ordinal);
            if (i != -1)
            {
                var j = dataAsAscii.IndexOf("\"", i, System.StringComparison.Ordinal);
                if (j != -1)
                {
                    int k = i + 8;
                    encodingName = dataAsAscii.Substring(k, (j - k) + 1);
                    var chArray = new char[2] { '>', '"' };
                    encodingName = encodingName.TrimEnd(chArray);
                }
            }
            return encodingName;
        }

        /// <summary>
        /// 发出一次新的Head请求,获取资源的长度
        /// 此请求会忽略PostingData, Files, StartPoint, EndPoint, Verb
        /// </summary>
        /// <returns>返回的资源长度</returns>
        public int HeadContentLength()
        {
            Reset();
            var lastVerb = _verb;
            _verb = HttpVerb.HEAD;
            using (var res = GetResponse())
            {
                _verb = lastVerb;
                return (int)res.ContentLength;
            }
        }

        /// <summary>
        /// 发出一次新的请求,把回应的主体内容保存到文件
        /// 调用此方法会触发StatusUpdate事件
        /// 如果指定的文件存在,它会被覆盖
        /// </summary>
        /// <param name="fileName">要保存的文件路径</param>
        public void SaveAsFile(string fileName)
        {
            SaveAsFile(fileName, FileExistsAction.Overwrite);
        }

        /// <summary>
        /// 发出一次新的请求,把回应的主体内容保存到文件
        /// 调用此方法会触发StatusUpdate事件
        /// </summary>
        /// <param name="fileName">要保存的文件路径</param>
        /// <param name="existsAction">指定的文件存在时的选项</param>
        /// <returns>是否向目标文件写入了数据</returns>
        public bool SaveAsFile(string fileName, FileExistsAction existsAction)
        {
            var data = GetBytes();
            switch (existsAction)
            {
                case FileExistsAction.Overwrite:
                    using (var writer = new BinaryWriter(new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write)))
                        writer.Write(data);
                    return true;

                case FileExistsAction.Append:
                    using (var writer = new BinaryWriter(new FileStream(fileName, FileMode.Append, FileAccess.Write)))
                        writer.Write(data);
                    return true;

                default:
                    if (!File.Exists(fileName))
                    {
                        using (var writer = new BinaryWriter(new FileStream(fileName, FileMode.Create, FileAccess.Write)))
                            writer.Write(data);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HttpClientContext
    {
        public CookieCollection Cookies { get; set; }

        public string Referer { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum HttpVerb
    {
        /// <summary>
        /// 
        /// </summary>
        GET,

        /// <summary>
        /// 
        /// </summary>
        POST,

        /// <summary>
        /// 
        /// </summary>
        HEAD,

        /// <summary>
        /// 
        /// </summary>
        PUT,

        /// <summary>
        /// 
        /// </summary>
        DELETE
    }

    /// <summary>
    /// 
    /// </summary>
    public enum FileExistsAction
    {
        /// <summary>
        /// 
        /// </summary>
        Overwrite,

        /// <summary>
        /// 
        /// </summary>
        Append,

        /// <summary>
        /// 
        /// </summary>
        Cancel,
    }

    /// <summary>
    /// 
    /// </summary>
    public class HttpUploadingFile
    {
        public string FileName { get; set; }

        public string FieldName { get; set; }

        public byte[] Data { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fieldName"></param>
        public HttpUploadingFile(string fileName, string fieldName)
        {
            this.FileName = fileName;
            this.FieldName = fieldName;
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var inBytes = new byte[stream.Length];
                stream.Read(inBytes, 0, inBytes.Length);
                Data = inBytes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        /// <param name="fieldName"></param>
        public HttpUploadingFile(byte[] data, string fileName, string fieldName)
        {
            this.Data = data;
            this.FileName = fileName;
            this.FieldName = fieldName;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class StatusUpdateEventArgs : EventArgs
    {
        private readonly int _bytesGot;
        private readonly int _bytesTotal;

        public StatusUpdateEventArgs(int got, int total)
        {
            _bytesGot = got;
            _bytesTotal = total;
        }

        /// <summary>
        /// 已经下载的字节数
        /// </summary>
        public int BytesGot
        {
            get { return _bytesGot; }
        }

        /// <summary>
        /// 资源的总字节数
        /// </summary>
        public int BytesTotal
        {
            get { return _bytesTotal; }
        }
    }

    internal class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public AcceptAllCertificatePolicy()
        {
        }

        public bool CheckValidationResult(ServicePoint sPoint,
           X509Certificate cert, WebRequest wRequest, int certProb)
        {
            // Always accept
            return true;
        }
    }
}
