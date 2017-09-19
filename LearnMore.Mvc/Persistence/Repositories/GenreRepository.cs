using System.Collections.Generic;
using System.Linq;
using LearnMore.Mvc.Core.Interfaces.Repositories;
using LearnMore.Mvc.Core.Models;

namespace LearnMore.Mvc.Persistence.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}