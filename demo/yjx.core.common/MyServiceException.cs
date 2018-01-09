using System;
using System.Collections.Generic;
using System.Text;

namespace yjx.core.common
{
    public class MyServiceException : ApplicationException
    {
        //带一个字符串参数的构造函数，作用：当程序员用Exception类获取异常信息而非 MyException时把自定义异常信息传递过去
        public MyServiceException(string msg) : base(msg)
        {

        }
    }
}
