﻿using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Netnr.Data
{
    public class DataBase
    {
        /// <summary>
        /// Global configuration
        /// </summary>
        public static IConfiguration Configuration;

        /// <summary>
        /// 数据库
        /// </summary>
        public enum TypeDB
        {
            SQLServer = 1,
            SQLite = 2
        }

        /// <summary>
        /// 项目属性常量
        /// </summary>
        public class ProjectAttr
        {
            public const string Domain = "Netnr.Domain";
            public const string Mapping = "Netnr.Mapping";
        }

        /// <summary>
        /// 分页参数
        /// </summary>
        public class Pagination
        {
            /// <summary>
            /// 页码
            /// </summary>
            public int PageNumber { get; set; }
            /// <summary>
            /// 页量
            /// </summary>
            public int PageSize { get; set; }
            /// <summary>
            /// 总数量
            /// </summary>
            public int Total { get; set; }
            /// <summary>
            /// 排序字段，英文逗号分隔
            /// </summary>
            public string SortName { get; set; }
            /// <summary>
            /// 排序类型，英文逗号分隔
            /// </summary>
            public string SortOrder { get; set; }
            /// <summary>
            /// 总页数
            /// </summary>
            public int PageTotal
            {
                get
                {
                    int pt = 0;
                    try
                    {
                        pt = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Total) / Convert.ToDecimal(PageSize)));
                    }
                    catch (Exception)
                    {
                    }
                    return pt;
                }
            }
        }

        /// <summary>
        /// 排序拼接
        /// </summary>
        /// <param name="sortName">排序字段，支持多个，逗号分隔</param>
        /// <param name="sortOrder">排序模式，支持多个，逗号分隔</param>
        /// <param name="bracket">排序字段带方括号 默认是</param>
        /// <returns></returns>
        public static string OrderByJoin(string sortName, string sortOrder, bool bracket = true)
        {
            string result = string.Empty;

            var sortlist = sortName.Split(',').ToList();
            var orderlist = sortOrder.Split(',').ToList();
            if (sortlist.Count == orderlist.Count)
            {
                for (int i = 0; i < sortlist.Count; i++)
                {
                    string sortField = sortlist[i].Trim().Replace("'", "").Replace(";", "").Replace(" ", "").Replace("[", "").Replace("]", "");
                    if (bracket)
                    {
                        sortField = "[" + sortField + "]";
                    }
                    string orderType = orderlist[i].ToLower().Trim() == "asc" ? "asc" : "desc";
                    result += sortField + " " + orderType + ",";
                }
            }

            return result.TrimEnd(',');
        }
    }
}

