using System;

namespace PX.HMRC.DAC
{
    [System.SerializableAttribute()]
    public class Obligation
    {
        #region PeriodKey
        /// <summary>
        /// The ID code for the period that this obligation belongs to. The format is a string of four alphanumeric characters. Occasionally the format includes the # symbol.
        /// For example: 18AD, 18A1, #001
        /// </summary>

        public string PeriodKey;//{ get; set; } Remove getters so they dont bind to grids
        #endregion
        #region Start
        /// <summary>
        /// The start date of this obligation period
        /// Date in the format YYYY-MM-DD
        /// For example: 2017-01-25
        /// </summary>
        public virtual DateTime? Start { get; set; }
        #endregion
        #region End
        /// <summary>
        /// The end date of this obligation period
        /// Date in the format YYYY-MM-DD
        /// For example: 2017-01-25
        /// </summary>
        public virtual DateTime? End { get; set; }
        #endregion
        #region Due
        /// <summary>
        /// The due date for this obligation period, in the format YYYY-MM-DD. 
        /// For example: 2017-01-25. The due date for monthly/quarterly obligations is one month and seven days from the end date. 
        /// The due date for Payment On Account customers is the last working day of the month after the end date. 
        /// For example if the end date is 2018-02-28, the due date is 2018-03-29 
        /// (because the 31 March is a Saturday and the 30 March is Good Friday).
        /// </summary>
        public virtual DateTime? Due { get; set; }
        #endregion
        #region Status
        /// <summary>
        /// Which obligation statuses to return (O = Open, F = Fulfilled)
        /// For example: F
        /// </summary>
        public virtual string Status { get; set; }
        #endregion
        #region Received
        /// <summary>
        /// The obligation received date, is returned when status is (F = Fulfilled)
        /// Date in the format YYYY-MM-DD
        /// For example: 2017-01-25
        /// </summary>
        public virtual DateTime? Received { get; set; }
        #endregion
    }
}
