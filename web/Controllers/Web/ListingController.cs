using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core;
using core.Models;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    public class ListingController : BaseController
    {
        public ListingController(IUserAuthenticationService auth) : base(auth)
        {
        }

		// employee listing screen
		public async Task<IActionResult> Index()
        {
            // check auth
            if (!await CheckAccess())
            {
                return Redirect("/");
            }

            return View();
        }

        // get employee listing results filtered
        [HttpPost]
        public IActionResult GetListFiltered([FromBody]LisitngModels.ListingRequest request)
        {
            // todo: return filtered list
            return Json(0);
        }
    }
}
