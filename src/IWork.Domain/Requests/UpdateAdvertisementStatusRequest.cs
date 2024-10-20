using IWork.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Requests
{
    public class UpdateAdvertisementStatusRequest
    {
        public UpdateAdvertisementStatusRequest(AdvertisementStatus status)
        {
            Status = status;
        }

        public AdvertisementStatus Status { get; set; }
    }
}
