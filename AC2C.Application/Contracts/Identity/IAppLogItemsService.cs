using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using AC2C.Dtos.Identity;

namespace AC2C.Application.Contracts.Identity
{
    public interface IAppLogItemsService
    {
        Task DeleteAllAsync(string logLevel = "");
        Task DeleteAsync(int logItemId);
        Task DeleteOlderThanAsync(DateTimeOffset cutoffDateUtc, string logLevel = "");
        Task<int> GetCountAsync(string logLevel = "");
        Task<PagedAppLogItemsDto> GetPagedAppLogItemsAsync(int pageNumber, int pageSize, SortOrder sortOrder, string logLevel = "");
    }
}