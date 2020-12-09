using System;

namespace PX.HMRC.Exceptions
{
	[Serializable]
	public class VATAPIException : Exception
	{
		public string Code { get; set; }

		public VATAPIException() { }

		public VATAPIException(string code, string message = null):base(message)
		{
			Code = code;
		}

		protected VATAPIException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}
	}
}