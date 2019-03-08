using Newtonsoft.Json;

namespace BayiPuan.MvcWebUi.GenericVM
{
    public class ListItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }
}