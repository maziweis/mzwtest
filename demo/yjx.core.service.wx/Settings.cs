using System;
using System.Collections.Generic;
using System.Text;

namespace yjx.core.service.wx
{
    public class Settings
    {
        /// <summary>
        /// 微信ID
        /// </summary>
        public string DevlopID { get; set; }
        /// <summary>
        /// 微信模版
        /// </summary>
        public string AppSecret { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WeixinAgentUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string WeixinAgentToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YouzuoyeUrl { get; set; }
        /// <summary>
        /// 每日向家长发送孩子的作业推送
        /// </summary>
        public string SendTextInformestemplateid { get; set; } 
        /// <summary>
        /// 学生作业完成提醒
        /// </summary>
        public string HomeWorkInformtemplateid { get; set; }  
        /// <summary>
        /// 班级提醒
        /// </summary>
        public string ClassInformtemplateid { get; set; } 
        /// <summary>
        /// 收到预习任务
        /// </summary>
        public string ReceivePreviewTasktemplateid { get; set; } 
        /// <summary>
        /// 预习任务完成
        /// </summary>
        public string SendPreviewTasktemplateid { get; set; }  
    }
}
