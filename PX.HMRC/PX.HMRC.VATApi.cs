using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using PX.HMRC.Model;
using PX.HMRC.Exceptions;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Win32;

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
        private OAuthSettings application;
        private OAuthToken token;
        private string vrn;
        private string urlSite;
        private Dictionary<string, string> fraudHeaders;

        #region Endpoitnts 
        private const string _isSignedUrl = "/hello/application";
        private const string _urlObligations = "/organisations/vat/{vrn}/obligations";
        private const string _urlReturns = "/organisations/vat/{vrn}/returns/{periodKey}";
        private const string _urlSubmitVATreturn = "/organisations/vat/{vrn}/returns";
        private const string _urlLiabilities = "/organisations/vat/{vrn}/liabilities";
        private const string _urlPayments = "/organisations/vat/{vrn}/payments";
        private const string _testFraudUrl = "/test/fraud-prevention-headers/validate";

        //private string urlSite = "https://test-api.service.hmrc.gov.uk";
        #endregion

        public Dictionary<string, string> FraudHeaders
        {
            get { return fraudHeaders; }
        }

        public delegate void SaveOAuthTokenDelegate(OAuthToken o);

        private SaveOAuthTokenDelegate saveOAuthToken;

        public VATApi(OAuthSettings Application, OAuthToken Token, string VRN, Dictionary<string, string> FraudHeaders, SaveOAuthTokenDelegate SaveOAuthToken = null)
        {
            application = Application;
            token = Token;
            urlSite = Application.ServerUrl;
            saveOAuthToken = SaveOAuthToken;
            setVRN(VRN);
            fraudHeaders = FraudHeaders;

            ApplicationProcessor = new MTDApplicationProcessor(Application.ServerUrl);
            try
            {
                if (token != null && token.RefreshToken != null)
                    RefreshAccessToken();
            }
            catch (VATAPIInvalidToken)
            {
                //     if (ex.Code == "asd")
                //         token = new OAuthToken();
            }

        }

        public void setVRN(string VRN)
        {
            vrn = VRN;
        }

        public async void SignIn(bool Manual = false)
        {
            if (Manual)
                ApplicationProcessor.SignIn(application);
            else
            {
                //OAuthToken  await ApplicationProcessor.SignIn(application, token); _token = null;
                var x = await ApplicationProcessor.SignIn(application, token);
                if (x == Browser.BrowserResultType.Success)
                    saveOAuthToken(token);
            }
        }

        /// <summary>
        /// Sign Out by Clearing Access and Refresh Token
        /// </summary>
        public void SignOut()
        {
            ApplicationProcessor.SignOut(application, token);
            token.AccessToken = "";
            token.RefreshToken = "";
            saveOAuthToken(token);
        }

        public void ProcessAuthorizationCode(string code)
        {
            if (ApplicationProcessor.ProcessAuthorizationCode(code, application, token))
                saveOAuthToken(token);
        }

        public string GetMachineGuid()
        {
            string x64Result = string.Empty;
            string x86Result = string.Empty;
            RegistryKey keyBaseX64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey keyBaseX86 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
            RegistryKey keyX64 = keyBaseX64.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadSubTree);
            RegistryKey keyX86 = keyBaseX86.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadSubTree);
            object resultObjX64 = keyX64.GetValue("MachineGuid", (object)"default");
            object resultObjX86 = keyX86.GetValue("MachineGuid", (object)"default");
            keyX64.Close();
            keyX86.Close();
            keyBaseX64.Close();
            keyBaseX86.Close();
            keyX64.Dispose();
            keyX86.Dispose();
            keyBaseX64.Dispose();
            keyBaseX86.Dispose();
            keyX64 = null;
            keyX86 = null;
            keyBaseX64 = null;
            keyBaseX86 = null;
            if (resultObjX64 != null && resultObjX64.ToString() != "default")
            {
                return resultObjX64.ToString();
            }
            if (resultObjX86 != null && resultObjX86.ToString() != "default")
            {
                return resultObjX86.ToString();
            }
            return "";
        }


        /// <summary>
        /// Refresh Access Token if it is need
        /// </summary>
        private bool RefreshAccessToken()
        {
            if (token == null || token.RefreshToken == null)
            {
                throw new Exceptions.VATAPIInvalidToken(Model.error.IMPOSSIBLE_TO_REFRESH_TOKEN);
            }
            if (token.UtcExpiredOn == null || token.UtcExpiredOn?.AddMinutes(-15) < System.DateTime.UtcNow)
            {
                try
                {
                    ApplicationProcessor.RefreshAccessToken(application, token);
                    saveOAuthToken(token);
                }
                catch (Exception ex)
                {
                    Trace.WriteError(ex);
                    //         throw;
                }
            }
            return false;
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
                message = String.Join("; ", err.errors.Select(o => o.message));

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
        /// Retrieve VAT Payments
        /// </summary>
        /// <returns></returns>
        public paymentResponse Payments(paymentRequest request, string testScenario = null)
        {
            RefreshAccessToken();
            string url = urlSite + _urlPayments.Replace("{vrn}", vrn) + "?from=" + request.from?.ToString("yyyy-MM-dd") + "&to=" + request.to?.ToString("yyyy-MM-dd");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            foreach (var item in fraudHeaders)
            {
                httpRequest.Headers.Add(item.Key, item.Value);
            }

            if (!String.IsNullOrEmpty(testScenario))
            {
                httpRequest.Headers.Add("Gov-Test-Scenario", testScenario);
            }

            paymentResponse paymentResponse = null;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
                checkApiResponse(response, VATApiType.RetrieveVATliabilities);

                var content = response.Content.ReadAsStringAsync();
                paymentResponse = JsonConvert.DeserializeObject<paymentResponse>(content.Result);
            }

            return paymentResponse;
        }

        /// <summary>
        /// Retrieve VAT Liabilities
        /// </summary>
        /// <returns></returns>
        public liabilityResponse Liabilities(liabilityRequest request, string testScenario = null)
        {
            RefreshAccessToken();
            string url = urlSite + _urlLiabilities.Replace("{vrn}", vrn) + "?from=" + request.from?.ToString("yyyy-MM-dd") + "&to=" + request.to?.ToString("yyyy-MM-dd");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            foreach (var item in fraudHeaders)
            {
                httpRequest.Headers.Add(item.Key, item.Value);
            }

            if (!String.IsNullOrEmpty(testScenario))
            {
                httpRequest.Headers.Add("Gov-Test-Scenario", testScenario);
            }

            liabilityResponse liabilityResponse = null;
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
                checkApiResponse(response, VATApiType.RetrieveVATliabilities);

                var content = response.Content.ReadAsStringAsync();
                liabilityResponse = JsonConvert.DeserializeObject<liabilityResponse>(content.Result);
            }

            return liabilityResponse;
        }

        /// <summary>
        /// Retrieve VAT obligations
        /// </summary>
        /// <returns></returns>
        public obligationResponse Obligations(obligationsRequest request, string testScenario = null)
        {
            RefreshAccessToken();
            string url = urlSite + _urlObligations.Replace("{vrn}", vrn) + "?from=" + request.from?.ToString("yyyy-MM-dd") + "&to=" + request.to?.ToString("yyyy-MM-dd");
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Get, url);
            httpRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            foreach (var item in fraudHeaders)
            {
                httpRequest.Headers.Add(item.Key, item.Value);
            }

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

            foreach (var item in fraudHeaders)
            {
                httpRequest.Headers.Add(item.Key, item.Value);
            }

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

            foreach (var item in fraudHeaders)
            {
                httpRequest.Headers.Add(item.Key, item.Value);
            }

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

        public bool IsSignedIn()
        {
            if (application == null || token == null)
            {
                return false;
            }

            RefreshAccessToken(); //might will wipe token!
            string url = urlSite + _isSignedUrl;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            foreach (var item in fraudHeaders)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            var httpClient = new HttpClient();
            var sresponse = httpClient.SendAsync(request);
            var response = sresponse.Result;

            return response?.StatusCode == HttpStatusCode.OK;

        }

        public String TestFraud()
        {
            if (application == null || token == null)
            {
                throw new Exception("No Application or Token");
            }
            RefreshAccessToken();
            string url = urlSite + _testFraudUrl;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_acceptHeader));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            foreach (var item in fraudHeaders)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            var httpClient = new HttpClient();
            // var response = httpClient.SendAsync(request).Result;

            //var httpClient = new HttpClient();
            using (HttpResponseMessage response = httpClient.SendAsync(request).Result)
            {
                using (HttpContent content = response.Content)
                {
                    var Result = content.ReadAsStringAsync().Result;

                    return Result;
                }
            }

            //return response;
            // return response?.StatusCode == HttpStatusCode.OK;
        }
    }
}
