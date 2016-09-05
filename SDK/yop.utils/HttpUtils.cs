using System;
using System.Collections.Generic;
using System.Text;

namespace SDK.yop.utils
{
  using System.Net;
  using System.IO;
  using client;
  using System.Collections.Specialized;
  public class HttpUtils
  {
    public static HttpWebResponse PostAndGetHttpWebResponse(YopRequest yopRequest, string method)//(string targetUrl, string param, string method, int timeOut)
    {
      try
      {
        string targetUrl = yopRequest.getAbsoluteURL();//请求地址
        CookieContainer cc = new CookieContainer();
        string param = yopRequest.toQueryString();//请求参数
        byte[] data = Encoding.GetEncoding("UTF-8").GetBytes(param);
        if (method.ToUpper() == "GET") targetUrl = targetUrl + (param.Length == 0 ? "" : ("?" + param));

        // 2.0 https证书无效解决方法
        ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
        // 1.1 https证书无效解决方法
        ServicePointManager.CertificatePolicy = new AcceptAllCertificatePolicy();

        System.GC.Collect();//垃圾回收
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(targetUrl);
        request.Timeout = yopRequest.getReadTimeout();
        request.Method = method.ToUpper();

        request.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, */*";
        request.ContentType = "application/x-www-form-urlencoded";
        request.ContentLength = method.ToUpper().Trim() == "POST" ? data.Length : 0;
        request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729)";
        //request.Referer = refererUrl;
        request.CookieContainer = cc;
        request.ServicePoint.Expect100Continue = false;
        request.ServicePoint.ConnectionLimit = 10000;
        request.AllowAutoRedirect = true;
        request.ProtocolVersion = HttpVersion.Version10; //尝试解决基础链接已关闭问题
        request.KeepAlive = false;//尝试解决基础链接已关闭问题 有可能影响证书问题

        if (method.ToUpper() == "POST")
        {
          Stream newStream = request.GetRequestStream();
          newStream.Write(data, 0, data.Length);
          newStream.Close();
        }

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        response.Cookies = cc.GetCookies(request.RequestUri);
        return response;
      }
      catch (WebException ex)
      {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return (HttpWebResponse)ex.Response;
      }
    }

    /// <summary>
    /// 解决证书问题 不管证书有效否，直接返回有效
    /// </summary>
    internal class AcceptAllCertificatePolicy : ICertificatePolicy
    {
      public bool CheckValidationResult(ServicePoint sPoint, System.Security.Cryptography.X509Certificates.X509Certificate cert, WebRequest wRequest, int certProb)
      {
        return true;
      }
    }

    /// <summary>
    /// 解决证书问题 不管证书有效否，直接返回有效
    /// </summary>
    /// <param name= "sender" ></param>
    /// <param name= "certificate" ></param>
    /// <param name= "chain" ></param>
    /// <param name= "errors" ></param>
    /// <returns></returns>
    public static bool CheckValidationResult(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors errors)
    {
      return true;
    }

    public static HttpWebResponse PostFile(YopRequest yopRequest, IEnumerable<UploadFile> files)
    {
      string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
      HttpWebRequest request = (HttpWebRequest)WebRequest.Create(yopRequest.getAbsoluteURL());
      request.ContentType = "multipart/form-data; boundary=" + boundary;
      request.Headers.Add("Request-Id", UUIDGenerator.generate());
      request.Method = "POST";
      request.KeepAlive = true;
      request.Credentials = CredentialCache.DefaultCredentials;

      MemoryStream stream = new MemoryStream();

      byte[] line = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
      NameValueCollection values = yopRequest.getParams();
      //提交文本字段
      if (values != null)
      {
        string format = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";
        foreach (string key in values.Keys)
        {
          string s = string.Format(format, key, values[key]);
          byte[] data = Encoding.UTF8.GetBytes(s);
          stream.Write(data, 0, data.Length);
        }
        stream.Write(line, 0, line.Length);
      }

      line = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
      //提交文件
      if (files != null)
      {
        string fformat = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n";
        foreach (UploadFile file in files)
        {
          string s = string.Format(fformat, file.Name, file.Filename);
          byte[] data = Encoding.UTF8.GetBytes(s);
          stream.Write(data, 0, data.Length);

          stream.Write(file.Data, 0, file.Data.Length);
          stream.Write(line, 0, line.Length);
        }
      }


      request.ContentLength = stream.Length;


      Stream requestStream = request.GetRequestStream();

      stream.Position = 0L;
      stream.WriteTo(requestStream);
      stream.Close();

      requestStream.Close();
      HttpWebResponse response = (HttpWebResponse)request.GetResponse();
      return response;
    }


  }
}
