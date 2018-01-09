using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yjx.core.service.wx;
using yjx.webapi.Helper;
using System.IO;
using Microsoft.Extensions.Logging;
using System.Net;

namespace yjx.webapi.Controllers
{
    /// <summary>
    /// 微信
    /// </summary>
    [Produces("application/json")]
    [Route("api/Wx")]
    public class WxController : Controller
    {
        private readonly IWXService wXService;
        private readonly ILogger<WxController> logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="wXService"></param>
        /// <param name="logger"></param>
        public WxController(IWXService wXService, ILogger<WxController> logger)
        {
            this.wXService = wXService;
            this.logger = logger;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        [HttpGet] 
        [Route("Send")]
        public void Send(List<string> featherOpenid, string templateid, object data, string url)
        {
            wXService.Send(featherOpenid, templateid, data, url);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        [HttpGet]
        [Route("GetData")]
        public void GetData(string openid,string userid)
        {
            //string logpath = Directory.GetCurrentDirectory();//F:\我的工作目录\demo\yjx.webapi
            //var filePath = System.IO.Path.Combine(logpath, "wxlog.txt");
            //Directory.CreateDirectory(filePath);
            logger.LogError(openid + ":" + userid);
        }
        //https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=gQEd8DwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAycl95cDRFT0FkeTIxdThXZGhxMW4AAgSA801aAwQIBwAA
        /// <summary>
        /// 
        /// </summary>
        /// <param name="openid"></param>
        /// <param name="userid"></param>
        [HttpGet]
        [Route("GetImg")]
        public void GetImg(string openid, string userid)
        {
            var prlimg = "https://mp.weixin.qq.com/cgi-bin/showqrcode?ticket=gQEd8DwAAAAAAAAAAS5odHRwOi8vd2VpeGluLnFxLmNvbS9xLzAycl95cDRFT0FkeTIxdThXZGhxMW4AAgSA801aAwQIBwAA";
            WebClient wc = new WebClient();
            byte[] b = wc.DownloadData(prlimg);
            Response.ContentType = "image/jpg";            
        }
    }
}