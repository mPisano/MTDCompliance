using PX.Data;
using PX.OAuthClient.DAC;

namespace PX.HMRC.DAC
{
    public class HMRCOAuthApplication: OAuthApplication
    {
        #region OAuthApplication
        public new abstract class applicationID : IBqlField { }
        public new abstract class type : IBqlField { }
        public new abstract class applicationName : IBqlField { }
        public new abstract class clientID : IBqlField   { }
        public new abstract class clientSecret : IBqlField { }
        #endregion

        #region ApplicationName
        public abstract class usrServerUrl : IBqlField{  }
        [PXDBString(100, IsUnicode = true)]
        [PXDefault("")]
        [PXUIField(DisplayName = "Server Url")]
        public virtual string UsrServerUrl { get; set; }
        #endregion
    }
}
