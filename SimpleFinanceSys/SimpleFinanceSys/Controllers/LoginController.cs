using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class LoginController : Controller
    {
        DAL.UserInfoDAL dal = new DAL.UserInfoDAL();
        //
        // GET: /Login/

        public ActionResult Index()
        {
            Helpers.MVCHelper helper = new Helpers.MVCHelper(this.HttpContext);
            string userid = helper.GetCookies("user");
            if (!string.IsNullOrEmpty(userid))
            {
                DAL.UserInfoDAL dal = new DAL.UserInfoDAL();
                int userCount = dal.GetCount(string.Format("userid = '{0}'", userid));
                if (userCount != 0)
                {
                    string userCookie = helper.GetCookies("user");
                    DateTime cookieTime = DateTime.Now;
                    string encodeCookie = string.Empty;
                    encodeCookie = Helpers.Helper.MD5(userCookie + cookieTime + "SFS") + Helpers.Helper.MD5(userCookie + "SFS");
                    var time = DateTime.Now;
                    helper.UpdateCookies("user", userCookie, 600);
                    helper.UpdateCookies("sign", encodeCookie, 600);
                    helper.UpdateCookies("loginTime", cookieTime.ToString("yyyy-MM-dd HH:mm:ss.fff"), 600);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(string userid, string pwd)
        {
            if (dal.GetCount(string.Format(" userid = '{0}' ", userid)) == 0)
            {
                return Error("账号不存在");
            }
            pwd = Helpers.Helper.MD5("SFS" + pwd);
            int count = dal.GetCount(string.Format(" userid = '{0}' and pwd = '{1}'", userid, pwd));
            if (count > 0)
            {
                if (Session["user"] != null)
                {
                    Session["user"] = null;   //注消原账号
                }
                var user = dal.GetModel(string.Format(" userid = '{0}' and pwd = '{1}'", userid, pwd));
                if (user.state < 0)
                {
                    return Error("账号已被冻结");
                }
                Session["user"] = user;
                var time = DateTime.Now;
                System.Web.HttpCookie cookie = new HttpCookie("user", userid);
                Helpers.MVCHelper mvcHelper = new Helpers.MVCHelper(this.HttpContext);
                cookie.Expires = time.AddMinutes(600);
                cookie.HttpOnly = true;
                HttpContext.Response.Cookies.Add(cookie);
                cookie = new HttpCookie("sign", Helpers.Helper.MD5(userid + time + "SFS") + Helpers.Helper.MD5(userid + "SFS"));
                cookie.Expires = time.AddMinutes(600);
                cookie.HttpOnly = true;
                HttpContext.Response.Cookies.Add(cookie);
                cookie = new HttpCookie("loginTime", time.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                cookie.Expires = time.AddMinutes(600);
                cookie.HttpOnly = true;
                HttpContext.Response.Cookies.Add(cookie);
                return Success("登陆成功");
            }
            else
            {
                return Error("密码错误");
            }
        }

        [HttpPost]
        public ActionResult SignUp(string userid,string pwd,string pwds,string email) {
            if (IsNull(userid, pwd, pwds, email)) {
                return Error("请将数据填写完整！");
            }
            if (pwd != pwds) {
                return Error("再次密码不一致，请重新输入！");
            }

            int count = dal.GetCount(string.Format("userid='{0}'", userid));
            if (count > 0) {
                return Error("该账号已存在，请重新输入");
            }
            Model.UserInfoModel model = new Model.UserInfoModel();
            model.Create();
            model.userid = userid;
            model.username = email;
            model.pwd = Helpers.Helper.MD5("SFS" + pwd);
            model.email = email;
            model.lastSignTime = DateTime.Parse("1970-01-01 00:00:00.000");
            int res = dal.InsertSql<Model.UserInfoModel>(model, "userinfo");
            if (InitUserSetting(model))
            {
                if (res > 0)
                {
                    return SuccessMsg("注册成功");
                }
                else
                {
                    return Error("网络繁忙，请明日再试！");
                }
            }
            else {
                return Error("网络繁忙");
            }

        }

        private bool InitUserSetting(Model.UserInfoModel model) {
            List<Model.Base_TypeModel> list = new List<Model.Base_TypeModel>();
            Model.Base_TypeModel btmodel = new Model.Base_TypeModel();
            btmodel.Create();
            btmodel.create_user = model.id;
            btmodel.state = 1;
            btmodel.name = "转存支出";
            btmodel.sort = 9999999;
            list.Add(btmodel);
            btmodel = new Model.Base_TypeModel();
            btmodel.Create();
            btmodel.create_user = model.id;
            btmodel.state = 1;
            btmodel.name = "借款收入";
            btmodel.sort = 9999999;
            list.Add(btmodel);
            btmodel = new Model.Base_TypeModel();
            btmodel.Create();
            btmodel.create_user = model.id;
            btmodel.state = 1;
            btmodel.name = "取款收入";
            btmodel.sort = 9999999;
            list.Add(btmodel);
            btmodel = new Model.Base_TypeModel();
            btmodel.Create();
            btmodel.create_user = model.id;
            btmodel.state = 1;
            btmodel.name = "还款支出";
            btmodel.sort = 9999999;
            list.Add(btmodel);
            int res = dal.InsertListSql<Model.Base_TypeModel>(list,"Base_Type");
            if (res == 4)
            {
                return true;
            }
            else {
                return false;
            }
        }

        /// <summary>
        /// 成功返回code=0,entity
        /// </summary>
        /// <param name="obj">entity的内容</param>
        /// <returns></returns>
        protected internal virtual JsonResult Success(object entity)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { status = "success", code = 0, model = entity });
        }

        /// <summary>
        /// 成功返回code=0,entity,pagins
        /// </summary>
        /// <param name="obj">entity的内容</param>
        /// <returns></returns>
        protected internal virtual JsonResult Success(object entity, object pagins)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            var res = Newtonsoft.Json.JsonConvert.SerializeObject(new { status = "success", code = 0, model = entity, pagins });
            return Json(res);
        }

        protected internal virtual JsonResult SuccessMsg(string msg)
        {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(new { status = "success", code = 0, msg = msg });
        }

        /// <summary>
        /// 失败返回code = 1,失败消息
        /// </summary>
        /// <param name="msg">返回的失败消息</param>
        /// <returns></returns>
        protected internal virtual JsonResult Error(string msg)
        {
            Response.StatusCode = 250;
            return Json(new { code = 1, msg = msg });
        }

        private bool IsNull(params string[] value) {
            foreach (var a in value) {
                if (string.IsNullOrEmpty(a)) {
                    return true;
                }
            }
            return false;
        }
    }
}
