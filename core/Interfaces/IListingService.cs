﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using core.Models;

namespace core
{
    interface IListingService
    {
        Task<LisitngModels.ListingResult> GetListFiltered(LisitngModels.ListingRequest request);
    }
}
