﻿using System.ComponentModel.DataAnnotations;

namespace indetityServer.Controllers
{
    public class RegisterViewModel
    {
        public string ReturnUrl { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}