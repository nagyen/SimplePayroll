using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core;
using core.Models;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IUserAuthenticationService auth, 
                              IEmployeePaymentService emp) : base(auth)
        {
        }

        // login page
        public async Task<IActionResult> Index()
        {
            // go to employee listing screen if logged in already
            if (await CheckAccess())
            {
                return Redirect("/listing");
            }

            // else show login page
            return View();
        }

        // employee listing screen
        public IActionResult Listing()
        {
            return View();
        }

        // get employee listing results filtered
        public IActionResult GetListFiltered([FromBody]EmpPayModels.ListingRequest request)
        {
            // todo: return filtered list
            return Json(0);
        }

        // error
        public IActionResult Error()
        {
            return View();
        }
    }
}
