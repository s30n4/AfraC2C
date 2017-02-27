using System.Collections.Generic;
using AC2C.Common.WebToolkit;
using AC2C.Entites.Identity;

namespace AC2C.Dtos.Identity
{
    public class DynamicRoleClaimsManagerDto
    {
        public string[] ActionIds { set; get; }

        public int RoleId { set; get; }

        public Role RoleIncludeRoleClaims { set; get; }

        public ICollection<MvcControllerViewModel> SecuredControllerActions { set; get; }
    }
}