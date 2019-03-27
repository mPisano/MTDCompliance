using System.Collections;
using PX.Data;
using PX.HMRC;
using PX.HMRC.DAC;


namespace PX.HMRC.Attributes
{
    public class ObligationSelectorAttribute : PXCustomSelectorAttribute
    {
        public ObligationSelectorAttribute()
        : base(typeof(Obligation.periodKey),
            typeof(Obligation.periodKey),
            typeof(Obligation.start),
            typeof(Obligation.end),
            typeof(Obligation.due),
            typeof(Obligation.status),
            typeof(Obligation.received))
        {
        }

        public virtual IEnumerable GetRecords()
        {
            return ((VATMaint)_Graph).obligations();
        }
    }
}
