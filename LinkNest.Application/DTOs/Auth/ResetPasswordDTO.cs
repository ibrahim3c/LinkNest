using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkNest.Application.DTOs.Auth
{
    public class ResetPasswordDTO
    {
        public string UserId { get; set; }
        public string code { get; set; }
        public string NewPassword { get; set; }
    }
}
