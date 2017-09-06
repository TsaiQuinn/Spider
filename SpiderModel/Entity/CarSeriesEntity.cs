#region ----------------备注----------------
// Author:CK 
// FileName:CarSeriesEntity.cs 
// Create Date:2017-09-06
// Create Time:11:19 
#endregion

using System;
using SpiderModel.Models;

namespace SpiderModel.Entity
{
    public class CarSeriesEntity : Car
    { 
        /// <summary>
        ///     车型ID
        /// </summary>
        public virtual int ModelId { get; set; }
        /// <summary>
        ///     车系名称
        /// </summary>
        public virtual string SeriesName { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public virtual string LinkUrl { get; set; }
        /// <summary>
        ///     全国参考价
        /// </summary>
        public virtual string ReferencePrice { get; set; }
        /// <summary>
        ///     厂商指导价
        /// </summary>
        public virtual string Msrp { get; set; }
        /// <summary>
        /// 全款购车参考价格
        /// </summary>
        public virtual string FullPaymentReference { get; set; }
        /// <summary>
        ///     排    量
        /// </summary>
        public virtual string Displacement { get; set; }
        /// <summary>
        ///    油耗
        /// </summary>
        public virtual string FuelConsumption { get; set; }
        /// <summary>
        ///    变速箱
        /// </summary>
        public virtual string Gearbox { get; set; }
        /// <summary>
        ///    发动机
        /// </summary>
        public virtual string Engine { get; set; }
        /// <summary>
        ///  添加时间      
        /// </summary>
        public virtual DateTime? AddTime { get; set; }
    }
}