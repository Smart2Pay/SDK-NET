namespace S2p.RestClient.Sdk.Validation
{
    public class ValidationResult
    {
        public string Message { get; set; } = string.Empty;
        public bool IsValid { get; set; } = true;
        public int ErrorsCount { get; set; } = 0;
    }
}
