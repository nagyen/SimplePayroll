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
        private IEmployeePaymentService EmployeeService { get; }

		public ListingController(IUserAuthenticationService auth,
                                 IEmployeePaymentService empService,
                                 IListingService listingService) : base(auth)
        {
            ListingService = listingService;
            EmployeeService = empService;
        }

		// employee listing screen
		public async Task<IActionResult> Index()
        {
            // todo: check auth
            //if (!await CheckAccess())
            //{
            //    return Redirect("/");
            //}

            var emplList = await EmployeeService.GetAllEmployees();
            var stateList = SeedingHelpers.StatesGenerator.List;
            var model = new core.Models.LisitngModels.ViewModel
            {
                EmplList = emplList.Select(x => new GenericModels.DropdownListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.FirstName} {x.LastName}"
                }),
                StatesList = stateList.Select(x => new GenericModels.DropdownListItem
				{
                    Value = x.Abbreviation,
                    Text = $"{x.Name}"
				})
            };
            return View(model);
        }

        // get employee listing results filtered
        [HttpPost]
        public async Task<IActionResult> GetListFiltered(LisitngModels.ListingRequest request)
        {
            // return filtered list
            return Ok(await ListingService.GetListFiltered(request));
        }

    }
}
