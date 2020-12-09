using System;

namespace PX.HMRC.Model
{
	[System.SerializableAttribute()]
	public class payment
	{
		/// <summary>
		/// The payment value. Defines a monetary value (to 2 decimal places), 
		/// between -9,999,999,999,999.99 and 9,999,999,999,999.99
		/// </summary>
		public decimal amount { get; set; }

		/// <summary>
		/// Payment received date
		/// Date in the format YYYY-MM-DD
		/// For example: 2017-01-25
		/// </summary>
		public string received { get; set; }
	}
}
