using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class PreferenceBackUrls
    {
        public PreferenceBackUrls(string success, string failure, string pending)
        {
            Success = success;
            Failure = failure;
            Pending = pending;
        }

        public string Success { get; set; }
        public string Failure { get; set; }
        public string Pending { get; set; }
    }
}
