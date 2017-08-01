﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Domain;

namespace core.Services
{
    public class EmployeePaymentService
    {
        #region Employee
        
        // add or update employee
        public async Task<EmpPayModels.EmpAddUpdateFeedback> AddUpdateEmployee(Employee employee)
        {
            // check required fields
            if (string.IsNullOrEmpty(employee.EmplId) || string.IsNullOrEmpty(employee.SSN))
            {
                // return error
                return new EmpPayModels.EmpAddUpdateFeedback
                {
                    Success = false,
                    Errors = "Invalid Employee Id/SSN."
                };
            }

            using (var db = new AppDbContext())
            {
                // check if employee exists
                var prevEmpl = await db.Employees.FirstOrDefaultAsync(x => x.EmplId == employee.EmplId || x.SSN == employee.SSN);

                // if adding new employee
                if (employee.Id == 0)
                {
                    // check if employee exists
                    if (prevEmpl != null)
                    {
                        // return error
                        return new EmpPayModels.EmpAddUpdateFeedback
                        {
                            Success = false,
                            Errors = "Employee alerady exists."
                        };
                    }

                    // if here valid employee
                    await db.Employees.AddAsync(employee);
                    await db.SaveChangesAsync();
                }
                else
                {
                    // check if employee exists
                    if (prevEmpl == null)
                    {
                        // return error
                        return new EmpPayModels.EmpAddUpdateFeedback
                        {
                            Success = false,
                            Errors = "Employee doesnot exist."
                        };
                    }

                    // if here update prev employee
                    prevEmpl.Insurance = employee.Insurance;
                    prevEmpl.Retirement401k = employee.Retirement401k;
                    prevEmpl.Retirement401kPreTax = employee.Retirement401kPreTax;
                    db.Employees.Update(prevEmpl);
                    await db.SaveChangesAsync();
                }


                // if here success
                return new EmpPayModels.EmpAddUpdateFeedback
                {
                    Success = true,
                    EmpId = employee.EmplId
                };
            }
        }

        // get single employee by empl id
        public async Task<Employee> GetEmployee(string emplId)
        {
            using (var db = new AppDbContext())
            {
                // check if employee exists
                return await db.Employees.FirstOrDefaultAsync(x => x.EmplId == emplId);
            }
        }

        #endregion

        #region Payments

        // todo: record payment
        //public async Task<EmpPayModels.PaymentFeedback> RecordPayment(Payment payment)
        //{
            
        //}

        #endregion
    }
}
