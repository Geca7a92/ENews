using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using ENews.Services.Mapping;
using ENews.Web.ViewModels.Administration.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENews.Services.Data
{
    public interface IUsersService
    {
        IEnumerable<IndexUserViewModel> GetAll();

        IEnumerable<IndexUserViewModel> GetAllBanned();

        Task UndeleteById(string id);

        Task DeleteById(string id);

        IEnumerable<string> GetUserRoles(string userId);
    }

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;
        private readonly IRolesService rolesService;

        public UsersService(
            IDeletableEntityRepository<ApplicationUser> userRepository,
            IRolesService rolesService)
        {
            this.userRepository = userRepository;
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

        public IEnumerable<IndexUserViewModel> GetAll()
        {
            var users
                = this.userRepository.All().Where(u => u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()).OrderByDescending(c => c.CreatedOn).To<IndexUserViewModel>().ToList();

            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    user.RolesNames.Add(this.rolesService.GetRoleNameById(role.RoleId));
                }
            }

            return users;
        }

        public IEnumerable<string> GetUserRoles(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IndexUserViewModel> GetAllBanned()
        {
            var users
                = this.userRepository.AllWithDeleted().Where(u => u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()).Where(u => u.IsDeleted).OrderByDescending(c => c.CreatedOn).To<IndexUserViewModel>().ToList();

            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    user.RolesNames.Add(this.rolesService.GetRoleNameById(role.RoleId));
                }
            }

            return users;
        }
    }
}
