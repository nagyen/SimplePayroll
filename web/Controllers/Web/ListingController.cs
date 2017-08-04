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
        private IListingService ListingService { get; }

        public ListingController(IUserAuthenticationService auth,
                                 IListingService listingService) : base(auth)
        {
            ListingService = listingService;
        }

		// employee listing screen
		public async Task<IActionResult> Index()
        {
            // check auth
            //if (!await CheckAccess())
            //{
            //    return Redirect("/");
            //}

            return View();
        }

        // get employee listing results filtered
        [HttpPost]
        public async Task<IActionResult> GetListFiltered(LisitngModels.ListingRequest request)
        {
            // todo: return filtered list
            return Ok(await ListingService.GetListFiltered(request));
        }

    }
}
