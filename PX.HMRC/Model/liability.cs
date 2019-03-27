namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class liability
	{
		/// <summary>
		/// The tax period
		/// </summary>
		public period taxPeriod { get; set; }

		/// <summary>
		/// The charge type of this liability. Max length, 30 characters.
		/// For example: VAT...
		/// </summary>
		public string type { get; set; }

		/// <summary>
		/// The original liability value. 
		/// Defines a monetary value (to 2 decimal places), 
		/// between -9,999,999,999,999.99 and 9,999,999,999,999.99
		/// </summary>
		public decimal originalAmount { get; set; }

		/// <summary>
		/// The outstanding liability value. 
		/// Defines a monetary value (to 2 decimal places), 
		/// between -9,999,999,999,999.99 and 9,999,999,999,999.99
		/// </summary>
		public decimal outstandingAmount { get; set; }

		/// <summary>
		/// Liability due date
		/// Date in the format YYYY-MM-DD
		/// For example: 2017-01-25
		/// </summary>
		public string due { get; set; }
	}
}
