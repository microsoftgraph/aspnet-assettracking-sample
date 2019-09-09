using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AssetTracking.Models
{
    public class OfficeBook
    {
        [JsonProperty(PropertyName = "id")]
        public string ItemId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [JsonProperty(PropertyName = "ResourceIDLookupId")]
        public string ResourceId { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; }

        [Required]
        [JsonProperty(PropertyName = "Author0")]
        public string Author { get; set; }
    }
}