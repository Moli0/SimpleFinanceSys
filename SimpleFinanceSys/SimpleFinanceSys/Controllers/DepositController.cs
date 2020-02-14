using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class DepositController : Controller
    {
        //
        // GET: /Deposit/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DrawMoney() {
            return View();
        }

    }
}
