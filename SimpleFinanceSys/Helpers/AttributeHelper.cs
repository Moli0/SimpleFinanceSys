using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Helpers
{
    public class UserLoginAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public bool IsLogin { get; set; }

        public UserLoginAttribute(bool isLogin = false)
        {
            IsLogin = isLogin;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            MVCHelper helper = new MVCHelper(filterContext.RequestContext.HttpContext);
            string userid = helper.GetCookies("user");
            if (string.IsNullOrEmpty(userid))
            {
                filterContext.RequestContext.HttpContext.Response.StatusCode = 430;
                var content = new ContentResult();
                content.Content = "<script>window.top.location='/Login';</script>";
                content.ContentEncoding = Encoding.UTF8;
                filterContext.Result = content;
                return;
            }
            else
            {
                string encodeCookie = helper.GetCookies("sign");
                string loginTime = helper.GetCookies("loginTime");
                if (encodeCookie != Helpers.Helper.MD5(userid + loginTime + "SFS") + Helpers.Helper.MD5(userid + "SFS")) {
                    helper.DeleteCookies("user");
                    helper.DeleteCookies("sign");
                    helper.DeleteCookies("loginTime");
                    filterContext.RequestContext.HttpContext.Response.StatusCode = 430;
                    var content = new ContentResult();
                    content.Content = "<script>window.top.location='/Login';</script>";
                    content.ContentEncoding = Encoding.UTF8;
                    filterContext.Result = content;
                    return;
                }
                DAL.UserInfoDAL dal = new DAL.UserInfoDAL();
                int userCount = dal.GetCount($"userid = '{userid}'");
                if (userCount == 0)
                {
                    filterContext.RequestContext.HttpContext.Response.StatusCode = 430;
                    var content = new ContentResult();
                    content.Content = "<script>window.top.location='/Login';</script>";
                    content.ContentEncoding = Encoding.UTF8;
                    filterContext.Result = content;
                    return;
                }
                else
                {
                    IsLogin = true;
                }
            }
        }
    }
}
