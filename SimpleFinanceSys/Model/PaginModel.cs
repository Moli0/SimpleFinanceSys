using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Model
{
    public class PaginModel
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int NowPage { get; set; }

        public int PageIndex { get; set; }

        /// <summary>
        /// 页面大小 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public string sortStr { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        public Hashtable hs { get; set; }

        /// <summary>
        /// 数据总条数
        /// </summary>
        public int counts { get; set; }

        /// <summary>
        /// 页面总数
        /// </summary>
        public int PageTotle { get; set; }
    }
}
