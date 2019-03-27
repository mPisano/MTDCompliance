using System;
using PX.Data;
using PX.Objects.CM;
using PX.Objects.TX;

namespace PX.HMRC.DAC
{
    [System.SerializableAttribute()]
    [PXProjection(typeof(Select2<TaxHistory, 
        InnerJoin<TaxTran, 
            On <TaxHistory.branchID, Equal<TaxTran.branchID>, 
                And<TaxHistory.vendorID, Equal<TaxTran.vendorID>, 
                And<TaxHistory.accountID, Equal<TaxTran.accountID>, 
                And<TaxHistory.subID, Equal<TaxTran.subID>,
                And<TaxHistory.taxPeriodID, Equal<TaxTran.taxPeriodID>,
                And<TaxHistory.taxID, Equal<TaxTran.taxID>,
                And<TaxHistory.revisionID, Equal<TaxTran.revisionID>,
                And<TaxTran.released, Equal<PX.Objects.CS.boolTrue>,
                And<TaxTran.voided, Equal<PX.Objects.CS.boolFalse>
                >>>>>>>>>>>))]
    public partial class TaxHistoryReleased : PX.Data.IBqlTable
    {
        #region BranchID
        public abstract class branchID : PX.Data.IBqlField { }

        [PXDBInt(IsKey = true, BqlTable = typeof(TaxHistory))]
        [PXDefault()]
        public virtual Int32? BranchID { get; set; }
        #endregion
        #region VendorID
        public abstract class vendorID : PX.Data.IBqlField { }

        [PXDBInt(IsKey = true, BqlTable = typeof(TaxHistory))]
        [PXDefault()]
        public virtual Int32? VendorID { get; set; }
        #endregion
        #region TaxPeriodID
        public abstract class taxPeriodID : PX.Data.IBqlField { }

        [PX.Objects.GL.FinPeriodID(IsKey = true, BqlTable = typeof(TaxHistory))]
        [PXDefault()]
        public virtual String TaxPeriodID { get; set; }
        #endregion
        #region RevisionID
        public abstract class revisionID : PX.Data.IBqlField { }

        [PXDBInt(IsKey = true, BqlTable = typeof(TaxHistory))]
        [PXDefault()]
        [PXUIField(DisplayName = "Revision ID", Visibility = PXUIVisibility.SelectorVisible, Enabled = false)]
        public virtual Int32? RevisionID { get; set; }
        #endregion
        #region LineNbr
        public abstract class lineNbr : PX.Data.IBqlField {  }

        [PXDBInt(IsKey = true, BqlTable = typeof(TaxHistory))]
        [PXDefault()]
        public virtual Int32? LineNbr { get; set; }
        #endregion
        #region FiledAmt
        public abstract class filedAmt : PX.Data.IBqlField { }
        [PXDBBaseCury(typeof(TaxHistory.vendorID), BqlTable = typeof(TaxHistory))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public virtual Decimal? FiledAmt { get; set; }
        #endregion
        #region UnfiledAmt
        public abstract class unfiledAmt : PX.Data.IBqlField { }
        [PXDBBaseCury(typeof(TaxHistory.vendorID), BqlTable = typeof(TaxHistory))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public virtual Decimal? UnfiledAmt { get; set; }
        #endregion
        #region ReportFiledAmt
        public abstract class reportFiledAmt : PX.Data.IBqlField { }

        [PXDBBaseCury(typeof(TaxHistory.vendorID), BqlTable = typeof(TaxHistory))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public virtual Decimal? ReportFiledAmt { get; set; }
        #endregion
        #region ReportUnfiledAmt
        public abstract class reportUnfiledAmt : PX.Data.IBqlField { }

        [PXDBBaseCury(typeof(TaxHistory.vendorID), BqlTable = typeof(TaxHistory))]
        [PXDefault(TypeCode.Decimal, "0.0")]
        [PXUIField(DisplayName = "Amount", Visibility = PXUIVisibility.Visible, Enabled = false)]
        public virtual Decimal? ReportUnfiledAmt { get; set; }
        #endregion
    }
}
