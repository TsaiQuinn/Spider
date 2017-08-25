#region ----------------备注----------------

// Author:CK 
// FileName:SpiderRequest.cs 
// Create Date:2017-08-22
// Create Time:15:29 

#endregion

using System.Net;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;

namespace SpiderCommon
{
    public static class SpiderRequest
    {
        /// <summary>
        /// 下载，返回字符
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Task<string> DownloadString(string url)
        {
            return url.GetStringAsync();
        }
    }
}