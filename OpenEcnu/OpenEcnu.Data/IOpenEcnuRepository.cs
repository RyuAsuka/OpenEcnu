using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenEcnu.Data.Models;

namespace OpenEcnu.Data
{
    /// <summary>
    /// 数据访问层接口
    /// </summary>
    public interface IOpenEcnuRepository
    {
        OpenEcnuContext DbContext { get; }

        User GetUser(string userId);
        User GetUserByCode(string code);
        UsersDetail GetUsersDetail(string userId);

        AppInfo GetAppInfo(int appKey);
        AppInfo GetAppInfo(string name);
        List<AppInfo> GetAppInfoes(string ownerId);
        bool CreateApp(AppInfo app);
        bool UpdateApp(int appKey, AppInfo newAppInfo);
        bool DeleteApp(int appKey);

        Authorization GetAuthorizationByUserIdAndAppKey(string userId, int appKey);
        Authorization GetAuthorizationByCode(string code);
        bool InsertAuthorization(Authorization authorization);
        bool DeleteAuthorization(int appKey, string userId);

        AccessToken GetAccessToken(int appKey, string userId);
        AccessToken GetAccessToken(string code);
        bool InsertAccessToken(AccessToken token);
        bool DeleteAccessToken(int appKey, string userId);

        bool UserLogin(string userId, string password);
        bool ValidateAccessToken(string accessToken, string userId);
        bool IsAccessTokenExists(int appKey, string code);

        bool SaveAll();
    }
}
