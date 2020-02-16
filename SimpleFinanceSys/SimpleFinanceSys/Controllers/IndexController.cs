using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class IndexController : BaseController
    {
        DAL.ChangeRecordDAL dal = new DAL.ChangeRecordDAL();
        //
        // GET: /Index/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBoxTotalModel() {
            var user = GetUser();
            DataTable dt = dal.GetBoxTotalModel(user.id).Tables[0];
            return Success(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetTotalForDay() {
            var user = GetUser();
            DataTable dt = dal.GetTotalForDay(user.id).Tables[0];
            return Success(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetTotalAmount() {
            var user = GetUser();
            DataTable dt = dal.GetTotal(user.id).Tables[0];
            return Success(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        }

        [HttpGet]
        public ActionResult GetMaxMoney() {
            var user = GetUser();
            DataTable dt = dal.GetMaxMoney(user.id).Tables[0];
            return Success(Newtonsoft.Json.JsonConvert.SerializeObject(dt));
        }

    }
}
