using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI20.Data.Interfaces;
using WebAPI20.Models;

namespace WebAPI20.Data.Repositories
{
    public class MoviesRepository : IMoviesRepositorycs
    {
        private AppDBContext _dbContext;

        public MoviesRepository (AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedList<Movie>> GetMoviesPaginated(int pageIndex, int pageSize)
        {
            //check pagesize not to b 0!!!!!!!!!!!!!!!!!!!
            int itemsToSkip = (pageIndex - 1) * pageSize;
            int itemsToTake = pageSize;

            IQueryable<Movie> itemsQuery = _dbContext.Movies;

            int totalItems = await itemsQuery.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            List<Movie> items = await itemsQuery.Skip(itemsToSkip).Take(itemsToTake).ToListAsync();

            return new PaginatedList<Movie>()
            {
                PageIndex = pageIndex,
                TotalPages = totalPages,
                Items = items
            };
        }

        public async Task<Movie> GetMovie(int id)
        {
            return await _dbContext.Movies.FindAsync(id);

        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _dbContext.Movies.ToListAsync();
        }

        public async Task<Movie> UpdateMovie(Movie movie)
        {
            var result = await _dbContext.Movies.FirstOrDefaultAsync(e => e.Id == movie.Id);
            if (result != null)
            {
                _dbContext.Entry(result).CurrentValues.SetValues(movie);
                await _dbContext.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<Movie> AddMovie(Movie movie)
        {
            var result = await _dbContext.Movies.FirstOrDefaultAsync(e => e.Id == movie.Id);
            if (result == null)
            {
                var newMovie = _dbContext.Movies.Add(movie);
                await _dbContext.SaveChangesAsync();
                return await _dbContext.Movies.FirstOrDefaultAsync(e => e.Id == movie.Id);
            }
            return null;
        }

        public async Task<Movie> DeleteMovie(int id)
        {
            var customer = await _dbContext.Movies.FirstOrDefaultAsync(e => e.Id == id);
            if (customer != null)
            {
                _dbContext.Movies.Remove(customer);
                await _dbContext.SaveChangesAsync();
            }
            return null;
        }
    }
}
