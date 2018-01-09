using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace yjx.core.common.Dict
{
    public class Term
    {
        public class TermModel
        {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public string BreeName { get; set; }
        }

        private static List<TermModel> d = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        static Term()
        {
            d = new List<TermModel>();
            d.Add(new TermModel { StartTime = DateTime.Parse("2016-02-08 00:00:00.000"), EndTime = DateTime.Parse("2016-08-31 00:00:00.000"), BreeName = "上" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2016-09-01 00:00:00.000"), EndTime = DateTime.Parse("2017-01-27 00:00:00.000"), BreeName = "上" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2017-01-28 00:00:00.000"), EndTime = DateTime.Parse("2017-08-31 00:00:00.000"), BreeName = "下" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2017-09-01 00:00:00.000"), EndTime = DateTime.Parse("2018-02-15 00:00:00.000"), BreeName = "上" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2018-02-16 00:00:00.000"), EndTime = DateTime.Parse("2018-08-31 00:00:00.000"), BreeName = "下" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2018-09-01 00:00:00.000"), EndTime = DateTime.Parse("2019-02-04 00:00:00.000"), BreeName = "上" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2019-02-05 00:00:00.000"), EndTime = DateTime.Parse("2019-08-31 00:00:00.000"), BreeName = "下" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2019-09-01 00:00:00.000"), EndTime = DateTime.Parse("2020-01-24 00:00:00.000"), BreeName = "上" });
            d.Add(new TermModel { StartTime = DateTime.Parse("2020-01-25 00:00:00.000"), EndTime = DateTime.Parse("2020-08-31 00:00:00.000"), BreeName = "下" });
        }

        /// <summary>
        /// 获取Value
        /// </summary>
        /// <returns></returns>
        public static string GetVal()
        {
            var now = DateTime.Now;
            return d.Where(w => now > w.StartTime && now <= w.EndTime).Select(s => s.BreeName).FirstOrDefault();
        }
    }
}
