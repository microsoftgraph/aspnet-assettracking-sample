using Newtonsoft.Json;

namespace AssetTracking.Models
{
    public class OfficeItem
    {
        [JsonProperty(PropertyName = "OfficeItemID")]
        public string ItemId { get; set; }
        [JsonProperty (PropertyName ="Title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "Resource_x0020_IDLookupId")]
        public string ResourceId { get; set; }
        [JsonProperty (PropertyName ="SerialNo")]
        public string SerialNo{ get; set; }
        [JsonProperty(PropertyName = "Description")]
        public string ItemDescription { get; set; }
    }
}