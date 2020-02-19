using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class PublicController : Controller
    {

        [HttpPost]
        public ActionResult Logout()
        {
            MVCHelper helper = new MVCHelper(this.HttpContext);
            helper.DeleteCookies("user");
            helper.DeleteCookies("sign");
            helper.DeleteCookies("loginTime");
            return Success("账号已注销");
        }

        public ActionResult TipsMessage(int code)
        {
            string msg = "";
            switch (code)
            {
                case 430: msg = $"<script>window.top.location='/Login';</script>"; break;
                case 431: msg = $"<script>alert('无效的令牌');window.history.go(-1);</script>"; break;
                case 432: msg = $"<script>window.top.location='/Login';</script>"; break;
                case 433: msg = $"<script>alert('抱歉，您暂时无权访问');window.history.go(-1);</script>"; break;
                case 435: msg = $"<script>alert('抱歉，您无权访问');window.history.go(-1);</script>"; break;
                case 480: msg = $"<script>alert('数据异常');window.history.go(-1);</script>"; break;
                default: msg = $"<script>alert('操作异常');window.history.go(-1);</script>"; break;
            }
            return Content(msg);
        }

        [Helpers.UserLogin]
        [HttpPost]
        public ActionResult UploadImgFile(string fileBase64Str, int fileType)
        {
            string filePath = Helpers.Tool.GetAppSetting("imgFilePathSave");
            string fileName = Guid.NewGuid().ToString();
            fileBase64Str = Helpers.Tool.ReplaceString(fileBase64Str, "data:image/png;base64,", "");
            fileBase64Str = Helpers.Tool.ReplaceString(fileBase64Str, "data:image/jpg;base64,", "");
            fileBase64Str = Helpers.Tool.ReplaceString(fileBase64Str, "data:image/gif;base64,", "");
            fileBase64Str = Helpers.Tool.ReplaceString(fileBase64Str, "data:image/jpeg;base64,", "");
            fileBase64Str = Helpers.Tool.ReplaceString(fileBase64Str, "data:image/bmp;base64,", "");
            var res = Helpers.Helper.Base64StringToImage(filePath, fileName, fileBase64Str, (Helpers.ImageType)fileType);
            string localtionUrl = Helpers.Tool.GetAppSetting("imgFilePathRead") + fileName + "." + ((Helpers.ImageType)fileType).ToString().ToLower();
            if (res == null)
            {
                return Error("上传失败");
            }
            else
            {
                return SuccessMsg(localtionUrl);
            }
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

    }
}