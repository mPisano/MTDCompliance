using System;

namespace PX.HMRC.Model
{
	public class error
	{
		#region Codes of response

		#region 400 (Bad Request) (Unauthorized) Refresh Token is invalid

		public const string IMPOSSIBLE_TO_REFRESH_TOKEN = "IMPOSSIBLE_TO_REFRESH_TOKEN";
		/// <summary>
		/// Refresh Token is missing
		/// </summary>
		public const string REFRESH_TOKEN_IS_INVALID = "REFRESH_TOKEN_IS_INVALID";
		/// <summary>
		/// Refresh Token is missing
		/// </summary>
		public const string REFRESH_TOKEN_IS_MISSING = "REFRESH_TOKEN_IS_MISSING";

		/// <summary>
		/// The provided VRN is invalid
		/// </summary>
		public const string VRN_INVALID = "IMPOSSIBLE_TO_REFRESH_TOKEN";

		/// <summary>
		/// Invalid period key
		/// </summary>
		public const string PERIOD_KEY_INVALID = "PERIOD_KEY_INVALID";
        /// <summary>
        /// Invalid request
        /// </summary>
        public const string INVALID_REQUEST = "INVALID_REQUEST";
        /// <summary>
        /// totalVatDue should be equal to the sum of vatDueSales and vatDueAcquisitions
        /// </summary>
        public const string VAT_TOTAL_VALUE = "VAT_TOTAL_VALUE";

        /// <summary>
        /// netVatDue should be the difference between the largest and the smallest values among totalVatDue and vatReclaimedCurrPeriod
        /// </summary>
        public const string VAT_NET_VALUE = "VAT_NET_VALUE";
        /// <summary>
        /// please provide a numeric field
        /// </summary>
        public const string INVALID_NUMERIC_VALUE = "INVALID_NUMERIC_VALUE";
        /// <summary>
        /// 1. amounts should be a non-negative number less than 9999999999999.99 with up to 2 decimal places
        /// 2. The value must be between -9999999999999 and 9999999999999
        /// 3. amount should be a monetary value (to 2 decimal places), between 0 and 99999999999.99
        /// </summary>
        public const string INVALID_MONETARY_AMOUNT = "INVALID_MONETARY_AMOUNT";
        #endregion

        #region 401 (Unauthorized)
        /// <summary>
        /// No OAuth token supplied for user-restricted or application-restricted endpoint
        /// </summary>
        public const string MISSING_CREDENTIALS= "MISSING_CREDENTIALS";
		/// <summary>
		/// Invalid OAuth token supplied for user-restricted or application-restricted endpoint (including expired token)
		/// </summary>
		public const string INVALID_CREDENTIALS = "INVALID_CREDENTIALS";
		/// <summary>
		/// Supplied OAuth token not authorised to access data for given tax identifier(s)
		/// </summary>
		public const string UNAUTHORIZED = "UNAUTHORIZED";
		/// <summary>
		/// User-restricted API is being accessed with a server token
		/// </summary>
		public const string INCORRECT_ACCESS_TOKEN_TYPE = "INCORRECT_ACCESS_TOKEN_TYPE";
		#endregion

		#region 403 (Forbidden)
		/// <summary>
		/// Request done with HTTP
		/// </summary>
		public const string HTTPS_REQUIRED = "HTTPS_REQUIRED";
		/// <summary>
		/// The OAuth token's application is not subscribed to the API
		/// </summary>
		public const string RESOURCE_FORBIDDEN = "RESOURCE_FORBIDDEN";
		/// <summary>
		/// The scope of the OAuth token is not sufficient to access the API
		/// </summary>
		public const string INVALID_SCOPE = "INVALID_SCOPE";
		/// <summary>
		/// The client and/or agent is not authorised.
		/// </summary>
		public const string CLIENT_OR_AGENT_NOT_AUTHORISED = "CLIENT_OR_AGENT_NOT_AUTHORISED";

		/// <summary>
		/// The date of the requested return cannot be more than four years from the current date
		/// </summary>
		public const string DATE_RANGE_TOO_LARGE = "PERIOD_KEY_INVALID";

        /// <summary>
        /// User has not declared VAT return as final
        /// </summary>
        public const string NOT_FINALISED = "NOT_FINALISED";

        /// <summary>
        /// User has already submitted a VAT return for the given period
        /// </summary>
        public const string DUPLICATE_SUBMISSION = "DUPLICATE_SUBMISSION";

        /// <summary>
        /// Return submitted too early
        /// </summary>
        public const string TAX_PERIOD_NOT_ENDED = "TAX_PERIOD_NOT_ENDED";


        #endregion

        #region 404 (Not Found)
        /// <summary>
        /// No endpoint could be found in the API for the request path
        /// </summary>
        public const string MATCHING_RESOURCE_NOT_FOUND = "MATCHING_RESOURCE_NOT_FOUND";
		/// <summary>
		/// The remote endpoint has indicated that no associated data is found
		/// </summary>
		public const string NOT_FOUND = "NOT_FOUND";
        public const string NOT_FOUND_MSG = "The remote endpoint has indicated that no associated data is found";
        /// <summary>
        /// No API could be found for the context
        /// </summary>
        public const string HTTP_NOT_FOUND = "HTTP_NOT_FOUND";
		#endregion

		#region 500 (Internal Server Error)
		/// <summary>
		/// Internal server error
		/// </summary>
		public const string INTERNAL_SERVER_ERROR = "INTERNAL_SERVER_ERROR";
		#endregion

		#region 501 (Not Implemented)
		/// <summary>
		/// API not implemented/deployed
		/// </summary>
		public const string NOT_IMPLEMENTED = "NOT_IMPLEMENTED";
		#endregion

		#region 503 (Service Unavailable)
		/// <summary>
		/// Service unavailable
		/// </summary>
		public const string SERVER_ERROR = "SERVER_ERROR";
		/// <summary>
		/// Scheduled maintenance
		/// </summary>
		public const string SCHEDULED_MAINTENANCE = "SCHEDULED_MAINTENANCE";
		#endregion

		#region 504 (Gateway Timeout)
		/// <summary>
		/// Request timed out
		/// </summary>
		public const string GATEWAY_TIMEOUT = "GATEWAY_TIMEOUT";
		#endregion
		#endregion

		public string code { get; set; }
		public string message { get; set; }
		public string path { get; set; }
		public Int64 reactivationTimestamp { get; set; }

		public error[] errors { get; set; }

        public static string getMessageByCode(string code) {
            string msg = "";
            switch (code) {
                case NOT_FOUND: return NOT_FOUND_MSG;
            }
            return msg;
        }
	}
}
