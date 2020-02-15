using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class AccountController : BaseController
    {

        DAL.UserInfoDAL dal = new DAL.UserInfoDAL();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }


        //修改用户信息
        [HttpPost]
        public ActionResult SubmitInfo(Model.UserInfoModel model) {
            var user = base.GetUser();
            if (user == null) {
                return Error("登陆已过期，请重新登陆");
            }
            if (user.userid != model.userid) {
                return Error("非法的操作，请刷新后重试！");
            }
            user.username = model.username;
            user.headUrl = model.headUrl;
            user.last_time = DateTime.Now;
            user.last_user = user.id;
            user.monthMaxPay = model.monthMaxPay;
            user.dayMaxPay = model.dayMaxPay;
            user.username = model.username;

            int res = dal.Update<Model.UserInfoModel>(user,"userinfo");
            if (res > 0)
            {
                return SuccessMsg("修改成功");
            }
            else {
                return Error("系统异常，请稍后再试");
            }
        }

        [HttpGet]
        public ActionResult GetUserInfo() {
            var user = GetUser();
            if (user == null) {
                return Error("登陆已过期，请重新登陆");
            }
            user.pwd = "";
            user.id = "";
            if (string.IsNullOrEmpty(user.headUrl)) {
                user.headUrl = Helpers.Tool.GetAppSetting("headImg");
            }
            return Success(user);
        }


        [HttpPost]
        public ActionResult ChangePwd(string oldPwd,string newPwd,string newPwds) {
            if (newPwd != newPwds) {
                return Error("两次密码不一致，请重新输入");
            }
            var user = GetUser();
            if (user == null) {
                return Error("登陆已过期，请重新登陆！");
            }

            var model = new Model.UserInfoModel();
            model = dal.GetModel(string.Format("userid = '{0}'", user.userid));
            if (Helpers.Helper.MD5("SFS" + oldPwd)!=model.pwd) {
                return Error("原密码错误，请重试！");
            }
            model.pwd = Helpers.Helper.MD5("SFS" + newPwd);
            int res = dal.Update<Model.UserInfoModel>(model,"userinfo");
            if (res > 0)
            {
                return SuccessMsg("修改成功");
            }
            else {
                return Error("系统繁忙，请稍后再试！");
            }

        }

        [HttpPost]
        public ActionResult ChangeMoney(string baseAmount,string nowMoney) {
            DAL.ChangeRecordDAL rdal = new DAL.ChangeRecordDAL();
            Model.ChangeRecordModel record = new Model.ChangeRecordModel();
            var user = GetUser();
            record.beforeAmount = user.nowMoney.ToString();
            if (user == null) {
                return Error("登陆已过期，请重新登陆!");
            }
            double baseM = 0;
            double nowM = 0;
            if (!double.TryParse(baseAmount, out baseM)) {
                return Error("请输入数值类型");
            }
            if (!double.TryParse(nowMoney, out nowM))
            {
                return Error("请输入数值类型");
            }
            if (user.state == 0)
            {
                user.baseAmount = baseM.ToString("f4");
                user.nowMoney = nowM.ToString("f4");
                user.state = 1;
            }
            else {
                user.nowMoney = nowM.ToString("f4");
            }
            user.Modify();
            user.last_user = user.id;
            int res = dal.Update<Model.UserInfoModel>(user,"userinfo");
            if (res > 0)
            {
                //TODO:添加记账记录
                DateTime dt = DateTime.Now;
                record.Create();
                record.create_user=user.id;
                int isSaveOrderId = rdal.GetCount(string.Format(" orderId like '{0}%' ",dt.ToString("yyyyMMddHHmmssfff")));
                record.orderId = dt.ToString("yyyyMMddHHmmssfff") + (isSaveOrderId + 1).ToString("D3");
                record.orderType = 2;
                record.payType = " ";
                record.payObject = " ";
                record.afterAmount = nowM.ToString("f4");
                record.amount = (double.Parse(record.afterAmount) - double.Parse(record.beforeAmount)).ToString("f4");
                record.remark = "";
                int save = rdal.InsertSql<Model.ChangeRecordModel>(record, "changeRecord");
                if (save > 0)
                {
                    return SuccessMsg("修改成功");
                }
                else {
                    user.nowMoney = record.beforeAmount;
                    user.Modify();
                    user.last_user = user.id;
                    dal.Update<Model.UserInfoModel>(user, "userinfo");
                    return Error("修改失败，无法将修正记录写入！");
                }
            }
            else
            {
                return Error("系统繁忙，请稍后再试！");
            }
        }

        [HttpPost]
        public ActionResult Record(Model.ChangeRecordModel model) {
            DAL.ChangeRecordDAL rdal = new DAL.ChangeRecordDAL();
            var user = GetUser();
            if (user.state < 1) {
                return Error("请先将账户资产信息维护完整");
            }
            if (string.IsNullOrEmpty(model.id))
            {
                //添加
                if (IsNull(model.payType, model.payObject, model.amount)) {
                    return Error("请将数据填写完整！");
                }
                DateTime dt = DateTime.Now;
                int isSaveOrderId = rdal.GetCount(string.Format(" orderId like '{0}%' ", dt.ToString("yyyyMMddHHmmssfff")));
                model.Create();
                model.create_user = GetUser().id;
                model.orderId = dt.ToString("yyyyMMddHHmmssfff") + (isSaveOrderId + 1).ToString("D3");
                model.beforeAmount = user.nowMoney;
                #region model.afterAmount
                if (model.orderType == 0)
                {
                    model.afterAmount = (double.Parse(model.beforeAmount) + double.Parse(model.amount)).ToString("f4");
                }
                else if (model.orderType == 1)
                {
                    model.afterAmount = (double.Parse(model.beforeAmount) - double.Parse(model.amount)).ToString("f4");
                }
                else {
                    return Error("无效的记账类型！");
                }
                #endregion
                int res = dal.InsertSql<Model.ChangeRecordModel>(model, "ChangeRecord");
                if (res > 0)
                {
                    user.nowMoney = model.afterAmount;
                    int t = dal.Update<Model.UserInfoModel>(user,"userinfo");
                    if (t > 0)
                    {
                        return SuccessMsg("添加成功");
                    }
                    else {
                        dal.Delete(model.id,"ChangeRecord");
                        return Error("用户数据更新失败，请稍后再试");
                    }
                }
                else {
                    return Error("网络异常，请重试");
                }
            }
            else {
                //修改
                //不可修改
                return Warnning("数据不可修改，请使用记账方式进行抹平");
                //model.Modify();
                //model.last_user = GetUser().id;
                //if (GetUser().id != dal.GetDataForType<Model.ChangeRecordModel>(model.id, "ChangeRecord").create_user) {
                //    return Error("无效的数据！");
                //}
                //int res = dal.Update<Model.ChangeRecordModel>(model);
                //if (res > 0)
                //{
                //    return SuccessMsg("修改成功");
                //}
                //else
                //{
                //    return Error("无效的数据！");
                //}
            }
        }
    }
}
