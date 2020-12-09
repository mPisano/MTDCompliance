using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PX.HMRC
{
    public class OAuthSettings
    {
        public OAuthSettings()
        {
            ServerUrl = "https://test-api.service.hmrc.gov.uk";
            ReturnUrl = @"urn:ietf:wg:oauth:2.0:oob";
        }

        public string ApplicationName { get; set; }
        public string ApplicationID { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string VRN { get; set; }
        public string ReturnUrl { get; set; }
        public string ServerUrl { get; set; }
        //   public string UserID { get; set; }
        //   public string Password { get; set; }
    }
}
