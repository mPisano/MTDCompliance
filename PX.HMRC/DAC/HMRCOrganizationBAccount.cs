using System;
using PX.Data;
using PX.Objects.CR;
using PX.Objects.GL.DAC;

namespace PX.HMRC.DAC
{
	[PXProjection(typeof(Select2<Organization,
		InnerJoin<BAccountR, On<BAccountR.bAccountID, Equal<Organization.bAccountID>>>, Where<True, Equal<True>>>))]
	[PXCacheName("HMRCOrganizationBAccount")]
	[Serializable]
	public partial class HMRCOrganizationBAccount : PX.Data.IBqlTable
	{
		#region BAccountID
		public abstract class bAccountID : IBqlField { }
		[PXDBInt(BqlField = typeof(Organization.bAccountID))]
		public virtual Int32? BAccountID { get; set; }
		#endregion
		#region OrganizationID
		public abstract class organizationID : IBqlField { }
		[PXDBInt(BqlField = typeof(Organization.organizationID))]
		public virtual Int32? OrganizationID { get; set; }
		#endregion
		#region FileTaxesByBranches
		public abstract class fileTaxesByBranches : IBqlField { }
		[PXDBBool(BqlField = typeof(Organization.fileTaxesByBranches))]
		public virtual bool? FileTaxesByBranches { get; set; }
		#endregion
		#region TaxRegistrationID
		public abstract class taxRegistrationID : PX.Data.IBqlField { }
		[PXDBString(50, IsUnicode = true, BqlField = typeof(BAccountR.taxRegistrationID))]
		public virtual String TaxRegistrationID { get; set; }
		#endregion
	}
}
