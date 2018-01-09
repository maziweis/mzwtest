using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Http;

namespace yjx.core.common
{
    public static class FunctionHelper
    {
        /// <summary>
        /// 给一个字符串进行MD5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string value)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("X2"));
            }

            return sBuilder.ToString().Substring(8, 16);
        }

        /// <summary>
        /// 获取请求ip
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetRequestIp(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            return ip;
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="WebRootPath"></param>
        public static void WriteLog(string content, string WebRootPath)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + ".log";
            string fileContent = content;
            System.IO.FileStream fs = null;
            try
            {
                string dir = string.Format("{0}\\logs\\", WebRootPath);
                if (!System.IO.Directory.Exists(dir))
                {
                    System.IO.Directory.CreateDirectory(dir);
                }

                string fileAllName = dir + fileName;
                if (System.IO.File.Exists(fileAllName))
                {
                    fs = new System.IO.FileStream(fileAllName, System.IO.FileMode.Open, System.IO.FileAccess.Write);
                }
                else
                {
                    fs = new System.IO.FileStream(fileAllName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                }

                byte[] Bt = new byte[fileContent.Length * 2];
                Bt = System.Text.Encoding.UTF8.GetBytes(fileContent);
                fs.Position = fs.Length;
                fs.Write(Bt, 0, Bt.Length);
                fs.Close();
            }
            catch
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        /// <summary>
        /// 根据年级获取学期
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public static string GetXq(string grade)
        {
            return Dict.Grade.GetVal(int.Parse(grade)) + Dict.Term.GetVal();
        }
    }
}
