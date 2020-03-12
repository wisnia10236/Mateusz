using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        /*  kod synchorniczny 
       [HttpGet]
        public IActionResult GetValues()
        {
            var values =_context.values.ToList(); // sciaga wartosci z bazy danych i wrzuca ja do listy
            return Ok(values);
        }
        */
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values = await _context.values.ToListAsync(); // sciaga wartosci z bazy danych i wrzuca ja do listy asynchronicznie
            return Ok(values);
        }

        // GET: api/Values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await _context.values.FirstOrDefaultAsync(x => x.id == id); // sciaga wartosc z bazy danych i zwraca ja async
            return Ok(value);
        }

        // POST: api/Values
        // dodawanie nowej wartosci
        [HttpPost]
        public async Task<IActionResult> AddValue([FromBody] Value value)
        {
            _context.values.Add(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        // PUT: api/Values/5
        //edycja danych
        [HttpPut("{id}")]
        public async Task<IActionResult> EditValue(int id, [FromBody] Value value)
        {
            var data = await _context.values.FindAsync(id); // szukamy ja w bazie i zapisujemy do zmiennej
            data.name = value.name;     // podmieniamy ja
            _context.values.Update(data);     // aktualizujemy ja
            await _context.SaveChangesAsync();     // zapisujemy zmiany
            return Ok(data);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteValue(int id)
        {
            var data = await _context.values.FindAsync(id);
            if (data == null)           // sprawdza czy jest id w bazie ,jesli nie to zwraca ze nie ma go
                return NoContent();
            _context.values.Remove(data);
            await _context.SaveChangesAsync();
            return Ok(data);
        }
    }
}
