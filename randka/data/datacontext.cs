using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using randka.models;

namespace randka.data
{
    public class datacontext : DbContext
    {
        public datacontext(DbContextOptions<datacontext> options) : base(options) { }
        // tworzy tabele z kolumnami value
        public DbSet<Value> values { get; set; }
    }
}
