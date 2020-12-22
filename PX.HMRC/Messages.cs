namespace PX.HMRC
{
    public static class Messages
    {
        public const string Prefix = "HMRC Error";

        public const string Fulfilled = "Fulfilled";
        public const string Open = "Open";

        public const string VatReturnWillBeSentToHMRC = "The VAT return will be sent to HMRC. Is the VAT return finalized?";
        public const string PleaseAuthorize = "Please Authorize.";

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
        public const string VATreturnIsAccepted = "The VAT return is accepted";

        public const string vatDueSales = "VAT due on sales and other outputs"; //Box 1
        public const string vatDueAcquisitions = "VAT due in the period on acquisitions of goods made in Northern Ireland from EU Member States"; //Box 2
        public const string totalVatDue = "Total VAT due"; //Box 3
        public const string vatReclaimedCurrPeriod = "VAT reclaimed in the period on purchases and other inputs (including acquisitions in Northern Ireland from EU member states)"; //Box 4
        public const string netVatDue = "The difference between Box 3 and Box 4"; //Box 5
        public const string totalValueSalesExVAT = "Total value of sales and all other outputs excluding any VAT"; //Box 6
        public const string totalValuePurchasesExVAT = "Total value of purchases and all other inputs excluding any VAT"; //Box 7
        public const string totalValueGoodsSuppliedExVAT = "Total value of dispatches of goods and related costs (excluding VAT) from Northern Ireland to EU Member States"; //Box 8
        public const string totalAcquisitionsExVAT = "Total value of acquisitions of goods and related costs (excluding VAT) made in Northern Ireland from EU Member States"; //Box 9

        public const string vatDueSales_Notes = "Include the VAT due on all goods and services you supplied in the period covered by the return. This does not include exports or dispatches as these are zero rated. Include the VAT due in this period on imports accounted for through postponed VAT accounting"; //Box 1
        public const string vatDueAcquisitions_Notes = "For goods moved under the Northern Ireland protocol only. Show the VAT due (but not paid) on all goods and related services you acquired in this period from EU Member States"; //Box 2
        public const string totalVatDue_Notes = "Show the total amount of the VAT due ie the sum of boxes 1 and 2. This is your total Output tax."; //Box 3
        public const string vatReclaimedCurrPeriod_Notes = "Show the total amount of deductible VAT charged on your business purchases. This is referred to as your ‘input VAT’ for the period. Include the VAT reclaimed in this period on imports accounted for through postponed VAT accounting"; //Box 4
        public const string netVatDue_Notes = "Take the figures in boxes 3 and 4. Deduct the smaller from the larger and enter the difference in box 5. If this amount is under £1, you need not send any payment, nor will any repayment be made to you, but you must still fill in this form and send it to the address on page 1"; //Box 5
        public const string totalValueSalesExVAT_Notes = "Show the value excluding VAT of your total outputs (supplies of goods and services). Include zero rated, exempt outputs and EU supplies from box 8"; //Box 6
        public const string totalValuePurchasesExVAT_Notes = " Show the value excluding VAT of all your inputs (purchases of goods and services). Include zero rated, exempt inputs and EU acquisitions from box 9"; //Box 7
        public const string totalValueGoodsSuppliedExVAT_Notes = "Show the total value of all supplies of goods and related costs, excluding any VAT, to EU Member States from Northern Ireland"; //Box 8
        public const string totalAcquisitionsExVAT_Notes= "Show the total value of all acquisitions of goods and related costs, excluding any VAT, from EU Member States to Northern Ireland"; //Box 9

        public const string euNIP_Header_Notes = "EU trade under the Northern Ireland protocol only";
        public const string euNIP_Body_Notes = "Only use boxes 8 & 9 if you have supplied goods to or acquired goods from a EU Member State under the Northern Ireland protocol. Include related costs such as freight and insurance where these form part of the invoice or contract price. The figures should exclude VAT. You can find details of EU Member States in Notice 60 and Notice 725 or on our website at www.gov.uk/topic/business-tax/vat and at www.uktradeinfo.com under Intrastat";

        public const string ImpossibleToRefreshToken = "Impossible to refresh a token";
        public const string RefreshTokenIsInvalid = "Refresh Token is invalid";
        public const string RefreshTokenIsMissing = "Refresh Token is missing";
    }
}
