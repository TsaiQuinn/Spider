#region ----------------备注----------------

// Author:CK 
// FileName:ICrawlForm.cs 
// Create Date:2017-08-02
// Create Time:14:33 

#endregion
 
using System;
using SpiderModel;
using SpiderModel.Entity;

namespace SpiderIView
{
    public interface ICrawlView : IView
    {
        /// <summary>
        ///     采集品牌
        /// </summary>
        event EventHandler PickBrandEventHandler;

        /// <summary>
        /// 采集车型
        /// </summary>
        event EventHandler PickModelEventHandler;

        /// <summary>
        /// 界面显示
        /// </summary>
        Action<ViewModelArg<CarBrandEntity>> ShowBrandInfoAction { get; set; }

        /// <summary>
        /// 界面显示
        /// </summary>
        Action<ViewModelArg<CarModelEntity>> ShowModelInfoAction { get; set; }
        /// <summary>
        /// 界面显示
        /// </summary>
        Action<ViewModelArg<CarSeriesEntity>> ShowSeriesInfoAction { get; set; }
    }
}