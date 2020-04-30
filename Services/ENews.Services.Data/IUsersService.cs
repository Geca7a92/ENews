namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ENews.Web.ViewModels.Administration.Users;

    public interface IUsersService
    {
        IEnumerable<IndexUserViewModel> GetAll(int? take = null, int skip = 0);

        IEnumerable<T> GetTopMembers<T>();

        IEnumerable<IndexUserViewModel> GetAllBanned(int? take = null, int skip = 0);

        int GetCountOfMembers();

        int GetCountOfUsers();

        int GetCountofActiveMembersAndUsers();

        int GetCountofBannedMembersAndUsers();

        Task UndeleteById(string id);

        Task DeleteById(string id);

        Task AddReporterRoleToUser(string userId);

        Task RemoveRepoertRoleFromUser(string userId);
    }
}
