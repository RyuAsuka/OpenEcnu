using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using log4net;
using OpenEcnu.Data.Models;

namespace OpenEcnu.Data
{
    /// <summary>
    /// 数据访问层实现，实现IOpenEcnuRepository，IDisposable接口
    /// </summary>
    public class OpenEcnuRepository : IOpenEcnuRepository, IDisposable
    {
        private readonly OpenEcnuContext dbContext;
        private readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 获取数据访问层的数据上下文
        /// </summary>
        public OpenEcnuContext DbContext
        {
            get { return dbContext; }
        }

        /// <summary>
        /// 数据访问层构造函数
        /// </summary>
        /// <param name="context">注入上下文</param>
        public OpenEcnuRepository(OpenEcnuContext context)
        {
            dbContext = context;
        }

        /// <summary>
        /// 根据用户ID查找用户信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>查找到的用户实体</returns>
        public User GetUser(string userId)
        {
            return dbContext.Users.Find(userId);
        }

        /// <summary>
        /// 通过授权码获得用户信息
        /// </summary>
        /// <param name="code">授权码</param>
        /// <returns>根据授权码查找到的用户信息</returns>
        public User GetUserByCode(string code)
        {
            var findAuth = dbContext.Authorizations.FirstOrDefault(auth => auth.code == code);
            return findAuth != null ? GetUser(findAuth.userid) : null;
        }

        /// <summary>
        /// 根据用户ID查找用户详细信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>查找到的用户详细信息实体</returns>
        public UsersDetail GetUsersDetail(string userId)
        {
            return userId == null ? null : dbContext.UsersDetails.Find(userId);
        }

        /// <summary>
        /// 根据AppKey查找应用信息
        /// </summary>
        /// <param name="appKey">应用的AppKey</param>
        /// <returns>查找到的应用信息实体</returns>
        public AppInfo GetAppInfo(int appKey)
        {
            return dbContext.AppInfoes.Find(appKey);
        }

        /// <summary>
        /// 根据App名称查找应用信息
        /// </summary>
        /// <param name="name">App名称</param>
        /// <returns>查找到的App应用信息实体</returns>
        public AppInfo GetAppInfo(string name)
        {
            return dbContext.AppInfoes.FirstOrDefault(app => app.name == name);
        }

        /// <summary>
        /// 根据用户ID查找该用户注册的所有应用
        /// </summary>
        /// <param name="ownerId">用户ID</param>
        /// <returns>该用户所有的应用信息列表</returns>
        public List<AppInfo> GetAppInfoes(string ownerId)
        {
            return dbContext.AppInfoes.Where(app => app.owner == ownerId).ToList();
        }

        /// <summary>
        /// 创建APP
        /// </summary>
        /// <param name="app">要创建的AppInfo对象</param>
        /// <returns>指示创建是否成功</returns>
        /// <exception cref="System.Data.DataException"></exception>
        public bool CreateApp(AppInfo app)
        {
            try
            {
                dbContext.AppInfoes.Add(app);
                if (!SaveAll())
                    throw new DataException();
                log.InfoFormat("创建appkey为{0}的app成功", app.appkey);
                return true;
            }
            catch (DataException ex)
            {
                log.ErrorFormat("创建appkey为{0}的app失败", app.appkey);
                log.Error("异常信息：", ex);
                return false;
            }
            catch (Exception ex)
            {
                log.Fatal("未知异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 修改指定ID的APP信息
        /// </summary>
        /// <param name="appKey">应用的AppKey</param>
        /// <param name="newAppInfo">新的AppInfo，用于覆盖旧的AppInfo</param>
        /// <returns>指示修改是否成功</returns>
        /// <exception cref="System.Data.DataException"></exception>
        public bool UpdateApp(int appKey, AppInfo newAppInfo)
        {
            AppInfo oldAppInfo = dbContext.AppInfoes.Find(appKey);
            // 目前仅允许修改应用的名称、描述和回调地址
            // 2014-04-10 更新：允许修改所有者
            oldAppInfo.name = newAppInfo.name;
            oldAppInfo.description = newAppInfo.description;
            oldAppInfo.redirecturi = newAppInfo.redirecturi;
            oldAppInfo.owner = newAppInfo.owner;
            try
            {
                dbContext.Entry(oldAppInfo).State = EntityState.Modified;
                if (!SaveAll())
                    throw new DataException();
                log.InfoFormat("修改appkey为{0}的app信息成功", appKey);
                return true;
            }
            catch (DataException ex)
            {
                log.ErrorFormat("修改appkey为{0}的app失败", appKey);
                log.Error("异常信息：", ex);
                return false;
            }
            catch (Exception ex)
            {
                log.Fatal("未知异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 删除指定AppKey的App
        /// </summary>
        /// <param name="appKey">要删除的应用的AppKey</param>
        /// <returns>指示删除是否成功</returns>
        /// <exception cref="System.Data.DataException"></exception>
        public bool DeleteApp(int appKey)
        {
            AppInfo appToDelete = dbContext.AppInfoes.Find(appKey);
            try
            {
                dbContext.AppInfoes.Remove(appToDelete);
                if (!SaveAll())
                    throw new DataException();
                log.InfoFormat("删除AppKey为{0}的app成功", appKey);
                return true;
            }
            catch (DataException ex)
            {
                log.ErrorFormat("删除AppKey为{0}的app失败", appKey);
                log.Error("异常信息：", ex);
                return false;
            }
            catch (Exception ex)
            {
                log.Fatal("未知异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 通过用户ID和AppKey查找授权码信息
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="appKey">应用的AppKey</param>
        /// <returns>查找到的授权码信息</returns>
        public Authorization GetAuthorizationByUserIdAndAppKey(string userId, int appKey)
        {
            return dbContext.Authorizations.Find(appKey, userId);
        }

        /// <summary>
        /// 通过授权码查找授权码信息
        /// </summary>
        /// <param name="code">授权码</param>
        /// <returns>授权码信息</returns>
        public Authorization GetAuthorizationByCode(string code)
        {
            return dbContext.Authorizations.FirstOrDefault(auth => auth.code == code);
        }

        /// <summary>
        /// 增加授权码信息记录
        /// </summary>
        /// <param name="authorization">要写入的授权码信息实体</param>
        /// <returns>指示插入是否成功</returns>
        public bool InsertAuthorization(Authorization authorization)
        {
            try
            {
                if (dbContext.Authorizations.Find(authorization.appkey, authorization.userid) != null)
                {
                    log.WarnFormat("授权码已存在，将删除旧的授权码，appkey={0}，userId={1}", authorization.appkey, authorization.userid);
                    DeleteAuthorization(authorization.appkey, authorization.userid);
                }
                dbContext.Authorizations.Add(authorization);
                if (!SaveAll())
                    throw new DataException();
                log.Info("添加了一条Authorization记录, appkey=" + authorization.appkey + ", userId=" + authorization.userid +
                         ", code=" + authorization.code);
                return true;
            }
            catch (DataException ex)
            {
                log.Error("无法添加新的Authorization记录", ex);
                return false;
            }
            catch (Exception ex)
            {
                log.Fatal("未知异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 删除授权码信息记录
        /// </summary>
        /// <param name="appKey">应用的AppKey</param>
        /// <param name="userId">用户ID</param>
        /// <returns>指示删除是否成功</returns>
        public bool DeleteAuthorization(int appKey, string userId)
        {
            Authorization authorization = dbContext.Authorizations.Find(appKey, userId);
            try
            {
                dbContext.Authorizations.Remove(authorization);
                if (!SaveAll())
                    throw new DataException();
                log.InfoFormat("删除一条Authorization记录, appKey={0}, userId={1}", authorization.appkey, authorization.userid);
                return true;
            }
            catch (DataException ex)
            {
                log.ErrorFormat("无法删除指定Authorization记录, appKey={0}, userId={1}", authorization.appkey, authorization.userid);
                log.Error("异常信息：", ex);
                return false;
            }
            catch (Exception ex)
            {
                log.Fatal("未知异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 根据AppKey和UserId查找Access Token信息
        /// </summary>
        /// <param name="appKey">应用AppKey</param>
        /// <param name="userId">用户ID</param>
        /// <returns>查找到的Access Token信息实体</returns>
        public AccessToken GetAccessToken(int appKey, string userId)
        {
            return dbContext.AccessTokens.Find(appKey, userId);
        }

        /// <summary>
        /// 根据授权码查找Access Token信息
        /// </summary>
        /// <param name="code">授权码请求</param>
        /// <returns>查找到的Access Token信息实体</returns>
        public AccessToken GetAccessToken(string code)
        {
            var findAuth = dbContext.Authorizations.FirstOrDefault(auth => auth.code == code);
            return findAuth != null ? GetAccessToken(findAuth.appkey, findAuth.userid) : null;
        }

        /// <summary>
        /// 添加Access Token记录
        /// </summary>
        /// <param name="token">要添加的AccessToken对象</param>
        /// <returns>指示添加是否成功</returns>
        /// <exception cref="System.Data.DataException"></exception>
        public bool InsertAccessToken(AccessToken token)
        {
            try
            {
                dbContext.AccessTokens.Add(token);
                if (!SaveAll())
                    throw new DataException();
                log.InfoFormat("增加一条AccessToken记录, appkey={0}, userId={1}, token={2}", token.appkey, token.userid, token.accesstoken1);
                return true;
            }
            catch (DataException ex)
            {
                log.Error("无法增加新的AccessToken记录", ex);
                return false;
            }
            catch (Exception ex)
            {
                log.Fatal("未知异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 删除过期的Access Token，如果没有过期，则删除失败
        /// </summary>
        /// <param name="appKey">应用AppKey</param>
        /// <param name="userId">用户ID</param>
        /// <returns>指示删除是否成功</returns>
        public bool DeleteAccessToken(int appKey, string userId)
        {
            AccessToken token = dbContext.AccessTokens.Find(appKey, userId);
            // 检验token是否过期
            if (token.expire >= DateTime.Now) return false;
            try
            {
                dbContext.AccessTokens.Remove(token);
                if (!SaveAll())
                    throw new DataException();
                log.InfoFormat("删除一条AccessToken记录, appkey={0}, userId={1}, token={2}", token.appkey, token.userid,
                    token.accesstoken1);
                return true;
            }
            catch (DataException ex)
            {
                log.ErrorFormat("无法删除指定的AccessToken记录, appkey={0}, userId={1}", appKey, userId);
                log.Error("异常信息：", ex);
                return false;
            }
            catch (Exception ex)
            {
                log.Fatal("未知异常：", ex);
                return false;
            }
        }

        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="password">用户的密码</param>
        /// <returns>指示登录是否成功</returns>
        public bool UserLogin(string userId, string password)
        {
            var user = GetUser(userId);
            if (user == null)
            {
                log.Warn("用户" + userId + "不存在");
                return false;
            }
            if (password != user.passwd)
            {
                log.Warn("用户" + userId + "试图登录失败，密码不正确");
                return false;
            }
            log.Info("用户" + userId + "登录成功");
            return true;
        }

        /// <summary>
        /// 校验Access Token是否合法
        /// </summary>
        /// <param name="accessToken">Access Token</param>
        /// <param name="userId">用户ID</param>
        /// <returns>指示验证是否通过</returns>
        public bool ValidateAccessToken(string accessToken, string userId)
        {
            var result = dbContext.AccessTokens.FirstOrDefault(token => token.userid == userId && token.accesstoken1 == accessToken);
            if (result == null) return false;
            if (result.expire < DateTime.Now)
            {
                log.Warn("Access Token 校验失败！Access Token已过期，将删除此Access Token。");
                DeleteAccessToken(result.appkey, result.userid);
                return false;
            }
            log.InfoFormat("Access Token校验通过, userId={0}, userId={1}", userId, accessToken);
            return true;
        }

        /// <summary>
        /// 根据Access Token请求参数判断Access Token是否已存在
        /// </summary>
        /// <param name="appKey">请求的AppKey</param>
        /// <param name="code">请求的授权码</param>
        /// <returns>指示Access Token是否存在</returns>
        public bool IsAccessTokenExists(int appKey, string code)
        {
            string userId = "";
            var findUserByCode = dbContext.Authorizations.FirstOrDefault(auth => auth.appkey == appKey && auth.code == code);
            if (findUserByCode != null)
            {
                userId = findUserByCode.userid;
            }
            return GetAccessToken(appKey, userId) != null;
        }

        /// <summary>
        /// 将更改写入数据库
        /// </summary>
        /// <returns>指示更改是否已保存</returns>
        public bool SaveAll()
        {
            try
            {
                return dbContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                log.Fatal("异常信息：", ex);
                return false;
            }
        }

        /// <summary>
        /// 释放数据上下文的连接
        /// </summary>
        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
