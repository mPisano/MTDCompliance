using System;

namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class paymentRequest : requestAuthorisation
	{
		/// <summary>
		/// Payments to return from date
		/// For example: 2017-01-25
		/// </summary>
		public DateTime? from { get; set; }

		/// <summary>
		/// Payments to return up to date
		/// For example: 2017-01-25
		/// </summary>
		public DateTime? to { get; set; }
	}
}
