using Newtonsoft.Json;

namespace AssetTracking.Models
{
    public class OfficeBook
    {
        [JsonProperty(PropertyName = "BookID")]
        public string ItemId { get; set; }

        public string Title { get; set; }

        [JsonProperty(PropertyName = "Resource_x0020_IDLookupId")]
        public string ResourceId { get; set; }

        public string ISBN { get; set; }

        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "Author")]
        public string Author { get; set; }
    }
}