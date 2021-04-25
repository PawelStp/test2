using System.Collections.Generic;
using System.Linq;

namespace Games.Api.Models.Users
{
    public class UsersPageResult : PageBaseResult<User>
    {
        public UsersPageResult(int count, int? pageIndex, IList<Core.Entities.Users.User> users)
        {
            Data = users.Select(x => User.Create(x)).ToList();
            Count = count;
            PageIndex = pageIndex ?? 1;
        }
    }
}
