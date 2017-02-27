using System.Collections.Generic;
using AC2C.Entites.Identity;
using cloudscribe.Web.Pagination;

namespace AC2C.Dtos.Identity
{
    public class PagedAppLogItemsDto
    {
        public PagedAppLogItemsDto()
        {
            Paging = new PaginationSettings();
        }

        public string LogLevel { get; set; } = string.Empty;

        public List<AppLogItem> AppLogItems { get; set; }

        public PaginationSettings Paging { get; set; }
    }
}