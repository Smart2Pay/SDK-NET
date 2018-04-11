using System;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardPaymentsFilter : FilterBase
    {
        public long? limit
        {
            get => GetOrAddDefault<long?>(nameof(limit));
            set => Set(nameof(limit), value);
        }

        public long? offset
        {
            get => GetOrAddDefault<long?>(nameof(offset));
            set => Set(nameof(offset), value);
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
        
    }
}
