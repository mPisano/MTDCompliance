using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using PX.HMRC.DAC;
using PX.HMRC.Model;
using System.Security.Cryptography;

namespace PX.HMRC
{
    public class VATMaint
    {
        #region result containers
        public VATreturn VATreturn = new VATreturn();
        public List<Payment> Payments = new List<Payment>();
        public List<Liability> Liabilities = new List<Liability>();
        public List<Obligation> Obligations = new List<Obligation>();
        #endregion


        #region Private variables
        private OAuthSettings _oAuthApplication;
        private string TokenKey;
        private string vrn = "";
        public VATApi VATProvider = null;
        string TokenFile;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public VATMaint(OAuthSettings hMRCOAuthApplication, Dictionary<string, string> FraudHeaders = null)
        {
            if (FraudHeaders == null)
                FraudHeaders = new Dictionary<string, string>();

            OAuthToken _oAuthToken;
            TokenKey = System.Security.Principal.WindowsIdentity.GetCurrent().User.Value.Replace("-", "");
            TokenKey = TokenKey.Substring(TokenKey.Length - 24);

            if (System.IO.File.Exists(@"Token.dat"))
            {
                TokenFile = @"Token.dat";
            }
            else
            {
                string fldr = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "HMRC", "VAT");
                System.IO.Directory.CreateDirectory(fldr);
#if DEBUG
                TokenFile = System.IO.Path.Combine(fldr, @"Token_Debug.dat");
#else
                TokenFile = System.IO.Path.Combine(fldr, @"Token.dat");
#endif 
            }


            vrn = hMRCOAuthApplication.VRN;
            _oAuthApplication = hMRCOAuthApplication;

            if (System.IO.File.Exists(TokenFile))
            {
                try
                {
                    System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(OAuthToken));
                    using (System.IO.StreamReader file = new System.IO.StreamReader(TokenFile))
                    {
                        _oAuthToken = (OAuthToken)reader.Deserialize(file);
                        file.Close();
                        _oAuthToken.RefreshToken = Decrypt(_oAuthToken.RefreshToken, TokenKey);
                        _oAuthToken.AccessToken = Decrypt(_oAuthToken.AccessToken, TokenKey);
                        _oAuthToken.Bearer = Decrypt(_oAuthToken.Bearer, TokenKey);
                    }
                }
                catch (Exception)
                {
                    _oAuthToken = new OAuthToken();
                }
            }
            else
                _oAuthToken = new OAuthToken();

            VATProvider = new VATApi(_oAuthApplication, _oAuthToken, vrn, FraudHeaders, SaveOAuthToken);
        }

        public static void SignInHMRCProc(VATMaint vatMaint)
        {
            vatMaint.VATProvider.SignIn();
            throw new Exception("Please Refresh");
        }

        public static void GetVATObligationsForYearProc(VATMaint vatMaint, DateTime to)
        {
            GetVATObligationsProc(vatMaint, to.AddYears(-1), to);
        }

        public static void GetVATObligationsProc(VATMaint vatMaint, DateTime from, DateTime to, string status = null, String testScenario = null)
        {
            obligationsRequest req = new obligationsRequest() { from = from, to = to, status = status };
            obligationResponse obligationResponse = null;
            try
            {
                obligationResponse = vatMaint.VATProvider.Obligations(req, testScenario);
            }
            catch (Exceptions.VATAPIInvalidToken eToken)
            {
                Trace.WriteError(eToken);
                //         vatMaint.signInHMRC.SetEnabled(true);
                throw new Exception(Messages.PleaseAuthorize);
            }
            catch (Exceptions.VATAPIException eApi)
            {
                Trace.WriteError(eApi);
                if (eApi.Data.Contains("json"))
                {
                    Trace.WriteError(eApi.Data["json"].ToString());
                }
                if (eApi.Code != error.MATCHING_RESOURCE_NOT_FOUND)
                {
                    throw eApi;
                }
            }
            catch (Exception e)
            {
                Trace.WriteError(e);
                throw e;
            }
            vatMaint.Obligations.Clear();
            if (obligationResponse != null)
                foreach (var o in obligationResponse.obligations)
                {
                    vatMaint.Obligations.Add(new Obligation()
                    {
                        Start = o.start,
                        End = o.end,
                        Due = o.due,
                        Status = o.status,
                        PeriodKey = o.periodKey,
                        Received = o.received
                    });
                }
            return;
        }

        public static void GetVATPaymentsProc(VATMaint vatMaint, DateTime from, DateTime to, string testScenario = null)
        {
            paymentRequest req = new paymentRequest() { from = from, to = to };
            paymentResponse paymentResponse = null;
            try
            {
                paymentResponse = vatMaint.VATProvider.Payments(req, testScenario);
            }
            catch (Exceptions.VATAPIInvalidToken eToken)
            {
                Trace.WriteError(eToken);
                //         vatMaint.signInHMRC.SetEnabled(true);
                throw new Exception(Messages.PleaseAuthorize);
            }
            catch (Exceptions.VATAPIException eApi)
            {
                Trace.WriteError(eApi);
                if (eApi.Data.Contains("json"))
                {
                    Trace.WriteError(eApi.Data["json"].ToString());
                }
                if (eApi.Code != error.MATCHING_RESOURCE_NOT_FOUND)
                {
                    throw eApi;
                }
            }
            catch (Exception e)
            {
                Trace.WriteError(e);
                throw e;
            }
            vatMaint.Payments.Clear();
            if (paymentResponse != null)
                foreach (var o in paymentResponse.payments)
                {
                    vatMaint.Payments.Add(new Payment()
                    {
                        amount = o.amount,
                        received = (o.received == null) ? (DateTime?)null : DateTime.ParseExact(o.received, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                    });
                }
            return;
        }

        public static void GetVATLiabilitiesProc(VATMaint vatMaint, DateTime from, DateTime to, string testScenario = null)
        {
            liabilityRequest req = new liabilityRequest() { from = from, to = to };
            liabilityResponse liabilityResponse = null;
            try
            {
                liabilityResponse = vatMaint.VATProvider.Liabilities(req, testScenario);
            }
            catch (Exceptions.VATAPIInvalidToken eToken)
            {
                Trace.WriteError(eToken);
                //         vatMaint.signInHMRC.SetEnabled(true);
                throw new Exception(Messages.PleaseAuthorize);
            }
            catch (Exceptions.VATAPIException eApi)
            {
                Trace.WriteError(eApi);
                if (eApi.Data.Contains("json"))
                {
                    Trace.WriteError(eApi.Data["json"].ToString());
                }
                if (eApi.Code != error.MATCHING_RESOURCE_NOT_FOUND)
                {
                    throw eApi;
                }
            }
            catch (Exception e)
            {
                Trace.WriteError(e);
                throw e;
            }
            vatMaint.Liabilities.Clear();
            if (liabilityResponse != null)
                foreach (var o in liabilityResponse.liabilities)
                {
                    vatMaint.Liabilities.Add(new Liability()
                    {
                        due = DateTime.ParseExact(o.due, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                        originalAmount = o.originalAmount,
                        outstandingAmount = o.outstandingAmount,
                        type = o.type,
                        from = DateTime.ParseExact(o.taxPeriod.from, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture),
                        to = DateTime.ParseExact(o.taxPeriod.to, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                    });
                }
            return;
        }

        public static void CheckVATReturnProc(VATMaint vatMaint, string periodKey)
        {
            if (String.IsNullOrWhiteSpace(periodKey))
                return;

            try
            {
                vatMaint.VATreturn = vatMaint.VATProvider.Returns(periodKey);
            }
            catch (Exceptions.VATAPIInvalidToken eToken)
            {
                Trace.WriteError(eToken);
                //  vatMaint.signInHMRC.SetEnabled(true);
                throw new Exception(Messages.PleaseAuthorize);
            }
            catch (Exceptions.VATAPIException eApi)
            {
                if (eApi.Code == error.NOT_FOUND)
                {
                    vatMaint.VATreturn = new VATreturn();
                }
                Trace.WriteError(eApi);
                if (eApi.Data.Contains("json"))
                {
                    Trace.WriteError(eApi.Data["json"].ToString());
                }
                throw eApi;
            }
            catch (Exception e)
            {
                Trace.WriteError(e);
                throw e;
            }
        }

        public VATreturnResponse VATReturnResponse;
        //public static void SendVATReturnProc(VATMaint vatMaint, VATPeriodFilter p, bool finalised = false)
        public static void SendVATReturnProc(VATMaint vatMaint, bool finalised = false)
        {
            vatMaint.VATreturn.finalised = finalised;
            try
            {
                vatMaint.VATReturnResponse = vatMaint.VATProvider.SendReturn(vatMaint.VATreturn);
                Trace.WriteInformation(JsonConvert.SerializeObject(vatMaint.VATReturnResponse));
            }
            catch (Exceptions.VATAPIInvalidToken eToken)
            {
                Trace.WriteError(eToken);
                throw new Exception(Messages.PleaseAuthorize);
            }
            catch (Exceptions.VATAPIException eApi)
            {
                Trace.WriteError(eApi);
                if (eApi.Data.Contains("errorJson"))
                {
                    Trace.WriteError(eApi.Data["errorJson"].ToString());
                }
                throw eApi;
            }
            catch (Exception e)
            {
                Trace.WriteError(e);
                throw e;
            }
            return;
            // throw new Exception(Messages.VATreturnIsAccepted);
        }

        public void SaveOAuthToken(OAuthToken o)
        {
            OAuthToken SaveToken = new OAuthToken();
            SaveToken.Bearer = o.Bearer;
            SaveToken.UtcExpiredOn = o.UtcExpiredOn;
            SaveToken.RefreshToken = Encrypt(o.RefreshToken, TokenKey);
            SaveToken.Bearer = Encrypt(o.Bearer, TokenKey);
            SaveToken.AccessToken = Encrypt(o.AccessToken, TokenKey);

            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(OAuthToken));
            System.IO.FileStream file = System.IO.File.Create(TokenFile);
            writer.Serialize(file, SaveToken);
            file.Close();
        }

        private static string Encrypt(string input, string key)
        {
            try
            {
                byte[] inputArray = System.Text.UTF8Encoding.UTF8.GetBytes(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = System.Text.UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return input; //Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return input;
            }
        }

        private static string Decrypt(string input, string key)
        {
            try
            {
                byte[] inputArray = Convert.FromBase64String(input);
                TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();
                tripleDES.Key = System.Text.UTF8Encoding.UTF8.GetBytes(key);
                tripleDES.Mode = CipherMode.ECB;
                tripleDES.Padding = PaddingMode.PKCS7;
                ICryptoTransform cTransform = tripleDES.CreateDecryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
                tripleDES.Clear();
                return System.Text.UTF8Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return input;
            }

        }
    }
}
