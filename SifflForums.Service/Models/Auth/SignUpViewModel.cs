﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SifflForums.Models.Auth
{
    public class SignUpViewModel 
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}