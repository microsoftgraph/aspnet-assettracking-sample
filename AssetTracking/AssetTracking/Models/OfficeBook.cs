using Newtonsoft.Json;

namespace AssetTracking.Models
{
    public class OfficeBook
    {
        [JsonProperty(PropertyName = "id")]
        public string ItemId { get; set; }
        [JsonProperty(PropertyName = "Title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "ResourceIDLookupId")]
        public string ResourceId { get; set; }
        [JsonProperty(PropertyName ="ISBN")]
        public string ISBN { get; set; }
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }
        [JsonProperty(PropertyName = "Author0")]
        public string Author { get; set; }
    }
}