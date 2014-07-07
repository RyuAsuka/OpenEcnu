using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using log4net;
using Newtonsoft.Json;
using OpenEcnuSDK.Entities;

namespace OpenEcnuSDK
{
    public class Oauth
    {
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private const string TOKEN_SERVER = "http://open.ecnu.edu.cn/oauth/oauth/token";
        private const string RESOURCE_SERVER = "http://open.ecnu.edu.cn/api/users/show";
        private const string AUTH_SERVER = "http://open.ecnu.edu.cn/oauth/oauth/authorize";
        private const string SERVER_HOST = "open.ecnu.edu.cn";
        private const string FORM_CONTENT_TYPE = "application/x-www-form-urlencoded";

        /// <summary>
        /// 获取全局的AccessToken
        /// </summary>
        public AccessToken Token { get; private set; }

        /// <summary>
        /// 跳转到取得授权码的URL
        /// </summary>
        /// <param name="appKey">应用的AppKey</param>
        /// <param name="redirectUri">应用的回调URI</param>
        /// <returns>表示该URL的字符串</returns>
        /// <example>
        /// 使用Redirect()方法跳转到该地址
        /// </example>
        public string GetAuthorizationCode(int appKey, string redirectUri)
        {
            return AUTH_SERVER + "?client_id=" + appKey + "&redirect_uri=" + redirectUri + "&response_type=code";
        }

        /// <summary>
        /// 从授权服务器获取Access Token
        /// </summary>
        /// <param name="appKey">应用的AppKey</param>
        /// <param name="appSecret">应用的AppSecret</param>
        /// <param name="redirectUri">应用的回调URI</param>
        /// <param name="code">获得的授权码</param>
        /// <returns>Access Token实体</returns>
        public AccessToken GetAccessToken(string appKey, string appSecret, string redirectUri, string code)
        {
            HttpWebRequest accessTokenRequest = WebRequest.Create(TOKEN_SERVER) as HttpWebRequest;
            if (accessTokenRequest == null) throw new Exception();
            accessTokenRequest.Method = "POST";
            accessTokenRequest.ContentType = FORM_CONTENT_TYPE;
            accessTokenRequest.Host = SERVER_HOST;

            string requestBodyString = "client_id=" + appKey
                                     + "&client_secret=" + appSecret
                                     + "&grant_type=authorization_code"
                                     + "&redirect_uri=" + redirectUri
                                     + "&code=" + code;
            byte[] requestBody = Encoding.UTF8.GetBytes(requestBodyString);
            Stream requestStream = accessTokenRequest.GetRequestStream();
            requestStream.Write(requestBody, 0, requestBody.Length);
            try
            {
                HttpWebResponse accessTokenResponse = accessTokenRequest.GetResponse() as HttpWebResponse;
                if (accessTokenResponse == null)
                {
                    throw new NullReferenceException();
                }
                using (Stream responseStream = accessTokenResponse.GetResponseStream())
                {
                    if (responseStream == null)
                    {
                        throw new NullReferenceException();
                    }
                    using (StreamReader sr = new StreamReader(responseStream))
                    {
                        string getedJson = sr.ReadToEnd();
                        Token = JsonConvert.DeserializeObject<AccessToken>(getedJson);
                    }
                    return Token;
                }
            }
            catch (WebException webEx)
            {
                logger.Fatal(webEx.Response);
                logger.Fatal(webEx.Status);
                return null;
            }
        }

        /// <summary>
        /// 根据用户ID获取用户数据
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户信息实体</returns>
        public UserInfo UsersShow(string userId)
        {
            string requestAddress = RESOURCE_SERVER + "?access_token=" + Token.access_token + 
                                    "&user_id=" + Token.user_id;
            HttpWebRequest userInfoRequest = WebRequest.Create(requestAddress) as HttpWebRequest;
            if (userInfoRequest == null) throw new Exception();
            userInfoRequest.Method = "GET";

            HttpWebResponse userInfoResponse = userInfoRequest.GetResponse() as HttpWebResponse;

            if (userInfoResponse == null) throw new NullReferenceException();
            try
            {
                using (Stream responseStream = userInfoResponse.GetResponseStream())
                {
                    UserInfo user;
                    if (responseStream == null) throw new NullReferenceException();
                    using (StreamReader sr = new StreamReader(responseStream))
                    {
                        string getedJson = sr.ReadToEnd();
                        user = JsonConvert.DeserializeObject<UserInfo>(getedJson);
                    }
                    return user;
                }

            }
            catch (Exception ex)
            {
                logger.Fatal(ex.Message);
                logger.Fatal(ex.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// 根据Access Token和用户ID获取用户数据
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <param name="userId">用户ID</param>
        /// <returns>用户信息实体</returns>
        public UserInfo UsersShow(string accessToken, string userId)
        {
            Token.access_token = accessToken;
            return UsersShow(userId);
        }
    }
}
