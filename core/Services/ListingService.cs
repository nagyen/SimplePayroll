using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using core.Domain;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Services
{
    public class ListingService: IListingService
    {
        // get employee list filtered
        public async Task<LisitngModels.ListingResult> GetListFiltered(LisitngModels.ListingRequest request)
        {
            // filters
            var filters = new List<Expression<Func<Employee, bool>>>();

            // employee name filter
            if (request.EmpId.HasValue)
            {
                filters.Add(x => x.Id == request.EmpId);
            }

            // state filter
            if (!string.IsNullOrEmpty(request.State))
            {
                filters.Add(x => x.State == request.State);
            }

            // pay posting date start
            if (request.PayPostingFrom.HasValue)
            {
                filters.Add(x => x.Payments.Any(y => y.CreateDateTime >= request.PayPostingFrom));
            }

            // pay posting date end
            if (request.PayPostingTo.HasValue)
            {
                filters.Add(x => x.Payments.Any(y => y.CreateDateTime < request.PayPostingTo.Value.AddDays(1)));
            }
            
            // get the filtered list
            List<Employee> filteredList;
            using (var db = new AppDbContext())
            {
                // get all employees with their payments
                var employees = db.Employees.Include(x => x.Payments).OrderByDescending(x => x.Id).AsQueryable();

                // apply filters
                if (filters.Any())
                {
                    foreach (var filter in filters)
                    {
                        employees = employees.Where(filter);
                    }
                }

                // get filtered list
                filteredList = await employees.ToListAsync();
            }
            
            // free form search
            if (!string.IsNullOrEmpty(request.SearchPhrase))
            {
                // get space seperated words
                var words = request.SearchPhrase.Split(' ');

                foreach (var word in words)
                {
                    var lowerword = word.ToLower();

                    filteredList = filteredList.Where(x =>
                            // search applicaple fields
                            x.FirstName.ToLower().Contains(lowerword) ||
                            x.LastName.ToLower().Contains(lowerword) ||
                            x.State.ToLower().Contains(lowerword) ||
                            x.Payments.Any(y => y.GrossPay.ToString().Equals(lowerword)) ||
                            x.Payments.Any(y => y.NetPay.ToString().Equals(lowerword))
                    ).ToList();
                }
            }

            // get transformed list
            var transformedList = filteredList.Select(TransformListingItem).ToList();

            // sort the list
            var sortedList = GetSortedList(transformedList, request.Sort);

            // get parial list
            var partialList = GetPartialList(request.Current, request.RowCount, sortedList);

            // return list
            return new LisitngModels.ListingResult
            {
                Current = request.Current,
                RowCount = request.RowCount,
                Total = filteredList.Count,
                Rows = partialList
            };
        }

        //get transformed list
        public static LisitngModels.ListingItem TransformListingItem(Employee employee)
        {
            var lastPayment = employee.Payments.OrderByDescending(x => x.CreateDateTime).FirstOrDefault();
            var ytdPay = employee.Payments.Where(x => x.CreateDateTime >= new DateTime(DateTime.Now.Year, 1, 1)).Sum(x => x.GrossPay);
            return new LisitngModels.ListingItem
            {
                EmpId = employee.Id,
                FullName = $"{employee.FirstName} {employee.LastName}",
                State = employee.State,
                LastPaymentDate = lastPayment?.CreateDateTime.ToString("d") ?? "",
                LastPaymentAmount = lastPayment?.GrossPay.ToString() ?? "",
                YtdPay = ytdPay.ToString()
            };
        }

        // function to get sorted list
        public static List<LisitngModels.ListingItem> GetSortedList(IEnumerable<LisitngModels.ListingItem> rows, Dictionary<string, string> sort)
        {
            // check if any sort
            if (sort != null && sort.Keys.Any())
            {
                // get the first key and direction
                var sortKey = sort.Keys.First();
                var dir = sort[sortKey];

                // check if any custom sort
                Func<LisitngModels.ListingItem, object> sortFunc;
                switch (sortKey.ToLower())
                {
                    case "fullname":
                    {
                        sortFunc = x => x.FullName;
                        break;
                    }
                    case "lastpaymentdate":
                    {
                        sortFunc = x => x.LastPaymentDate;
                        break;
                    }
                    case "lastpaymentamount":
                    {
                        sortFunc = x => x.LastPaymentAmount;
                        break;
                    }
                    case "ytdpay":
                    {
                        sortFunc = x => x.YtdPay;
                        break;
                    }
                    default:
                    {
                        sortFunc = null;
                        break;
                    }
                }
                if (sortFunc != null)
                {
                    if (dir == "desc")
                    {
                        return rows.OrderByDescending(sortFunc).ToList();
                    }
                    else
                    {
                        return rows.OrderBy(sortFunc).ToList();
                    }
                }
            }

            // if here, reutrn the original list
            return rows.ToList();
        }

        // function that returns a filtered list
        public static List<LisitngModels.ListingItem> GetPartialList(int current, int rowCount, IEnumerable<LisitngModels.ListingItem> rows)
        {
            // get the list            
            int count = rows.Count();

            // get the portion we need - all if -1
            var partial = rowCount == -1 ? rows : rows.Skip((current - 1) * rowCount).Take(rowCount).ToList();

            // return 
            return partial.ToList();
        }

    }
}
