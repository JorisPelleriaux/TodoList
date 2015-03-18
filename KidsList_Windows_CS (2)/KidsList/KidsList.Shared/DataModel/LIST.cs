using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KidsList
{
    public class LIST
    {
        public int ID { get; set; }

        [JsonProperty(PropertyName = "TASK")]
        public string TASK { get; set; }
    }
}
