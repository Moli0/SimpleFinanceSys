using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    [Helpers.UserLogin]
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var userModel = GetUser();
            if (userModel != null)
            {
                UpdateCookie(filterContext);
            }
            base.OnActionExecuting(filterContext);
        }

        protected void UpdateCookie(ActionExecutingContext filterContext)
        {
            Helpers.MVCHelper helper = new Helpers.MVCHelper(this.HttpContext);
            string userCookie = helper.GetCookies("user");
            DateTime cookieTime = DateTime.Now;
            string encodeCookie = string.Empty;
            encodeCookie = Helpers.Helper.MD5(userCookie + cookieTime + "SFS") + Helpers.Helper.MD5(userCookie + "SFS");
            var time = DateTime.Now;
            helper.UpdateCookies("user", userCookie, 600);
            helper.UpdateCookies("sign", encodeCookie, 600);
            helper.UpdateCookies("loginTime", cookieTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), 600);
        }

        /// <summary>
        /// 取得登录的用户信息
        /// </summary>
        /// <returns></returns>
        protected Model.UserInfoModel GetUser()
        {
            Helpers.MVCHelper helper = new Helpers.MVCHelper(this.HttpContext);
            string userid = helper.GetCookies("user");
            if (!string.IsNullOrEmpty(userid))
            {
                DAL.UserInfoDAL dal = new DAL.UserInfoDAL();
                Model.UserInfoModel user = new Model.UserInfoModel();
                user = dal.GetModel(string.Format("userid = '{0}'", userid));
                return user;
            }
            return null;
        }


        protected internal virtual JsonResult SuccessMsg(string msg)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { status = "success", code = 0, msg = msg });
        }

        /// <summary>
        /// 成功返回code=0,model
        /// </summary>
        /// <param name="obj">model的内容</param>
        /// <returns></returns>
        protected internal virtual JsonResult Success(object model)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { status = "success", code = 0, model }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 成功返回code=0,model,pagins
        /// </summary>
        /// <param name="obj">model的内容</param>
        /// <returns></returns>
        protected internal virtual JsonResult Success(object model, object pagins)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            //var res = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = "success", code = 0, model, pagins });
            return Json(new { status = "success", code = 0, model, pagins }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 失败返回code = 1,失败消息
        /// </summary>
        /// <param name="msg">返回的失败消息</param>
        /// <returns></returns>
        protected internal virtual JsonResult Error(string msg)
        {
            Response.StatusCode = 250;
            return Json(new { code = 1, msg = msg }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 警告返回code = 2,警告消息
        /// </summary>
        /// <param name="msg">警告的消息</param>
        /// <returns></returns>
        protected internal virtual JsonResult Warnning(string msg)
        {
            Response.StatusCode = 230;
            return Json(new { code = 2, msg = msg });
        }

        protected internal virtual ActionResult Forbidden(string msg = "抱歉，您无权访问")
        {
            Response.StatusCode = 435;
            var content = new ContentResult();
            content.Content = string.Format("<script>alert('{0}');window.history.go(-1);</script>",msg);
            content.ContentEncoding = Encoding.UTF8;
            return content;
        }

        protected internal virtual bool IsNull(params string[] value)
        {
            foreach (var a in value)
            {
                if (string.IsNullOrEmpty(a))
                {
                    return true;
                }
            }
            return false;
        }
    }
}