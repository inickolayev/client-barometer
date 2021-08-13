using System;
using System.Threading.Tasks;
using Flurl.Http;
using Ngrok.Adapter.Data;

namespace Ngrok.Adapter.Service
{
    public class NgrokService : INgrokService
    {
        private readonly string _ngrokHost;

        public string HttpsUrl => $"http://{_ngrokHost}/api/tunnels/command_line";

        public NgrokService(string ngrokHost)
        {
            _ngrokHost = ngrokHost;
        }

        public async Task<string> GetHttpsTunnelUrl()
        {
            try
            {
                var res = await HttpsUrl.GetJsonAsync<TunnelDto>();
                return res.PublicUrl;
            }
            catch (Exception e)
            {
                throw new Exception($"Cannot connect to ngrok: {e.Message}");
            }
        }
    }
}
