using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using log4net;
using OpenEcnu.Data;
using OpenEcnu.Data.Models;
using OpenEcnu.ResourceServer.Models;

namespace OpenEcnu.ResourceServer.Controllers.Users
{
    /// <summary>
    /// users/show接口
    /// </summary>
    public class ShowController : ApiController
    {
        private readonly IOpenEcnuRepository repo = new OpenEcnuRepository(new OpenEcnuContext());
        private readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public HttpResponseMessage GetUserById(string access_token, string user_id)
        {
            ErrorModel error = new ErrorModel();
            UserModel user = new UserModel();

            if (repo.ValidateAccessToken(access_token, user_id))
            {
                UsersDetail ud = repo.GetUsersDetail(user_id);
                if (ud != null)
                {
                    user.UserId = ud.userId;
                    user.Name = ud.Name;
                    user.Gender = ud.Gender;
                    user.Email = ud.Email;
                    user.Birthday = ud.Birthday != null ? ud.Birthday.Value.ToLongDateString() : "";
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
            }
            error.Request = "oauth/users/show";
            error.ErrorCode = "201";
            error.ErrorMessage = "Invalidate User";
            logger.ErrorFormat("用户{0}试图访问资源失败，无法验证的客户",user_id);
            return Request.CreateResponse(HttpStatusCode.BadRequest, error);
        }
    }
}
