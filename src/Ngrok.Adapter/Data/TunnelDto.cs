using Newtonsoft.Json;

namespace Ngrok.Adapter.Data
{
    /// <summary>
    ///     Описание тунеля
    /// </summary>
    public class TunnelDto
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        [JsonProperty("public_url")]
        public string PublicUrl { get; set; }
        public string Proto { get; set; }
        public TunnelConfig Config { get; set; }
    }

    public class TunnelConfig
    {
        public string Addr { get; set; }
        public bool Inspect { get; set; }
    }
}
