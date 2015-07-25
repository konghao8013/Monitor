using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Monitor
{
    public static class ObjectExp
    {
        static JavaScriptSerializer _JavaScriptSerializer;
        /// <summary>
        /// JSON序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Json<T>(this T obj)
        {
            InitJavaScriptSerializer();
            return _JavaScriptSerializer.Serialize(obj);
        }

        private static void InitJavaScriptSerializer()
        {
            if (_JavaScriptSerializer == null)
            {
                _JavaScriptSerializer = new JavaScriptSerializer();
            }
        }
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
