using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using core.Models;
using core.Domain;

namespace core.Services
{
    public class EmployeePaymentService: IEmployeePaymentService
    {
        // constructor
        public EmployeePaymentService()
        {
            
        }

        #region Employee
        
        // add or update employee
        public async Task<EmpPayModels.EmpAddUpdateFeedback> AddUpdateEmployee(Employee employee)
        {
            // check required fields
            if (string.IsNullOrEmpty(employee.SSN))
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
                var prevEmpl = await db.Employees.FirstOrDefaultAsync(x => x.SSN == employee.SSN);

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

                    // if here update employee
                    prevEmpl.Insurance = employee.Insurance;
                    prevEmpl.Retirement401kPercent = employee.Retirement401kPercent;
                    prevEmpl.Retirement401kPreTax = employee.Retirement401kPreTax;
                    prevEmpl.State = employee.State;
                    db.Employees.Update(prevEmpl);
                    await db.SaveChangesAsync();
                }

                // if here success
                return new EmpPayModels.EmpAddUpdateFeedback
                {
                    Success = true,
                    EmpId = employee.Id
                };
            }
        }

        // get single employee by empl id
        public async Task<Employee> GetEmployee(long id)
        {
            using (var db = new AppDbContext())
            {
                // check if employee exists
                return await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        // get all employees
        public async Task<List<Employee>> GetAllEmployees()
        {
            using (var db = new AppDbContext())
            {
                // check if employee exists
                return await db.Employees.OrderBy(x => x.FirstName).ThenBy(x => x.LastName).ToListAsync();
            }
        }

        // get all employee with payments
        public async Task<IEnumerable<Employee>> GetAllEmployeesWithPayments()
        {
            using (var db = new AppDbContext())
            {
                return await db.Employees.Include(x => x.Payments).ToListAsync();
            }
        }

        #endregion

        #region Payments

        // record payment
        public async Task<EmpPayModels.PaymentFeedback> RecordPayment(Payment payment)
        {
            // check if valid employee
            var empl = await GetEmployee(payment.EmpId);
            if(empl == null)
            {
                // return error
                return new EmpPayModels.PaymentFeedback
                {
                    Success = false,
                    Errors = "Invalid Employee."
                };
            }

            // check if valid gross pay
            if(payment.GrossPay <= 0)
            {
                // return error
				return new EmpPayModels.PaymentFeedback
				{
					Success = false,
					Errors = "Invalid Pay. Gross pay should be more than 0."
				};
            }

            // final pay after deductions
            var netpay = new TaxCalculationService().CalculateFinalPayAfterDeductions(empl, payment.GrossPay);

            // check if valid pay
            if(netpay < 0)
            {
                // return error
				return new EmpPayModels.PaymentFeedback
				{
					Success = false,
					Errors = "Invalid Pay. Net pay cannot be less than 0."
				};
            }

            // if here valid pay
            // set payment fields
            payment.NetPay = Math.Round(netpay, 2);
            payment.CreateDateTime = DateTime.Now;

            // save
            using(var db = new AppDbContext())
            {
                await db.Payments.AddAsync(payment);
                await db.SaveChangesAsync();
            }

            // return success
            return new EmpPayModels.PaymentFeedback
            {
                Success = true,
                EmpId = empl.Id
            };
        }

        // get all payments for employee
        public async Task<IEnumerable<Payment>> GetAllPaymentsForEmployee(long empId)
        {
            using (var db = new AppDbContext())
            {
                return await db.Payments.Where(x => x.EmpId == empId).ToListAsync();
            }
        }
        
        // get paged results of employee payments
        public async Task<IEnumerable<Payment>> GetPagedPaymentsForEmployee(long empId, int page)
        {
            if (page < 1) page = 1;
            var limit = 10;
            var startFrom = (page - 1)  * limit;
            using (var db = new AppDbContext())
            {
                return await db.Payments.Where(x => x.EmpId == empId).OrderByDescending(x => x.CreateDateTime).Skip(startFrom).Take(limit).ToListAsync();
            }
        }

        #endregion
    }
}
