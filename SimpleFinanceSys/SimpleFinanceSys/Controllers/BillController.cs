using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class BillController : BaseController
    {
        //
        // GET: /Bill/
        DAL.ChangeRecordDAL dal = new DAL.ChangeRecordDAL();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBillList(Model.PaginModel pagin,string year,string orderType,string startTime,string endTime,string payType,string payObject,string key) {
            try
            {
                var user = GetUser();
                string cond = string.Format(" create_user = '{0}' ", user.id);
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
                if (!string.IsNullOrWhiteSpace(payType))
                {
                    cond += string.Format(" and payType = '{0}' ", payType);
                }
                if (!string.IsNullOrWhiteSpace(orderType))
                {
                    cond += string.Format(" and orderType = '{0}' ", orderType);
                }
                if (!string.IsNullOrWhiteSpace(payObject))
                {
                    cond += string.Format(" and payObject = '{0}' ", payObject);
                }
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (key.Length > 8)
                    {
                        cond += string.Format(" and (remark like '%{0}%' or orderId = '{0}' ) ", key.Trim());
                    }
                    else
                    {
                        cond += string.Format(" and (remark like '%{0}%' or orderId = '{0}' or amount = '{0}' ) ", key.Trim());
                    }
                }
                DataSet ds = new DataSet();
                string table = @"ChangeRecord t1
		left join Base_Type t2 on t2.id = t1.payType
		left join Base_Object t3 on t3.id = t1.payObject";
                ds = DAL.Helper.GetPaginList(pagin, table, "t1.*,t2.name typeName,t3.name objectName,convert(varchar(100),t1.create_time,120) create_timeStr", cond);
                DataTable dt = ds.Tables[0];
                pagin.counts = dal.GetCount(cond);
                return Success(Newtonsoft.Json.JsonConvert.SerializeObject(dt),pagin);
            }
            catch {
                return Error("网络繁忙！");
            }
        }

        [HttpGet]
        public ActionResult GetYearForBill() {
            var user = GetUser();
            string cond = string.Format(" create_user = '{0}' ", user.id);
            DataSet ds = new DataSet();
            ds = dal.GetYearForBill(cond);
            DataTable dt = ds.Tables[0];
            return Success(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        }

    }
}
