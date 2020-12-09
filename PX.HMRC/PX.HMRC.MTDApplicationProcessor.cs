using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using PX.HMRC.Browser;


namespace PX.HMRC
{
    public class MTDApplicationProcessor
    {
        private const string _isSignedAcceptHeader = "application/vnd.hmrc.1.0+json";
        private const string _scope = "read:vat+write:vat";

        private const string _authorizationUrl = "/oauth/authorize";
        private const string _tokenUrl = "/oauth/token";


        private string authorizationUrl;
        private string tokenUrl;
        const string SuccessTitle = "Success code";
        public bool HasRefreshToken => true;

        public string SignInFailedMessage => throw new NotImplementedException();

        public MTDApplicationProcessor(string urlSite)
        {
            authorizationUrl = urlSite + _authorizationUrl;
            tokenUrl = urlSite + _tokenUrl;
        }

        /// <summary>
        /// Uses the Embedded Broswer for login and auto process the AccessCode 
        /// </summary
        public async Task<BrowserResultType> SignIn(OAuthSettings oAuthApplication, OAuthToken token)
        {
            var request = new RequestUrl(authorizationUrl);
            var url = request.CreateAuthorizeUrl(
                clientId: oAuthApplication.ClientID,
                responseType: OidcConstants.ResponseTypes.Code,
                scope: _scope,
                state: oAuthApplication.ApplicationID.ToString(),
                redirectUri: oAuthApplication.ReturnUrl);

            var Browser = new HMRC.Browser.WinFormsBroswer();
            var browserOptions = new BrowserOptions(url, "", SuccessTitle);

            browserOptions.DisplayMode = DisplayMode.Visible;

            var browserResult = await Browser.InvokeAsync(browserOptions);
            if (browserResult.ResultType == BrowserResultType.Success)
            {
                var r = browserResult.Response.Split('&');
                Dictionary<String, String> p = new Dictionary<string, string>();
                foreach (var item in r)
                {
                    var r1 = item.Split('=');
                    p.Add(r1[0], r1[1]);
                }
                string code = p[SuccessTitle];
                ProcessAuthorizationCode(code, oAuthApplication, token);
            }
            return browserResult.ResultType;
        }

        /// <summary>
        /// Starts the default browser to obtain the AccessCode to be used with ProcessAuthorizationCode()
        /// </summary>
        public void SignIn(OAuthSettings oAuthApplication)
        {
            var request = new RequestUrl(authorizationUrl);
            var url = GetSigninURL(oAuthApplication);
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// Created a Login URL incase you want to Call a different Browser from SignIn()
        /// </summary>
        public string GetSigninURL(OAuthSettings oAuthApplication)
        {
            var request = new RequestUrl(authorizationUrl);
            var url = request.CreateAuthorizeUrl(
                clientId: oAuthApplication.ClientID,
                responseType: OidcConstants.ResponseTypes.Code,
                scope: _scope,
                state: oAuthApplication.ApplicationID.ToString(),
                redirectUri: oAuthApplication.ReturnUrl);
            //Wish I could send this in!
            //UserName = oAuthApplication.UserID,
            //Password = oAuthApplication.Password,
            return url;
        }


        public void SignOut(OAuthSettings application, OAuthToken token)
        {
            //Nothing to do at API Level
        }

        /// <summary>
        /// Process Authorization Code from External Browser and request Token
        /// </summary>
        public bool ProcessAuthorizationCode(string code, OAuthSettings application, OAuthToken token)
        {
            AuthorizationCodeTokenRequest request = new AuthorizationCodeTokenRequest()
            {
                ClientId = application.ClientID,
                ClientSecret = application.ClientSecret,
                Code = code,
                RequestUri = new Uri(tokenUrl),
                RedirectUri = application.ReturnUrl
            };

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    Task<TokenResponse> T = Task.Run(() => httpClient.RequestAuthorizationCodeTokenAsync(request));
                    TokenResponse tokenResponse = T.Result;
                    FillTokenFromTokenResponse(tokenResponse, token);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void FillTokenFromTokenResponse(TokenResponse tokenResponse, OAuthToken token)
        {
            token.AccessToken = tokenResponse.AccessToken;
            token.RefreshToken = tokenResponse.RefreshToken;
            token.UtcExpiredOn = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
            token.Bearer = tokenResponse.TokenType;
        }

        public void RefreshAccessToken(OAuthSettings oAuthApplication, OAuthToken token)
        {
            if (token.RefreshToken == null)
                throw new Exceptions.VATAPIInvalidToken(Model.error.REFRESH_TOKEN_IS_MISSING);
            var request = new RefreshTokenRequest()
            {
                ClientId = oAuthApplication.ClientID,
                ClientSecret = oAuthApplication.ClientSecret,
                RefreshToken = token.RefreshToken,
                RequestUri = new Uri(tokenUrl)
            };

            using (HttpClient httpClient = new HttpClient())
            {
                Task<TokenResponse> T = Task.Run(() => httpClient.RequestRefreshTokenAsync(request));
                TokenResponse tokenResponse = T.Result;
                if (!tokenResponse.IsError)
                {
                    FillTokenFromTokenResponse(tokenResponse, token);
                    return;
                }
                string error = tokenResponse.Error;
                string error_description = "";
                try
                {
                    error_description = (((Newtonsoft.Json.Linq.JValue)tokenResponse.Json["error_description"]).Value).ToString();
                }
                catch (Exception) { }

                Exceptions.VATAPIInvalidToken e = new Exceptions.VATAPIInvalidToken(Model.error.IMPOSSIBLE_TO_REFRESH_TOKEN);
                e.Data.Add("AccessToken", token.AccessToken);
                e.Data.Add("RefreshToken", token.RefreshToken);
                e.Data.Add("Raw", tokenResponse.Raw);
                throw e;
            }
        }
    }
}
