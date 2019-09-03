using Newtonsoft.Json;
using System;

namespace AssetTracking.Models
{
    public class BorrowedResources
    {
        [JsonProperty(PropertyName = "BorrowedResourceID")]
        public string ItemId { get; set; }
        public string BorrowedResourceID { get; set; }
        [JsonProperty(PropertyName = "ResourceID")]
        public string ResourceID { get; set; }
        [JsonProperty(PropertyName = "BorrowDate")]
        public DateTime BorrowDate { get; set; }
        [JsonProperty(PropertyName = "ReturnDate")]
        public DateTime ReturnDate { get; set; }
        [JsonProperty(PropertyName = "MemberID")]
        public string MemberID { get; set; }
        [JsonProperty(PropertyName = "ISBN")]
        public string ISBN { get; set; }
        [JsonProperty(PropertyName = "BooktTitle")]
        public string BookTitle { get; set; }
        [JsonProperty(PropertyName = "Author0")]
        public string Author { get; set; }
        [JsonProperty(PropertyName = "DueDate")]
        public DateTime DueDate { get; set; }
    }
}
