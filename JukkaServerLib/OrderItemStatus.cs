using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JukkaServerLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OrderItemStatus
    {
        [JsonProperty]
        public string MachineId { get; set; }
        [JsonProperty]
        public int OrderId { get; set; }
        [JsonProperty]
        public int ItemId { get; set; }
        [JsonProperty]
        public int ProductId { get; set; }
        [JsonProperty]
        public string Status { get; set; }

        public OrderItemStatus()
        {

        }

        public OrderItemStatus(string machineId, int orderId, int itemId, int productId, string status)
        {
            MachineId = machineId;
            OrderId = orderId;
            ItemId = itemId;
            ProductId = productId;
            Status = status;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
