using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;
using PX.Data;
using PX.OAuthClient.DAC;
using PX.OAuthClient.Handlers;
using PX.OAuthClient.Processors;

namespace PX.HMRC
{
	public class MTDApplicationProcessor : IExternalApplicationProcessor
	{
		private const string _isSignedAcceptHeader = "application/vnd.hmrc.1.0+json";
		private const string _scope = "read:vat+write:vat";
		private string _urlSite = "https://test-api.service.hmrc.gov.uk";
		private const string _authorizationUrl = "/oauth/authorize";
		private const string _tokenUrl = "/oauth/token";
		private const string _isSignedUrl = "/hello/application";

		private string urlSite= "https://test-api.service.hmrc.gov.uk";

		private string authorizationUrl = "https://test-api.service.hmrc.gov.uk/oauth/authorize";
		private string tokenUrl			= "https://test-api.service.hmrc.gov.uk/oauth/token";
		private string isSignedUrl		= "https://test-api.service.hmrc.gov.uk/hello/application";

		public string TypeName => "HMRC MTD";
		public string TypeCode => "RCMTD";

		public bool HasRefreshToken => true;

		public string SignInFailedMessage => throw new NotImplementedException();

		public MTDApplicationProcessor(string UrlSite=null) {
			urlSite = String.IsNullOrEmpty(UrlSite) ? _urlSite : UrlSite;

			authorizationUrl = urlSite + _authorizationUrl;
			tokenUrl = urlSite + _tokenUrl;
			isSignedUrl = urlSite + _isSignedUrl;
		}

		public void SignIn(OAuthApplication oAuthApplication, ref OAuthToken token)
		{
			var request = new AuthorizeRequest(authorizationUrl);
			var url = request.CreateAuthorizeUrl(
				clientId: oAuthApplication.ClientID,
				responseType: OidcConstants.ResponseTypes.Code,
				scope: _scope,
				state: oAuthApplication.ApplicationID.ToString(),
				redirectUri: AuthenticationHandler.ReturnUrl);

			throw new PXRedirectToUrlException(url, PXBaseRedirectException.WindowMode.InlineWindow, "Authenticate");
		}

		public void ProcessAuthorizationCode(string code, OAuthApplication application, OAuthToken token)
		{
			var tokenClient = new TokenClient(tokenUrl, application.ClientID, application.ClientSecret, AuthenticationStyle.PostValues);
			var tokenResponse = tokenClient.RequestAuthorizationCodeAsync(code, AuthenticationHandler.ReturnUrl).Result;

			FillTokenFromTokenResponse(tokenResponse, token);
		}

		private static void FillTokenFromTokenResponse(TokenResponse tokenResponse, OAuthToken token)
		{
			token.AccessToken = tokenResponse.AccessToken;
			token.RefreshToken = tokenResponse.RefreshToken;
			token.UtcExpiredOn = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
			token.Bearer = tokenResponse.TokenType;
			
		}

		public void RefreshAccessToken(OAuthToken token, OAuthApplication oAuthApplication)
		{
			var refreshToken = token.RefreshToken;
			var client = new TokenClient(tokenUrl, oAuthApplication.ClientID, oAuthApplication.ClientSecret, AuthenticationStyle.PostValues);
			var tokenResponse = client.RequestRefreshTokenAsync(refreshToken).Result;
			if (!tokenResponse.IsError)
			{
				FillTokenFromTokenResponse(tokenResponse, token);
				return;
			}
			string error = tokenResponse.Error;
			string error_description = "";
			try{
				error_description = (((Newtonsoft.Json.Linq.JValue)tokenResponse.Json["error_description"]).Value).ToString();
			}catch (Exception){}

			Exceptions.VATAPIInvalidToken e = new Exceptions.VATAPIInvalidToken(Model.error.IMPOSSIBLE_TO_REFRESH_TOKEN);
			e.Data.Add("AccessToken", token.AccessToken);
			e.Data.Add("RefreshToken", token.RefreshToken);
			e.Data.Add("Raw", tokenResponse.Raw);

			throw e;
		}

		public Task<IEnumerable<Resource>> GetResources(OAuthToken token, OAuthApplication application)
		{
			return Task.FromResult(Enumerable.Empty<Resource>());
		}

		public bool IsSignedIn(OAuthApplication application, OAuthToken token)
		{
			if (application == null || token == null)
			{
				return false;
			}

			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, isSignedUrl);
			request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_isSignedAcceptHeader));
			request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			var httpClient = new HttpClient();
			var response = httpClient.SendAsync(request).Result;

			return response?.StatusCode == HttpStatusCode.OK;
		}

		public string ResourceHtml(OAuthToken token, OAuthResource resource, OAuthApplication application)
		{
			return string.Empty;
		}
	}


}
