namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentsFilter : BaseFilter
    {
        public string pageIndex
        {
            get => GetOrAddDefault(nameof(pageIndex));
            set => PropertyDictionary[nameof(pageIndex)] = value;
        }

        public string pageSize
        {
            get => GetOrAddDefault(nameof(pageSize));
            set => PropertyDictionary[nameof(pageSize)] = value;
        }

        public string methodID
        {
            get => GetOrAddDefault(nameof(methodID));
            set => PropertyDictionary[nameof(methodID)] = value;
        }

        public string statusID
        {
            get => GetOrAddDefault(nameof(statusID));
            set => PropertyDictionary[nameof(statusID)] = value;
        }

        public string country
        {
            get => GetOrAddDefault(nameof(country));
            set => PropertyDictionary[nameof(country)] = value;
        }

        public string currency
        {
            get => GetOrAddDefault(nameof(currency));
            set => PropertyDictionary[nameof(currency)] = value;
        }

        public string merchantTransactionID
        {
            get => GetOrAddDefault(nameof(merchantTransactionID));
            set => PropertyDictionary[nameof(merchantTransactionID)] = value;
        }

        public string minimumAmount
        {
            get => GetOrAddDefault(nameof(minimumAmount));
            set => PropertyDictionary[nameof(minimumAmount)] = value;
        }

        public string maximumAmount
        {
            get => GetOrAddDefault(nameof(maximumAmount));
            set => PropertyDictionary[nameof(maximumAmount)] = value;
        }

        public string startDate
        {
            get => GetOrAddDefault(nameof(startDate));
            set => PropertyDictionary[nameof(startDate)] = value;
        }

        public string endDate
        {
            get => GetOrAddDefault(nameof(endDate));
            set => PropertyDictionary[nameof(endDate)] = value;
        }

        public string methodTransactionID
        {
            get => GetOrAddDefault(nameof(methodTransactionID));
            set => PropertyDictionary[nameof(methodTransactionID)] = value;
        }

        public string typeID
        {
            get => GetOrAddDefault(nameof(typeID));
            set => PropertyDictionary[nameof(typeID)] = value;
        }

        public string sortBy
        {
            get => GetOrAddDefault(nameof(sortBy));
            set => PropertyDictionary[nameof(sortBy)] = value;
        }

        public string sortDirection
        {
            get => GetOrAddDefault(nameof(sortDirection));
            set => PropertyDictionary[nameof(sortDirection)] = value;
        }
    }
}
