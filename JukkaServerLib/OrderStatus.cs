using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JukkaServerLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OrderStatus
    {
        [JsonProperty]
        public string MachineId { get; set; }
        [JsonProperty]
        public int OrderId { get; set; }
        [JsonProperty]
        public string Status { get; set; }

        public OrderStatus() { }

        public OrderStatus(string mId, int oId, string status)
        {
            MachineId = mId;
            OrderId = oId;
            Status = status;
        }
  
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
