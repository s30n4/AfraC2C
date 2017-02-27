using System.Collections.Generic;
using AC2C.Entites.Identity;
using cloudscribe.Web.Pagination;

namespace AC2C.Dtos.Identity
{
    public class PagedUsersListDto
    {
        public PagedUsersListDto()
        {
            Paging = new PaginationSettings();
        }

        public List<User> Users { get; set; }

        public List<Role> Roles { get; set; }

        public PaginationSettings Paging { get; set; }
    }
}
