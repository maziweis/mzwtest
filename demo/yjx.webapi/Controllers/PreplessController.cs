using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using yjx.webapi.Models;
using yjx.service.prepless;
using yjx.core.service.modold;

namespace yjx.webapi.Controllers
{
    /// <summary>
    /// 备课
    /// </summary>
    [Produces("application/json")]
    [Route("api/Prepless")]
    public class PreplessController : Controller
    {
        private readonly IPreplessService preplessService;
        private readonly IResourceService resourceService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="preplessService"></param>
        /// <param name="resourceService"></param>
        public PreplessController(IPreplessService preplessService, IResourceService resourceService)
        {
            this.preplessService = preplessService;
            this.resourceService = resourceService;
        }

        /// <summary>
        /// 【测试接口】获取系统时间
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetDateTime")]
        public ResultModel<DateTime> GetDateTime(string id)
        {
            return new ResultModel<DateTime> { Success = true, Data = DateTime.Now };
        }

        /// <summary>
        /// 【测试接口】获取服务内容
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTest")]
        public ResultModel<string> GetTest()
        {
            return new ResultModel<string> { Success = true, Data = preplessService.test() };
        }
    }
}