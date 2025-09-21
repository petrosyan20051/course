using System.Text.Json.Serialization;

namespace gui.Services {
    public class ApiErrorResponse {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
