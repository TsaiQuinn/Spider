#region ----------------备注----------------
// Author:CK 
// FileName:CarModelEntity.cs 
// Create Date:2017-09-06
// Create Time:11:18 
#endregion

using System; 

namespace SpiderModel.Entity
{
    public class CarModelEntity : Car
    {
        /// <summary>
        /// 主键
        /// </summary>
//        public virtual int Id { get; set; }
        /// <summary>
        /// 品牌ID
        /// </summary>
        public virtual int BrandId { get; set; }

        /// <summary>
        ///     车系名称
        /// </summary>
        public virtual string ModelName { get; set; }

        /// <summary>
        ///     车系链接地址
        /// </summary>
        public virtual string LinkUrl { get; set; }

        /// <summary>
        ///     厂家
        /// </summary>
        public virtual string Factory { get; set; }

        /// <summary>
        ///     车系logo地址
        /// </summary>
        public virtual string ImagePath { get; set; }
        /// <summary>
        ///    
        /// </summary>
        public virtual int LevelId { get; set; }
        /// <summary>
        ///     全国参考价
        /// </summary>
        public virtual string NationalReferencePrice { get; set; }
        /// <summary>
        ///     厂商指导价
        /// </summary>
        public virtual string Msrp { get; set; }
        /// <summary>
        ///     二手车
        /// </summary>
        public virtual string SecondHand { get; set; }
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
        ///    保修
        /// </summary>
        public virtual string Warranty { get; set; }
        /// <summary>
        ///    颜色链接
        /// </summary>
        public virtual string ColorLink { get; set; }
        /// <summary>
        ///  添加时间      
        /// </summary>
        public virtual DateTime? AddTime { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public virtual string ImageUrl { get; set; }
    }
}