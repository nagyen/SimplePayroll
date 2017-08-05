using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using core;

namespace web.Controllers
{
    [Route("/api/[controller]")]
    public class Employee : BaseController
    {
        private IEmployeePaymentService EmployeeService { get; set; }
        public Employee(IUserAuthenticationService auth,
                        IEmployeePaymentService empService): base(auth)
        {
            EmployeeService = empService;
        }

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
                return emp == null ? ErrorResponse("Cannot find employee.") : Ok(emp);
            }
            catch(Exception)
            {
                return ErrorResponse("Error while loading employee");
            }
        }

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
                if(res.Success)
                {
				    return Ok(res);
                }
                // return error
                return ErrorResponse(res.Errors);
            }
			catch (Exception)
			{
				return ErrorResponse("Error while saving employee");
			}
        }
    }
}