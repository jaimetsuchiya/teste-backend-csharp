using System.Threading.Tasks;

namespace Infrastructure.TorreHanoi.ServiceAgent
{
    public interface ISlackServiceAgent
    {
        Task<bool> Post(string mensgem);
    }
}