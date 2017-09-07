#region ----------------备注----------------

// Author:CK 
// FileName:ViewModelArg.cs 
// Create Date:2017-09-06
// Create Time:15:01 

#endregion

using System;

namespace SpiderModel
{
    public class ViewModelArg<T> : EventArgs where T : class
    {
        public int Type { get; set; }

        public T Car { get; set; }
    }
}