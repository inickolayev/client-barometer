using System.Threading.Tasks;

namespace Ngrok.Adapter.Service
{
    public interface INgrokService
    {
        /// <summary>
        ///     Получить адрес https тунеля
        /// </summary>
        Task<string> GetHttpsTunnelUrl();
    }
}
