#region ----------------备注----------------

// Author:CK 
// FileName:ICrawlForm.cs 
// Create Date:2017-08-02
// Create Time:14:33 

#endregion

using SpiderModel.Models;
using System;

namespace SpiderIView
{
    public interface ICrawlView : IView
    {
        /// <summary>
        ///     采集品牌
        /// </summary>
        event EventHandler PickBrandEvent;

        /// <summary>
        /// 采集车型
        /// </summary>
        event EventHandler PickModelEvent;

        /// <summary>
        /// 界面显示
        /// </summary>
        Action<ViewModelEventArg> ShowAction { get; set; } 
    }
}