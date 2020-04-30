namespace ENews.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ENews.Common;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;
    using ENews.Services.Mapping;
    using ENews.Web.ViewModels.Administration.Users;
    using Microsoft.AspNetCore.Identity;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IRolesService rolesService;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            UserManager<ApplicationUser> userManager,
            IRolesService rolesService)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
            this.rolesService = rolesService;
        }

        public async Task DeleteById(string id)
        {
            var user = await this.userRepository.GetByIdWithDeletedAsync(id);
            this.userRepository.Delete(user);
            await this.userRepository.SaveChangesAsync();
        }

        public async Task UndeleteById(string id)
        {
            var user = await this.userRepository.GetByIdWithDeletedAsync(id);
            this.userRepository.Undelete(user);
            await this.userRepository.SaveChangesAsync();
        }

        public IEnumerable<IndexUserViewModel> GetAll(int? take = null, int skip = 0)
        {
            var users
                = this.userRepository.All().Where(u => u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()).OrderByDescending(c => c.CreatedOn).Skip(skip).Take(take.Value).To<IndexUserViewModel>().ToList();

            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    user.RolesNames.Add(this.rolesService.GetRoleNameById(role.RoleId));
                }
            }

            return users;
        }

        public IEnumerable<IndexUserViewModel> GetAllBanned(int? take = null, int skip = 0)
        {
            var users
                = this.userRepository.AllWithDeleted()
                .Where(u => u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                .Where(u => u.IsDeleted)
                .OrderByDescending(c => c.CreatedOn)
                .Skip(skip)
                .Take(take.Value)
                .To<IndexUserViewModel>()
                .ToList();

            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    user.RolesNames.Add(this.rolesService.GetRoleNameById(role.RoleId));
                }
            }

            return users;
        }

        public async Task AddReporterRoleToUser(string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(u => u.Id == userId);

            await this.userManager.AddToRoleAsync(user, GlobalConstants.ReporterRoleName);
        }

        public async Task RemoveRepoertRoleFromUser(string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(u => u.Id == userId);

            await this.userManager.RemoveFromRoleAsync(user, GlobalConstants.ReporterRoleName);
        }

        public IEnumerable<T> GetTopMembers<T>()
        {
            var users = this.userRepository.All().Where(u => u.Roles.Any()).OrderByDescending(u => u.Articles.Count()).Take(4);

            return users.To<T>().ToList();
        }

        public int GetCountOfMembers()
        {
            return this.userRepository.All().Where(a => a.Roles.Any()).Count();
        }

        public int GetCountOfUsers()
        {
            return this.userRepository.All().Where(a => !a.Roles.Any()).Count();
        }

        public int GetCountofActiveMembersAndUsers()
        {
            return this.userRepository.All().Count();
        }

        public int GetCountofBannedMembersAndUsers()
        {
            return this.userRepository.AllWithDeleted().Where(u => u.IsDeleted).Count();
        }
    }
}
