using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using log4net;
using OpenEcnu.AuthorizationServer.Models;
using OpenEcnu.Data;
using OpenEcnu.Data.Models;
using RandomString;

namespace OpenEcnu.AuthorizationServer.Controllers
{
    /// <summary>
    /// Access Token控制器，负责向客户端发放Access Token
    /// </summary>
    public class TokenController : ApiController
    {
        private readonly IOpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 接受POST方法发送的Access Token请求报文，校验参数后生成Access Token响应返回客户端。
        /// </summary>
        /// <param name="model">请求报文参数</param>
        /// <returns>Access Token响应</returns>
        public HttpResponseMessage Post([FromBody] TokenRequestModel model)
        {
            try
            {
                TokenErrorResponseModel errorModel = new TokenErrorResponseModel();
                TokenResponseModel responseModel = new TokenResponseModel();

                // 判断是否存在Access Token，如果存在，则验证之，如果验证通过直接使用其重定向。否则重新分配Access Token
                if (repo.IsAccessTokenExists(model.client_id, model.code))
                {
                    AccessToken token = repo.GetAccessToken(model.code);
                    if (repo.ValidateAccessToken(token.accesstoken1, token.userid))
                    {
                        responseModel.access_token = token.accesstoken1;
                        responseModel.user_id = token.userid;
                        return Request.CreateResponse(HttpStatusCode.OK, responseModel);
                    }
                }
                logger.Warn("Access Token已过期，将重新生成");
                responseModel.user_id = repo.GetUserByCode(model.code).userid;
                responseModel.access_token = RandomStringImpl.CreateRandomString(20);
                AccessToken newToken = new AccessToken
                {
                    accesstoken1 = responseModel.access_token,
                    userid = responseModel.user_id,
                    createtime = DateTime.Now,
                    expire = DateTime.Now.AddSeconds(responseModel.expires_in),
                    appkey = model.client_id
                };

                // 分配完成Access Token后删除该用户用于请求Access Token的授权码
                if (repo.InsertAccessToken(newToken))
                {
                    if (repo.DeleteAuthorization(model.client_id, responseModel.user_id))
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, responseModel);
                    }
                }

                // 如果出错则构造出错响应报文并回传客户端
                errorModel.error = "Bad Request";
                errorModel.error_description = "Bad Request";
                errorModel.error_uri = "error";
                logger.Error("发放Access Token出错，请通过调试查找原因");
                return Request.CreateResponse(HttpStatusCode.BadRequest, errorModel);

            }
            catch (Exception ex)
            {
                TokenErrorResponseModel errorModel = new TokenErrorResponseModel
                {
                    error = "Bad Request",
                    error_description = "Bad Request",
                    error_uri = "error"
                };
                logger.Error("发放Access Token出错，请通过调试查找原因", ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, errorModel);
            }
        }
    }
}
