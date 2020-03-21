using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using randka.data;
using randka.Dtos;
using randka.models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace randka.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase // kontroler bez wspierania widokow poniewaz mamy agulara
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repository,IConfiguration config) // dzieki dodaniu scoped w startup ,interface wstrzyknie repozytorium
        {
            _repository = repository;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto) // przekazujemy dto z userforregister dla rejestracji
        {

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower(); // zmiaana na male litery aby sie nie mieszalo

            if (await _repository.UserExitst(userForRegisterDto.Username)) // sprawdzamy przy pomocy repo istnieje taka nazwa uzytk 
                return BadRequest("Uzytkownik o takiej nazwie istnieje"); // jesli zle to wysyla blad ...

            var usertocreate = new User // tworzymy obiekt dla klasy w modelu
            {
                Username = userForRegisterDto.Username
            };
            var createduser = await _repository.Register(usertocreate, userForRegisterDto.Password); // przekazujemy dla rejestracji obiekt z klasy modelu i password aby zahashowal itp 
            
            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var userfromrepo = await _repository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password); // przekazujemy do zmiennej metode login z repo wraz z username i haslo

            if (userfromrepo == null)
                return Unauthorized(); // nie ma dostepu jak nie istnieje

            // tworzymy token aby sprawdezac lokalnie ze jest autoryzowany dla stron z jego kontem

            //create token
            var claims = new[] // poswiadczenia dla tokena
            {
                new Claim(ClaimTypes.NameIdentifier,userfromrepo.Id.ToString()), // dla id
                new Claim(ClaimTypes.Name, userfromrepo.Username)// dla nazwy uzytkownika
            };
            // klucz dla tokena ,tworzymy z kodowania utf 8 od konfiguracji ze wstrzykiwania zaleznosci z konstruktora IConfiguration dla sekcji z appsetting.json
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // dane uwierzytalnosciowe nasz klucz dajemy i jak go kodowac

            // kryptokoder i data wygasniecia i poswiadczenia
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // poswiadczenia co utworzylismy z naszych id i uzytkownika
                Expires = DateTime.Now.AddHours(12), // data wygasniecia tokena
                SigningCredentials = creds 
            };

            

            var tokenhandler = new JwtSecurityTokenHandler();

            // tworzenie tokena

            var token = tokenhandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenhandler.WriteToken(token) }); // jak uzytk zaloguje sie prawidlowo to stworzy token i zwroci go sobie u sibie na kompie 
        }
        
    }
}
