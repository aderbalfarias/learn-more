using LearnMore.Mvc.Core.Interfaces.Repositories;

namespace LearnMore.Mvc.Core.Interfaces.Generics
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