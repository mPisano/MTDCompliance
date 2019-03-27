namespace PX.HMRC.Model
{
	/// <summary>
	/// HTTP status: 201 (Created)
	/// </summary>
	[System.SerializableAttribute()]
	public class VATreturnResponse
	{
		/// <summary>
		/// The time that the message was processed in the system.
		/// </summary>		/// 
		public string processingDate { get; set; }

		/// <summary>
		/// Unique number that represents the form bundle. 
		/// The system stores VAT Return data in forms, which are held in a unique form bundle.
		/// Must conform to the regular expression ^[0-9]{12}$
		/// </summary>
		public string paymentIndicator { get; set; }

		/// <summary>
		/// Is DD if the netVatDue value is a debit and HMRC holds a Direct Debit Instruction for the client. 
		/// Is BANK if the netVatDue value is a credit and HMRC holds the client’s bank data. 
		/// Otherwise not present.
		/// Limited to the following possible values:
		/// DD
		/// BANK
		/// </summary>
		public string formBundleNumber { get; set; }

		/// <summary>
		/// The charge reference number is returned, only if the netVatDue value is a debit. 
		/// Between 1 and 16 characters.
		/// </summary>
		public string chargeRefNumber { get; set; }

	}
}
