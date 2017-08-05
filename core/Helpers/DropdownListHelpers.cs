using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using core.Models;
using core.Services;
namespace core
{
    public class DropdownListHelpers
    {
        // states drop down list
        public static IEnumerable<GenericModels.DropdownListItem> GetStatesList()
        {
            var stateList = SeedingHelpers.StatesGenerator.List;
            return stateList.Select(x => new GenericModels.DropdownListItem
            {
                Value = x.Abbreviation,
                Text = $"{x.Name}"
            });
        }
        
        // employee dropdown list
        public static async Task<IEnumerable<GenericModels.DropdownListItem>> GetEmployeeList()
        {
            var emplList = await new EmployeePaymentService().GetAllEmployees();
            return emplList.Select(x => new GenericModels.DropdownListItem
            {
                Value = x.Id.ToString(),
                Text = $"{x.FirstName} {x.LastName}"
            });
        }
    }
}