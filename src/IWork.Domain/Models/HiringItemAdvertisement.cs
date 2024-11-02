using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class HiringItemAdvertisement
    {
        public HiringItemAdvertisement(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public Guid Id { get; set; }
        public Guid HiringAdvertisementId { get; set; }
        public string Name { get; set; }
        //public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
