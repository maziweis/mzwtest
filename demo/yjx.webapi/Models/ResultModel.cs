using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace yjx.webapi.Models
{
    /// <summary>
    /// 公共返回值
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public int ErrCode { get; set; }
        /// <summary>
        /// 返回消息信息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 公共返回值
    /// </summary>
    public class ResultModel<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 成功码
        /// </summary>
        public int SuccessCode { get; set; }
        /// <summary>
        /// 返回数据信息
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// 返回消息信息
        /// </summary>
        public string Message { get; set; }
    }
}
