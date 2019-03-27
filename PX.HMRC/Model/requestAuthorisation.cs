namespace PX.HMRC.Model
{
	/// <summary>
	/// Authorisation
	/// </summary>
	[System.SerializableAttribute()]
	public class requestAuthorisation
	{
		/// <summary>
		/// VAT registration number. 
		/// A nine-digit number.
		/// For example: 123456789
		/// </summary>
		public string vrn { get; set; }

		/// <summary>
		/// Server token of Head of HTTP request
		/// Example: Authorization - Bearer 70fc461f8c82d2a322cfe87066ffb284
		/// </summary>
		public string serverToken { get; set; }
	}
}
