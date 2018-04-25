﻿namespace S2p.RestClient.Sdk.Validation
{
    public class ValidationRegexConstants
    {
        public const string ID = @"^\d{1,12}$";
        public const string MerchantID = @"^\d{1,10}$";
        public const string MerchantTransactionID = @"^[0-9a-zA-Z_-]{1,50}$";
        public const string Amount = @"^\d{1,12}$";
        public const string Currency = @"^[A-Z]{3}$";
        public const string ReturnURL = @"^(http(s)?(://|%3A%2F%2F).{1,512})$";
        public const string Hash = @"^\w{1,64}$";
        public const string MethodID = @"^([0-9]{1,10})$";
        public const string MethodOptionID = @"^([0-9]{1,10})$";
        public const string CustomerName = @"^.{1,65}$";
        public const string CustomerEmail = @"^([^@]+@[^\.]+\..+){1,150}$";
        public const string IncludeMethodIDs = @"^(\d{1,10})(,\d{1,10})*$";
        public const string IncludeMethodOptionIDs = @"^(\d{1,10})(,\d{1,10})*$";
        public const string ExcludeMethodIDs = @"^(\d{1,10})(,\d{1,10})*$";
        public const string ExcludeMethodOptionIDs = @"^(\d{1,10})(,\d{1,10})*$";
        public const string PrioritizedMethodIDs = @"^(\d{1,10})(,\d{1,10})*$";
        public const string Guaranteed = @"^0|1$";
        public const string CustomerID = @"^[0-9a-zA-Z_-]{1,30}$";
        public const string Description = @"^.{1,255}$";
        public const string SkinID = @"^\w{1,50}$";
        public const string Language = @"^[a-zA-Z]{2,3}-[a-zA-Z]{2,4}(-[a-zA-Z]{2,4})*$";
        public const string Installment = @"^([1-9]|10|11|12)$";
        public const string InstallmentPagTotal = @"^([0-9]|10|11|12)$";
        public const string CustomerPhone = @"^[\+]{0,1}\d{1,15}$";
        public const string ZipCode = @"^(\d{1,9})$";
        public const string City = @"^.{1,40}$";
        public const string AccountNumber = @"^[0-9]{4,10}$";
        public const string BankCode = @"^[0-9]{8}$";
        public const string AccountNumberV2 = @"^\w{1,30}$";
        public const string BankCodeV2 = @"^\w{1,30}$";
        public const string CardNumber = @"^\d{1,20}$";
        public const string CardPIN = @"^\d{1,6}$";
        public const string CardDetailsNumber = @"^(\d{16,19})$";
        public const string CardExpirationMonth = @"^(0?[1-9]|1[0-2])$";
        public const string CardExpirationYear = @"^(20|)([0-9]{2})$";
        public const string CardSecurityCode = @"^[0-9]{3,4}$";
        public const string CardPaymentDescription = @"^(.{1,500})?$";
        public const string MaskCardNumber = "Number\"\\s*:\\s*\\\"(\\d*)";
        public const string MaskSecurityCode = "SecurityCode\"\\s*:\\s*\"\\d*";
        public const string MaskCardNumberQS = "Number=(\\d*)";
        public const string MaskSecurityCodeQS = "SecurityCode=\\d*";
        public const string MaskMerchantParameters = "MerchantParameters\"\\s*:\\s*\".*";
        public const string CustomerPhoneV2 = @"^.{1,20}$";
        public const string CustomerDetails = @"^.{1,255}$";
        public const string InstallmentV2 = @"^([1-9]{1})([0-9]{0,1})$";
        public const string CardHolderName = @"^([A-Z a-z]{1,50})$";
        public const string CardType = @"^([A-Z]{1,50})$";
        public const string Month = @"^(0[1-9]|1[012])$";
        public const string Year = @"^[0-9]{4}$";
        public const string SecurityCode = @"^[0-9]{0,4}$";
        public const string Address = @"^[0-9a-zA-Z .']{1,50}$";
        public const string CPF = @"^([0-9]{11}|[0-9]{14})$";
        public const string CustomerNameV2 = @"^.{1,30}$";
        public const string BuyerID = @"^\w{1,64}$";
        public const string MerchantPreapprovalID = @"^[0-9a-zA-Z_-]{1,50}$";
        public const string PreapprovalReturnURL = @"^(http(s)?(://|%3A%2F%2F).{1,512})$";
        public const string PreapprovalDescription = @"^.{1,100}$";
        public const string InitialPaymentID = @"^[0-9]{1,50}$";
        public const string DescriptionRefund = @"^.{1,100}$";
        public const string SiteID = @"^\d{1,10}$";
        public const string Country = @"^[a-zA-Z]{2}$";
        public const string ZipCodeV2 = @"^.{1,10}$";
        public const string CustomerPhoneV3 = @"^\d{1,15}$";
        public const string ArticleID = @"^[0-9a-zA-Z_-]{1,50}$";
        public const string ArticleName = @"^.{1,250}$";
        public const string ArticleQuantity = @"^\d{1,3}$";
        public const string ArticleType = @"^(1|2|3|4|5|6|7|8|9|10|11)$";
        public const string CustomerNameV3 = @"^.{1,100}$";
        public const string CustomerSocialSecurityNumber = @"^[0-9a-zA-Z-]{1,15}$";
        public const string CustomerGender = @"^(0|1)$";
        public const string AddressV2 = @"^.{1,512}$";
        public const string Street = @"^.{1,200}$";
        public const string ZipCodeV3 = @"^.{1,50}$";
        public const string State = @"^.{1,50}$";
        public const string BankName = @"^.{1,50}$";
        public const string SWIFT = @"^[a-zA-Z]{6}[a-zA-Z0-9]{2}([a-zA-Z0-9]{3})?$";
        public const string BankAgencyCode = @"^\d{1,10}$";
        public const string IBAN = @"^[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}$";
        public const string BIC = @"^([a-zA-Z]{4}[a-zA-Z]{2}[a-zA-Z0-9]{2}([a-zA-Z0-9]{3})?)$";
        public const string StatementDescriptor = @"^(.{1,250})?$";
        public const string ReferenceNumber = @"^[0-9a-zA-Z_-]{1,50}$";
        public const string CustomerPhoneV4 = @"^[0-9+-/.() \\]{1,15}$";
        public const string MerchantCustomerID = @"^[0-9a-zA-Z_-]{1,50}$";
        public const string CustomerCompany = @"^.{1,255}$";
        public const string StreetNumber = @"^.{1,255}$";
        public const string HouseNumber = @"^.{1,255}$";
        public const string HouseExtension = @"^.{1,255}$";
        public const string ReferenceNumberV1 = @"^[0-9a-zA-Z_-]{1,50}$";
        public const string Percent = @"^\d{1,4}$";
        public const string RecurringPeriod = @"^\d{1,5}$";
        public const string Password = "(?=^.{12,100}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_\\-+}{\":;'?/>.<,])(?!.*\\s).*$";
        public const string MerchantSiteName = @"^.{1,50}$";
        public const string MerchantSiteCountry = @"^[a-zA-Z]{2}$";
        public const string MerchantSiteAddress = @"^.{1,512}$";
        public const string MerchantSiteBankName = @"^.{1,50}$";
        public const string MerchantSiteBankAddress = @"^.{1,512}$";
        public const string MerchantSiteAccountIBAN = @"^[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}$";
        public const string MerchantSiteAccountSWIFT = @"^\w{1,30}$";
        public const string MerchantSiteBankSWIFTID = @"^[a-zA-Z]{6}[a-zA-Z0-9]{2}([a-zA-Z0-9]{3})?$";
        public const string MerchantSiteBankCode = @"^[a-zA-Z]{4}[a-zA-Z]{2}[a-zA-Z0-9]{2}[XXX0-9]{0,3}";
        public const string MerchantSiteCity = @"^.{1,50}$";
        public const string MerchantSiteEmail = @"^([^@]{1,100}@[^\.]{1,40}\..{1,8})$";
        public const string MerchantSiteVATNumber = @"^.{1,50}$";
        public const string MerchantSiteRegistrationNumber = @"^.{1,50}$";
        public const string MerchantSiteAlias = @"^.{1,255}$";
        public const string CustomerDateOfBirth = @"^(((19|20)\d\d)(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01]))$";
        public const string OriginatorTransactionID = @"^[0-9a-zA-Z_-]{1,50}$";
        public const string AddressSepaDD = @"^[\p{L}0-9a-zA-Z; .'-/,]{1,70}$";
        public const string ArticlesPrice = @"^-?\d{1,12}?$";
        public const string CustomerCPFCNPJ = @"^(?:(\d{3}[.]?\d{3}[.]?\d{3}[-]?\d{2})|(\d{2}[.]?\d{3}[.]?\d{3}[/]?\d{4}[-]?\d{2}))$";
        public const string CustomerSocialSecurityNumberGeneral = @"^.{1,50}$";
        public const string DNI_AR = @"^.{7,9}$|^.{11}$";
        public const string RUT_CLandPE = @"^.{8,9}$";
        public const string CC_CO = @"^.{6,10}$";
        public const string CURP_MX = @"^.{10,18}$";
        public const string CI_UY = @"^.{6,8}$";
        public const string ID_TR = @"^.{1,11}$";
        public const string PAN_IN = @"^.{1,25}$";
        public const string PreapprovalFrequency = @"^(daily|monthly|weekly)$";
        public const string CustomerNameV4 = @"^\s*([\p{L}]{2,}\s+([\p{L}]{1,}\s*'?\s*-?\s*[\p{L}]*\s*'?\s*-?\s*){1,})";
        public const string MerchantSiteMCC = @"^[0-9]{1,10}$";
        public const string MerchantSiteMainBusiness = @"^.{1,100}$";
        public const string StoreName = @"^.{1,50}$";
        public const string StoreID = @"^.{1,32}$";
        public const string TerminalID = @"^.{1,50}$";
    }
}
