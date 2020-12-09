using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
namespace PX.HMRC
{
    public class OAuthToken 
    {
        public DateTime? UtcExpiredOn;
        public string AccessToken;
        public string RefreshToken;
        public string Bearer;
    }
}
