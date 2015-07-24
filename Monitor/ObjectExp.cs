using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor
{
    public static class ObjectExp
    {
        /// <summary>
        /// 字符串拼接
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> items, string split)
        {
            var index = 0;
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                if (index > 0)
                {
                    sb.Append(split);
                }
                sb.Append(item);
                index++;

            }
            return sb.ToString();
        }
    }
}
