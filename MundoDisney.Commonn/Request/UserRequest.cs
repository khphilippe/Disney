using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MundoDisney.Commonn.Request
{
  public  class UserRequest
    {
        [Required]
        public string Email { get; set; }

        public string Username { get; set; }


        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string PasswordConfirm { get; set; }
    }
}
