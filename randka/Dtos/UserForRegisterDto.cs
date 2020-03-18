using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace randka.Dtos
{
    public class UserForRegisterDto // przekazuje z rejestracji login i haslo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
