#region ----------------备注----------------

// Author:TQ 
// FileName:IView.cs 
// Create Date:2017-06-15
// Create Time:15:52 

#endregion

using System;

namespace SpiderIView
{
    public interface IView
    {
        /// <summary>
        ///     加载窗体
        /// </summary>
        event EventHandler Load;

        /// <summary>
        ///     关闭
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        ///     提示消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        void ShowMessage(string msg, string title);
    }
}