﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Domain { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
        public int SecurityLevel { get; set; }
    }
}
