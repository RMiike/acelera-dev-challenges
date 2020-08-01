using Newtonsoft.Json;

namespace Codenation.Challenge.Models
{
    public class QuoteView
    {
        public QuoteView(long id, string actor, string detail)
        {
            Id = id;
            Actor = actor;
            Detail = detail;
        }

        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("actor")]
        public string Actor { get; set; }

        [JsonProperty("quote")]
        public string Detail { get; set; }

    }
}