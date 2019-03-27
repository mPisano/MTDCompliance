namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class liabilityRequest : requestAuthorisation
	{
		/// <summary>
		/// Liabilities to return from date
		/// For example: 2017-01-25
		/// </summary>
		public string from { get; set; }

		/// <summary>
		/// Liabilities to return up to date
		/// For example: 2017-01-25
		/// </summary>
		public string to { get; set; }
	}
}
