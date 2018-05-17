using System;

namespace S2p.RestClient.Sdk.Entities
{
    public class AlternativePaymentsFilter : FilterBase
    {
        public long? pageIndex
        {
            get => GetOrAddDefault<long?>(nameof(pageIndex));
            set => Set(nameof(pageIndex), value);
        }

        public long? pageSize
        {
            get => GetOrAddDefault<long?>(nameof(pageSize));
            set => Set(nameof(pageSize), value);
        }

        public long? methodID
        {
            get => GetOrAddDefault<long?>(nameof(methodID));
            set => Set(nameof(methodID), value);
        }

        public int? statusID
        {
            get => GetOrAddDefault<int?>(nameof(statusID));
            set => Set(nameof(statusID), value);
        }

        public string country
        {
            get => GetOrAddDefault<string>(nameof(country));
            set => Set(nameof(country), value);
        }

        public string currency
        {
            get => GetOrAddDefault<string>(nameof(currency));
            set => Set(nameof(currency), value);
        }

        public string merchantTransactionID
        {
            get => GetOrAddDefault<string>(nameof(merchantTransactionID));
            set => Set(nameof(merchantTransactionID), value);
        }

        public int? minimumAmount
        {
            get => GetOrAddDefault<int?>(nameof(minimumAmount));
            set => Set(nameof(minimumAmount), value);
        }

        public int? maximumAmount
        {
            get => GetOrAddDefault<int?>(nameof(maximumAmount));
            set => Set(nameof(maximumAmount), value);
        }

        public DateTime? startDate
        {
            get => GetOrAddDefault<DateTime?>(nameof(startDate));
            set => Set(nameof(startDate), value);
        }

        public DateTime? endDate
        {
            get => GetOrAddDefault<DateTime?>(nameof(endDate));
            set => Set(nameof(endDate), value);
        }

        public string methodTransactionID
        {
            get => GetOrAddDefault<string>(nameof(methodTransactionID));
            set => Set(nameof(methodTransactionID), value);
        }

        public int? typeID
        {
            get => GetOrAddDefault<int?>(nameof(typeID));
            set => Set(nameof(typeID), value);
        }

        public long? sortBy
        {
            get => GetOrAddDefault<long?>(nameof(sortBy));
            set => Set(nameof(sortBy), value);
        }

        public string sortDirection
        {
            get => GetOrAddDefault<string>(nameof(sortDirection));
            set => Set(nameof(sortDirection), value);
        }
    }
}
