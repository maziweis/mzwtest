using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using NLog.Web;
using NLog.Extensions.Logging;

namespace yjx.webapi
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });

            services.Configure<yjx.core.service.modold.Settings>(options =>
            {
                options.serviceAddressResource = Configuration.GetSection("modold:serviceAddressResource").Value;
                options.serviceAddressData = Configuration.GetSection("modold:serviceAddressData").Value;
            });

            services.Configure<yjx.service.prepless.Settings>(options =>
            {
                options.dbConnectionString_1 = Configuration.GetSection("prepless:dbConnectionString_1").Value;
                options.dbName_1 = Configuration.GetSection("prepless:dbName_1").Value;
            });

            services.Configure<yjx.core.service.wx.Settings>(options =>
            {
                options.DevlopID = Configuration.GetSection("wx:DevlopID").Value;
                options.AppSecret = Configuration.GetSection("wx:AppSecret").Value;
                options.ClassInformtemplateid = Configuration.GetSection("wx:ClassInformtemplateid").Value;
                options.HomeWorkInformtemplateid = Configuration.GetSection("wx:HomeWorkInformtemplateid").Value;
                options.ReceivePreviewTasktemplateid = Configuration.GetSection("wx:ReceivePreviewTasktemplateid").Value;
                options.SendPreviewTasktemplateid = Configuration.GetSection("wx:SendPreviewTasktemplateid").Value;
                options.SendTextInformestemplateid = Configuration.GetSection("wx:SendTextInformestemplateid").Value;
                options.Token = Configuration.GetSection("wx:Token").Value;
                options.WeixinAgentToken = Configuration.GetSection("wx:WeixinAgentToken").Value;
                options.WeixinAgentUrl = Configuration.GetSection("wx:WeixinAgentUrl").Value;
                options.YouzuoyeUrl = Configuration.GetSection("wx:YouzuoyeUrl").Value;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Title = "作业3.0API接口文档",
                        Version = "v1",
                        Description = "限用于开发接口调试.",
                        Contact = new Contact
                        {
                            Name = "魏巍",
                            Email = "68235081@qq.com"
                        }
                    }
                 );

                var filePath = System.IO.Path.Combine(Microsoft.Extensions.PlatformAbstractions.PlatformServices.Default.Application.ApplicationBasePath, "yjx.webapi.xml");
                c.IncludeXmlComments(filePath);
            });

            //services.AddSingleton<service.core.Database.IDbRepository, service.core.Database.DbRepository>();
            services.AddSingleton<core.service.modold.IResourceService, core.service.modold.ResourceService>();
            services.AddScoped<service.prepless.IPreplessService, service.prepless.PreplessService>();  
            services.AddSingleton<core.service.wx.IWXService, core.service.wx.WXService>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();//添加NLog
            env.ConfigureNLog("nlog.config");//读取Nlog配置文件

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "doc/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "doc";
                c.SwaggerEndpoint("/doc/v1/swagger.json", "API v1");
            });

            app.UseMvc();
        }
    }
}
