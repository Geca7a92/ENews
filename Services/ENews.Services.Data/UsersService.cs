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

        public IEnumerable<IndexUserViewModel> GetAll(string sortBy, string search, int? take = null, int skip = 0)
        {
            var users = new List<IndexUserViewModel>();
            switch (sortBy)
            {
                case "emailDesc":
                    users = this.userRepository.All()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .OrderByDescending(c => c.Email)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "emailAsc":
                    users = this.userRepository.All()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .OrderBy(c => c.Email)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "createdOnDesc":
                    users = this.userRepository.All()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .OrderByDescending(c => c.CreatedOn)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "createdOnAsc":
                    users = this.userRepository.All()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .OrderBy(c => c.CreatedOn)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "usernameAsc":
                    users = this.userRepository.All()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .OrderBy(c => c.UserName)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "usernameDesc":
                    users = this.userRepository.All()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .OrderByDescending(c => c.UserName)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                default:
                    users = this.userRepository.All()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .OrderBy(c => c.CreatedOn)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
            }

            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    user.RolesNames.Add(this.rolesService.GetRoleNameById(role.RoleId));
                }
            }

            return users;
        }

        public IEnumerable<IndexUserViewModel> GetAllBanned(string sortBy, string search, int? take = null, int skip = 0)
        {
            var users = new List<IndexUserViewModel>();

            switch (sortBy)
            {
                case "emailDesc":
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderByDescending(c => c.Email)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "emailDesc2":
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderByDescending(c => c.Email)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "emailAsc":
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderBy(c => c.Email)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "createdOnDesc":
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderByDescending(c => c.CreatedOn)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "createdOnAsc":
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderBy(c => c.CreatedOn)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "usernameAsc":
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderBy(c => c.UserName)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                case "usernameDesc":
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderByDescending(c => c.UserName)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
                default:
                    users = this.userRepository.AllWithDeleted()
                        .Where(u => search != null ? u.UserName.ToLower().Contains(search.ToLower()) && (u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any()) : u.Roles.Any(r => r.RoleId != this.rolesService.GetAdministratorId()) || !u.Roles.Any())
                        .Where(u => u.IsDeleted)
                        .OrderBy(c => c.CreatedOn)
                        .Skip(skip)
                        .Take(take.Value)
                        .To<IndexUserViewModel>()
                        .ToList();
                    break;
            }

            foreach (var user in users)
            {
                foreach (var role in user.Roles)
                {
                    user.RolesNames.Add(this.rolesService.GetRoleNameById(role.RoleId));
                }
            }

            return users;
        }

        public IEnumerable<IndexUserViewModel> GetAllReporters(int? take = 10, int skip = 0)
        {
            var roleId = this.rolesService.GetReporterId();
            var test = this.userRepository.AllWithDeleted()
                .Where(u => u.Roles.Any(r => r.RoleId == roleId)).Select(x => x.UserName).ToList();

            var users = this.userRepository.AllWithDeleted()
                .Where(u => u.Roles.Any(r => r.RoleId == roleId))
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

        //public IEnumerable<T> GetSubCategoriesOfCategoryId<T>(int id)
        //{
        //    IQueryable<SubCategory> query
        //        = this.subCategoryRepository.All().OrderBy(x => x.Title).Where(x => x.CategoryId == id);

        //    return query.To<T>().ToList();
        //}

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

        public int GetCountofActiveAccountsByUsername(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return this.userRepository.All().Count();
            }
            else
            {
                return this.userRepository.All().Where(u => u.UserName.Contains(search)).Count();
            }
        }

        public int GetCountofBannedUsersByName(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return this.userRepository.AllWithDeleted().Where(u => u.IsDeleted).Count();
            }
            else
            {
                return this.userRepository.AllWithDeleted().Where(u => u.IsDeleted && u.UserName.Contains(search)).Count();
            }
        }
    }
}
