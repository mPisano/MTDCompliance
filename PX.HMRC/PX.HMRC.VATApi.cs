using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using PX.OAuthClient.DAC;
using PX.HMRC.Model;
using PX.HMRC.Exceptions;
using Newtonsoft.Json;
using System.Text;

namespace PX.HMRC
{
    /// <summary>
    /// VAT (MTD) API
    /// https://developer.service.hmrc.gov.uk/api-documentation/docs/api/service/vat-api/1.0
    /// </summary>
	public class VATApi
	{
		private const string _acceptHeader = "application/vnd.hmrc.1.0+json";
		private const string _contentTypeHeader = "application/json";

		MTDApplicationProcessor ApplicationProcessor;
		private OAuthApplication application;
		private OAuthToken token;
		private string vrn;

		#region Endpoitnts 
		private const string _urlObligations = "/organisations/vat/{vrn}/obligations";
		private const string _urlReturns = "/organisations/vat/{vrn}/returns/{periodKey}";
		private const string _urlSubmitVATreturn = "/organisations/vat/{vrn}/returns";
		private const string _urlLiabilities = "/organisations/vat/{vrn}/liabilities";
		private const string _urlPayments = "/organisations/vat/{vrn}/payments";

		private string urlSite = "https://test-api.service.hmrc.gov.uk";
		#endregion

		public delegate void UpdateOAuthTokenDelegate(OAuthToken o);

		private UpdateOAuthTokenDelegate updateOAuthToken;

		public VATApi(string UrlSite, OAuthApplication Application, OAuthToken Token, string VRN, UpdateOAuthTokenDelegate UpdateOAuthToken=null)
        {
			application = Application;
			token = Token;
            urlSite = UrlSite;
			updateOAuthToken = UpdateOAuthToken;
            setVRN(VRN);

            ApplicationProcessor = new MTDApplicationProcessor(UrlSite);
		}

        public void setVRN(string VRN)
        {
            vrn = VRN;
        }

        public void SignIn()
		{
			OAuthToken _token = null;
			ApplicationProcessor.SignIn(application, ref _token);
			token = _token;
		}

		/// <summary>
		/// Refresh Access Token if it is need
		/// </summary>
		public void RefreshAccessToken()
        {
            if (token == null)
            {
                throw new Exceptions.VATAPIInvalidToken(Model.error.IMPOSSIBLE_TO_REFRESH_TOKEN);
            }
			if (token.UtcExpiredOn == null || token.UtcExpiredOn?.AddMinutes(-15) < token.UtcNow)
			{
				ApplicationProcessor.RefreshAccessToken(token, application);
                updateOAuthToken(token);
			}
		}

        /// <summary>
        /// Сheck Api Response
        /// </summary>
        /// <param name="response"></param>
        public void checkApiResponse(HttpResponseMessage response, VATApiType? api = null)
        {
            //if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created) return;
            if (response.IsSuccessStatusCode) return;

            string errJson = response.Content.ReadAsStringAsync().Result;
            error err = JsonConvert.DeserializeObject<error>(errJson);

            if (err == null)
            {
                err = new error();
                switch (response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        err.code = error.NOT_FOUND;
                        err.message = error.NOT_FOUND_MSG;
                        break;
                    default:
                        err.code = "HTTP_" + response.StatusCode.ToString();
                        break;
                }
            }
            string message = err.message;
            if (err.errors != null)
                message=String.Join("; ", err.errors.Select(o => o.message));

            Exception exception = null;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized: exception = new VATAPIInvalidToken(err.code, message); break;
                default:
                    exception = new VATAPIException(err.code, message);
                    break;
            }

            if (exception != null)
            {
               exception.Data.Add("errorJson", errJson);
               throw exception;
            }
        }

        /// <summary>
        /// Retrieve VAT obligations
        /// </summary>
        /// <returns></returns>
        public obligationResponse Obligations(obligationsRequest request, string testScenario=null)
        {
			RefreshAccessToken();
            string url = urlSite + _urlObligations.Replace("{vrn}", vrn) + "?from=" + request.from?.ToString("yyyy-MM-dd") + "&to=" + request.to?.ToString("yyyy-MM-dd");
			HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
			httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
			httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			if (!String.IsNullOrEmpty(testScenario))
			{
				httpRequest.Headers.Add("Gov-Test-Scenario", testScenario);
			}

			obligationResponse obligationResponse = null;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
                checkApiResponse(response, VATApiType.RetrieveVATobligations);

                var content = response.Content.ReadAsStringAsync();
                obligationResponse = JsonConvert.DeserializeObject<obligationResponse>(content.Result);
            }

			return obligationResponse;
		}

        /// <summary>
        /// Submit VAT return for period
        /// </summary>
        /// <param name="periodkey"></param>
        /// <returns></returns>
        public VATreturn Returns(string periodkey)
		{
			RefreshAccessToken();
            string url = urlSite + _urlReturns.Replace("{vrn}", vrn).Replace("{periodKey}", periodkey);
			HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
			httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
			httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

			VATreturn vATreturn = null;

			using (var httpClient = new HttpClient())
			{
				HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
                checkApiResponse(response, VATApiType.ViewVATReturn);

				var content = response.Content.ReadAsStringAsync();
				vATreturn = JsonConvert.DeserializeObject<VATreturn>(content.Result);
			}

			return vATreturn;
		}

		public VATreturnResponse SendReturn(VATreturn vatReturn)
		{
			RefreshAccessToken();
			string url = urlSite + _urlSubmitVATreturn.Replace("{vrn}", vrn);
			HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, url);
			httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            string json = JsonConvert.SerializeObject(vatReturn, new DecimalJsonConverter());
            httpRequest.Content = new StringContent(json, Encoding.UTF8, _contentTypeHeader);
            VATreturnResponse ret;

			using (var httpClient = new HttpClient())
			{
				HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
                checkApiResponse(response, VATApiType.SubmitVATreturnForPeriod);

				ret = JsonConvert.DeserializeObject<VATreturnResponse>(response.Content.ReadAsStringAsync().Result);
			}
			return ret;
		}
	}
}
