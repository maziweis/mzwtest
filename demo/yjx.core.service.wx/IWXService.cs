using System;
using System.Collections.Generic;
using System.Text;

namespace yjx.core.service.wx
{
    public interface IWXService
    {
        void Send(List<string> featherOpenid, string templateid, object data, string url);
        
    }
}
