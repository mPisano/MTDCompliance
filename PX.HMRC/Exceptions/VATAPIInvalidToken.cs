using System;

namespace PX.HMRC.Exceptions
{
	//  https://developer.service.hmrc.gov.uk/api-documentation/docs/reference-guide#http-status-codes
	//  https://developer.service.hmrc.gov.uk/api-documentation/docs/authorisation/user-restricted-endpoints
	public class VATAPIInvalidToken : Exception
	{
		#region constants
		public const string IMPOSSIBLE_TO_REFRESH_TOKEN = "IMPOSSIBLE_TO_REFRESH_TOKEN";
		public const string REFRESH_TOKEN_IS_INVALID = "REFRESH_TOKEN_IS_INVALID";
		public const string REFRESH_TOKEN_IS_MISSING = "REFRESH_TOKEN_IS_MISSING";
		public const string SERVER_ERROR = "SERVER_ERROR";		
		#endregion

		public string Code { get; set; }

		public VATAPIInvalidToken() { }

		public VATAPIInvalidToken(string code, string message=null): base(getMessageByCode(code, message))
		{
			Code = code;
		}

		private static string getMessageByCode(string code, string message = null)
		{
			if (String.IsNullOrEmpty(message))
			{
				switch (code)
				{
					case IMPOSSIBLE_TO_REFRESH_TOKEN: message = "Impossible to refresh a token"; break;
					case REFRESH_TOKEN_IS_INVALID: message = "Refresh Token is invalid"; break;
					case REFRESH_TOKEN_IS_MISSING: message = "Refresh Token is missing"; break;
				}
			}
			return message;
		}


	}
}
