using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JukkaServerLib
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MachineStatus
    {
        [JsonProperty]
        public string MachineId { get; set; }
        [JsonProperty]
        public string Status { get; set; }
        [JsonProperty]
        public string Reason { get; set; }
        [JsonProperty]
        public DateTime TimeStamp { get; set; }

        public MachineStatus()
        {

        }

        public MachineStatus(string mId, string status, string reason, DateTime timeStamp)
        {
            MachineId = mId;
            Status = status;
            Reason = reason;
            TimeStamp = timeStamp;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
