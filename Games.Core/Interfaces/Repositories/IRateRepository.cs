using System.Collections.Generic;
using System.Threading.Tasks;

namespace Games.Core.Interfaces.Repositories
{
    public interface IRateRepository
    {
        Task<List<long>> GetSuggestedIds(long userId);
    }
}
