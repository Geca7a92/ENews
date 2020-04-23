using ENews.Common;
using ENews.Data.Common.Repositories;
using ENews.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENews.Services.Data
{
    public interface IRolesService
    {
        string GetAdministratorId();

        string GetRoleNameById(string roleId);
    }

    public class RolesService : IRolesService
    {
        private readonly IDeletableEntityRepository<ApplicationRole> roleRepository;

        public RolesService(IDeletableEntityRepository<ApplicationRole> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public string GetAdministratorId()
        {
            return this.roleRepository.All().Where(r => r.Name == GlobalConstants.AdministratorRoleName).FirstOrDefault().Id;
        }

        public string GetRoleNameById(string roleId)
        {
            var name = this.roleRepository.GetByIdWithDeletedAsync(roleId).Result.Name;
            return name;
        }
    }
}
