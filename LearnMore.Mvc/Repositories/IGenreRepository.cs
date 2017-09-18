using LearnMore.Mvc.Models;
using System.Collections.Generic;

namespace LearnMore.Mvc.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetGenres();
    }
}