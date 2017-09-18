using LearnMore.Mvc.Models;
using LearnMore.Mvc.Repositories;

namespace LearnMore.Mvc.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEventRepository Events { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Events = new EventRepository(context);
            Attendances = new AttendanceRepository(context);
            Genres = new GenreRepository(context);
            Followings = new FollowingRepository(context);
            ApplicationUsers = new ApplicationUserRepository(context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}