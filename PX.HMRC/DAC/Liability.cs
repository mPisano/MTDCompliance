using System;

namespace PX.HMRC.DAC
{
    [System.SerializableAttribute()]
    public class Liability
    {
        //  public virtual TaxPeriod taxPeriod { get; set; }
        public DateTime? from { get; set; }
        public DateTime? to { get; set; }
        public virtual string type { get; set; }
        public virtual Decimal? originalAmount { get; set; }
        public virtual Decimal? outstandingAmount { get; set; }
        public virtual DateTime? due { get; set; }
    }
}
