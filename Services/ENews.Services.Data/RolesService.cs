namespace ENews.Services.Data
{
    using System.Linq;

    using ENews.Common;
    using ENews.Data.Common.Repositories;
    using ENews.Data.Models;

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

        public string GetReporterId()
        {
            var res = this.roleRepository.AllWithDeleted().FirstOrDefault(r => r.Name == GlobalConstants.ReporterRoleName).Id;
            return res;
        }

        public string GetRoleNameById(string roleId)
        {
            var name = this.roleRepository.GetByIdWithDeletedAsync(roleId).Result.Name;
            return name;
        }
    }
}
