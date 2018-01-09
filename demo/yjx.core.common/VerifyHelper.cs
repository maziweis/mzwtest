using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace yjx.core.common
{
    /// <summary>
    /// 验证
    /// </summary>
    public static class VerifyHelper
    {
        /// <summary>
        /// 检测是否为有效的手机号码
        /// </summary>
        /// <param name="phone">手机号</param>
        /// <returns></returns>
        public static bool IsPhone(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return false;

            Regex rcode = new Regex(@"^[1][3-8]+\d{9}$");
            if (!rcode.IsMatch(phone))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
