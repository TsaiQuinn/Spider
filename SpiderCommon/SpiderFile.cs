#region ----------------备注----------------

// Author:CK 
// FileName:SpiderFile.cs 
// Create Date:2017-09-04
// Create Time:17:58 

#endregion

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SpiderCommon
{
    public static class SpiderFile
    {
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <param name="diskPath">存储的硬盘路径</param>
        /// <param name="dataBasePath">数据库中存储的路径</param> 
        public static async Task<string> DownImageAsync(string url, string diskPath, string dataBasePath)
        {
            var indexOf = url.LastIndexOf("/", StringComparison.Ordinal);
            string logo = url.Substring(indexOf + 1);
            var exist = File.Exists(diskPath + logo);
            if (exist)
            {
                int size = File.ReadAllBytes(diskPath + logo).Length;
                if (size < 1024)
                {
                    File.Delete(diskPath + logo);
                }
                else
                {
                    return dataBasePath + logo;
                }
            }
            if (!Directory.Exists(diskPath))
            {
                Directory.CreateDirectory(diskPath);
            }
            var length = 0;
            HttpClient client = new HttpClient();
            do
            { 
                var resultBytes = await client.GetByteArrayAsync(url);
                if (resultBytes != null)
                {
                    FileStream fs = new FileStream(diskPath+ logo, FileMode.Create);
                    BinaryWriter bw = new BinaryWriter(fs);
                    bw.Write(resultBytes);
                    bw.Close();
                    fs.Close(); 
                }
                length = File.ReadAllBytes(diskPath + logo).Length;
            } while (length < 1024);
            return dataBasePath + logo;
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url">图片地址</param>
        /// <param name="diskPath">保存路径</param>
        /// <param name="tryTimes">次数</param>
        /// <returns></returns>
        public static string DownImage(string url, string diskPath, int tryTimes = 3)
        {
            if (!Directory.Exists(diskPath))
            {
                Directory.CreateDirectory(diskPath);
            }
            WebClient client = new WebClient();
            bool flag = true;
            var times = 0;
            while (flag)
            {
                times++;
                try
                {
                    var resultBytes = client.DownloadData(new Uri(url));
                    if (resultBytes != null)
                    {
                        FileStream fs = new FileStream(diskPath, FileMode.Create);
                        BinaryWriter bw = new BinaryWriter(fs);
                        bw.Write(resultBytes);
                        bw.Close();
                        fs.Close();
                        flag = false;
                    }
                    else
                    {
                        if (times == tryTimes)
                        {
                            flag = false;
                        }
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    flag = times != tryTimes;
                }
            }
            return diskPath;
        }
        /// <summary>
        /// 获取URL内容
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public static async Task<string> GetStringAsync(string url)
        {
            var client=new HttpClient(); 
            return await client.GetStringAsync(new Uri(url));
        }

        /// <summary>
        /// 获取URL内容
        /// </summary>
        /// <param name="url">地址</param>
        /// <returns></returns>
        public static async Task<Stream> GetStreamAsync(string url)
        {
            var client = new HttpClient();
            return await client.GetStreamAsync(new Uri(url));
        }
    }
}