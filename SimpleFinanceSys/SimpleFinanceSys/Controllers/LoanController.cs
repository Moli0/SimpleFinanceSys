using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleFinanceSys.Controllers
{
    public class LoanController : Controller
    {
        //
        // GET: /Loan/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Repayment() {
            return View();
        }

    }
}
