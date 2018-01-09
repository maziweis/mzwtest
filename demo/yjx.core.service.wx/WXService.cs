using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace yjx.core.service.wx
{
    public class WXService : IWXService
    {
        private readonly IOptions<Settings> settings;
        private const string GetBaseUserInfoApi = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";
        private const string SendMessageApi = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
        public WXService(IOptions<Settings> settings)
        {
            this.settings = settings;
        }

        /// <summary>
        /// 每日向家长发送孩子的作业推送
        /// </summary>
        /// <param name="l_featherOpenid">家长ID集合</param>
        /// <param name="templateid">模板ID</param>
        /// <param name="data">内容主体</param>
        /// <param name="url">详情地址</param>
        public void Send(List<string> l_featherOpenid, string templateid, object data, string url)
        {
            //            UserID ID  SelfID OtherID CreateDate IsSure  IsRemove Message RelationType ClassID ClassName Flag
            //1098348034  A11C1D47 - 737F - 4A44 - A4D6 - 18EF4CD06670    1098348034  ociRiwk0i48zzG7EF6s0bDTqWhBE    2017 - 12 - 27 20:34:07.373 1   0       2   NULL NULL    0
            //模板标号
            templateid = settings.Value.SendTextInformestemplateid;
            string devlopID = settings.Value.DevlopID.ToString();//微信里面开发者模式：开发者ID
            string devlogPsw = settings.Value.AppSecret.ToString();//微信里面开发者模式： 开发者密码
            foreach (var openid in l_featherOpenid)
            {
                SendTemplateMessage(templateid, openid, url, data, devlopID, devlogPsw);
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateId">模板ID</param>
        /// <param name="openId">推送用户ID</param>
        /// <param name="url">详情地址</param>
        /// <param name="data">内容主体</param>
        /// <param name="devlopID">开发者ID</param>
        /// <param name="devlogPsw">开发者密码</param>
        public static void SendTemplateMessage(string templateId, string openId, string url, object data, string devlopID, string devlogPsw)
        {
            string url_token = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + devlopID + "&secret=" + devlogPsw;
            string result1 = GetResponse(url_token);
            accessToken deserializedProduct = common.JsonHelper.DeserializeJsonToObject<accessToken>(result1);
            var accessToken = deserializedProduct.access_Token;
            var getInfoUrl = string.Format(GetBaseUserInfoApi, accessToken, openId);
            WeiXinUserInfo userInfo = GetResponse<WeiXinUserInfo>(getInfoUrl);
            //判断用户是否关注公众号
            if (userInfo.Subscribe != 0)
            {
                var sendUrl = string.Format(SendMessageApi, accessToken);
                var msg = new TemplateMessage
                {
                    template_id = templateId,
                    touser = openId,
                    url = url,
                    data = data
                };
                //序列化实体为json
                string json = common.JsonHelper.SerializeObject(msg);
                //调用消息发送接口
                PostResponse<TemplateMessageResult>(string.Format(sendUrl, accessToken), json);
            }
        }
        /// <summary>
        /// 发起post请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url">url</param>
        /// <param name="postData">post数据</param>
        /// <returns></returns>
        private static void PostResponse<T>(string url, string postData)
            where T : class, new()
        {
            HttpContent httpContent = new StringContent(postData);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpClient httpClient = new HttpClient();
            //让微信发送模板
            HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
        }

        public string GetAccessToken() //获取通行证
        {
            string devlopID = settings.Value.DevlopID.ToString();//微信里面开发者模式：开发者ID
            string devlogPsw = settings.Value.AppSecret.ToString();//微信里面开发者模式： 开发者密码
            string url_token = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + devlopID + "&secret=" + devlogPsw;
            string result = GetResponse(url_token);
            accessToken deserializedProduct = common.JsonHelper.DeserializeJsonToObject<accessToken>(result);
            string AccessToken = deserializedProduct.access_Token;
            return AccessToken;
        }
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetResponse(string url)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                return result;
            }
            return null;
        }
        public static T GetResponse<T>(string url)
            where T : class, new()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = httpClient.GetAsync(url).Result;

            T result = default(T);

            if (response.IsSuccessStatusCode)
            {
                Task<string> t = response.Content.ReadAsStringAsync();
                string s = t.Result;

                result = common.JsonHelper.DeserializeJsonToObject<T>(s);
            }
            return result;
        }       
    }
    public class accessToken
    {
        private string access_token;
        public string access_Token
        {
            get { return access_token; }
            set { access_token = value; }
        }
        private int expires_in;
        public int expires_In
        {
            get { return expires_in; }
            set { expires_in = value; }
        }
    }
    public class WeiXinUserInfo
    {
        /// <summary>
        ///  用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public int Subscribe { get; set; }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string OpenId { get; set; }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 用户所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 用户所在国家
        /// </summary>
        public string Conuntry { get; set; }
        /// <summary>
        /// 用户所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 用户的语言，简体中文为zh_CN
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空
        /// </summary>
        public string HeadImgUrl { get; set; }
        /// <summary>
        /// 用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
        /// </summary>
        public string Subscribe_Time { get; set; }
        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段
        /// </summary>
        public string UnionId { get; set; }

    }
    public class TemplateMessage
    {
        public TemplateMessage()
        {
            topcolor = "#FF0000";
        }
        /// <summary>
        /// 接收者微信OpenId
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 模板Id
        /// </summary>
        public string template_id { get; set; }
        /// <summary>
        /// 跳转url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 顶部颜色
        /// </summary>
        public string topcolor { get; set; }
        /// <summary>
        /// 具体模板数据
        /// </summary>
        public object data { get; set; }
    }
    /// <summary>
    /// 发送模板消息结果
    /// </summary>
    public class TemplateMessageResult : WxJsonResult
    {
        /// <summary>
        /// msgid
        /// </summary>
        public int msgid { get; set; }
    }
    /// <summary>
    /// JSON返回结果（用于微信响应接口等）
    /// </summary>
    public class WxJsonResult
    {
        public ReturnCode errcode { get; set; }
        public string errmsg { get; set; }
    }
    public enum ReturnCode
    {
        系统繁忙此时请开发者稍候再试 = -1,
        请求成功 = 0,
        获取access_token时AppSecret错误或者access_token无效 = 40001,
        不合法的凭证类型 = 40002,
        不合法的OpenID = 40003,
        不合法的媒体文件类型 = 40004,
        不合法的文件类型 = 40005,
        不合法的文件大小 = 40006,
        不合法的媒体文件id = 40007,
        不合法的消息类型 = 40008,
        不合法的图片文件大小 = 40009,
        不合法的语音文件大小 = 40010,
        不合法的视频文件大小 = 40011,
        不合法的缩略图文件大小 = 40012,
        不合法的APPID = 40013,
        不合法的access_token = 40014,
        不合法的菜单类型 = 40015,
        不合法的按钮个数1 = 40016,
        不合法的按钮个数2 = 40017,
        不合法的按钮名字长度 = 40018,
        不合法的按钮KEY长度 = 40019,
        不合法的按钮URL长度 = 40020,
        不合法的菜单版本号 = 40021,
        不合法的子菜单级数 = 40022,
        不合法的子菜单按钮个数 = 40023,
        不合法的子菜单按钮类型 = 40024,
        不合法的子菜单按钮名字长度 = 40025,
        不合法的子菜单按钮KEY长度 = 40026,
        不合法的子菜单按钮URL长度 = 40027,
        不合法的自定义菜单使用用户 = 40028,
        不合法的oauth_code = 40029,
        不合法的refresh_token = 40030,
        不合法的openid列表 = 40031,
        不合法的openid列表长度 = 40032,
        不合法的请求字符不能包含uxxxx格式的字符 = 40033,
        不合法的参数 = 40035,
        不合法的请求格式 = 40038,
        不合法的URL长度 = 40039,
        不合法的分组id = 40050,
        分组名字不合法 = 40051,
        缺少access_token参数 = 41001,
        缺少appid参数 = 41002,
        缺少refresh_token参数 = 41003,
        缺少secret参数 = 41004,
        缺少多媒体文件数据 = 41005,
        缺少media_id参数 = 41006,
        缺少子菜单数据 = 41007,
        缺少oauth_code = 41008,
        缺少openid = 41009,
        access_token超时 = 42001,
        refresh_token超时 = 42002,
        oauth_code超时 = 42003,
        需要GET请求 = 43001,
        需要POST请求 = 43002,
        需要HTTPS请求 = 43003,
        需要接收者关注 = 43004,
        需要好友关系 = 43005,
        多媒体文件为空 = 44001,
        POST的数据包为空 = 44002,
        图文消息内容为空 = 44003,
        文本消息内容为空 = 44004,
        多媒体文件大小超过限制 = 45001,
        消息内容超过限制 = 45002,
        标题字段超过限制 = 45003,
        描述字段超过限制 = 45004,
        链接字段超过限制 = 45005,
        图片链接字段超过限制 = 45006,
        语音播放时间超过限制 = 45007,
        图文消息超过限制 = 45008,
        接口调用超过限制 = 45009,
        创建菜单个数超过限制 = 45010,
        回复时间超过限制 = 45015,
        系统分组不允许修改 = 45016,
        分组名字过长 = 45017,
        分组数量超过上限 = 45018,
        不存在媒体数据 = 46001,
        不存在的菜单版本 = 46002,
        不存在的菜单数据 = 46003,
        解析JSON_XML内容错误 = 47001,
        api功能未授权 = 48001,
        用户未授权该api = 50001,
        参数错误invalid_parameter = 61451,
        无效客服账号invalid_kf_account = 61452,
        客服帐号已存在kf_account_exsited = 61453,
        /// <summary>
        /// 客服帐号名长度超过限制(仅允许10个英文字符，不包括@及@后的公众号的微信号)(invalid kf_acount length) 
        /// </summary>
        客服帐号名长度超过限制 = 61454,
        /// <summary>
        /// 客服帐号名包含非法字符(仅允许英文+数字)(illegal character in kf_account) 
        /// </summary>
        客服帐号名包含非法字符 = 61455,
        /// <summary>
        ///  	客服帐号个数超过限制(10个客服账号)(kf_account count exceeded) 
        /// </summary>
        客服帐号个数超过限制 = 61456,
        无效头像文件类型invalid_file_type = 61457,
        系统错误system_error = 61450,
        日期格式错误 = 61500,
        日期范围错误 = 61501,

        //新加入的一些类型，以下文字根据P2P项目格式组织，非官方文字
        发送消息失败_48小时内用户未互动 = 10706,
        发送消息失败_该用户已被加入黑名单_无法向此发送消息 = 62751,
        发送消息失败_对方关闭了接收消息 = 10703,
        对方不是粉丝 = 10700


    }
    public class TemplateDataItem
    {
        /// <summary>
        /// 项目值
        /// </summary>
        public string value { get; set; }
        /// <summary>
        /// 16进制颜色代码，如：#FF0000
        /// </summary>
        public string color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v">value</param>
        /// <param name="c">color</param>
        public TemplateDataItem(string v, string c = "#173177")
        {
            value = v;
            color = c;
        }
    }
}
