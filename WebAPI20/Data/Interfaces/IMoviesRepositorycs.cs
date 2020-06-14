using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI20.Models;

namespace WebAPI20.Data.Interfaces
{
    public interface IMoviesRepositorycs
    {
        Task<IEnumerable<Movie>> GetMovies();
        Task<PaginatedList<Movie>> GetMoviesPaginated(int pageIndex, int pageSize);
        Task<Movie> GetMovie(int id);
        Task<Movie> AddMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<Movie> DeleteMovie(int id);
    }
}
