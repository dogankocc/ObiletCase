using Newtonsoft.Json;

namespace ObiletService.Core.Domain.Wrapper
{
    [Serializable]
    public class Response<T>
    {
        public bool IsSuccessful { get; set; }
        public bool HasExceptionError { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Exception Exception { get; set; }
        public Dictionary<string, string> ErrorMessages { get; set; }
        public int Status { get; set; }

        [JsonProperty]
        public T Result { get; set; }
        public List<T> List { get; set; }
        public Dictionary<string, T> Dictionary { get; set; }
        public T Default { get; set; }
        public object DataApi { get; set; }
        public long Count { get; set; }
        public bool IsValid => !HasExceptionError && string.IsNullOrEmpty(Message);

    }
}
