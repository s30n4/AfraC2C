﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AC2C.Dtos.Identity
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "(*)")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه‌ی عبور فعلی")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "(*)")]
        [StringLength(100, ErrorMessage = "{0} باید حداقل {2} و حداکثر {1} حرف باشند.", MinimumLength = 6)]
        [Remote("ValidatePassword", "ChangePassword",
            AdditionalFields = DtoConstants.AntiForgeryToken, HttpMethod = "POST")]
        [DataType(DataType.Password)]
        [Display(Name = "کلمه‌ی عبور جدید")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "(*)")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار کلمه‌ی عبور جدید")]
        [Compare(nameof(NewPassword), ErrorMessage = "کلمات عبور وارد شده با هم تطابق ندارند")]
        public string ConfirmPassword { get; set; }

        public DateTimeOffset? LastUserPasswordChangeDate { get; set; }
    }
}