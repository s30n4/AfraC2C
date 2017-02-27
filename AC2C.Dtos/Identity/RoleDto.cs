using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AC2C.Dtos.Identity
{
    public class RoleDto
    {
        [HiddenInput]
        public string Id { set; get; }

        [Required(ErrorMessage = "(*)")]
        [Display(Name = "نام نقش")]
        public string Name { set; get; }
    }
}