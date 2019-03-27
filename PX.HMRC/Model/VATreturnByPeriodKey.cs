namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class VATreturnByPeriodKey : requestAuthorisation
	{
		/// <summary>
		/// The ID code for the period that this obligation belongs to. 
		/// The format is a string of four alphanumeric characters. 
		/// Occasionally the format includes the “#” symbol, which must be URL-encoded.
		/// For example: 18AD, 18A1, #001
		/// </summary>
		public string periodKey { get; set; }
	}
}
