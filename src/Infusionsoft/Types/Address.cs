using Newtonsoft.Json;

namespace Infusionsoft
{
    public class Address
    {
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("line1")]
        public string Line1 { get; set; }

        [JsonProperty("line2")]
        public string Line2 { get; set; }

        [JsonProperty("locality")]
        public string Locality { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }

        [JsonProperty("zip_four")]
        public string ZipFour { get; set; }
    }
}