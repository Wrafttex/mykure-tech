
using Newtonsoft.Json;

namespace AppLockerService.Models
{
  public partial class App
  {
    [JsonProperty("id")]
    public int Id { get; set; } 
    [JsonProperty("code")]
    public int Code { get; set; }
    [JsonProperty("name")]
    public string Name { get; set; }
    [JsonProperty("description")]
    public string Description { get; set; }
    [JsonProperty("isLocked")]
    public bool IsLocked { get; set; }
    [JsonProperty("reason")]
    public string Reason { get; set; }
  }
}
