using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class LoanController : BaseController
    {
        //
        // GET: /Loan/

        DAL.LoanRecoredDAL dal = new DAL.LoanRecoredDAL();

        public ActionResult LoanManagement()
        {
            return View();
        }
        public ActionResult RepaymentManagement() {
            return View();
        }

        public ActionResult DepositManagement()
        {
            return View();
        }
        public ActionResult DrawMoneyManagement()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddLoan(Model.LoanRecoredModel model, string oldId, string isCash) {
            var user = GetUser();
            DAL.Base_TypeDAL tdal = new DAL.Base_TypeDAL();
            DAL.ChangeRecordDAL rdal = new DAL.ChangeRecordDAL();
            if (IsNull(model.title, model.amount)) {
                return Error("请将数据填写完整");
            }
            model.Create();
            model.create_user = user.id;
            if (model.recordType < 3)
            {
                //添加存款、取款、借款
                DateTime dt = DateTime.Now;
                int isSaveOrderId = dal.GetCount(string.Format(" orderId like '{0}%' ", dt.ToString("yyyyMMddHHmmssfff")));
                model.orderId = dt.ToString("yyyyMMddHHmmssfff") + (isSaveOrderId + 1).ToString("D3");
                if (model.recordType == 1) {
                    model.nowAmount = model.amount;
                }
                if (isCash == "1")
                {
                    Model.ChangeRecordModel cmodel = new Model.ChangeRecordModel();
                    string orderType = " ";
                    cmodel.Create();
                    cmodel.create_user = user.id;
                    cmodel.beforeAmount = user.nowMoney;
                    cmodel.amount = model.amount;
                    isSaveOrderId = rdal.GetCount(string.Format(" orderId like '{0}%' ", dt.ToString("yyyyMMddHHmmssfff")));
                    cmodel.orderId = dt.ToString("yyyyMMddHHmmssfff") + (isSaveOrderId + 1).ToString("D3");
                    cmodel.payObject = model.orderObject;
                    //款项金额与现金关联,需添加现金记录
                    if (model.recordType == 0)
                    {
                        //存款  --从现金中来，减少现金
                        if (double.Parse(user.nowMoney) < double.Parse(model.amount)) {
                            return Error("现金不足！");
                        }
                        orderType = "转存支出";
                        cmodel.afterAmount = (double.Parse(cmodel.beforeAmount) - double.Parse(cmodel.amount)).ToString("f4");
                        cmodel.orderType = 1;
                    }
                    else if (model.recordType == 1 || model.recordType == 2)
                    {
                        //借款  --增加到现金中，现金增加
                        if (model.recordType == 1)
                        {
                            orderType = "借款收入";
                        }
                        else {
                            orderType = "取款收入";
                        }
                        cmodel.afterAmount = (double.Parse(cmodel.beforeAmount) + double.Parse(cmodel.amount)).ToString("f4");
                        cmodel.orderType = 0;
                    }
                    else {
                        return Error("操作异常！");
                    }
                    Model.Base_TypeModel tmodel = tdal.GetModel(string.Format(" state = 1 and name = '{0}' and create_user = '{1}' ", orderType, user.id));
                    cmodel.payType = tmodel.id;

                    int res = dal.InsertSql<Model.LoanRecoredModel>(model, "LoanRecored");
                    if (res > 0)
                    {
                        int insertCRecordRes = rdal.InsertSql<Model.ChangeRecordModel>(cmodel, "ChangeRecord");
                        if (insertCRecordRes > 0)
                        {
                            user.nowMoney = cmodel.afterAmount;
                            int updateUserNowMoney = dal.Update<Model.UserInfoModel>(user,"UserInfo");
                            if (updateUserNowMoney > 0)
                            {
                                return SuccessMsg("添加成功");
                            }
                            else {
                                dal.Delete(model.id, "LoanRecored");
                                rdal.Delete(cmodel.id, "ChangeRecord");
                                return Error("网络繁忙！");
                            }
                        }
                        else
                        {
                            dal.Delete(model.id, "LoanRecored");
                            return Error("网络繁忙！");
                        }
                    }
                    else {
                        return Error("网络繁忙！");
                    }
                }
                else {
                    int res = dal.InsertSql<Model.LoanRecoredModel>(model, "LoanRecored");
                    if (res > 0) {
                        return SuccessMsg("添加成功");
                    }
                    else
                    {
                        return Error("网络繁忙！");
                    }
                }
            }
            else {
                if (string.IsNullOrWhiteSpace(oldId)) {
                    return Error("请将数据填写完整");
                }
                Model.LoanRecoredModel oldModel = dal.GetModel(string.Format(" orderId = '{0}' ", oldId));
                oldModel.nowAmount = (double.Parse(oldModel.nowAmount) - double.Parse(model.amount)).ToString("f4");
                if (double.Parse(oldModel.nowAmount) < 0) {
                    return Warnning("还款金额大于待还金额！");
                }
                if (double.Parse(user.nowMoney) < double.Parse(model.amount))
                {
                    return Error("现金不足！");
                }
                if (double.Parse(oldModel.nowAmount) == 0) {
                    oldModel.isFinish = 1;
                }
                DateTime dt = DateTime.Now;
                int isSaveOrderId = dal.GetCount(string.Format(" orderId like '{0}%' ", dt.ToString("yyyyMMddHHmmssfff")));
                model.orderId = dt.ToString("yyyyMMddHHmmssfff") + (isSaveOrderId + 1).ToString("D3");
                //添加还款
                if (isCash == "1")
                {
                    //还款金额来源于现金

                    Model.ChangeRecordModel cmodel = new Model.ChangeRecordModel();
                    cmodel.Create();
                    cmodel.create_user = user.id;
                    cmodel.beforeAmount = user.nowMoney;
                    cmodel.amount = model.amount;
                    isSaveOrderId = rdal.GetCount(string.Format(" orderId like '{0}%' ", dt.ToString("yyyyMMddHHmmssfff")));
                    cmodel.orderId = dt.ToString("yyyyMMddHHmmssfff") + (isSaveOrderId + 1).ToString("D3");
                    cmodel.payObject = model.orderObject;
                    Model.Base_TypeModel tmodel = tdal.GetModel(string.Format(" state = 1 and name = '还款支出' and create_user = '{0}' ", user.id));
                    cmodel.payType = tmodel.id;

                    cmodel.afterAmount = (double.Parse(cmodel.beforeAmount) - double.Parse(cmodel.amount)).ToString("f4");
                    cmodel.orderType = 1;

                    model.topOrderId = oldId;
                    int res = dal.InsertSql<Model.LoanRecoredModel>(model, "LoanRecored");
                    if (res > 0) {
                        int insertCRecordRes = rdal.InsertSql<Model.ChangeRecordModel>(cmodel, "ChangeRecord");
                        if (insertCRecordRes > 0)
                        {
                            user.nowMoney = cmodel.afterAmount;
                            int updateUserNowMoney = dal.Update<Model.UserInfoModel>(user, "UserInfo");
                            if (updateUserNowMoney > 0)
                            {
                                int updateOldModel = dal.Update<Model.LoanRecoredModel>(oldModel, "LoanRecored");
                                if (updateOldModel > 0)
                                {
                                    return SuccessMsg("还款成功");
                                }
                                else
                                {
                                    user.nowMoney = cmodel.beforeAmount;
                                    dal.Update<Model.UserInfoModel>(user, "UserInfo");
                                    dal.Delete(model.id, "LoanRecored");
                                    rdal.Delete(cmodel.id, "ChangeRecord");
                                    return Error("网络繁忙");
                                }
                            }
                            else {
                                dal.Delete(model.id, "LoanRecored");
                                rdal.Delete(cmodel.id, "ChangeRecord");
                                return Error("网络繁忙");
                            }
                        }
                        else
                        {
                            dal.Delete(model.id, "LoanRecored");
                            return Error("网络繁忙");
                        }
                    } else {
                        return Error("网络繁忙！");
                    }
                }
                else {
                    int res = dal.InsertSql<Model.LoanRecoredModel>(model, "LoanRecored");
                    if (res > 0)
                    {
                        int updateOldModel = dal.Update<Model.LoanRecoredModel>(oldModel, "LoanRecored");
                        if (updateOldModel > 0)
                        {
                            return SuccessMsg("还款成功");
                        }
                        else
                        {
                            dal.Delete(model.id, "LoanRecored");
                            return Error("网络繁忙");
                        }
                    }
                    else
                    {
                        return Error("网络繁忙！");
                    }
                }
            }
        }

        [HttpGet]
        public ActionResult GetLoanIdList() {
            var user = GetUser();
            List<Model.LoanRecoredModel> list = new List<Model.LoanRecoredModel>();
            list = dal.GetListModel(string.Format("create_user = '{0}' and recordType = 1 and isFinish = 0 ", user.id));
            return Success(list);
        }

        [HttpGet]
        public ActionResult GetModel(string orderId) {
            var user = GetUser();
            Model.LoanRecoredModel model = dal.GetModel(string.Format(" create_user = '{0}' and orderId = '{1}'", user.id, orderId));
            return Success(model);
        }

        [HttpGet]
        public ActionResult GetList(int type, Model.PaginModel pagin, string year, string startTime, string endTime, string payObject, string key) {
            var user = GetUser();
            string cond = string.Format(" create_user = '{0}' and recordType = {1} ", user.id, type);
            if (!string.IsNullOrWhiteSpace(year))
            {
                cond += string.Format(" and Left(Convert(varchar(100),create_time,120),4) = '{0}' ", year);
            }
            if (!string.IsNullOrWhiteSpace(startTime))
            {
                cond += string.Format(" and create_time > '{0}' ", startTime);
            }
            if (!string.IsNullOrWhiteSpace(endTime))
            {
                cond += string.Format(" and create_time < '{0}' ", endTime);
            }
            if (!string.IsNullOrWhiteSpace(payObject))
            {
                cond += string.Format(" and orderObject = '{0}' ", payObject);
            }
            if (!string.IsNullOrWhiteSpace(key))
            {
                if (key.Length > 8)
                {
                    cond += string.Format(" and (remark like '%{0}%' or title = '%{0}%' or orderId = '{0}' ) ", key.Trim());
                }
                else
                {
                    cond += string.Format(" and (remark like '%{0}%' or title = '%{0}%' or orderId = '{0}' or amount = '{0}' ) ", key.Trim());
                }
            }
            DataSet ds = new DataSet();
            string table = @"LoanRecored t1
left join Base_Object t2 on t2.id = t1.orderObject";
            ds = DAL.Helper.GetPaginList(pagin, table, "t1.*,t2.name objectName", cond);
            DataTable dt = ds.Tables[0];
            pagin.counts = dal.GetCount(cond);
            return Success(Newtonsoft.Json.JsonConvert.SerializeObject(dt), pagin);
        }

    }
}
