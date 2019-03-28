using System;
using PX.Data;

namespace PX.HMRC.DAC
{
	[System.SerializableAttribute()]
    [PXHidden]
    public class VATRow : IBqlTable
	{
		#region taxBoxNbr
		public abstract class taxBoxNbr : IBqlField { }

		[PXString(12, IsKey = true, IsUnicode = true)]
		[PXUIField(DisplayName = Messages.TaxBoxNumber, Visibility = PXUIVisibility.Visible)]
		public virtual String TaxBoxNbr { get; set; }
		#endregion
		#region taxBoxCode
		public abstract class taxBoxCode : IBqlField { }

		[PXString(15, IsUnicode = true)]
		[PXUIField(DisplayName =Messages.TaxBoxCode, Visibility = PXUIVisibility.Visible)]
		public virtual String TaxBoxCode { get; set; }
		#endregion
		#region Descr
		public abstract class descr : IBqlField { }

		[PXDBString(128, IsUnicode = true)]
		[PXUIField(DisplayName = Messages.Description, Visibility = PXUIVisibility.SelectorVisible)]
		public virtual String Descr { get; set; }
		#endregion
		#region Amt
		public abstract class amt : PX.Data.IBqlField { }

		[PXDecimal(2)]
		[PXDefault(TypeCode.Decimal, "0.0", PersistingCheck = PXPersistingCheck.Nothing)]
		[PXUIField(DisplayName = Messages.Amount, Visibility = PXUIVisibility.Visible, Enabled = false)]
		public virtual Decimal? Amt { get; set; }
		#endregion
	}
}
