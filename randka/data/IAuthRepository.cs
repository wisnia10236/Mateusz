using randka.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace randka.data
{
    public interface IAuthRepository // logika dla repozytorium dla logowania i rejestracji
    {
        Task<User> Login(string username, string password); // metoda zwraca uzytkownika dla ktorego przekazuje login i haslo
        Task<User> Register(User user, string password); // metoda rejestracji ktora przyjmuje klase user i pass aby go zarejestrowac
        Task<bool> UserExitst(string username); // sprawdza czy istnieje username taki
    }
}
