#region ----------------备注----------------
// Author:CK 
// FileName:SpiderLogger.cs 
// Create Date:2017-09-04
// Create Time:17:02 
#endregion

using System;
using System.Threading.Tasks;

namespace SpiderCommon
{
    public static class SpiderLogger
    {
        public static void LogExceptions(this Task task)
        {
            task.ContinueWith(t =>
            {
                var exceptions = t.Exception.Flatten();
                foreach (var exception in exceptions.InnerExceptions)
                {
                    Console.Write(exception);
                } 
            }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}