namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class response
	{
		/// <summary>
		/// Unique id for operation tracking 
		/// String, 36 characters.
		/// For example: c75f40a6-a3df-4429-a697-471eeec46435
		/// </summary>
		public string XCorrelationId { get; set; }

		/// <summary>
		/// Unique reference number returned for a submission 
		/// String, 36 characters.
		/// For example: 2dd537bc-4244-4ebf-bac9-96321be13cdc
		/// </summary>
		public string ReceiptID { get; set; }

		/// <summary>
		/// The timestamp from the signature, in ISO8601 format
		/// For example: 2018-02-14T09:32:15Z
		/// </summary>
		public string ReceiptTimestamp { get; set; }

		/// <summary>
		/// This header is not currently used
		/// For example: DO NOT USE
		/// </summary>
		public string ReceiptSignature { get; set; }
	}
}
