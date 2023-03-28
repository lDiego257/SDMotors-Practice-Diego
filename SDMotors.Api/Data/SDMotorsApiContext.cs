using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using MySql.EntityFrameworkCore;
using SDMotors.Api.Models;

namespace SDMotors.Api.Data
{
    public class SDMotorsApiContext : DbContext
    {
        public SDMotorsApiContext (DbContextOptions<SDMotorsApiContext> options)
            : base(options)
        {

        }

        public DbSet<SDMotors.Api.Models.Marca> Marcas { get; set; } = default!;
        public DbSet<SDMotors.Api.Models.Vehiculo> Vehiculos { get; set; } = default!;
    }
}
