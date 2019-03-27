using System;

/// <summary>
/// VAT (MTD) API
/// https://developer.service.hmrc.gov.uk/api-documentation/docs/api/service/vat-api/1.0
/// </summary>
namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class obligation
	{
		/// <summary>
		/// The start date of this obligation period
		/// Date in the format YYYY-MM-DD
		/// For example: 2017-01-25
		/// </summary>
		public DateTime? start { get; set; }

		/// <summary>
		/// The end date of this obligation period
		/// Date in the format YYYY-MM-DD
		/// For example: 2017-01-25
		/// </summary>
		public DateTime? end { get; set; }

		/// <summary>
		/// The due date for this obligation period, in the format YYYY-MM-DD. 
		/// For example: 2017-01-25. The due date for monthly/quarterly obligations is one month and seven days from the end date. 
		/// The due date for Payment On Account customers is the last working day of the month after the end date. 
		/// For example if the end date is 2018-02-28, the due date is 2018-03-29 
		/// (because the 31 March is a Saturday and the 30 March is Good Friday).
		/// </summary>
		public DateTime? due { get; set; }

		/// <summary>
		/// Which obligation statuses to return (O = Open, F = Fulfilled)
		/// For example: F
		/// </summary>
		public string status { get; set; }

		/// <summary>
		/// The ID code for the period that this obligation belongs to. The format is a string of four alphanumeric characters. Occasionally the format includes the # symbol.
		/// For example: 18AD, 18A1, #001
		/// </summary>
		public string periodKey { get; set; }

		/// <summary>
		/// The obligation received date, is returned when status is (F = Fulfilled)
		/// Date in the format YYYY-MM-DD
		/// For example: 2017-01-25
		/// </summary>/// 
		public DateTime? received { get; set; }
	}
}
