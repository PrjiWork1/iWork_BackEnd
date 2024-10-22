using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Requests
{
    public class AdvertisementNumberOfSalesRequest
    {
        public AdvertisementNumberOfSalesRequest(int numberOfSales)
        {
            NumberOfSales = numberOfSales;
        }

        public int NumberOfSales { get; set; }
    }
}
