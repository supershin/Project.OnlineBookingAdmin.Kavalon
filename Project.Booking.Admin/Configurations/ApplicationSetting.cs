using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Booking.Admin.Configurations
{
    public class ApplicationSetting
    {
        public string AdminTitleID { get; set; }
        public string UrlSalekit { get; set; }
        public SignalRConfig SignalRConfig { get; set; }
    }
    public class SignalRConfig
    {
        public string Url { get; set; }
    }

}
