using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiderModel.Models
{
    class CarModel
    {
        /// <summary>
        ///     主键ID
        /// </summary>
        public virtual int? Id { get; set; }

        /// <summary>
        /// 品牌ID
        /// </summary>
        public virtual int? BrandId { get; set; }

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
        ///     品牌TAG
        /// </summary>
        public virtual string TagName { get; set; }

        /// <summary>
        ///     添加时间
        /// </summary>
        public virtual DateTime? AddTime { get; set; }
    }
}
