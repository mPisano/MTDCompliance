using PX.Data;

namespace PX.HMRC.DAC
{
	public class ObligationStatus
	{
		public class ListAttribute : PXStringListAttribute
		{
			public ListAttribute()
				: base(
				new string[] { Fulfilled, Open },
				new string[] { Messages.Fulfilled, Messages.Open })
			{; }
		}

		public const string Fulfilled = "F";
		public const string Open = "O";

		public class fulfilled : Constant<string>
		{
			public fulfilled() : base(Fulfilled) {; }
		}

		public class open : Constant<string>
		{
			public open() : base(Open) {; }
		}
	}
}
