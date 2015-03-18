using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace KidsList
{
    public class CHILDREN
    {
        public int ID { get; set; }

        [JsonProperty(PropertyName = "NAME")]
        public char[] NAME { get; set; }

        [JsonProperty(PropertyName = "USERNAME")]
        public char[] USERNAME { get; set; }

        [JsonProperty(PropertyName = "PASSWORD")]
        public char[] PASSWORD { get; set; }
    }
}
