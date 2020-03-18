using Microsoft.AspNetCore.Mvc;
using randka.data;
using randka.Dtos;
using randka.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace randka.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase // kontroler bez wspierania widokow poniewaz mamy agulara
    {
        private readonly IAuthRepository _repository;

        public AuthController(IAuthRepository repository) // dzieki dodaniu scoped w startup ,interface wstrzyknie repozytorium
        {
            _repository = repository;
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
        
    }
}
