#region ----------------备注----------------

// Author:CK 
// FileName:SpiderFile.cs 
// Create Date:2017-09-04
// Create Time:17:58 

#endregion

using System;
using System.IO;
using System.Threading.Tasks;
using Flurl.Http;

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
        /// <param name="func">下载完后执行的回调</param>
        public static void DownImage(string url, string diskPath, string dataBasePath, Func<string, int> func)
        {
            var indexOf = url.LastIndexOf("/", StringComparison.Ordinal);
            string logo = url.Substring(indexOf + 1);
            var exist = File.Exists(diskPath + logo);
            if (!exist || File.ReadAllBytes(diskPath + logo).Length == 0)
            {
                if (exist)
                {
                    File.Delete(diskPath + logo);
                }
                Task.Run(async () => await url.DownloadFileAsync(diskPath)).ContinueWith(
                    downImageTask => { func?.Invoke(dataBasePath + logo); }).LogExceptions();
            }
            else
            {
                func?.Invoke(dataBasePath + logo);
            }
        }
    }
}