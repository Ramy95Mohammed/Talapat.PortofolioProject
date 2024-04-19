﻿using System.ComponentModel.DataAnnotations;

namespace Talapat.Api.DTOS
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
