using System;
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

        // record payment
        Task<EmpPayModels.PaymentFeedback> RecordPayment(Payment payment);
    }
}
