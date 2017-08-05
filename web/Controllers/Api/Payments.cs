﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core;
using Microsoft.AspNetCore.Mvc;

namespace web.Controllers
{
    [Route("api/[controller]")]
    public class Payments : BaseController
    {
        private IEmployeePaymentService PaymentService { get; set; }
        public Payments(IUserAuthenticationService auth,
                       IEmployeePaymentService paymentService) : base(auth)
        {
            PaymentService = paymentService;
        }

        // GET api/payments/5
        [HttpGet("{empId:long}")]
        public async Task<IActionResult> Get(int empId, [FromQuery]int page = 1)
        {
			try
			{
                // check auth
				if (!await CheckAccess())
				{
					return NoAccess();
				}

                // return payements for employee
                var payments = await PaymentService.GetPagedPaymentsForEmployee(empId, page);
                return Ok(payments);
			}
			catch (Exception)
			{
				return ErrorResponse("Error while getting payements.");
			}
        }

        // POST api/payments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]core.Domain.Payment payment)
        {
			try
			{
				// check auth
				if (!await CheckAccess())
				{
					return NoAccess();
				}

                // record new payment
                var res = await PaymentService.RecordPayment(payment);
                if(res.Success)
                {
				    return Ok(res);
                }
                // return error
                return ErrorResponse(res.Errors);
			}
			catch (Exception)
			{
				return ErrorResponse("Error while getting payements.");
			}
        }
    }
}
