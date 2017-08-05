using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core;

namespace web.Controllers
{
    [Route("/api/employee")]
    public class Employee : BaseController
    {
        private IEmployeePaymentService EmployeeService { get; set; }
        public Employee(IUserAuthenticationService auth,
                        IEmployeePaymentService empService): base(auth)
        {
            EmployeeService = empService;
        }

        // get single employee by id
        // GET: /api/employee/1
        [HttpGet("{id:long}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                // check auth
                if(!await CheckAccess())
                {
                    return NoAccess();
                }
                // return employee
				var emp = await EmployeeService.GetEmployee(id);
				return Ok(emp);
            }
            catch(Exception e)
            {
                return ErrorResponse("Error while loading employee");
            }
        }

        // add employee
        // POST: /api/employee
        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]core.Domain.Employee employee)
        {
            try
            {
				// check auth
				if (!await CheckAccess())
				{
					return NoAccess();
				}
				// return add/update respose
                var res = await EmployeeService.AddUpdateEmployee(employee);
				return Ok(res);
            }
			catch (Exception e)
			{
				return ErrorResponse("Error while saving employee");
			}
        }
    }
}