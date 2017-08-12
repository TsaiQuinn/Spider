#region ----------------备注----------------

// Author:CK 
// FileName:ICrawlForm.cs 
// Create Date:2017-08-02
// Create Time:14:33 

#endregion

using System;
using SpiderModel.Models;

namespace SpiderIView
{
    public interface ICrawlView : IView
    {
        /// <summary>
        ///     采集品牌
        /// </summary>
        event EventHandler<CarBrandEventArgs> PickBrand;
    }
}