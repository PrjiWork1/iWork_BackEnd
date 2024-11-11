using IWork.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IWork.Domain.Requests
{
    public class PreferenceRequest
    {
        public List<PrefenceItem> Items { get; set; } = new List<PrefenceItem>();
        // public List<PreferenceBackUrls> BackUrls { get; set; } = new List<PreferenceBackUrls>();
    }
}
