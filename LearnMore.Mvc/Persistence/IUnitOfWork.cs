using LearnMore.Mvc.Repositories;

namespace LearnMore.Mvc.Persistence
{
    public interface IUnitOfWork
    {
        IEventRepository Events { get; }
        IAttendanceRepository Attendances { get; }
        IGenreRepository Genres { get; }
        IFollowingRepository Followings { get; }
        IApplicationUserRepository ApplicationUsers { get; }
        void Complete();
    }
}