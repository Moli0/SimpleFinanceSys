using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class PublicController : BaseController
    {

        [HttpPost]
        public ActionResult Logout()
        {
            MVCHelper helper = new MVCHelper(this.HttpContext);
            helper.DeleteCookies("loginUserId");
            helper.DeleteCookies("loginSession");
            helper.DeleteCookies("loginTime");
            helper.DeleteCookies("ASP.NET_SessionId");
            helper.DeleteCookies("__RequestVerificationToken");
            return Success("账号已注销");
        }


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
    }
}