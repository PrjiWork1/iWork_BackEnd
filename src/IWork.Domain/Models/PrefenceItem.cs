using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IWork.Domain.Models
{
    public class PrefenceItem
    {
        public PrefenceItem(string title, string id, int unitPrice)
        {
            Title = title;
            Id = id;
            UnitPrice = unitPrice;
        }

        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("unitprice")]
        public int UnitPrice { get; set; }
    }
}
