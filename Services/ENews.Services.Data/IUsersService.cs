namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ENews.Web.ViewModels.Administration.Users;

    public interface IUsersService
    {
        IEnumerable<IndexUserViewModel> GetAll(string sortBy, string search, int? take = null, int skip = 0);

        IEnumerable<IndexUserViewModel> GetAllReporters(int? take = 10, int skip = 0);

        IEnumerable<T> GetTopMembers<T>();

        IEnumerable<IndexUserViewModel> GetAllBanned(string sortBy, string search, int? take = null, int skip = 0);

        Task<T> GetUserByUsername<T>(string username);

        int GetCountOfMembers();

        int GetCountOfUsers();

        int GetCountofActiveAccountsByUsername(string search);

        int GetCountofBannedUsersByName(string search);

        Task UndeleteById(string id);

        Task DeleteById(string id);

        Task AddReporterRoleToUser(string userId);

        Task RemoveRepoertRoleFromUser(string userId);
    }
}
