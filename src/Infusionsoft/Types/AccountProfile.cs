using System.Collections.Generic;
using Newtonsoft.Json;

namespace Infusionsoft
{
    public class AccountProfile
    {
        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("business_goals")]
        public List<string> BusinessGoals { get; set; }

        [JsonProperty("business_type")]
        public string BusinessType { get; set; }

        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("language_tag")]
        public string LanguageTag { get; set; }

        [JsonProperty("logo_url")]
        public string LogoUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("phone_ext")]
        public string PhoneExt { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }
    }
}