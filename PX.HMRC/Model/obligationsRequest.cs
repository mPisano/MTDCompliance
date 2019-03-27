using System;

/// <summary>
/// VAT (MTD) API
/// https://developer.service.hmrc.gov.uk/api-documentation/docs/api/service/vat-api/1.0
/// </summary>
namespace PX.HMRC.Model
{

	[System.SerializableAttribute()]
	public class obligationsRequest : requestAuthorisation
	{
		/// <summary>
		/// Date from which to return obligations. Mandatory unless the status is O.
		/// For example: 2017-01-25
		/// </summary>
		public DateTime? from { get; set; }

		/// <summary>
		/// Date to which to return obligations. Mandatory unless the status is O.
		/// For example: 2017-01-25
		/// </summary>
		public DateTime? to { get; set; }

		/// <summary>
		/// Which obligation statuses to return (O=Open, F=Fulfilled)
		/// For example: F
		/// </summary>
		public string status { get; set; }
	}
}
