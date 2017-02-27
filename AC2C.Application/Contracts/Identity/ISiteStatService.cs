using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AC2C.Dtos.Identity;
using AC2C.Entites.Identity;

namespace AC2C.Application.Contracts.Identity
{
    public interface ISiteStatService
    {
        Task<List<User>> GetOnlineUsersListAsync(int numbersToTake, int minutesToTake);

        Task<List<User>> GetTodayBirthdayListAsync();

        Task UpdateUserLastVisitDateTimeAsync(ClaimsPrincipal claimsPrincipal);

        Task<AgeStatDto> GetUsersAverageAge();
    }
}