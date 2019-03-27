using System;

namespace PX.HMRC.Exceptions
{
	public class VATAPIException : Exception
	{
		public string Code { get; set; }

		public VATAPIException() { }

		public VATAPIException(string code, string message = null):base(message)
		{
			Code = code;
		}
	}
}