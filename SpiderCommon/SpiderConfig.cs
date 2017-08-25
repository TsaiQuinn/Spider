#region ----------------备注----------------

// Author:CK 
// FileName:SpiderConfig.cs 
// Create Date:2017-08-22
// Create Time:16:04 

#endregion

using System.Configuration;

namespace SpiderCommon
{
    public static class SpiderConfig
    {
        /// <summary>
        /// 根据键获取值
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public static string GetConfigValue(string keyName)
        {
            return ConfigurationManager.AppSettings[keyName];
        }
    }
}