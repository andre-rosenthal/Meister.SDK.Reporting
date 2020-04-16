﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace MeisterSDKReporting.MeisterModel
{
    public partial class ReportRunResponse
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("messages")]
        public List<Message> Messages { get; set; }
    }
}