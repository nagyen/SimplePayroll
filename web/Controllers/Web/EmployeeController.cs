using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers.Web
{
    public class EmployeeController : BaseController
    {
        private IEmployeePaymentService EmployeePaymentService { get; set; }

        public EmployeeController(IUserAuthenticationService auth,
                                  IEmployeePaymentService empPayService) : base(auth)
        {
            EmployeePaymentService = empPayService;
        }

        // add employee partial
        public IActionResult AddEmployeeDialog()
        {
            return View("~/Views/Shared/Partials/AddEmployee.cshtml");
        }
    }
}
