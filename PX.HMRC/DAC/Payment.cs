using System;

namespace PX.HMRC.DAC
{
    [System.SerializableAttribute()]
    public class Payment  
    {
        public virtual Decimal? amount { get; set; }
        public virtual DateTime? received { get; set; }
    }
}
