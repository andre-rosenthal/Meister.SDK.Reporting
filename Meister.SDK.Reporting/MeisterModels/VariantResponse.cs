using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeisterSDKReporting.MeisterModel
{
    /// <summary>
    /// Summary description for VariantResponse
    /// </summary>
    public partial class VariantResponse
    {
        [JsonProperty("report")]
        public string Report { get; set; }

        [JsonProperty("variants")]
        public List<Variant> Variants { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
}