using System;
using System.Collections.Generic;
using System.Text;

namespace TEST
{
  using SDK.common;
  using SDK.yop;
  using SDK.yop.client;
  using SDK.yop.encrypt;

  using Org.BouncyCastle.Crypto.Engines;
  using Org.BouncyCastle.Crypto.Parameters;
  using Org.BouncyCastle.Utilities.Encoders;
  using Org.BouncyCastle.Crypto;

  using System.Security.Cryptography;
  using BlowfishNET;
  using System.Reflection;
  class Program
  {

    static void Main(string[] args)
    {
      ////1. 查询接口基于appKey授权
      //YopRequest request = new YopRequest("B112345678901237",
      //          "nUXQx0Mt0aSKvR0uNOp6kg==",
      //          "http://10.151.30.88:8064/yop-center");

      //request.setEncrypt(true);
      //request.setSignRet(true);
      //request.setSignAlg("SHA");
      //request.addParam("customerNo", "10040011444");
      //request.addParam("requestId", "124");
      //request.addParam("platformUserNo", "8880222");
      //System.Diagnostics.Debug.WriteLine(request.toQueryString());
      //YopResponse response = YopClient.post("/rest/v1.0/member/queryAccount", request);
      //Type type = response.GetType();
      //foreach (PropertyInfo p in type.GetProperties())
      //{
      //  System.Diagnostics.Debug.WriteLine(p.Name + " " + p.GetValue(response, null));
      //}
      //Console.WriteLine(response.ToString());

      ////2. 查询接口基于二代商编
      //YopRequest request = new YopRequest(null,
      //          "8intulgnqibv77f1t8q9j0hhlkiy6ei6c82sknv63vib3zhgyzl8uif9ky7a", "http://10.151.30.88:8064/yop-center");
      //request.setEncrypt(true);
      //request.setSignRet(true);
      //request.addParam("customerNo", "10040011444");
      //request.addParam("requestId", "124");
      //request.addParam("platformUserNo", "8880222");
      //System.Diagnostics.Debug.WriteLine(request.toQueryString());
      //YopResponse response = YopClient.post("/rest/v1.0/member/queryAccount", request);
      //System.Diagnostics.Debug.WriteLine(response.ToString());

      ////3. 文件上传基于appKey授权
      //YopRequest request = new YopRequest("B112345678901237",
      //          "nUXQx0Mt0aSKvR0uNOp6kg==",
      //          "http://10.151.30.87:8064/yop-center");
      //request.setEncrypt(true);
      //request.setSignRet(true);
      //request.addParam("appKey", "B112345678901237");
      //request.addParam("fileType", "IMAGE");
      //request.addParam("fileURI", @"file:\\psf\Home\Desktop\testimg.jpeg");
      //System.Diagnostics.Debug.WriteLine(request.toQueryString());
      //YopResponse response = YopClient.upload("/rest/v1.0/file/upload", request);
      //System.Diagnostics.Debug.WriteLine(response.ToString());

      ////4. 文件上传基于二代商编
      //YopRequest request = new YopRequest(null,
      //          "8intulgnqibv77f1t8q9j0hhlkiy6ei6c82sknv63vib3zhgyzl8uif9ky7a",
      //          "http://10.151.30.87:8064/yop-center");
      //request.setEncrypt(true);
      //request.setSignRet(true);
      //request.addParam("customerNo", "10040011444");
      //request.addParam("fileType", "IMAGE");
      //request.addParam("fileURI", @"file:\\psf\Home\Desktop\testimg.jpeg");
      //System.Diagnostics.Debug.WriteLine(request.toQueryString());
      //YopResponse response = YopClient.upload("/rest/v1.0/file/upload", request);
      //System.Diagnostics.Debug.WriteLine(response.ToString());


      Console.ReadLine();
    }
  }
}
