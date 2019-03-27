using PX.Common;

namespace PX.HMRC
{
    [PXLocalizable(Messages.Prefix)]
    public static class Messages
    {
        public const string Prefix = "HMRC Error";

        public const string Fulfilled = "Fulfilled";
        public const string Open = "Open";

        public const string VatReturnWillBeSentToHMRC = "The VAT return will be sent to HMRC. Is the VAT return finalized?";
        public const string PleaseAuthorize = "Please authorize.";

        public const string FromDate = "From Date";
        public const string ToDate = "To Date";
        public const string DueDate = "Due Date";
        public const string SubmitVATReturn = "Submit VAT Return";
        public const string RetrieveVATreturn = "Retrieve VAT return";
        public const string SubmittedVATReturn = "Submitted VAT Return";
        public const string SignInHMRC = "Sign In HMRC";
        public const string PeriodCode = "Period code";
        public const string ReceivedDate = "Received Date";
        public const string Status = "Status";
        public const string Description = "Description";
        public const string Amount = "Amount";
        public const string TaxBoxCode = "Tax Box Code";
        public const string TaxBoxNumber = "Tax Box Number";
        public const string VATreturnIsAccepted = "The VAT return is accepted.";
    }
}
