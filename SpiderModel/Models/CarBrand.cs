#region ----------------备注----------------

// Author:TQ 
// FileName:CarBrand.cs 
// Create Date:2017-06-10
// Create Time:15:42 

#endregion

using System;

namespace SpiderModel.Models
{
    public class CarBrand
    {
        /// <summary>
        ///     主键ID
        /// </summary>
        public virtual int? Id { get; set; }

        /// <summary>
        ///     品牌链接地址
        /// </summary>
        public virtual string Url { get; set; }

        /// <summary>
        ///     品牌名称
        /// </summary>
        public virtual string BrandName { get; set; }

        /// <summary>
        ///     品牌logo地址
        /// </summary>
        public virtual string BrandLogo { get; set; }

        /// <summary>
        ///     品牌TAG
        /// </summary>
        public virtual string TagName { get; set; }

        /// <summary>
        ///     添加时间
        /// </summary>
        public virtual DateTime? AddTime { get; set; }
    }
}