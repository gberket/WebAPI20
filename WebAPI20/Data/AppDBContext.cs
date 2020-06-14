using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI20.Models;
using WebAPI20.Models.Entities;

namespace WebAPI20.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
           : base(options)
        {
        }

        /*******************************************************************
          DB SETS
        ********************************************************************/
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }

        /*******************************************************************
          DATA SEEDING
        ********************************************************************/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().HasData(
                new Movie { Id = 1, Title = "Titanic", Genre = "Drama, Romance", YearCreated = 1997, Director = "James Cameron" },
                new Movie { Id = 2, Title = "Inception", Genre = "Action, Adventure, Sci-fi", YearCreated = 2010, Director = "Christopher Nolan" },
                new Movie { Id = 3, Title = "Gladiator", Genre = "Action, Adventure, Drama", YearCreated = 2000, Director = "Ridley Scott" },
                new Movie { Id = 4, Title = "Scarface", Genre = "Crime, Drama", YearCreated = 1983, Director = "Brian De Palma" },
                new Movie { Id = 5, Title = "The Avatar", Genre = "Action, Adventure, Sci-fi", YearCreated = 2009, Director = "James Cameron" },
                new Movie { Id = 6, Title = "Alien", Genre = "Horror, Sci-fi", YearCreated = 1979, Director = "Ridley Scott" }
            );
        }
    }
}
