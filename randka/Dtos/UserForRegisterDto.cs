using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace randka.Dtos
{
    public class UserForRegisterDto // przekazuje z rejestracji login i haslo
    {
        [Required(ErrorMessage ="Nazwa Uzytkownika jest wymagana")] // wymagane dla rejestracji zeby nie bylo puste
        public string Username { get; set; }
        [Required(ErrorMessage = "Haslo jest wymagane jest wymagana")]
        [StringLength(12,MinimumLength =6,ErrorMessage ="Haslo musi sie skladac od 6 do 12 znakow")] // wymagania odnosnie ilosci liter w hasle
        public string Password { get; set; }
    }
}
