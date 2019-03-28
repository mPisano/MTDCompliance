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

        public const string vatDueSales = "VAT due on sales and other outputs";
        public const string vatDueAcquisitions = "VAT due on acquisitions from other EC Member States.";
        public const string totalVatDue = "Total VAT due";
        public const string vatReclaimedCurrPeriod = "VAT reclaimed on purchases and other inputs";
        public const string netVatDue = "The difference between Box 3 and Box 4";
        public const string totalValueSalesExVAT = "Total value of sales and all other outputs excluding any VAT";
        public const string totalValuePurchasesExVAT = "Total value of purchases and all other inputs excluding any VAT";
        public const string totalValueGoodsSuppliedExVAT = "Total value of all supplies of goods and related costs, excluding any VAT, to other EC member states.";
        public const string totalAcquisitionsExVAT = "Total value of acquisitions of goods and related costs excluding any VAT, from other EC member states.";

        public const string ImpossibleToRefreshToken = "Impossible to refresh a token";
        public const string RefreshTokenIsInvalid = "Refresh Token is invalid";
        public const string RefreshTokenIsMissing = "Refresh Token is missing";


    }
}
