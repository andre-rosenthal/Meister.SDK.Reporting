using System.Collections.Generic;

namespace MeisterSDKReporting.MeisterModel
{
    public partial class ReportsByUser
    {
        public ReportsByUser()
        {
            Reports = new List<MyReport>();
        }
        public string User { get; set; }
        public List<MyReport> Reports { get; set; }
    }
}
