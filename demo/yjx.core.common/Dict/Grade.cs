using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace yjx.core.common.Dict
{
    public class Grade
    {
        private static Dictionary<int, string> d = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        static Grade()
        {
            d = new Dictionary<int, string>();
            d.Add(2, "一年级");
            d.Add(3, "二年级");
            d.Add(4, "三年级");
            d.Add(5, "四年级");
            d.Add(6, "五年级");
            d.Add(7, "六年级");
            d.Add(8, "七年级");
            d.Add(9, "八年级");
            d.Add(10, "九年级");
            d.Add(11, "高一");
            d.Add(12, "高二");
            d.Add(13, "高三");
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> Get()
        {
            return d;
        }

        /// <summary>
        /// 获取Value
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetVal(int key)
        {
            return d.Keys.Contains(key) ? d[key] : null;
        }
    }
}
