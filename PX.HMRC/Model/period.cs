namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class period
	{
		/// <summary>
		/// The from date of this tax period
		/// Date in the format YYYY-MM-DD
		/// For example: 2017-01-25
		/// </summary>
		public string from { get; set; }

		/// <summary>
		/// The to date of this tax period
		/// Date in the format YYYY-MM-DD
		/// For example: 2017-01-25
		/// </summary>
		public string to { get; set; }
	}
}
