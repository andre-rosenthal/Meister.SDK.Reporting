using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeisterSDKReporting.Controllers
{
    public class MeisterConnectionsController
    {
        public List<MeisterConnectionsGateways> Gateways { get; set; }
        public MeisterConnectionsController()
        {
            Gateways = new List<MeisterConnectionsGateways>();
        }
    }

    public class MeisterConnectionsGateways
    {
        public MeisterConnectionsGateways(string url, int port, string client, bool isODataV4)
        {
            Url = url + ':' + port;
            Uri uri = null;
            if (Uri.TryCreate(Url, UriKind.Absolute, out uri) && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                Uri = uri;
            Client = client;
            Port = port;
            IsODataV4 = isODataV4;
        }
        public string Url { get; internal set; }
        public int Port { get; internal set; }
        public string Client { get; internal set; }
        public bool IsODataV4 { get; internal set; }
        public Uri Uri { get; internal set; }
    }
}
