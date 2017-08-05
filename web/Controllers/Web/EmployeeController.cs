using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core;
using core.Models;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers.Web
{
    public class EmployeeController : BaseController
    {
        private IEmployeePaymentService EmployeePaymentService { get; set; }
        private IListingService ListingService { get; }

        public EmployeeController(IUserAuthenticationService auth,
                                          IEmployeePaymentService empPayService,
                                          IListingService listingService) : base(auth)
        {
            EmployeePaymentService = empPayService;
            ListingService = listingService;
        }

        // employee listing screen
        public async Task<IActionResult> Listing()
        {
            // check auth
            if (!await CheckAccess())
            {
                return Redirect("/");
            }

            var emplList = await EmployeePaymentService.GetAllEmployees();
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
        
        // employee payments page
        [HttpGet("/[controller]/{empId}/[action]")]
        public async Task<IActionResult> Payments(long empId)
        {
            if (!await CheckAccess())
            {
                // show login screen if not authenticated
                return Redirect("/");
            }
            return View(empId);
        }

        // add employee partial
        public IActionResult AddEmployeeDialog()
        {
            return View("~/Views/Shared/Partials/AddEmployee.cshtml");
        }
    }
}
