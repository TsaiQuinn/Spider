#region ----------------备注----------------
// Author:CK 
// FileName:CarBrandEntity.cs 
// Create Date:2017-09-05
// Create Time:17:36 
#endregion

using System;
using SpiderModel.Models;

namespace SpiderModel.Entity
{
    public class CarBrandEntity :Car
    {  
        /// <summary>
        /// 关联ID
        /// </summary>
        public virtual int? Rid { get; set; }

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