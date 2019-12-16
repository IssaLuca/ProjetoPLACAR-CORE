using Top10Trab.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10Trab.Data
{
    public class DBTop10Contexto : DbContext
    {
        public static DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder<DBTop10Contexto>();
        public DBTop10Contexto() : base(optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DBTop10Contexto;Trusted_Connection=True;").Options)
        {
        }

        public DbSet<Jogador> Jogadores { get; set; }
        public DbSet<Placar> Placares { get; set; }
    

}
}
