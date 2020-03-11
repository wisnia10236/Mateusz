using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using randka.data;
using randka.models;

namespace randka.controller
{
    // jak dostac sie do kontrolera a reszta znaczy o dodawaniu itp
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly datacontext _context;

        // konstruktor aby sciagac wartosci z db
        public ValuesController(datacontext context)
        {
            _context = context;
        }
        // GET: api/Values
        [HttpGet]
        public IActionResult GetValues()
        {
            var values =_context.values.ToList(); // sciaga wartosci z bazy danych i wrzuca ja do listy
            return Ok(values);
        }

        // GET: api/Values/5
        [HttpGet("{id}")]
        public IActionResult GetValue(int id)
        {
            var value = _context.values.FirstOrDefault(x => x.id == id); // sciaga wartosc z bazy danych i zwraca ja 
            return Ok(value);
        }

        // POST: api/Values
        // dodawanie nowej wartosci
        [HttpPost]
        public IActionResult AddValue([FromBody] Value value)
        {
            _context.values.Add(value);
            _context.SaveChanges();
            return Ok(value);
        }

        // PUT: api/Values/5
        //edycja danych
        [HttpPut("{id}")]
        public IActionResult EditValue(int id, [FromBody] Value value)
        {
            var data = _context.values.Find(id); // szukamy ja w bazie i zapisujemy do zmiennej
            data.name = value.name;     // podmieniamy ja
            _context.values.Update(data);     // aktualizujemy ja
            _context.SaveChanges();     // zapisujemy zmiany
            return Ok(data);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult DeleteValue(int id)
        {
            var data = _context.values.Find(id);
            if (data == null)           // sprawdza czy jest id w bazie ,jesli nie to zwraca ze nie ma go
                return NoContent();
            _context.values.Remove(data);
            _context.SaveChanges();
            return Ok(data);
        }
    }
}
