using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using core.Models;
using core.Domain;

namespace core
{
    public interface IEmployeePaymentService
    {
		// add or update employee
		Task<EmpPayModels.EmpAddUpdateFeedback> AddUpdateEmployee(Employee employee);

		// get single employee by empl id
		Task<Employee> GetEmployee(long id);

        // get all employees
        Task<List<Employee>> GetAllEmployees();

        // record payment
        Task<EmpPayModels.PaymentFeedback> RecordPayment(Payment payment);

        // get all employee with payments
        Task<IEnumerable<Employee>> GetAllEmployeesWithPayments();

        // get all payments for employee
        Task<IEnumerable<Payment>> GetAllPaymentsForEmployee(long empId);
	    
	    // get paged results of employee payments
	    Task<IEnumerable<Payment>> GetPagedPaymentsForEmployee(long empId, int page);
	    
	    // get ytd gross and net pay
	    Task<Tuple<decimal, decimal>> GetYtdPay(long empId);
    }
}
